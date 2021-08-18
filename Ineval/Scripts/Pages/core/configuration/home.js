
$(function () {
    $.ajax({
        type: 'GET',
        url: '/Home/GetAllPerson',
        dataType: 'json',
        cache: true,
        success: function (data) {
            $('#person-all').html(parseFloat(data).toLocaleString('en'));
            $('#ratios-financieros').removeClass('hide');
        },
        complete: function () {
            $('#ratios-financieros-spinner').addClass('hide');
        },
        error: function () {
            $('#ratios-financieros-error').removeClass('hide');
        },
    });

    $.ajax({
        type: 'GET',
        url: '/Home/GetAllEmpresa',
        dataType: 'json',
        cache: true,
        success: function (data) {
            $('#product-all').html(parseFloat(data).toLocaleString('en'));
            $('#home-product').removeClass('hide');
        },
        complete: function () {
            $('#home-product-spinner').addClass('hide');
        },
        error: function () {
            $('#home-product-error').removeClass('hide');
        },
    });

    $.ajax({
        type: 'GET',
        url: '/Home/GetAllAppointmentByDay',
        dataType: 'json',
        cache: true,
        success: function (data) {
            $('#appointment-all').html(parseFloat(data).toLocaleString('en'));
            $('#home-appointment').removeClass('hide');
        },
        complete: function () {
            $('#home-appointment-spinner').addClass('hide');
        },
        error: function () {
            $('#home-appointment-error').removeClass('hide');
        },
    });

    $.ajax({
        type: 'GET',
        url: '/Home/GetAllSalesByMonth',
        dataType: 'json',
        cache: true,
        success: function (data) {
            $('#sales-all').html('$&nbsp;' + parseFloat(data).toLocaleString('en'));
            $('#home-sales').removeClass('hide');
        },
        complete: function () {
            $('#home-sales-spinner').addClass('hide');
        },
        error: function () {
            $('#home-sales-error').removeClass('hide');
        },
    });
})

function frmVentasLoaded() {
    $('.loadVentas').fadeOut();
}