var tablaRol;

$(document).ready(function () {
    // Inicializar DataTable para roles
    tablaRol = $('#tbRol').DataTable({
        "ajax": {
            "url": getListaRolURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Descripcion" },
            {
                "data": "Codigo", "render": function (data, type, row) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpFormRol(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>";
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
function abrirPopUpFormRol(json) {
    if (json != null) {
        $("#txtIdRol").val(json.Codigo);
        $("#txtRol").val(json.Descripcion);
    } else {
        $("#txtIdRol").val(0);
        $("#txtRol").val("");
    }
    $('#FormModal').modal('show');
}

// Función para guardar el rol
function GuardarRol() {
    if ($("#form").valid()) {
        var request = {
            rol: {
                Codigo: $("#txtIdRol").val(),
                Descripcion: $("#txtRol").val()
            }
        };

        jQuery.ajax({
            url: postCrearRolURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (data.data === true) { // Cambiar data.respuesta a data.data
                    console.log(data.data);
                    tablaRol.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el rol", "success");
                } else {
                    swal("Mensaje", "No se pudo guardar el rol", "warning");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}
