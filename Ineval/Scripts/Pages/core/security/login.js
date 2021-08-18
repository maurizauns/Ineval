$(function () {
    var $form = $('form[name=formLogin]');
    $form.on('click', 'input[type=submit]', function (e) {
        e.preventDefault();
        if ($form.parsley().validate()) {
            $.ajax({
                type: "POST",
                url: "/account/login/",
                data: $("#formLogin").serialize(),
                dataType: "json",
                beforeSend: function () {
                    _load()
                },
                success: function (data) {
                    window.location.href = "/";
                },
                error: function (e) {
                    _stopLoad()
                    error(e)
                },
                complete: function () {
                    _stopLoad()
                }
            });
        } else {
            $form
                .removeClass('animation animating shake')
                .addClass('animation animating shake')
                .one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                    $(this).removeClass('animation animating shake');
                });
        }

    });

})

function blockCampos() {
    $("#UserName").prop("readonly", true);
    $("#Password").prop("readonly", true);
    $("#btnLogin").prop("disabled", true);
}

function noBlockCampos() {
    $("#UserName").prop("readonly", false);
    $("#Password").prop("readonly", false);
    $("#btnLogin").prop("disabled", false);
}
