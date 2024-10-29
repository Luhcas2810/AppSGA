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
            { "data": "_Rol" },
            {
                "data": "IdRol", "render": function (data, type, row) {
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
    $("#txtIdRol").val(0);
    if (json != null) {
        $("#txtIdRol").val(json.IdRol);
        $("#txtRol").val(json._Rol);
    } else {
        $("#txtRol").val("");
    }
    $('#FormModal').modal('show');
}

// Función para guardar el rol
function GuardarRol() {
    if ($("#form").valid()) {
        var request = {
            rol: {
                IdRol: $("#txtIdRol").val(),
                _Rol: $("#txtRol").val()
            }
        };

        jQuery.ajax({
            url: postCrearRolURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.respuesta) {
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
