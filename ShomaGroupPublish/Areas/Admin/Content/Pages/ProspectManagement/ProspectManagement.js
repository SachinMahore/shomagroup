$(document).ready(function () {
    fillDdlSalesAgent();
    //alert(model.AssignAgentID);
    //$('#ddlAgentAssign').val("Model.AssignAgentID");
});
var clearBank = function () {
    $("#divCard").addClass("hidden");
  
    $("#lblAccCardname").text("Account Name");
    $("#lblAccCardnum").text("Account Number");
    $("#lblPaymentDet").text("The bank must be based on United State. Enter Bank Details.");
}
var clearCard = function () {

    $("#divCard").removeClass("hidden");
    $("#lblAccCardname").text("Card Name");
    $("#lblAccCardnum").text("Card Number");
    $("#lblPaymentDet").text("The Credit card must be based on United State. Enter Card Details.");
}
var gotoProspList = function () {
    window.location = "/Admin/ProspectManagement/";
}

var saveupdateProspect = function () {
    $("#divLoader").show();
    var msg = "";
    var salesAgent = $("#ddlAgentAssign").val();
    var appointmentStatus = $("#ddlAppointmentStatus").val();
    var prospectId = $("#hndProspectID").val();
    var appDate = $("#txtAppointmentDate").text();
    if (salesAgent == "0") {
        msg += "Select the sales Agent</br>";
    }
    if (appointmentStatus == "0") {
        msg += "Select the appointment status</br>";
    }
    if (msg != "") {
        $.alert({
            title: '',
            content: msg,
            type: 'red'
        });
        $("#divLoader").hide();
        return;
    }
    var model = { AssignAgentID: salesAgent, PID: prospectId, RequiredDateText: appDate, AppointmentStatus: appointmentStatus };
    $.ajax({
        url: "/ProspectManagement/SaveProspectForm/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: '',
                content: response.model,
                type: 'blue',
                buttons: {
                    ok: {
                        text: 'Ok',
                        action: function (ok) {
                            //window.location.href = '/Admin/ProspectManagement';
                            window.location = "/Admin/ProspectManagement/";
                        }
                    }
                }
            });
            $("#divLoader").hide();
        }

    });
}

var fillStateDDL = function () {
    var param= { CID: 1 };
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
                $("#ddlState").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
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
                    console.log(response)
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var getPropertyList = function () {
    $.ajax({
        url: "/PropertyManagement/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
           

                $("#ddlProperty").empty();
                $("#ddlProperty").append("<option value='0'>Select Property</option>");
                $.each(response.model, function (elementType, elementValue) {
                    var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                    $("#ddlProperty").append(option);

                });
            }
        }
    });
}
var getPropertyUnitList = function (pid) {
    //var pid = $("#hndPID").val();
    var model = { PID: pid };
    $.ajax({
        url: "/PropertyManagement/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
   
                $("#ddlUnit").empty();
                $.each(response.model, function (elementType, elementValue) {
                    var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                    $("#ddlUnit").append(option);

                });

            }
        }
    });
}
var addNewProspect = function () {
    window.location.href = "../ProspectManagement/AddEdit/0";
}
var convertToTenant = function () {
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
   
    if ($("#ddlState").val() == 0) {
        msg += "State is required.</br>"
    }
    if ($("#txtEmail").val() == '') {
        msg += "Email is required.</br>"
    }
    else {
        var emailId = $("#txtEmail").val();
        if (!validateEmail(emailId)) {
            msg += 'Invalid email Id.<br/>';
        }
    }
    if ($("#txtPhone").val() == '') {
        msg += "Phone is required.</br>"
    }
    else {
        var phone = $("#txtPhone").val();
        if (!validatePhone(phone)) {
            msg += 'Invalid Phone No.<br/>';
        }
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
            ProspectID: $("#hndProspectID").val(),
            FirstName: $("#txtFirstName").val(),
           // MiddleInitial: $("#txtMiddleInitial").val(),
            LastName: $("#txtLastName").val(),
            PropertyID: $("#ddlProperty").val(),
            UnitID: $("#ddlUnit").val(),
            Address: $("#txtAddress").val(),
            City: $("#ddlCity").val(),
            State: $("#ddlState").val(),
            Zip: $("#txtZip").val(),
            Gender: $("#ddlGender").val(),            
            HomePhone: $("#txtPhone").val(),
           
            OfficeEmail: $("#txtEmail").val(),
                      
        };
        $.ajax({
            url: "/Admin/Tenant/ConvertToTenant",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                   
                        $.alert({
                            title: 'Alert!',
                            content: 'Tenant Created Successfully',
                            type: 'blue'
                        })
                        $("#hndTenantID").val(response.ID);
                        window.location.href = "/Admin/Tenant/Edit/" + response.ID;
                    
                }
                else {
                    //showMessage("Error!", response.error);
                }
            }
        });
    }
}
var getVisitLists = function () {
    var model = {
        ProspectID: $("#hndProspectID").val(),
    }
    $.ajax({
        url: "/ProspectManagement/GetVisitList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblVisit>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.VisitID + ">";
                html += "<td>" + elementValue.VisitID + "</td>";
                html += "<td>" + elementValue.VisitNumber + "</td>";
                html += "<td>" + elementValue.VisitDateTimeText + "</td>";
                html += "<td>" + elementValue.VisitSlot + "</td>";
                html += "<td>" + elementValue.VisitInchargeText + "</td>";
                html += "<td>" + elementValue.Details + "</td>";
                html += "</tr>";
                $("#tblVisit>tbody").append(html);
            });

        }
    });
}

