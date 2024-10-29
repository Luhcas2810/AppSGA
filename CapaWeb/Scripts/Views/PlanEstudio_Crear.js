var tablaPlanEstudio;

$(document).ready(function () {
    // Obtener lista de programas
    jQuery.ajax({
        url: getListaProgramaURL,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#cboPrograma").html("");
            if (data.data != null) {
                $.each(data.data, function (i, item) {
                    $("<option>").attr({ "value": item.IdPrograma }).text(item.Carrera).appendTo("#cboPrograma");
                });
                $("#cboPrograma").val($("#cboPrograma option:first").val());
            }
        },
        error: function (error) {
            console.log(error);
        }
    });

    // Inicializar DataTable para planes de estudio
    tablaPlanEstudio = $('#tbPlanEstudio').DataTable({
        "ajax": {
            "url": getListaPlanEstudioURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Semestre" },
            {
                "data": "_Programa", render: function (data) {
                    return data.Carrera;
                }
            },
            {
                "data": "Estado", "render": function (data) {
                    return data === 1 ? '<span class="badge badge-success">Activo</span>' : '<span class="badge badge-danger">No Activo</span>';
                }
            },
            {
                "data": "IdPlan", "render": function (data, type, row) {
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
    $("#txtIdPlan").val(0);
    if (json != null) {
        $("#txtIdPlan").val(json.IdPlan);
        $("#txtSemestre").val(json.Semestre);
        $("#cboPrograma").val(json._Programa.IdPrograma);
        $("#cboEstado").val(json.Estado === 1 ? 1 : 0);
    } else {
        $("#txtSemestre").val("");
        $("#cboPrograma").val($("#cboPrograma option:first").val());
        $("#cboEstado").val(1); // Por defecto, estado activo
    }
    $('#FormModal').modal('show');
}

// Función para guardar o actualizar el plan de estudio
function GuardarPlanEstudio() {
    if ($("#form").valid()) {
        var request = {
            planEstudio: {
                IdPlan: $("#txtIdPlan").val(),
                Semestre: $("#txtSemestre").val(),
                IdPrograma: parseInt($("#cboPrograma").val()),
                Estado: parseInt($("#cboEstado").val())
            }
        };

        jQuery.ajax({
            url: postActualizarPlanEstudioURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.respuesta) {
                    tablaPlanEstudio.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el plan de estudio", "success");
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
