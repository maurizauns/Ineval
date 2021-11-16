function pass(id) {
    $("#IdClave").val(id)
    $('#cambio-modal').modal('show')

}

function usuariosCallback(data) {
    $('#Id').val(data.Id);
    $('#Identificacion').val(data.Identificacion);
    $('#NombresCompletos').val(data.NombresCompletos);
    $("#Email").val(data.Email);
    $('#EmpresaId option[value=' + data.EmpresaId + "]").prop('selected', true);
    $('#TipoIdentificacion option[value=' + data.TipoIdentificacion + "]").prop('selected', true);
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

$('#btn-nueva-clave').click(function () {
    if ($('#txt-new-clave').val() == "") {
        error("El campo Nueva Clave Obligatorio")
    } else if (!verificarFormatoClave($('#txt-new-clave').val())) {
        error('El Formato de la Clave debe Tener Mayusculas,Minusculas,Numeros,Caracteres Especiales')
    } else if ($("#txt-repeat-clave").val() != $('#txt-new-clave').val()) {
        error("Las claves no coinciden")
    } else {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/Account/ResetPassword1",
            data: JSON.stringify({
                userId: $('#IdClave').val(),
                Password: $('#txt-new-clave').val(),
        
            }),
            success: function (Data) {
                swal("", "Cambio de Clave Correcta", "success");
                setTimeout(() => {
                    window.location.reload();
                }, 2000)
                
                

            }
        });
    }
})

const verificarFormatoClave = contrasenna => {
    if (contrasenna.length >= 8) {
        var mayuscula = false;
        var minuscula = false;
        var numero = false;
        var caracter_raro = false;

        for (var i = 0; i < contrasenna.length; i++) {
            if (contrasenna.charCodeAt(i) >= 65 && contrasenna.charCodeAt(i) <= 90) {
                mayuscula = true;
            }
            else if (contrasenna.charCodeAt(i) >= 97 && contrasenna.charCodeAt(i) <= 122) {
                minuscula = true;
            }
            else if (contrasenna.charCodeAt(i) >= 48 && contrasenna.charCodeAt(i) <= 57) {
                numero = true;
            }
            else {
                caracter_raro = true;
            }
        }
        if (mayuscula == true && minuscula == true && caracter_raro == true && numero == true) {
            return true;
        }
    }
    return false;
}



