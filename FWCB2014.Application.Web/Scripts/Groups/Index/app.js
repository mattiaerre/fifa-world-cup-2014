$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Scripts/Groups/Index/Syndication.Groups.json',
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
