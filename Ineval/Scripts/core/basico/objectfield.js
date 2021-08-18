ObjectField.currentField = null;  // variable global con el field al que el iframed debe devolver el valor seleccionado

function ObjectField(opt, add) {
	/* Contructor, recibe un diccionario con las opciones del control 
	Opciones:
	el - Hidden input (representación html del ObjectField)
	size - Tamaño del input text de descripción del objeto, por defecto 30
	getObjectById - Función a invocar cuando se quiera buscar un objeto por ID
	getObjectByDesc - Función a invocar cuando se quiera buscar un objeto por descripción
	idField - Nombre del atributo que contiene el id del objeto relacionado, por defecto id
	descField - Nombre del atributo que contiene la descripción del objeto relacionado, por defecto nombre
	onSetObj - Función a invocar cuando el valor del field cambie, por defecto null
	validateDescObj - Función utilizada para verificar si lo ingresado es un valor válido de búsqueda, por defecto null
	*/
    if (!opt['el']) {
        opt = this.createOpt(opt); // se leen las opciones de los atributos del input
    }
    if (add) {
        opt['showAdd'] = true;
    }
    this.setAttributes(opt);
    if (this.el.val()) {
        this.getObjectById(this.el.val(), true, this.setObj, this);
    }
}


ObjectField.prototype.createOpt = function (el) {

    /* Lee la config del field del hidden input */
    var opt = {};
    opt['el'] = el;
    opt['getObjectById'] = el.attr('getObjectById') ? eval(el.attr('getObjectById')) : null;
    opt['getObjectByDesc'] = el.attr('getObjectByDesc') ? eval(el.attr('getObjectByDesc')) : null;
    opt['getObjDesc'] = el.attr('getObjDesc') ? eval(el.attr('getObjDesc')) : this.getObjDesc;
    opt['dlgTitle'] = el.attr('dlgTitle');
    opt['size'] = el.attr('size');
    opt['dlgUrl'] = el.attr('dlgUrl');
    opt['idField'] = el.attr('idField');
    opt['descField'] = el.attr('descField');
    opt['onSetObj'] = el.attr('onSetObj') ? eval(el.attr('onSetObj')) : null;
    opt['validateDescObj'] = el.attr('validateDescObj') ? eval(el.attr('validateDescObj')) : null;
    opt['showAdd'] = el.attr('showAdd') ? eval(el.attr('showAdd')) : false;
    opt['showQuery'] = el.attr('showQuery') ? eval(el.attr('showQuery')) : false;
    opt['getObjectQueryUrl'] = el.attr('getObjectQueryUrl') ? eval(el.attr('getObjectQueryUrl')) : null;
    opt['onSetObj'] = el.attr('onSetObj') ? eval(el.attr('onSetObj')) : null;
    opt['urlQuery'] = el.attr('urlQuery');
    opt['dlgUrlAdd'] = el.attr('dlgUrlAdd') ? el.attr('dlgUrlAdd') : null;
    opt['dlgHeight'] = el.attr('dlgHeight');
    opt['dlgWidth'] = el.attr('dlgWidth');
    opt['autocompletar'] = el.attr('autocompletar') ? el.attr('autocompletar') : false;
    return opt;
}

