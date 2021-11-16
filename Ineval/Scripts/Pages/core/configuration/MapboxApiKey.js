


function mapboxCallback(data) {
    $('#Id').val(data.Id);
    $('#Code').val(data.Code);
    $('#Description').val(data.Description);
    $('#APIKEY').val(data.APIKEY);
    $('#NumeroMaximoConsulta').val(data.NumeroMaximoConsulta);
    $('#NumeroMininoConsulta').val(data.NumeroMininoConsulta);
    $('#NumeroUsadasConsultas').val(data.NumeroUsadasConsultas);
}
