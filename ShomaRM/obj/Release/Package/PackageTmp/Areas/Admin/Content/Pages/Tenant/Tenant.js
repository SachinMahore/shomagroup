$(document).ready(function () {
    onFocus();
    $("#ddlGender").on("change", function () {
        if ($(this).val() == 3) {
            $("#txtOtherGender").attr("disabled", false);
        }
        else {
            $("#txtOtherGender").attr("disabled", true);
        }
    });

    fillStateDDL_Home();
    fillStateDDL_Office();
    fillStateDDL_EmeContact();
    fillStateDDL();
    fillCountryDropDownList();

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

    if ($("#ddlIsInter").val() == 1) {
        $("#passportDiv").removeClass("hidden");
        $("#divSSNNumber").addClass("col-sm-4 hidden");
    }
    else {
        $("#passportDiv").addClass("hidden");
        $("#divSSNNumber").removeClass("col-sm-4 hidden");
        $("#divSSNNumber").addClass("col-sm-4");
    }
    $("#ddlIsInter").on("change", function () {
        if ($(this).val() == 1) {
            $("#passportDiv").removeClass("hidden");
            $("#divSSNNumber").addClass("col-sm-4 hidden");
        }
        else {
            $("#passportDiv").addClass("hidden");
            $("#divSSNNumber").removeClass("col-sm-4 hidden");
            $("#divSSNNumber").addClass("col-sm-4");
        }
    });
    $("#ddladdHistory").on("change", function () {
        if ($(this).val() == 1) {
            $("#residence2").removeClass("hidden");
        }
        else {
            $("#txtAddress12,#txtAddress22,#txtZip2,#txtMoveInDate2,#txtMonthlyPayment2,#txtReasonforleaving2").empty();
            $("#residence2").addClass("hidden");
        }
    });
    $("#ddlDocumentTypePersonal").on("change", function () {
        var newText = $('option:selected', this).text();
        $("#lblUploadIdentity").text(newText);
    });

    $('input[type=radio]').on('ifChanged', function (event) {

        if ($("#rbtnPaystub").is(":checked")) {
            $('#divUpload3').removeClass('hidden');
            $('#lblUpload1').text('Paystub 1');
            $('#lblUpload2').text('Paystub 2');
            $('#lblUpload3').text('Paystub 3');
        }
        else if ($("#rbtnFedralTax").is(":checked")) {
            $('#divUpload3').addClass('hidden');
            $('#lblUpload1').text('Fedral Tax Return 1');
            $('#lblUpload2').text('Fedral Tax Return 2');
            $('#lblUpload3').text('Fedral Tax Return 3');
        }
    });
    if ($("#rbtnPaystub").is(":checked")) {
        $('#divUpload3').removeClass('hidden');
        $('#lblUpload1').text('Paystub 1');
        $('#lblUpload2').text('Paystub 2');
        $('#lblUpload3').text('Paystub 3');
    }

    else if ($("#rbtnFedralTax").is(":checked")) {
        $('#divUpload3').addClass('hidden');
        $('#lblUpload1').text('Fedral Tax Return 1');
        $('#lblUpload2').text('Fedral Tax Return 2');
        $('#lblUpload3').text('Fedral Tax Return 3');
    }

    var geturlId = window.location.href.split("/")[6];
    if (geturlId != "") {
        getTenantOnlineData(geturlId);
    }
    tenantOnlineID = $("#hdnOPId").val();
    // getTenantOnlineData(tenantOnlineID);
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

    //$("#ddlMaritalStatus").empty();
    //$("#ddlMaritalStatus").append("<option value='0'>Select Marital Status</option>");
    //$("#ddlMaritalStatus").append("<option value='1'>Married</option>");
    //$("#ddlMaritalStatus").append("<option value='2'>Unmarried</option>");

    //$("#ddlStudentStatus").empty();
    //$("#ddlStudentStatus").append("<option value='0'>Select Student Status</option>");

    //$("#ddlGender").empty();
    //$("#ddlGender").append("<option value='1'>Male</option>");
    //$("#ddlGender").append("<option value='2'>Female</option>");

    getTenantData($("#hndTenantID").val());

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
    getApplicantHistoryList();
    
    
});
var getTenantData = function (tenantID) {
    //alert(userID);
    var params = { TenantID: tenantID };
    var saveurl = "";
    if ($("#hndTenantID").val() == "0") {
        saveurl = "../tenant/GetTenantInfo";
    }
    else {
        saveurl = "../GetTenantInfo";
    }
    $.ajax({
        url: saveurl,
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearTenantData();
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

            }
        }
    });
}
//$("#txtWorkPhone").val(formatPhoneFax(response.OfficePhone));
//$("#txtHomePhone").val(formatPhoneFax(response.HomePhone));
var clearTenantData = function () {
    $("#hndTenantID").val("0");
    $("#txtFirstName").val("");
    $("#txtMiddleInitial").val("");
    $("#txtLastName").val("");
    $("#ddlProperty").val("0");
    $("#ddlUnit").val("0");
    $("#txtAddress").val("");
    $("#txtCity").val("");
    $("#ddlState").val("0");
    $("#txtZip").val("");
    $("#txtJobCode").val("");
    $("#txtOfficePhone").val("");
    $("#txtWorkExtension").val("");
    $("#txtWorkPhone").val("");
    $("#txtOfficeName").val("");
    $("#txtOfficeAddress").val("");
    $("#txtOfficeCity").val("");
    $("#ddlOfficeState").val("0");
    $("#txtOfficeZip").val("");
    $("#txtOccupation").val("");
    $("#txtSocialSecurityNumber").val("");
    $("#txtDrivingLicense").val("");
    $("#txtDateOfBirth").val("");
    $("#txtCarMake").val("");
    $("#txtCarModel").val("");
    $("#txtCarLicense").val("");
    $("#txtEmergencyContact").val("");
    $("#txtEmergencyPhone").val("");
    $("#txtIncome").val("");
    $("#txtEmployerContact").val("");
    $("#ddlMaritalStatus").val("0");
    $("#ddlStudentStatus").val("0");
    $("#txtRentResp").val("");
    //$("#spanSaveUpdate").text("Save");
}
var newTenant = function () {
    if ($("#hndTenantID").val() == "0") {
        window.location.href = "../../admin/tenant/new";
    }
    else {
        window.location.href = "../../tenant/new";
    }
}
var saveUpdateTenant = function () {

    var msg = "";
    if ($.trim($("#txtFirstName").val()).length <= 0) {
        msg += "First Name is required.</br>"
    }
    if ($.trim($("#txtLastName").val()).length <= 0) {
        msg += "Last Name is required.</br>"
    }
    if ($("#ddlGender").val() == 0) {
        msg += "Gender is required.</br>"
    }
    if ($.trim($("#txtDateOfBirth").val()).length <= 0) {
        msg += "Date of Birth is required.</br>"
    }
    if ($("#ddlMaritalStatus").val() == 0) {
        msg += "Marital Status is required.</br>"
    }
    if ($("#ddlState").val() == 0) {
        msg += "State is required.</br>"
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
            url: "/Admin/Tenant/SaveUpdateTenant",
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
                            content: 'Tenant Data Saved Successfully',
                            type: 'blue'
                        })
                        $("#hndTenantID").val(response.ID);
                        window.location.href = "../Tenant/Edit/" + response.ID;
                    }
                    else {
                        $("#hndTenantID").val(response.ID);
                        $.alert({
                            title: 'Alert!',
                            content: 'Tenant Data Updated Successfully',
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
var searchTenant = function () {
    window.location.href = "/../../../Admin/Tenant/";
}
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
    var model = { PID: pid }
    $.ajax({
        url: "/PropertyManagement/GetPropertyUnitList/",
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

var gotoLease = function (id) {
    window.location.href = "/Admin/LeaseManagement/Edit/" + id;
}
var goToStep = function (stepid, id) {
    if (stepid == "1") {
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step1").removeClass("hidden");
    }
    if (stepid == "2") {
        getApplicantHistoryList();
        $("#li2").addClass("active");
        $("#li1").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step2").removeClass("hidden");
    }
    if (stepid == "3") {
        getVehicleLists();
        $("#li3").addClass("active");
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step3").removeClass("hidden");
    }
    if (stepid == "4") {
        getPetLists();
        $("#li4").addClass("active");
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step4").removeClass("hidden");
    }
    if (stepid == "5") {
        $.get("../../LeaseManagement/Edit/?id=" + id, function (data) {
            $("#step5").html(data);
        });
        $("#li5").addClass("active");
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step5").removeClass("hidden");
    }
    if (stepid == "6") {
        $("#li6").addClass("active");
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li7").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step6").removeClass("hidden");
    }
    if (stepid == "7") {

        getNoticeLists();
        $("#li7").addClass("active");
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").removeClass("hidden");
    }
}

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


    //if (!validateEmail(Email)) {
    //    msg += "Invalid Email </br>";
    //}
    //if (!validateEmail(SupervisorEmail)) {
    //        msg += "Invalid Email </br>";
    //    }
    //if (!validateEmail(EmergencyEmail)) {
    //        msg += "Invalid Email </br>";
    //}
    //if (!validatePhone(Mobile)) {
    //        msg += "Invalid Phone Number </br>";
    //}
    //if (!validatePhone(Mobile)) {
    //    msg += "Invalid Phone Number </br>";
    //}
    //if (!validatePhone(SupervisorPhone)) {
    //    msg += "Invalid Phone Number </br>";
    //}
    //if (!validatePhone(EmergencyHomePhone)) {
    //    msg += "Invalid Phone Number </br>";
    //}
    //if (!validatePhone(EmergencyWorkPhone)) {
    //    msg += "Invalid Phone Number </br>";
    //}

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
            document.getElementById('hndProspectID').value = response.model.ProspectID;
            $("#ddlIsInter").val(response.model.IsInternational).change();
            $("#txtFirstNamePersonal").val(response.model.FirstName);
            $("#txtMiddleInitial").val(response.model.MiddleInitial);
            $("#txtLastNamePersonal").val(response.model.LastName);
            $("#txtDateOfBirth").val(response.model.DateOfBirthTxt);
            $("#ddlGender").val(response.model.Gender).change();
            $("#txtOtherGender").val(response.model.OtherGender);
            if (response.model.SSN != '') {
                $("#txtSSNNumber").val("***-**-" + response.model.SSN.substr(response.model.SSN.length - 5, 4));
                $("#txtSSNNumber").data("value", response.model.SSN);
                $("#hndSSNNumber").val(response.model.SSN);
            }
            else {
                $("#txtSSNNumber").val('');
            }
            $("#txtEmailNew").val(response.model.Email);
            $("#txtMobileNumber").val(formatPhoneFax(response.model.Mobile));
            $("#txtPassportNum").val(response.model.PassportNumber);
            $("#txtCOI").val(response.model.CountryIssuance);
            $("#txtDateOfIssuance").val(response.model.DateIssuanceTxt);
            $("#txtDateOfExpiration").val(response.model.DateExpireTxt);

            $("#ddlDocumentTypePersonal").val(response.model.IDType).change();
            setTimeout(function () {
                $("#ddlStatePersonal").find("option[value='" + response.model.State + "']").attr('selected', 'selected');
            }, 1500);
            if (response.model.IDNumber != '') {
                $("#txtIDNumber").val(("*".repeat(response.model.IDNumber.length - 4) + response.model.IDNumber.substr(response.model.IDNumber.length - 4, 4)));
                $("#txtIDNumber").data("value", response.model.IDNumber);
            }
            setTimeout(function () {
                $("#txtCountry").find("option[value='" + response.model.Country + "']").attr('selected', 'selected');
                $("#txtCountry2").find("option[value='" + response.model.Country2 + "']").attr('selected', 'selected');
            }, 2000);
            $("#txtAddress1").val(response.model.HomeAddress1);
            $("#txtAddress2").val(response.model.HomeAddress2);
            setTimeout(function () {
                $("#ddlStateHome").find("option[value='" + response.model.StateHome + "']").attr('selected', 'selected');
            }, 1500);
            $("#ddlCityHome").val(response.model.CityHome);
            $("#txtZip").val(response.model.ZipHome);
            $("#ddlRentOwn").val(response.model.RentOwn);
            $("#txtMoveInDateFrom").val(response.model.MoveInDateFromTxt);
            $("#txtMoveInDateTo").val(response.model.MoveInDateToTxt);
            $("#txtMonthlyPayment").val(formatMoney(response.model.MonthlyPayment));
            $("#txtReasonforleaving").val(response.model.Reason);
            $("#txtEmployerName").val(response.model.EmployerName);
            $("#txtJobTitle").val(response.model.JobTitle);
            $("#ddlJobType").val(response.model.JobType);
            $("#txtStartDate").val(response.model.StartDateTxt);
            $("#txtAnnualIncome").val(formatMoney(response.model.Income));
            $("#txtAddAnnualIncome").val(formatMoney(response.model.AdditionalIncome));
            $("#txtSupervisiorName").val(response.model.SupervisorName);
            $("#txtSupervisiorPhone").val(formatPhoneFax(response.model.SupervisorPhone));
            $("#txtSupervisiorEmail").val(response.model.SupervisorEmail);
            setTimeout(function () {
                $("#txtCountryOffice").find("option[value='" + response.model.OfficeCountry + "']").attr('selected', 'selected');
            }, 2000);
            $("#txtofficeAddress1").val(response.model.OfficeAddress1);
            $("#txtofficeAddress2").val(response.model.OfficeAddress2);
            setTimeout(function () {
                $("#ddlStateEmployee").find("option[value='" + response.model.OfficeState + "']").attr('selected', 'selected');
            }, 1500);
            $("#ddlCityEmployee").val(response.model.OfficeCity);
            $("#txtZipOffice").val(response.model.OfficeZip);
            $("#txtRelationship").val(response.model.Relationship);
            $("#txtEmergencyFirstName").val(response.model.EmergencyFirstName);
            $("#txtEmergencyLastName").val(response.model.EmergencyLastName);
            $("#txtEmergencyMobile").val(formatPhoneFax(response.model.EmergencyMobile));
            $("#txtEmergencyHomePhone").val(formatPhoneFax(response.model.EmergencyHomePhone));
            $("#txtEmergencyWorkPhone").val(formatPhoneFax(response.model.EmergencyWorkPhone));
            $("#txtEmergencyEmail").val(response.model.EmergencyEmail);
            setTimeout(function () {
                $("#txtEmergencyCountry").find("option[value='" + response.model.EmergencyCountry + "']").attr('selected', 'selected');
            }, 2000);
            $("#txtEmergencyAddress1").val(response.model.EmergencyAddress1);
            $("#txtEmergencyAddress2").val(response.model.EmergencyAddress2);
            setTimeout(function () {
                $("#ddlStateContact").find("option[value='" + response.model.EmergencyStateHome + "']").attr('selected', 'selected');
            }, 1500);
            $("#ddlCityContact").val(response.model.EmergencyCityHome);
            $("#txtEmergencyZip").val(response.model.EmergencyZipHome);
            //$("#downPassport").empty();
            //var dowPass = "<a id='downPassport' href='../../../Content/assets/img/PersonalInformation/" + response.model.PassportDocument + "' download='" + response.model.UploadOriginalPassportName + "' class='btn btn-default' target='_blank'><i class='fa fa-download fa-1x'></i>" + " " + response.model.UploadOriginalPassportName + "</a>";
            //$("#downPassport").append(dowPass);
            //$("#downIdentity").empty();
            //var dowId = "<a id='downIdentity' href='../../../Content/assets/img/PersonalInformation/" + response.model.IdentityDocument + "' download='" + response.model.UploadOriginalIdentityName + "' class='btn btn-default' target='_blank'><i class='fa fa-download fa-1x'></i>" + " " + response.model.UploadOriginalIdentityName + "</a>";
            //$("#downIdentity").append(dowId);
            //$("#downUpload1").empty();
            //var dowUp1 = "<a id='downUpload1' href='../../../Content/assets/img/PersonalInformation/" + response.model.TaxReturn + "' download='" + response.model.UploadOriginalFileName1 + "' class='btn btn-default' target='_blank'><i class='fa fa-download fa-1x'></i>" + " " + response.model.UploadOriginalFileName1 + "</a>";
            //$("#downUpload1").append(dowUp1);
            //$("#downUpload2").empty();
            //var dowUp2 = "<a href='../../../Content/assets/img/PersonalInformation/" + response.model.TaxReturn2 + "' download='" + response.model.UploadOriginalFileName2 + "' target='_blank' class='btn btn-default'><i class='fa fa-download fa-1x'></i>" + " " + response.model.UploadOriginalFileName2 + "</a>";
            //$("#downUpload2").append(dowUp2);
            //$("#downUpload3").empty();
            //var dowUp3 = "<a id='downUpload3' href='../../../Content/assets/img/PersonalInformation/" + response.model.TaxReturn3 + "' download='" + response.model.UploadOriginalFileName3 + "' class='btn btn-default' target='_blank'><i class='fa fa-download fa-1x'></i>" + " " + response.model.UploadOriginalFileName3 + "</a>";
            //$("#downUpload3").append(dowUp3);
            if (response.model.IsPaystub == true) {

                $("#rbtnPaystub").iCheck('check');
            }
            else {
                $("#rbtnFedralTax").iCheck('check');
            }
            $("#hndDontHaveVehicle").val(response.model.HaveVehicleString);
            $("#hndDontHavePet").val(response.model.HavePetString);
            if ($("#hndDontHaveVehicle").val() == 'true') {
                $("#chkDontHaveVehicle").iCheck('check');
            }
            else {
                $("#chkDontHaveVehicle").iCheck('uncheck');
            }
            if ($("#hndDontHavePet").val() == 'true') {
                $("#chkDontHavePet").iCheck('check');
            }
            else {
                $("#chkDontHavePet").iCheck('uncheck');
            }
            var resultPassport = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.PassportDocument);

            if (resultPassport == true) {
                $('#downPassport').attr('href', '/Content/assets/img/PersonalInformation/' + response.model.PassportDocument);
                $('#downPassport').attr('download', response.model.UploadOriginalPassportName);
                $('#downPassport').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalPassportName);
            } else {
                $('#downPassport').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalPassportName);
                $('#downPassport').attr('href', 'JavaScript:Void(0)');
                $('#downPassport').attr('onclick', 'fileNotExist()');
            }

            var resultIdentity = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.IdentityDocument);

            if (resultIdentity == true) {
                $('#downIdentity').attr('href', '/Content/assets/img/PersonalInformation/' + response.model.IdentityDocument);
                $('#downIdentity').attr('download', response.model.UploadOriginalIdentityName);
                $('#downIdentity').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalIdentityName);
            } else {
                $('#downIdentity').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalIdentityName);
                $('#downIdentity').attr('href', 'JavaScript:Void(0)');
                $('#downIdentity').attr('onclick', 'fileNotExist()');
            }

            var resultTaxReturn = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturn);

            if (resultTaxReturn == true) {
                $('#downUpload1').attr('href', '/Content/assets/img/PersonalInformation/' + response.model.TaxReturn);
                $('#downUpload1').attr('download', response.model.UploadOriginalFileName1);
                $('#downUpload1').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalFileName1);
            } else {
                $('#downUpload1').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalFileName1);
                $('#downUpload1').attr('href', 'JavaScript:Void(0)');
                $('#downUpload1').attr('onclick', 'fileNotExist()');
            }

            var resultTaxReturn2 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturn2);

            if (resultTaxReturn2 == true) {
                $('#downUpload2').attr('href', '/Content/assets/img/PersonalInformation/' + response.model.TaxReturn2);
                $('#downUpload2').attr('download', response.model.UploadOriginalFileName2);
                $('#downUpload2').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalFileName2);
            } else {
                $('#downUpload2').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalFileName2);
                $('#downUpload2').attr('href', 'JavaScript:Void(0)');
                $('#downUpload2').attr('onclick', 'fileNotExist()');
            }

            var resultTaxReturn3 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturn3);

            if (resultTaxReturn3 == true) {
                $('#downUpload3').attr('href', '/Content/assets/img/PersonalInformation/' + response.model.TaxReturn3);
                $('#downUpload3').attr('download', response.model.UploadOriginalFileName3);
                $('#downUpload3').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalFileName3);
            } else {
                $('#downUpload3').html("<i class='fa fa-download'></i> " + response.model.UploadOriginalFileName3);
                $('#downUpload3').attr('href', 'JavaScript:Void(0)');
                $('#downUpload3').attr('onclick', 'fileNotExist()');
            }
        }
    });
}
var fillStateDDL_Home = function (countryid) {
    var param = { CID: countryid };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlStateHome").empty();
                $("#ddlStateHome2").empty();
                $("#ddlStateHome").append("<option value='0'>--Select State--</option>");
                $("#ddlStateHome2").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStateHome").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStateHome2").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillStateDDL_Office = function (countryid) {
    var param = { CID: countryid };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlStateEmployee").empty();
                $("#ddlStateEmployee").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStateEmployee").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillStateDDL_EmeContact = function (countryid) {
    var param = { CID: countryid };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlStateContact").empty();
                $("#ddlStateContact").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStateContact").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillStateDDL = function () {
    var param = { CID: 1 };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlState").empty();
                $("#ddlStatePersonal").empty();
                $("#ddlVState").empty();
                $("#ddlState").append("<option value='0'>--Select State--</option>");
                $("#ddlStatePersonal").append("<option value='0'>--Select State--</option>");
                $("#ddlVState").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStatePersonal").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlVState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillCountryDropDownList = function () {
    $.ajax({
        url: '/City/FillCountryDropDownList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#txtCountry").empty();
                $("#txtCountry2").empty();
                $("#txtCountryOffice").empty();
                $("#txtEmergencyCountry").empty();
                //$("#txtCountry").append("<option value='0'>Select Country</option>");
                $.each(response, function (index, elementValue) {
                    $("#txtCountry").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                    $("#txtCountry2").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                    $("#txtCountryOffice").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                    $("#txtEmergencyCountry").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                });
                $("#txtCountry").val(1);
                $("#txtCountry2").val(1);
                $("#txtCountryOffice").val(1);
                $("#txtEmergencyCountry").val(1);
                fillStateDDL_Home(1);
                fillStateDDL_Office(1);
                fillStateDDL_EmeContact(1);
                //setTimeout(function () {
                //    $("#ddlStateHome").find("option[value='0']").attr('selected', 'selected');
                //    $("#ddlStateEmployee").find("option[value='0']").attr('selected', 'selected');
                //    $("#ddlStateContact").find("option[value='0']").attr('selected', 'selected');
                //}, 1500);
            }
        }
    });
}

