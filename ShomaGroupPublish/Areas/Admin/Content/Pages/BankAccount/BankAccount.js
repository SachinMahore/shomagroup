$(document).ready(function () {
});
function saveupdateBankDetail() {
    var msg = "";
    var baid = $("#hndBAID").val();
    var bankname = $("#txtBankName").val();
    var acc_num = $("#txtAccountNumber").val();
    var acc_type = $("#ddlAccountType").val();
    if (acc_type === "0") {
        msg += "Please select Account Type</br>";
    }
    if (acc_num === "") {
        msg += "Please enter Account Number</br>";
    }
    if (bankname === "") {
        msg += "Please enter Bank Name</br>";
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
        BAID: baid,
        Bank_Name: bankname,
        Account_Number: acc_num,
        Account_Type: acc_type,
    };
    $.ajax({
        url: "/BankAccount/SaveUpdateBankDetails/",
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
};
var bankDetailList = function () {
    window.location.href = "/Admin/BankAccount/";
};
var addNewBankAccount = function () {
    window.location.href = "/Admin/BankAccount/Edit/0";
};
