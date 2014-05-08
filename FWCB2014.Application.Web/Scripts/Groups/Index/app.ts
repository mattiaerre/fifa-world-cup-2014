var groupId: string = groupId || '';

$(document).ready(() => {
    //var url = '/Scripts/Groups/Index/Groups.json';

    var url = 'http://fwcb2014-syndication.azurewebsites.net/api/groups';
    url = url + '/' + groupId;

    toastr.info('url: ' + url);

    $.ajax({
        type: 'GET',
        url: url,
        data: {},
        dataType: 'json',
        success: (data) => {
            ko.applyBindings(data);
        },
        error: (jqXhr, textStatus, errorThrown) => {
            //var options = { positionClass: 'toast-bottom-full-width'};
            //toastr.options = options;

            toastr.error('jqXhr: ' + JSON.stringify(jqXhr));
            toastr.error('textStatus: ' + textStatus);
            toastr.error('errorThrown: ' + errorThrown);
        }
    });
});