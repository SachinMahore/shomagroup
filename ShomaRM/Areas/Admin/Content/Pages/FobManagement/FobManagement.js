$(document).ready(function () {
    getApplicantListForFob();
});
var viewApplicantDetails = function (id) {
    
    var AppId = id;

    var model = {
        ApplicantId: AppId,
    };
    $.ajax({
        url: "/FobManagement/GetApplicantData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#lblNameAppDetail").text(response.FullName);
            $("#lblDOBAppDetail").text(response.DateOfBirthTxt);
            $("#lblSSNAppDetail").text(response.SSNstring);
            $("#lblEmailAppDetail").text(response.Email);
            $("#lblGenderAppDetail").text(response.Gender == '1' ? 'Male' : response.Gender == '2' ? 'Female' : 'Other');

            //table
            $("#tblAppDetails tbody").empty();
            var htmlResp15 = "<tr data-value='" + response.ApplicantID + "'>";
            htmlResp15 += "<td> " + response.FullName + "</td>";
            htmlResp15 += "<td> " + response.Type + "</td>";
            htmlResp15 += "<td> " + response.MoveInPercentage + "%</td>";
            htmlResp15 += "<td> $" + formatMoney(response.MoveInCharges) + "</td>";
            htmlResp15 += "<td> " + response.MonthlyPercentage + "%</td>";
            htmlResp15 += "<td> $" + formatMoney(response.MonthlyCharges) + "</td>";
            htmlResp15 += "</tr>";
            $("#tblAppDetails tbody").append(htmlResp15);
            //end table
            
        }
    });

    $('#modalViewApplicantDetails').modal('show');
}
var saveTenantFob = function (id, oid, tid, sr) {
    var tId = $(tid).val();
    $("#divLoader").show();
    var msg = '';
    var FobKey = $('#txt' + sr).val();
    //var activateOrNot = $('#chk' + id).is(":checked") ? "1" : "0";
    var activateOrNot = tId;
    var otherId = oid;
    if (!otherId) {
        otherId = "0";
    }
    if (activateOrNot == '0') {
        msg += 'Please select the status</br>';
        
    }
    if (!FobKey) {
        msg += 'Please fill the FOB key</br>';
    }
    if (msg != '') {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        });
        $(tid).val("0");
        $("#divLoader").hide();
        return
    }

    var model = {
        TenantID: $("#hdnTenantId").val(),
        ApplicantId: id,
        Status: activateOrNot,
        FobKey: FobKey,
        OtherId: otherId
    };
    $.ajax({
        url: "/FobManagement/SaveTenantFob",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            getApplicantListForFob();
            $.alert({
                title: "",
                content: response,
                type: 'red'
            });
            $("#divLoader").hide();
        }
    });
}

var getApplicantListForFob = function () {
    var model = {
        TenantID: $("#hdnTenantId").val()
    };
    $.ajax({
        url: "/FobManagement/GetAllApplicant",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#tblFobMoveInCheckList>tbody").empty();
            var srNo = 0;
            $.each(response, function (elementType, elementValue) {
                srNo++;
                if (elementValue.Status == '1') {
                    //var check = "<input id='chk" + elementValue.ApplicantID + "' type='checkbox' class='' checked='checked' onclick='saveTenantFob(" + elementValue.ApplicantID + ");' />";
                    var selectddl = "<select data-value='" + elementValue.OtherId + "' id='ddl" + srNo + "' onchange='saveTenantFob(" + elementValue.ApplicantID + "," + elementValue.OtherId + ", this ," + srNo + ");'><option value='0'>Select</option><option value='1' selected='selected'>Activate</option><option value='2'>Deactivate</option></select>";
                    var input = "<div style='margin-left: 24px;margin-right:24px;width:207px;margin-top:9px;margin-bottom:9px;'><input type='text' class='form-control' id='txt" + srNo + "' value='" + elementValue.FobID + "' disabled='disabled' /></div>"
                    var edit = "<i class='fa fa-trash fa-lg' style='cursor:pointer; 'onclick='delTenantFob(" + elementValue.ApplicantID + "," + elementValue.OtherId + ")'></i>";
                }
                else if (elementValue.Status == '2') {
                    //var check = "<input id='chk" + elementValue.ApplicantID + "' type='checkbox' class='' checked='checked' onclick='saveTenantFob(" + elementValue.ApplicantID + ");' />";
                    var selectddl = "<select data-value='" + elementValue.OtherId + "' id='ddl" + srNo + "' onchange='saveTenantFob(" + elementValue.ApplicantID + "," + elementValue.OtherId + ", this ," + srNo + ");'><option value='0'>Select</option><option value='1'>Activate</option><option value='2' selected='selected'>Deactivate</option></select>";
                    var input = "<div style='margin-left: 24px;margin-right:24px;width:207px;margin-top:9px;margin-bottom:9px;'><input type='text' class='form-control' id='txt" + srNo + "' value='" + elementValue.FobID + "' disabled='disabled' /></div>"
                    var edit = "<i class='fa fa-trash fa-lg' style='cursor:pointer; 'onclick='delTenantFob(" + elementValue.ApplicantID + "," + elementValue.OtherId + ")'></i>";
                }
                else {
                    //var check = "<input id='chk" + elementValue.ApplicantID + "' type='checkbox' class='' onclick='saveTenantFob(" + elementValue.ApplicantID + ");' />";
                    var selectddl = "<select data-value='" + elementValue.OtherId + "' id='ddl" + srNo + "' onchange='saveTenantFob(" + elementValue.ApplicantID + "," + elementValue.OtherId + ", this ," + srNo + ");'><option value='0' selected='selected'>Select</option><option value='1'>Activate</option><option value='2'>Deactivate</option></select>";
                    var input = "<div style='margin-left: 24px;margin-right:24px;width:207px;margin-top:9px;margin-bottom:9px;'><input type='text' class='form-control' id='txt" + srNo + "' value='" + elementValue.FobID + "' /></div>"
                    var edit = "";
                }
                
                var html = "<tr data-value='" + elementValue.ApplicantID + "'>";
                html += "<td style='text-align:center;'>" + srNo + "</td>";
                html += "<td style='text-align:center;width:50px'>" + input + "</td>";
                html += "<td style='text-align:center;'>" + elementValue.UnitNo + "</td>";
                html += "<td style='text-align:center;'>" + elementValue.Type + "</td>";
                html += "<td style='text-align:center;'>" + elementValue.FullName + "</td>";
                html += "<td style='text-align:center;'>" + elementValue.Relationship + "</td>";
                html += "<td class='text-center'>";
                html += selectddl;
                html += "</td>";
                html += "<td style='text-align:center;'>";
                html += edit;
                html += "</td>";
                html += "</tr>";
                $("#tblFobMoveInCheckList>tbody").append(html);
            });
        }
    });
}

