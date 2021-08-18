function Cie(data) {
    this.setAttributes(data);
}

Cie.prototype.setAttributes = function (data) {
    this.Id = data['Id'];
    this.code = data['Code'];
    this.nombre = data['Code'];
    this.description = data['Description'];
    this.desc = data['Code'];
};

Cie.getById = function (id, async, callback, oThis) {
    $.ajax({
        type: "GET",
        url: "/Cie/GetEntity?id=" + id,
        data: { 'format': 'json' },
        dataType: "json",
        async: true,
        success: function (data, textStatus) {
            var cie = null;
            cie = new Cie(data);
            if (oThis) {
                callback.call(oThis, cie);
            }
            else {
                callback(cie);
            }
        },
        error: function () {
            error('Ocurrio un error al intentar cargar los datos del Cie, por favor intentelo nuevamente');
        }
    });
};

Cie.getObjDesc = function () {
    return this.obj.code;
};

Cie.getCodigoNombre = function () {
    return this.obj.code + " - " + this.obj.description;
};