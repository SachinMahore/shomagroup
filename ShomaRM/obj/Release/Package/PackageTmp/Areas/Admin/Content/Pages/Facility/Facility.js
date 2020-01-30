$(document).ready(function () {
    getPropertyList();
});
var SaveUpdateFacility = function () {
    var msg = "";
    var facilityId = $("#hdnFacilityId").val();
    var facilityName = $("#txtFacilityName").val();
    var propertyId = $("#ddlProperty").val();
    var description = $("#txtDescription").val();

    if (facilityName == "") {
        msg += "Please Fill The Facility Name </br>";
    }
    if (propertyId == "0") {
        msg += "Please Select The Property </br>";
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
    $formData.append('FacilityID', facilityId);
    $formData.append('FacilityName', facilityName);
    $formData.append('PropertyID', propertyId);
    $formData.append('Description', description);
    var photo = document.getElementById('wizard-picture');
    for (var i = 0; i < photo.files.length; i++) {
        $formData.append('file-' + i, photo.files[i]);
    }
    $.ajax({
        url: '/Admin/Facility/SaveUpdateFacility',
        type: 'post',
        data: $formData,
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.msg,
                type: 'blue'
            });
            setInterval(function () {
                window.location.replace("/Admin/Facility/");
            }, 3000);
        }
    });
};
var goFacilityList = function () {
    window.location.replace("/Admin/Facility/");
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
var FacilityList = function () {
    $.ajax({
        url: '/Facility/FacilityDataList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $("#ddlFacility").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = '<option value=' + elementValue.FacilityID + '>' + elementValue.FacilityName + '</option>';
                $("#ddlFacility").append(option);
            });
        }
    });
};

