if (typeof vmFormParametrosIniciales === 'undefined') {
    var vmFormParametrosIniciales = {};
} else {
    if (typeof vmFormParametrosIniciales.isInNew == 'function') {
        if (vmFormParametrosIniciales.isInNew() == true) {
            $('.nav-tabs a[href="#Nuevo"]').tab('show');
        }
    }
}

var vmFormParametrosIniciales = {};
$(document).ready(function () {
    var url = window.location.pathname;

    PatientID = url.substring(url.lastIndexOf('/') + 1);

    function KnockoutFormParametrosIniciales(ParametrosIniciales) {
        vmFormParametrosIniciales = ko.mapping.fromJS(ParametrosIniciales);
        vmFormParametrosIniciales.Existe = ko.observable(false);
        vmFormParametrosIniciales.VisibleEditar = ko.observable(false);
        vmFormParametrosIniciales.EditarEnable = ko.observable(false);
        vmFormParametrosIniciales.VisibleNew = ko.observable(false);
        vmFormParametrosIniciales.VisibleSave = ko.observable(false);
        vmFormParametrosIniciales.VisibleEdit = ko.observable(false);
        vmFormParametrosIniciales.VisibleCancel = ko.observable(false);

        vmFormParametrosIniciales.VisibleNumeroLab = ko.observable(false);


        vmFormParametrosIniciales.Id = ko.observable("");
        vmFormParametrosIniciales.SinoNumeroLaboratorios = ko.observable(false);
        vmFormParametrosIniciales.NumeroLaboratorios = ko.observable("");
        vmFormParametrosIniciales.SinoNumeroEquipos = ko.observable(false);
        vmFormParametrosIniciales.NumeroEquipos = ko.observable("");
        vmFormParametrosIniciales.SinoNumerosSesiones = ko.observable(false);
        vmFormParametrosIniciales.NumerosSesiones = ko.observable("");
        vmFormParametrosIniciales.SinoNumeroDiasEvaluar = ko.observable(false);
        vmFormParametrosIniciales.NumeroDiasEvaluar = ko.observable("");
        vmFormParametrosIniciales.SinoDuracionPrueba = ko.observable(false);
        vmFormParametrosIniciales.DuracionPrueba = ko.observable("");
        vmFormParametrosIniciales.SinoTiempoViaje = ko.observable(false);
        vmFormParametrosIniciales.TiempoViaje = ko.observable("");

        vmFormParametrosIniciales.New = function () {
            vmFormParametrosIniciales.Existe(true);
            vmFormParametrosIniciales.EditarEnable(true);
            vmFormParametrosIniciales.VisibleSave(true);
            vmFormParametrosIniciales.VisibleEditar(false);
            vmFormParametrosIniciales.VisibleCancel(true);
        }

        vmFormParametrosIniciales.Guardar = function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ParametrosIniciales/Save",
                data: JSON.stringify({
                    Id: vmFormParametrosIniciales.Id(),
                    AsignacionId: vmh.CurrentId(),
                    SinoNumeroLaboratorios: vmFormParametrosIniciales.SinoNumeroLaboratorios(),
                    NumeroLaboratorios: vmFormParametrosIniciales.NumeroLaboratorios(),
                    SinoNumeroEquipos: vmFormParametrosIniciales.SinoNumeroEquipos(),
                    NumeroEquipos: vmFormParametrosIniciales.NumeroEquipos(),
                    SinoNumerosSesiones: vmFormParametrosIniciales.SinoNumerosSesiones(),
                    NumerosSesiones: vmFormParametrosIniciales.NumerosSesiones(),
                    SinoNumeroDiasEvaluar: vmFormParametrosIniciales.SinoNumeroDiasEvaluar(),
                    NumeroDiasEvaluar: vmFormParametrosIniciales.NumeroDiasEvaluar(),
                    SinoDuracionPrueba: vmFormParametrosIniciales.SinoDuracionPrueba(),
                    DuracionPrueba: vmFormParametrosIniciales.DuracionPrueba(),
                    SinoTiempoViaje: vmFormParametrosIniciales.SinoTiempoViaje(),
                    TiempoViaje: vmFormParametrosIniciales.TiempoViaje()
                }),
                success: function (Data) {
                    swal(Data.message, "Registro Guardado", "success");
                    vmFormParametrosIniciales = {};

                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();

                }
            });
        }

        vmFormParametrosIniciales.Edit = function () {
            vmFormParametrosIniciales.EditarEnable(true);
            vmFormParametrosIniciales.VisibleSave(true);
            vmFormParametrosIniciales.VisibleEdit(false);
            vmFormParametrosIniciales.VisibleCancel(true);
        }

        vmFormParametrosIniciales.Cancel = function () {
            vmFormParametrosIniciales = {};
            $("#Content").load(vmh.CurrentUrl());
            $("#Content").show();

        }

        vmFormParametrosIniciales.allSubscriptionsUnchecked = function () {
            //debugger
            //if (vmFormParametrosIniciales.SinoNumeroEquipos()) {
            //    success();
            //} else {
            //    error();
            //}
        }

        if (typeof vmFormParametrosIniciales.ParametrosIniciales === 'function') {
            vmFormParametrosIniciales.Existe(false);
            vmFormParametrosIniciales.VisibleNew(true);
            vmFormParametrosIniciales.VisibleEditar(false);
            vmFormParametrosIniciales.EditarEnable(false);
            vmFormParametrosIniciales.VisibleSave(false);
            vmFormParametrosIniciales.VisibleEdit(false);
            vmFormParametrosIniciales.VisibleCancel(false);

            vmFormParametrosIniciales.Id("");
            vmFormParametrosIniciales.SinoNumeroLaboratorios(false);
            vmFormParametrosIniciales.NumeroLaboratorios("");
            vmFormParametrosIniciales.SinoNumeroEquipos(false);
            vmFormParametrosIniciales.NumeroEquipos("");
            vmFormParametrosIniciales.SinoNumerosSesiones(false);
            vmFormParametrosIniciales.NumerosSesiones("");
            vmFormParametrosIniciales.SinoNumeroDiasEvaluar(false);
            vmFormParametrosIniciales.NumeroDiasEvaluar("");
            vmFormParametrosIniciales.SinoDuracionPrueba(false);
            vmFormParametrosIniciales.DuracionPrueba("");
            vmFormParametrosIniciales.SinoTiempoViaje(false);
            vmFormParametrosIniciales.TiempoViaje("");
        } else if (vmFormParametrosIniciales.ParametrosIniciales != null) {
            vmFormParametrosIniciales.Existe(true);
            vmFormParametrosIniciales.VisibleEditar(false);
            vmFormParametrosIniciales.EditarEnable(false);
            vmFormParametrosIniciales.VisibleSave(false);
            vmFormParametrosIniciales.VisibleEdit(true);
            vmFormParametrosIniciales.VisibleCancel(true);
            vmFormParametrosIniciales.Id(vmFormParametrosIniciales.ParametrosIniciales.Id());
            vmFormParametrosIniciales.SinoNumeroLaboratorios(vmFormParametrosIniciales.ParametrosIniciales.SiNoNumeroLaboratorios());
            vmFormParametrosIniciales.NumeroLaboratorios(vmFormParametrosIniciales.ParametrosIniciales.NumeroLaboratorios());
            vmFormParametrosIniciales.SinoNumeroEquipos(vmFormParametrosIniciales.ParametrosIniciales.SiNoNumeroEquipos());
            vmFormParametrosIniciales.NumeroEquipos(vmFormParametrosIniciales.ParametrosIniciales.NumeroEquipos());
            vmFormParametrosIniciales.SinoNumerosSesiones(vmFormParametrosIniciales.ParametrosIniciales.SiNoNumerosSesiones());
            vmFormParametrosIniciales.NumerosSesiones(vmFormParametrosIniciales.ParametrosIniciales.NumerosSesiones());
            vmFormParametrosIniciales.SinoNumeroDiasEvaluar(vmFormParametrosIniciales.ParametrosIniciales.SiNoNumeroDiasEvaluar());
            vmFormParametrosIniciales.NumeroDiasEvaluar(vmFormParametrosIniciales.ParametrosIniciales.NumeroDiasEvaluar());
            vmFormParametrosIniciales.SinoDuracionPrueba(vmFormParametrosIniciales.ParametrosIniciales.SiNoDuracionPrueba());
            vmFormParametrosIniciales.DuracionPrueba(vmFormParametrosIniciales.ParametrosIniciales.DuracionPrueba());
            vmFormParametrosIniciales.SinoTiempoViaje(vmFormParametrosIniciales.ParametrosIniciales.SiNoTiempoViaje());
            vmFormParametrosIniciales.TiempoViaje(vmFormParametrosIniciales.ParametrosIniciales.TiempoViaje());
        } else {
            vmFormParametrosIniciales.VisibleNew(true);
        }

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormParametrosIniciales, $("#Content")[0]);
    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/ParametrosIniciales/GetFormulario?id=" + vmh.CurrentId(),
        success: KnockoutFormParametrosIniciales
    });
});