$(document).ready(function () {

    var geturlId = window.location.href.split("/")[6];
    if (geturlId != "") {
        getTenantOnlineData(geturlId);
    }
    //getTenantData($("#hndTenantID").val());
    getPropertyList();
    $("#ddlProperty").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getPropertyUnitList(selected);
        }
    });
    setTimeout(function () {
        fillStateDDL();
    }, 1500);
    $("#ddlState").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityList(selected);
        }
    });

    $("#ddlStateHome").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityListHome(selected);
        }
    });
    $("#ddlStateEmployee").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityListEmployee(selected);
        }
    });
    $("#ddlStateContact").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityListContact(selected);
        }
    });
    getTransationLists();
    TableClick();
    getChargeType();
    $("#popTransaction").PopupWindow({
        title: "Add Transaction",
        modal: false,
        autoOpen: false,
        top: 120,
        left: 300,
        height: 560,

    });
    $("#popApplicant").PopupWindow({
        title: "Add Applicant",
        modal: false,
        autoOpen: false,
        top: 120,
        left: 300,
        height: 400,

    });
    $("#popVehicle").PopupWindow({
        title: "Add Vehicle",
        modal: false,
        autoOpen: false,
        top: 120,
        left: 300,
        height: 400,

    });
    $("#popPet").PopupWindow({
        title: "Add Pet",
        modal: false,
        autoOpen: false,
        top: 120,
        left: 300,
        height: 400,

    });
    $("#btnAddTrans").on("click", function (event) {
        clearTrans();
        $("#popTransaction").PopupWindow("open");
    });
    $("#btnAddApplicant").on("click", function (event) {
        clearApplicant();
        $("#popApplicant").PopupWindow("open");
    });
    $("#btnAddVehicle").on("click", function (event) {
        clearVehicle();
        $("#popVehicle").PopupWindow("open");
    });
    $("#btnAddPet").on("click", function (event) {
        clearPet();
        $("#popPet").PopupWindow("open");
    });

    if ($("#ddlIsInter").val() == 1) {
        $("#passportDiv").removeClass("hidden");
    }
    else {
        $("#passportDiv").addClass("hidden");
    }
    $("#ddlIsInter").on("change", function () {
        if ($(this).val() == 1) {
            $("#passportDiv").removeClass("hidden");
        }
        else {
            $("#passportDiv").addClass("hidden");
        }
    });

});

var getTenantData = function (userID) {
    //alert(userID);
    var params = { TenantID: userID };
    $.ajax({
        url: "../GetTenantInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {

            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndTenantID").val(response.ID);
                $("#txtFirstName").val(response.FirstName);
                $("#txtMiddleInitial").val(response.MiddleInitial);
                $("#txtLastName").val(response.LastName);

                $("#txtAddress").val(response.Address);
                $("#txtCity").val(response.City);

                $("#txtZip").val(response.Zip);
                $("#ddlGender").val(response.Gender);
                $("#txtJobCode").val(response.JobCode);
                $("#txtWorkPhone").val(response.OfficePhone);
                $("#txtHomePhone").val(response.HomePhone);
                $("#txtWorkExtension").val(response.OfficePhoneExtension);
                $("#txtOfficeEmail").val(response.OfficeEmail);
                $("#txtOfficeName").val(response.OfficeName);
                $("#txtOfficeAddress").val(response.OfficeAddress);
                $("#txtOfficeCity").val(response.OfficeCity);

                $("#txtOfficeZip").val(response.OfficeZip);
                $("#txtOccupation").val(response.Occupation);
                $("#txtSocialSecurityNumber").val(response.SocialSecurityNum);
                $("#txtDrivingLicense").val(response.DriverLicense);
                $("#txtDateOfBirth").val(response.DateOfBirthText);
                $("#txtCarMake").val(response.CarMake);
                $("#txtCarModel").val(response.CarModel);
                $("#txtCarLicense").val(response.CarLic);
                $("#txtEmergencyContact").val(response.EmergencyContact);
                $("#txtEmergencyPhone").val(response.EmergencyPhone);
                $("#txtIncome").val(response.Income);
                $("#txtEmployerContact").val(response.EmployerContact);

                $("#txtRentResp").val(response.RentResp);

                $("#ddlGender").val(response.Gender);
                $("#ddlGender").trigger('change');

                $("#ddlMaritalStatus").val(response.MaritalStatus);
                $("#ddlMaritalStatus").trigger('change');



                setTimeout(function () {
                    $("#ddlState").val(response.State);
                    $("#ddlState").trigger('change');

                    $("#ddlOfficeState").val(response.OfficeState);
                    $("#ddlOfficeState").trigger('change');

                    $("#ddlProperty").val(response.PropertyID);
                    $("#ddlProperty").trigger('change');

                }, 1200);

                setTimeout(function () {

                    $("#ddlCity").val(response.City);
                    $("#ddlCity").trigger('change');

                    $("#ddlOfficeCity").val(response.OfficeCity);
                    $("#ddlOfficeCity").trigger('change');


                    $("#ddlUnit").val(response.UnitID);
                    $("#ddlUnit").trigger('change');
                }, 1600);



                $("#ddlStudentStatus").val(response.StudentStatus);
                $("#ddlStudentStatus").trigger('change');

                if ($("#hndTenantID").val() != "0") {
                    $("#spanSaveUpdate").text("Update");
                }
                else {
                    $("#spanSaveUpdate").text("Save");
                }

            }
        }
    });
}

