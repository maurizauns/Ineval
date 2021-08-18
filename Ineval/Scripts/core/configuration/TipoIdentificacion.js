function categoryTypeCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#BackgroundColor').val(data.BackgroundColor);
    $('#Color').val(data.Color);

    $("#BackgroundColor").trigger("change");
    $("#Color").trigger("change");

    $('#SubCategoryTypeId option[value=' + data.SubCategoryTypeId + "]").prop('selected', true);
}

$('#color').colorpicker({});
$('#cp2').colorpicker();
$('#cp2c').colorpicker();