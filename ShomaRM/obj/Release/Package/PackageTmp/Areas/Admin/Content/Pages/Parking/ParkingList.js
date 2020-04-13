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
        buildPaganationParkingList($("#hdnCurrentPage_CR").val());
    });
};
var buildPaganationParkingList = function (pagenumber) {
   
    var model = {
        Criteria: $("#ddlCriteria").val(),
        CriteriaByText: $("#txtCriteria").val(),
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ParkingList").val()
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
var fillParkingList = function (pagenumber) {
    var model = {
        Criteria: $("#ddlCriteria").val(),
        CriteriaByText: $("#txtCriteria").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ParkingList").val()
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
        }
    });
};
$(document).ready(function () {
    fillRPP_ParkingList();
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
            $("#hdnCurrentPage").val(page);
            fillParkingList(page);
        }
    });
    $('#tblParking tbody').on('click', 'tr', function () {
        $('#tblParking tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblParking tbody').on('dblclick', 'tr', function () {
        goToEditParking();
    });
    if ($("#ddlCriteria").val() == 0) {
        $("#txtCriteria").val("");
        $("#txtCriteria").attr("disabled", "disabled");
    }
    else {
        $("#txtCriteria").removeAttr("disabled", "disabled");
    }
    $("#ddlCriteria").on('change', function () {
        if ($("#ddlCriteria").val() == 0) {
            $("#txtCriteria").val("");
            $("#txtCriteria").attr("disabled", "disabled");
        }
        else {
            $("#txtCriteria").removeAttr("disabled", "disabled");
        }
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationParkingList(1);
    }
});

