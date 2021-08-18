var REGEX_EMAIL = "([a-z0-9!#$%&\'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&\'*+/=?^_`{|}~-]+)*@" +
    "(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)";


$(function () {
    
    crearSelectizeEmail();
    llenarSelectEmail();
})

function crearSelectizeEmail() {
    
    $("#Email").selectize({
        plugins: ['remove_button'],
        delimiter: ' ',
        persist: false,
        maxItems: 4,
        valueField: "email",
        labelField: "name",
        searchField: ["email"],
        options: [],
        render: {
            item: function (item, escape) {
                return "<div>" +
                    (item.email ? "<small class=\"text-muted ml10\">" + escape(item.email) + "</small>" : "") +
                    "</div>";
            },
            option: function (item, escape) {

                var label = item.email;
                return "<div>" +
                    "<span class=\"text-primary\">" + escape(label) + "</span>" +
                    "</div>";
            }
        },
        create: function (input) {
            if ((new RegExp("^" + REGEX_EMAIL + "$", "i")).test(input)) {
                return {
                    email: input
                };
            }
            var match = input.match(new RegExp("^([^<]*)\<" + REGEX_EMAIL + "\>$", "i"));
            if (match) {
                return {
                    email: match[2]
                };
            }
            return false;
        }
    });
}

function llenarSelectEmail() {
    var id = $('#id_id').val();
    var email_personas;

    if (id) {
        email_personas = $('#id_email').val();
        var nom = email_personas.split(",");
        var selectize = $("#Email")[0].selectize;
        selectize.clear();
        selectize.clearOptions();
        selectize.renderCache = {};

        for (var i in nom) {
            var email_item = nom[i];
            //<!--console.log(email_item);-->
            selectize.addOption({
                email: email_item
            });

        }
        selectize.refreshOptions(true);
        selectize.setValue(nom);

    }

}

