if (typeof vmFormProcesoAsignacionLabo === 'undefined') {
    var vmFormProcesoAsignacionLabo = {};
} else {
    if (typeof vmFormProcesoAsignacionLabo.isInNew == 'function') {
        if (vmFormProcesoAsignacionLabo.isInNew() == true) {
            $('.nav-tabs a[href="#Nuevo"]').tab('show');
        }
    }
}

var vmFormProcesoAsignacionLabo = {};
$(document).ready(function () {
    var url = window.location.pathname;

    PatientID = url.substring(url.lastIndexOf('/') + 1);

    function KnockoutFormParametrosIniciales(ParametrosIniciales) {
        vmFormProcesoAsignacionLabo = ko.mapping.fromJS(ParametrosIniciales);
        vmFormProcesoAsignacionLabo.Existe = ko.observable(false);
        vmFormProcesoAsignacionLabo.VisibleEditar = ko.observable(false);
        vmFormProcesoAsignacionLabo.EditarEnable = ko.observable(false);
        vmFormProcesoAsignacionLabo.VisibleNew = ko.observable(false);
        vmFormProcesoAsignacionLabo.VisibleSave = ko.observable(false);
        vmFormProcesoAsignacionLabo.VisibleEdit = ko.observable(false);
        vmFormProcesoAsignacionLabo.VisibleCancel = ko.observable(false);

        vmFormProcesoAsignacionLabo.VisibleNumeroLab = ko.observable(false);


        vmFormProcesoAsignacionLabo.Id = ko.observable("");
        vmFormProcesoAsignacionLabo.Filtro1 = ko.observable("");
        vmFormProcesoAsignacionLabo.Filtro2 = ko.observable("");
        vmFormProcesoAsignacionLabo.Filtro3 = ko.observable("");
        vmFormProcesoAsignacionLabo.Filtro4 = ko.observable("");
        vmFormProcesoAsignacionLabo.Filtro5 = ko.observable("");

        vmFormProcesoAsignacionLabo.visibleFiltro2 = ko.observable(false);
        vmFormProcesoAsignacionLabo.visibleFiltro3 = ko.observable(false);
        vmFormProcesoAsignacionLabo.visibleFiltro4 = ko.observable(false);
        vmFormProcesoAsignacionLabo.visibleFiltro5 = ko.observable(false);

        vmFormProcesoAsignacionLabo.New = function () {
            vmFormProcesoAsignacionLabo.Existe(true);
            vmFormProcesoAsignacionLabo.EditarEnable(true);
            vmFormProcesoAsignacionLabo.VisibleSave(true);
            vmFormProcesoAsignacionLabo.VisibleEditar(false);
            vmFormProcesoAsignacionLabo.VisibleCancel(true);
        }

        vmFormProcesoAsignacionLabo.Guardar = function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ViewTest/SaveFiltros",
                data: JSON.stringify({
                    Id: vmFormProcesoAsignacionLabo.Id(),
                    AsignacionId: vmh.CurrentId()
                }),
                success: function (Data) {
                    _load();
                    swal(Data.message, "Registro Guardado", "success");
                    vmFormProcesoAsignacionLabo = {};

                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();

                }
            });
        }

        vmFormProcesoAsignacionLabo.Edit = function () {
            vmFormProcesoAsignacionLabo.EditarEnable(true);
            vmFormProcesoAsignacionLabo.VisibleSave(true);
            vmFormProcesoAsignacionLabo.VisibleEdit(false);
            vmFormProcesoAsignacionLabo.VisibleCancel(true);
        }

        vmFormProcesoAsignacionLabo.Cancel = function () {
            vmFormProcesoAsignacionLabo = {};
            $("#Content").load(vmh.CurrentUrl());
            $("#Content").show();

        }

        vmFormProcesoAsignacionLabo.cambiarFiltro = function () {
            if (vmFormProcesoAsignacionLabo.Filtro1() == 1) {
                vmFormProcesoAsignacionLabo.visibleFiltro2(true);
                vmFormProcesoAsignacionLabo.Filtro2("");
            } else {
                vmFormProcesoAsignacionLabo.Filtro2("");
                vmFormProcesoAsignacionLabo.Filtro3("");
                vmFormProcesoAsignacionLabo.visibleFiltro2(false);
                vmFormProcesoAsignacionLabo.visibleFiltro3(false);
                vmFormProcesoAsignacionLabo.visibleFiltro4(false);
                vmFormProcesoAsignacionLabo.visibleFiltro5(false);
            }
        }

        vmFormProcesoAsignacionLabo.cambiarFiltro2 = function () {
            if (vmFormProcesoAsignacionLabo.Filtro2() != "") {
                if (vmFormProcesoAsignacionLabo.Filtro2() == 1) {
                    vmFormProcesoAsignacionLabo.visibleFiltro3(true);
                } else {
                    vmFormProcesoAsignacionLabo.visibleFiltro3(false);
                    vmFormProcesoAsignacionLabo.visibleFiltro4(false);
                    vmFormProcesoAsignacionLabo.visibleFiltro5(false);
                }
            } else {
                error("Debse Seleccionar Alguno");
                vmFormProcesoAsignacionLabo.Filtro3("");
                vmFormProcesoAsignacionLabo.visibleFiltro3(false);
                vmFormProcesoAsignacionLabo.visibleFiltro4(false);
                vmFormProcesoAsignacionLabo.visibleFiltro5(false);
            }
        }

        vmFormProcesoAsignacionLabo.cambiarFiltro3 = function () {
            if (vmFormProcesoAsignacionLabo.Filtro3() != "") {
                if (vmFormProcesoAsignacionLabo.Filtro3() == 1) {
                    vmFormProcesoAsignacionLabo.visibleFiltro3(true);
                } else {
                    vmFormProcesoAsignacionLabo.visibleFiltro3(false);
                    vmFormProcesoAsignacionLabo.visibleFiltro4(false);
                    vmFormProcesoAsignacionLabo.visibleFiltro5(false);
                }
            } else {
                error("Debse Seleccionar Alguno");
            }
        }

        vmFormProcesoAsignacionLabo.Filtro = function () {
            if (vmFormProcesoAsignacionLabo.Filtro1() != "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/ViewTest/Filtro",
                    data: JSON.stringify({
                        Id: vmh.CurrentId(),
                        Parametro1: $("#cmbFiltro1").val(),
                        Parametro2: $("#cmbFiltro2").val() != null ? $("#cmbFiltro2").val().toString() : null,
                        Parametro3: $("#cmbFiltro3").val(),
                    }),
                    beforeSend: function () {
                        _load();
                    },
                    success: function (Data) {
                        if (Data.result != "" && Data.result != null) {

                        } else {
                            swal(Data.message, "", Data.status);
                            vmFormProcesoAsignacionLabo = {};

                            $("#Content").load(vmh.CurrentUrl());
                            $("#Content").show();
                        }
                    },
                    complete: function () {
                        _stopLoad();
                    }
                });
            } else {
                error("Debe selecionar al menos 1");
            }

        }

        if (typeof vmFormProcesoAsignacionLabo.data === 'function') {
            vmFormProcesoAsignacionLabo.Existe(false);
            vmFormProcesoAsignacionLabo.VisibleNew(true);
            vmFormProcesoAsignacionLabo.VisibleEditar(false);
            vmFormProcesoAsignacionLabo.EditarEnable(false);
            vmFormProcesoAsignacionLabo.VisibleSave(false);
            vmFormProcesoAsignacionLabo.VisibleEdit(false);
            vmFormProcesoAsignacionLabo.VisibleCancel(false);

            vmFormProcesoAsignacionLabo.Id("");
            vmFormProcesoAsignacionLabo.Filtro1("");
            vmFormProcesoAsignacionLabo.Filtro2("");
            vmFormProcesoAsignacionLabo.Filtro3("");
            vmFormProcesoAsignacionLabo.Filtro4("");
            vmFormProcesoAsignacionLabo.Filtro5("");
        } else if (vmFormProcesoAsignacionLabo.data != null) {
            vmFormProcesoAsignacionLabo.Existe(true);
            vmFormProcesoAsignacionLabo.VisibleEditar(false);
            vmFormProcesoAsignacionLabo.EditarEnable(false);
            vmFormProcesoAsignacionLabo.VisibleSave(false);
            vmFormProcesoAsignacionLabo.VisibleEdit(true);
            vmFormProcesoAsignacionLabo.VisibleCancel(true);
            vmFormProcesoAsignacionLabo.Id(vmFormProcesoAsignacionLabo.data.Id());
            vmFormProcesoAsignacionLabo.Filtro1(vmFormProcesoAsignacionLabo.data.Filtro1());

            if (vmFormProcesoAsignacionLabo.data.Filtro1() == 1) {
                vmFormProcesoAsignacionLabo.visibleFiltro2(true);
                if (vmFormProcesoAsignacionLabo.data.Filtro2() == 1) {
                    vmFormProcesoAsignacionLabo.visibleFiltro2(true);
                    if (vmFormProcesoAsignacionLabo.data.Filtro3() == 1) {
                        vmFormProcesoAsignacionLabo.visibleFiltro3(true);
                    }
                }

            } else {
                vmFormProcesoAsignacionLabo.visibleFiltro2(false);
                vmFormProcesoAsignacionLabo.visibleFiltro3(false);
                vmFormProcesoAsignacionLabo.visibleFiltro4(false);
                vmFormProcesoAsignacionLabo.visibleFiltro5(false);
            }

            vmFormProcesoAsignacionLabo.Filtro2(vmFormProcesoAsignacionLabo.data.Filtro2());
            vmFormProcesoAsignacionLabo.Filtro3(vmFormProcesoAsignacionLabo.data.Filtro3());
            vmFormProcesoAsignacionLabo.Filtro4(vmFormProcesoAsignacionLabo.data.Filtro4());
            vmFormProcesoAsignacionLabo.Filtro5(vmFormProcesoAsignacionLabo.data.Filtro5());
        } else {
            vmFormProcesoAsignacionLabo.VisibleNew(true);
        }

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormProcesoAsignacionLabo, $("#Content")[0]);

        $("#cmbFiltro2").chosen();
        $("#cmbFiltro2_chosen").width("100%");

    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/ViewTest/GetFiltros?AsignacionId=" + vmh.CurrentId(),
        success: KnockoutFormParametrosIniciales
    });
});