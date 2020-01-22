var goToEditVendor = function () {
    var row = $('#tblVendor tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.replace("/Admin/Vendor/Edit/" + ID);
    }
};
var addNewVendor = function () {
    window.location.replace("/Admin/Vendor/Edit/0");
};
var fillRPP_VendorList = function () {
    $("#ddlRPP_VendorList").empty();
    $("#ddlRPP_VendorList").append("<option value='25'>25</option>");
    $("#ddlRPP_VendorList").append("<option value='50'>50</option>");
    $("#ddlRPP_VendorList").append("<option value='75'>75</option>");
    $("#ddlRPP_VendorList").append("<option value='100'>100</option>");
    $("#ddlRPP_VendorList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationVendorList($("#hdnCurrentPage_VL").val());
    });
};
var buildPaganationVendorList = function (pagenumber) {
    var searchtype = $("#hdnSearchType_VL").val();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_VendorList").val()
    };
    $.ajax({
        url: "/Vendor/BuildPaganationVendorList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_VendorList').addClass("hidden");
                }
                else {
                    $('#divPagination_VendorList').removeClass("hidden");
                    $('#ulPagination_VendorList').pagination('updateItems', response.NOP);
                    $('#ulPagination_VendorList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillVendorList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_VendorList").val()
    };
    $.ajax({
        url: "/Vendor/GetVendorDataList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblVendor>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.Vendor_ID + ">";
                    html += "<td>" + elementValue.Vendor_Name + "</td>";
                    html += "<td>" + elementValue.Mobile_Number + "</td>";
                    html += "<td>" + elementValue.Email_Id + "</td>";
                    html += "<td>" + elementValue.Address + "</td>";
                    html += "<td>" + elementValue.State + "</td>";
                    html += "<td>" + elementValue.City + "</td>";
                    html += "</tr>";
                    $("#tblVendor>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_VendorList();
    $('#ulPagination_VendorList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_VL").val(1);
            buildPaganationVendorList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_VL").val(page);
            fillVendorList(page);
        }
    });
    $('#tblVendor tbody').on('click', 'tr', function () {
        $('#tblVendor tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblVendor tbody').on('dblclick', 'tr', function () {
        goToEditVendor();
    });
    //fillVendorList($("#hdnCurrentPage_VL").val());
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationVendorList(1);
    }
});

