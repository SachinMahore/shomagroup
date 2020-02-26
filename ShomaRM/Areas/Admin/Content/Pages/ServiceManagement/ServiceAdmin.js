
$(document).ready(function () {

    goToServiceDetails($("#hndServiceID").val());
    onFocus();
    fillDdlUser();
    document.getElementById('fileCompleted').onchange = function () {
        uploadServiceFile();
    };

    document.getElementById('uploadphoto').onchange = function () {
        OwnerSignature();
    };
    $("#txtClosingDate").datepicker();
    $('#ClosingDate').click(function () {
        $("#txtClosingDate").focus();
    });
    $('#RequestedDate').click(function () {
        $("#txtRequestedDate").focus();
    });
});
var ServiceList = function () {
    window.location.href = ("../../ServicesManagement/Index/");
};
var goToServiceDetails = function (ServiceID) {
    $("#divLoader").show();
    var model = {
        ServiceID: ServiceID,
    };
    $.ajax({
        url: '/ServicesManagement/goToServiceDetails',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            $("#hndServiceID").val(response.model.ServiceID);
            $('#lbltenantName').text(response.model.TenantName);
            $('#ProblemCatrgory').text(response.model.ProblemCategorystring);
            $('#lblLeaseStartDate').text(response.model.MStartDateString);
            $('#lblLeaseTerminationtDate').text(response.model.MLeaseEndDateString);
            if (response.model.Details == null || response.model.Details == '') {

                $('#ProbleOther').addClass('hidden');

            } else {

                $('#lblProbleOther').text(response.model.Details);
                $('#ProbleOther').removeClass('hidden');
            }
            if (response.model.OtherItemCaussing == null || response.model.OtherItemCaussing == '') {

                $('#CaussingOther').addClass('hidden');
            } else {

                $('#lblCaussingOther').text(response.model.OtherItemCaussing);
                $('#CaussingOther').removeClass('hidden');
            }
            if (response.model.OtherItemIssue == null || response.model.OtherItemIssue == '') {

                $('#IssueOther').addClass('hidden');
            } else {
                $('#lblIssueOther').text(response.model.OtherItemIssue);
                $('#IssueOther').removeClass('hidden');
            }
            $('#lbltenantDate').text(response.model.MoveIndate);
            $('#lblProject').text(response.model.Project);
            $('#lbltenantId').text(response.model.TenantID);
            $('#lblcaussingIssue').text(response.model.CausingIssue);
            $('#lblIssue').text(response.model.Issue);
            $('#lblLocation').text(response.model.LocationString);
            $('#lblUnitNo').text(response.model.Unit);
            $('#lblContactNo').text(formatPhoneFax(response.model.Phone));
            $('#lblCurrentStatus').text(response.model.StatusString);
            $('#lblEnteryNote').text(response.model.Notes);
            if (response.model.UrgentStatus == '1') {
                $('#Urgent').iCheck('check');

            } else {
                $('#NotUrgent').iCheck('check');
            }

            if (response.model.WarrantyStatus == 1) {
                $('#Yes').iCheck('check');

            } else {
                $('#No').iCheck('check');
            }

            $('#lblEmergencyNo').text(formatPhoneFax(response.model.EmergencyMobile));
            $('#lblEmail').text(response.model.Email);

            if (response.model.PermissionComeDateString != null) {
                $('#AnyTime').addClass('hidden');
                $('#ActualAnytime').addClass('hidden');
                $('#ActualDateTime').removeClass('hidden');
                if (response.model.PermissionComeTime != 'Any Time') {
                    $('#lblRequestedTime').val(response.model.PermissionComeTime);
                }
                else {
                    $('#lblRequestedTime').val(0);
                }

                $('#lblAppointmentActualTime').text(response.model.PermissionComeTime);
                $('#lblAppointmentActualdate').text(response.model.PermissionComeDateString);
                if (response.model.PermissionComeDateString == 'Any Time') {
                    $("#txtRequestedDate").val();
                }
                else {
                    $("#txtRequestedDate").val(response.model.PermissionComeDateString);
                    $("#txtRequestedDate").datepicker("setdate", response.model.PermissionComeDateString);
                    $("#txtRequestedDate").val(response.model.PermissionComeDateString);
                }

            }

            if (response.model.Status != null) {

                if (response.model.Status == "1") {
                    $('#UnAssigned').iCheck('check');
                }
                else if (response.model.Status == "2") {
                    $('#Resolved').iCheck('check');
                }
                else if (response.model.Status == "3") {
                    $('#cancel').iCheck('check');
                }
                else if (response.model.Status == "4") {
                    $('#Active').iCheck('check');
                }

            }
            if (response.model.ServicePerson == "1") {
                $("#ddlUser").val('0');
            }
            else {
                setTimeout(function () {
                    $("#ddlUser").val(response.model.ServicePerson);
                }, 1000);

            }
            $("#txtClosingNotes").val(response.model.ClosingNotes);
            $("#txtClosingDate").val(response.model.ClosingDatestring);
            $("#txtClosingDate").datepicker("setdate", response.model.ClosingDatestring);


            if (response.model.TempServiceFile != null) {
                var fileExist = doesFileExist('/Content/assets/img/Document/' + response.model.TempServiceFile);
                if (fileExist) {
                    $('#wizardPicturePreview').attr('src', '/Content/assets/img/Document/' + response.model.TempServiceFile);
                }
                else {
                    $('#wizardPicturePreview').attr('src', '/Content/assets/img/aaa.png');

                }
            }
            else {
                $('#wizardPicturePreview').attr('src', '/Content/assets/img/aaa.png');
            }
        }
    });
    $("#divLoader").hide();
};
var onFocus = function () {

    $("#txtCellPhone").focusout(function () { $("#txtCellPhone").val(formatPhoneFax($("#txtCellPhone").val())); })
        .focus(function () {
            $("#txtCellPhone").val(unformatText($("#txtCellPhone").val()));
        });
}

