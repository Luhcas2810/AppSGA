var tablaUsuario;

$(document).ready(function () {
    $.ajax({
        url: getListaRolURL,
        type: "GET",
        dataType: "json",
        success: function (data) {
            $("#cboRol").html("");
            if (data.data) {
                $.each(data.data, function (i, item) {
                    $("<option>").val(item.Codigo).text(item.Descripcion).appendTo("#cboRol");
                });
            }
        }
    });
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
    $("#txtFechaNacimiento").datepicker();
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
                    return data.Descripcion;
                }
            },
            { "data": "Nombre" },
            { "data": "Apellido" },
            { "data": "Correo" },
            {
                "data": "IdUsuario", render: function (data, type, row) {
                    return "<button class='btn btn-primary btn-sm' onclick='abrirPopUpFormUsuario(" + JSON.stringify(row) + ")'>Editar</button>";
                },
                "orderable": false,
                "searchable": false
            }
            //{
            //    "data": "Estado", render: function (data) {
            //        return data === 1 ? "Activo" : "No Activo";
            //    }
            //},
        ],
        "language": { "url": getDatatableSpanish }
    });
});
function abrirPopUpFormUsuario(json) {
    if (json) {
        $("#txtIdUsuario").val(json.Codigo);
        $("#txtUsuario").val(json._Usuario);
        $("#cboRol").val(json.CodigoRol);
        $("#txtContrasenia").val(json.Contrasenia);
        $("#txtNombre").val(json.Nombre);
        $("#txtApellido").val(json.Apellido);
        $("#txtIdentificacion").val(json.Identificacion);
        $("#txtCorreo").val(json.Correo);
        $("#txtTelefono").val(json.Telefono);
        $("#txtDireccion").val(json.Direccion);
        $("#txtFechaNacimiento").val(ObtenerFecha(json.FechaNacimiento));
    }
    else {
        $("#txtIdUsuario").val(0);
        $("#txtUsuario").val('');
        $("#cboRol").val(1);
        $("#txtContrasenia").val('');
        $("#txtNombre").val('');
        $("#txtApellido").val('');
        $("#txtIdentificacion").val('');
        $("#txtCorreo").val('');
        $("#txtTelefono").val('');
        $("#txtDireccion").val('');
        $("#txtFechaNacimiento").val(ObtenerFecha(null));
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
function ObtenerFecha(date) {
    if (!date) {
        date = new Date();
    }
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var output = (('' + day).length < 2 ? '0' : '') + day + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + date.getFullYear();
    return output;
}
