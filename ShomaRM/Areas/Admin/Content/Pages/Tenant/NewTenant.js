$(document).ready(function () {
    
    var newTenantID = localStorage.getItem("NewTenantID");
    $("#hndNewTenant").val(newTenantID);
    
    getTenantData(newTenantID); 
    GetTenantDetails(newTenantID);
    getVehicleLists();
    getPetLists();

});



var getAccountHistory = function () {

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
                html += "<td>" + elementValue.Charge_Amount + "</td>";
                html += "</tr>";
                $("#tblAccountHistory>tbody").append(html);
            });
        }
    });
}

var getServiceRequestList = function () {
    var tenantId = $("#hndNewTenant").val();
    var serviceReq = $("#ddlOpenServiceRequest").val();
    var model = {
        TenantId: tenantId,
        ServiceRequest: serviceReq
    };
    $.ajax({
        url: '/ServiceRequest/GetServiceRequestList',
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
                //html += "<td align='center'><img src='/content/assets/img/pet/" + elementValue.TempServiceFile + "' class='picture-src' title='' style='height:70px;width:70px;'/></td>";
                // html += "<td> <a  target='_blank' href='/Content/assets/img/Document/" + elementValue.TempServiceFile + "'><i class='fa fa-eye'></i></a></td>";
                html += "</tr>";
                $("#tblServiceRequest>tbody").append(html);
            });
        }
    });
};

var getReservationRequestList = function () {
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
                    html += "<td>" + elementValue.DesiredTime + "</td>";
                    html += "<td>" + elementValue.Duration + "</td>";
                    html += "<td>" + elementValue.Status + "</td>";

                    html += "</tr>";
                    $("#tblReservationRequest>tbody").append(html);
                });
            }
        }
    });
};

