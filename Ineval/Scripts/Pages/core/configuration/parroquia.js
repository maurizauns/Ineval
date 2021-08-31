var urlProvince;
var urlCanton;
debugger
$(document).ready(function () {
    $("#CountryId").change(function () {
        var paisId = $("#CountryId").val();
        loadSelect2("#ProvinceId", urlProvince, { id: paisId });
    });
    $("#ProvinceId").change(function () {
        var provinceId = $("#ProvinceId").val();
        loadSelect2("#CantonId", urlCanton, { id: provinceId });
    });
});

function cantonCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#CountryId option[value=' + data.CountryId + "]").prop('selected', true);
    $('#ProvinceId option[value=' + data.ProvinceId + ']').prop('select', true);
    $('#CantonId option[value=' + data.CantonId + ']').prop('select', true);
    loadSelect2("#ProvinceId", urlProvince, { id: data.CountryId }, data.ProvinceId);
    loadSelect2("#CantonId", urlCanton, { id: data.ProvinceId }, data.ProvinceId);
}