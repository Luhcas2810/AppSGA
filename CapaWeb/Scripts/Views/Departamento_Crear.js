var tablaDepartamento;

$(document).ready(function () {
    // Inicializar DataTable para departamentos
    tablaDepartamento = $('#tbDepartamento').DataTable({
        "ajax": {
            "url": getListaDepartamentoURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Descripcion" },
            /*{ "data": "Jefe" },*/
            {
                "data": "IdDepartamento", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpFormDepartamento(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>";
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
function abrirPopUpFormDepartamento(json) {
    /*$("#txtIdDepartamento").val(0);*/
    if (json != null) {
        $("#txtCodigo").val(json.Codigo);
        $("#txtDescripcion").val(json.Descripcion);
        /*$("#txtJefe").val(json.Jefe);*/
    } else {
        $("#txtCodigo").val(0);
        $("#txtDescripcion").val("");
        /*$("#txtJefe").val("");*/
    }
    $('#FormModal').modal('show');
}

// Función para guardar o actualizar el departamento
function GuardarDepartamento() {
    if ($("#form").valid()) {
        var request = {
            departamento: {
                Codigo: $("#txtCodigo").val(),
                Descripcion: $("#txtDescripcion").val(),
                /*Jefe: $("#txtJefe").val()*/
            }
        };

        jQuery.ajax({
            url: postCrearDepartamentoURL,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (data.data === true) {
                    console.log(data.data);
                    tablaDepartamento.ajax.reload();
                    $('#FormModal').modal('hide');
                    swal("Mensaje", "Se guardó exitosamente el departamento", "success");
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
