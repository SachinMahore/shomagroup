var goToEditParking = function () {
    var row = $('#tblParking tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Parking/Edit/" + ID;
    }
};
var addNewParking = function () {
    window.location.href = "/Admin/Parking/Edit/0";
};
var fillRPP_ParkingList = function () {
    $("#ddlRPP_ParkingList").empty();
    $("#ddlRPP_ParkingList").append("<option value='25'>25</option>");
    $("#ddlRPP_ParkingList").append("<option value='50'>50</option>");
    $("#ddlRPP_ParkingList").append("<option value='75'>75</option>");
    $("#ddlRPP_ParkingList").append("<option value='100'>100</option>");
    $("#ddlRPP_ParkingList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationParkingList($("#hdnCurrentPage").val());
    });
};
var buildPaganationParkingList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "ParkingID";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        Criteria: $("#ddlCriteria").val(),
        CriteriaByText: $("#txtCriteria").val(),
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ParkingList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: "/Parking/BuildPaganationParkingList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_ParkingList').addClass("hidden");
                }
                else {
                    $('#divPagination_ParkingList').removeClass("hidden");
                    $('#ulPagination_ParkingList').pagination('updateItems', response.NOP);
                    $('#ulPagination_ParkingList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillParkingList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "ParkingID";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        Criteria: $("#ddlCriteria").val(),
        CriteriaByText: $("#txtCriteria").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ParkingList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };

    $.ajax({
        url: '/Parking/FillParkingSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblParking>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    console.log(JSON.stringify(response));
                    var html = '';
                    var Available = "";
                    if (elementValue.UnitNo == 0 && elementValue.Type == 2) {
                        Available = "Yes";
                    }
                    else {
                        Available = "No";
                    }
                    html += '<tr data-value="' + elementValue.ParkingID + '">';
                    html += '<td class="pds-Number" style="color:#3d3939;">' + elementValue.ParkingID + '</td>';
                    html += '<td class="pds-Charge" style="color:#3d3939;">' + formatMoney(elementValue.Charges) + '</td>';
                    html += '<td class="pds-Available" style="color:#3d3939;">' + Available + '</td>';
                    html += '<td class="pds-Location" style="color:#3d3939;">' + elementValue.Description + '</td>';
                    html += '<td class="pds-Space" style="color:#3d3939;">' + elementValue.ParkingName + '</td>';
                    html += '<td class="pds-UnitNo" style="color:#3d3939;">' + elementValue.UnitNo + '</td>';
                    html += '<td class="pds-Tag" style="color:#3d3939;">' + elementValue.VehicleTag + '</td>';
                    html += '<td class="pds-Owner" style="color:#3d3939;">' + elementValue.OwnerName + '</td>';
                    html += "</tr>";
                    $("#tblParking>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
            $("#divLoader").hide();
        }
    });
};

$(document).ready(function () {
    fillRPP_ParkingList();
    checkCriteria();
    $('#ulPagination_ParkingList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage").val(1);
            buildPaganationParkingList(1);
        },
        onPageClick: function (page, evt) {
            var SortByValueProsVerification = localStorage.getItem("SortByValueParking");
            var OrderByValueProsVerification = localStorage.getItem("OrderByValueParking");
            $("#hdnCurrentPage").val(page);
            fillParkingList(page, SortByValueProsVerification, OrderByValueProsVerification);
        }
    });
    $('#tblParking tbody').on('click', 'tr', function () {
        $('#tblParking tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblParking tbody').on('dblclick', 'tr', function () {
        goToEditParking();
    });

    $("#ddlCriteria").on('change', function () {
        if ($("#ddlCriteria").val() == 0) {
            $("#txtCriteria").val("");
            $("#txtCriteria").attr("disabled", "disabled");
        }
        else {
            $("#txtCriteria").removeAttr("disabled", "disabled");
        }
    });

    $("#ddlCriteria").on('change', function () {
        checkCriteria();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationParkingList(1);
    }
});

var checkCriteria = function () {

    $("#divCriteriatxt").empty();
    var html = '';
    if ($("#ddlCriteria").val() == 0) {
        html = '<input id="txtCriteria"  class="form-control form-control-small" type="text" value=""  disabled/>';
        $("#txtCriteria").val("");
    }
    else if ($("#ddlCriteria").val() == 1) {
        html += '<select id="txtCriteria" class="form-control form-control-small">';
        html += '<option value = "0" selected > Yes </option >';
        html += '<option value="1"> No </option>';
        html += '</select>';
    }
    else if ($("#ddlCriteria").val() == 6) {
        html = '<input id="txtCriteria"  class="form-control form-control-small" type="text" value=""  disabled/>';
        $("#txtCriteria").val("");
    }
    else if ($("#ddlCriteria").val() == 7) {
        html = '<input id="txtCriteria"  class="form-control form-control-small" type="text" value=""  disabled/>';
        $("#txtCriteria").val("");
    }
    else {
        html = '<input id="txtCriteria"  class="form-control form-control-small" type="text" value="" />';
    }

    $("#divCriteriatxt").append(html);
};


var count = 0;
var sortTableParking = function (sortby) {

    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }
    $('#sortParkingIDIcon').removeClass('fa fa-sort-up');
    $('#sortParkingIDIcon').removeClass('fa fa-sort-down');
    $('#sortChargeIcon').removeClass('fa fa-sort-up');
    $('#sortChargeIcon').removeClass('fa fa-sort-down');
    $('#sortTypeIcon').removeClass('fa fa-sort-up');
    $('#sortTypeIcon').removeClass('fa fa-sort-down');
    $('#sortDescriptionIcon').removeClass('fa fa-sort-up');
    $('#sortDescriptionIcon').removeClass('fa fa-sort-down');
    $('#sortParkingNameIcon').removeClass('fa fa-sort-up');
    $('#sortParkingNameIcon').removeClass('fa fa-sort-down');
    $('#sortPropertyIDIcon').removeClass('fa fa-sort-up');
    $('#sortPropertyIDIcon').removeClass('fa fa-sort-down');
    $('#sortTagIcon').removeClass('fa fa-sort-up');
    $('#sortTagIcon').removeClass('fa fa-sort-down');
    $('#sortOwnerNameIcon').removeClass('fa fa-sort-up');
    $('#sortOwnerNameIcon').removeClass('fa fa-sort-down');
    if (count % 2 == 1) {
        orderby = "ASC";
        if (sortby == 'ParkingID') {
            $('#sortParkingIDIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Charges') {
            $('#sortChargeIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Type') {
            $('#sortTypeIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Description') {
            $('#sortDescriptionIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'ParkingName') {
            $('#sortParkingNameIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'PropertyID') {
            $('#sortPropertyIDIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Tag') {
            $('#sortTagIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'OwnerName') {
            $('#sortOwnerNameIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        if (sortby == 'ParkingID') {
            $('#sortParkingIDIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Charges') {
            $('#sortChargeIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Type') {
            $('#sortTypeIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Description') {
            $('#sortDescriptionIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'ParkingName') {
            $('#sortParkingNameIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'PropertyID') {
            $('#sortPropertyIDIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Tag') {
            $('#sortTagIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'OwnerName') {
            $('#sortOwnerNameIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueParking", sortby);
    localStorage.setItem("OrderByValueParking", orderby);
    count++;
    buildPaganationParkingList(pNumber, sortby, orderby);
    fillParkingList(pNumber, sortby, orderby);
};
