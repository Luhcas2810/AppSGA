var tablaprograma;
$(document).ready(function () {
    tablaprograma = $('#tbPrograma').DataTable({
        "ajax": {
            "url": getListaProgramaURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{
            //    "data": "IdUsuario", render: function (data) {
            //        return data.Descripcion
            //    }
            //},
            { "data": "Carrera" },
            { "data": "Duracion" }
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
function abrirPopUpFormPrograma(json) {
    $("#txtid").val(0);
    if (json != null) {
        $("#txtIdPrograma").val(json.IdPrograma);
        $("#txtCarrera").val(json.Carrera);
        $("#intDuracion").val(json.Duracion);
    } else {
        $("#txtIdPrograma").val(0);
        $("#txtCarrera").val("");
        $("#intDuracion").val(1);
    }
    $('#FormModal').modal('show');
}
function GuardarPrograma() {
    if ($("#form").valid()) {
        var request = {
            programa: {
                IdPrograma: $("#txtIdPrograma").val(),
                Carrera: $("#txtCarrera").val(),
                Duracion: $("#intDuracion").val()
            }
        };

        jQuery.ajax({
            url: postActualizarProgramaURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log("Respuesta del servidor:", data); // Depuración: verificar respuesta del servidor

                if (data.data == true) {
                    tablaprograma.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el programa", "success");
                } else {
                    swal("Mensaje", "No se pudo guardar los cambios", "warning");
                }
            },
            error: function (error) {
                console.log("Error en la solicitud:", error); // Depuración: verificar si hubo un error
                swal("Mensaje", "Ocurrió un error en la comunicación con el servidor", "error");
            }
        });
    }
}