var TableClick = function () {

    $('#tblVisit tbody').on('click', 'tr', function () {
        $('#tblVisit tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblVisit tbody').on('dblclick', 'tr', function () {
        goToEditVisit();
    });
}
var goToEditVisit = function () {
   
    var row = $('#tblVisit tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {

        $("#hndVisitID").val(ID);
        var model = { id: ID };
        $.ajax({
            url: "/ProspectManagement/GetVisitDetails",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#popVisit").PopupWindow("open");

                $("#txtVisitDate").val(response.model.VisitDateTimeText);
                $("#ddlVisitResult").val(response.model.ResultCode);
                $("#txtVisitDetails").val(response.model.Details);
                $("#ddlVisitNumber").val(response.model.VisitNumber);
                $("#ddlVisitActivity").val(response.model.Activity);
                $("#ddlVisitSlot").val(response.model.VisitSlot);
                $("#ddlVisitIncharge").val(response.model.VisitIncharge);
                $("#ddlVisitIncharge").trigger('change');
            }
        });

    } else {

    }
}
function saveupdateVisit() {

    var msg = "";
    var visitid = $("#hndVisitID").val();
    var prospectid = $("#hndProspectID").val();
    var visitdate = $("#txtVisitDate").val();
    var visitincharge = $("#ddlVisitIncharge").val();
    var visitact = $("#ddlVisitActivity").val();
    var visitresult = $("#ddlVisitResult").val();
    var visitdet = $("#txtVisitDetails").val();
    var visitno = $("#ddlVisitNumber").val();
    var visitslot = $("#ddlVisitSlot").val();

    if (visitincharge == 0) {
        msg += "Select Visit Incharge";
    }
    if (visitresult == 0) {
        msg += "Select Visit Result";
    }
    if (visitdate == "") {
        msg += "Enter Visit Date";
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
        VisitID : visitid,
        ProspectID : prospectid,
        VisitDateTime : visitdate,
        ResultCode : visitresult,
        VisitNumber : visitno,
        Details : visitdet,
        Activity: visitact,
        VisitIncharge: visitincharge,
        VisitSlot: visitslot,
    };

    $.ajax({
        url: "/ProspectManagement/SaveUpdateVisit",
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
            $("#popVisit").PopupWindow("close");
            clearVisit();
            getVisitLists();
        }


    });

}
var clearVisit = function () {
    $("#hndVisitID").val(0);

    $("#ddlVisitIncharge").val(0);
    $("#txtVisitDate").val("");
    $("#ddlVisitActivity").val(1);
    $("#ddlVisitResult").val("PA");
    $("#ddlVisitSlot").val(0);
    $("#txtVisitDetails").val("");
    
}

