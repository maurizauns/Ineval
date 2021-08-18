
function usuariosCallback(data) {
    $('#Id').val(data.Id);
    $('#EmpresaId option[value=' + data.EmpresaId + "]").prop('selected', true);
    $('#TipoIdentificacion option[value=' + data.TipoIdentificacion + "]").prop('selected', true);
    $('#Identificacion').val(data.Identificacion);
    $('#NombresCompletos').val(data.NombresCompletos);
    $("#Email").val(data.Email);
    $('#ApplicationRoleName option[value=' + data.ApplicationRoleName + "]").prop('selected', true);

    if (!data.Establecimientos) {
        data.Establecimientos = "";
    }

    $('#Establecimientos').val(data.Establecimientos);
    llenarEstablecimientos(data.Establecimientos.split("|"));
}


$(document).on("click", "#dibAgregarEstablecimiento", function () {

    var establecimiento = $("#txtEstablecimiento").val();

    if ($.trim(establecimiento) == "") {
        alert("Ingrese un establecimiento");
        return;
    }

    var lista = $("#Establecimientos").val().split("|");
    lista.push(establecimiento);

    llenarEstablecimientos(lista);
});

var llenarEstablecimientos = function (lista) {

    var establecimientos = "";
    $("#divEstablecimientos").empty();
    $.each(lista, function (index, item) {
        if ($.trim(item) != "") {
            establecimientos += item + "|";
            $("#divEstablecimientos")
                .append($("<div class='col-md-8'>").html("<span class = 'form-control'>" + item + "</span>"))
                .append($("<div class='col-md-4'>").html("<div class='btn btn-danger btn-xs' data-establecimiento='" + item + "'><span class='glyphicon glyphicon-remove'></span></div>")
                    .click(function () {
                        var est = $(this).find("div").data("establecimiento");
                        var ests = $("#Establecimientos").val();
                        ests = ests.replace(est, "");
                        var lis = ests.split("|");
                        llenarEstablecimientos(lis);
                    }));
        }
    });

    if (establecimientos.length > 0) {
        establecimientos = establecimientos.substring(0, establecimientos.length - 1);
    }
    $("#Establecimientos").val(establecimientos);
    $("#txtEstablecimiento").val("").focus();
}