ObjectField.prototype.setAttributes = function (opt) {
    // seteo de config del objeto
    this.obj = null;
    this.el = opt['el'];
    this.onButtonClick = null;
    if (opt['el'].attr('data-data')) {
        this.make_ajax = 0;
        this.data = opt['el'].attr('data-data');
    } else {
        this.make_ajax = 1;
    }
    this.getObjectById = opt['getObjectById'];
    this.getObjectByDesc = opt['getObjectByDesc'];
    this.getObjDesc = opt['getObjDesc'];
    this.dlgTitle = opt['dlgTitle'];
    this.dlgUrl = opt['dlgUrl'];
    this.dlgUrlAdd = opt['dlgUrlAdd'];
    this.idField = opt['idField'] ? opt['idField'] : 'Id';
    this.descField = opt['descField'] ? opt['descField'] : 'Descripcion';
    this.onSetObj = opt['onSetObj'] ? opt['onSetObj'] : null;
    this.validateDescObj = opt['validateDescObj'] ? opt['validateDescObj'] : null;
    this.dlgHeight = opt['dlgHeight'] ? opt['dlgHeight'] : 420;
    this.dlgWidth = opt['dlgWidth'] ? opt['dlgWidth'] : 700;
    // seteo los elementos html asociados al control
    this.showAdd = opt['showAdd'];
    this.showQuery = opt['showQuery'];
    this.autocompletar = opt['autocompletar'];
    this.table = this.makeHtml();
    this.elButton = $(this.table).find('span.object-button');
    this.elDescription = $(this.table).find('input.object-description');
    this.elDescription.attr('size', opt['size'] ? opt['size'] : 30)
    this.elqueryButton = $(this.table).find('span.query-button');
    this.elqueryLi = $(this.table).find('li.li-query');
    this.urlQuery = opt['urlQuery'];
    this.getObjectQueryUrl = opt['getObjectQueryUrl'];


    if (this.showAdd) {
        this.elAddButton = $(this.table).find('span.object-add-button');
        $(this.elAddButton).bind('click', { 'field': this }, this.handleAddButtonClick);
    }
    else {
        this.elAddButton = null;
    }


    if (this.showQuery) {
        this.elEditButton = $(this.table).find('span.object-edit-button');
        $(this.elEditButton).bind('click', { 'field': this }, this.handleQueryButtonClick);
    } else {
        this.elEditButton = null;
    }

    // manejo los eventos de los elementos html
    $(this.elButton).bind('click', { 'field': this }, this.handleButtonClick);
    $(this.elqueryButton).bind('click', { 'field': this }, this.handleQueryButtonClick);
    $(this.elDescription).bind('keyup', { 'field': this }, this.handleKeyUp);
    $(this.elDescription).bind('focus', { 'field': this }, this.handleFocus);
    var elemento = this;
    if (this.autocompletar == "True") {
        $(this.elDescription).attr("data_id", $(this.el).attr("id"));
        $(this.elDescription).autocomplete({
            source: elemento.handleUrlAutocompletar(),
            minLength: 2,
            select: function (event, ui) {
                $("#" + $(this).attr("data_id")).val(ui.item.Id);
                var extra = $('<div></div>').append(ui.item.NombreAutocomplete).find(".auto_extra").text();
                //elemento.selectObj(ui.item.id,extra);
                //autocompletarSeleccionado(ui.item.id,elemento);	
                elemento.selectObj(ui.item.Id, extra);
                autocompletarSeleccionado(ui.item.Id, elemento);
            },
            response: function (event, ui) {
                if (ui.content.length === 0) {
                    ui.content.push({
                        label: '<span class="text-italic">No hay resultados para mostrar</span>',
                        value: 'no-results'
                    });
                }
            }
        }).data("ui-autocomplete")._renderItem = function (ul, item) { //popup
            if (item.value == 'no-results') {
                return $('<li class="ui-state-disabled">' + item.label + '</li>').appendTo(ul);
            } else {
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.NombreAutocomplete + "</a>")
                    .appendTo(ul);
            }
        };
    }
}

function autocompletarSeleccionado(id, elemento) {
}

function objectGetParametroAdicional(url, elemento) {
    if (getURLParameter(url, 'tipoPersona') != null) return "";
    var urlConsulta = $("#" + $(elemento).attr("data_id")).attr("dlgUrl");
    var parametro = getURLParameter(url, 'tipoPersona');
    if (parametro == null) return "";
    return "?tipoPersona=" + parametro;
}

function getURLParameter(url, name) {
    var results = new RegExp('[\\?&amp;]' + name + '=([^&amp;#]*)').exec(url);
    if (results == null) return null;
    return results[1] || 0;
}

ObjectField.prototype.getObjId = function () {
    /* Función invocada para obtener el ID del objeto relacionado al Field */
    if (this.obj) {
        return this.obj[this.idField];
    }
    return null;
}

ObjectField.prototype.getObjDesc = function () {
    /* Función invocada para obtener la descripción del objeto relacionado al Field */
    if (this.obj) {
        return this.obj[this.descField];
    }
    return null;
}

ObjectField.prototype.getDesc = function () {
	/* Obtiene la descripción ingresada en el input text del Field, su valor puede ser distinto 
	a lo devuelto por getObjDesc */
    return this.elDescription.val();
}

ObjectField.prototype.makeHtml = function () {
    /* Genera el html del control */
    //var html = "<table style='width:100%;'><tr><td>";
    var html = '<div class="input-group">';
    html = html + "<input class='object-description form-control' type='text' value='' placeholder='&middot;&middot;&middot;' />";
    //// html = html + "</td><td>";
    // // html = html + '<ul class="icons ui-widget ui-helper-clearfix">';
    // html = html + '<div class="btn-group">';
    // // html = html + '<li class="ui-state-default ui-corner-all" title="Buscar">';
    html = html + '<span class="input-group-addon object-button btn btn-primary glow" style="cursor:pointer;" type="button"><i class="bx bx-search"></i></span>';
    if (this.showAdd) {
        // html = html + '<li class="ui-state-default ui-corner-all" title="Agregar">';
        html = html + '<span class="input-group-addon object-add-button btn btn-primary glow btn-default"><span class="bx bx-plus"></span></span>';
        // html = html + "</li>";
    }
    if (this.showQuery) {
        html = html + '<span class="input-group-addon btn btn-primary glow object-edit-button btn-default"><span class="bx bx-new-window"></span></span>';
    }
    html = html + '</div>';
    //html = html + '<a class="object-button" style="" href="javascript:;"><span class="glyphicon glyphicon-search"></span></a>';
    // // html = html + "</li>";
    // if (this.showAdd){
    // // html = html + '<li class="ui-state-default ui-corner-all" title="Agregar">';
    // html = html + '<a class="object-add-button" href="javascript:;"><span class="glyphicon glyphicon-plus"></span></a>&nbsp;';
    // // html = html + "</li>";
    // }
    // // html = html + '<li class="li-query ui-state-default ui-corner-all" title="Consultar" style="margin-left:3px; display:none;">';
    // html = html + '<a class="query-button" href="javascript:;"><span class="glyphicon glyphicon-folder-open"></span></a>&nbsp;';
    // // html = html + '</li>';
    //html = html + "</td></tr></table>";
    return $(html).insertBefore(this.el).get(0); // devuelvo la tabla generada
}

