var urlProvince;
var urlCanton;

function officeCallback(data) {
    $('#Id').val(data.Id);
    $('#EmpresaId').val(data.EmpresaId);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#Address').val(data.Address);
    $('#Phone').val(data.Phone);
    $('#CountryId option[value=' + data.CountryId + "]").prop('selected', true);
    loadSelect2("#ProvinceId", urlProvince, { id: data.CountryId }, data.ProvinceId);
    loadSelect3("#CantonId", urlCanton, { id: data.ProvinceId }, data.CantonId);

}

$(document).on("change", "#CountryId", function () {
    var paisId = $(this).val();
    loadSelect3("#ProvinceId", urlProvince, { id: paisId }, "", function () {
        var provinciaId = $("#ProvinceId").val();
        loadSelect3("#CantonId", urlCanton, { id: provinciaId });
    });
});

$(document).on("change", "#ProvinceId", function () {
    var provinciaId = $(this).val();
    loadSelect2("#CantonId", urlCanton, { id: provinciaId });
});