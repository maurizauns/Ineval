var vmFormBMI = {};
$(document).ready(function () {
    var url = window.location.pathname;

    PatientID = url.substring(url.lastIndexOf('/') + 1);

    function KnockoutFormBMI(ListaDatosExcel) {
        vmFormBMI = ko.mapping.fromJS(ListaDatosExcel);
        vmFormBMI.DatosExcel = ko.observable();
        vmFormBMI.DatosExcelIDS = ko.observable([]);
        vmFormBMI.NombreDocumento = ko.observable();



        vmFormBMI.Guardar = function () {
            var arrayPropertiesRiesgos = new Array();
            $('.micheckbox:checked').each(
                function () {
                    var properties = new Object();
                    properties.Id = $(this).val();                    
                    arrayPropertiesRiesgos.push(properties);
                }

            );
            vmFormBMI.DatosExcelIDS(arrayPropertiesRiesgos);
            vmFormBMI.NombreDocumento($("#NombreDocumento").val());

            if (vmFormBMI.DatosExcelIDS().length > 0 && vmFormBMI.NombreDocumento() != "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/DatosExcelLaboratorio/Generar",
                    data: JSON.stringify({
                        Ids: ko.toJS(vmFormBMI.DatosExcelIDS())
                    }),
                    success: function (Data) {
                        if (Data != null) {
                            window.open(`/DatosExcelLaboratorio/ExportarExcel?cabecera=${Data.cabecera}&NombreDocumento=${vmFormBMI.NombreDocumento()}`, "_blank");
                        }
                        vmFormBMI = {};
                        //window.location = "/DatosExcelCabecera"
                        $("#Content").load(vmh.CurrentUrl());
                        $("#Content").show();
                    }
                });
            } else {
                swal("error", "Debe seleccionar los items y llenar el Nombre del Documento", "error");
            }

        }

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormBMI, $("#Content")[0]);

        $("#NombreDocumento").val(vmFormBMI.NombreProceso());
        vmFormBMI.DatosExcel(vmFormBMI.ListaDatosExcel());

    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/DatosExcelLaboratorio/GetFormulario?id=" + vmh.CurrentId(),
        success: KnockoutFormBMI
    });
});

function ExportarExcel() {
    window.location = "/DatosExcelLaboratorio/ExportarExcel?Datos="; //+ vmFormBMI.DatosExcelIDS();
}