var saveUpdateTenant = function () {

var msg = "";
    if ($.trim($("#txtFirstName").val()).length <= 0) {
        msg += "First Name is required.</br>"
    }
    if ($.trim($("#txtLastName").val()).length <= 0) {
        msg += "Last Name is required.</br>"
    }
    if ($("#ddlGender").val()== 0) {
        msg += "Gender is required.</br>"
    }
    if ($.trim($("#txtDateOfBirth").val()).length <= 0) {
        msg += "Date of Birth is required.</br>"
    }
    if ($("#ddlMaritalStatus").val() == 0) {
        msg += "Marital Status is required.</br>"
    }
    if ($("#ddlState").val() == 0) {
        msg += "Sate is required.</br>"
    }
    if ($("#ddlCity").val() == 0) {
        msg += "City is required.</br>"
    }

    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        })
        return;
    }
    else {
        var model = {
            ID: $("#hndTenantID").val(),
            FirstName: $("#txtFirstName").val(),
            MiddleInitial: $("#txtMiddleInitial").val(),
            LastName: $("#txtLastName").val(),
            PropertyID: $("#ddlProperty").val(),
            UnitID: $("#ddlUnit").val(),
            Address: $("#txtAddress").val(),
            City: $("#ddlCity").val(),
            State: $("#ddlState").val(),
            Zip: $("#txtZip").val(),
            Gender: $("#ddlGender").val(),
            JobCode: $("#txtJobCode").val(),
            HomePhone: $("#txtHomePhone").val(),
            OfficePhoneExtension: $("#txtWorkExtension").val(),
            OfficeEmail: $("#txtOfficeEmail").val(),
            OfficePhone: $("#txtWorkPhone").val(),
            OfficeName: $("#txtOfficeName").val(),
            OfficeAddress: $("#txtOfficeAddress").val(),
            OfficeCity: $("#ddlOfficeCity").val(),
            OfficeState: $("#ddlOfficeState").val(),
            OfficeZip: $("#txtOfficeZip").val(),
            Occupation: $("#txtOccupation").val(),
            SocialSecurityNum: $("#txtSocialSecurityNumber").val(),
            DriverLicense: $("#txtDrivingLicense").val(),
            DateOfBirth: $("#txtDateOfBirth").val(),
            CarMake: $("#txtCarMake").val(),
            CarModel: $("#txtCarModel").val(),
            CarLic: $("#txtCarLicense").val(),
            EmergencyContact: $("#txtEmergencyContact").val(),
            EmergencyPhone: $("#txtEmergencyPhone").val(),
            Income: $("#txtIncome").val(),
            EmployerContact: $("#txtEmployerContact").val(),
            MaritalStatus: $("#ddlMaritalStatus").val(),
            StudentStatus: $("#ddlStudentStatus").val(),
            RentResp: $("#txtRentResp").val(),
        };
        $.ajax({
            url: "../SaveUpdateTenant",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndTenantID").val() == 0) {
                        $.alert({
                            title: 'Alert!',
                            content: 'Data Saved Successfully',
                            type: 'blue'
                        })
                        $("#hndTenantID").val(response.ID);
                        window.location.href = "../Index/" + response.ID;
                    }
                    else {
                        $("#hndTenantID").val(response.ID);
                        $.alert({
                            title: 'Alert!',
                            content: 'Data Updated Successfully',
                            type: 'blue'
                        })
                    }
                }
                else {
                    //showMessage("Error!", response.error);
                }
            }
        });
    }
}

var getPropertyList = function () {
    $.ajax({
        url: "/Lease/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlProperty").empty();
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                $("#ddlProperty").append(option);

            });

        }
    });

};

var getPropertyUnitList = function (pid) {
    var model = { PID: pid }
    $.ajax({
        url: "/Lease/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlUnit").empty();
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                $("#ddlUnit").append(option);

            });

        }
    });

};

