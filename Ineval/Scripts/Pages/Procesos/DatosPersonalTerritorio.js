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
        url: "/DatosPersonalTerritorio/GetDatos?id=" + vmh.CurrentId(),
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

//var Table = null;

//$(document).ready(function () {

//    CreateDataTable();

//    function CreateDataTable() {
//        Table = $('#cargarDatosPT').DataTable({
//            'paging': true, // Table pagination
//            'ordering': true, // Column ordering 
//            'info': true, // Bottom left status text
//            "aaSorting": [],
//            'iDisplayLength': 10,
//            "language": {
//                "emptyTable": "No hay datos disponibles en la tabla",
//                "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
//                "infoEmpty": "No hay registros disponibles",
//                "infoFiltered": "(filtrado de _MAX_ registros totales)",
//                "lengthMenu": "Mostrar _MENU_ registros por página",
//                "loadingRecords": "Cargando...",
//                "processing": "Procesando...",
//                "search": "Buscar:",
//                "searchPlaceholder": "Presionar 'Enter' para buscar...",
//                "zeroRecords": "No he encontrado nada - lo siento",
//                "paginate": {
//                    "first": "Primera",
//                    "last": "Última",
//                    "next": "Siguiente",
//                    "previous": "Anterior"
//                }
//            },
//            sAjaxSource: '/GetDatos/GetUsers',
//            aoColumns: [
//                { mData: "CedulaMedico" },
//                { mData: "CedulaMedico" },
//                { mData: "UserName" },
//                { mData: "UserName" },
//                { mData: "UserName" },
//                { mData: "UserName" },
                
//            ],
//            responsive: true
//        });
//    };
//});