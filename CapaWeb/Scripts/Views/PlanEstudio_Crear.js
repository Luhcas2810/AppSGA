var tablaPlanEstudio;

$(document).ready(function () {
    // Cargar lista de Escuelas al abrir el modal
    cargarEscuelas();

    // Inicializar DataTable para planes de estudio
    tablaPlanEstudio = $('#tbPlanEstudio').DataTable({
        "ajax": {
            "url": getListaPlanEstudioURL,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "_Escuela", render: function (data) {
                    return data.Carrera;
                }
            },
            { "data": "Descripcion" },
            {
                "data": "Estado", "render": function (data) {
                    return data === 1 ? '<span class="badge badge-success">Activo</span>' : '<span class="badge badge-danger">No Activo</span>';
                }
            },
            {
                "data": "Codigo", "render": function (data, type, row) {
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

// Función para cargar la lista de Escuelas en el combo
function cargarEscuelas() {
    $.ajax({
        url: getListaEscuelaURL, // Asegúrate de tener esta URL en tu controlador
        type: "GET",
        dataType: "json",
        success: function (data) {
            $("#cboCodigoEscuela").html("");
            if (data.data) {
                $.each(data.data, function (i, item) {
                    $("<option>").val(item.Codigo).text(item.Carrera).appendTo("#cboCodigoEscuela");
                });
            }
        },
        error: function (error) {
            console.log("Error al cargar escuelas:", error);
        }
    });
}

// Función para abrir el modal y cargar datos en caso de edición
function abrirPopUpFormPlanEstudio(json) {
    if (json) {
        // Caso de edición
        $("#txtCodigo").val(json.Codigo);
        $("#cboCodigoEscuela").val(json.CodigoEscuela);
        $("#cboCodigoEscuela").prop("disabled", true);
        $("#txtDescripcion").val(json.Descripcion);
        
        /*$("#cboEstado").val(json.Estado);*/
    } else {
        // Caso de creación
        $("#txtCodigo").val("");
        $("#cboCodigoEscuela").val(1);
        $("#cboCodigoEscuela").prop("disabled", true);
        $("#txtDescripcion").val("");
        
        //$("#cboEstado").val(1); // Activo por defecto

        // Habilitar campos
        $("#cboCodigoEscuela, #txtDescripcion").prop("disabled", false);
    }

    $('#FormModal').modal('show');
}

// Función para guardar o actualizar el plan de estudio
function GuardarPlanEstudio() {
    
    var request = {
        planEstudio: {
            Codigo: $("#txtCodigo").val(),
            CodigoEscuela: $("#cboCodigoEscuela").val(),
            Descripcion: $("#txtDescripcion").val(),
            /*Estado: parseInt($("#cboEstado").val())*/
        }
    };
    console.log(request);
    jQuery.ajax({
        url: postGuardarPlanEstudioURL,
        type: "POST",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.data.Respuesta === true) {
                tablaPlanEstudio.ajax.reload();
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

