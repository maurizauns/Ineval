if (typeof vmFormProcesoAsignacion === 'undefined') {
    var vmFormProcesoAsignacion = {};
} else {
    if (typeof vmFormProcesoAsignacion.isInNew == 'function') {
        if (vmFormProcesoAsignacion.isInNew() == true) {
            $('.nav-tabs a[href="#Nuevo"]').tab('show');
        }
    }
}

var vmFormProcesoAsignacion = {};
$(document).ready(function () {
    var url = window.location.pathname;

    PatientID = url.substring(url.lastIndexOf('/') + 1);

    function KnockoutFormParametrosIniciales(ParametrosIniciales) {
        vmFormProcesoAsignacion = ko.mapping.fromJS(ParametrosIniciales);
        vmFormProcesoAsignacion.Existe = ko.observable(false);
        vmFormProcesoAsignacion.VisibleEditar = ko.observable(false);
        vmFormProcesoAsignacion.EditarEnable = ko.observable(false);
        vmFormProcesoAsignacion.VisibleNew = ko.observable(false);
        vmFormProcesoAsignacion.VisibleSave = ko.observable(false);
        vmFormProcesoAsignacion.VisibleEdit = ko.observable(false);
        vmFormProcesoAsignacion.VisibleCancel = ko.observable(false);

        vmFormProcesoAsignacion.VisibleNumeroLab = ko.observable(false);


        vmFormProcesoAsignacion.Id = ko.observable("");
        vmFormProcesoAsignacion.Filtro1 = ko.observable("");
        vmFormProcesoAsignacion.Filtro2 = ko.observable("");
        vmFormProcesoAsignacion.Filtro3 = ko.observable("");
        vmFormProcesoAsignacion.Filtro4 = ko.observable("");
        vmFormProcesoAsignacion.Filtro5 = ko.observable("");

        vmFormProcesoAsignacion.visibleFiltro2 = ko.observable(false);
        vmFormProcesoAsignacion.visibleFiltro3 = ko.observable(false);
        vmFormProcesoAsignacion.visibleFiltro4 = ko.observable(false);
        vmFormProcesoAsignacion.visibleFiltro5 = ko.observable(false);

        vmFormProcesoAsignacion.New = function () {
            vmFormProcesoAsignacion.Existe(true);
            vmFormProcesoAsignacion.EditarEnable(true);
            vmFormProcesoAsignacion.VisibleSave(true);
            vmFormProcesoAsignacion.VisibleEditar(false);
            vmFormProcesoAsignacion.VisibleCancel(true);
        }

        vmFormProcesoAsignacion.Guardar = function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ViewTest/SaveFiltros",
                data: JSON.stringify({
                    Id: vmFormProcesoAsignacion.Id(),
                    AsignacionId: vmh.CurrentId()
                }),
                success: function (Data) {
                    _load();
                    swal(Data.message, "Registro Guardado", "success");
                    vmFormProcesoAsignacion = {};

                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();

                }
            });
        }

        vmFormProcesoAsignacion.Edit = function () {
            vmFormProcesoAsignacion.EditarEnable(true);
            vmFormProcesoAsignacion.VisibleSave(true);
            vmFormProcesoAsignacion.VisibleEdit(false);
            vmFormProcesoAsignacion.VisibleCancel(true);
        }

        vmFormProcesoAsignacion.Cancel = function () {
            vmFormProcesoAsignacion = {};
            $("#Content").load(vmh.CurrentUrl());
            $("#Content").show();

        }

        vmFormProcesoAsignacion.cambiarFiltro = function () {
            if (vmFormProcesoAsignacion.Filtro1() == 1) {
                vmFormProcesoAsignacion.visibleFiltro2(true);
                vmFormProcesoAsignacion.Filtro2("");
            } else {
                vmFormProcesoAsignacion.Filtro2("");
                vmFormProcesoAsignacion.Filtro3("");
                vmFormProcesoAsignacion.visibleFiltro2(false);
                vmFormProcesoAsignacion.visibleFiltro3(false);
                vmFormProcesoAsignacion.visibleFiltro4(false);
                vmFormProcesoAsignacion.visibleFiltro5(false);
            }
        }

        vmFormProcesoAsignacion.cambiarFiltro2 = function () {
            let dat = document.getElementById('cmbFiltro3')
            let array1 = $('#cmbFiltro2').val()
            
            if (array1 != null) {
                dat.innerHTML = '<option value="">Seleccione</option>'
                array1.map(x => {
                    dat.innerHTML += `<option value="${x}">${x}</option>`
                })
            } else {
                dat.innerHTML = '<option value="">Seleccione</option>'
            }
            
            if (vmFormProcesoAsignacion.Filtro2() != "") {
                vmFormProcesoAsignacion.visibleFiltro3(true);

                /*if (vmFormProcesoAsignacion.Filtro2() == 1) {
                    vmFormProcesoAsignacion.visibleFiltro3(true);
                } else {
                    vmFormProcesoAsignacion.visibleFiltro3(false);
                    vmFormProcesoAsignacion.visibleFiltro4(false);
                    vmFormProcesoAsignacion.visibleFiltro5(false);
                }*/
            } else {
                error("Debse Seleccionar Alguno");
                vmFormProcesoAsignacion.Filtro3("");
                vmFormProcesoAsignacion.visibleFiltro3(false);
                vmFormProcesoAsignacion.visibleFiltro4(false);
                vmFormProcesoAsignacion.visibleFiltro5(false);
            }
        }

        vmFormProcesoAsignacion.cambiarFiltro3 = function () {
            if (vmFormProcesoAsignacion.Filtro3() != "") {
                if (vmFormProcesoAsignacion.Filtro3() == 1) {
                    vmFormProcesoAsignacion.visibleFiltro3(true);
                } else {
                    vmFormProcesoAsignacion.visibleFiltro3(false);
                    vmFormProcesoAsignacion.visibleFiltro4(false);
                    vmFormProcesoAsignacion.visibleFiltro5(false);
                }
            } else {
                error("Debse Seleccionar Alguno");
            }
        }

        vmFormProcesoAsignacion.Filtro = function () {
            if (vmFormProcesoAsignacion.Filtro1() != "") {
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
                            vmFormProcesoAsignacion = {};

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

        if (typeof vmFormProcesoAsignacion.data === 'function') {
            vmFormProcesoAsignacion.Existe(false);
            vmFormProcesoAsignacion.VisibleNew(true);
            vmFormProcesoAsignacion.VisibleEditar(false);
            vmFormProcesoAsignacion.EditarEnable(false);
            vmFormProcesoAsignacion.VisibleSave(false);
            vmFormProcesoAsignacion.VisibleEdit(false);
            vmFormProcesoAsignacion.VisibleCancel(false);

            vmFormProcesoAsignacion.Id("");
            vmFormProcesoAsignacion.Filtro1("");
            vmFormProcesoAsignacion.Filtro2("");
            vmFormProcesoAsignacion.Filtro3("");
            vmFormProcesoAsignacion.Filtro4("");
            vmFormProcesoAsignacion.Filtro5("");
        } else if (vmFormProcesoAsignacion.data != null) {
            vmFormProcesoAsignacion.Existe(true);
            vmFormProcesoAsignacion.VisibleEditar(false);
            vmFormProcesoAsignacion.EditarEnable(false);
            vmFormProcesoAsignacion.VisibleSave(false);
            vmFormProcesoAsignacion.VisibleEdit(true);
            vmFormProcesoAsignacion.VisibleCancel(true);
            vmFormProcesoAsignacion.Id(vmFormProcesoAsignacion.data.Id());
            vmFormProcesoAsignacion.Filtro1(vmFormProcesoAsignacion.data.Filtro1());

            if (vmFormProcesoAsignacion.data.Filtro1() == 1) {
                vmFormProcesoAsignacion.visibleFiltro2(true);
                if (vmFormProcesoAsignacion.data.Filtro2() == 1) {
                    vmFormProcesoAsignacion.visibleFiltro2(true);
                    if (vmFormProcesoAsignacion.data.Filtro3() == 1) {
                        vmFormProcesoAsignacion.visibleFiltro3(true);
                    }
                }

            } else {
                vmFormProcesoAsignacion.visibleFiltro2(false);
                vmFormProcesoAsignacion.visibleFiltro3(false);
                vmFormProcesoAsignacion.visibleFiltro4(false);
                vmFormProcesoAsignacion.visibleFiltro5(false);
            }

            vmFormProcesoAsignacion.Filtro2(vmFormProcesoAsignacion.data.Filtro2());
            vmFormProcesoAsignacion.Filtro3(vmFormProcesoAsignacion.data.Filtro3());
            vmFormProcesoAsignacion.Filtro4(vmFormProcesoAsignacion.data.Filtro4());
            vmFormProcesoAsignacion.Filtro5(vmFormProcesoAsignacion.data.Filtro5());
        } else {
            vmFormProcesoAsignacion.VisibleNew(true);
        }

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormProcesoAsignacion, $("#Content")[0]);

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