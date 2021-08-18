var decimales = null;
function getNumeroDecimales() {
    $.ajax({
        type: "GET",
        url: urlprefix + "/AccountsPlan/GetDecimals/",
        data: null,
        dataType: "json",
        async: false,
        success: function (data, textStatus) {
            decimales = data['Decimales'];
        }
    });
    return decimales;
}

$(function () {

    $.each({
        '#ContribuyenteEspecial': false,
        '#ObligadoContabilidad': false,
        '#GeneraClaveAcceso': false,
        '#Exportador': false,
        '#SmtpHabilitaSsl': false,
        '#EmailIncluyeTipoDocumento': false,
    }, function (key, value) {
        $(key).bind('click', function () { showFieldWhenValue(key, value); });
        showFieldWhenValue(key, value);
    });
    function showFieldWhenValue(checkboxSelector, fieldSelector) {
        if ($(checkboxSelector).is(':checked')) {
            $(checkboxSelector).val(true);
        }
        else {
            $(checkboxSelector).val(false);
        }
    }
});