var onFocus = function () {

    $("#txtApplicantPhone").focusout(function () { $("#txtApplicantPhone").val(formatPhoneFax($("#txtApplicantPhone").val())); })
        .focus(function () {
            $("#txtApplicantPhone").val(unformatText($("#txtApplicantPhone").val()));
        });

    $("#txtMobileNumber").focusout(function () { $("#txtMobileNumber").val(formatPhoneFax($("#txtMobileNumber").val())); })
        .focus(function () {
            $("#txtMobileNumber").val(unformatText($("#txtMobileNumber").val()));
        });

    $("#txtSupervisiorPhone").focusout(function () { $("#txtSupervisiorPhone").val(formatPhoneFax($("#txtSupervisiorPhone").val())); })
        .focus(function () {
            $("#txtSupervisiorPhone").val(unformatText($("#txtSupervisiorPhone").val()));
        });

    $("#txtEmergencyMobile").focusout(function () { $("#txtEmergencyMobile").val(formatPhoneFax($("#txtEmergencyMobile").val())); })
        .focus(function () {
            $("#txtEmergencyMobile").val(unformatText($("#txtEmergencyMobile").val()));
        });

    $("#txtEmergencyHomePhone").focusout(function () { $("#txtEmergencyHomePhone").val(formatPhoneFax($("#txtEmergencyHomePhone").val())); })
        .focus(function () {
            $("#txtEmergencyHomePhone").val(unformatText($("#txtEmergencyHomePhone").val()));
        });

    $("#txtEmergencyWorkPhone").focusout(function () { $("#txtEmergencyWorkPhone").val(formatPhoneFax($("#txtEmergencyWorkPhone").val())); })
        .focus(function () {
            $("#txtEmergencyWorkPhone").val(unformatText($("#txtEmergencyWorkPhone").val()));
        });

    $("#txtMonthlyPayment").focusout(function () { $("#txtMonthlyPayment").val(formatMoney($("#txtMonthlyPayment").val())); })
        .focus(function () {
            $("#txtMonthlyPayment").val(unformatText($("#txtMonthlyPayment").val()));
        });

    $("#txtMonthlyPayment2").focusout(function () { $("#txtMonthlyPayment2").val(formatMoney($("#txtMonthlyPayment2").val())); })
        .focus(function () {
            $("#txtMonthlyPayment2").val(unformatText($("#txtMonthlyPayment2").val()));
        });

    $("#txtAnnualIncome").focusout(function () { $("#txtAnnualIncome").val(formatMoney($("#txtAnnualIncome").val())); })
        .focus(function () {
            $("#txtAnnualIncome").val(unformatText($("#txtAnnualIncome").val()));
        });

    $("#txtAddAnnualIncome").focusout(function () { $("#txtAddAnnualIncome").val(formatMoney($("#txtAddAnnualIncome").val())); })
        .focus(function () {
            $("#txtAddAnnualIncome").val(unformatText($("#txtAddAnnualIncome").val()));
        });

    $("#txtSSNNumber").focusout(function () {
        var ssn = $(this).val();
        $(this).data("value", ssn);
        if (ssn.length > 4) {
            $(this).val("***-**-" + ssn.substr(ssn.length - 5, 4))
        }
    })
        .focus(function () {
            $(this).val($(this).data("value"));
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

function formatSSN(ssn) {
    if (ssn == null)
        ssn = "";
    ssn = ssn.replace(/[^0-9]/g, '');
    if (ssn.length == 0)
        return ssn;

    return ssn.substring(0, 3) + (ssn.length > 3 ? '-' : '') + ssn.substring(3, 5) + (ssn.length > 5 ? '-' : '') + ssn.substring(5);
}

$(function () {
    $("#txtIDNumber").blur(function () {
        cCardNum = $(this).val();
        $(this).data("value", cCardNum);
        if (cCardNum.length > 4) {
            $(this).val(("*".repeat(cCardNum.length - 4) + cCardNum.substr(cCardNum.length - 4, 4)))
        }
    }).focus(function () {
        $(this).val($(this).data("value"));
    });

});

var getApplicantHistoryList = function () {

    var model = {
        TenantID: $("#hndProspectID").val()
        //TenantID: '65'
    };
    $.ajax({
        url: "/ApplyNow/GetApplicantHistoryList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            //console.log(JSON.stringify(response))
            $("#tblAHR>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.AHID + "' data-value='" + elementValue.AHID + "'>";
                html += "<td>" + elementValue.Country + "</td>";
                html += "<td>" + elementValue.HomeAddress1 + "</td>";
                html += "<td>" + elementValue.HomeAddress2 + "</td>";
                html += "<td>" + elementValue.StateHomeTxt + "</td>";
                html += "<td>" + elementValue.CityHome + "</td>";
                html += "<td>" + elementValue.ZipHome + "</td>";

                html += "<td>" + elementValue.MoveInDateFromTxt + "</td>";
                html += "<td>" + elementValue.MoveInDateToTxt + "</td>";
                html += "<td>" + elementValue.MonthlyPayment + "</td>";

                html += "<td class='text-center'>";
                html += "<button style='background: transparent;' id='updateAHRInfo' onclick='getApplicantHistoryInfo(" + elementValue.AHID + ")'><span class='fa fa-eye' ></span></button>";
                html += "</tr>";
                $("#tblAHR>tbody").append(html);
            });
        }
    });
};

var getVehicleLists = function () {
    var model = {

        TenantID: $("#hndProspectID").val(),
        //TenantID:'105'
    }
    $.ajax({
        url: "/Tenant/Vehicle/GetVehicleList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblVehicle>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.Vehicle_ID + "' data-value='" + elementValue.Vehicle_ID + "'>";
                html += "<td>" + elementValue.OwnerName + "</td>";
                html += "<td>" + elementValue.Make + "</td>";
                html += "<td>" + elementValue.VModel + "</td>";
                html += "<td>" + elementValue.Year + "</td>";
                html += "<td>" + elementValue.Color + "</td>";
                html += "<td>" + elementValue.License + "</td>";
                html += "<td class='text-center'><button style='background: transparent;' onclick='getVehicleInfo(" + elementValue.Vehicle_ID + ")'><span class='fa fa-eye'></span></button>";

                html += "</tr>";
                $("#tblVehicle>tbody").append(html);
            });

        }
    });
}

