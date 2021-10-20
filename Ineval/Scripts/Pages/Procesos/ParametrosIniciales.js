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
        vmFormParametrosIniciales.startToAdd = ko.observable("");
        vmFormParametrosIniciales.HoraMaxima = ko.observable("");
        vmFormParametrosIniciales.HoraInicio = ko.observable("");
        vmFormParametrosIniciales.HoraFin = ko.observable("");
        vmFormParametrosIniciales.TiempoEvaluacion = ko.observable("");
        vmFormParametrosIniciales.TiempoReceso = ko.observable("");
        vmFormParametrosIniciales.TiempoReal = ko.observable("");
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
                    TiempoViaje: vmFormParametrosIniciales.TiempoViaje(),
                    HoraInicio: vmFormParametrosIniciales.HoraInicio(),
                    HoraFin: vmFormParametrosIniciales.HoraFin(),
                    HoraMaxima: vmFormParametrosIniciales.HoraMaxima(),
                    TiempoEvaluacion: vmFormParametrosIniciales.TiempoEvaluacion(),
                    TiempoReceso: vmFormParametrosIniciales.TiempoReceso(),
                    TiempoReal: vmFormParametrosIniciales.TiempoReal()
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

        vmFormParametrosIniciales.EventoCambioFecha = function () {
            if (vmFormParametrosIniciales.HoraInicio() != "") {
                
            } else {
                error("No debe estar vacio");
            }

            if (vmFormParametrosIniciales.HoraFin() != "") {

            } else {
                error("No debe estar vacio");
            }

            if (vmFormParametrosIniciales.TiempoEvaluacion() != "") {

            } else {
                error("No debe estar vacio");
            }

            if (vmFormParametrosIniciales.TiempoReceso() != "") {

            } else {
                error("No debe estar vacio");
            }

            //vmFormParametrosIniciales.TiempoReal(vmFormParametrosIniciales.HoraInicio() + vmFormParametrosIniciales.TiempoReceso());

            //while (vmFormParametrosIniciales.HoraSession() <= vmFormParametrosIniciales.HoraInicio()) {

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
            vmFormParametrosIniciales.HoraMaxima = ko.observable("");
            vmFormParametrosIniciales.HoraInicio = ko.observable("");
            vmFormParametrosIniciales.HoraFin = ko.observable("");
            vmFormParametrosIniciales.TiempoEvaluacion = ko.observable("");
            vmFormParametrosIniciales.TiempoReceso = ko.observable("");
            vmFormParametrosIniciales.TiempoReal = ko.observable("");
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
            vmFormParametrosIniciales.HoraMaxima(vmFormParametrosIniciales.ParametrosIniciales.HoraMaxima());
            vmFormParametrosIniciales.HoraInicio(vmFormParametrosIniciales.ParametrosIniciales.HoraInicio());
            vmFormParametrosIniciales.HoraFin(vmFormParametrosIniciales.ParametrosIniciales.HoraFin());
            vmFormParametrosIniciales.TiempoEvaluacion(vmFormParametrosIniciales.ParametrosIniciales.TiempoEvaluacion());
            vmFormParametrosIniciales.TiempoReceso(vmFormParametrosIniciales.ParametrosIniciales.TiempoReceso());
            vmFormParametrosIniciales.TiempoReal(vmFormParametrosIniciales.ParametrosIniciales.TiempoReal());
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

$(function () {
    //$('#datetimepickerStart').datetimepicker({
    //    icons: {
    //        time: 'fa fa-clock-o',
    //        date: 'fa fa-calendar',
    //        up: 'fa fa-chevron-up',
    //        down: 'fa fa-chevron-down',
    //        previous: 'fa fa-chevron-left',
    //        next: 'fa fa-chevron-right',
    //        today: 'fa fa-crosshairs',
    //        clear: 'fa fa-trash',
    //    },
    //    format: 'HH:mm'
    //});

    //$('#datetimepickerEnd').datetimepicker({
    //    icons: {
    //        time: 'fa fa-clock-o',
    //        date: 'fa fa-calendar',
    //        up: 'fa fa-chevron-up',
    //        down: 'fa fa-chevron-down',
    //        previous: 'fa fa-chevron-left',
    //        next: 'fa fa-chevron-right',
    //        today: 'fa fa-crosshairs',
    //        clear: 'fa fa-trash',
    //        format: "mm-dd-yyyy"
    //    },
    //    format: 'HH:mm'
    //});

    //$('#datetimepicker1').datetimepicker();
    //$('#datetimepicker2').datetimepicker();
    //$('#datetimepicker10').datetimepicker();
    //$('#datetimepicker20').datetimepicker();

    //$("#datetimepicker1").on("dp.change", function (e) {
    //    $('#datetimepicker2').data("DateTimePicker").minDate(e.date);
    //});
    //$("#datetimepicker2").on("dp.change", function (e) {
    //    $('#datetimepicker1').data("DateTimePicker").maxDate(e.date);
    //});
    //$("#datetimepicker10").on("dp.change", function (e) {
    //    $('#datetimepicker20').data("DateTimePicker").minDate(e.date);
    //});
    //$("#datetimepicker20").on("dp.change", function (e) {
    //    $('#datetimepicker10').data("DateTimePicker").maxDate(e.date);
    //});
});

function CheckTime(str) {
    hora = str.value;
    if (hora == '') {
        return;
    }
    if (hora.length > 8) {
        error("Introdujo una cadena mayor a 8 caracteres");
        return;
    }
    if (hora.length != 8) {
        error("Introducir HH:MM:SS");
        return;
    }
    a = hora.charAt(0); //<=2
    b = hora.charAt(1); //<4
    c = hora.charAt(2); //:
    d = hora.charAt(3); //<=5
    e = hora.charAt(5); //:
    f = hora.charAt(6); //<=5
    if ((a == 2 && b > 3) || (a > 2)) {
        error("El valor que introdujo en la Hora no corresponde, introduzca un digito entre 00 y 23");
        return;
    }
    if (d > 5) {
        error("El valor que introdujo en los minutos no corresponde, introduzca un digito entre 00 y 59");
        return;
    }
    if (f > 5) {
        error("El valor que introdujo en los segundos no corresponde");
        return;
    }
    if (c != ':' || e != ':') {
        error("Introduzca el caracter ':' para separar la hora, los minutos y los segundos");
        return;
    }
}