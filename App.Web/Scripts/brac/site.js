
var jsonTopMenuData;
$(document).ready(function () {
    LoadMenuTopBRAC("../main/MenuData");
});
function LoadMenuTopBRAC(url)
{
    var jsonTopMenuText = $.ajax({
        url: url,
        dataType: "json",
        async: false
    }).responseText;

    jsonTopMenuData = JSON.parse(jsonTopMenuText);

    $("#spnactivities").empty();
    $("#spnactivities").html(jsonTopMenuData.act.length);
    $("#spnmessage").empty();
    $("#spnmessage").html(jsonTopMenuData.msg.length);

    $("#spnactivitiestop").empty();
    $("#spnactivitiestop").html(jsonTopMenuData.act.length);
    $("#spnmessagetop").empty();
    $("#spnmessagetop").html(jsonTopMenuData.msg.length);

    $("#divactivities").empty();
    var item = '<h6 class="dropdown-header d-flex align-items-center"><span class="mr-3">Activities</span> <span id="spnactivities" class="count-badge green">' + jsonTopMenuData.act.length + '</span></h6>';

    $.each(jsonTopMenuData.act, function (index, element) {
        item += element.Name;
    });
    $("#divactivities").html(item);

}

