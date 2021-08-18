function MasterDetail(opt) {
	this.setAttributes(opt);
}

MasterDetail.prototype.setAttributes = function(opt) {
	this.templateDetalle = opt['templateDetalle'];
	this.tableDetalle = opt['tableDetalle'];
	this.prefixDetalle = opt['prefixDetalle'];
	this.funcCompDetalle = opt['funcCompDetalle'];
	this.klassDetalle = opt['klassDetalle'];
	this.idetalles = 0;
	this.detalles = [];
	var master = this;
	$(this.tableDetalle).children('tr').not(this.templateDetalle).each(function (i){
		master.agregarDetalle(this);
	});
}

MasterDetail.prototype.getDetalle = function(tr) {
	var i;
	for (i = 0; i < this.detalles.length; i = i + 1) {
		if (this.funcCompDetalle(this.detalles[i].tr, tr)) {
			return this.detalles[i];
		}
	}
	return null;
}

MasterDetail.prototype.agregarDetalle = function (tr) {
	var firstLoad = true;
	if (tr == undefined) {
		tr = cloneTemplate(this.templateDetalle, this.tableDetalle, this.prefixDetalle, this.idetalles + 1);
		firstLoad = false;
	}
	this.detalles[this.detalles.length] = new this.klassDetalle(tr, firstLoad);
	this.idetalles = this.idetalles + 1;
	
	//$(tr).find('a[rel="tooltip"]').tooltip();
	$('[data-toggle="tooltip"]').tooltip();
}

MasterDetail.prototype.getLastRow = function() {
	return this.detalles[this.detalles.length - 1].tr;
}

MasterDetail.prototype.puedeEliminar = function (el) {
	return true;
}

MasterDetail.prototype.eliminarDetalle = function(el, func) {
	if (this.puedeEliminar(el)) {
		var master = this;
		var str='detalle';
		var str2='valor seleccionado';
		if($(el).attr('id')){
			var id = $(el).attr('id');
			if(id.indexOf('variante') >= 0){
				str = 'valor';
			}
		}
		else{
			str2 = str;
		}
		showConfirm('Eliminar '+str, '¿Está seguro que desea eliminar el '+str2+'?', function() {
			var detalle = master.getDetalle($(el).closest('tr')); 
			master.detalles.splice(master.detalles.indexOf(detalle), 1);
			$(detalle.tr).remove();
			if (func) {
				func();
			}
		});				
	}
}

MasterDetail.prototype.eliminarDetalles = function(el) {
	if (this.puedeEliminar(el)) {
		var master = this;
		var detalle = master.getDetalle($(el).closest('tr')); 
		master.detalles.splice(master.detalles.indexOf(detalle), 1);
		$(detalle.tr).remove();					
	}
} 
