var tablaCurso;

$(document).ready(function () {
    // Obtener lista de planes de estudio
    jQuery.ajax({
        url: getListaPlanURL,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#cboPlan").html("");
            if (data.data != null) {
                $.each(data.data, function (i, item) {
                    $("<option>").attr({ "value": item.IdPlan }).text(item.Nombre).appendTo("#cboPlan");
                });
                $("#cboPlan").val($("#cboPlan option:first").val());
            }
        },
        error: function (error) {
            console.log(error);
        }
    });

    // Inicializar DataTable para cursos
    tablaCurso = $('#tbCurso').DataTable({
        "ajax": {
            "url": getListaCursoURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            { "data": "IdCurso" },  // Cambié "Codigo" por "IdCurso"
            { "data": "IdPlan" },
            { "data": "Creditos" },
            {
                "data": "IdCurso", "render": function (data, type, row) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>";
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }
        ],
        "language": {
            "url": getDatatableSpanish
        },
        responsive: true
    });
});

// Función para abrir el modal y cargar datos en caso de edición
function abrirPopUpForm(json) {
    $("#txtIdCurso").val(0);
    if (json != null) {
        $("#txtIdCurso").val(json.IdCurso);
        $("#txtNombre").val(json.Nombre);
        $("#cboPlan").val(json.IdPlan);
        $("#txtCreditos").val(json.Creditos);
    } else {
        $("#txtNombre").val("");
        $("#cboPlan").val($("#cboPlan option:first").val());
        $("#txtCreditos").val("");
    }
    $('#FormModal').modal('show');
}

// Función para guardar o actualizar el curso
function GuardarCurso() {
    if ($("#form").valid()) {
        var request = {
            curso: {
                IdCurso: $("#txtIdCurso").val(),
                Nombre: $("#txtNombre").val(),
                IdPlan: parseInt($("#cboPlan").val()),
                Creditos: parseInt($("#txtCreditos").val())
            }
        };

        jQuery.ajax({
            url: postActualizarCursoURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.respuesta) {
                    tablaCurso.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el curso", "success");
                } else {
                    swal("Mensaje", "No se pudo guardar los cambios", "warning");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}
