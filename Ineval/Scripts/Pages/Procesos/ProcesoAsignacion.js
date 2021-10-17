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
        vmFormProcesoAsignacion.SinoNumeroLaboratorios = ko.observable(false);
        vmFormProcesoAsignacion.NumeroLaboratorios = ko.observable("");
        vmFormProcesoAsignacion.SinoNumeroEquipos = ko.observable(false);
        vmFormProcesoAsignacion.NumeroEquipos = ko.observable("");
        vmFormProcesoAsignacion.SinoNumerosSesiones = ko.observable(false);
        vmFormProcesoAsignacion.NumerosSesiones = ko.observable("");
        vmFormProcesoAsignacion.SinoNumeroDiasEvaluar = ko.observable(false);
        vmFormProcesoAsignacion.NumeroDiasEvaluar = ko.observable("");
        vmFormProcesoAsignacion.SinoTiempoViaje = ko.observable(false);
        vmFormProcesoAsignacion.TiempoViaje = ko.observable("");

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
                url: "/ParametrosIniciales/Save",
                data: JSON.stringify({
                    Id: vmFormProcesoAsignacion.Id(),
                    AsignacionId: vmh.CurrentId(),
                    SinoNumeroLaboratorios: vmFormProcesoAsignacion.SinoNumeroLaboratorios(),
                    NumeroLaboratorios: vmFormProcesoAsignacion.NumeroLaboratorios(),
                    SinoNumeroEquipos: vmFormProcesoAsignacion.SinoNumeroEquipos(),
                    NumeroEquipos: vmFormProcesoAsignacion.NumeroEquipos(),
                    SinoNumerosSesiones: vmFormProcesoAsignacion.SinoNumerosSesiones(),
                    NumerosSesiones: vmFormProcesoAsignacion.NumerosSesiones(),
                    SinoNumeroDiasEvaluar: vmFormProcesoAsignacion.SinoNumeroDiasEvaluar(),
                    NumeroDiasEvaluar: vmFormProcesoAsignacion.NumeroDiasEvaluar(),
                    SinoTiempoViaje: vmFormProcesoAsignacion.SinoTiempoViaje(),
                    TiempoViaje: vmFormProcesoAsignacion.TiempoViaje()
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

        vmFormProcesoAsignacion.Filtro = function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ViewTest/Filtro",
                data: JSON.stringify({
                    Id: vmh.CurrentId(),
                    Parametro1: $("#cmbFiltro1").val(),
                    Parametro2: $("#cmbFiltro2").val(),
                    Parametro3: $("#cmbFiltro3").val(),
                }),
                beforeSend: function () {
                    _load();
                },
                success: function (Data) {
                    
                    swal(Data.message, "Registro Guardado", "success");
                    vmFormProcesoAsignacion = {};

                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();

                },
                complete: function () {
                    _stopLoad();
                }
            });
        }

        if (typeof vmFormProcesoAsignacion.ParametrosIniciales === 'function') {
            vmFormProcesoAsignacion.Existe(false);
            vmFormProcesoAsignacion.VisibleNew(true);
            vmFormProcesoAsignacion.VisibleEditar(false);
            vmFormProcesoAsignacion.EditarEnable(false);
            vmFormProcesoAsignacion.VisibleSave(false);
            vmFormProcesoAsignacion.VisibleEdit(false);
            vmFormProcesoAsignacion.VisibleCancel(false);

            vmFormProcesoAsignacion.Id("");
            vmFormProcesoAsignacion.SinoNumeroLaboratorios(false);
            vmFormProcesoAsignacion.NumeroLaboratorios("");
            vmFormProcesoAsignacion.SinoNumeroEquipos(false);
            vmFormProcesoAsignacion.NumeroEquipos("");
            vmFormProcesoAsignacion.SinoNumerosSesiones(false);
            vmFormProcesoAsignacion.NumerosSesiones("");
            vmFormProcesoAsignacion.SinoNumeroDiasEvaluar(false);
            vmFormProcesoAsignacion.NumeroDiasEvaluar("");
            vmFormProcesoAsignacion.SinoTiempoViaje(false);
            vmFormProcesoAsignacion.TiempoViaje("");
        } else if (vmFormProcesoAsignacion.ParametrosIniciales != null) {
            vmFormProcesoAsignacion.Existe(true);
            vmFormProcesoAsignacion.VisibleEditar(false);
            vmFormProcesoAsignacion.EditarEnable(false);
            vmFormProcesoAsignacion.VisibleSave(false);
            vmFormProcesoAsignacion.VisibleEdit(true);
            vmFormProcesoAsignacion.VisibleCancel(true);
            vmFormProcesoAsignacion.Id(vmFormProcesoAsignacion.ParametrosIniciales.Id());
            vmFormProcesoAsignacion.SinoNumeroLaboratorios(vmFormProcesoAsignacion.ParametrosIniciales.SiNoNumeroLaboratorios());
            vmFormProcesoAsignacion.NumeroLaboratorios(vmFormProcesoAsignacion.ParametrosIniciales.NumeroLaboratorios());
            vmFormProcesoAsignacion.SinoNumeroEquipos(vmFormProcesoAsignacion.ParametrosIniciales.SiNoNumeroEquipos());
            vmFormProcesoAsignacion.NumeroEquipos(vmFormProcesoAsignacion.ParametrosIniciales.NumeroEquipos());
            vmFormProcesoAsignacion.SinoNumerosSesiones(vmFormProcesoAsignacion.ParametrosIniciales.SiNoNumerosSesiones());
            vmFormProcesoAsignacion.NumerosSesiones(vmFormProcesoAsignacion.ParametrosIniciales.NumerosSesiones());
            vmFormProcesoAsignacion.SinoNumeroDiasEvaluar(vmFormProcesoAsignacion.ParametrosIniciales.SiNoNumeroDiasEvaluar());
            vmFormProcesoAsignacion.NumeroDiasEvaluar(vmFormProcesoAsignacion.ParametrosIniciales.NumeroDiasEvaluar());
            vmFormProcesoAsignacion.SinoTiempoViaje(vmFormProcesoAsignacion.ParametrosIniciales.SiNoTiempoViaje());
            vmFormProcesoAsignacion.TiempoViaje(vmFormProcesoAsignacion.ParametrosIniciales.TiempoViaje());
        } else {
            vmFormProcesoAsignacion.VisibleNew(true);
        }

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormProcesoAsignacion, $("#Content")[0]);
    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/ParametrosIniciales/GetFormulario?id=" + vmh.CurrentId(),
        success: KnockoutFormParametrosIniciales
    });
});