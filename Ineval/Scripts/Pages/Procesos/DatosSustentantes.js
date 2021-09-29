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
                url: "/DatosSustentantes/AddSustentantesMasiva",
                data: { archivo: formData, Id: vmh.CurrentId() },
                processData: false,
                contentType: false,
                success: function (resp) {
                    //$("#ModalCargando").modal("hide");
                    swal('Exito', 'Cargados', 'success');
                    //Consultar();
                },
                error: function () {
                    $("#ModalCargando").modal("hide");
                    swal({ type: 'error', title: 'Oops...', text: 'Algo salió mal!' })
                },
                complete: function () {
                    //$("#ModalCargando").modal("hide");
                },
            })
        }
    }).catch(swal.noop)
}