function validarDocumento() {
    numero = document.getElementById('Identifier').value;
    var tipoIdentificacion = $('#IdentificationTypeId option:selected').text();
    if (tipoIdentificacion != 'RUC' && tipoIdentificacion != 'CEDULA') {
        $("#Identifier").removeClass("form-control-warning is-invalid");
        return;
    }
    /* alert(numero); */
    var suma = 0;
    var residuo = 0;
    var pri = false;
    var pub = false;
    var nat = false;
    var numeroProvincias = 22;
    var modulo = 11;

    /* Verifico que el campo no contenga letras */
    var ok = 1;
    for (i = 0; i < numero.length && ok == 1; i++) {
        var n = parseInt(numero.charAt(i));
        if (isNaN(n)) ok = 0;
    }
    if (ok == 0) {
        info("No puede ingresar caracteres en el número");
        //$("#GuardarId").hide();
        $("#Identifier").addClass("form-control-warning is-invalid");
        return false;
    }

    if (numero.length < 10) {
        info('El número ingresado no es válido');
        //$("#GuardarId").hide();
        $("#Identifier").addClass("form-control-warning is-invalid");
        return false;
    }

    /* Los primeros dos digitos corresponden al codigo de la provincia */
    provincia = numero.substr(0, 2);
    if (provincia < 1 || provincia > numeroProvincias) {
        info('El código de la provincia (dos primeros dígitos) es inválido');
        //$("#GuardarId").hide();
        $("#Identifier").addClass("form-control-warning is-invalid");
        return false;
    }
    /* Aqui almacenamos los digitos de la cedula en variables. */
    d1 = numero.substr(0, 1);
    d2 = numero.substr(1, 1);
    d3 = numero.substr(2, 1);
    d4 = numero.substr(3, 1);
    d5 = numero.substr(4, 1);
    d6 = numero.substr(5, 1);
    d7 = numero.substr(6, 1);
    d8 = numero.substr(7, 1);
    d9 = numero.substr(8, 1);
    d10 = numero.substr(9, 1);

    /* El tercer digito es: */
    /* 9 para sociedades privadas y extranjeros   */
    /* 6 para sociedades publicas */
    /* menor que 6 (0,1,2,3,4,5) para personas naturales */
    if (d3 == 7 || d3 == 8) {
        info('El tercer dígito ingresado es inválido');
        //$("#GuardarId").hide();
        $("#Identifier").addClass("form-control-warning is-invalid");
        return false;
    }

    /* Solo para personas naturales (modulo 10) */
    if (d3 < 6) {
        nat = true;
        p1 = d1 * 2; if (p1 >= 10) p1 -= 9;
        p2 = d2 * 1; if (p2 >= 10) p2 -= 9;
        p3 = d3 * 2; if (p3 >= 10) p3 -= 9;
        p4 = d4 * 1; if (p4 >= 10) p4 -= 9;
        p5 = d5 * 2; if (p5 >= 10) p5 -= 9;
        p6 = d6 * 1; if (p6 >= 10) p6 -= 9;
        p7 = d7 * 2; if (p7 >= 10) p7 -= 9;
        p8 = d8 * 1; if (p8 >= 10) p8 -= 9;
        p9 = d9 * 2; if (p9 >= 10) p9 -= 9;
        modulo = 10;
    }
    /* Solo para sociedades publicas (modulo 11) */
    /* Aqui el digito verficador esta en la posicion 9, en las otras 2 en la pos. 10 */
    else if (d3 == 6) {
        pub = true;
        p1 = d1 * 3;
        p2 = d2 * 2;
        p3 = d3 * 7;
        p4 = d4 * 6;
        p5 = d5 * 5;
        p6 = d6 * 4;
        p7 = d7 * 3;
        p8 = d8 * 2;
        p9 = 0;
    }

    /* Solo para entidades privadas (modulo 11) */
    else if (d3 == 9) {
        pri = true;
        p1 = d1 * 4;
        p2 = d2 * 3;
        p3 = d3 * 2;
        p4 = d4 * 7;
        p5 = d5 * 6;
        p6 = d6 * 5;
        p7 = d7 * 4;
        p8 = d8 * 3;
        p9 = d9 * 2;
    }

    suma = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9;
    residuo = suma % modulo;
    /* Si residuo=0, dig.ver.=0, caso contrario 10 - residuo*/
    digitoVerificador = residuo == 0 ? 0 : modulo - residuo;
    /* ahora comparamos el elemento de la posicion 10 con el dig. ver.*/
    if (pub == true) {
        if (digitoVerificador != d9) {
            info('El ruc de la empresa del sector público es incorrecto.');
            return false;
        }
        /* El ruc de las empresas del sector publico terminan con 0001*/
        if (numero.substr(9, 4) != '0001') {
            info('El ruc de la empresa del sector público debe terminar con 0001');
            //$("#GuardarId").hide();
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;

        }
    }
    else if (pri == true) {
        if (digitoVerificador != d10) {
            info('El ruc de la empresa del sector privado es incorrecto.');
            //$("#GuardarId").hide();
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;
        }
        if (numero.substr(10, 3) != '001') {
            info('El ruc de la empresa del sector privado debe terminar con 001');
            //$("#GuardarId").hide();
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;
        }
    }
    else if (nat == true) {
        if (digitoVerificador != d10) {
            info('El número de cédula de la persona natural es incorrecto.');
            //$("#GuardarId").hide();
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;
        }
        if (numero.length > 10 && numero.substr(10, 3) != '001') {
            info('El ruc de la persona natural debe terminar con 001');
            //$("#GuardarId").hide();
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;
        }
    }
    if (tipoIdentificacion == 'RUC') {
        if (numero.length != 13) {
            info('El número ingresado no es válido en la opcion Ruc');
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;
        }
    }
    if (tipoIdentificacion == 'CEDULA') {
        if (numero.length != 10) {
            info('El número ingresado no es válido en la opcion Cedula');
            $("#Identifier").addClass("form-control-warning is-invalid");
            return false;
        }
    }
    $("#GuardarId").show();
    $("#Identifier").removeClass("form-control-warning is-invalid").addClass("is-valid");
    return true;
}