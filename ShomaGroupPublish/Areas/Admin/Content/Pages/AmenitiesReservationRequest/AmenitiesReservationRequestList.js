var goToEditReservationRequest = function () {
    var row = $('#tblReservationRequest tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Amenities/ARREdit/" + ID;
    }
};
var addNewReservationRequest = function () {
    window.location.href = "/Admin/Amenities/ARREdit/0";
};
var fillRPP_ReservationRequestList = function () {
    $("#ddlRPP_ReservationRequestList").empty();
    $("#ddlRPP_ReservationRequestList").append("<option value='25'>25</option>");
    $("#ddlRPP_ReservationRequestList").append("<option value='50'>50</option>");
    $("#ddlRPP_ReservationRequestList").append("<option value='75'>75</option>");
    $("#ddlRPP_ReservationRequestList").append("<option value='100'>100</option>");
    $("#ddlRPP_ReservationRequestList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationReservationRequestList($("#hdnCurrentPage_FL").val());
    });
};
var buildPaganationReservationRequestList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "TenantName";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_ReservationRequestList").val(),
        UnitId: $("#ddlUnitNoAmeResReq").val(),
        AmenityName: $("#txtAmenitiesAmeResReq").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: "/Tenant/AmenitiesRR/BuildPaganationReservationRequestList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_ReservationRequestList').addClass("hidden");
                    $("#tblReservationRequest>tbody").empty();
                }
                else {
                    $('#divPagination_ReservationRequestList').removeClass("hidden");
                    //$('#tblReservationRequest').removeClass("hidden");
                    $('#ulPagination_ReservationRequestList').pagination('updateItems', response.NOP);
                    $('#ulPagination_ReservationRequestList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillReservationRequestList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "TenantName";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    $("#divLoader").show();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ReservationRequestList").val(),
        UnitId: $("#ddlUnitNoAmeResReq").val(),
        AmenityName: $("#txtAmenitiesAmeResReq").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Tenant/AmenitiesRR/FillReservationRequestSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblReservationRequest>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.ARID + " data-amenity=" + elementValue.AmenityID + ">";
                    html += "<td>" + elementValue.TenantName + "</td>";
                    html += "<td>" + elementValue.UnitNo + "</td>";
                    html += "<td>" + elementValue.AmenityName + "</td>";
                    html += "<td>" + elementValue.DesiredDate + "</td>";
                    html += "<td>" + elementValue.DesiredTimeFrom + "</td>";
                    html += "<td>" + elementValue.DesiredTimeTo + "</td>";
                    html += "<td>" + elementValue.Duration + " hours</td>";
                    html += "<td>" + elementValue.Guest + "</td>";
                    html += "<td>" + elementValue.Status + "</td>";

                    html += "</tr>";
                    $("#tblReservationRequest>tbody").append(html);
                });
            }
        }
    });
    $("#divLoader").hide();
};
$(document).ready(function () {
    $('#txtAmenitiesAmeResReq').val('');
    fillRPP_ReservationRequestList();
    $('#ulPagination_ReservationRequestList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_FL").val(1);
            buildPaganationReservationRequestList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_FL").val(page);
            var sortByValue = localStorage.getItem("SortByValueAmiReg");
            var OrderByValue = localStorage.getItem("OrderByValueAmiReg");
            fillReservationRequestList(page, sortByValue, OrderByValue);
           
        }
    });
    $('#tblReservationRequest tbody').on('click', 'tr', function () {
        $('#tblReservationRequest tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblReservationRequest tbody').on('dblclick', 'tr', function () {
        goToEditReservationRequest();
    });
    bindUnitNoDDL();
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationReservationRequestList(1);
    }
});

var bindUnitNoDDL = function () {
    $.ajax({
        url: "/Tenant/AmenitiesRR/BindUnitNoFromTenantInfo",
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#ddlUnitNoAmeResReq").empty();
            var ddlUnit = "<option value='0'>Select Unit</option>";
            $.each(response.model, function (elementType, elementValue) {
                ddlUnit += "<option value=" + elementValue.UnitId + ">" + elementValue.UnitName + "</option>"
            })
            $("#ddlUnitNoAmeResReq").append(ddlUnit);
        }
    });
};

