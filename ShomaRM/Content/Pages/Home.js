$(document).ready(function () {
   
    getPropertyList();
});

var getPropertyList = function () {

    var model = { City: 0, SearchText: "" };

    $.ajax({
        url: "/Property/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
                $("#list-type").empty();
               // console.log(response.model)
                $.each(response.model, function (elementType, value) {
                    var html = "<div class='col-sm-6 col-md-3 p0'><div class='box-two proerty-item'>";
                    html += "<div class='item-thumb'><a href = '#' onclick='getPropertyDetails(" + value.PID + ")'> <img src='../content/assets/img/demo/" + value.Picture + "'></a></div> ";
                    html += " <div class='item-entry overflow'><h5><a href = '#' onclick='getPropertyDetails(" + value.PID + ")'> " + value.Title + "</a></h5>  <div class='dot-hr'></div>";
                    html += " <span class='pull-left'><b> Units :</b> " + value.NoOfUnits + "</span>";
                    html += "<span class='proerty-price pull-right'><b> Floors :</b>" + value.NoOfFloors + " </span>";
                    html += "<p style='display: none;'>" + value.Description + "</p> </div> </div> </div>";
                    $("#list-type").append(html);
                });
            }
        }
    });
}
var getPropertyUnitList = function () {
    $("#divLoader").show();
    if (!$("#ddlBedroom").val()) {
        $.alert({
            title: 'Alert!',
            content: "Please Select Beds",
            type: 'red'
        });
        $("#divLoader").hide();
    }
    var bed = $("#ddlBedroom").val();
    var midate = $("#txtMoveInDate").val();
    var rent = "10000";
    var model = { Bedroom: bed, MoveInDate: midate, MaxRent: rent };

    $.ajax({
        url: "/Property/SetSearchFromHome/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            window.location.href = "/ApplyNow/Index/0";
            $("#divLoader").hide();
        }
    });

}
var goToallProp = function () {
    window.location = "/Property/";
}