$(document).ready(function () {
    getPropertyList();
    getTransationLists();
});

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
}

var SaveLeaseDocumentVerification = function () {

    //var msg = "";
    //var documentVerificationID = $("#hdnDocumentVerificationId").val();
    var PID = $("#hndANID").val();
    //var address = $("#txtAddress").val();
    //var documentType = $("#ddlDocumentType").val();

    //var documentName = $("#txtDocumentName").val();


    //if (documentType == "0") {
    //    msg += "Please Select The Document Type </br>";
    //}
    //if (msg != "") {
    //    $.alert({
    //        title: "Alert!",
    //        content: msg,
    //        type: 'red'
    //    })
    //    return;
    //}

    $formData = new FormData();

    //$formData.append('DocID', documentVerificationID);
    $formData.append('ProspectusID', PID);
    //$formData.append('Address', address);
    //$formData.append('DocumentType', documentType);
    //$formData.append('DocumentName', documentName);
    var photo = document.getElementById('wizard-picture');
    if (photo.files.length > 0) {
        for (var i = 0; i < photo.files.length; i++) {
            $formData.append('file-' + i, photo.files[i]);
        }

        $.ajax({
            url: '/Verification/SaveLeaseDocumentVerification',
            type: 'post',
            data: $formData,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue'
                });
            }
        });
    }
}

function savePayment() {

    var msg = "";
    var propertyId = $("#ddlProperty").val();
    var unitId = $("#ddlUnit").val();
    var nameonCard = $("#txtNameonCard").val();
    var cardNumber = $("#txtCardNumber").val();
    var cardMonth = $("#ddlcardmonth").val();
    var cardYear = $("#ddlcardyear").val();
    var cvvNumber = $("#txtCcvNumber").val();
    var prospectID = $("#hndANID").val();
    var amounttoPay = $("#lblTotalPayement").text();

    if (nameonCard == "") {
        msg += "Please Enter Name on Card</br>";
    }
    if (cardNumber == "" || cardNumber.length != 14) {
        msg += "Please enter your 14 digit Card Number</br>";
    }
    if (cardMonth == "0") {
        msg += "Please enter Card Month</br>";
    }
    if (cardYear == "0") {
        msg += "Please enter Card Year</br>";
    }
    if (cvvNumber == "" || cvvNumber.length != 3) {
        msg += "Please enter your 3 digit CCV Number</br>";
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
        PID: unitId,
        PropertyID: propertyId,
        Name_On_Card: nameonCard,
        CardNumber: cardNumber,
        CardMonth: cardMonth,
        CardYear: cardYear,
        CCVNumber: cvvNumber,
        Charge_Amount: amounttoPay,
        ProspectID: prospectID
    };

    $.ajax({
        url: "/Verification/SavePaymentDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            if (response.Msg == "Transaction Successfull") {
                getTransationLists();
                $("#Paymentdet").addClass("hidden");
                $("#btnnext").removeAttr("disabled");
                $("#ResponseMsg").html(response.Msg + "For Payment $" + amounttoPay);
            }
        }
    });
}
var getPropertyUnitDetails = function (uid) {

    var model = { UID: uid };
    $.ajax({
        url: "/Property/GetPropertyUnitDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {


                $("#ddlProperty").val(response.model.PID);
                $("#ddlProperty").trigger('change');
                getPropertyUnitList(response.model.PID);
                setTimeout(function () {
                    $("#ddlUnit").val(uid);
                    $("#ddlUnit").trigger('change');
                }, 1200)

            $("#lblRent").text("$" + response.model.Current_Rent);

            $("#lblDeposit").text("$" + response.model.Deposit);
            $("#lblTotalPayement").text(parseFloat(response.model.Current_Rent) + parseFloat(response.model.Deposit));
        }
    })
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
var saveLeaseDoc = function () {
    var param = { TenantID: $("#hdnUserId").val() };
    $.ajax({
        url: "/ProspectVerification/PDFBuilder",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {

            window.open("/Content/assets/img/Document/" + response.result, "popupWindow", "width=900,height=600,scrollbars=yes");
        }
    });
}
var getTransationLists = function () {
    var model = {

        TenantID: $("#hdnUserId").val(),
    }
    $.ajax({
        url: "/Transaction/GetOnlineTransactionList",
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
                html += "<td>" + elementValue.Description + "</td>";
                html += "<td>" + elementValue.CreatedDateString + "</td>";
                html += "</tr>";
                $("#tblTransaction>tbody").append(html);
            });

        }
    });
}