var fillUserDDL = function () {
    var param = { UserType: 2 };
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
                $("#ddlVisitIncharge").empty();
                $("#ddlVisitIncharge").append("<option value='0'>-- Select Incharge --</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlVisitIncharge").append("<option value=" + elementValue.UserID + ">" + elementValue.FirstName + ' ' + elementValue.LastName + "</option>");
                });
            }
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

var goToStep = function (stepid, id) {
    if (stepid == "1") {
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step1").removeClass("hidden");
        $("#step5").addClass("hidden");
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
        $("#step5").addClass("hidden");
    }
    if (stepid == "3") {
        getChargeType();
        getTransationLists();
        $("#li3").addClass("active");
        $("#li2").removeClass("active");
        $("#li1").removeClass("active");
        $("#li4").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step3").removeClass("hidden");
        $("#step5").addClass("hidden");
    }
    if (stepid == "4") {
        getDocumentLists();
        $("#li4").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li1").removeClass("active");
        $("#li5").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").removeClass("hidden");
        $("#step5").addClass("hidden");
    }
    if (stepid == "5") {
        
        $("#li5").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li1").removeClass("active");
        $("#li4").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").removeClass("hidden");
    }
}
var getTransationLists = function () {
    var model = {
       
        ProspectID: $("#hndProspectID").val(),
    }
    $.ajax({
        url: "/Transaction/GetProspectTransactionList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblTransaction>tbody").empty();

            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.TransID + ">";
                html += "<td>" + elementValue.TransID + "</td>";
                //html += "<td>" + elementValue.TenantIDString + "</td>";
                html += "<td>" + elementValue.Transaction_DateString + "</td>";
                html += "<td>$" + elementValue.Charge_Amount + "</td>";
                html += "<td>" + elementValue.Transaction_Type + "</td>";
                html += "<td>" + elementValue.Charge_Type + "</td>";
                html += "<td>" + elementValue.TBankName + "</td>";
                html += "<td>" + elementValue.CreatedDateString + "</td>";
                html += "</tr>";
                $("#tblTransaction>tbody").append(html);
            });

        }
    });
}
function saveupdateTransaction() {

    var msg = "";
    var transid = $("#hndTransID").val();
    var propertyid = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var prospectid = $("#hndProspectID").val();
    var leaseId = $("#hndLID").val();
    //var createddate = $("#txtCreatedDate").val();
    var revision_num = 1;
    // var paymentId = $("#txtPaymentID").val();
    // var transdate = $("#txtTransactionDate").val();
    var transtype = $("#ddlTransactionType").val();
    //var creditamount = $("#txtCreaditAmount").val();
    var chargeDate = $("#txtChargeDate").val();
    var chargeType = $("#ddlChargeType").val();
    var chargeAmount = $("#txtChargeAmount").val();
    var summaryCharge = $("#txtSummaryCharge").val();
    //var cashDebitAcc = $("#txtCashDebitAccount").val();
    //var cashCreditAcc = $("#txtCashCreditAccount").val();
    //var accrualDebitAcc = $("#txtAccrualDebitAccount").val();
    // var accrualCreditAcc = $("#txtCreditDebitAccount").val();
    // var batch = $("#txtBatch").val();
    // var batchSource = $("#txtBatchSource").val();
    // var journal = $("#txtJournal").val();
    // var run = $("#txtRun").val();
    // var taxSequence = $("#txtTaxSequence").val();
    // var reference = $("#txtReference").val();
    var desc = $("#txtDesc").val();

    var bankname = $("#txtBankName").val();
    var acccardno = $("#txtAccountNumber").val();
    var accname = $("#txtAccountName").val();
    var routingno = $("#txtRoutingNumber").val();
    var cexpmonth = $("#ddlCEMonth").val();
    var cexpyr = $("#ddlCEYear").val();
    var secuityno = $("#txtSecurityNumber").val();

    //var transdesc = $("#txtTransDesc").val();

    if (transtype == 0) {
        msg += "Select Transaction Type</br>";
    }
    if (chargeType == 0) {
        msg += "Select Charge Type</br>";
    }
    if (chargeDate == "") {
        msg += "Enter Charge Date</br>";
    }
    if (chargeAmount == 0) {
        msg += "Enter Charge Amount</br>";
    }
    if (bankname == "") {
        msg += "Enter bank name</br>";
    }
    if (acccardno == "") {
        msg += "Enter Account/Card Number</br>";
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
        TenantID: 0,
        ProspectID: prospectid,
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
        //Cash_Debit_Account: cashDebitAcc,
        // Cash_Credit_Account: cashCreditAcc,
        // Accrual_Debit_Acct: accrualDebitAcc,
        // Accrual_Credit_Acct: accrualCreditAcc,
        // Batch: batch,
        // Batch_Source: batchSource,
        // Journal: journal,
        // Run: run,
        // Tax_Sequence: taxSequence,
        // Reference: reference,
        Description: desc,
        TBankName: bankname,
        TAccCardNumber: acccardno,
        TAccCardName: accname,
        TRoutingNumber: routingno,
        TCardExpirationMonth: cexpmonth,
        TCardExpirationYear: cexpyr,
        TSecurityNumber: secuityno,
  
    };

    $.ajax({
        url: "/Transaction/SaveUpdateTransaction/",
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

var SaveUpdateDocument = function () {

    var msg = "";
    var documentId = $("#hdnDocumentId").val();
    var tenantId = $("#hndProspectID").val();
    var documentName = $("#txtDocumentName").val();
    var documentType = $("#ddlDocumentType").val();
    var documentNumber = $("#txtDocumentNumber").val();

    if (tenantId == "0") {
        msg += "Please Select The Tenant </br>";
    }
    if (documentName == "") {
        msg += "Please Fill The DocumentName </br>";
    }
    if (documentType == "0") {
        msg += "Please Select The Document Type </br>";
    }
    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        })
        return;
    }

    $formData = new FormData();

    $formData.append('DocID', documentId);
    $formData.append('TenantID', tenantId);
    $formData.append('DocumentName', documentName);
    $formData.append('DocumentType', documentType);
    $formData.append('DocumentNumber', documentNumber);
    var photo = document.getElementById('wizard-picture');
    if (photo.files.length > 0) {
        for (var i = 0; i < photo.files.length; i++) {
            $formData.append('file-' + i, photo.files[i]);
        }

        $.ajax({
            url: '/Admin/Document/SaveUpdateDocument',
            type: 'post',
            data: $formData,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                $.alert({
                    title: 'Alert!',
                    content: "Document uploaded successfully",
                    type: 'blue'
                });
                getDocumentLists();
            }
        });
    }
}

