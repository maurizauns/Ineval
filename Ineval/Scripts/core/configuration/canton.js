var urlProvince;

$(document).ready(function () {
    $("#CountryId").change(function () {
        var paisId = $("#CountryId").val();
        loadSelect2("#ProvinceId", urlProvince, { id: paisId });
    });
});

function cantonCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#CountryId option[value=' + data.CountryId + "]").prop('selected', true);
    loadSelect2("#ProvinceId", urlProvince, { id: data.CountryId }, data.ProvinceId);
}