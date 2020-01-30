var selectFieldCampaingDataSource = [
    { text: "First Name", value: "FirstName" },
    { text: "LastName", value: "LastName" },
];
var goToEditTransaction = function () {
    var row = $('#tblTransaction tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.replace("/Admin/Transaction/Edit/" + ID);
    }
};
var addNewTransaction = function () {
    window.location.replace("/Admin/Transaction/Edit/0");
};
var fillRPP_TransactionList = function () {
    $("#ddlRPP_TransactionList").empty();
    $("#ddlRPP_TransactionList").append("<option value='25'>25</option>");
    $("#ddlRPP_TransactionList").append("<option value='50'>50</option>");
    $("#ddlRPP_TransactionList").append("<option value='75'>75</option>");
    $("#ddlRPP_TransactionList").append("<option value='100'>100</option>");
    $("#ddlRPP_TransactionList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationTransactionList($("#hdnCurrentPage").val());
    });
};
var buildPaganationTransactionList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_TransactionList").val()
    };
    $.ajax({
        url: "/Transaction/BuildPaganationTransactionList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_TransactionList').addClass("hidden");
                }
                else {
                    $('#divPagination_TransactionList').removeClass("hidden");
                    $('#ulPagination_TransactionList').pagination('updateItems', response.NOP);
                    $('#ulPagination_TransactionList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillTransactionList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_TransactionList").val()
    };
    $.ajax({
        url: "/Transaction/GetTransactionList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblTransaction>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.TransID + ">";
                    html += "<td>" + elementValue.TransID + "</td>";
                    html += "<td>" + elementValue.TenantIDString + "</td>";
                    html += "<td>" + elementValue.TransactionDate + "</td>";
                    html += "<td>$" +parseFloat( elementValue.Credit_Amount).toFixed(2) + "</td>";
                    html += "<td>" + elementValue.Transaction_Type + "</td>";
                    html += "<td>" + elementValue.Charge_Type + "</td>";
                    html += "<td>" + elementValue.CreatedByText + "</td>";
                    html += "<td>" + elementValue.CreatedDate + "</td>";
                    html += "</tr>";
                    $("#tblTransaction>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_TransactionList();
    $('#ulPagination_TransactionList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage").val(1);
            buildPaganationTransactionList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillTransactionList(page);
        }
    });
    $('#tblTransaction tbody').on('click', 'tr', function () {
        $('#tblTransaction tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblTransaction tbody').on('dblclick', 'tr', function () {
        goToEditTransaction();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationTransactionList(1);
    }
});

