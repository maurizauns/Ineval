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