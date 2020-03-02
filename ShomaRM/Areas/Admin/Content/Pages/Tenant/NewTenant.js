
$(document).ready(function () {
    
    var newTenantID = localStorage.getItem("NewTenantID");
    $("#hndNewTenant").val(newTenantID);
    
    getTenantData(newTenantID); 
    GetTenantDetails(newTenantID);

    serviceRequestChangeDDL()
    getLeaseInfoDocuments();
    getPetLeaseInfoDocuments();
    getVehicleLeaseInfoDocuments();

});

var getAccountHistory = function () {
    $("#divLoader").show();

    var model = {
        TenantId: $("#hndNewTenant").val()
    };
    $.ajax({
        url: '/Transaction/getAccountHistory',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#tblAccountHistory>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.TransID + ">";
                html += "<td>" + elementValue.TransactionDateString + "</td>";
                html += "<td>" + elementValue.Description + "</td>";

                html += "<td> $" + formatMoney(elementValue.Charge_Amount) + "</td>";

                html += "</tr>";
                $("#tblAccountHistory>tbody").append(html);
            });
            $("#divLoader").hide();
        }
    });
}

var getServiceRequestList = function () {
    $("#divLoader").show();
    var tenantId = $("#hndNewTenant").val();
    var serviceReq = 4;
    var model = {
        TenantId: tenantId,
        ServiceRequest: serviceReq
    };
    $.ajax({
        url: '/ServiceRequest/GetServiceRequestListAdmin',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#tblServiceRequest>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.ServiceID + ">";
                html += "<td>" + elementValue.DateString + "</td>";
                html += "<td>" + elementValue.ProblemCategorystring + "</td>";
                html += "<td>" + elementValue.StatusString + "</td>";
                html += "<td>" + elementValue.PriorityString + "</td>";
                html += "</tr>";
                $("#tblServiceRequest>tbody").append(html);
            });

            $("#divLoader").hide();
        }
    });
};

var getReservationRequestList = function () {
    $("#divLoader").show();
    
    var tenantID = $("#hndNewTenant").val();
    var ProspectID = $("#hndUserId").val();
    //alert(tenantID + " " + ProspectID);
    var model = {
        TenantID: tenantID
    };
    $.ajax({
        url: '/Tenant/MyAccount/FillRRList',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblReservationRequest>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    //console.log(JSON.stringify(response));
                    var html = "<tr data-value=" + elementValue.ARID + " data-amenity=" + elementValue.AmenityID + " data-tenantid=" + elementValue.TenantID + ">";
                    html += "<td>" + elementValue.TenantName + "</td>";
                    html += "<td>" + elementValue.AmenityName + "</td>";
                    html += "<td>" + elementValue.DesiredDate + "</td>";
                    html += "<td>" + elementValue.DesiredTimeFrom + "</td>";
                    html += "<td>" + elementValue.DesiredTimeTo + "</td>";
                    html += "<td>" + elementValue.Duration + " hours</td>";
                    html += "<td>" + elementValue.Guest + "</td>";
                    html += "<td>" + elementValue.Status + "</td>";

                    html += "</tr>";
                    $("#tblReservationRequest>tbody").append(html);
                });

                $("#divLoader").hide();
            }
        }
    });
};

