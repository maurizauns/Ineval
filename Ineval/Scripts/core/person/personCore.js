
function Person(data) {
    this.setAttributes(data);
}

Person.prototype.setAttributes = function (data) {
    this.Id = data['Id'];
    this.ruc = data['Identifier'];
    this.nombre_comercial = data['NombreCompleto'];
    this.razon_social = data['NombreCompleto'];
    this.Descripcion = data['Description'];
    this.nombre = data['NombreCompleto'];
}

Person.getById = function (id, async, callback, oThis) {
    $.ajax({
        type: "GET",
        url: urlprefix + "/Personas/GetPersonas?Id=" + id,
        dataType: "json",
        async: true,
        success: function (data, textStatus) {
            var persona = new Personas(data);
            if (oThis) {
                callback.call(oThis, persona);
            }
            else {
                callback(persona);
            }
        },
        error: function () {
            showMessage('Error', 'Ocurrio un error al intentar cargar la persona, por favor intentelo nuevamente');
        }
    });
}
