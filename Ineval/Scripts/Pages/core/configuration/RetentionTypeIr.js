function TipoRetencionIR(data) {
	this.setAttributes(data);
}

TipoRetencionIR.prototype.setAttributes = function (data) {
	this.Id = data['Id'];
	this.codigo = data['Code'];
	this.Descripcion = data['Description'];
	this.porcentaje = data['Percentage'];
}

TipoRetencionIR.getObjDesc = function () {
	return this.obj.codigo + " - " + this.obj.Descripcion;
};

TipoRetencionIR.getById = function (id, async, callback, oThis) {
	if (async == undefined) async = true;
	var tiporet = null;
	var fecha_emision_retencion = $('#id_fecha_emision_retencion').val();
	if (typeof fecha_emision_retencion !== 'undefined' && fecha_emision_retencion != "" && fecha_emision_retencion !== null) {
		fecha_cadena = ('' + fecha_emision_retencion).split('/');
		fecha = fecha_cadena[2] + '-' + fecha_cadena[1] + '-' + fecha_cadena[0];
		url_tiporetir = "/Retention/GetEntity?id=" + id + "/?fecha=" + fecha;
	} else {
		url_tiporetir = "/Retention/GetEntity?id=" + id;
	}
	var a = $.ajax({
		type: "GET",
		url: urlprefix + url_tiporetir,
		dataType: "json",
		async: async,
		success: function (data, textStatus) {
			tiporet = new TipoRetencionIR(data);
			if (async) {
				if (oThis) {
					callback.call(oThis, tiporet);
				}
				else {
					callback(tiporet);
				}
			}
		},
		error: function () {
			erro('Error', 'Ocurrio un error al intentar cargar el tipo de retención, por favor inténtelo nuevamente');
			//showMessage('Error', 'OcurriÃ³ un error al intentar cargar el tipo de retenciÃ³n, por favor intÃ©ntelo nuevamente');
		}
	});
	if (typeof promises_js !== 'undefined') {
		promises_js.push(a);
	}
	if (!async) {
		return tiporet;
	}
};

DetalleCuentaRetencionIRForm.sonIguales = function (detalle1, detalle2) {
	var input1 = $(detalle1).find('input[type=hidden]').get(0);
	var input2 = $(detalle2).find('input[type=hidden]').get(0);
	return (input1.id == input2.id);
};

function DetalleCuentaRetencionIRForm(tr, firstLoad) {
	this.tr = tr; // almacena la fila que representa a un detalle
	this.cuentaretfield = new ObjectField($(this.tr).find('input.object-hidden[id$="tipo_retir_id"]'));
	this.cuentaretclifield = new ObjectField($(this.tr).find('input.object-hidden[id$="cuenta_cli_id"]'));
	this.cuentaretprofield = new ObjectField($(this.tr).find('input.object-hidden[id$="cuenta_pro_id"]'));
};