var getLeaseInfoDocuments = function () {
    $("#divLoader").show();
    var model = { UserId: $("#hndNewProspectID").val() };
   
    $.ajax({
        url: '/MyAccount/GetTenantLeaseDocuments',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            var intCount = parseInt(1);

            //For Lease Documents
            $('#accordionSubLeaseDocument').empty();
            var Ldhtml = '';
            
            if (response.model.EnvelopeID != null) {
                intCount++;
                var resultLease = doesFileExist('/Content/assets/img/Document/' + response.model.EnvelopeID);
                if (resultLease == true) {
                    Ldhtml += "<a href='/Content/assets/img/Document/" + response.model.EnvelopeID + "' download='" + response.model.EnvelopeID + "'>" + response.model.EnvelopeID + "</a></br>";
                }
                else {
                    Ldhtml += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + response.model.EnvelopeID + "</a></br>";
                }
            }
            $('#accordionSubLeaseDocument').append(Ldhtml);

            //For Identity
            $('#accordionSubIdentity').empty();
            var Html = '';
            if (response.model.PassportDoc != null) {
                if (response.model.PassportDoc != '0') {
                    intCount++;
                    var resultPass = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.PassportDoc);
                    if (resultPass == true) {
                        Html += "<a href='/Content/assets/img/PersonalInformation/" + response.model.PassportDoc + "' download='" + response.model.OriginalPassportDoc + "'>" + response.model.OriginalPassportDoc + "</a></br>";
                    }
                    else {
                        Html += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + response.model.OriginalPassportDoc + "</a></br>";
                    }
                }
            }

            if (response.model.IdentityDoc != null) {
                if (response.model.IdentityDoc != '0') {
                    intCount++;
                    var resultIdent = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.IdentityDoc);
                    if (resultIdent == true) {
                        Html += "<a href='/Content/assets/img/PersonalInformation/" + response.model.IdentityDoc + "' download='" + response.model.OriginalIdentityDoc + "'>" + response.model.OriginalIdentityDoc + "</a></br>";
                    }
                    else {
                        Html += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + response.model.OriginalIdentityDoc + "</a></br>";
                    }
                }
            }
            $('#accordionSubIdentity').append(Html);

            //For Tax Return
            $('#accordionSubTaxReturn').empty();
            var Thtml = '';
            if (response.model.TaxReturnDoc1 != null) {
                if (response.model.TaxReturnDoc1 != '0') {
                    intCount++;
                    var resultTax1 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturnDoc1);
                    if (resultTax1 == true) {
                        Thtml += "<a href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc1 + "' download='" + response.model.OriginalTaxReturnDoc1 + "'>" + response.model.OriginalTaxReturnDoc1 + "</a></br>";
                    }
                    else {
                        Thtml += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + response.model.OriginalTaxReturnDoc1 + "</a></br>";
                    }
                }
            }

            if (response.model.TaxReturnDoc2 != null) {
                if (response.model.TaxReturnDoc2 != '0') {
                    intCount++;
                    var resultTax2 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturnDoc2);
                    if (resultTax2 == true) {
                        Thtml += "<a href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc2 + "' download='" + response.model.OriginalTaxReturnDoc2 + "'>" + response.model.OriginalTaxReturnDoc2 + "</a></br>";
                    }
                    else {
                        Thtml += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + response.model.OriginalTaxReturnDoc2 + "</a></br>";
                    }
                }
            }

            if (response.model.TaxReturnDoc3 != null) {
                if (response.model.TaxReturnDoc3 != '0') {
                    intCount++;
                    var resultTax3 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturnDoc3);
                    if (resultTax3 == true) {
                        Thtml += "<a href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc3 + "' download='" + response.model.OriginalTaxReturnDoc3 + "'>" + response.model.OriginalTaxReturnDoc3 + "</a></br>";
                    }
                    else {
                        Thtml += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + response.model.OriginalTaxReturnDoc3 + "</a></br>";
                    }
                }
            }

            $('#accordionSubTaxReturn').append(Thtml);
            $("#divLoader").hide();

        }
    });
};

var getPetLeaseInfoDocuments = function () {
    $("#divLoader").show();
    var model = { UserId: $("#hndNewProspectID").val() };

    $.ajax({
        url: '/MyAccount/GetTenantPetLeaseDocuments',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            var intCount = parseInt(1);
            $('#accordionSubPetCertificate').empty();
            $.each(response.model, function (elementType, elementValue) {
                var Phtml = '';
                intCount++;
                var resultPet = doesFileExist('/Content/assets/img/pet/' + elementValue.PetVaccinationDoc);
                if (resultPet == true) {
                    Phtml += "<a href='/Content/assets/img/pet/" + elementValue.PetVaccinationDoc + "' download='" + elementValue.OriginalPetVaccinationDoc + "'>" + elementValue.OriginalPetVaccinationDoc + "</a></br>";
                }
                else {
                    Phtml += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + elementValue.OriginalPetVaccinationDoc + "</a></br>";
                }

                $('#accordionSubPetCertificate').append(Phtml);
            });
            $("#divLoader").hide();
        }
    });
};