var getDocumentLists = function () {
    var model = {
        ProspectID: $("#hndProspectID").val(),
    }
    $.ajax({
        url: "/Admin/Document/GetDocumentData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblDocument>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.DocID + ">";
              
                html += "<td>" + elementValue.DocumentName + "</td>";
                html += "<td>" + elementValue.DocumentType + "</td>";
                html += "<td>" + elementValue.DocumentNumber + "</td>";
                html += "</tr>";
                $("#tblDocument>tbody").append(html);
            });

        }
    });
}

var fillMarketSourceDDL = function () {
    $.ajax({
        url: '/Admin/ProspectManagement/GetDdlMarketSourceList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            $("#ddlMarketSource").empty();
            $("#ddlMarketSource").append("<option value='0'>-- Select Market Source --</option>");
            $.each(response.model, function (index, elementValue) {
                    $("#ddlMarketSource").append("<option value=" + elementValue.AdID + ">" + elementValue.Advertiser + "</option>");
                });
            
        }
    });
}

var fillDdlSalesAgent = function () {
    var param = { UserType: 6 };
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
                $("#ddlAgentAssign").empty();
                $("#ddlAgentAssign").append("<option value='0'>Select Sales Agent</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlAgentAssign").append("<option value=" + elementValue.UserID + ">" + elementValue.FirstName + ' ' + elementValue.LastName + "</option>");
                });
            }
        }
    });
}