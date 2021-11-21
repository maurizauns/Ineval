$(function () {

    function EnableInputs() {
        let element = Array.from($('#frm').find('input'))
        element.map(el => {
            $('#'+el.id).prop("disabled",true)
            
        })
        $('#btn-guardar').css("display", "none")
        $('#btn-cancelar').css("display", "none")
        $('#btn-editar').css("display", "inline-block")
    }
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/ParametrosIniciales/GetFormulario?id=" + vmh.CurrentId(),
        success: function (r) {
            console.log(r.ParametrosIniciales)
            if (r.ParametrosIniciales.HoraInicio == "") {
                $('#btn-nuevo').css("display", "inline-block")
                $('#existe').css("display", "inline-block")
            } else {
                EnableInputs()
                $('#btn-nuevo').css("display", "none")
                $('#existe').css("display", "none")
                $('#txt-HoraInicio').val(r.ParametrosIniciales.HoraInicio)
                $('#txt-HoraFin').val(r.ParametrosIniciales.HoraFin)
                $('#txt-HoraEvaluacion').val(r.ParametrosIniciales.TiempoEvaluacion)
                $('#txt-HoraReceso').val(r.ParametrosIniciales.TiempoReceso)
                $('#NumSesion').val(r.ParametrosIniciales.NumerosSesiones)
                $('#txt-diaseval').val(r.ParametrosIniciales.NumeroDiasEvaluar)
                $('#txt-tiempoViaje').val(r.ParametrosIniciales.TiempoViaje)
                $('#Id').val(r.ParametrosIniciales.Id)
                $('#NumeroSustentantes').val(r.ParametrosIniciales.NumeroEquipos)
                if (r.ParametrosIniciales.Tipo != null) {
                    if (r.ParametrosIniciales.Tipo == 1) {
                        $("#tipoNacional").attr("checked", true);
                        $("#tipoInterno").attr("checked", false);
                    } else {
                        $("#tipoInterno").attr("checked", true);
                        $("#tipoNacional").attr("checked", false);
                    }
                } else {
                    $("#tipoNacional").attr("checked", true);
                    $("#tipoInterno").attr("checked", false);
                }
            }
            
            
        }
    });

    $('#btn-editar').click(function () {
        let element = Array.from($('#frm').find('input'))
        element.map(el => {
            $('#' + el.id).prop("disabled", false)

        })
        $('#btn-guardar').css("display", "inline-block")
        $('#btn-editar').css("display", "none")
        $('#btn-cancelar').css("display", "inline-block")
    })

    $('#btn-cancelar').click(function () {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "/ParametrosIniciales/GetFormulario?id=" + vmh.CurrentId(),
            success: function (r) {
                console.log(r.ParametrosIniciales)
                if (r.ParametrosIniciales.HoraInicio == "") {
                    $('#btn-nuevo').css("display", "inline-block")
                    $('#existe').css("display", "inline-block")
                } else {
                    EnableInputs()
                    $('#btn-nuevo').css("display", "none")
                    $('#existe').css("display", "none")
                    $('#txt-HoraInicio').val(r.ParametrosIniciales.HoraInicio)
                    $('#txt-HoraFin').val(r.ParametrosIniciales.HoraFin)
                    $('#txt-HoraEvaluacion').val(r.ParametrosIniciales.TiempoEvaluacion)
                    $('#txt-HoraReceso').val(r.ParametrosIniciales.TiempoReceso)
                    $('#NumSesion').val(r.ParametrosIniciales.NumerosSesiones)
                    $('#txt-diaseval').val(r.ParametrosIniciales.NumeroDiasEvaluar)
                    $('#txt-tiempoViaje').val(r.ParametrosIniciales.TiempoViaje)
                    $('#Id').val(r.ParametrosIniciales.Id)
                    $('#NumeroSustentantes').val(r.ParametrosIniciales.NumeroEquipos)
                    if (r.ParametrosIniciales.Tipo != null) {
                        if (r.ParametrosIniciales.Tipo == 1) {
                            $("#tipoNacional").attr("checked", true);
                            $("#tipoInterno").attr("checked", false);
                        } else {
                            $("#tipoInterno").attr("checked", true);
                            $("#tipoNacional").attr("checked", false);
                        }
                    } else {
                        $("#tipoNacional").attr("checked", true);
                        $("#tipoInterno").attr("checked", false);
                    }
                }


            }
        });
    })

    $('#txt-diaseval').keyup(function () {

        if ($(this).val() != "") {
            calculoSesion()
        }
    })

    $('#btn-guardar').click(function () {
        let horainicio = $('#txt-HoraInicio').val()
        let horafin = $('#txt-HoraFin').val()
        let horaevaluacion = $('#txt-HoraEvaluacion').val()
        let horareceso = $('#txt-HoraReceso').val()
        let fechaInicio = moment("01/01/2021 " + horainicio, 'DD/MM/YYYY HH:mm').format('DD/MM/YYYY HH:mm');
        let fechaFin = moment("01/01/2021 " + horafin, 'DD/MM/YYYY HH:mm').format('DD/MM/YYYY HH:mm');
        console.log(fechaInicio)
        console.log(fechaFin)

        if (fechaInicio > fechaFin) {
            error("La hora inicio no puede ser mayor a la hora fin")

        } else if (horaevaluacion == "") {
            error("La hora de evaluación debe ser definida")
        } else if (horareceso == "") {
            error("La hora de receso debe ser definida")
        } else if ($('#NumSesion').val() == "") {
            error("El numero de sesiones debe ser definida")
        } else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ParametrosIniciales/Save",
                data: JSON.stringify({
                    Id: $('#Id').val(),
                    AsignacionId: vmh.CurrentId(),
                    SinoNumeroLaboratorios: true,
                    NumeroLaboratorios: 1,
                    SinoNumeroEquipos: true,
                    NumeroEquipos: $("#NumeroSustentantes").val(),
                    SinoNumerosSesiones: true,
                    NumerosSesiones: $('#NumSesion').val(),
                    SinoNumeroDiasEvaluar: true,
                    NumeroDiasEvaluar: $('#txt-diaseval').val(),
                    SinoDuracionPrueba: true,
                    DuracionPrueba: 1,
                    SinoTiempoViaje: true,
                    TiempoViaje: $('#txt-tiempoViaje').val(),
                    HoraInicio: $('#txt-HoraInicio').val(),
                    HoraFin: $('#txt-HoraFin').val(),
                    HoraMaxima: $('#txt-HoraFin').val(),
                    TiempoEvaluacion: $('#txt-HoraEvaluacion').val(),
                    TiempoReceso: $('#txt-HoraReceso').val(),
                    TiempoReal: "",
                    Tipo: $('input:radio[name=tipo]:checked').val()

                }),
                success: function (Data) {
                    swal(Data.message, "Registro Guardado", "success");
                    vmFormParametrosIniciales = {};

                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();

                }
            });

        }
    })

})

