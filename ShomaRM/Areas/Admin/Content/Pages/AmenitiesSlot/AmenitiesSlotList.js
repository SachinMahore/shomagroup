var goToEditSlot = function () {
    var row = $('#tblSlot tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/AmenitiesSlot/Edit/" + ID;
    }
};
var addNewSlot = function () {
    window.location.href = "/Admin/AmenitiesSlot/Edit/0";
};
var fillRPP_SlotList = function () {
    $("#ddlRPP_SlotList").empty();
    $("#ddlRPP_SlotList").append("<option value='25'>25</option>");
    $("#ddlRPP_SlotList").append("<option value='50'>50</option>");
    $("#ddlRPP_SlotList").append("<option value='75'>75</option>");
    $("#ddlRPP_SlotList").append("<option value='100'>100</option>");
    $("#ddlRPP_SlotList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationSlotList($("#hdnCurrentPage_FL").val());
    });
};
var buildPaganationSlotList = function (pagenumber, sortbyAS, orderbyAS) {
    if (!sortbyAS) {
        sortbyAS = "AmenityName";
    }
    if (!orderbyAS) {
        orderbyAS = "ASC";
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_SlotList").val(),
        SortBy: sortbyAS,
        OrderBy: orderbyAS
    };
    
    $.ajax({
        url: "/AmenitiesSlot/BuildPaganationSlotList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_SlotList').addClass("hidden");
                }
                else {
                    $('#divPagination_SlotList').removeClass("hidden");
                    $('#ulPagination_SlotList').pagination('updateItems', response.NOP);
                    $('#ulPagination_SlotList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillSlotList = function (pagenumber, sortbyAS, orderbyAS) {
    if (!sortbyAS) {
        sortbyAS = "AmenityName";
    }
    if (!orderbyAS) {
        orderbyAS = "ASC";
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_SlotList").val(),
        SortBy: sortbyAS,
        OrderBy: orderbyAS
    };
    $.ajax({
        url: '/AmenitiesSlot/FillSlotSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblSlot>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    console.log(JSON.stringify(response));
                    var html = "<tr data-value=" + elementValue.ID + " data-amenity=" + elementValue.AmenityID + ">";
                    html += "<td>" + elementValue.AmenityName + "</td>";
                    html += "<td>" + elementValue.Duration + "</td>";
                    html += "<td>" + elementValue.Deposit + "</td>";
                    html += "<td>" + elementValue.Fees + "</td>";
                    html += "</tr>";
                    $("#tblSlot>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_SlotList();
    $('#ulPagination_SlotList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_FL").val(1);
            buildPaganationSlotList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_FL").val(page);
            var sortByValueAS = localStorage.getItem("SortByValueAS");
            var OrderByValueAS = localStorage.getItem("OrderByValueAS");
            fillSlotList(page, sortByValueAS, OrderByValueAS);
        }
    });
    $('#tblSlot tbody').on('click', 'tr', function () {
        $('#tblSlot tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblSlot tbody').on('dblclick', 'tr', function () {
        goToEditSlot();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationSlotList(1);
    }
});

var count = 0;
var sortTable = function (sortbyAS) {
    var orderbyAS = "";
    var pagenumber = $("#hndPageNo").val();
    if (!pagenumber) {
        pagenumber = 1;
    }

    if (count % 2 == 1) {
        orderbyAS = "ASC";
        $("#SortIcon").removeClass('fa fa-sort-down');
        $("#SortIcon").addClass('fa fa-sort-up fa-lg');
    }
    else {
        orderbyAS = "DESC";
        $("#SortIcon").removeClass('fa fa-sort-up');
        $("#SortIcon").addClass('fa fa-sort-down fa-lg');
    }
    localStorage.setItem("SortByValueAS", sortbyAS);
    localStorage.setItem("OrderByValueAS", orderbyAS);
    count++;
    buildPaganationSlotList(pagenumber, sortbyAS, orderbyAS);
    fillSlotList(pagenumber, sortbyAS, orderbyAS);
};
