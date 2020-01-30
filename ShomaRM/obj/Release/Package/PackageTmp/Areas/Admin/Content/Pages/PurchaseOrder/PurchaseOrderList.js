var goToEditPurchaseOrder = function () {
    var row = $('#tblPurchaseOrder tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/PurchaseOrder/Edit/" + ID;
    }
};
var addNewPurchaseOrder = function () {
    window.location.href = "/Admin/PurchaseOrder/Edit/0";
};
var fillRPP_PurchaseOrderList = function () {
    $("#ddlRPP_PurchaseOrderList").empty();
    $("#ddlRPP_PurchaseOrderList").append("<option value='25'>25</option>");
    $("#ddlRPP_PurchaseOrderList").append("<option value='50'>50</option>");
    $("#ddlRPP_PurchaseOrderList").append("<option value='75'>75</option>");
    $("#ddlRPP_PurchaseOrderList").append("<option value='100'>100</option>");
    $("#ddlRPP_PurchaseOrderList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPurchaseOrderList($("#hdnCurrentPage").val());
    });
};
var buildPaganationPurchaseOrderList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_PurchaseOrderList").val()
    };
    $.ajax({
        url: "/PurchaseOrder/BuildPaganationPurchaseOrderList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_PurchaseOrderList').addClass("hidden");
                }
                else {
                    $('#divPagination_PurchaseOrderList').removeClass("hidden");
                    $('#ulPagination_PurchaseOrderList').pagination('updateItems', response.NOP);
                    $('#ulPagination_PurchaseOrderList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillPurchaseOrderList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_PurchaseOrderList").val()
    };
    $.ajax({
        url: '/PurchaseOrder/FillPurchaseOrderSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblPurchaseOrder>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.POID + ">";
                    html += "<td>" + elementValue.PropertyID + "</td>";
                    html += "<td>" + elementValue.OrderNumber + "</td>";
                    html += "<td>" + elementValue.Vendor + "</td>";
                    html += "<td>" + elementValue.PODate + "</td>";
                    html += "<td>" + elementValue.TotalAmount + "</td>";
                    html += "<td>" + elementValue.Route + "</td>";
                    html += "<td>" + elementValue.ApprovedDate + "</td>";
                    html += "<td>" + elementValue.ApprovedBy + "</td>";
                    html += "</tr>";
                    $("#tblPurchaseOrder>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_PurchaseOrderList();
    $('#ulPagination_PurchaseOrderList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage").val(1);
            buildPaganationPurchaseOrderList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillPurchaseOrderList(page);
        }
    });
    $('#tblPurchaseOrder tbody').on('click', 'tr', function () {
        $('#tblPurchaseOrder tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblPurchaseOrder tbody').on('dblclick', 'tr', function () {
        goToEditPurchaseOrder();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationPurchaseOrderList(1);
    }
});

