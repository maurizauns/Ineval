function periodCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#StartDate').val(data.StartDate);
    $('#EndDate').val(data.EndDate);
}

$(function () {
    $('#MonthlyClosure').is(':checked') ? $('.MonthlyClosure').removeClass("hide") : $('.MonthlyClosure').addClass("hide");
})

$('#MonthlyClosure').on('click', function () {
    $(this).is(':checked') ? $('#MonthlyClosure').val(true) : $('#MonthlyClosure').val(false);
    $(this).is(':checked') ? $('.MonthlyClosure').removeClass("hide") : $('.MonthlyClosure').addClass("hide");
});


$('#Status').on('click', function () {
    $(this).is(':checked') ? $('#Status').val(true) : $('#Status').val(false);
});

$('.btnNew').on('click', function () {
    $('#MonthlyClosure').prop("checked", false);
    $('#Status').prop("checked", false);
    $('.MonthlyClosure').addClass("hide");
});