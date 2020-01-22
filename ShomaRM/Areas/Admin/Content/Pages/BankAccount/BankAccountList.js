var selectFieldCampaingDataSource = [
    { text: "First Name", value: "FirstName" },
    { text: "LastName", value: "LastName" }
];
var goToEditBankAccount = function () {
    var row = $('#tblBankAccountList tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.replace("/Admin/BankAccount/Edit/" + ID);
    }
};
var addNewBankAccount = function () {
    window.location.replace("/Admin/BankAccount/Edit/0");
};
var fillRPP_BankAccountList = function () {
    $("#ddlRPP_BankAccountList").empty();
    $("#ddlRPP_BankAccountList").append("<option value='25'>25</option>");
    $("#ddlRPP_BankAccountList").append("<option value='50'>50</option>");
    $("#ddlRPP_BankAccountList").append("<option value='75'>75</option>");
    $("#ddlRPP_BankAccountList").append("<option value='100'>100</option>");
    $("#ddlRPP_BankAccountList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationBankAccountList($("#hdnCurrentPage_BAL").val());
    });
};
var buildPaganationBankAccountList = function (pagenumber) {
    var model = {
        Filter: $("#ddlFilter_BankAccountList").val(),
        Criteria: $("#txtCriteria_BankAccountList").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_BankAccountList").val()
    };
    $.ajax({
        url: "/BankAccount/BuildPaganationBankAccountList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_BankAccountList').addClass("hidden");
                }
                else {
                    $('#divPagination_BankAccountList').removeClass("hidden");

                    $('#ulPagination_BankAccountList').pagination('updateItems', response.NOP);
                    $('#ulPagination_BankAccountList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillBankAccountList = function (pagenumber) {
    var model = {
        Filter: $("#ddlFilter_BankAccountList").val(),
        Criteria: $("#txtCriteria_BankAccountList").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_BankAccountList").val()
    };
    $.ajax({
        url: "/BankAccount/GetBankAccountList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblBankAccountList>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.BAID + ">";
                    html += "<td>" + elementValue.BAID + "</td>";
                    html += "<td>" + elementValue.Bank_Name + "</td>";
                    html += "<td>" + elementValue.Account_Number + "</td>";
                    html += "<td>" + elementValue.Account_TypeText + "</td>";
                    html += "</tr>";
                    $("#tblBankAccountList>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_BankAccountList();
    $('#ulPagination_BankAccountList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_BAL").val(1);
            buildPaganationBankAccountList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_BAL").val(page);
            fillBankAccountList(page);
        }
    });
    $('#tblBankAccountList tbody').on('click', 'tr', function () {
        $('#tblBankAccountList tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblBankAccountList tbody').on('dblclick', 'tr', function () {
        goToEditBankAccount();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationBankAccountList(1);
    }
});

