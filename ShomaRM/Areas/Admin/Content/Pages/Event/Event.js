$(document).ready(function () {
    getPropertyList();
});
var SaveUpdateEvent = function () {
    var msg = "";
    var eventId = $("#hdnEventId").val();
    var eventName = $("#txtEventName").val();
    var propertyId = $("#ddlProperty").val();
    var eventDate = $("#txtEventDate").val();
    var description = $("#txtDescription").val();
    var eventType = $("#ddlEventType").val();
    var eventTime = $("#txtEventTime").val();
    var eventFees = $("#txtEventFees").val();

    if (eventName == "") {
        msg += "Please Fill The Event Name </br>";
    }
    if (propertyId == "0") {
        msg += "Please Select The Property </br>";
    }
    if (eventDate == "") {
        msg += "Please Fill The Event Date </br>";
    }
    if (eventTime == "") {
        msg += "Please Fill The Event Time </br>";
    }
    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        });
        return;
    }

    $formData = new FormData();

    $formData.append('EventID', eventId);
    $formData.append('EventName', eventName);
    $formData.append('PropertyID', propertyId);
    $formData.append('EventDate', eventDate);
    $formData.append('Description', description);
    $formData.append('Type', eventType);
    $formData.append('EventTime', eventTime);
    $formData.append('Fees', eventFees);
    var photo = document.getElementById('wizard-picture');
    if (photo.files.length > 0) {
        for (var i = 0; i < photo.files.length; i++) {
            $formData.append('file-' + i, photo.files[i]);
            console.log(photo.files[i]);
        }
    }
    
    $.ajax({
        url: '/Admin/Event/SaveUpdateEvent',
        type: 'post',
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.models,
                type: 'blue'
            });
            setInterval(function () {
                window.location.replace("/Admin/Event");
            }, 3000);
        }
    });
};
var goEventList = function () {
    window.location.replace("/Admin/Event/");
};
var getPropertyList = function () {
    $.ajax({
        url: "/PropertyManagement/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlProperty").empty();
            $("#ddlProperty").append("<option value='0'>Select Property</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                $("#ddlProperty").append(option);

            });
        }
    });
};
var EventList = function () {
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

