
$(function () {
    $.ajax({
        type: 'GET',
        url: '/Home/GetAllProcesos',
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
        url: '/Home/GetAllProcesosActivos',
        dataType: 'json',
        cache: true,
        success: function (data) {
            $('#person-activos').html(parseFloat(data).toLocaleString('en'));
            $('#ratios-financieros1').removeClass('hide');
        },
        complete: function () {
            $('#ratios-financieros-spinner1').addClass('hide');
        },
        error: function () {
            $('#ratios-financieros-error1').removeClass('hide');
        },
    });

    $.ajax({
        type: 'GET',
        url: '/Home/GetAllProcesosFinalizados',
        dataType: 'json',
        cache: true,
        success: function (data) {
            $('#person-finalizados').html(parseFloat(data).toLocaleString('en'));
            $('#ratios-financieros2').removeClass('hide');
        },
        complete: function () {
            $('#ratios-financieros-spinner2').addClass('hide');
        },
        error: function () {
            $('#ratios-financieros-error2').removeClass('hide');
        },
    });

    $.ajax({
        type: 'GET',
        url: '/Home/UpdateDatosMapbos',
        dataType: 'json',
        cache: true,
        success: function (data) {
            if (data) {

            } else{
                error("No se puedo actualizar");
            }
        },        
    });
})

function frmVentasLoaded() {
    $('.loadVentas').fadeOut();
}