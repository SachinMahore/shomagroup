$(document).ready(function () {
    getPropertyList();
    $("#ddlProperty").on('change', function (evt, params) {
        var selected = $(this).val();

        if (selected !== null) {
            getPropertyUnitList(selected);
        }
    });
    $("#ddlUnit").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected !== null) {
            getTenantList(selected);
        }
    });
});
var getTenantList = function (pid) {
    var model = { PID: pid }
    $.ajax({
        url: "/Admin/Tenant/FillTenantDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlTenant").empty();
            $("#ddlTenant").append("<option value='0'>-Select Tenant-</option>");
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
            $("#ddlProperty").append("<option value='0'>-Select Property-</option>");
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
            $("#ddlUnit").append("<option value='0'>-Select Property Unit-</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                $("#ddlUnit").append(option);

            });
        }
    });
};
var goCashList = function () {
    window.location.href = "/Admin/CashReceipt/";
};
function saveupdateCashReceipt() {
    var msg = "";
    var crid = $("#hndCRID").val();
    var propertyid = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var tenantid = $("#ddlTenant").val();
    var status = $("#ddlStatus").val();
    var revision_num = $("#txtRevisionNumber").val();
    var batch = $("#txtBatch").val();
    var desc = $("#txtDesc").val();
    var balance = $("#txtBalance").val();
    var prepayment = $("#txtPrePayment").val();
    var payment = $("#txtPaymentAmount").val();
    var checknumber = $("#txtCheckNumber").val();
    var paymentdate = $("#txtPaymentDate").val();
    var paymenttype = $("#txtPaymentType").val();
    var bankaccount = $("#txtBankAccount").val();
    var createddate = $("#txtCreatedDate").val();
    var isapplicant = $(".icheckbox_square-yellow").hasClass("checked") ? 1 : 0;
    var depositedate = $("#txtDepositeDate").val();

    if (propertyid === "0") {
        msg += "Please Select Property</br>";
    }
    if (unitid === "0") {
        msg += "Please Select Property Unit</br>";
    }
    if (tenantid === "0") {
        msg += "Please Select Tenant</br>";
    }
    if (revision_num === "") {
        msg += "Please Enter The Revision Number</br>";
    }
    if (status === "0")
    {
        msg += "Please Select Status</br>";
    }
    if (balance === "") {
        msg += "Please Enter The Balance</br>";
    }
    if (payment === "") {
        msg += "Please Enter The Payment Amount</br>";
    }
    if (paymentdate === "") {
        msg += "Please Enter The Payment Date</br>";
    }
    if (paymenttype === "") {
        msg += "Please Enter The Payment Type</br>";
    }
    if (msg !== "")
    {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
    var model = {
        CRID : crid,
        PropertyID :  propertyid,
        UnitID : unitid,
        TenantID : tenantid,
        Revision_Num :  revision_num,
        Status : status,
        Batch :batch,
        Description:desc,
        Balance :balance,
        PrePayment:prepayment,
        PaymentAmount :payment,
        CheckNumber :checknumber,
        PaymentDate:paymentdate,
        PaymentType  :paymenttype,
        BankAccount:bankaccount,
        DateStamp :createddate,
        IsApplicant:isapplicant,
        DepositAcctDate:depositedate,
    };

    $.ajax({
        url: "/CashReceipt/SaveUpdateCashReceipt/",
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