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
                    //    alert("El checkbox con valor " + $(this).val() + " está seleccionado");
                    arrayPropertiesRiesgos.push(properties);
                }
                
            );
            vmFormBMI.DatosExcelIDS(arrayPropertiesRiesgos);
            vmFormBMI.NombreDocumento($("#NombreDocumento").val());

            if (vmFormBMI.DatosExcelIDS().length > 0 && vmFormBMI.NombreDocumento() != "") {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/DatosExcelCabecera/Generar",
                    data: JSON.stringify({
                        Ids: ko.toJS(vmFormBMI.DatosExcelIDS())
                    }),
                    success: function (Data) {
                        if (Data != null) {
                            window.open(`/DatosExcelCabecera/ExportarExcel?cabecera=${Data.cabecera}&NombreDocumento=${vmFormBMI.NombreDocumento()}`, "_blank");
                        }
                        vmFormBMI = {};
                        //window.location = "/DatosExcelCabecera"
                        $("#ContentGeneral").load("/DatosExcelCabecera");
                        $("#ContentGeneral").show();
                    }
                });
            } else {
                swal("error", "Debe seleccionar los items y llenar el Nombre del Documento", "error");
            }

        }

        ko.cleanNode($("#ContentGeneral")[0]);
        ko.applyBindings(vmFormBMI, $("#ContentGeneral")[0]);

        vmFormBMI.DatosExcel(vmFormBMI.ListaDatosExcel());

    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/DatosExcelCabecera/GetFormulario?id=" + 5,
        success: KnockoutFormBMI
    });
});

function ExportarExcel() {
    window.location = "/DatosExcelCabecera/ExportarExcel?Datos="; //+ vmFormBMI.DatosExcelIDS();
}