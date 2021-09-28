var vmFormAsignacion = {};
$(document).ready(function () {
    function KnockoutFormAsignacion(Asignacion) {
        debugger
        vmFormAsignacion = ko.mapping.fromJS(Asignacion);
        vmFormAsignacion.Edit = ko.observable(false);

        vmFormAsignacion.New = function () {
            debugger
            vmFormAsignacion.Edit(true);
        }

        vmFormAsignacion.Save = function () {

        }

        vmFormAsignacion.Cancel = function () {
            vmFormAsignacion = {};
            window.location = "/Asignacion"
            //$("#ContentGeneral").load("/Asignacion/Index");
            $("#ContentGeneral").show();
        }

        var select = $("#TipoProceso");
        var options = '';
        select.empty();
        options += "<option value=''>Seleccione</option>"
        if (vmFormAsignacion.procesosList().length == 0) {
            options += "<option value=''>No se encontraron Procesos...</option>"
        }
        for (var i = 0; i < vmFormAsignacion.procesosList().length; i++) {
            options += "<option  value='" + vmFormAsignacion.procesosList()[i].Id() + "'>" + vmFormAsignacion.procesosList()[i].Description() + "</option>";
        }
        select.append(options);
        //select.trigger("chosen:updated");
        //$("#TipoPaciente_chosen").width("90%");
        //$(".chosen-select").chosen();

        ko.cleanNode($("#ContentGeneral")[0]);
        ko.applyBindings(vmFormAsignacion, $("#ContentGeneral")[0]);

    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/Asignacion/GetFormulario?id=" + 5,
        success: KnockoutFormAsignacion
    });
});