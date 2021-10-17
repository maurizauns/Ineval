var urlNombreProceso;

function asignacionCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#NombreProcesoId option[value=' + data.NombreProcesoId + "]").prop('selected', true);
}
