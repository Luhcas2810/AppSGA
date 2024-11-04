var tablaAula;

$(document).ready(function () {
    // Inicializar DataTable para aulas
    tablaAula = $('#tbAula').DataTable({
        "ajax": {
            "url": getListaAulaURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            { "data": "Capacidad" },
            {
                "data": "IdAula", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpFormAula(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>";
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
function abrirPopUpFormAula(json) {
    $("#txtIdAula").val(0);
    if (json != null) {
        $("#txtIdAula").val(json.IdAula);
        $("#txtNombre").val(json.Nombre);
        $("#txtCapacidad").val(json.Capacidad);
    } else {
        $("#txtNombre").val("");
        $("#txtCapacidad").val("");
    }
    $('#FormModal').modal('show');
}

// Función para guardar o actualizar el aula
function GuardarAula() {
    if ($("#form").valid()) {
        var request = {
            aula: {
                IdAula: $("#txtIdAula").val(),
                Nombre: $("#txtNombre").val(),
                Capacidad: $("#txtCapacidad").val()
            }
        };

        jQuery.ajax({
            url: postActualizarAulaURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.respuesta == true) {
                    tablaAula.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el aula", "success");
                } else {
                    swal("Mensaje", "No se pudo guardar los cambios", "warning");
                }
            },
            error: function (error) {
                console.log("Error en la solicitud:", error);
                swal("Mensaje", "Ocurrió un error en la comunicación con el servidor", "error");
            }
        });
    }
}
