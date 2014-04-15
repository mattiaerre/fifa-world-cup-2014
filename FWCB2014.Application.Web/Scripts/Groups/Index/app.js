$(document).ready(function () {
    var url = '/Scripts/Groups/Index/Syndication.Groups.json';

    //var url = 'http://fwcb2014-syndication.azurewebsites.net/api/groups';
    $.ajax({
        type: 'GET',
        url: url,
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