function formatPhoneFax(phonefax) {
    if (phonefax == null)
        phonefax = "";
    phonefax = phonefax.replace(/[^0-9]/g, '');
    if (phonefax.length == 0)
        return phonefax;

    return '(' + phonefax.substring(0, 3) + ') ' + phonefax.substring(3, 6) + (phonefax.length > 6 ? '-' : '') + phonefax.substring(6);
}

function unformatText(text) {
    if (text == null)
        text = "";

    return text.replace(/[^\d\.]/g, '');
}
var fillDdlUser = function () {
    var param = { UserType: 7 };
    $.ajax({
        url: '/Users/GetUserListByType',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlUser").empty();
                $("#ddlUser").append("<option value='0'>Select Service Person</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlUser").append("<option value=" + elementValue.UserID + ">" + elementValue.FirstName + ' ' + elementValue.LastName + "</option>");
                });
            }
        }
    });
}

var StatusUpdateServiceRequest = function (id) {
    $("#divLoader").show();
    var msg = '';
    var id = $("#hndServiceID").val();
    var emmob = unformatText($("#lblEmergencyNo").text());
    var CompletedFileTemp = $("#hndfileCompleted").val();
    var CompletedFileOriginal = $("#hndOriginalfileCompleted").val();
    var closingNotes = $("#txtClosingNotes").val();
    var closingDate = $("#txtClosingDate").val();
    //var rescheduledate = $("#txtRequestedDate").val();
    //var rescheduletime = $("#lblRequestedTime").val();
    var ownerSignature = $("#hndHomeownerSignature").val();
    var tempOwnerSignature = $("#hndOriginalHomeownerSignature").val();

    //if (!rescheduledate) {
    //    msg += 'Select the Date</br>'
    //}
    //if (rescheduletime == '0') {
    //    msg += 'Select the Time</br>'
    //}
    //if (employee == '0') {
    //    msg += 'Select The Employee</br>'
    //}

    //var Urgentstatus = '';
    //if ($("#Urgent").is(":checked")) {
    //    Urgentstatus = 1;
    //}
    //else if ($("#NotUrgent").is(":checked")) {
    //    Urgentstatus = 0;
    //}

    //var status = '';
    //if ($("#UnAssigned").is(":checked")) {
    //    status = 1;
    //}
    //else if ($("#Active").is(":checked")) {
    //    status = 4;
    //}
    //else if ($("#Resolved").is(":checked")) {
    //    status = 2;
    //}
    //else if ($("#Cancel").is(":checked")) {
    //    status = 3;
    //}



    if (msg != "") {
        $.alert({
            title: '',
            content: msg,
            type: 'red'
        });
        $("#divLoader").hide();
        return
    }

    var model = {
        ServiceID: id,
        CompletedPicture: CompletedFileOriginal,
        TempCompletedPicture: CompletedFileTemp,
        ClosingNotes: closingNotes,
        ClosingDate: closingDate,
        OwnerSignature: ownerSignature,
        TempOwnerSignature: tempOwnerSignature,
    };
    $.ajax({
        url: '/ServicesManagement/StatusUpdateServiceRequest',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: '',
                content: response.model,
                type: 'red'
            });

            $("#fileCompletedShow").val('');
            $("#txtClosingNotes").val('');

        }
    });
    $("#divLoader").hide();
}
var StatusUpdateForServicePerson = function (id) {
    $("#divLoader").show();
    var msg = '';
    var id = $("#hndServiceID").val();
    var emmob = unformatText($("#lblEmergencyNo").text());
    var employee = $("#ddlUser").val();
    var rescheduledate = $("#txtRequestedDate").val();
    var rescheduletime = $("#lblRequestedTime").val();
    var tenantname = $("#lbltenantName").text();
    var tenantemail = $("#lblEmail").text();
    var problecategory = $("#ProblemCatrgory").text();

    var CompletedFileTemp = $("#hndfileCompleted").val();
    var CompletedFileOriginal = $("#hndOriginalfileCompleted").val();
    var closingNotes = $("#txtClosingNotes").val();
    var closingDate = $("#txtClosingDate").val();
    var ownerSignature = $("#hndHomeownerSignature").val();
    var tempOwnerSignature = $("#hndOriginalHomeownerSignature").val();

    if (!rescheduledate) {
        msg += 'Select the Date</br>'
    }
    if (rescheduletime == '0') {
        msg += 'Select the Time</br>'
    }
    if (employee == '0') {
        msg += 'Select The Employee</br>'
    }

    var Urgentstatus = '';
    if ($("#Urgent").is(":checked")) {
        Urgentstatus = 1;
    }
    else if ($("#NotUrgent").is(":checked")) {
        Urgentstatus = 0;
    }

    var status = '';
    if ($("#UnAssigned").is(":checked")) {
        status = 1;
    }
    else if ($("#Active").is(":checked")) {
        status = 4;
    }
    else if ($("#Resolved").is(":checked")) {
        status = 2;
    }
    else if ($("#Cancel").is(":checked")) {
        status = 3;
    }
    var Warrantystatus = '';
    if ($("#Yes").is(":checked")) {
        Warrantystatus = 1;
    }
    else if ($("#No").is(":checked")) {
        Warrantystatus = 0;
    }


    if (msg != "") {
        $.alert({
            title: '',
            content: msg,
            type: 'red'
        });
        $("#divLoader").hide();
        return
    }

    var model = {
        ServiceID: id,
        ServicePerson: employee,
        UrgentStatus: Urgentstatus,
        Status: status,
        PermissionComeDate: rescheduledate,
        PermissionComeTime: rescheduletime,
        TenantName: tenantname,
        Email: tenantemail,
        ProblemCategorystring: problecategory,
        CompletedPicture: CompletedFileOriginal,
        TempCompletedPicture: CompletedFileTemp,
        ClosingNotes: closingNotes,
        ClosingDate: closingDate,
        OwnerSignature: ownerSignature,
        TempOwnerSignature: tempOwnerSignature,
        WarrantyStatus: Warrantystatus,
    };
    $.ajax({
        url: '/ServicesManagement/StatusUpdateForServicePerson',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: '',
                content: response.model,
                type: 'red'
            });
            $("#ddlStatus1").val('0');
            $("#ddlUser").val('0');

        }
    });
    $("#divLoader").hide();
}
var uploadServiceFile = function () {
    $("#divLoader").show();
    $formData = new FormData();

    var ServiceFile = document.getElementById('fileCompleted');

    for (var i = 0; i < ServiceFile.files.length; i++) {
        $formData.append('file-' + i, ServiceFile.files[i]);
    }

    $.ajax({
        url: '/ServicesManagement/UploadServiceFile',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndfileCompleted').val(response.model.TempCompletedPicture);
            $('#hndOriginalfileCompleted').val(response.model.CompletedPicture);
            $('#fileCompletedShow').text(response.model.CompletedPicture);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'red'
            });
            $("#divLoader").hide();
        }
    });
};
var OwnerSignature = function () {
    $("#divLoader").show();
    $formData = new FormData();

    var OwnerSignatureFile = document.getElementById('uploadphoto');

    for (var i = 0; i < OwnerSignatureFile.files.length; i++) {
        $formData.append('file-' + i, OwnerSignatureFile.files[i]);
    }

    $.ajax({
        url: '/ServicesManagement/OwnerSignatureFile',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndHomeownerSignature').val(response.model.OwnerSignature);
            $('#hndOriginalHomeownerSignature').val(response.model.TempOwnerSignature);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'red'
            });
            $("#divLoader").hide();
        }
    });
};