var fillStateDDL = function () {

    $.ajax({
        url: '/City/FillStateDropDownList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlState").empty();
                $("#ddlStatePersonal").empty();
                $("#ddlStateHome").empty();
                $("#ddlStateEmployee").empty();
                $("#ddlStateContact").empty();
                $("#ddlVState").empty();
                $("#ddlState").append("<option value='0'>Select State</option>");
                $("#ddlStatePersonal").append("<option value='0'>Select State</option>");
                $("#ddlStateHome").append("<option value='0'>Select State</option>");
                $("#ddlStateEmployee").append("<option value='0'>Select State</option>");
                $("#ddlStateContact").append("<option value='0'>Select State</option>");
                $("#ddlVState").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStatePersonal").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStateHome").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStateEmployee").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStateContact").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlVState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });

            }
        }
    });
}

var fillCityList = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCity").empty();
                $("#ddlCity").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var fillCityListHome = function (stateid) {

    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCityHome").empty();
                $("#ddlCityHome").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {

                    $("#ddlCityHome").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");

                });
            }
        }
    });
}
var fillCityListEmployee = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCityEmployee").empty();
                $("#ddlCityEmployee").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {

                    $("#ddlCityEmployee").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");

                });
            }
        }
    });
}
var fillCityListContact = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCityContact").empty();
                $("#ddlCityContact").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {

                    $("#ddlCityContact").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");


                });
            }
        }
    });
}


var goToStep = function (stepid, id) {
    if (stepid == "1") {
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step1").removeClass("hidden");
        $("#step7").addClass("hidden");
        $("#li7").removeClass("active");
    }
    if (stepid == "2") {
        $.get("../../Lease/Edit/?id=" + id, function (data) {
            $("#step2").html(data);
        });
        $("#li2").addClass("active");
        $("#li1").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step2").removeClass("hidden");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#li7").removeClass("active");
    }
    if (stepid == "3") {
        $("#li3").addClass("active");
        $("#li2").removeClass("active");
        $("#li1").removeClass("active");
        $("#li4").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step3").removeClass("hidden");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#li7").removeClass("active");
    }
    if (stepid == "4") {
        getApplicantLists();
        $("#li4").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li1").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").removeClass("hidden");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#li7").removeClass("active");
    }
    if (stepid == "5") {
        $("#step5").removeClass("hidden");
        getVehicleLists();
        $("#li5").addClass("active");
    
        $("#li4").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li1").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#li6").removeClass("active");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#li7").removeClass("active");
    }
    if (stepid == "6") {
        getPetLists();
        $("#li6").addClass("active");
        $("#step6").removeClass("hidden");
        $("#step1").addClass("hidden");
        $("#li1").removeClass("active");
        $("#step2").addClass("hidden");
        $("#li2").removeClass("active");
        $("#step3").addClass("hidden");
        $("#li3").removeClass("active");
        $("#step4").addClass("hidden");
        $("#li4").removeClass("active");
        $("#step5").addClass("hidden");
        $("#li5").removeClass("active");
        $("#step7").addClass("hidden");
        $("#li7").removeClass("active");
    }
    if (stepid == "7") {

        getNoticeLists();
        $("#li7").addClass("active");
        $("#step7").removeClass("hidden");
        $("#step1").addClass("hidden");
        $("#li1").removeClass("active");
        $("#step2").addClass("hidden");
        $("#li2").removeClass("active");
        $("#step3").addClass("hidden");
        $("#li3").removeClass("active");
        $("#step4").addClass("hidden");
        $("#li4").removeClass("active");
        $("#step5").addClass("hidden");
        $("#li5").removeClass("active");
        $("#step6").addClass("hidden");
        $("#li6").addClass("active");
    }
};

var getNoticeLists = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val(),
        TenantID: $("#hndTenantID").val(),
    }
    $.ajax({
        url: "/Notice/GetTenantNoticeList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblNotice>tbody").empty();
            $.each(response.result, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.NoticeID + ">";
                html += "<td>" + elementValue.NoticeID + "</td>";
                html += "<td>" + elementValue.Revision_Num + "</td>";
                html += "<td>" + elementValue.NoticeDateString + "</td>";
                html += "<td>" + elementValue.TerminationReason + "</td>";
                html += "</tr>";
                $("#tblNotice>tbody").append(html);
            });

        }
    });
}
var addNewNotice = function () {
    window.location.replace("/Admin/Notice/Edit/" + 0)
}

