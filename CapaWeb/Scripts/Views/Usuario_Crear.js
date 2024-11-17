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
    $('#cboRol').on('change', function () {
        var Id = $(this).val();
        if (Id == 3) {
            document.getElementById("lblEscDep").innerText = "Escuela";
            $.ajax({
                url: getListaEscuelaURL,
                type: "GET",
                dataType: "json",
                success: function (data) {
                    $("#cboEscDep").html("");
                    if (data.data) {
                        $.each(data.data, function (i, item) {
                            $("<option>").val(item.Codigo).text(item.Carrera).appendTo("#cboEscDep");
                        });
                    }
                }
            });
        }
        else {
            document.getElementById("lblEscDep").innerText = "Departamento";
            $.ajax({
                url: getListaDepartamentoURL,
                type: "GET",
                dataType: "json",
                success: function (data) {
                    $("#cboEscDep").html("");
                    if (data.data) {
                        $.each(data.data, function (i, item) {
                            $("<option>").val(item.Codigo).text(item.Descripcion).appendTo("#cboEscDep");
                        });
                    }
                }
            });
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
                "data": "Activo", render: function (data) {
                    return data ? "<span class='badge badge-success'>Activo</span>" : "<span class='badge badge-danger'>Inactivo</span>";
                }
            },
            {
                "data": "IdUsuario", render: function (data, type, row) {
                    return "<button class='btn btn-primary btn-sm' onclick='abrirPopUpFormUsuario(" + JSON.stringify(row) + ")'><i class='bi bi-pencil-square'></i></button> <button class='btn btn-" + row.Color + " btn-sm' onclick='cambiarEstado(" + JSON.stringify(row) + ")'><i class='bi bi-" + row.Icono + "-fill'></i></button>";
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
function cambiarEstado(json) {
    var Codigo = json.Codigo;
    jQuery.ajax({
        url: postCambiarEstadoUsuarioURL,
        type: "POST",
        data: JSON.stringify({ CodigoUsuario: Codigo }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.data.Respuesta === true) {
                tablaUsuario.ajax.reload();
                //swal("Mensaje positivo", data.data.Mensaje, "success");
            } else {
                swal("Mensaje negativo", data.data.Mensaje, "warning");
            }
        },
        error: function (error) {
            console.log("Error en la solicitud:", error);
            swal("Error en la comunicación con el servidor", "", "error");
        }
    });
}
function abrirPopUpFormUsuario(json) {
    if (json) {
        $("#txtCodigo").val(json.Codigo);
        //$("#txtUsuario").val(json._Usuario);
        //$("#txtUsuario").prop("readonly", true);
        $("#cboRol").val(json._Rol.Codigo);
        $("#cboRol").prop("disabled", true);
        $("#txtContrasenia").val('');
        $("#txtNombre").val(json.Nombre);
        $("#txtNombre").prop("readonly", true);
        $("#txtApellido").val(json.Apellido);
        $("#txtApellido").prop("readonly", true);
        $("#txtIdentificacion").val(json.Identificacion);
        $("#txtIdentificacion").prop("readonly", true);
        /*$("#txtCorreo").val(json.Correo);*/
        $("#txtTelefono").val(json.Telefono);
        $("#txtDireccion").val(json.Direccion);
        $("#dtFechaNacimiento").val(json._fechaNacimiento);
        $("#dtFechaNacimiento").prop("disabled", true);
        $("#cboEscDep").val(json.EscDep);
        $("#cboEscDep").prop("disabled", true);
    }
    else {
        $("#txtCodigo").val(0);
        //$("#txtUsuario").val('');
        //$("#txtUsuario").prop("readonly", false);
        $("#cboRol").val(1);
        $("#cboRol").prop("disabled", false);
        $("#txtContrasenia").val('');
        $("#txtNombre").val('');
        $("#txtNombre").prop("readonly", false);
        $("#txtApellido").val('');
        $("#txtApellido").prop("readonly", false);
        $("#txtIdentificacion").val('');
        $("#txtIdentificacion").prop("readonly", false);
        /*$("#txtCorreo").val('');*/
        $("#txtTelefono").val('');
        $("#txtDireccion").val('');
        $("#dtFechaNacimiento").val(ObtenerFecha());
        $("#dtFechaNacimiento").prop("disabled", false);
        $("#cboEscDep").val(1);
        $("#cboEscDep").prop("disabled", false);
    }
    $('#cboRol').trigger('change');
    $('#FormModal').modal('show');
}
function GuardarUsuario() {
    //console.log("postCrearUsuarioURL:", postCrearUsuarioURL);
    var request = {
        usuario : {
            Codigo: $("#txtCodigo").val(),
            CodigoRol: $("#cboRol").val(),
            /*_Usuario: $("#txtUsuario").val(),*/
            Contrasenia: $("#txtContrasenia").val(),
            Nombre: $("#txtNombre").val(),
            Apellido: $("#txtApellido").val(),
            Identificacion: $("#txtIdentificacion").val(),
            /*Correo: $("#txtCorreo").val(),*/
            Telefono: $("#txtTelefono").val(),
            Direccion: $("#txtDireccion").val(),
            FechaNacimiento: $("#dtFechaNacimiento").val() + "T00:00:00",
            EscDep: $("#cboEscDep").val()
        }
    }
    console.log(request);
    jQuery.ajax({
        url: postGuardarUsuarioURL,
        type: "POST",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.data.Respuesta === true) {
                tablaUsuario.ajax.reload();
                $('#FormModal').modal('hide');
                swal("Mensaje positivo", data.data.Mensaje, "success");
            } else {
                swal("Mensaje negativo", data.data.Mensaje, "warning");
            }
        },
        error: function (error) {
            console.log("Error en la solicitud:", error);
            swal("Error en la comunicación con el servidor", "", "error");
        }
    });
}
function ObtenerFecha() {
    let today = new Date();
    let day = String(today.getDate()).padStart(2, '0');
    let month = String(today.getMonth() + 1).padStart(2, '0');
    let year = today.getFullYear();
    let formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
}