ObjectField.prototype.showDialog = function (url) {
    /* Muestra el dialogo	*/
    if ($('#dlgSelectObj').size() == 0) { // si no existe el dialogo lo genero
        
        var html = '<div id="" class="modal fade" style="display: none; overflow:hidden;">';
        html += '<div class="modal-dialog-centered modal-dialog-scrollable"><div class="modal-content">'
        html = html + '<iframe id="frmObj" src="" width="100%" height="100%" frameborder="0">';
        html = html + '</iframe></div></div></div>';
        $('body').append(html);
    }

    var frame = document.getElementById("frmObj"),
        frameDoc = frame.contentDocument || frame.contentWindow.document;
    frameDoc.removeChild(frameDoc.documentElement);

    ObjectField.currentField = this; // para que el iframe devuelva el valor a este field
    $('#frmObj').attr('src', url);
    var box = bootbox.dialog({
        className: 'custom_contifico_bootbox',
        message: frame,
        title: this.dlgTitle
    });
}

ObjectField.prototype.selectObj = function (id, extraSelectParams) {
    /* Setea el objeto seleccionado en el diálogo */
    ObjectField.currentField = null;
    this.extraSelectParams = extraSelectParams;
    this.getObjectById(id, true, this.setObj, this);
    bootbox.hideAll();
}

ObjectField.prototype.handleButtonClick = function (e) {
    /* Maneja el evento de click del botón de búsqueda */
    var field = e.data['field'];
    var desc = field.getDesc();
    if (field.onButtonClick && !field.onButtonClick()) {
        return;
    }
    field.showDialog(field.dlgUrl);
}

ObjectField.prototype.handleAddButtonClick = function (e) {
    var field = e.data['field'];
    field.showDialog(field.dlgUrlAdd);
}

ObjectField.prototype.handleQueryButtonClick = function (e) {
    if (e.data.field.getObjectQueryUrl) {
        window.open(e.data.field.getObjectQueryUrl(e.data.field.getObjId()), '_blank');
    }
}

ObjectField.prototype.handleUrlAutocompletar = function () {
    var field = this;
    if (field['dlgUrl'].charAt(field['dlgUrl'].length - 1) == '/') parametro = "?atc=1";
    else parametro = "&atc=1";
    return field['dlgUrl'] + parametro;
}


ObjectField.prototype.handleFocus = function (e) {
    var field = e.data['field'];
    if (field["autocompletar"] == null || field['dlgUrl'] == null || field["autocompletar"] == false || field["autocompletar"] == "False") return;

    var parametro = null;
    if (field.onButtonClick && !field.onButtonClick()) return;
    if (field['dlgUrl'].charAt(field['dlgUrl'].length - 1) == '/') parametro = "?Estado=1";
    else parametro = "&Estado=1";
    $(e.target).autocomplete("option", "source", field['dlgUrl'] + parametro);

}

ObjectField.prototype.handleKeyUp = function (e) {
    /* Maneja el evento keyup del input text de la descripción */
    var field = e.data['field'];
    if (field.obj) {
        if (field.getObjDesc() != e.target.value) {
            field.setObj(null);
        }
    }
    if (e.which == 113) {
        field.elButton.trigger('click');
    }
}

ObjectField.prototype.setObj = function (obj) {
	/* Setea los datos del objeto en los elementos html, dispara un evento de 
	cambio de valor del control */
    
    this.obj = obj;
    if (obj) {
        $(this.el).val(this.getObjId());
        $(this.elDescription).val(this.getObjDesc());
        if (this.showQuery) {
            this.elqueryLi.show();
        }
    } else {
        $(this.el).val('');
        this.elqueryLi.hide();
    }
    var event = jQuery.Event("selected");
    $(this.elDescription).trigger(event);
    if (this.onSetObj) {
        this.onSetObj(obj, this);
    }
}

ObjectField.prototype.reset = function () {
    this.setObj(null);
    $(this.elDescription).val('');
}

ObjectField.prototype.hide = function () {
    $(this.table).css('display', 'none');
}

ObjectField.prototype.show = function () {
    $(this.table).css('display', '');
}

ObjectField.prototype.quitarTooltipOnSetObj = function () {
    if (this.elDescription.hasClass('highlight-red')) {
        var description_element = this.elDescription;
        var container_group = description_element.closest('.input-group');

        description_element.removeClass('highlight-red')
        description_element.removeAttr('name');
        container_group.find('.objfield-highlight-tooltip').remove();
    }
}