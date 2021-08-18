$(document).ready(function () {
    window.signal = $.connection.signalHub;
    window.signal.client.updateHomeCompany = function (userId, agendaId) {
        debugger
        reloadGrid("agendas-grid", 1);
    }

    $.connection.hub.start().done(function () {
        window.actionbegin = function (userId, agendaId) {
            //window.signal.server.updateHome(userId, agendaId, details);
            window.signal.server.updateHomeCompany(userId, agendaId);
        }

        //if (window.signal.connection.state !== 1) {
            window.signal.server.joinGroup()
       // }
    })
});