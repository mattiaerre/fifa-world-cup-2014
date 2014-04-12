$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: 'http://fwcb2014-syndication.azurewebsites.net/api/groups',
        data: {},
        dataType: 'json',
        success: function (data) {
            ko.applyBindings(data); // todo: order by position
        },
        error: function (jqXhr, textStatus, errorThrown) {
        }
    });
});
//# sourceMappingURL=app.js.map
