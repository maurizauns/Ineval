

$(function () {

    function EnableInputs() {
        let element = Array.from($('#frm').find('input'))
        element.map(el => {
            $('#' + el.id).prop("disabled", true)
        })

        let elementcheckbox = Array.from($('#frm').find('checkbox'))
        elementcheckbox.map(el => {
            $('#' + el.id).prop("disabled", true)
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
            if (r.ParametrosIniciales === null) {
                $('#btn-nuevo').css("display", "inline-block")
                $('#existe').css("display", "inline-block")
            } else {
                if (r.ParametrosIniciales.HorariosSesion) {
                    JSON.parse(r.ParametrosIniciales.HorariosSesion).map((x, i) => {

                        document.getElementById('detalle_datos').innerHTML += `<tr>
                                                                <td>${x.sesion}</td>
                                                                <td>${x.fecha}</td>
                                                                 <td>${x.hora}</td>
                                                               </tr>`

                    })
                }
                
               // console.log(r)
                $('#txt-FechaSesion').val(moment(r.ParametrosIniciales.FechaSesion).format('YYYY-MM-DD'))
                EnableInputs()
                $('#btn-nuevo').css("display", "none")
                $('#existe').css("display", "none")
                //nuevo
                //$('#txt-FechaSesion').val(r.ParametrosIniciales.FechaSesion)

                $('#txtTotalSustentantes').val(r.ParametrosIniciales.SustentantesTotales),
                    $('#txtTotalSesionesOcupar').val(r.ParametrosIniciales.SesionesOcupar),
                    $('#txtTotalLaboratorios').val(r.ParametrosIniciales.TotalLaboratorios)

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

                $('#chk-aplicarnumlabo').attr("checked", r.ParametrosIniciales.SiNoNumeroLaboratorios);
                $('#txt-NumeroLaboratorios').val(r.ParametrosIniciales.NumeroLaboratorios);

            }


        }
    });

    $('#btn-editar').click(function () {
        let element = Array.from($('#frm').find('input'))
        element.map(el => {
            $('#' + el.id).prop("disabled", false)

        })

        let elementcheckbox = Array.from($('#frm').find('checkbox'))
        elementcheckbox.map(el => {
            $('#' + el.id).prop("disabled", false)
        })

        let aplicalabo = $("#chk-aplicarnumlabo").is(":checked") ? true : false;
        if (aplicalabo) {
            $('#txt-NumeroLaboratorios').attr('disabled', false);
        } else {
            $('#txt-NumeroLaboratorios').val(0);
            $('#txt-NumeroLaboratorios').attr('disabled', true);
        }


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
                //console.log(r.ParametrosIniciales)
                if (r.ParametrosIniciales.HoraInicio == "") {
                    $('#btn-nuevo').css("display", "inline-block")
                    $('#existe').css("display", "inline-block")
                } else {
                    EnableInputs()
                    $('#btn-nuevo').css("display", "none")
                    $('#existe').css("display", "none")
                    //nuevo
                    //$('#txt-FechaSesion').val(r.ParametrosIniciales.FechaSesion)

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

                    $('#chk-aplicarnumlabo').attr("checked", r.ParametrosIniciales.SiNoNumeroLaboratorios);
                    $('#txt-NumeroLaboratorios').val(r.ParametrosIniciales.NumeroLaboratorios);


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

        var lista = [];
        var row = $('#tbl_valores tbody tr');
        for (let i = 0; i < row.length; i++) {
            var properties = {};
            properties.sesion = $(row[i]).find("td:eq(0)").html()
            properties.fecha = $(row[i]).find("td:eq(1)").html()
            properties.hora = $(row[i]).find("td:eq(2)").html()
            lista.push(properties);
        }
        
        //console.log(lista)
        if (fechaInicio > fechaFin) {
            error("La hora inicio no puede ser mayor a la hora fin")

        } else if ($('#txt-FechaSesion').val() == "") {
            error("La fecha sesión debe ser definida")
        } else if (FechaAntigua($("#txt-FechaSesion").val()) == false) {
            error("La fecha sesión no puede ser menor a la fecha actual")
        } else if (horaevaluacion == "") {
            error("La hora de evaluación debe ser definida")
        } else if (horareceso == "") {
            error("La hora de receso debe ser definida")
        } else if ($('#NumSesion').val() == "") {
            error("El número de sesiones debe ser definida")
        } else if (parseInt($('#txt-diaseval').val()) > 5) {
            error("El número de días a evaluar no puede ser mayor a 5")
        } else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ParametrosIniciales/Save",
                data: JSON.stringify({
                    Id: $('#Id').val(),
                    AsignacionId: vmh.CurrentId(),
                    SinoNumeroLaboratorios: $("#chk-aplicarnumlabo").is(":checked") ? true : false,
                    NumeroLaboratorios: $('#txt-NumeroLaboratorios').val(),
                    SinoNumeroEquipos: true,
                    //FechaSesion: $("#txt-FechaSesion").val(),
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
                    Tipo: $('input:radio[name=tipo]:checked').val(),
                    FechaSesion: $("#txt-FechaSesion").val(),
                    HorariosSesion: JSON.stringify(lista),
                    SustentantesTotales: $('#txtTotalSustentantes').val(),
                    SesionesOcupar: $('#txtTotalSesionesOcupar').val(),
                    TotalLaboratorios: $('#txtTotalLaboratorios').val()
                    
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
    



    if (horainicio == "" || horafin == "" || horaevaluacion == "" || horareceso == "") {
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
        //console.log(horeval)
        var divi = (diffInMinutes / horeval)
        $('#NumSesion').val(Math.round(divi))
        generar(document.getElementById('NumSesion'))
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
    return moment.utc().hours(h).minutes(m).format("HH:mm");
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

function FechaAntigua(date) {
    
    let con = true;
    let fechasesion = moment(date).format('DD/MM/YYYY');
    let fechaactual = moment().format('DD/MM/YYYY');
    if (fechasesion < fechaactual) {
        con = false
    }

    return con
   
}

/*function generar(el) {
    
    let dias = 0
    let arrayElement = []
    let hort = 0
    let horformater = ''
    
    if ($("#txt-FechaSesion").val() == "") {
        error("Fecha de la sesión debe ser definida")
    } else if (parseInt(el.value) > 5) {
        error("El número de días a evaluar no puede ser mayor a 5")
    } else {
        if (el.value != "") {

            let dia = $("#txt-FechaSesion").val().split("-")
            for (let i = 0; i < parseInt(el.value); i++) {
                
                dias = parseInt(dia[2]) + i

               
                for (let j = 0; j < parseInt($("#NumSesion").val()); j++) {
                    let objectElement = {}
                    
                    if (j == 0) {
                        hort = transforMinuteG($('#txt-HoraInicio').val())
                        
                    } else {
                        hort = (transforMinuteG(horformater) + transforMinuteG($('#txt-HoraEvaluacion').val()) + transforMinuteG($('#txt-HoraReceso').val()))
                    }
                    horformater = getTimeFromMins(hort)
                    objectElement.fecha = dias + `/` + dia[1] + `/` + dia[0]
                    objectElement.hora = horformater
                    arrayElement.push(objectElement)
                }
                
                
            }
            
        }
    }
    document.getElementById('detalle_datos').innerHTML=''
    arrayElement.map((x, i) => {

        document.getElementById('detalle_datos').innerHTML += `<tr>
                                                                <td>S${i+1}</td>
                                                                <td>${x.fecha}</td>
                                                                 <td>${x.hora}</td>
                                                               </tr>`
        
    })


}*/

function transforMinuteG(input) {
    let minutes = 0;

    var eval1 = input.split(':')
    let horeval = (parseInt(eval1[0]) * 60)
    minutes = horeval + parseInt(eval1[1])


    return minutes

}

function generar(el) {
    if ($("#txt-FechaSesion").val() == "") {
        error("Fecha de la sesión debe ser definida")
    } else {
        document.getElementById('detalle_datos').innerHTML = ''
        for (let i = 0; i < el.value; i++) {
            
            

                document.getElementById('detalle_datos').innerHTML += `<tr>
                                                                <td>S${i + 1}</td>
                                                                <td>${$("#txt-FechaSesion").val()}</td>
                                                                 <td></td>
                                                               </tr>`

           
        }

    }

    CalcularTotalLaboratorios(el)
    

}

function validarFechaSesion(el) {
    
    let fechasesion = moment(el.value).format('DD/MM/YYYY');
    let fechaactual = moment().format('DD/MM/YYYY');
    if (fechasesion < fechaactual) {
        error("La fecha sesión no puede ser menor a la fecha actual")
        el.value = ""
        document.getElementById('detalle_datos').innerHTML = ''
    } else {

        generar(document.getElementById('NumSesion'))
    }
}

function AplicarLaboratorio(estado) {
    if (estado.checked) {
        $('#txt-NumeroLaboratorios').attr('disabled', false);
    } else {
        $('#txt-NumeroLaboratorios').val(0);
        $('#txt-NumeroLaboratorios').attr('disabled', true);
    }
}

function CalcularSesionesOcupar(el) {
    let st = el.value;
    let ss = $('#NumeroSustentantes').val()
    if (st != "" && ss !="") {
        let so = parseInt(st) / parseInt(ss)
        $('#txtTotalSesionesOcupar').val(Math.round(so))
    } else {
        $('#txtTotalSesionesOcupar').val("")
    }
    
}

function CalcularSesionesOcupar1(el) {
   
    let ss = el.value;
    let st = $('#txtTotalSustentantes').val()

    if (ss != "" && st !="") {
        let so = parseInt(st) / parseInt(ss)
        $('#txtTotalSesionesOcupar').val(Math.round(so))
    } else {
        $('#txtTotalSesionesOcupar').val("")
    }
    
}

function CalcularTotalLaboratorios(el) {

    let ns = el.value;
    let ts = $('#txtTotalSesionesOcupar').val()

    if (ns != "" && ts != "") {
        let tl = parseInt(ts) / parseInt(ns)
        $('#txtTotalLaboratorios').val(Math.round(tl))
    } else {
        $('#txtTotalLaboratorios').val("")
    }

}