var getLeaseInfoDocuments = function () {
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
                Ldhtml += "<div class='panel panel-default'>";
                Ldhtml += "<div class='panel-heading'>";
                Ldhtml += "<h3 class='panel-title'>";
                Ldhtml += "<a data-toggle='collapse' data-parent='#accordionSubLeaseDocuments' href='#collapse1Sub1" + intCount + "'><i class='fa fa-file-pdf-o' style='color:red'></i> " + response.model.EnvelopeID + "<i class='fa fa-angle-right pull-right'></i></a>";
                Ldhtml += "</h3>";
                Ldhtml += "</div>";
                Ldhtml += "<div id='collapse1Sub1" + intCount + "' class='panel-collapse collapse'>";
                Ldhtml += "<div class='panel-body'>";
                var resultLease = doesFileExist('/Content/assets/img/Document/' + response.model.EnvelopeID);
                if (resultLease == true) {
                    Ldhtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' download='" + response.model.EnvelopeID + "' href='/Content/assets/img/Document/" + response.model.EnvelopeID + "'><i class='fa fa-download'></i></a>";
                    Ldhtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' target='_blank' href='/Content/assets/img/Document/" + response.model.EnvelopeID + "' style='margin-left: 15px;'><i class='fa fa-eye'></i></a>";
                }
                else {
                    Ldhtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' href='javascript:void(0);' onclick='FileNotFound();'><i class='fa fa-download'></i></a>";
                    Ldhtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' style='margin-left: 15px;' onclick='FileNotFound();'><i class='fa fa-eye'></i></a>";
                }
                Ldhtml += "</div>";
                Ldhtml += "</div>";
                Ldhtml += "</div>";
            }
            $('#accordionSubLeaseDocument').append(Ldhtml);

            //For Identity
            $('#accordionSubIdentity').empty();
            var Html = '';
            if (response.model.PassportDoc != null) {
                if (response.model.PassportDoc != '0') {
                    intCount++;
                    Html += "<div class='panel panel-default'>";
                    Html += "<div class='panel-heading'>";
                    Html += "<h3 class='panel-title'>";
                    Html += "<a data-toggle='collapse' data-parent='#accordionSubIdentity' href='#collapse2Sub1" + intCount + "'><i class='fa fa-file-pdf-o' style='color:red'></i> " + response.model.OriginalPassportDoc + "<i class='fa fa-angle-right pull-right'></i></a>";
                    Html += "</h3>";
                    Html += "</div>";
                    Html += "<div id='collapse2Sub1" + intCount + "' class='panel-collapse collapse'>";
                    Html += "<div class='panel-body'>";
                    var resultPass = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.PassportDoc);
                    if (resultPass == true) {
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' download='" + response.model.OriginalPassportDoc + "' href='/Content/assets/img/PersonalInformation/" + response.model.PassportDoc + "'><i class='fa fa-download'></i></a>";
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='View' target='_blank' href='/Content/assets/img/PersonalInformation/" + response.model.PassportDoc + "' style='margin-left: 15px;'><i class='fa fa-eye'></i></a>";
                    }
                    else {
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' href='javascript:void(0);' onclick='FileNotFound();'><i class='fa fa-download'></i></a>";
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='View' style='margin-left: 15px;' onclick='FileNotFound();'><i class='fa fa-eye'></i></a>";
                    }
                    Html += "</div>";
                    Html += "</div>";
                    Html += "</div>";
                }
            }

            if (response.model.IdentityDoc != null) {
                if (response.model.IdentityDoc != '0') {
                    intCount++;
                    Html += "<div class='panel panel-default'>";
                    Html += "<div class='panel-heading'>";
                    Html += "<h3 class='panel-title'>";
                    Html += "<a data-toggle='collapse' data-parent='#accordionSubIdentity' href='#collapse2Sub2" + intCount + "'><i class='fa fa-file-pdf-o' style='color:red'></i> " + response.model.OriginalIdentityDoc + "<i class='fa fa-angle-right pull-right'></i></a>";
                    Html += "</h3>";
                    Html += "</div>";
                    Html += "<div id='collapse2Sub2" + intCount + "' class='panel-collapse collapse'>";
                    Html += "<div class='panel-body'>";
                    var resultIdent = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.IdentityDoc);
                    if (resultIdent == true) {
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' download='" + response.model.OriginalIdentityDoc + "' href='/Content/assets/img/PersonalInformation/" + response.model.IdentityDoc + "'><i class='fa fa-download'></i></a>";
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='View' target='_blank' href='/Content/assets/img/PersonalInformation/" + response.model.IdentityDoc + "' style='margin-left: 15px;'><i class='fa fa-eye'></i></a>";
                    }
                    else {
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' href='javascript:void(0);' onclick='FileNotFound();'><i class='fa fa-download'></i></a>";
                        Html += "<a class='btn btn-primary' data-toggle='tooltip' title='View' style='margin-left: 15px;' onclick='FileNotFound();'><i class='fa fa-eye'></i></a>";
                    }
                    Html += "</div>";
                    Html += "</div>";
                    Html += "</div>";
                }
            }
            $('#accordionSubIdentity').append(Html);

            //For Tax Return
            $('#accordionSubTaxReturn').empty();
            var Thtml = '';
            if (response.model.TaxReturnDoc1 != null) {
                if (response.model.TaxReturnDoc1 != '0') {
                    intCount++;
                    Thtml += "<div class='panel panel-default'>";
                    Thtml += "<div class='panel-heading'>";
                    Thtml += "<h3 class='panel-title'>";
                    Thtml += "<a data-toggle='collapse' data-parent='#accordionSubTaxReturn' href='#collapse3Sub1" + intCount + "'><i class='fa fa-file-pdf-o' style='color:red'></i> " + response.model.OriginalTaxReturnDoc1 + "<i class='fa fa-angle-right pull-right'></i></a>";
                    Thtml += "</h3>";
                    Thtml += "</div>";
                    Thtml += "<div id='collapse3Sub1" + intCount + "' class='panel-collapse collapse'>";
                    Thtml += "<div class='panel-body'>";
                    var resultTax1 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturnDoc1);
                    if (resultTax1 == true) {
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' download='" + response.model.OriginalTaxReturnDoc1 + "' href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc1 + "'><i class='fa fa-download'></i></a>";
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' target='_blank' href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc1 + "' style='margin-left: 15px;'><i class='fa fa-eye'></i></a>";
                    }
                    else {
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' href='javascript:void(0);' onclick='FileNotFound();'><i class='fa fa-download'></i></a>";
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' style='margin-left: 15px;' onclick='FileNotFound();'><i class='fa fa-eye'></i></a>";
                    }
                    Thtml += "</div>";
                    Thtml += "</div>";
                    Thtml += "</div>";
                }
            }

            if (response.model.TaxReturnDoc2 != null) {
                if (response.model.TaxReturnDoc2 != '0') {
                    intCount++;
                    Thtml += "<div class='panel panel-default'>";
                    Thtml += "<div class='panel-heading'>";
                    Thtml += "<h3 class='panel-title'>";
                    Thtml += "<a data-toggle='collapse' data-parent='#accordionSubTaxReturn' href='#collapse3Sub2" + intCount + "'><i class='fa fa-file-pdf-o' style='color:red'></i> " + response.model.OriginalTaxReturnDoc2 + "<i class='fa fa-angle-right pull-right'></i></a>";
                    Thtml += "</h3>";
                    Thtml += "</div>";
                    Thtml += "<div id='collapse3Sub2" + intCount + "' class='panel-collapse collapse'>";
                    Thtml += "<div class='panel-body'>";
                    var resultTax2 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturnDoc2);
                    if (resultTax2 == true) {
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' download='" + response.model.OriginalTaxReturnDoc2 + "' href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc2 + "'><i class='fa fa-download'></i></a>";
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' target='_blank' href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc2 + "' style='margin-left: 15px;'><i class='fa fa-eye'></i></a>";
                    }
                    else {
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' href='javascript:void(0);' onclick='FileNotFound();'><i class='fa fa-download'></i></a>";
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' style='margin-left: 15px;' onclick='FileNotFound();'><i class='fa fa-eye'></i></a>";
                    }
                    Thtml += "</div>";
                    Thtml += "</div>";
                    Thtml += "</div>";
                }
            }

            if (response.model.TaxReturnDoc3 != null) {
                if (response.model.TaxReturnDoc3 != '0') {
                    intCount++;
                    Thtml += "<div class='panel panel-default'>";
                    Thtml += "<div class='panel-heading'>";
                    Thtml += "<h3 class='panel-title'>";
                    Thtml += "<a data-toggle='collapse' data-parent='#accordionSubTaxReturn' href='#collapse3Sub3" + intCount + "'><i class='fa fa-file-pdf-o' style='color:red'></i> " + response.model.OriginalTaxReturnDoc3 + "<i class='fa fa-angle-right pull-right'></i></a>";
                    Thtml += "</h3>";
                    Thtml += "</div>";
                    Thtml += "<div id='collapse3Sub3" + intCount + "' class='panel-collapse collapse'>";
                    Thtml += "<div class='panel-body'>";
                    var resultTax3 = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturnDoc3);
                    if (resultTax3 == true) {
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' download='" + response.model.OriginalTaxReturnDoc3 + "' href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc3 + "'><i class='fa fa-download'></i></a>";
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' target='_blank' href='/Content/assets/img/PersonalInformation/" + response.model.TaxReturnDoc3 + "' style='margin-left: 15px;'><i class='fa fa-eye'></i></a>";
                    }
                    else {
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='Download' href='javascript:void(0);' onclick='FileNotFound();'><i class='fa fa-download'></i></a>";
                        Thtml += "<a class='btn btn-primary' data-toggle='tooltip' title='View' style='margin-left: 15px;' onclick='FileNotFound();'><i class='fa fa-eye'></i></a>";
                    }
                    Thtml += "</div>";
                    Thtml += "</div>";
                    Thtml += "</div>";
                }
            }

            $('#accordionSubTaxReturn').append(Thtml);
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
    }
    if (stepid == "11") {
       
        getLeaseInfoDocuments();

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
                    guestIdTag = '<a href="/Content/assets/img/TenantGuestInformation/' + elementValue.DriverLicence + '" target="_blank" class="fa fa-eye"></a>';
                }
                else {
                    guestIdTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();" class="fa fa-eye"></a>';
                }
                var isVehicleRegFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + elementValue.VehicleRegistration);
                if (isVehicleRegFileExist) {
                    guestVehicleRegTag = '<a href="/Content/assets/img/TenantGuestInformation/' + elementValue.VehicleRegistration + '" target="_blank" class="fa fa-eye"></a>';
                }
                else {
                    guestVehicleRegTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();" class="fa fa-eye"></a>';
                }
                var html = '';
                html += '<tr data-value="' + elementValue.GuestID + '">';
                html += '<td style="color:#3d3939;">' + elementValue.FirstName + ' ' + elementValue.LastName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.TenantName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Phone + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Email + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.VisitStartDateString + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.VisitEndDateString + '</td>';
                html += '<td style="color:#3d3939;">' + guestIdTag + '</td>';
                html += '<td style="color:#3d3939;">' + guestVehicleRegTag + '</td>';
                html += '<td style="color:#3d3939;"><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delGuestRegistration(' + elementValue.GuestID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                html += '</tr>';
                $("#tblGuestRegistration>tbody").append(html);
            });
        }
    });
    $("#divLoader").hide();
};