var delTenantFob = function (id, did) {
    if (!did) {
        did = 0;
    }
    var model = {
        TenantID: $("#hdnTenantId").val(),
        ApplicantId: id,
        OtherId: did
    };
    $.alert({
        title: "",
        content: "Are you sure to remove?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $("#divLoader").show();
                    $.ajax({
                        url: "/FobManagement/DelTenantFob",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            getApplicantListForFob();
                            $.alert({
                                title: "",
                                content: response,
                                type: 'red'
                            });
                            $("#divLoader").hide();
                        }
                    });
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                }
            }
        }
    });
};

var addNewFOB = function () {
    getApplicantNamesForFob();
    clearAddFob();
    $('#modalAddFob').modal('show');
};

var getApplicantNamesForFob = function () {
    $("#divLoader").show();
    var TenantID = $("#hdnTenantId").val();

    var model = {
        TenantID: TenantID,
    };
    $.ajax({
        url: "/FobManagement/GetApplicantNamesForFob",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlFobUserNameAddFob").empty();
            var html = "<option value='0'>Select FOB User Name</option>"
            $.each(response, function (elementType, elementValue) {
                html += "<option value='" + elementValue.ApplicantID + "'>" + elementValue.FullName + "</option>"
            });
            html += "<option value='other'>Other</option>"
            $("#ddlFobUserNameAddFob").append(html);
            $("#divLoader").hide();
        }
    });
}

var fobUserNameddlFunction = function () {
    if ($('#ddlFobUserNameAddFob').val() == 'other') {
        $('#divAddFobFirstName').removeClass('hidden');
        $('#divAddFobLastName').removeClass('hidden');

        $('#divAddFobRelationship').removeClass('hidden');
        $('#divAddFobTenantLabel').addClass('hidden');
        $('#divAddFobRelationshipLabel').addClass('hidden');
    }
    else {
        $('#divAddFobFirstName').addClass('hidden');
        $('#divAddFobLastName').addClass('hidden');

        $('#divAddFobRelationship').addClass('hidden');
        $('#divAddFobTenantLabel').removeClass('hidden');
        $('#divAddFobRelationshipLabel').removeClass('hidden');
    }
    getApplicantDataForFob();
};

