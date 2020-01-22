$(document).ready(function () {
  

    getPropertyList();
    $("#ddlProperty").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getPropertyUnitList(selected);
        }
    });
    $("#ddlUnit").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {          
            getTenantList(selected);
        }
    });
});
var getTenantList = function (pid) {
    var model = { PID: pid };
    $.ajax({
        url: "/Admin/Tenant/FillTenantDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlTenant").empty();
            $("#ddlTenant").append("<option value='0'>Select Tenant</option>");
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.ID + ">" + elementValue.FullName + "</option>";
                $("#ddlTenant").append(option);

            });
        }
    });
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
var getPropertyUnitList = function (pid) {
    var model = { PID: pid };
    $.ajax({
        url: "/PropertyManagement/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlUnit").empty();
            $("#ddlUnit").append("<option value='0'>Select Unit</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                $("#ddlUnit").append(option);

            });
        }
    });
};
var noticeList = function () {
    window.location.replace("/Admin/Notice/");
};
function saveupdateNotice() {
    var msg = "";
    var noticeid = $("#hndNoticeID").val();
    var propertyid = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var tenantid = $("#ddlTenant").val();
    var revision_num = $("#txtRevisionNumber").val();
    var notice_date = $("#txtNoticeDate").val();
    var intended_moveout = $("#txtIntendedMoveOut").val();
    var cancle_notice_date = $("#txtIntendedMoveOut").val();
    var termination_reason = $("#txtTerminationReason").val();

    if (notice_date == "") {
        msg += "Please Enter Notice Date</br>";
    }
    if (revision_num == "") {
        msg += "Please Enter Revision Number</br>";
    }
    if (termination_reason == "") {
        msg += "Please Enter Termination Reason</br>";
    }
    if (!tenantid) {
        msg += "Please Select Tenant </br>";
    }

    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
    var model = {
        NoticeID : noticeid,
        PID : propertyid,
        UID : unitid,
        TID: tenantid,
        Revision_Num: revision_num,
        NoticeDate: notice_date,
        IntendedMoveOut: intended_moveout,
        CancelNoticeDate: cancle_notice_date,
        TerminationReason: termination_reason,
    };

    $.ajax({
        url: "/Notice/SaveUpdateNotice/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: 'Message!',
                content: response.Msg,
                type: 'blue',
            });
        }
    });
}