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
            { "data": "IdRol" },
            {
                "data": "Estado", "render": function (data) {
                    return data === 1 ? "Activo" : "No Activo";
                }
            },
            {
                "data": "IdUsuario", "render": function (data, type, row) {
                    return "<button class='btn btn-primary btn-sm' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'>Editar</button>";
                },
                "orderable": false,
                "searchable": false
            }
        ],
        "language": { "url": getDatatableSpanish }
    });
});

function abrirPopUpFormUsuario(json) {
    $("#txtIdUsuario").val(0);
    $("#txtUsuario, #txtContrasenia").val("");
    $("#cboEstado").val(1);

    if (json) {
        $("#txtIdUsuario").val(json.IdUsuario);
        $("#txtUsuario").val(json._Usuario);
        $("#cboRol").val(json.IdRol);
        $("#cboEstado").val(json.Estado);
    }

    $('#FormModal').modal('show');
}

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
            if (data.data == true) {
                tablaUsuario.ajax.reload();
                $('#FormModal').modal('hide');
                swal("Usuario creado exitosamente", "", "success");
            } else {
                swal("No se pudo crear el usuario", "", "warning");
            }
        }
    });
}