var getPetLists = function () {
    var model = {
        TenantID: $("#hndProspectID").val()
        //TenantID:'105'
    };
    $.ajax({
        url: "/Tenant/Pet/GetPetList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#tblPet>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.PetID + "' data-value='" + elementValue.PetID + "'>";
                html += "<td align='center'><img src='/content/assets/img/pet/" + elementValue.Photo + "' class='picture-src' title='' style='height:70px;width:70px;'/></td>";
                html += "<td>" + elementValue.PetName + "</td>";
                html += "<td>" + elementValue.Breed + "</td>";
                html += "<td>" + elementValue.Weight + "</td>";
                html += "<td>" + elementValue.VetsName + "</td>";
                html += "<td class='text-center'>";
                html += "<button style='background: transparent;' id='updatePetInfo' onclick='getPetInfo(" + elementValue.PetID + ")'><span class='fa fa-eye'></span></button>";
                html += "</td >";
                html += "</tr>";
                $("#tblPet>tbody").append(html);
            });
        }
    });
};

var getApplicantHistoryInfo = function (id) {
    var model = {
        AHID: id
    };
    $.ajax({
        url: '/ApplyNow/GetApplicantHistoryDetails/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#popApplicantHistory").modal("show");
            $("#hdnAHRId").val(response.model.AHID);
            $("#txtCountry2").find("option[value='" + response.model.Country2 + "']").attr('selected', 'selected');

            $("#txtAddress12").val(response.model.HomeAddress1);
            $("#txtAddress22").val(response.model.HomeAddress2);
            $("#ddlStateHome2").find("option[value='" + response.model.StateHome + "']").attr('selected', 'selected');
            $("#ddlCityHome2").val(response.model.CityHome);
            $("#txtZip2").val(response.model.ZipHome);
            $("#ddlRentOwn2").val(response.model.RentOwn);
            $("#txtMoveInDateFrom2").val(response.model.MoveInDateFromTxt);
            $("#txtMoveInDateTo2").val(response.model.MoveInDateToTxt);
            $("#txtMonthlyPayment2").val(response.model.MonthlyPayment);
            $("#txtReasonforleaving2").val(response.model.Reason);
        }
    });
};

