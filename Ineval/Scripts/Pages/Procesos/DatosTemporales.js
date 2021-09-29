var vmFormTemporales = {};
$(document).ready(function () {

    function KnockoutFormTemporales(temporales) {
        vmFormTemporales = ko.mapping.fromJS(temporales);
        vmFormTemporales.NumeroTotal = ko.observable("");
        vmFormTemporales.NumeroTotal(vmFormTemporales.ParametrosIniciales());

        vmFormTemporales.Cancel = function () {
        }

        ko.cleanNode($("#Content")[0]);
        ko.applyBindings(vmFormTemporales, $("#Content")[0]);
    }

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/DatosTemporales/GetFormulario?id=" + vmh.CurrentId(),
        success: KnockoutFormTemporales
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
            debugger
            var file = $('.swal2-file')[0].files[0];
            formData.append("archivo", file);
            formData.append("Id", vmh.CurrentId());
            $.ajax({
                headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') },
                method: 'POST',
                url: "/DatosTemporales/AddSustentantesMasiva",
                data: formData,
                processData: false,
                contentType: false,
                success: function (resp) {                    
                    swal('Exito', 'Cargados', 'success');
                    vmFormParametrosIniciales = {};
                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();
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





