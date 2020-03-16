$(document).ready(function () {
    getPropertyList();
    document.getElementById('wizard-picture').onchange = function () {
        var fileUploadEventBool = restrictFileUpload($(this).val());
        if (fileUploadEventBool == true) {
            eventFileUpload();
        }
        else {
            document.getElementById('wizard-picture').value = '';
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
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
    var eventPhoto = $("#hndFileUploadEventPicture").val();

    if (propertyId == "0") {
        msg += "Please Select The Property </br>";
    }
    if (eventName == "") {
        msg += "Please Fill The Event Name </br>";
    }
    if (eventDate == "") {
        msg += "Please Fill The Event Date </br>";
    }
    if (eventTime == "") {
        msg += "Please Fill The Event Time </br>";
    }
    if (eventFees == "") {
        msg += "Please Fill The Event Fees </br>";
    }
    if (eventType == "0") {
        msg += "Please Fill The Event Type </br>";
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
    $formData.append('EventTimeString', eventTime);
    $formData.append('Fees', eventFees);
    $formData.append('Photo', eventPhoto);
    //var photo = document.getElementById('wizard-picture');
    //if (photo.files.length > 0) {
    //    for (var i = 0; i < photo.files.length; i++) {
    //        $formData.append('file-' + i, photo.files[i]);
    //        console.log(photo.files[i]);
    //    }
    //}
    
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
var eventFileUpload = function () {
    $("#divLoader").show();
    $formData = new FormData();

    var eventFile = document.getElementById('wizard-picture');

    for (var i = 0; i < eventFile.files.length; i++) {
        $formData.append('file-' + i, eventFile.files[i]);
    }

    $.ajax({
        url: '/Admin/Event/EventFileUpload',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndFileUploadEventPicture').val(response.model.Photo);
            $('#hndOriginalFileUploadEventPicture').val(response.model.OriginalPhoto);

            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
            $("#divLoader").hide();
        }
    });
};


