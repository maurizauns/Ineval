if (vmh.PasswordMapBox()) {
    var vmFormViewMapas = {};
    $(document).ready(function () {

        function KnockoutFormMapas(datosMapas) {
            vmFormViewMapas = ko.mapping.fromJS(datosMapas);

            cargarCombo(datosMapas.Provincia, "cmbProvincia", "cmbProvincia");
            cargarCombo(datosMapas.Canton, "cmbCanton", "cmbCanton");
            cargarCombo(datosMapas.Parroquia, "cmbParroquia", "cmbParroquia");

            ko.cleanNode($("#Content")[0]);
            ko.applyBindings(vmFormViewMapas, $("#Content")[0]);
        }

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "/DatosMapas/GetFormulario?id=" + vmh.CurrentId(),
            success: KnockoutFormMapas
        });

    })



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


        if (id == "cmb") {
            select.empty();
            options += "<option value=''>Todos..</option>";
        }
        else {
            select.empty();
            options += "<option value=''></option>";
        }

        if (result.length == 0) {
            options += "<option value=''>No se encontraron " + nombre + "...</option>"
        }

        for (var i = 0; i < result.length; i++) {
            if (id == 'cmbProvincia') {
                options += "<option value='" + result[i].Code + "'>" + result[i].Description + "</option>";
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

    function cambioProvincia() {
        var provinca = $("#cmbProvincia").val();
        if (provinca != "") {
            cargarProvincia(provinca);
        }
        else {

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

    function cargarProvincia(id_provincia) {
        var marker, i;

        fetch('/DatosMapas/MapaByProvincia?Id=' + vmh.CurrentId() + "&id_provincia=" + id_provincia).then(async function (response) {
            return await response.json();
        }).then(function (d) {
            for (i = 0; i < d.result.length; i++) {
                marker = new mapboxgl.Marker({
                    color: getRandomColor(),
                    draggable: true,
                }).setLngLat([d.result[i].coordenada_Lat, d.result[i].coordenada_Lng])
                    .setPopup(new mapboxgl.Popup().setHTML("<p>Insitutucion:" + d.result[i].NombreInstitucion + "<br>Procoso:2021</p>"))
                    .addTo(map)
                    .togglePopup();;
            }
        })
    }

    function cargarCanton(canton_id) {
        var marker, i;

        fetch('/DatosMapas/MapaByCanton?Id=' + vmh.CurrentId() + '&canton_id=' + canton_id).then(async function (response) {
            return await response.json();
        }).then(function (d) {
            for (i = 0; i < d.result.length; i++) {
                marker = new mapboxgl.Marker({
                    color: getRandomColor(),
                    draggable: true,
                }).setLngLat([d.result[i].coordenada_Lat, d.result[i].coordenada_Lng])
                    .setPopup(new mapboxgl.Popup().setHTML("<p>Insitutucion:" + d.result[i].NombreInstitucion + "<br>Procoso:2021</p>"))
                    .addTo(map)
                    .togglePopup();;
            }
        })
    }

    function cargarParroquia(id_parroquia) {
        var marker, i;
        fetch('/DatosMapas/MapaByParroquia?Id=' + vmh.CurrentId() + '&id_parroquia=' + id_parroquia).then(async function (response) {
            return await response.json();
        }).then(function (d) {
            for (i = 0; i < d.result.length; i++) {
                marker = new mapboxgl.Marker({
                    color: getRandomColor(),
                    draggable: true,
                }).setLngLat([d.result[i].coordenada_Lat, d.result[i].coordenada_Lng])
                    .setPopup(new mapboxgl.Popup().setHTML("<p>Insitutucion:" + d.result[i].NombreInstitucion + "<br>Proceso:2021</p>"))
                    .addTo(map)
                    .togglePopup();;
            }
        })
    }

    $('#downloadLink').click(function () {
        var img = map.getCanvas().toDataURL('image/png')
        this.href = img
    })
} else {
    swal("Para acceder a este modulo necesita la clave API KEY de Mapbox","","error")
}