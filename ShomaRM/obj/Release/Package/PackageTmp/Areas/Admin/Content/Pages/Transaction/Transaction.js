$(document).ready(function () {
    getChargeType();
    TableClick();
    getBankAcoountNoListCD();
    getBankAcoountNoListCC();
    getBankAcoountNoListAD();
    getBankAcoountNoListAC();
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
var numberValidate = function (Numval, id) {
    var filter = /^[0-9]+$/;
    if (!filter.test(Numval))
    {
        $("#" + id).val("");
    }
};
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
var getPropertyList = function (pid) {
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
    }).done(function () {
        $("#ddlProperty").val(pid);
        $("#ddlProperty").trigger('change');
    });
};
var getBankAcoountNoListCD = function () {
    var model = { PID: 1 };
    $.ajax({
        url: "/BankAccount/FillBankAccountDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlCashDebitAccount").empty();
            //$("#ddlCashDebitAccount").append("<option value='0'>Select Account</option>");
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.BAID + ">" + elementValue.Bank_Name + " (" + elementValue.Account_Number + ") " + "</option>";
                $("#ddlCashDebitAccount").append(option);
            });
        }
    });
};
var getBankAcoountNoListCC = function () {
    var model = { PID: 2 };
    $.ajax({
        url: "/BankAccount/FillBankAccountDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlCashCreditAccount").empty();
            //$("#ddlCashCreditAccount").append("<option value='0'>Select Account</option>");
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.BAID + ">" + elementValue.Bank_Name + " (" + elementValue.Account_Number + ") " + "</option>";
                $("#ddlCashCreditAccount").append(option);
            });
        }
    });
};
var getBankAcoountNoListAD = function () {
    var model = { PID: 3 };
    $.ajax({
        url: "/BankAccount/FillBankAccountDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlAccrualDebitAccount").empty();
           // $("#ddlAccrualDebitAccount").append("<option value='0'>Select Account</option>");
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.BAID + ">" + elementValue.Bank_Name + " (" + elementValue.Account_Number + ") " + "</option>";
                $("#ddlAccrualDebitAccount").append(option);
            });
        }
    });
};
var getBankAcoountNoListAC = function () {
    var model = { PID: 4 };
    $.ajax({
        url: "/BankAccount/FillBankAccountDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlAccrualCreditAccount").empty();
           // $("#ddlAccrualCreditAccount").append("<option value='0'>Select Account</option>");
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.BAID + ">" + elementValue.Bank_Name + " (" + elementValue.Account_Number + ") " + "</option>";
                $("#ddlAccrualCreditAccount").append(option);
            });
        }
    });
};
var getPropertyUnitList = function (pid) {
    var model = { PID: pid };
    $.ajax({
        url: "/PropertyManagement/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlUnit").empty();
            $("#ddlUnit").append("<option value='0'>Select Unit</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                $("#ddlUnit").append(option);
            });
        }
    });
};
var getTenantList = function (tid) {
    var model = { PID: tid };
    $.ajax({
        url: "/Admin/Tenant/FillTenantDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlTenant").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.ID + ">" + elementValue.FullName + "</option>";
                $("#ddlTenant").append(option);
            });
        }
    });
};
var addNewTransaction = function () {
    window.location.href = "/Admin/Transaction/Edit/0";
};
var transactionList = function () {
    window.location.href = "/Admin/Transaction/";
};
var TableClick = function () {
    $('#tblTransaction tbody').on('click', 'tr', function () {
        $('#tblTransaction tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblTransaction tbody').on('dblclick', 'tr', function () {
        goToEditTransaction();
    });
};
function saveupdateTransaction() {
    var msg = "";
    var transid = $("#hndTransID").val();
    var propertyid = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var tenantid = $("#ddlTenant").val();
    var leaseId = $("#txtLease").val();
    var createddate = $("#txtCreatedDate").val();
    var revision_num = $("#txtRevisionNumber").val();
    var paymentId = $("#txtPaymentID").val();
    var transdate = $("#txtTransactionDate").val();
    var transtype = $("#ddlTransactionType").val();
    var creditamount = $("#txtCreaditAmount").val();
    var chargeDate = $("#txtChargeDate").val();
    var chargeType = $("#ddlChargeType").val();
    var chargeAmount = $("#txtChargeAmount").val();
    var summaryCharge = $("#txtSummaryCharge").val();
    var cashDebitAcc = $("#ddlCashDebitAccount").val();
    var cashCreditAcc = $("#ddlCashCreditAccount").val();
    var accrualDebitAcc = $("#ddlAccrualDebitAccount").val();
    var accrualCreditAcc = $("#ddlAccrualCreditAccount").val();
    var batch = $("#txtBatch").val();
    var batchSource = $("#txtBatchSource").val();
    var journal = $("#txtJournal").val();
    var run = $("#txtRun").val();
    var taxSequence = $("#txtTaxSequence").val();
    var reference = $("#txtReference").val();
    var desc = $("#txtDesc").val();
    var applOfOrigin = $("#txtApplicationofOrigin").val();
    var entriescreated = $("#txtEntriesCreated").val();
    var transref1 = $("#txtTranasactionReference1").val();
    var transref2 = $("#txtTranasactionReference2").val();
    var transdesc = $("#txtTransDesc").val();

    if (tenantid === "0")
    {
        msg += "Please select Tenant</br>";
    }
    if(leaseId === "")
    {
        msg += "Please enter Lease ID</br>";
    }
    if (revision_num === "")
    {
        msg += "Please enter Revision Number</br>";
    }
    if (transdate === "")
    {
         msg += "Please enter Transaction date</br>";
    }
    if (run === "")
    {
        msg += "Please enter Run</br>";
    }
    if (transdesc === "")
    {
        msg += "Please enter Tranasaction Description</br>";
    }
    if (transref1 === "")
    {
        msg += "Please enter Tranasaction Reference 1</br>";
    }
    if (transref2 === "")
    {
        msg += "Please enter Tranasaction Reference 2</br>";
    }
    if (entriescreated === "")
    {
        msg += "Please enter Entries Created</br>";
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
        TransID: transid,
        PropertyID: propertyid,
        UnitID: unitid,
        TenantID: tenantid,
        LeaseID: leaseId,
        CreatedDate:createddate,
        Revision_Num: revision_num,
        Payment_ID: paymentId,
        Transaction_Date: transdate,
        Transaction_Type: transtype,
        Credit_Amount: creditamount,
        Charge_Date: chargeDate,
        Charge_Type: chargeType,
        Charge_Amount: chargeAmount,
        Summary_Charge_Type: summaryCharge,
        Cash_Debit_Account: cashDebitAcc,
        Cash_Credit_Account: cashCreditAcc,
        Accrual_Debit_Acct: accrualDebitAcc,
        Accrual_Credit_Acct: accrualCreditAcc,
        Batch: batch,
        Batch_Source: batchSource,
        Journal: journal,
        Run: run,
        Tax_Sequence: taxSequence,
        Reference: reference,     
        Description: desc,
        Appl_of_Origin: applOfOrigin,
        GL_Entries_Created: entriescreated,
        GL_Trans_Description : transdesc,
        GL_Trans_Reference_1: transref1,
        GL_Trans_Reference_2: transref2,
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
        }
    });
}