var getVehicleLeaseInfoDocuments = function () {
    $("#divLoader").show();
    var model = { UserId: $("#hndNewProspectID").val() };

    $.ajax({
        url: '/MyAccount/GetTenantVehicleLeaseDocuments',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            var intCount = parseInt(1);
            $('#accordionSubVehicleCertificate').empty();
            $.each(response.model, function (elementType, elementValue) {
                var Vhtml = '';
                intCount++;
                var resultVehicle = doesFileExist('/Content/assets/img/VehicleRegistration/' + elementValue.VehicleRegistrationDoc);
                if (resultVehicle == true) {
                    Vhtml += "<a href='/Content/assets/img/VehicleRegistration/" + elementValue.VehicleRegistrationDoc + "' download='" + elementValue.OriginalVehicleRegistrationDoc + "'>" + elementValue.OriginalVehicleRegistrationDoc + "</a></br>";
                }
                else {
                    Vhtml += "<a href='javascript:void(0)' onclick='FileNotFound();'>" + elementValue.OriginalVehicleRegistrationDoc + "</a></br>";
                }

                $('#accordionSubVehicleCertificate').append(Vhtml);
            });
            $("#divLoader").hide();

        }
    });
};

var goToStep = function (stepid, id) {


    if (stepid == "8") {
       // getServiceInfo();
        getAccountHistory();
        $("#li8").addClass("active");
        $("#step8").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");

        $("#li9").removeClass("active");
        $("#step9").addClass("hidden");
        $("#li10").removeClass("active");
        $("#step10").addClass("hidden");
        $("#li11").removeClass("active");
        $("#step11").addClass("hidden");
        $("#li12").removeClass("active");
        $("#step12").addClass("hidden");
        $("#li13").removeClass("active");
        $("#step13").addClass("hidden");
        $("#li14").removeClass("active");
        $("#step14").addClass("hidden");

    }

    if (stepid == "9") {
        getServiceRequestList();

        $("#li9").addClass("active");
        $("#step9").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");

        $("#li8").removeClass("active");
        $("#step8").addClass("hidden");
        $("#li10").removeClass("active");
        $("#step10").addClass("hidden");
        $("#li11").removeClass("active");
        $("#step11").addClass("hidden");
        $("#li12").removeClass("active");
        $("#step12").addClass("hidden");
        $("#li13").removeClass("active");
        $("#step13").addClass("hidden");
        $("#li14").removeClass("active");
        $("#step14").addClass("hidden");
    }
    if (stepid == "10") {
        getReservationRequestList();
        
       

        $("#li10").addClass("active");
        $("#step10").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");

        $("#li8").removeClass("active");
        $("#step8").addClass("hidden");
        $("#li9").removeClass("active");
        $("#step9").addClass("hidden");
        $("#li11").removeClass("active");
        $("#step11").addClass("hidden");
        $("#li12").removeClass("active");
        $("#step12").addClass("hidden");
        $("#li13").removeClass("active");
        $("#step13").addClass("hidden");
        $("#li14").removeClass("active");
        $("#step14").addClass("hidden");
    }
    if (stepid == "11") {
       
        getLeaseInfoDocuments();
        getPetLeaseInfoDocuments();
        getVehicleLeaseInfoDocuments();
        $("#li11").addClass("active");
        $("#step11").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");
        $("#li8").removeClass("active");
        $("#step8").addClass("hidden");
        $("#li9").removeClass("active");
        $("#step9").addClass("hidden");
        $("#li10").removeClass("active");
        $("#step10").addClass("hidden");

        $("#li12").removeClass("active");
        $("#step12").addClass("hidden");
        $("#li13").removeClass("active");
        $("#step13").addClass("hidden");
        $("#li14").removeClass("active");
        $("#step14").addClass("hidden");
    }
    if (stepid == "12") {
        getGuestRegistrationList();
        $("#li12").addClass("active");
        $("#step12").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");
        $("#li8").removeClass("active");
        $("#step8").addClass("hidden");
        $("#li9").removeClass("active");
        $("#step9").addClass("hidden");
        $("#li10").removeClass("active");
        $("#step10").addClass("hidden");
        $("#li11").removeClass("active");
        $("#step11").addClass("hidden");

        $("#li13").removeClass("active");
        $("#step13").addClass("hidden");
        $("#li14").removeClass("active");
        $("#step14").addClass("hidden");
    }
    if (stepid == "13") {
        $("#li13").addClass("active");
        $("#step13").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");
        $("#li8").removeClass("active");
        $("#step8").addClass("hidden");
        $("#li9").removeClass("active");
        $("#step9").addClass("hidden");
        $("#li10").removeClass("active");
        $("#step10").addClass("hidden");
        $("#li11").removeClass("active");
        $("#step11").addClass("hidden");
        $("#li12").removeClass("active");
        $("#step12").addClass("hidden");
        $("#li14").removeClass("active");
        $("#step14").addClass("hidden");
    }

    if (stepid == "14") {
        getCommunityActivityList();
        $("#li14").addClass("active");
        $("#step14").removeClass("hidden");

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
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#step7").addClass("hidden");
        $("#li8").removeClass("active");
        $("#step8").addClass("hidden");
        $("#li9").removeClass("active");
        $("#step9").addClass("hidden");
        $("#li10").removeClass("active");
        $("#step10").addClass("hidden");
        $("#li11").removeClass("active");
        $("#step11").addClass("hidden");
        $("#li12").removeClass("active");
        $("#step12").addClass("hidden");
        $("#li13").removeClass("active");
        $("#step13").addClass("hidden");
    }

};

