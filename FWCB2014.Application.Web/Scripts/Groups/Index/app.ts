$(document).ready(() => {
  $.ajax({
    type: 'GET',
    url: 'http://fwcb2014-syndication.azurewebsites.net/api/groups',
    data: {},
    dataType: 'json',
    success: (data) => {
      ko.applyBindings(data); // todo: order by position
    },
    error: (jqXhr, textStatus, errorThrown) => {

    }
  });
});