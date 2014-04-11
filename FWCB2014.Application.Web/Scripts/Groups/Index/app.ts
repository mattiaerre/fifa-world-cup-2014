$(document).ready(() => {
  $.ajax({
    type: 'GET',
    url: '/Scripts/Groups/Index/Syndication.Groups.json',
    data: {},
    dataType: 'json',
    success: (data) => {
      ko.applyBindings(data); // todo: order by position
    },
    error: (jqXhr, textStatus, errorThrown) => {

    }
  });
});