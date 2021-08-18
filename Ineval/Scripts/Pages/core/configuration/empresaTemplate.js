
debugger
var empresaTemplate = function () {
    var p;
    var plantilla;

    var $container;
    var $titleActions;

    var $typeId;
    var $code;
    var $description;
    var $head;
    var $detalle;
    var $pie;

    var initEvents = function () {
        debugger
        $container = $("#frm-empresa-template");
        $titleActions = $(".title_actions");

        $typeId = $("#TypeId");
        $code = $("#Code");
        $description = $("#Description");

        CKEDITOR.on('instanceCreated', function (event) {
            var editor = event.editor,
                element = editor.element;


            //$cabecera = CKEDITOR.instances['Head'];
            $detalle = CKEDITOR.instances['Body'];
            $pie = CKEDITOR.instances['Foot'];

            $head = CKEDITOR.document.getById('Head');

            $head.setHtml(
                'Detalle de Cabecera de Plantilla de impresion'
            );



            editor.on('configLoaded', function () {

                // Remove unnecessary plugins to make the editor simpler.
                editor.config.removePlugins = 'find,flash,reference' +
                    'newpage,' +
                    'smiley,specialchar,templates,iframe,save';

                // Rearrange the layout of the toolbar.
                editor.config.toolbarGroups = [
                    { name: 'clipboard', groups: ['clipboard', 'undo'] },
                    { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
                    { name: 'links' },
                    { name: 'insert' },
                    { name: 'forms' },
                    { name: 'tools' },
                    { name: 'document', groups: ['mode', 'document', 'doctools'] },
                    { name: 'others' },
                    '/',
                    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
                    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align'] },
                    { name: 'styles' },
                    { name: 'colors' },
                    { name: 'about' }
                ];
            });
        });

        $("#grabar-plantilla").click(function () {
            empresaTemplate.grabar();
        });

        $("#TypeId").change(function () {
            getCamposPlantillas();
        });
    }

    var getCamposPlantillas = function () {

        var data = {
            tipoPlantilla: $typeId.val()
        };

        $("#campos-disponibles").empty();

        $.ajax({
            url: "/EmpresaTemplate/GetColumn",
            data: data,
            dataType: "json",
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                debugger
                if (result.success) {
                    debugger
                    $.each(result.modelo, function (index, grupo) {
                        var camposString = "";
                        camposString += "<b>" + grupo.nombre + "</b>";
                        camposString += "<ul>";

                        for (var i = 0; i < grupo.campos.length; i++) {
                            camposString += "<li>" + grupo.campos[i] + "</li>";
                        }
                        camposString += "</ul>";
                        $("#campos-disponibles").append(camposString);
                    });


                } else {
                    error(result.message);
                }
            }
        });

    }

    var handleValidacion = function () {
        $container.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: true, // do not focus the last invalid input
            ignore: "",
            rules: {},
            messages: {},
            invalidHandler: function (event, validator) { //display error alert on form submit
            },
            highlight: function (element) { $(element).closest('.form-group').addClass('has-error'); },
            unhighlight: function (element) { $(element).closest('.form-group').removeClass('has-error'); },
            success: function (label) { label.closest('.form-group').removeClass('has-error'); },
            submitHandler: function (form) {
                savePlantilla();
            }
        });
    };

    var getModelo = function () {
        plantilla.code = $code.val();
        plantilla.description = $description.val();
        plantilla.typeId = $typeId.val();
        plantilla.head = CKEDITOR.instances['Head'].getData();
        plantilla.body = CKEDITOR.instances['Body'].getData();
        plantilla.foot = CKEDITOR.instances['Foot'].getData();
        return plantilla;
    }

    var savePlantilla = function () {
        debugger
        var data = {
            model: plantilla
        };

        getModelo();

        $.ajax({
            url: "/EmpresaTemplate/SaveEmpresaTemplate",
            data: JSON.stringify(data),
            dataType: "json",
            type: "post",
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                if (d.success) {
                    success(d.message || "Grabado Correctamente !!");
                    var url = "/EmpresaTemplate"
                    window.location.href = url;
                    //plantilla.id = result.id;
                    //p.accion = "ver";

                    //window.location = baseUrl + "/Core/PlantillaImpresion/Ver/" + plantilla.id;

                } else {
                    error(d.message);
                }
            }
        });

    }

    var loadPlantilla = function () {


        // setEstadoPlantilla();

        $code.val(plantilla.code);
        $description.val(plantilla.description);
        $typeId.val(plantilla.typeId);

        if (typeof (plantilla.head) != "undefined") {

            CKEDITOR.instances['Head'].setData(plantilla.head);
            CKEDITOR.instances['Body'].setData(plantilla.body);
            CKEDITOR.instances['Foot'].setData(plantilla.foot);

        }

        //if (plantilla.tipoPlantillaImpresion > 0) {
        //    getCamposPlantillas();
        //}

        //if (p.accion === "ver") {
        //    setSololectura();

        //    $container.css("visibility", "visible");
        //    $titleActions.css("visibility", "visible");

        //    $("#campos-div").hide();
        //    $("#datos-div").removeClass().addClass("col-md-12 col-lg-12 col-xs-12");
        //}

        //if (p.accion === "editar") {
        //    $container.css("visibility", "visible");
        //    $titleActions.css("visibility", "visible");

        //    $("#campos-div").show();
        //    $("#datos-div").removeClass().addClass("col-md-10 col-lg-10 col-xs-10");
        //}

        //if (p.accion === "nuevo") {
        //    $container.css("visibility", "visible");
        //    $titleActions.css("visibility", "visible");

        //    $("#campos-div").show();
        //    $("#datos-div").removeClass().addClass("col-md-10 col-lg-10 col-xs-10");
        //}

    };

    var newPlantilla = function () {

        plantilla = {
            fechaPlantilla: formatDate(new Date()),
            estadoPlantilla: 1, // Borrador
            tipoPlantillaImpresion: p.config.tipoPlantillaImpresion
        }
    };

    return {

        init: function (params) {

            p = params;
            debugger
            initEvents();

            if (p.id > 0) {
                getPlantilla(p.id);
            } else {
                newPlantilla();
                loadPlantilla();
            }

            handleValidacion();

        },

        validar: function () {
            validar();
        },

        grabar: function (opcion) {
            $container.submit();
        },

        plantilla: function () {
            return plantilla;
        },

        getPlantilla: function () {
            return getPlantilla();
        }
    }
}();

(function () {

})();



$(function () {
    empresaTemplate.init({
        id: "",
        accion: "nuevo",
        config: { "typeId": 0, "cabecera": "CAB", "detalle": "DET", "pie": "", "code": "", "description": "" },
    });
})

