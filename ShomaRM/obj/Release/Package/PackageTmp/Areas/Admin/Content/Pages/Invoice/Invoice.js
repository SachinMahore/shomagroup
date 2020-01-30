$(document).ready(function () {
    vendorList();
});


var SaveUpdateInvoice = function () {

    var msg = "";
    var invoiceId = $("#hdnInvoiceId").val();
    var invoiceNumber = $("#txtInvoiceNumber").val();
    var vendor = $("#ddlVendor").val();
    var invoiceDesc = $("#txtInvoiceDesc").val();
    var invoiceDate = $("#txtInvoiceDate").val();
    var recivedDate = $("#txtRecievedDate").val();
    var paymentDate = $("#txtPaymentDate").val();
    var totalAmount = $("#txtTotalAmount").val();
    var route = $("#txtRoute").val();
    var exportNow = $("#ddlExportNow").val();
    var exportedDate = $("#txtExportedDate").val();
    var exportedBy = $("#txtExportedBy").val();
    var approved = $("#txtApprovedDate").val();
    var approvedBy = $("#txtApprovedBy").val();
    var batch = $("#ddlBatch").val();

    if (invoiceNumber == "") {
        msg += "Please Fill The Invoice Number </br>";
    }
    if (vendor == "0") {
        msg += "Please Select The Vendor </br>";
    }
    if (invoiceDate == "") {
        msg += "Please Fill The Invoice Date </br>";
    }
    if (totalAmount == "") {
        msg += "Please Fill The Total Amount </br>";
    }
    if (approved == "") {
        msg += "Please Select The Approved Date </br>";
    }

    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        })
        return;
    }

    var model = {
        InvoiceID: invoiceId,
        InvoiceNumber: invoiceNumber,
        Vendor: vendor,
        InvoiceDesc: invoiceDesc,
        InvoiceDate: invoiceDate,
        ReceivedDate: recivedDate,
        PaymentDate: paymentDate,
        TotalAmount: totalAmount,
        Route: route,
        ExportNow: exportNow,
        ExportedDate: exportedDate,
        ExportedBy: exportedBy,
        Approved: approved,
        ApprovedBy: approvedBy,
        BatchID: batch
    }

    $.ajax({
        url: '/Admin/Invoice/SaveUpdateInvoice',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.msg,
                type: 'blue'
            })
            setInterval(function () {
                window.location.replace("/Admin/Invoice")
            }, 3000);
        }
    });
}
var goInvoiceList = function () {
    window.location.replace("/Admin/Invoice/");
}
var vendorList = function () {
    $.ajax({
        url: '/Vendor/VendorDataList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $("#ddlVendor").empty();
            $("#ddlVendor").append("<option value='0'>Select Vendor</option>");
            $.each(response.result, function (elementType, elementValue) {
                var option = '<option value=' + elementValue.Vendor_ID + '>' + elementValue.Vendor_Name + '</option>';
                $("#ddlVendor").append(option);
            })
        }
    });
}