var getPetInfo = function (id) {
    $("#popPet").modal("show");
    var model = {
        id: id
    };
    $.ajax({
        url: '/Tenant/Pet/GetPetDetails/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#hndPetID").val(response.model.PetID);
            $("#txtpetName").val(response.model.PetName);
            $("#txtpetVetsName").val(response.model.VetsName);
            $("#ddlpetType").val(response.model.PetType);
            $("#txtpetBreed").val(response.model.Breed);
            $("#txtpetWeight").val(response.model.Weight);
            $("#txtpetAge").val(response.model.Age);
            $("#hndPetPicture").val(response.model.Photo);
            $("#hndOriginalPetPicture").val(response.model.OriginalPetNameFile);
            $("#hndPetVaccinationCertificate").val(response.model.PetVaccinationCertificate);
            $("#hndOriginalPetVaccinationCertificate").val(response.model.OriginalPetVaccinationCertificateFile);
            if (response.model.OriginalPetVaccinationCertificateFile != '') {
                $("#filePetPictireShow").text(response.model.OriginalPetNameFile);
            }
            if (response.model.OriginalPetVaccinationCertificateFile != '') {
                $("#filePetVaccinationCertificateShow").text(response.model.OriginalPetVaccinationCertificateFile);
            }
            //$('#dwnldPetPhoto').attr('href', '/Content/assets/img/pet/' + response.model.Photo);
            //$('#dwnldPetPhoto').attr('download', response.model.OriginalPetNameFile);
            //$('#dwnldPetPhoto').html("<i class='fa fa-download'></i> " + response.model.OriginalPetNameFile);
            //$('#dwnldVaccinationCertificate').attr('href', '/Content/assets/img/pet/' + response.model.PetVaccinationCertificate);
            //$('#dwnldVaccinationCertificate').attr('download', response.model.OriginalPetVaccinationCertificateFile);
            //$('#dwnldVaccinationCertificate').html("<i class='fa fa-download'></i> " + response.model.OriginalPetVaccinationCertificateFile);

            var result = doesFileExist('/Content/assets/img/pet/' + response.model.Photo);

            if (result == true) {
                $('#dwnldPetPhoto').attr('href', '/Content/assets/img/pet/' + response.model.Photo);
                $('#dwnldPetPhoto').attr('download', response.model.OriginalPetNameFile);
                $('#dwnldPetPhoto').html("<i class='fa fa-download'></i> " + response.model.OriginalPetNameFile);
            } else {
                $('#dwnldPetPhoto').html("<i class='fa fa-download'></i> " + response.model.OriginalPetNameFile);
                $('#dwnldPetPhoto').attr('href', 'JavaScript:Void(0)');
                $('#dwnldPetPhoto').attr('onclick', 'fileNotExist()');
            }

            var result1 = doesFileExist('/Content/assets/img/pet/' + response.model.PetVaccinationCertificate);

            if (result1 == true) {
                $('#dwnldVaccinationCertificate').attr('href', '/Content/assets/img/pet/' + response.model.PetVaccinationCertificate);
                $('#dwnldVaccinationCertificate').attr('download', response.model.OriginalPetVaccinationCertificateFile);
                $('#dwnldVaccinationCertificate').html("<i class='fa fa-download'></i> " + response.model.OriginalPetVaccinationCertificateFile);
            } else {
                $('#dwnldVaccinationCertificate').html("<i class='fa fa-download'></i> " + response.model.OriginalPetVaccinationCertificateFile);
                $('#dwnldVaccinationCertificate').attr('href', 'JavaScript:Void(0)');
                $('#dwnldVaccinationCertificate').attr('onclick', 'fileNotExist()');
            }
        }
    });
};

