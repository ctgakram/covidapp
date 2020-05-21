var jsonTopMenuData;
var baseurl = document.location.origin;
$(document).ready(function () {
    LoadMenuTopBRAC("../main/MenuData");
});
function LoadMenuTopBRAC(url) {
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
    $("#divmessages").empty();
    var actitem = '<h6 class="dropdown-header d-flex align-items-center"><span class="mr-3">Activities</span> <span id="spnactivities" class="count-badge green">' + jsonTopMenuData.act.length + '</span></h6>';
    var rptitem = '<h6 class="dropdown-header d-flex align-items-center justify-content-between"><span><span class="mr-3">Circulars & SitReps</span><span id="spnmessage" class="count-badge red">' + jsonTopMenuData.msg.length + '</span></span><a class="view-all-msg" href="' + baseurl + '/Report/Index">View all</a></h6>';//@Url.Action("Index","Report")


    $.each(jsonTopMenuData.act, function (index, element) {
        actitem += element.Name;
    });
    $("#divactivities").html(actitem);

    $.each(jsonTopMenuData.msg, function (index, element) {

        //var baseurl = '@Url.Content("~")';
        rptitem += '<li><div class="msg"><a target="_blank" href="' + baseurl + element.Blob + '" class="brac-link" >' + element.Name + '</a></div></li>';
    });
    $("#divmessages").html('<ul>' + rptitem + '</ul>');
}

$(document).ready(function () {
    $(".toggle-trigger").on("click", function () {
        $(".dt-carret").toggleClass("active");
        $(".tnav").slideToggle();
    });
    $(".dropdown").click(function () {
       
        if ($(this).find(".dropdown-menu").hasClass("show") === true) {
            $(this).find(".dropdown-menu").removeClass("show");
           
        } else {
            $(this).find(".dropdown-menu").addClass("show");
           
        }
    });
});