function saveupdateLease() {
    var msg = "";
    var lid = $("#hndLID").val();
    var property = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var revision_number = $("#txtRevision").val();
    var status = $("#ddlStatus").val();
    var being_terminated = $("#ddlBeingTerminated").val();
    var phase = $("#txtPhase").val();

    var noticedate = $("#txtNoticeDate").val();
    var termination_reason = $("#txtTerminateReason").val();
    var balanced = $("#txtBalance").val();
    var previous_balance = $("#txtPreviousbalance").val();
    var number_return_check = $("#txtNRChecks").val();

    var last_return_check_date = $("#txtLastReturnDate").val();
    var last_return_payment_id = $("#txtLastReturnPmt").val();
    var last_return_check_number = $("#txtLastReturnCheckNum").val();
    var original_lease_start_date = $("#txtOriginalStart").val();
    var actual_lease_start_date = $("#txtActualStart").val();

    var original_lease_end_date = $("#txtOriginalEnd").val();
    var actual_lease_end_date = $("#txtActualEnd").val();
    var intended_movein_date = $("#txtIntendedMoveIn").val();
    var actual_movein_date = $("#txtActualMoveIn").val();
    var intended_moveout_date = $("#txtIntendedMoveOut").val();

    var actual_moveout_date = $("#txtActualMoveOut").val();
    var statement_type = $("#txtStatementType").val();
    var last_rent_roll_date = $("#txtLastRentDate").val();
    var created_by = $("#txtCreatedBy").val();
    var created_date = $("#txtCreatedDate").val();

    var recounciled_date = $("#txtReconciledDate").val();
    var terms = $("#txtTerms").val();


    if (revision_number == "") {
        msg += "Plz enter Revision Number</br>";
    }

    if (balanced == "") {
        msg += "Plz enter Balance</br>";
    }
    if (previous_balance == "") {
        msg += "Plz enter Previous Balance</br>";
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
        LID: lid,
        PID: property,
        UID: unitid,
        Revision_Num: revision_number,
        Status: status,
        Being_Terminated: being_terminated,
        Phase: phase,
        Notice_Date: noticedate,
        Termination_Reason: termination_reason,
        Balance: balanced,
        Previous_Balance: previous_balance,
        Number_Returned_Checks: number_return_check,

        Last_Return_Ck_Date: last_return_check_date,
        Last_Return_Pmt_ID: last_return_payment_id,
        Last_Return_Ck_Num: last_return_check_number,
        Original_Start: original_lease_start_date,
        Actual_Start: actual_lease_start_date,
        Original_Lease_End: original_lease_end_date,
        Actual_Lease_End: actual_lease_end_date,
        Intended_MoveIn_Date: intended_movein_date,
        Actual_MoveIn_Date: actual_movein_date,
        Intend_MoveOut_Date: intended_moveout_date,
        Actual_MoveOut_Date: actual_moveout_date,
        Statement_Type: statement_type,
        Last_Rent_Roll_Date: last_rent_roll_date,
        CreatedBy: created_by,
        CreatedDate: created_date,
        Reconciled: recounciled_date,
        Term: terms,
    };

    $.ajax({
        url: "/Lease/SaveUpdateLease/",
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

var getTransationLists = function () {
    var model = {
        FromDate: $("#txtTransFromDate").val(),
        ToDate: $("#txtTransToDate").val(),
        TenantID: $("#hndTenantID").val(),
    }
    $.ajax({
        url: "/MyTransaction/GetTenantTransactionList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblTransaction>tbody").empty();

            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.TransID + ">";
                html += "<td>" + elementValue.TransID + "</td>";
                html += "<td>" + elementValue.TenantIDString + "</td>";
                html += "<td>" + elementValue.Transaction_DateString + "</td>";
                html += "<td>$" + elementValue.Charge_Amount + "</td>";
                html += "<td>" + elementValue.Transaction_Type + "</td>";
                html += "<td>" + elementValue.Charge_Type + "</td>";
                html += "<td>" + elementValue.CreatedByText + "</td>";
                html += "<td>" + elementValue.CreatedDateString + "</td>";
                html += "</tr>";
                $("#tblTransaction>tbody").append(html);
            });

        }
    });
}
var getChargeType = function () {
    $.ajax({
        url: "/ChargeType/GetCTypeList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlChargeType").empty();
            $("#ddlChargeType").append("<option value='0'>Select Charge Type</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.CTID + ">" + elementValue.Charge_Type + "</option>";
                $("#ddlChargeType").append(option);
            });

        }
    });

};
var TableClick = function () {

    $('#tblTransaction tbody').on('click', 'tr', function () {
        $('#tblTransaction tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblTransaction tbody').on('dblclick', 'tr', function () {
        goToEditTransaction();
    });
}
var goToEditTransaction = function () {

    var row = $('#tblTransaction tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {
      
        $("#hndTransID").val(ID);
        var model = { id:ID };
        $.ajax({
            url: "/MyTransaction/GetTransactionDetails/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#popTransaction").PopupWindow("open");
                $("#ddlTransactionType").val(response.model.Transaction_Type);
                $("#txtChargeDate").val(response.model.Charge_Date);
                $("#ddlChargeType").val(response.model.Charge_Type);
                $("#txtChargeAmount").val(response.model.Charge_Amount);
                $("#txtSummaryCharge").val(response.model.Description);
                $("#txtDesc").val(response.model.Description);
            }
        });
       
    } else {

    }
}
function saveupdateTransaction() {

    var msg = "";
    var transid = $("#hndTransID").val();
    var propertyid = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var tenantid = $("#hndTenantID").val();
    var leaseId = $("#hndLID").val();
    //var createddate = $("#txtCreatedDate").val();
    var revision_num =1;
   // var paymentId = $("#txtPaymentID").val();
   // var transdate = $("#txtTransactionDate").val();
    var transtype = $("#ddlTransactionType").val();
    //var creditamount = $("#txtCreaditAmount").val();
    var chargeDate = $("#txtChargeDate").val();
    var chargeType = $("#ddlChargeType").val();
    var chargeAmount = $("#txtChargeAmount").val();
    var summaryCharge = $("#txtSummaryCharge").val();
   
    var desc = $("#txtDesc").val();
   
    if (transtype == 0) {
        msg += "Select Transaction Type";
    }
    if (chargeType == 0) {
        msg += "Select Charge Type";
    }
    if (chargeDate =="") {
        msg += "Enter Charge Date";
    }
    if (chargeAmount == 0) {
        msg += "Enter Charge Amount";
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
        TransID: transid,
        PropertyID: propertyid,
        UnitID: unitid,
        TenantID: tenantid,
        LeaseID: leaseId,
        //CreatedDate: createddate,
        Revision_Num: revision_num,
       // Payment_ID: paymentId,
        //Transaction_Date: transdate,
        Transaction_Type: transtype,
        //Credit_Amount: creditamount,
        Charge_Date: chargeDate,
        Charge_Type: chargeType,
        Charge_Amount: chargeAmount,
        Summary_Charge_Type: summaryCharge,
     
        Description: desc,
    
    };

    $.ajax({
        url: "/MyTransaction/SaveUpdateTransaction/",
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
            clearTrans();
            getTransationLists();
        }


    });

}
var clearTrans = function () {
    $("#hndTransID").val(0);
  
    $("#ddlTransactionType").val(0);
    $("#txtChargeDate").val("");
     $("#ddlChargeType").val(0);
    $("#txtChargeAmount").val(0);
   $("#txtSummaryCharge").val("");
   $("#txtDesc").val("");
}

