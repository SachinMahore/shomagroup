var goToEditCashReceipt = function () {
    var row = $('#tblCashReceipt tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/CashReceipt/Edit/" + ID;
    }
};
var addNewCashReceipt = function () {
    window.location.href = "/Admin/CashReceipt/Edit/0";
};
var fillRPP_CashReceiptList = function () {
    $("#ddlRPP_CashReceiptList").empty();
    $("#ddlRPP_CashReceiptList").append("<option value='25'>25</option>");
    $("#ddlRPP_CashReceiptList").append("<option value='50'>50</option>");
    $("#ddlRPP_CashReceiptList").append("<option value='75'>75</option>");
    $("#ddlRPP_CashReceiptList").append("<option value='100'>100</option>");
    $("#ddlRPP_CashReceiptList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationCashReceiptList($("#hdnCurrentPage_CR").val());
    });
};
var buildPaganationCashReceiptList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_CashReceiptList").val()
    };
    $.ajax({
        url: "/CashReceipt/BuildPaganationCashReceiptList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_CashReceiptList').addClass("hidden");
                }
                else {
                    $('#divPagination_CashReceiptList').removeClass("hidden");
                    $('#ulPagination_CashReceiptList').pagination('updateItems', response.NOP);
                    $('#ulPagination_CashReceiptList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillCashReceiptList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_CashReceiptList").val()
    };
    $.ajax({
        url: '/CashReceipt/FillCashReceiptSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblCashReceipt>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.CRID + ">";
                    html += "<td>" + elementValue.CRID + "</td>";
                    html += "<td>" + elementValue.PropertyTitle + "</td>";
                    html += "<td>" + elementValue.UnitName + "</td>";
                    html += "<td>" + elementValue.TenantName + "</td>";
                    html += "<td class='hidden'>" + elementValue.Revision_Num + "</td>";
                    html += "<td>" + elementValue.PaymentAmount + "</td>";
                    html += "<td>" + elementValue.PrePayment + "</td>";
                    html += "<td>" + elementValue.Balance + "</td>";
                    html += "<td>" + elementValue.PaymentDate + "</td>";
                    html += "<td>" + elementValue.PaymentType + "</td>";
                    html += "<td>" + elementValue.DepositAcctDate + "</td>";
                    html += "</tr>";
                    $("#tblCashReceipt>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_CashReceiptList();
    $('#ulPagination_CashReceiptList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_CR").val(1);
            buildPaganationCashReceiptList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_CR").val(page);
            fillCashReceiptList(page);
        }
    });
    $('#tblCashReceipt tbody').on('click', 'tr', function () {
        $('#tblCashReceipt tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblCashReceipt tbody').on('dblclick', 'tr', function () {
        goToEditCashReceipt();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationCashReceiptList(1);
    }
});