var getGuestRegistrationList = function () {
    $("#divLoader").show();
    var model = {
        TenantId: $("#hndNewTenant").val()
    };
    $.ajax({
        url: '/GuestManagement/GetGuestList',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#tblGuestRegistration>tbody").empty();

            $.each(response.model, function (index, elementValue) {
                var guestIdTag = '';
                var guestVehicleRegTag = '';
               
                var isIdentityFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + elementValue.DriverLicence);
                if (isIdentityFileExist) {
                    guestIdTag = '<a href="/Content/assets/img/TenantGuestInformation/' + elementValue.DriverLicence + '" target="_blank">' + elementValue.OriginalDriverLicence + '</a>';
                    if (elementValue.DriverLicence == '0') {
                        guestIdTag = 'File Not Uploaded';
                    }
                  
                }
                else {
                    guestIdTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();">' + elementValue.OriginalDriverLicence + '</a>';
                    if (elementValue.DriverLicence == '0') {
                        guestIdTag = 'File Not Uploaded';
                    }
                }
                var isVehicleRegFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + elementValue.VehicleRegistration);
                if (isVehicleRegFileExist) {
                    guestVehicleRegTag = '<a href="/Content/assets/img/TenantGuestInformation/' + elementValue.VehicleRegistration + '" target="_blank">' + elementValue.OriginalVehicleRegistration + '</a>';
                    if (elementValue.VehicleRegistration == '0') {
                        guestVehicleRegTag = 'File Not Uploaded';
                    }
                }
                else {
                    guestVehicleRegTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();">' + elementValue.OriginalVehicleRegistration + '</a>';
                    if (elementValue.VehicleRegistration == '0') {
                        guestVehicleRegTag = 'File Not Uploaded';
                    }
                }
                var html = '';
                html += '<tr data-value="' + elementValue.GuestID + '">';
                html += '<td style="color:#3d3939;">' + elementValue.FirstName + ' ' + elementValue.LastName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.TenantName + '</td>';
                html += '<td style="color:#3d3939;">' + formatPhoneFax(elementValue.Phone) + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Email + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.VisitStartDateString + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.VisitEndDateString + '</td>';
                html += '<td style="color:#3d3939;">' + guestIdTag + '</td>';
                html += '<td style="color:#3d3939;">' + guestVehicleRegTag + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.StatusString + '</td>';
                //html += '<td style="color:#3d3939;"><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delGuestRegistration(' + elementValue.GuestID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                html += '</tr>';
                $("#tblGuestRegistration>tbody").append(html);
            });
        }
    });
    $("#divLoader").hide();
};


//My profile
var getTenantData = function (userID) {
    $("#divLoader").show();
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
                $("#hndNewTenant").val(response.ID);
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

                if ($("#hndNewTenant").val() != "0") {
                    $("#spanSaveUpdate").text("Update");
                }
                else {
                    $("#spanSaveUpdate").text("Save");
                }

            }
            $("#divLoader").hide();
        }
    });
};

