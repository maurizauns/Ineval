var vmFormTemporales = {};
$(document).ready(function () {

    function KnockoutFormTemporales(temporales) {
        vmFormTemporales = ko.mapping.fromJS(temporales);
        vmFormTemporales.NumeroTotal = ko.observable("");
        vmFormTemporales.NumeroTotal(vmFormTemporales.Total());

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormTemporales, $("#Content")[0]);
    }

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/DatosExcelLaboratorio/GetInstituciones?id=" + vmh.CurrentId(),
        beforeSend: function () { // Before we send the request, remove the .hidden class from the spinner and default to inline-block.
            _load();
        },
        success: KnockoutFormTemporales,
        complete: function () {
            _stopLoad();
        },
    });
})

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
            formData.append("Id", vmh.CurrentId());
            $.ajax({
                headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') },
                method: 'POST',
                url: "/DatosExcelLaboratorio/AddSustentantesMasiva",
                data: formData,
                processData: false,
                contentType: false,
                beforeSend: function () { // Before we send the request, remove the .hidden class from the spinner and default to inline-block.
                    _load();
                },
                success: function (resp) {
                    /**/
                    swal('Exito', 'Cargados', 'success');
                    vmFormTemporales = {};
                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();
                },
                complete: function () {
                    _stopLoad();
                },
                error: function () {
                    $("#ModalCargando").modal("hide");
                    swal({ type: 'error', title: 'Oops...', text: 'Algo salió mal!' })
                },
            })
        }
    }).catch(swal.noop)
}