var saveupdateVehicle = function () {

    var msg = "";
    var vid = $("#hndVehicleID").val();
    var vmake = $("#txtVehicleMake").val();
    var vmodel = $("#txtVehicleModel").val();
    var vyear = $("#ddlVehicleyear").val();
    var vcolor = $("#txtVehicleColor").val();
    var vlicence = $("#txtVehicleLicence").val();
    var vstate = $("#ddlVState").val();

    if (vmake == "") {
        msg += "Enter Vehicle Make</br>";
    }
    if (vlicence == "") {
        msg += "Enter Vehicle Licence</br>";
    }
    if (vyear == "") {
        msg += "Enter Vehicle Year";
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
        Vehicle_ID: vid,
        Make: vmake,
        VModel: vmodel,
        Year: vyear,
        Color: vcolor,
        State: vstate,
        License: vlicence,
             
    };
  
    $.ajax({
        url: "/Tenant/Vehicle/SaveUpdateVehicle/",
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
          
            getVehicleLists();
            $("#popVehicle").PopupWindow("close");
        }


    });

}
var getVehicleLists = function () {
    var model = {
       
        TenantID: $("#hndTenantID").val(),
    }
    $.ajax({
        url: "/Vehicle/GetVehicleList",
        type: "post",
        contentType: "application/json utf-8",
       data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblVehicle>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.Vehicle_ID + ">";
                html += "<td>" + elementValue.Make + "</td>";
                html += "<td>" + elementValue.VModel + "</td>";
                html += "<td>" + elementValue.Year + "</td>";
                html += "<td>" + elementValue.Color + "</td>";
                html += "<td>" + elementValue.License + "</td>";
                html += "<td>" + elementValue.State + "</td>";
                html += "</tr>";
                $("#tblVehicle>tbody").append(html);
            });

        }
    });
}
var clearVehicle = function () {
   $("#hndVehicleID").val(0);
    $("#txtVehicleMake").val("");
    $("#txtVehicleModel").val("");

     $("#txtVehicleColor").val("");
   $("#txtVehicleLicence").val("");
    $("#ddlVState").val(0);
}

