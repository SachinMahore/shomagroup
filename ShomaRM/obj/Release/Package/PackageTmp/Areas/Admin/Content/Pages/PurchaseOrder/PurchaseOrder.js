$(document).ready(function () {
    getPropertyList();
    getPurchaseOrderLists();
    TableClickPurchaseOrder();
    vendorList();
});

var SaveUpdatePurchaseOrder = function () {

    var msg = '';
    var purchaseOrderId = $("#hdnPurchaseOrderId").val();
    var property = $("#ddlProperty").val();
    var orderNumber = $("#txtOrderNumber").val();
    var vendor = $("#ddlVendor").val();
    var purchaseOrderDesc = $("#txtPurchaseOrderDesc").val();
    var purchaseOrderDate = $("#txtPurchaseOrderDate").val();
    var totalAmount = $("#txtTotalAmount").val();
    var route = $("#txtRoute").val();
    var approvedDate = $("#txtApprovedDate").val();
    var approvedBy = $("#txtApprovedBy").val();
    var cancelDate = $("#txtCancelDate").val();
    var cancelBy = $("#ddlCancelBy").val();

    if (property == 0) { msg += "Please Select The Property </br>"; }
    if (orderNumber == '') { msg += "Please Fill The Order Number </br>"; }
    if (vendor == 0) { msg += "Please Select The Vendor </br>"; }
    if (purchaseOrderDate == '') { msg += "Please Fill The Purchase Order Date </br>"; }
    if (totalAmount == '') { msg += "Please Fill The Total Amount </br>"; }

    if (msg != '') {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        })
        return;
    }

    var model = {
        POID:purchaseOrderId,
        PropertyID: property,
        OrderNumber: orderNumber,
        Vendor: vendor,
        PODesc: purchaseOrderDesc,
        PODate: purchaseOrderDate,
        TotalAmount: totalAmount,
        Route: route,
        ApprovedDate: approvedDate,
        ApprovedBy: approvedBy,
        CanceledDate: cancelDate,
        CanceledBy: cancelBy,
    }

    $.ajax({
        url: '/PurchaseOrder/SaveUpdatePurchaseOrder',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utl-8',
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.msg,
                type: 'blue'
            });
            //setInterval(function () {
            //    window.location.replace("/Admin/PurchaseOrder")
            //}, 3000);
        }
    });
}
var goPOList = function () {
    window.location.replace("/Admin/PurchaseOrder/")
}
var getPurchaseOrderLists = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    }
    $.ajax({
        url: "/PurchaseOrder/GetPurchaseOrderList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblPurchaseOrder>tbody").empty();
            $.each(response.result, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.POID + ">";
                html += "<td>" + elementValue.PropertyName + "</td>";
                html += "<td>" + elementValue.OrderNumber + "</td>";
                html += "<td>" + elementValue.Vendor + "</td>";
                html += "<td>" + elementValue.PODateString + "</td>";
                html += "<td>" + elementValue.TotalAmount + "</td>";
                html += "<td>" + elementValue.Route + "</td>";
                html += "<td>" + elementValue.ApprovedDateString + "</td>";
                html += "<td>" + elementValue.ApprovedBy + "</td>";
                html += "</tr>";
                $("#tblPurchaseOrder>tbody").append(html);
            });

        }
    });
}

var TableClickPurchaseOrder = function () {

    $('#tblPurchaseOrder tbody').on('click', 'tr', function () {
        $('#tblPurchaseOrder tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblPurchaseOrder tbody').on('dblclick', 'tr', function () {
        goToEditPurchaseOrder();
    });
}

var goToEditPurchaseOrder = function () {

    var row = $('#tblPurchaseOrder tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {

        window.location.replace("/Admin/PurchaseOrder/Edit/" + ID);
    } else {

    }
}

var addNewPurchaseOrder = function () {
    window.location.replace("/Admin/PurchaseOrder/Edit/" + 0)
}

var getPropertyList = function () {
    $.ajax({
        url: "/WorkOrder/GetPropertyList/",
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
    });

};

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