var GetTenantDetails = function (userID) {
    $("#divLoader").show();

    var params = { TenantID: userID };
    $.ajax({
        url: "/MyAccount/GetTenantDetails",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {

            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndNewTenant").val(response.ID);
                $("#hndProspectID").val(response.ProspectID);
                $("#hndNewProspectID").val(response.ProspectID);
                $("#FirstName").text(response.FirstName + " " + response.LastName);
                $("#Mobile").text(response.Mobile);
                $("#Email").text(response.Email);

                $("#FirstName1").text(response.FirstName + " " + response.LastName);
                $("#Mobile1").text(formatPhoneFax(response.Mobile));
                $("#Email1").text(response.Email);

                $("#EmFirstName").text(response.EmFirstNane + " " + response.EmLastName);
                $("#EmRelationship").text(response.EmRelation);
                $("#EmAddress").text(response.EmergencyAddress1);
                $("#EmMobile").text(formatPhoneFax(response.EmMobile));
                $("#EmWorkPhone").text(formatPhoneFax(response.EmWorkPhone));
                $("#EmEmail").text(response.EmEmail);
                $("#EmAddress").text(response.EmAddress1 + "" + response.EmAddress1);

                $("#txtFirstName").val(response.FirstName);
                $("#txtLastName").val(response.LastName);
                $("#txtMobile").val(formatPhoneFax(response.Mobile));
                $("#txtEmail").val(response.Email);
                $("#txtMiddleName").val(response.MiddleInitial);


                $("#txtEmFirstName").val(response.EmFirstNane);
                $("#txtEmLastName").val(response.EmLastName);
                $("#txtEmRelationship").val(response.EmRelation);
                $("#txtEmergencyAddress1").val(response.EmAddress1);
                $("#txtEmergencyAddress2").val(response.EmAddress2);
                $("#txtEmMobile").val(response.EmMobile);
                $("#txtEmHomePhone").val(response.EmHomePhone);
                $("#txtEmWorkPhone").val(response.EmWorkPhone);
                $("#txtEmEmail").val(response.EmEmail);

                $("#EmployerName").text(response.EmployerName);
                $("#JobTitle").text(response.JobTitle);
                $("#JobType").text(response.JobTypeString);
                $("#txtEmployerName").val(response.EmployerName);
                $("#txtJobTitle").val(response.JobTitle);
                $('#imgTenantProfilePicture').attr('src');
                if (response.TempProfilePic != null) {
                    var fileExist = doesFileExist('/Content/assets/img/tenantProfile/' + response.TempProfilePic);
                    if (fileExist) {
                        $('#imgTenantProfilePicture').attr('src', '/Content/assets/img/tenantProfile/' + response.TempProfilePic);
                    }
                    else {
                        $('#imgTenantProfilePicture').attr('src', '/Content/assets/img/circle.png');
                    }
                }
                else {
                    $('#imgTenantProfilePicture').attr('src', '/Content/assets/img/circle.png');
                }
                setTimeout(function () {
                    $("#ddlJobType").find("option[value='" + response.JobType + "']").attr('selected', 'selected');
                }, 1500);

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

                if ($("#hndNewTenant").val() != "0") {
                    $("#spanSaveUpdate").text("Update");
                }
                else {
                    $("#spanSaveUpdate").text("Save");
                }
            }
            getVehicleLists();
            getPetLists();
            $("#divLoader").hide();
        }
    });
};

//Vehicle

var getVehicleLists = function () {
    $("#divLoader").show();
    var model = {
        TenantID: $("#hndProspectID").val(),
    };
    $.ajax({
        url: "/Tenant/Vehicle/GetVehicleList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblVehicle>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var resultVehicle = doesFileExist('../../Content/assets/img/VehicleRegistration/' + elementValue.VehicleRegistration);
                var html = "<tr id='tr_" + elementValue.Vehicle_ID + "' data-value='" + elementValue.Vehicle_ID + "'>";
                html += "<td>" + elementValue.OwnerName + "</td>";
                html += "<td>" + elementValue.Make + "</td>";
                html += "<td>" + elementValue.VModel + "</td>";
                html += "<td>" + elementValue.Year + "</td>";
                html += "<td>" + elementValue.Color + "</td>";
                html += "<td>" + elementValue.License + "</td>";
                if (resultVehicle == true) {
                    html += "<td><a style='background: transparent;' href='../../../Content/assets/img/VehicleRegistration/" + elementValue.VehicleRegistration + "' download=" + elementValue.VehicleRegistration + " target='_blank'><span class='fa fa-download' style='background: transparent;'></span></a></td>";
                }
                else {
                    html += "<td><a style='background: transparent;' href='javascript:void(0)' onclick='fileDoesNotExist()'><span class='fa fa-download' style='background: transparent;'></span></a></td>";
                }

                html += "</tr>";
                $("#tblVehicle>tbody").append(html);
            });

            $("#divLoader").hide();

        }
    });
};

//Pet

