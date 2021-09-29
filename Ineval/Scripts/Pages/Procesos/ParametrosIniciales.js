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
        vmFormParametrosIniciales.Id = ko.observable("");
        vmFormParametrosIniciales.NumeroLaboratorios = ko.observable("");
        vmFormParametrosIniciales.NumeroEquipos = ko.observable("");
        vmFormParametrosIniciales.NumerosSesiones = ko.observable("");
        vmFormParametrosIniciales.NumeroDiasEvaluar = ko.observable("");
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
                    NumeroLaboratorios: vmFormParametrosIniciales.NumeroLaboratorios(),
                    NumeroEquipos: vmFormParametrosIniciales.NumeroEquipos(),
                    NumerosSesiones: vmFormParametrosIniciales.NumerosSesiones(),
                    NumeroDiasEvaluar: vmFormParametrosIniciales.NumeroDiasEvaluar(),
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

        if (typeof vmFormParametrosIniciales.ParametrosIniciales === 'function') {
            vmFormParametrosIniciales.Existe(false);
            vmFormParametrosIniciales.VisibleNew(true);
            vmFormParametrosIniciales.VisibleEditar(false);
            vmFormParametrosIniciales.EditarEnable(false);
            vmFormParametrosIniciales.VisibleSave(false);
            vmFormParametrosIniciales.VisibleEdit(false);
            vmFormParametrosIniciales.VisibleCancel(false);
            vmFormParametrosIniciales.Id("");
            vmFormParametrosIniciales.NumeroLaboratorios("");
            vmFormParametrosIniciales.NumeroEquipos("");
            vmFormParametrosIniciales.NumerosSesiones("");
            vmFormParametrosIniciales.NumeroDiasEvaluar("");
            vmFormParametrosIniciales.TiempoViaje("");
        } else if (vmFormParametrosIniciales.ParametrosIniciales != null) {
            vmFormParametrosIniciales.Existe(true);
            vmFormParametrosIniciales.VisibleEditar(false);
            vmFormParametrosIniciales.EditarEnable(false);
            vmFormParametrosIniciales.VisibleSave(false);
            vmFormParametrosIniciales.VisibleEdit(true);
            vmFormParametrosIniciales.VisibleCancel(true);
            vmFormParametrosIniciales.Id(vmFormParametrosIniciales.ParametrosIniciales.Id());
            vmFormParametrosIniciales.NumeroLaboratorios(vmFormParametrosIniciales.ParametrosIniciales.NumeroLaboratorios());
            vmFormParametrosIniciales.NumeroEquipos(vmFormParametrosIniciales.ParametrosIniciales.NumeroEquipos());
            vmFormParametrosIniciales.NumerosSesiones(vmFormParametrosIniciales.ParametrosIniciales.NumerosSesiones());
            vmFormParametrosIniciales.NumeroDiasEvaluar(vmFormParametrosIniciales.ParametrosIniciales.NumeroDiasEvaluar());
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