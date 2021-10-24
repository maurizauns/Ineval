var vmFormPersonalInfo = {};
$(document).ready(function () {

    function KnockoutFormPersonalInfo(personalInfo) {
        vmFormPersonalInfo = ko.mapping.fromJS(personalInfo);
        vmFormPersonalInfo.Identificaion = ko.observable("");


        vmFormPersonalInfo.Save = function () {

            $.ajax({
                url: '/Manage/SavePersonalInfo',
                type: 'POST',
                data: {
                    TipoIdentificacion: vmFormPersonalInfo.Data.TipoIdentificacion(),
                    Identificacion: vmFormPersonalInfo.Data.Identificacion(),
                    Email: vmFormPersonalInfo.Data.Email(),
                    NombresCompletos: vmFormPersonalInfo.Data.NombresCompletos(),
                    APIKEY: vmFormPersonalInfo.Data.APIKEY()

                },
                success: function (Data) {
                    if (Data.status != "error") {
                        swal({
                            title: Data.Result,
                            text: "",
                            type: Data.status,
                        }).then(function () {
                            window.location.href = '/Manage/';
                        });
                    } else {
                        swal({
                            title: Data.Result,
                            text: "",
                            type: Data.status,
                        });
                    }
                },
                error: function (data, textStatus, jqXHR) {
                    alert(data);
                }
            });
        };

        vmFormPersonalInfo.Cancel = function () {
            window.location.href = '/Manage/';
        }

        ko.cleanNode($("#PersonalInfo")[0]);
        ko.applyBindings(vmFormPersonalInfo, $("#PersonalInfo")[0]);
    }
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/Manage/PersonalInfo",
        success: KnockoutFormPersonalInfo
    });
})