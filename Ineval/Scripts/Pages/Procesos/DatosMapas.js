if (vmh.PasswordMapBox()) {
    var vmFormViewMapas = {};
    $(document).ready(function () {

        function KnockoutFormMapas(datosMapas) {
            vmFormViewMapas = ko.mapping.fromJS(datosMapas);

            cargarCombo(datosMapas.result, "cmbSedes", "cmbSedes");
            //cargarCombo(datosMapas.Canton, "cmbCanton", "cmbCanton");
            //cargarCombo(datosMapas.Parroquia, "cmbParroquia", "cmbParroquia");

            ko.cleanNode($("#Content")[0]);
            ko.applyBindings(vmFormViewMapas, $("#Content")[0]);
        }

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "/DatosMapas/GetFormulario?id=" + vmh.CurrentId(),
            beforeSend: function () {
                _load();
            },
            success: KnockoutFormMapas,
            complete: function () {
                _stopLoad()
            }            
        });

    })


    //var marker;
    mapboxgl.accessToken = vmh.PasswordMapBox();
    const map = new mapboxgl.Map({
        container: 'map', // container ID
        style: 'mapbox://styles/mapbox/streets-v11', // style URL
        center: [-78.4329147227382, -1.16017132420716], // starting position [lng, lat]
        zoom: 5 /* starting zoom*/,
        preserveDrawingBuffer: true
    });

    


    function getRandomColor() {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }


    function cargarCombo(data, id, nombre) {
        result = data;
        var select = $("#" + id);
        var options = '';


        if (id == "cmbSedes") {
            select.empty();
            options += "<option value=''></option>";
            options += "<option value='All'>Todos..</option>";
        }
        else {
            select.empty();
            options += "<option value=''></option>";
        }

        if (result.length == 0) {
            options += "<option value=''>No se encontraron " + nombre + "...</option>"
        }

        for (var i = 0; i < result.length; i++) {
            if (id == 'cmbSedes') {
                options += "<option value='" + result[i].Id + "'>" + result[i].Description + "</option>";
            }
            else if (id == 'cmbCanton') {
                options += "<option value='" + result[i].Code + "'>" + result[i].Description + "</option>";
            }
            else if (id == 'cmbParroquia') {
                options += "<option value='" + result[i].Code + "'>" + result[i].Description + "</option>";
            }
        }
        select.append(options);
        //ActiveChose(select, nombre);
    }

    function ActiveChose(select, titulo) {
        setTimeout(function () {
            select.trigger("chosen:updated");
            //select.chosen({
            //    disable_search_threshold: 1,
            //    width: "100%"
            //});
            select.chosen({
                allow_single_deselect: true,
                disable_search_threshold: 1,
                no_results_text: "No se encontró " + titulo + "!",
                width: "100%"
            });
            $(".chosen-select").chosen();
        }, 200);
    }

    function cambioSedes() {
        var sede = $("#cmbSedes").val();
        if (sede != "") {
            if (sede == "All") {
                cargarSedes();
            } else {
                cargarSedes(sede);
            }
            
        }
        else {
            error("Debe selecionar alguno");
        }
    }

    function cambioCanton() {
        var canton = $("#cmbCanton").val();
        if (canton != "") {
            cargarCanton(canton);
        }
        else {

        }
    }

    function cambioParroquia() {
        var parroquia = $("#cmbParroquia").val();
        if (parroquia != "") {
            cargarParroquia(parroquia);
        }
        else {

        }
    }

    function cargarSedes(id_sede) {
        var marker, i;
        //const marker = new mapboxgl.Marker().addTo(map);
        //marker.remove();
        fetch('/DatosMapas/MapaByProvincia?Id=' + vmh.CurrentId() + "&id_sede=" + id_sede).then(async function (response) {
            return await response.json();
        }).then(function (d) {
            for (i = 0; i < d.result.length; i++) {
                marker = new mapboxgl.Marker({
                    color: getRandomColor(),
                    draggable: true,
                }).setLngLat([d.result[i].coordenada_lat, d.result[i].coordenada_lng])
                    .setPopup(new mapboxgl.Popup().setHTML("<p>Sede:" + d.result[i].Description + "<br>Sesiones:" + d.result[i].NumeroSession + "<br>Laboratorio:" + d.result[i].NumeroLaboratorio + "<br>N° Sustentantes:" + d.result[i].NumeroTotalSustentantes + "</p>"))
                    .addTo(map)
                    .togglePopup();;
            }
        })
    }

    //function cargarCanton(canton_id) {
    //    var marker, i;

    //    fetch('/DatosMapas/MapaByCanton?Id=' + vmh.CurrentId() + '&canton_id=' + canton_id).then(async function (response) {
    //        return await response.json();
    //    }).then(function (d) {
    //        for (i = 0; i < d.result.length; i++) {
    //            marker = new mapboxgl.Marker({
    //                color: getRandomColor(),
    //                draggable: true,
    //            }).setLngLat([d.result[i].coordenada_Lat, d.result[i].coordenada_Lng])
    //                .setPopup(new mapboxgl.Popup().setHTML("<p>Insitutucion:" + d.result[i].NombreInstitucion + "<br>Procoso:2021</p>"))
    //                .addTo(map)
    //                .togglePopup();;
    //        }
    //    })
    //}

    //function cargarParroquia(id_parroquia) {
    //    var marker, i;
    //    fetch('/DatosMapas/MapaByParroquia?Id=' + vmh.CurrentId() + '&id_parroquia=' + id_parroquia).then(async function (response) {
    //        return await response.json();
    //    }).then(function (d) {
    //        for (i = 0; i < d.result.length; i++) {
    //            marker = new mapboxgl.Marker({
    //                color: getRandomColor(),
    //                draggable: true,
    //            }).setLngLat([d.result[i].coordenada_Lat, d.result[i].coordenada_Lng])
    //                .setPopup(new mapboxgl.Popup().setHTML("<p>Insitutucion:" + d.result[i].NombreInstitucion + "<br>Proceso:2021</p>"))
    //                .addTo(map)
    //                .togglePopup();;
    //        }
    //    })
    //}

    $('#downloadLink').click(function () {
        var img = map.getCanvas().toDataURL('image/png')
        this.href = img
    })

    function crearimagen() {
        //html2canvas($('#map'),
        //    {
        //        onrendered: function (canvas) {
        //            var a = document.createElement('a');
        //            a.href = canvas.toDataURL("image/png");
        //            a.download = 'image.png';
        //            a.click();
        //        }
        //    });
        downloadCanvas('map', 'imagen.png');
    }


    function downloadCanvas(canvasId, filename) {
        // Obteniendo la etiqueta la cual se desea convertir en imagen
        var domElement = document.getElementById(canvasId);

        // Utilizando la función html2canvas para hacer la conversión
        html2canvas(domElement, {
            onrendered: function (domElementCanvas) {
                // Obteniendo el contexto del canvas ya generado
                var context = domElementCanvas.getContext('2d');

                // Creando enlace para descargar la imagen generada
                var link = document.createElement('a');
                link.href = domElementCanvas.toDataURL("image/png");
                link.download = filename;

                // Chequeando para browsers más viejos
                if (document.createEvent) {
                    var event = document.createEvent('MouseEvents');
                    // Simulando clic para descargar
                    event.initMouseEvent("click", true, true, window, 0,
                        0, 0, 0, 0,
                        false, false, false, false,
                        0, null);
                    link.dispatchEvent(event);
                } else {
                    // Simulando clic para descargar
                    link.click();
                }
            }
        });
    }

    
    // Haciendo la conversión y descarga de la imagen al presionar el botón
    $('#boton-descarga').click(function () {
        
    });
   
} else {
    swal("Para acceder a este modulo necesita la clave API KEY de Mapbox","","error")
}