var saveupdateApplicant = function () {

    var msg = "";
    var aid = $("#hndApplicantID").val();
    var fname = $("#txtApplicantFirstName").val();
    var lname = $("#txtApplicantLastName").val();
    var aphone = $("#txtApplicantPhone").val();
    var aemail = $("#txtApplicantEmail").val();
    var agender = $("#ddlApplicantGender").val();
    var dob = $("#txtADateOfBirth").val();

    if (fname == "") {
        msg += "Enter Applicant First Name</br>";
    }
    if (lname == "") {
        msg += "Enter Applicant Last Name</br>";
    }
    if (aphone == "") {
        msg += "Enter Applicant Phone";
    }
    if (dob == "") {
        msg += "Enter Date of Birth";
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
        ApplicantID: aid,
        FirstName: fname,
        LastName: lname,
        Phone: aphone,
        Email: aemail,
        Gender: agender,
        DateOfBirth:dob,
    };

    $.ajax({
        url: "/Tenant/Applicant/SaveUpdateApplicant/",
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

            getApplicantLists();
            $("#popApplicant").PopupWindow("close");
        }


    });

}
var getApplicantLists = function () {
    var model = {

        TenantID: $("#hndTenantID").val(),
    }
    $.ajax({
        url: "/Applicant/GetApplicantList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblApplicant>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.ApplicantID + ">";
                html += "<td>" + elementValue.FirstName + " " + elementValue.LastName +"</td>";
               
                html += "<td>" + elementValue.Phone + "</td>";
                html += "<td>" + elementValue.Email + "</td>";
                html += "<td>" + elementValue.Gender + "</td>";
              
                html += "</tr>";
                $("#tblApplicant>tbody").append(html);
            });

        }
    });
}
var clearApplicant = function () {
    $("#hndApplicantID").val(0);
    $("#txtApplicantFirstName").val("");
    $("#txtApplicantLastName").val("");

    $("#txtApplicantPhone").val("");
    $("#txtApplicantEmail").val("");
    $("#ddlApplicantGender").val(0);
}

var saveupdatePet = function () {

    var msg = "";
    var petId = $("#hndPetID").val();
    var petType = $("#ddlpetType").val();
    var breed = $("#txtpetBreed").val();
    var weight = $("#txtpetWeight").val();
    var age = $("#txtpetAge").val();


    if (petType === 0) {
        msg += "Select Pet Type</br>";
    }
    if (breed === "") {
        msg += "Enter Pet Breed</br>";
    }
    if (weight === "") {
        msg += "Enter Pet weight";
    }
    if (age === "") {
        msg += "Enter Pet age";
    }

    if (msg !== "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        PetID: petId,
        PetType: petType,
        Breed: breed,
        Weight: weight,
        Age: age
    };

    $.ajax({
        url: "/Tenant/Pet/SaveUpdatePet/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: 'Message!',
                content: response.Msg,
                type: 'blue'
            });

            getPetLists();
            $("#popPet").PopupWindow("close");
        }
    });
};
var getPetLists = function () {
    var model = {
        TenantID: $("#hndTenantID").val()
    };
    $.ajax({
        url: "/Pet/GetPetList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblPet>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.PetID + ">";
                if (elementValue.PetType == '1') {
                    html += "<td>Cat</td>";
                }
                else {
                    html += "<td>Dog</td>";
                }
               
                html += "<td>" + elementValue.Breed + "</td>";
                html += "<td>" + elementValue.Weight + "</td>";
                html += "<td>" + elementValue.Age + "</td>";
                html += "</tr>";
                $("#tblPet>tbody").append(html);
            });

        }
    });
};
var clearPet = function () {
    var petId = $("#hndPetID").val("0");
    var petType = $("#ddlpetType").val("0");
    var breed = $("#txtpetBreed").val("");
    var weight = $("#txtpetWeight").val("");
    var age = $("#txtpetAge").val("");
};

//Amit's Work

