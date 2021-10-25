function setSelectEmail(id, data) {
    var nom = data.split(" ");
    var selectize = $(id)[0].selectize;
    selectize.clear();
    selectize.clearOptions();
    selectize.renderCache = {};

    for (var i in nom) {
        var email_item = nom[i];
        selectize.addOption({
            email: email_item
        });

    }
    selectize.refreshOptions(true);
    selectize.setValue(nom);
}

function crearSelectizeEmail(emails, maxItem) {
    for (var i = 0; i < emails.length; i++) {
        email = $("#" + emails[i]).selectize({
            plugins: ['remove_button'],
            delimiter: ' ',
            persist: false,
            maxItems: maxItem,
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
}

function validarEmail(e) {
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    var mensaje = e.id + "Mensaje";
    if (emailRegex.test(e.value)) {
        $("#" + mensaje).html("");
        return 1;
    } else {
        $("#" + mensaje).html("Correo electrónico no valido");
        return 0;
    }
}