var goToEditInvoice = function () {
    var row = $('#tblInvoice tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Invoice/Edit/" + ID;
    }
};
var addNewInvoice = function () {
    window.location.href = "/Admin/Invoice/Edit/0";
};
var fillRPP_InvoiceList = function () {
    $("#ddlRPP_InvoiceList").empty();
    $("#ddlRPP_InvoiceList").append("<option value='25'>25</option>");
    $("#ddlRPP_InvoiceList").append("<option value='50'>50</option>");
    $("#ddlRPP_InvoiceList").append("<option value='75'>75</option>");
    $("#ddlRPP_InvoiceList").append("<option value='100'>100</option>");
    $("#ddlRPP_InvoiceList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationInvoiceList($("#hdnCurrentPage_IL").val());
    });
};
var buildPaganationInvoiceList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_InvoiceList").val()
    };
    $.ajax({
        url: "/Invoice/BuildPaganationInvoiceList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_InvoiceList').addClass("hidden");
                }
                else {
                    $('#divPagination_InvoiceList').removeClass("hidden");
                    $('#ulPagination_InvoiceList').pagination('updateItems', response.NOP);
                    $('#ulPagination_InvoiceList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillInvoiceList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_InvoiceList").val()
    };
    $.ajax({
        url: '/Invoice/FillInvoiceSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblInvoice>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.InvoiceID + ">";
                    html += "<td>" + elementValue.InvoiceNumber + "</td>";
                    html += "<td>" + elementValue.Vendor + "</td>";
                    html += "<td>" + elementValue.InvoiceDate + "</td>";
                    html += "<td>" + elementValue.ReceivedDate + "</td>";
                    html += "<td>" + elementValue.PaymentDate + "</td>";
                    html += "<td>" + elementValue.TotalAmount + "</td>";
                    html += "<td>" + elementValue.Approved + "</td>";
                    html += "<td>" + elementValue.ApprovedBy + "</td>";
                    html += "</tr>";
                    $("#tblInvoice>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_InvoiceList();
    $('#ulPagination_InvoiceList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_IL").val(1);
            buildPaganationInvoiceList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_IL").val(page);
            fillInvoiceList(page);
        }
    });
    $('#tblInvoice tbody').on('click', 'tr', function () {
        $('#tblInvoice tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblInvoice tbody').on('dblclick', 'tr', function () {
        goToEditInvoice();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationInvoiceList(1);
    }
});

