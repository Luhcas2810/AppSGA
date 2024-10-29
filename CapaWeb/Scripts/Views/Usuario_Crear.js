var tablausuario;
$(document).ready(function () {
    //OBTENER ROLES
    jQuery.ajax({
        url: getListaRolURL,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#cboRol").html("");

            if (data.data != null) {
                $.each(data.data, function (i, item) {
                    $("<option>").attr({ "value": item.IdRol }).text(item._Rol).appendTo("#cboRol");
                })
                $("#cboRol").val($("#cboRol option:first").val());
            }
        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
        },
    });
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
            { "data": "_Usuario" },
            {
                "data": "_Rol", render: function (data) {
                    return data._Rol
                }
            },
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
        $("#txtUsuario").val(json._Usuario);
        $("#txtClave").val(json.Contrasenia);
        $("#cboRol").val(json._Rol.IdRol);
        $("#cboEstado").val(json.Activo == true ? 1 : 0);
        $("#txtClave").prop("disabled", true);
    } else {
        $("#txtUsuario").val("");
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
                _Usuario: $("#txtUsuario").val(),
                Contrasenia: $("#txtClave").val(),
                IdRol: parseInt($("#cboRol").val()),
                Estado: parseInt($("#cboEstado").val())
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