var getPetLists = function () {
    $("#divLoader").show();
    var model = {
        TenantID: $("#hndProspectID").val()
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
                var resultPet = doesFileExist('../../Content/assets/img/pet/' + elementValue.PetVaccinationCertificate);
                var html = "<tr id='tr_" + elementValue.PetID + "' data-value='" + elementValue.PetID + "'>";
                //html += "<td align='center'><img src='/content/assets/img/pet/" + elementValue.Photo + "' class='picture-src' title='' style='height:70px;width:70px;'/></td>";

                html += "<td>" + elementValue.PetName + "</td>";
                html += "<td>" + elementValue.Breed + "</td>";
                html += "<td>" + elementValue.Weight + "</td>";
                html += "<td>" + elementValue.VetsName + "</td>";
                if (resultPet == true) {
                    html += "<td><a href='../../Content/assets/img/pet/" + elementValue.PetVaccinationCertificate + "' download=" + elementValue.PetVaccinationCertificate + " target='_blank'><span class='fa fa-download'></span></a></td>";
                }
                else {
                    html += "<td><a href='javascript:void(0)' onclick='fileDoesNotExist()'><span class='fa fa-download'></span></a></td>";
                }
                html += "</tr>";
                $("#tblPet>tbody").append(html);
            });

            $("#divLoader").hide();
        }
    });
};


//Community Activity

var getCommunityActivityList = function () {
    
   // alert($('#hndNewTenant').val());
    $("#divLoader").show();
    var model = { TenantId: $('#hndNewTenant').val() };

    $.ajax({
        url: '/Tenant/CommunityActivity/GetCommunityActivityAdminList',
        type: 'post',
        contentType: 'application/json utf-8',
        data: JSON.stringify(model),
        dataType: 'json',
        success: function (response) {
            var attachFile = '';
            $('#tblCommunityActivity>tbody').empty();
            $.each(response.model, function (elementType, elementValue) {
                console.log(JSON.stringify(response));
                var fileExist = doesFileExist('/Content/assets/img/CommunityPostFiles/' + elementValue.AttatchFile);
                if (fileExist == true) {
                    attachFile = "<a href='/Content/assets/img/CommunityPostFiles/" + elementValue.AttatchFile + "' target='_blank'><i class='fa fa-paperclip'></i> Attachment</a>";
                }
                else {
                    attachFile = "<span>No Attachment</span>";
                }

                var html = "<tr data-value=" + elementValue.CID + ">";
                html += "<td>" + elementValue.Details + "</td>";
                html += "<td>" + attachFile + "</td>";
                html += "<td>" + elementValue.DateString + "</td>";
                html += "<td><button class='btn btn-danger' style='padding:5px 8px !important;' onclick='deleteCommunityPost(" + elementValue.CID + ")'><i class='fa fa-trash' aria-hidden='true'></i></button></td>";
                html += "</tr>";
                $("#tblCommunityActivity>tbody").append(html);
            });
           
            $("#divLoader").hide();
        }
    });
};

var deleteCommunityPost = function (cid) {
    $("#divLoader").show();
    
    $.alert({
        title: "",
        content: "Are you sure to remove Post?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    var model = { CID: cid };
                    $.ajax({
                        url: '/Tenant/CommunityActivity/DeleteCommunityActivityPost',
                        type: 'post',
                        contentType: 'application/json utf-8',
                        data: JSON.stringify(model),
                        dataType: 'json',
                        success: function (response) { }
                    });
                    getCommunityActivityList();
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                }
            }
        }
    });

    $("#divLoader").hide();
};

function formatPhoneFax(phonefax) {
    if (phonefax == null)
        phonefax = "";
    phonefax = phonefax.replace(/[^0-9]/g, '');
    if (phonefax.length == 0)
        return phonefax;

    return '(' + phonefax.substring(0, 3) + ') ' + phonefax.substring(3, 6) + (phonefax.length > 6 ? '-' : '') + phonefax.substring(6);
}

function formatMoney(number) {
    number = number || 0;
    var places = 2;
    var symbol = "";
    var thousand = ",";
    var decimal = ".";
    var negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
}

var serviceRequestChangeDDL = function () {
    $("#ddlOpenServiceRequest").on("change", function () {
        getServiceRequestList();
    });
};

var fileDoesNotExist = function () {
    $.alert({
        title: '',
        content: "File does not exist!",
        type: 'blue',
    });
};

var delGuestRegistration = function (id) {
    $("#divLoader").show();
    $.alert({
        title: "",
        content: "Are you sure to remove Vehicle?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    var model = { GuestID: id };
                    $.ajax({
                        url: '/GuestManagement/DeleteGuestRegistrationList',
                        method: "post",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            getGuestRegistrationList();
                            $.alert({
                                title: '',
                                content: response.model,
                                type: 'blue',
                            });
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
    $("#divLoader").hide();
};