var count = 0;
var sortTableAminiReg = function (sortby) {
    var orderby = "";
    var pagenumber = $("#hndPageNo").val();
    if (!pagenumber) {
        pagenumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $("#SortIconARFN").removeClass('fa fa-sort-up');
        $("#SortIconARFN").removeClass('fa fa-sort-down');
        $("#SortIconAUn").removeClass('fa fa-sort-up');
        $("#SortIconAUn").removeClass('fa fa-sort-down');
        $("#SortIconAmini").removeClass('fa fa-sort-up');
        $("#SortIconAmini").removeClass('fa fa-sort-down');
        $("#SortIconARDR").removeClass('fa fa-sort-up');
        $("#SortIconARDR").removeClass('fa fa-sort-down');
        $("#SortIconARFT").removeClass('fa fa-sort-up');
        $("#SortIconARFT").removeClass('fa fa-sort-down');
        $("#SortIconARTT").removeClass('fa fa-sort-up');
        $("#SortIconARTT").removeClass('fa fa-sort-down');
        $("#SortIconARDD").removeClass('fa fa-sort-up');
        $("#SortIconARDD").removeClass('fa fa-sort-down');
        $("#SortIconARG").removeClass('fa fa-sort-up');
        $("#SortIconARG").removeClass('fa fa-sort-down');
        $("#SortIconARS").removeClass('fa fa-sort-up');
        $("#SortIconARS").removeClass('fa fa-sort-down');

        if (sortby == 'TenantName') {
            $("#SortIconARFN").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'UnitNo') {
            $("#SortIconAUn").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'AmenityName') {
            $("#SortIconAmini").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'DesiredDate') {
            $("#SortIconARDR").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'DesiredTimeFrom') {
            $("#SortIconARFT").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'DesiredTimeTo') {
            $("#SortIconARTT").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Duration') {
            $("#SortIconARDD").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Guest') {
            $("#SortIconARG").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Status') {
            $("#SortIconARS").addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $("#SortIconARFN").removeClass('fa fa-sort-up');
        $("#SortIconARFN").removeClass('fa fa-sort-down');
        $("#SortIconAUn").removeClass('fa fa-sort-up');
        $("#SortIconAUn").removeClass('fa fa-sort-down');
        $("#SortIconAmini").removeClass('fa fa-sort-up');
        $("#SortIconAmini").removeClass('fa fa-sort-down');
        $("#SortIconARDR").removeClass('fa fa-sort-up');
        $("#SortIconARDR").removeClass('fa fa-sort-down');
        $("#SortIconARFT").removeClass('fa fa-sort-up');
        $("#SortIconARFT").removeClass('fa fa-sort-down');
        $("#SortIconARTT").removeClass('fa fa-sort-up');
        $("#SortIconARTT").removeClass('fa fa-sort-down');
        $("#SortIconARDD").removeClass('fa fa-sort-up');
        $("#SortIconARDD").removeClass('fa fa-sort-down');
        $("#SortIconARG").removeClass('fa fa-sort-up');
        $("#SortIconARG").removeClass('fa fa-sort-down');
        $("#SortIconARS").removeClass('fa fa-sort-up');
        $("#SortIconARS").removeClass('fa fa-sort-down');
        if (sortby == 'TenantName') {
            $("#SortIconARFN").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'UnitNo') {
            $("#SortIconAUn").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'AmenityName') {
            $("#SortIconAmini").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'DesiredDate') {
            $("#SortIconARDR").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'DesiredTimeFrom') {
            $("#SortIconARFT").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'DesiredTimeTo') {
            $("#SortIconARTT").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Duration') {
            $("#SortIconARDD").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Guest') {
            $("#SortIconARG").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Status') {
            $("#SortIconARS").addClass('fa fa-sort-down fa-lg');
        }
       
    }
    localStorage.setItem("SortByValueAmiReg", sortby);
    localStorage.setItem("OrderByValueAmiReg", orderby);
    count++;
    buildPaganationReservationRequestList(pagenumber, sortby, orderby);
    fillReservationRequestList(pagenumber, sortby, orderby);
};