function provinceCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#CountryId option[value=' + data.CountryId + "]").prop('selected', true);
}