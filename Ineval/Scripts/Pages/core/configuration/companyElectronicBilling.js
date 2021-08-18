let tipos_doc_default = {
    'FAC': 'Factura',
    'RET': 'Retención',
    'NCT': 'Nota de Crédito',
    'NDT': 'Nota de Débito',
    'GUI': 'Guia Remisión'
};

function openModelElectronicBilling() {
    $('#electronicBilling-modal').modal();
}


$(document).on("submit", "form.frmEmissionPoint", function () {
    var $form = $(this),
        data = getCrudFields($form),
        url = $form.attr('action');
    if (url && url != '') {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            dataType: 'json',
            beforeSend: function () {
               
            },
            success: function (d) {
                result = d['result'];
                msg = d['message'];
                if (d.success) {
                    success('Grabado Correctamente!!');
                    tr = '<tr id="default_' + result.Id + '">';
                    tr += '<td><b>' + tipos_doc_default[result.CategoryType.Code] + '</b></td><td>' + result.Establishment + '-' + result.Emission + '</td>';
                    tr += '<td><span class="btn btn-xs btn-danger glow" onclick="javascript:quitarDefault(' + "'" + result.Id + "'" + ')">';
                    tr += '<span class="bx bx-trash"></span></span></td></tr>';
                    $('.table-est-ptoemi').append(tr);
                } else {
                    error(d.message.length == 0 ? "Ocurrió un error. Por favor vuelva a interntarlo" : d.message);
                }
            },
            complete: function () {
                
            },
            error: function (e) {
                error("Ocurrió un error. Por favor vuelva a interntarlo");
            },
        });
    }
    return false;
});



async function quitarDefault(id) {
    confirm("Estás seguro de eliminar ?", function () {
        $.ajax({
            type: "post",
            async: true,
            url: "/Empresas/RemoveBroadcastPoint/",
            data: {
                'id': id
            },
            success: function (d) {
                if (d.success) {
                    success('Grabado Correctamente!!');
                    $('#default_' + id).remove();
                }
            },
            error: function (err) {
                error(err);
            },
            dataType: 'json'
        });
    });
    return false;
}



//async function Agregar() {
//    tipd = $('#CategoryTypeId').val();
//    esta = $('#Establishment').val();
//    ptoe = $('#Emission').val();
//    empresaId = $('#Id').val();
//    var data = { "CategoryTypeId": tipd, "Establishment": esta, "Emission": ptoe, "EmpresaId": empresaId };
//    $.ajax({
//        url: "/Empresas/SaveBroadcastPoint/",
//        type: "POST",
//        async: true,
//        data: data,
//        error: function () { },
//        success: function (data) {
//            result = data['result'];
//            msg = data['message'];
//            if (data.success) {
//                success('Grabado Correctamente!!');
//                tr = '<tr id="default_' + result.Id + '">';
//                tr += '<td><b>' + tipos_doc_default[result.CategoryType.Code] + '</b></td><td>' + esta + '-' + ptoe + '</td>';
//                tr += '<td><span class="btn btn-xs btn-danger glow" onclick="javascript:quitarDefault(' + "'" + result.Id + "'" + ')">';
//                tr += '<span class="bx bx-trash"></span></span></td></tr>';
//                $('.table-est-ptoemi').append(tr);
//            }
//            else {
//                info(msg);
//            }
//        },
//        complete: function () { }
//    });
//}