var saveupdateTenantOnline = function () {
    var msg = "";
    var tenantID = $("#hndTenantID").val();
    var ProspectID = $("#hdnOPId").val();
    var IsInternational = $("#ddlIsInter").val();
    var FirstName = $("#txtFirstNamePersonal").val();
    var MiddleInitial = $("#txtMiddleInitial").val();
    var LastName = $("#txtLastNamePersonal").val();
    var DateOfBirth = $("#txtDateOfBirth").val();
    var Gender = $("#ddlGender").val();
    var Email = $("#txtEmailNew").val();
    var Mobile = $("#txtMobileNumber").val();
    var PassportNumber = $("#txtPassportNum").val();
    var CountryIssuance = $("#txtCOI").val();
    var DateIssuance = $("#txtDateOfIssuance").val();
    var DateExpire = $("#txtDateOfExpiration").val();
    var IDType = $("#ddlDocumentTypePersonal").val();
    var State = $("#ddlStatePersonal").val();
    var IDNumber = $("#txtIDNumber").val();
    var Country = $("#txtCountry").val();
    var HomeAddress1 = $("#txtAddress1").val();
    var HomeAddress2 = $("#txtAddress2").val();
    var StateHome = $("#ddlStateHome").val();
    var CityHome = $("#ddlCityHome").val();
    var ZipHome = $("#txtZip").val();
    var RentOwn = $("#ddlRentOwn").val();
    var MoveInDate = $("#txtMoveInDate").val();
    var MonthlyPayment = $("#txtMonthlyPayment").val();
    var Reason = $("#txtReasonforleaving").val();
    var EmployerName = $("#txtEmployerName").val();
    var JobTitle = $("#txtJobTitle").val();
    var JobType = $("#ddlJobType").val();
    var StartDate = $("#txtStartDate").val();
    var Income = $("#txtAnnualIncome").val();
    var AdditionalIncome = $("#txtAddAnnualIncome").val();
    var SupervisorName = $("#txtSupervisiorName").val();
    var SupervisorPhone = $("#txtSupervisiorPhone").val();
    var SupervisorEmail = $("#txtSupervisiorEmail").val();
    var OfficeCountry = $("#txtCountryOffice").val();
    var OfficeAddress1 = $("#txtofficeAddress1").val();
    var OfficeAddress2 = $("#txtofficeAddress2").val();
    var OfficeState = $("#ddlStateEmployee").val();
    var OfficeCity = $("#ddlCityEmployee").val();
    var OfficeZip = $("#txtZipOffice").val();
    var Relationship = $("#txtRelationship").val();
    var EmergencyFirstName = $("#txtEmergencyFirstName").val();
    var EmergencyLastName = $("#txtEmergencyLastName").val();
    var EmergencyMobile = $("#txtEmergencyMobile").val();
    var EmergencyHomePhone = $("#txtEmergencyHomePhone").val();
    var EmergencyWorkPhone = $("#txtEmergencyWorkPhone").val();
    var EmergencyEmail = $("#txtEmergencyEmail").val();
    var EmergencyCountry = $("#txtEmergencyCountry").val();
    var EmergencyAddress1 = $("#txtEmergencyAddress1").val();
    var EmergencyAddress2 = $("#txtEmergencyAddress2").val();
    var EmergencyStateHome = $("#ddlStateContact").val();
    var EmergencyCityHome = $("#ddlCityContact").val();
    var EmergencyZipHome = $("#txtEmergencyZip").val();


    if (FirstName == "") {
        msg += "Please fill the First Name </br>";
    }
    if (LastName == "") {
        msg += "Please fill the  Last Name </br>";
    }


    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return;
    }

    var model = {
        TenantID: tenantID,
        ProspectID: ProspectID,
        IsInternational: IsInternational,
        FirstName: FirstName,
        MiddleInitial: MiddleInitial,
        LastName: LastName,
        DateOfBirth: DateOfBirth,
        Gender: Gender,
        Email: Email,
        Mobile: Mobile,
        PassportNumber: PassportNumber,
        CountryIssuance: CountryIssuance,
        DateIssuance: DateIssuance,
        DateExpire: DateExpire,
        IDType: IDType,
        State: State,
        IDNumber: IDNumber,
        Country: Country,
        HomeAddress1: HomeAddress1,
        HomeAddress2: HomeAddress2,
        StateHome: StateHome,
        CityHome: CityHome,
        ZipHome: ZipHome,
        RentOwn: RentOwn,
        MoveInDate: MoveInDate,
        MonthlyPayment: MonthlyPayment,
        Reason: Reason,
        EmployerName: EmployerName,
        JobTitle: JobTitle,
        JobType: JobType,
        StartDate: StartDate,
        Income: Income,
        AdditionalIncome: AdditionalIncome,
        SupervisorName: SupervisorName,
        SupervisorPhone: SupervisorPhone,
        SupervisorEmail: SupervisorEmail,
        OfficeCountry: OfficeCountry,
        OfficeAddress1: OfficeAddress1,
        OfficeAddress2: OfficeAddress2,
        OfficeState: OfficeState,
        OfficeCity: OfficeCity,
        OfficeZip: OfficeZip,
        Relationship: Relationship,
        EmergencyFirstName: EmergencyFirstName,
        EmergencyLastName: EmergencyLastName,
        EmergencyMobile: EmergencyMobile,
        EmergencyHomePhone: EmergencyHomePhone,
        EmergencyWorkPhone: EmergencyWorkPhone,
        EmergencyEmail: EmergencyEmail,
        EmergencyCountry: EmergencyCountry,
        EmergencyAddress1: EmergencyAddress1,
        EmergencyAddress2: EmergencyAddress2,
        EmergencyStateHome: EmergencyStateHome,
        EmergencyCityHome: EmergencyCityHome,
        EmergencyZipHome: EmergencyZipHome,

    };

    $.ajax({
        url: '/Admin/Tenant/SaveTenantOnline',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            //getApplyNowList(idmsg[0]);
            $.alert({
                title: 'Alert!',
                content: response.msg,
                type: 'blue'
            });
        }
    });
};
var getTenantOnlineData = function (id) {
    var model = {
        id: id
    };
    $.ajax({
        url: '/Admin/Tenant/getTenantOnlineData',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            setTimeout(function () {
                $("#ddlIsInter").find("option[value='" + response.model.IsInternational + "']").attr('selected', 'selected');
            }, 1500);
            $("#txtFirstNamePersonal").val(response.model.FirstName);
            $("#txtMiddleInitial").val(response.model.MiddleInitial);
            $("#txtLastNamePersonal").val(response.model.LastName);
            $("#txtDateOfBirth").val(response.model.DateOfBirthTxt);
            setTimeout(function () {
                $("#ddlGender").find("option[value='" + response.model.Gender + "']").attr('selected', 'selected');
            }, 1500);
            $("#txtEmailNew").val(response.model.Email);
            $("#txtMobileNumber").val(response.model.Mobile);
            $("#txtPassportNum").val(response.model.PassportNumber);
            $("#txtCOI").val(response.model.CountryIssuance);
            $("#txtDateOfIssuance").val(response.model.DateIssuanceTxt);
            $("#txtDateOfExpiration").val(response.model.DateExpireTxt);
            $("#ddlDocumentTypePersonal").val(response.model.IDType).change();
            setTimeout(function () {
                $("#ddlStatePersonal").find("option[value='" + response.model.State + "']").attr('selected', 'selected');
            }, 2000);
            $("#txtIDNumber").val(response.model.IDNumber);
            $("#txtCountry").val(response.model.Country);
            $("#txtAddress1").val(response.model.HomeAddress1);
            $("#txtAddress2").val(response.model.HomeAddress2);
            fillCityListHome(response.model.StateHome);
            setTimeout(function () {
                $("#ddlStateHome").find("option[value='" + response.model.StateHome + "']").attr('selected', 'selected');
            }, 2000);
            setTimeout(function () {
                $("#ddlCityHome").find("option[value='" + response.model.CityHome + "']").attr('selected', 'selected');

            }, 2500);
            $("#txtZip").val(response.model.ZipHome);
            $("#ddlRentOwn").val(response.model.RentOwn);
            $("#txtMoveInDate").val(response.model.MoveInDateTxt);
            $("#txtMonthlyPayment").val(response.model.MonthlyPayment);
            $("#txtReasonforleaving").val(response.model.Reason);
            $("#txtEmployerName").val(response.model.EmployerName);
            $("#txtJobTitle").val(response.model.JobTitle);
            $("#ddlJobType").val(response.model.JobType);
            $("#txtStartDate").val(response.model.StartDateTxt);
            $("#txtAnnualIncome").val(response.model.Income);
            $("#txtAddAnnualIncome").val(response.model.AdditionalIncome);
            $("#txtSupervisiorName").val(response.model.SupervisorName);
            $("#txtSupervisiorPhone").val(response.model.SupervisorPhone);
            $("#txtSupervisiorEmail").val(response.model.SupervisorEmail);
            $("#txtCountryOffice").val(response.model.OfficeCountry);
            $("#txtofficeAddress1").val(response.model.OfficeAddress1);
            $("#txtofficeAddress2").val(response.model.OfficeAddress2);
            fillCityListEmployee(response.model.OfficeState);
            setTimeout(function () {
                $("#ddlStateEmployee").find("option[value='" + response.model.OfficeState + "']").attr('selected', 'selected');
            }, 2000);
            setTimeout(function () {
                $("#ddlCityEmployee").find("option[value='" + response.model.OfficeCity + "']").attr('selected', 'selected');

            }, 2500);
            $("#txtZipOffice").val(response.model.OfficeZip);
            $("#txtRelationship").val(response.model.Relationship);
            $("#txtEmergencyFirstName").val(response.model.EmergencyFirstName);
            $("#txtEmergencyLastName").val(response.model.EmergencyLastName);
            $("#txtEmergencyMobile").val(response.model.EmergencyMobile);
            $("#txtEmergencyHomePhone").val(response.model.EmergencyHomePhone);
            $("#txtEmergencyWorkPhone").val(response.model.EmergencyWorkPhone);
            $("#txtEmergencyEmail").val(response.model.EmergencyEmail);
            $("#txtEmergencyCountry").val(response.model.EmergencyCountry);
            $("#txtEmergencyAddress1").val(response.model.EmergencyAddress1);
            $("#txtEmergencyAddress2").val(response.model.EmergencyAddress2);
            fillCityListContact(response.model.EmergencyStateHome);
            setTimeout(function () {
                $("#ddlStateContact").find("option[value='" + response.model.EmergencyStateHome + "']").attr('selected', 'selected');
            }, 2000);
            setTimeout(function () {
                $("#ddlCityContact").find("option[value='" + response.model.EmergencyCityHome + "']").attr('selected', 'selected');

            }, 2500);
            $("#txtEmergencyZip").val(response.model.EmergencyZipHome);
        }
    });
}