//My profile
var getTenantData = function (userID) {
    
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
        }
    });
}

var GetTenantDetails = function (userID) {
   // alert(userID);
    
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
                $("#Mobile1").text(response.Mobile);
                $("#Email1").text(response.Email);

                $("#EmFirstName").text(response.EmFirstNane + " " + response.EmLastName);
                $("#EmRelationship").text(response.EmRelation);
                $("#EmAddress").text(response.EmergencyAddress1);
                $("#EmMobile").text(response.EmMobile);
                $("#EmWorkPhone").text(response.EmWorkPhone);
                $("#EmEmail").text(response.EmEmail);
                $("#EmAddress").text(response.EmAddress1 + "" + response.EmAddress1);

                $("#txtFirstName").val(response.FirstName);
                $("#txtLastName").val(response.LastName);
                $("#txtMobile").val(response.Mobile);
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
                        $('#imgTenantProfilePicture').attr('src', '/Content/assets/img/Circle.png');
                    }
                }
                else {
                    $('#imgTenantProfilePicture').attr('src', '/Content/assets/img/Circle.png');
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
        }
    });
}

//Vehicle

var getVehicleLists = function () {
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
                var html = "<tr id='tr_" + elementValue.Vehicle_ID + "' data-value='" + elementValue.Vehicle_ID + "'>";
                html += "<td>" + elementValue.OwnerName + "</td>";
                html += "<td>" + elementValue.Make + "</td>";
                html += "<td>" + elementValue.VModel + "</td>";
                html += "<td>" + elementValue.Year + "</td>";
                html += "<td>" + elementValue.Color + "</td>";
                html += "<td>" + elementValue.License + "</td>";
                html += "<td><a style='background: transparent;' href='../../Content/assets/img/VehicleRegistration/" + elementValue.VehicleRegistration + "' download=" + elementValue.VehicleRegistration + " target='_blank'><span class='fa fa-download' style='background: transparent;'></span></a></td>";

                html += "</tr>";
                $("#tblVehicle>tbody").append(html);
            });

        }
    });
}

//Pet

var getPetLists = function () {
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
                var html = "<tr id='tr_" + elementValue.PetID + "' data-value='" + elementValue.PetID + "'>";
                //html += "<td align='center'><img src='/content/assets/img/pet/" + elementValue.Photo + "' class='picture-src' title='' style='height:70px;width:70px;'/></td>";

                html += "<td>" + elementValue.PetName + "</td>";
                html += "<td>" + elementValue.Breed + "</td>";
                html += "<td>" + elementValue.Weight + "</td>";
                html += "<td>" + elementValue.VetsName + "</td>";
                html += "<td><a href='../../Content/assets/img/pet/" + elementValue.PetVaccinationCertificate + "' download=" + elementValue.PetVaccinationCertificate + " target='_blank'><span class='fa fa-download'></span></a></td>";
                html += "</tr>";
                $("#tblPet>tbody").append(html);
            });
        }
    });
};




