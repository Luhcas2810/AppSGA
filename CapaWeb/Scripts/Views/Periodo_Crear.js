var tablaPeriodo;

$(document).ready(function () {
    // Inicializar DataTable para períodos
    tablaPeriodo = $('#tbPeriodo').DataTable({
        "ajax": {
            "url": getListaPeriodoURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            {
                "data": "FechaInicio",
                "render": function (data) {
                    return convertDateDotNet(data); // Convierte la fecha de .NET al formato deseado
                }
            },
            {
                "data": "FechaFin",
                "render": function (data) {
                    return convertDateDotNet(data); // Convierte la fecha de .NET al formato deseado
                }
            },
            {
                "data": "IdPeriodo",
                "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpFormPeriodo(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>";
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

// Función para convertir la fecha en formato .NET a yyyy-MM-dd
function convertDateDotNet(dotNetDate) {
    // Extrae los milisegundos del formato /Date(ticks)/
    var timestamp = parseInt(dotNetDate.match(/\d+/)[0]);
    var fecha = new Date(timestamp);

    // Formatear como yyyy-MM-dd
    var year = fecha.getFullYear();
    var month = String(fecha.getMonth() + 1).padStart(2, '0');
    var day = String(fecha.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
}

// Función para abrir el modal y cargar datos en caso de edición
function abrirPopUpFormPeriodo(json) {
    $("#txtIdPeriodo").val(0);
    if (json != null) {
        $("#txtIdPeriodo").val(json.IdPeriodo);
        $("#txtNombre").val(json.Nombre);
        $("#dtFechaInicio").val(convertDateDotNet(json.FechaInicio)); // Aplicar formato
        $("#dtFechaFin").val(convertDateDotNet(json.FechaFin));       // Aplicar formato
    } else {
        $("#txtNombre").val("");
        $("#dtFechaInicio").val("");
        $("#dtFechaFin").val("");
    }
    $('#FormModal').modal('show');
}

// Función para guardar o actualizar el período
function GuardarPeriodo() {
    if ($("#form").valid()) {
        var request = {
            periodo: {
                IdPeriodo: $("#txtIdPeriodo").val(),
                Nombre: $("#txtNombre").val(),
                FechaInicio: $("#dtFechaInicio").val(),
                FechaFin: $("#dtFechaFin").val()
            }
        };

        jQuery.ajax({
            url: postActualizarPeriodoURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.respuesta == true) {
                    tablaPeriodo.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el período", "success");
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