var getVehicleInfo = function (id) {
    $("#popVehicle").modal("show");
    var model = {
        VehicleId: id
    };
    $.ajax({
        url: '/Tenant/Vehicle/GetVehicleInfo/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#hndVehicleID").val(response.model.Vehicle_ID);
            $("#txtVehicleOwnerName").val(response.model.OwnerName);
            $("#txtVehicleMake").val(response.model.Make);
            $("#txtVehicleModel").val(response.model.VModel);
            $("#txtVehicleColor").val(response.model.Color);
            $("#txtVehicleLicence").val(response.model.License);
            $("#txtVehicleNote").val(response.model.Notes);
            setTimeout(function () {
                $("#ddlVehicleyear").find("option[value='" + response.model.Year + "']").attr('selected', 'selected');
            }, 1500);
            setTimeout(function () {
                $("#ddlVState").find("option[value='" + response.model.State + "']").attr('selected', 'selected');
            }, 1500);

            var result = doesFileExist('/Content/assets/img/VehicleRegistration/' + response.model.VehicleRegistration);

            if (result == true) {
                $('#dwnldVehicleRegistration').attr('href', '/Content/assets/img/VehicleRegistration/' + response.model.VehicleRegistration);
                $('#dwnldVehicleRegistration').attr('download', response.model.OriginalVehicleRegistation);
                $('#dwnldVehicleRegistration').html("<i class='fa fa-download'></i> " + response.model.OriginalVehicleRegistation);
            } else {
                $('#dwnldVehicleRegistration').html("<i class='fa fa-download'></i> " + response.model.OriginalVehicleRegistation);
                $('#dwnldVehicleRegistration').attr('href', 'JavaScript:Void(0)');
                $('#dwnldVehicleRegistration').attr('onclick', 'fileNotExist()');
            }

            //$('#dwnldVehicleRegistration').attr('href', '/Content/assets/img/VehicleRegistration/' + response.model.VehicleRegistration);
            //$('#dwnldVehicleRegistration').attr('download', response.model.OriginalVehicleRegistation);
            //$('#dwnldVehicleRegistration').html("<i class='fa fa-download'></i> " + response.model.OriginalVehicleRegistation);
        }
    });
};

var fileNotExist = function () {
    $.alert({
        title: "Alert!",
        content: 'File Not Found',
        type: 'red'
    })
}