function Valhora1(t) {

    let horafin = $('#txt-HoraFin').val()
    if (t.value > horafin) {
        error("La hora de inicio no puede ser mayor a la hora fin")
        t.value = ""
    }
    calculoSesion()
}

function Valhora2(t) {
    let horainicio = $('#txt-HoraInicio').val()
    if (t.value < horainicio) {
        error("La hora fin no puede ser menor a la hora de inicio")
        t.value = ""
    }
    calculoSesion()
}

function ValEvaluacion(t) {
    let horaeval = t.value.split(':')
    if (parseInt(horaeval[0]) > 8) {
        error("La hora de evaluación no puede ser mayor a 8 horas")
        t.value = ""
    } else {
        if (parseInt(horaeval[0]) == 8 && parseInt(horaeval[1]) > 0) {
            error("La hora de evaluación no puede ser mayor a 8 horas")
            t.value = ""
        }
    }
    calculoSesion()
}

function calculoSesion() {
    let horainicio = $('#txt-HoraInicio').val()
    let horafin = $('#txt-HoraFin').val()
    let horaevaluacion = $('#txt-HoraEvaluacion').val()
    let horareceso = $('#txt-HoraReceso').val()
    let diaeval = $('#txt-diaseval').val()



    if (horainicio == "" || horafin == "" || horaevaluacion == "" || horareceso == "" || diaeval == "") {
        $('#NumSesion').val("")
    } else {
        var eval1 = horaevaluacion.split(':')
        let horeval = (parseInt(eval1[0]) * 60)
        horeval = horeval + parseInt(eval1[1])
        var diffInMinutes = moment(horafin, "HH:mm").diff(moment(horainicio, "HH:mm"), 'minutes');
        horeval = transforMinute(horareceso, horeval)
        //var di = getTimeFromMins(diffInMinutes)
        //var di1 = moment(di, "HH:mm").diff(moment(horareceso, "HH:mm"), 'minutes');
        //var di1 = moment(di, 'HH:mm').add(moment(horareceso, "HH:mm"), 'minutes').minutes();
        console.log(horeval)
        var divi = (diffInMinutes / horeval)
        $('#NumSesion').val(Math.round(divi * parseInt(diaeval)))

    }

}

function transforMinute(input,horv) {
    let minutes = 0;

    var eval1 = input.split(':')
    let horeval = (parseInt(eval1[0]) * 60)
    minutes = horeval + parseInt(eval1[1])


    return minutes + horv

}

function getTimeFromMins(mins) {

    if (mins >= 24 * 60 || mins < 0) {
        throw new RangeError("Valid input should be greater than or equal to 0 and less than 1440.");
    }
    var h = mins / 60 | 0,
        m = mins % 60 | 0;
    return moment.utc().hours(h).minutes(m).format("hh:mm");
}



function ValReceso(t) {
    let horaeval = t.value.split(':')
    if (parseInt(horaeval[0]) > 1) {
        error("La hora de receso no puede ser mayor a 1 hora")
        t.value = ""
    } else {
        if (parseInt(horaeval[0]) == 1 && parseInt(horaeval[1]) > 0) {
            error("La hora de receso no puede ser mayor a 1 hora")
            t.value = ""
        }
    }
    calculoSesion()
}