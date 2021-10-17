var vmFormTemporales = {};
$(document).ready(function () {

    function KnockoutFormTemporales(temporales) {
        debugger
        vmFormTemporales = ko.mapping.fromJS(temporales);
        vmFormTemporales.NumeroTotal = ko.observable("");        
        vmFormTemporales.NumeroTotal(vmFormTemporales.Total());        

        vmFormTemporales.Cancel = function () {
        }

        vmFormTemporales.MigrarDatosSustentantes = function () {

        }

        vmFormTemporales.MigrarDatosInstituciones = function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/DatosTemporales/MigrarDatosInstituciones",
                data: JSON.stringify({
                    Id: vmh.CurrentId()

                }),
                beforeSend: function () { // Before we send the request, remove the .hidden class from the spinner and default to inline-block.
                    _load();
                },
                success: function (Data) {
                    //_load();
                    swal(Data.message, "", "success");
                    vmFormTemporales = {};
                    $("#Content").load(vmh.CurrentUrl());
                    $("#Content").show();

                },
                complete: function () {
                    _stopLoad();
                },
            });
        }

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

var Table = null;
var TableOtros = null;

$(document).ready(function () {


    CreateDataTable();
    CreateDataTablePasivos();

    function CreateDataTable() {
        Table = $('#cargarDatosPT').DataTable({
            'paging': true, // Table pagination
            'ordering': true, // Column ordering 
            'info': true, // Bottom left status text
            "aaSorting": [],
            'iDisplayLength': 10,
            "language": {
                "emptyTable": "No hay datos disponibles en la tabla",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                "infoEmpty": "No hay registros disponibles",
                "infoFiltered": "(filtrado de _MAX_ registros totales)",
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "searchPlaceholder": "Presionar 'Enter' para buscar...",
                "zeroRecords": "No he encontrado nada - lo siento",
                "paginate": {
                    "first": "Primera",
                    "last": "Última",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            },
            sAjaxSource: '/GetDatos/GetUsers',
            aoColumns: [{
                mData: "Suspendido",
                "render": function (data, type, row) {
                    if (data)
                        return '<span class="badge badge-pill label-danger">I</span>';
                    else
                        return '<span class="badge badge-pill badge-primary">A</span>';
                }
            },
            { mData: "CedulaMedico" },
            {
                mData: "LastName",
                "render": function (data, type, row) {
                    var result = "";
                    result = row["LastName"] + " " + row["FirstName"];
                    return result;
                }
            },
            { mData: "UserName" },
            {
                mData: "Roles",
                "render": function (data, type, row) {
                    var Roles = "";
                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            Roles = Roles + '<span class="badge badge-pill badge-primary">' + lista[i] + '</span>';
                        }
                    }
                    return Roles;
                }
            },
            { mData: "Ciudad" },
            { mData: "Producto" },
            {
                mData: "Avisos",
                "render": function (data, type, row) {
                    var result = "";
                    if (row["FacturacionElectronica"]) {
                        result += "Facturación" + "<br/>";
                    }
                    if (row["Telemedicina"] != null) {
                        if (row["Telemedicina"]) {
                            result += "Telemedicina" + "<br/>";
                        }
                    }
                    if (row["Suspendido"]) {
                        result += "Suspendido" + "<br/>";
                    }
                    if (row["Deuda"]) {
                        result += "Deuda" + "<br/>";
                    }
                    if (row["Quirurgic"] != null) {
                        if (row["Quirurgic"]) {
                            result += "Quirurgic" + "<br/>";
                        }
                    }
                    return result;
                }
            },

            {
                mData: "Pharmacys",
                "render": function (data, type, row) {
                    var fasmacias = "";

                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            fasmacias = fasmacias + (fasmacias === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return fasmacias;
                }
            },
            {
                mData: "Convenios",
                "render": function (data, type, row) {
                    var convenios = "";

                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            convenios = convenios + (convenios === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return convenios;
                }
            },
            {
                mData: "RelatedUsers",
                "render": function (data, type, row) {
                    var Relacionados = "";

                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            Relacionados = Relacionados + (Relacionados === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return Relacionados;
                }
            },
            {
                mData: "OtherRelatedUsers",
                "render": function (data, type, row) {
                    var Relacionados = "";
                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            Relacionados = Relacionados + (Relacionados === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return Relacionados;
                }
            },
            {
                mData: "Action",
                "render": function (data, type, row) {
                    let html = ""
                    if (row["ValidacionCorreo"]) {
                        html += "<li><a class='btn btnreliv blue btn-xs btn-wm-table' type='button' id='Contrasena' onclick='ActivarUsuario(" + row["Id"] + ")'>Activar</a></li> ";
                    } else {
                        html += "<li><a class='btn btnreliv blue btn-xs btn-wm-table' type='button' id='Contrasena' onclick='DesactivarUsuario(" + row["Id"] + ")'>Desactivar</a></li> ";
                    }
                    if (row.UserName === 'Administrador') {
                        return '<a class="btn btnreliv green btn-xs btn-wm-table" href="/UsersAdmin/Details/' + row["Id"] + '">Detalles</a>' +
                            '<a class="btn btnreliv cancel btn-xs btn-wm-table" href="/UsersAdmin/ChangePassword/' + row["Id"] + '">Contraseña</a>' +
                            html;
                    } else {
                        return '<div class="btn-group"><button type="button" class="btn btn-primary dropdown-toggle btn-xs btn-wm-table" data-toggle="dropdown" aria-expanded="true">Acción <span class="caret"></span> </button><ul class="dropdown-menu pull-right " role="menu">' +
                            '<li><a class="btn btn-info btn-xs btn-wm-table" href="/UsersAdmin/LoginAdmin/' + row["Id"] + '">Login</a></li>' +
                            '<li><a class="btn btnreliv green btn-xs btn-wm-table" href="/UsersAdmin/Details/' + row["Id"] + '">Detalles</a></li>' +
                            '<li><a class="btn btnreliv orange btn-xs btn-wm-table" href="/UsersAdmin/Edit/' + row["Id"] + '">Editar</a></li>' +
                            '<li><a class="btn btnreliv red btn-xs btn-wm-table" href="/UsersAdmin/Delete/' + row["Id"] + '">Borrar</a></li>' +
                            '<li><a class="btn btnreliv cancel btn-xs btn-wm-table" href="/UsersAdmin/ChangePassword/' + row["Id"] + '">Contraseña</a></li>' +
                            html +
                            '</ul></div > ';
                    }
                }
            }
            ],
            responsive: true
        });

        $('#UsuariosTable tbody').on('click', 'tr', function () {
            //console.log(Table.row(this).data());
        });

        $('#UsuariosTable tbody').on('click', 'td', function () {
            //var idx = Table.cell(this).index().column;
            ////console.log(idx);
            //var title = Table.column(idx).header();

            //alert( 'Column title clicked on: '+$(title).html() );
        });


    };

    //PASIVOS
    function CreateDataTablePasivos() {
        TableOtros = $('#UsuariosTablePasivos').DataTable({
            'paging': true, // Table pagination
            'ordering': true, // Column ordering 
            'info': true, // Bottom left status text
            "aaSorting": [],
            'iDisplayLength': 10,
            "language": {
                "emptyTable": "No hay datos disponibles en la tabla",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                "infoEmpty": "No hay registros disponibles",
                "infoFiltered": "(filtrado de _MAX_ registros totales)",
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "searchPlaceholder": "Presionar 'Enter' para buscar...",
                "zeroRecords": "No he encontrado nada - lo siento",
                "paginate": {
                    "first": "Primera",
                    "last": "Última",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            },
            sAjaxSource: '/UsersAdmin/GetUsersPasivosOtros',
            aoColumns: [{
                mData: "Suspendido",
                "render": function (data, type, row) {
                    if (data)
                        return '<span class="badge badge-pill label-danger">I</span>';
                    else
                        return '<span class="badge badge-pill badge-primary">A</span>';
                }
            },
            { mData: "CedulaMedico" },
            {
                mData: "LastName",
                "render": function (data, type, row) {
                    var result = "";
                    result = row["LastName"] + " " + row["FirstName"];
                    return result;
                }
            },
            { mData: "UserName" },
            {
                mData: "Roles",
                "render": function (data, type, row) {
                    var Roles = "";
                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            Roles = Roles + '<span class="badge badge-pill badge-primary">' + lista[i] + '</span>';
                        }
                    }
                    return Roles;
                }
            },
            { mData: "Ciudad" },
            { mData: "Producto" },
            {
                mData: "Avisos",
                "render": function (data, type, row) {
                    var result = "";
                    if (row["FacturacionElectronica"]) {
                        result += "Facturación" + "<br/>";
                    }
                    if (row["Telemedicina"] != null) {
                        if (row["Telemedicina"]) {
                            result += "Telemedicina" + "<br/>";
                        }
                    }
                    if (row["Suspendido"]) {
                        result += "Suspendido" + "<br/>";
                    }
                    if (row["Deuda"]) {
                        result += "Deuda" + "<br/>";
                    }
                    if (row["Quirurgic"] != null) {
                        if (row["Quirurgic"]) {
                            result += "Quirurgic" + "<br/>";
                        }
                    }
                    return result;
                }
            },

            {
                mData: "Pharmacys",
                "render": function (data, type, row) {
                    var fasmacias = "";

                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            fasmacias = fasmacias + (fasmacias === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return fasmacias;
                }
            },
            {
                mData: "Convenios",
                "render": function (data, type, row) {
                    var convenios = "";

                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            convenios = convenios + (convenios === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return convenios;
                }
            },
            {
                mData: "RelatedUsers",
                "render": function (data, type, row) {
                    var Relacionados = "";

                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            Relacionados = Relacionados + (Relacionados === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return Relacionados;
                }
            },
            {
                mData: "OtherRelatedUsers",
                "render": function (data, type, row) {
                    var Relacionados = "";
                    if (data !== null && data !== "") {
                        var lista = data.split(';');
                        for (var i = 0; i < lista.length; i++) {
                            Relacionados = Relacionados + (Relacionados === "" ? lista[i] : ', ' + lista[i]);
                        }
                    }
                    return Relacionados;
                }
            },
            {
                mData: "Action",
                "render": function (data, type, row) {
                    let html = ""
                    if (row["ValidacionCorreo"]) {
                        html += "<li><a class='btn btnreliv blue btn-xs btn-wm-table' type='button' id='Contrasena' onclick='ActivarUsuario(" + row["Id"] + ")'>Activar</a></li> ";
                    } else {
                        html += "<li><a class='btn btnreliv blue btn-xs btn-wm-table' type='button' id='Contrasena' onclick='DesactivarUsuario(" + row["Id"] + ")'>Desactivar</a></li> ";
                    }
                    if (row.UserName === 'Administrador') {
                        return '<a class="btn btnreliv green btn-xs btn-wm-table" href="/UsersAdmin/Details/' + row["Id"] + '">Detalles</a>' +
                            '<a class="btn btnreliv cancel btn-xs btn-wm-table" href="/UsersAdmin/ChangePassword/' + row["Id"] + '">Contraseña</a>' +
                            html;
                    } else {
                        return '<div class="btn-group"><button type="button" class="btn btn-primary dropdown-toggle btn-xs btn-wm-table" data-toggle="dropdown" aria-expanded="true">Acción <span class="caret"></span> </button><ul class="dropdown-menu pull-right " role="menu">' +
                            '<li><a class="btn btn-info btn-xs btn-wm-table" href="/UsersAdmin/LoginAdmin/' + row["Id"] + '">Login</a></li>' +
                            '<li><a class="btn btnreliv green btn-xs btn-wm-table" href="/UsersAdmin/Details/' + row["Id"] + '">Detalles</a></li>' +
                            '<li><a class="btn btnreliv orange btn-xs btn-wm-table" href="/UsersAdmin/Edit/' + row["Id"] + '">Editar</a></li>' +
                            '<li><a class="btn btnreliv red btn-xs btn-wm-table" href="/UsersAdmin/Delete/' + row["Id"] + '">Borrar</a></li>' +
                            '<li><a class="btn btnreliv cancel btn-xs btn-wm-table" href="/UsersAdmin/ChangePassword/' + row["Id"] + '">Contraseña</a></li>' +
                            html +
                            '</ul></div > ';
                    }
                }
            }
            ],
            responsive: true
        });

        $('#UsuariosTable tbody').on('click', 'tr', function () {
            //console.log(Table.row(this).data());
        });

        $('#UsuariosTable tbody').on('click', 'td', function () {
            //var idx = Table.cell(this).index().column;
            ////console.log(idx);
            //var title = Table.column(idx).header();

            //alert( 'Column title clicked on: '+$(title).html() );
        });


    }



});