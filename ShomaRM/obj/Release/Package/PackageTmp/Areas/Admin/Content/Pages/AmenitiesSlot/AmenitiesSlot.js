$(document).ready(function () {
    getAmenityList();

    $("#ddlAmenities").on("change", function () {
        var selectedOption = $(this).val();

        getDurationSlot(selectedOption);
    });

    $("#ddlDuration").on("change", function () {
        var optionDep = $(this).find(":selected").attr("data-dep");
        var optionRes = $(this).find(":selected").attr("data-res");
        $("#txtDepositFee").val(optionDep);
        $("#txtReservationFee").val(optionRes);

    });
});

var SaveUpdateSlot = function () {
    var msg = "";
    var slotID = $("#hdnEventId").val();
    var amenityId = $("#ddlAmenities").val();
    var duration = $("#ddlDuration").find(":selected").text();
    var durationID = $("#ddlDuration").val();
    var depositFee = $("#txtDepositFee").val();
    var reservationFee = $("#txtReservationFee").val();
    console.log(amenityId + " " + duration + " " + depositFee + " " + reservationFee);
    if (amenityId == "0") {
        msg += "Please Select The Amenity </br>";
    }
    if (duration == "0") {
        msg += "Please Fill The Duration </br>";
    }
    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        ID: slotID,
        AmenityID : amenityId,
        Duration: duration,
        DurationID: durationID,
        Deposit : depositFee,
        Fees : reservationFee
    };
    $.ajax({
        url: '/Admin/AmenitiesSlot/SaveUpdateSlot',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.msg,
                type: 'blue'
            });
            setInterval(function () {
                window.location.replace("/Admin/AmenitiesSlot");
            }, 3000);
        }
    });
};
var goSlotList = function () {
    window.location.replace("/Admin/AmenitiesSlot/");
};

var SlotList = function () {
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

var getDurationSlot = function (selectedValue) {
    $("#ddlDuration").empty();
    var option = "<option value=0>Select Duration Slot</option>";
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


