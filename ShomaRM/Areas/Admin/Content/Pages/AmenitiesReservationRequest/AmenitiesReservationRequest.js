$(document).ready(function () {
    getAmenityList();

    $("#ddlAmenities").on("change", function () {
        var selectedOption = $(this).val();

        getDurationReservationRequest(selectedOption);
    });

    $("#ddlDuration").on("change", function () {
        var optionDep = $(this).find(":selected").attr("data-dep");
        var optionRes = $(this).find(":selected").attr("data-res");
        $("#txtDepositFee").val(optionDep);
        $("#txtReservationFee").val(optionRes);

    });
    getARRdata();
});

var saveupdateStatus = function () {
    var msg = "";
    var ReservationRequestID = $("#hdnARId").val();
    var ststus = $("#ddlStatus").val();
 
    var model = {
        AmenityName: $("#txtAmenityName").val(),
        ARID: ReservationRequestID,
        Status: ststus
    };
    $.ajax({
        url: '/Admin/Amenities/SaveUpdateAmenityRequestStatus',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.model,
                type: 'blue'
            });
            setInterval(function () {
                window.location.replace("/Admin/Amenities/AmenitiesReservationRequest");
            }, 3000);
        }
    });
};
var goReservationRequestList = function () {
    window.location.replace("/Admin/Amenities/AmenitiesReservationRequest/");
};

var ReservationRequestList = function () {
    $.ajax({
        url: '/Event/EventDataList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $("#ddlEvent").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = '<option value=' + elementValue.EventID + '>' + elementValue.EventName + '</option>';
                $("#ddlEvent").append(option);
            });
        }
    });
};

var getAmenityList = function () {
    $.ajax({
        url: "/Tenant/MyAccount/GetAmenityList",
        type: "post",
        contentType: "application/json utf-8",

        success: function (response) {

            $("#ddlAmenities").empty();
            var option = "<option value=0>Select Amenity</option>";
            $.each(response.model, function (elementType, elementValue) {
                option += "<option value=" + elementValue.ID + ">" + elementValue.Amenity + "</option>";

            });
            $("#ddlAmenities").append(option);
        }
    });
};

var getDurationReservationRequest = function (selectedValue) {
    $("#ddlDuration").empty();
    var option = "<option value=0>Select Duration ReservationRequest</option>";
    if (selectedValue == 1) {
        option += "<option value=1 data-res='100' data-dep='100'> 2 hours </option>";
        option += "<option value=2 data-res='200' data-dep='200'> 4 hours </option>";
    }
    else if (selectedValue == 3) {
        option += "<option value='1' data-res='250' data-dep='500'> 3 hours </option>";
        option += "<option value=2 data-res='500' data-dep='1000'> 5 hours </option>";
    }
    else if (selectedValue == 11) {
        option += "<option value=1 data-res='20' data-dep='50'> 2 hours </option>";
        option += "<option value=2 data-res='40' data-dep='100'> 4 hours </option>";
    }
    else if (selectedValue == 12) {
        option += "<option value=1 data-res='75' data-dep='250'> 2 hours </option>";
        option += "<option value=2 data-res='150' data-dep='450'> 4 hours </option>";
    }
    else if (selectedValue == 13) {
        option += "<option value=1 data-res='0' data-dep='0'> 2 hours </option>";
    }

    $("#ddlDuration").append(option);
};

var getARRdata = function () { 

    var params = { Id: $("#hdnARId").val()};
    $.ajax({
        url: "/Amenities/GetRRInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            //clearRRdata();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndRRID").val(response.ARID);
                $("#txtTenantName").val(response.TenantName);
                $("#txtAmenityName").val(response.AmenityName);
                $("#txtDesiredDate").val(response.DesiredDateString);
                $("#txtDesiredTime").val(response.DesiredTime);
                $("#txtDesiredDuration").val(response.Duration);
                $("#txtDepositFee").val(response.DepositFee);
                $("#txtReservationFee").val(response.ReservationFee);
                setTimeout(function () { $("#ddlDuration").find("option[value='" + response.Status + "']").attr('selected', 'selected'); }, 1600);
                if ($("#hndAmenityID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};

