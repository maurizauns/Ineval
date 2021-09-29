function CargaMatriz() {
    swal({
        title: 'Seleccione archivo de Importación',
        input: 'file',
        inputAttributes: {
            'accept': '.csv, text/csv',
            'id': 'fileToUpload',
            'aria-label': 'Subir datos de Matriz'
        },
        inputPlaceholder: 'archivo.csv',
        showCancelButton: true,
        confirmButtonColor: "#07A803",
        confirmButtonText: 'Cargar',
        onBeforeOpen: () => {
            $(".swal2-file").change(function () {
                var reader = new FileReader();
                reader.readAsDataURL(this.files[0]);
            });
        }
    }).then((file) => {
        if (file) {
            //$("#ModalCargando").modal("show");
            var formData = new FormData();
            var file = $('.swal2-file')[0].files[0];
            formData.append("archivo", file);
            $.ajax({
                headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') },
                method: 'POST',
                url: "/DatosPersonalTerritorio/AddSustentantesMasiva",
                data: formData,
                processData: false,
                contentType: false,
                success: function (resp) {
                    swal('Exito', 'Cargados', 'success');
                },
                error: function () {
                    $("#ModalCargando").modal("hide");
                    swal({ type: 'error', title: 'Oops...', text: 'Algo salió mal!' })
                },
                complete: function () {
                },
            })
        }
    }).catch(swal.noop)
}

var vmFormTemporales = {};
$(document).ready(function () {

    function KnockoutFormTemporales(temporales) {
        vmFormTemporales = ko.mapping.fromJS(temporales);

        vmFormTemporales.Cancel = function () {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "/DatosPersonalTerritorio/Contar",
                //data: JSON.stringify({
                //    Ids: ko.toJS(vmFormBMI.DatosExcelIDS())
                //}),
                success: function (Data) {
                    if (Data != null) {
                        window.open(`/DatosPersonalTerritorio/ExportarExcel?cabecera=${Data.cabecera}&NombreDocumento=${vmFormBMI.NombreDocumento()}`, "_blank");
                    }
                    vmFormBMI = {};
                    //window.location = "/DatosExcelCabecera"
                    $("#ContentGeneral").load("/DatosPersonalTerritorio");
                    $("#ContentGeneral").show();
                }
            });
        }

        ko.cleanNode($("#ContentGeneral")[0]);
        ko.applyBindings(vmFormTemporales, $("#ContentGeneral")[0]);
    }

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/Asignacion/GetFormulario?id=" + 5,
        success: KnockoutFormTemporales
    });

})



