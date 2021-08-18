var cuentaretencionir;
var cuentaretencioniva;
$(function () {
	cuentaretencionir = new MasterDetail({
		'templateDetalle': $('#tr_template_cuentaretencionir'),
		'tableDetalle': $('#tbody_template_cuentaretencionir'),
		'prefixDetalle': 'template',
		'funcCompDetalle': DetalleCuentaRetencionIRForm.sonIguales,
		'klassDetalle': DetalleCuentaRetencionIRForm
	});

	cuentaretencioniva = new MasterDetail({
		'templateDetalle': $('#tr_template_cuentaretencioniva'),
		'tableDetalle': $('#tbody_template_cuentaretencioniva'),
		'prefixDetalle': 'template',
		'funcCompDetalle': DetalleCuentaRetencionIVAForm.sonIguales,
		'klassDetalle': DetalleCuentaRetencionIVAForm
	});
})
