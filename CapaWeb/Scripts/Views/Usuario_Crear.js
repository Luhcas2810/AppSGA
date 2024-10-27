var tablausuario;
$(document).ready(function () {
    tablausuario = $('#tbUsuario').DataTable({
        "ajax": {
            "url": getListaUsuarioURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{
            //    "data": "IdUsuario", render: function (data) {
            //        return data.Descripcion
            //    }
            //},
            { "data": "Nombre" },
            { "data": "Rol" },
            { "data": "Estado" }
            //{
            //    "data": "Activo", "render": function (data) {
            //        if (data) {
            //            return '<span class="badge badge-success">Activo</span>'
            //        } else {
            //            return '<span class="badge badge-danger">No Activo</span>'
            //        }
            //    }
            //},
            //{
            //    "data": "IdUsuario", "render": function (data, type, row, meta) {
            //        return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
            //            "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
            //    },
            //    "orderable": false,
            //    "searchable": false,
            //    "width": "90px"
            //}

        ],
        "language": {
            "url": getDatatableSpanish
        },
        responsive: true
    });
});
function abrirPopUpForm(json) {
    $("#txtid").val(0);
    if (json != null) {
        $("#txtid").val(json.IdUsuario);
        $("#txtNombres").val(json.Nombres);
        $("#txtCorreo").val(json.Correo);
        $("#txtClave").val(json.Clave);
        $("#cboRol").val(json.IdRol);
        $("#cboEstado").val(json.Activo == true ? 1 : 0);
        $("#txtClave").prop("disabled", true);
    } else {
        $("#txtNombres").val("");
        $("#txtCorreo").val("");
        $("#txtClave").val("");
        $("#cboRol").val($("#cboRol option:first").val());
        $("#cboEstado").val(1);
        $("#txtClave").prop("disabled", false);
    }
    $('#FormModal').modal('show');

}
function Guardar() {
    //alert("Si pasa");
    if ($("#form").valid()) {

        var request = {
            usuario: {
                IdUsuario: $("#txtid").val(),
                Nombre: $("#txtNombres").val(),
                Correo: $("#txtCorreo").val(),
                Contrasenia: $("#txtClave").val(),
                Rol: $("#cboRol").val(),
                Estado: parseInt($("#cboEstado").val()) == 1 ? true : false
            }
        }

        jQuery.ajax({
            url: postActualizarUsuarioURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.respuesta) {
                    tablausuario.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente al usuario", "success");
                } else {
                    swal("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }

}
