var tablaUsuario;

$(document).ready(function () {
    // Cargar roles
    $.ajax({
        url: getListaRolURL,
        type: "GET",
        dataType: "json",
        success: function (data) {
            $("#cboRol").html("");
            if (data.data) {
                $.each(data.data, function (i, item) {
                    $("<option>").val(item.IdRol).text(item._Rol).appendTo("#cboRol");
                });
            }
        }
    });

    // Inicializar DataTable
    tablaUsuario = $('#tbUsuario').DataTable({
        "ajax": {
            "url": getListaUsuarioURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "_Usuario" },
            {
                "data": "_Rol", render: function (data) {
                    return data ? data._Rol : "Sin Rol"; // Mostramos el nombre del rol o "Sin Rol" si es null
                }
            },
            {
                "data": "Estado", render: function (data) {
                    return data === 1 ? "Activo" : "No Activo";
                }
            },
            {
                "data": "IdUsuario", render: function (data, type, row) {
                    return "<button class='btn btn-primary btn-sm' onclick='abrirPopUpFormUsuario(" + JSON.stringify(row) + ")'>Editar</button>";
                },
                "orderable": false,
                "searchable": false
            }
        ],
        "language": { "url": getDatatableSpanish }
    });
});

// Función para abrir el modal y cargar datos en caso de edición
function abrirPopUpFormUsuario(json) {
    $("#txtIdUsuario").val(0);
    $("#txtUsuario, #txtContrasenia").val("");
    $("#cboEstado").val(1);

    if (json) {
        $("#txtIdUsuario").val(json.IdUsuario);
        $("#txtUsuario").val(json._Usuario);
        $("#cboRol").val(json._Rol ? json._Rol.IdRol : 0); // Asigna el IdRol del rol si está presente
        $("#cboEstado").val(json.Estado);
    }

    $('#FormModal').modal('show');
}

// Función para guardar el usuario
function GuardarUsuario() {
    var usuario = {
        IdUsuario: $("#txtIdUsuario").val(),
        _Usuario: $("#txtUsuario").val(),
        Contrasenia: $("#txtContrasenia").val(),
        IdRol: $("#cboRol").val(),
        Estado: $("#cboEstado").val()
    };

    $.ajax({
        url: postCrearUsuarioURL,
        type: "POST",
        data: JSON.stringify(usuario),
        contentType: "application/json",
        success: function (data) {
            if (data.data === true) {
                tablaUsuario.ajax.reload();
                $('#FormModal').modal('hide');
                swal("Usuario creado exitosamente", "", "success");
            } else {
                swal("No se pudo crear el usuario", "", "warning");
            }
        },
        error: function (error) {
            console.log("Error en la solicitud:", error);
            swal("Error en la comunicación con el servidor", "", "error");
        }
    });
}