var getApplicantDataForFob = function () {
    $("#divLoader").show();
    var fAppId = $("#ddlFobUserNameAddFob").val();
    var AppId = '';
    if (fAppId == 'other') {
        AppId = -1;
    }
    else {
        AppId = fAppId;
    }
    var model = {
        ApplicantId: AppId,
        TenantId: $("#hdnTenantId").val()
    };
    $.ajax({
        url: "/FobManagement/GetTenantFobData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response.StatusString == 'Already Activated') {
                $("#lblUnitNoAddFob").text("");
                $("#lblTenantAddFob").text("");
                $("#lblRelationshipAddFob").text("");
                $("#btnSaveAddFob").attr('disabled', true);
                $.alert({
                    title: "",
                    content: response.StatusString,
                    type: 'red'
                })
            }
            else if (response.StatusString == 'InvalidId') {
                $("#lblUnitNoAddFob").text("");
                $("#lblTenantAddFob").text("");
                $("#lblRelationshipAddFob").text("");
                $("#btnSaveAddFob").attr('disabled', true);
            }
            else {
                $("#hndOtherId").val(response.OtherId);
                $("#lblUnitNoAddFob").text(response.UnitNo);
                $("#lblTenantAddFob").text(response.Type);
                $("#lblRelationshipAddFob").text(response.Relationship);
                $("#btnSaveAddFob").attr('disabled', false);
            }
            $("#divLoader").hide();
        }
    });
}

var saveOtherTenantFob = function () {
    $("#divLoader").show();
    var msg = '';
    var fName = $('#txtFirstNameAddFob').val();
    var lName = $('#txtLastNameAddFob').val();
    var rel = $('#ddlRelationshipAddFobOther').val();
    var fobKey = $('#txtFobKeyAddFob').val();
    var status = $('#ddlActivatedAddFob').val();
    var otherId = $('#hndOtherId').val();
    var tenantId = $('#hdnTenantId').val();
    var appId = $('#ddlFobUserNameAddFob').val();

    if (appId == '0') {
        msg = 'Select FOB user Name</br>'
    }
    else if (appId == 'other') {
        appId = 0;
        if (!fName) {
            msg += 'Fill First Name</br>'
        }
        if (!lName) {
            msg += 'Fill Last Name</br>'
        }
        if (rel == '0') {
            msg += 'Select Relationship</br>'
        }
    }
    if (!otherId) {
        otherId = 0;
    }

    if (!fobKey) {
        msg += 'Fill FOB key</br>'
    }
    if (status == '0') {
        msg += 'Select Status</br>'
    }
    if (msg != '') {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return
    }

    var model = {
        ApplicantId: appId,
        TenantID: tenantId,
        Fname: fName,
        Lname: lName,
        Relationship: rel,
        FobKey: fobKey,
        Status: status,
        OtherId: otherId,
    };

    $.ajax({
        url: "/FobManagement/SaveOtherTenantFob",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            getApplicantListForFob();
            $.alert({
                title: "",
                content: response,
                type: 'red'
            })
            $('#modalAddFob').modal('hide');
            $("#divLoader").hide();

        }
    });
};

var clearAddFob = function () {
    $('#txtFirstNameAddFob').val("");
    $('#txtLastNameAddFob').val("");
    $('#ddlRelationshipAddFobOther').val("Family Member");
    $('#txtFobKeyAddFob').val("");
    $('#ddlActivatedAddFob').val("0");
    $('#divAddFobFirstName').addClass('hidden');
    $('#divAddFobLastName').addClass('hidden');
    $('#divAddFobRelationship').addClass('hidden');
    $('#divAddFobTenantLabel').removeClass('hidden');
    $('#divAddFobRelationshipLabel').removeClass('hidden');
    $('#ddlFobUserNameAddFob').val("0");
    $('#lblUnitNoAddFob').text("");
    $('#btnSaveAddFob').attr("disabled", true);
}

var downloadInsuranceDoc = function (id) {
    var dtv = $("#dwnldPOI" + id).data('value');
    var url = "/../Content/assets/img/ChecklistDocument/" + dtv;
    var boolFileExist = doesFileExist(url);
    if (boolFileExist) {
        $("#dwnldPOI" + id).attr('href', '/../Content/assets/img/ChecklistDocument/' + dtv);
        $("#dwnldPOI" + id).attr('download', dtv);
        $("#dwnldPOI" + id).attr('target', '_blank');
        $("#dwnldPOI" + id).html('<i class="fa fa-download"></i>' + ' ' + dtv);
    }
    else {
        $.alert({
            title: "",
            content: 'File Not Exist!',
            type: 'red'
        })
    }
};

var downloadElectricDoc = function (id) {
    var dtv = $("#dwnldPOE" + id).data('value');
    var url = "/../Content/assets/img/ChecklistDocument/" + dtv;
    var boolFileExist = doesFileExist(url);
    if (boolFileExist) {
        $("#dwnldPOE" + id).attr('href', '/../Content/assets/img/ChecklistDocument/' + dtv);
        $("#dwnldPOE" + id).attr('download', dtv);
        $("#dwnldPOE" + id).attr('target', '_blank');
        $("#dwnldPOE" + id).html('<i class="fa fa-download"></i>' + ' ' + dtv);
    }
    else {
        $.alert({
            title: "",
            content: 'File Not Exist!',
            type: 'red'
        })
    }
};
