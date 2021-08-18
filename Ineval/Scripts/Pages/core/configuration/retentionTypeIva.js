function TipoRetencionIVA(data) {
	this.setAttributes(data);
}

TipoRetencionIVA.prototype.setAttributes = function (data) {

	this.Id = data['Id'];
	this.codigo = data['Code'];
	this.descripcion = data['Description'];
	this.porcentaje = data['Percentage'];
}

TipoRetencionIVA.getObjDesc = function () {
	return this.obj.codigo + " - " + this.obj.descripcion;
};

TipoRetencionIVA.getById = function (id, async, callback, oThis) {
	if (async == undefined) async = true;
	var tiporet = null;

	var a = $.ajax({
		type: "GET",
		url: urlprefix + "/Retention/GetEntity?id=" + id,
		dataType: "json",
		async: async,
		success: function (data, textStatus) {
			tiporet = new TipoRetencionIVA(data);
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
			showMessage('Error', 'OcurriÃ³ un error al intentar cargar el tipo de retenciÃ³n iva, por favor intÃ©ntelo nuevamente');
		}
	});
	if (typeof promises_js !== 'undefined') {
		promises_js.push(a);
	}
	if (!async) {
		return tiporet;
	}

}

function DetalleCuentaRetencionIVAForm(tr, firstLoad) {
	this.tr = tr; // almacena la fila que representa a un detalle
	this.cuentaretfield = new ObjectField($(this.tr).find('input.object-hidden[id$="tipo_retiva_id"]'));
	this.cuentaretclifield = new ObjectField($(this.tr).find('input.object-hidden[id$="cuenta_cli_id"]'));
	this.cuentaretprofield = new ObjectField($(this.tr).find('input.object-hidden[id$="cuenta_pro_id"]'));
};