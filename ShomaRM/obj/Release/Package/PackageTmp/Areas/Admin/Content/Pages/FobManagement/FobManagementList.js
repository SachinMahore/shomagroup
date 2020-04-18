$(document).ready(function () {
    searchTable();
});


var fillRPP_PreMoving = function () {
    $("#ddlRPP_PreMoving").empty();
    $("#ddlRPP_PreMoving").append("<option value='25'>25</option>");
    $("#ddlRPP_PreMoving").append("<option value='50'>50</option>");
    $("#ddlRPP_PreMoving").append("<option value='75'>75</option>");
    $("#ddlRPP_PreMoving").append("<option value='100'>100</option>");
    $("#ddlRPP_PreMoving").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPreMoving($("#hdnCurrentPage_FL").val());
    });
};

var buildPaganationPreMoving = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'FirstName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_PreMoving").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: "/FobManagement/BuildPaganationPreMoving",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_PreMoving').addClass("hidden");
                }
                else {
                    $('#divPagination_PreMoving').removeClass("hidden");
                    $('#ulPagination_PreMoving').pagination('updateItems', response.NOP);
                    $('#ulPagination_PreMoving').pagination('selectPage', 1);
                }
            }
        }
    });
};

var fillPreMoving = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'CreatedDate';
    }
    if (!orderby) {
        orderby = 'DESC';
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_PreMoving").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/FobManagement/FillPreMovingSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tbl_FobManagement>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value='" + elementValue.ApplyNowID + "'>";
                    html += "<td style='text-align:center;'>" + elementValue.FirstName + "</td>";
                    html += "<td style='text-align:center;'>" + elementValue.LastName + "</td>";
                    html += "<td style='text-align:center;'>" + elementValue.UnitNo + "</td>";
                    html += "<td style='text-align:center;'>" + elementValue.Building + "</td>";
                    html += "<td class='text-center'><i class='fa fa-edit fa-lg' style='cursor:pointer' onclick='goToAddEditFobManagement(" + elementValue.ApplyNowID + ")'></i>";
                    html += "</td>";
                    html += "</tr>";
                    $("#tbl_FobManagement>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
        }
    });
};
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationPreMoving(1);
    }
});
var count = 0;
var sortTablePreMoving = function (sortby) {

    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#sortFnamePreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortFnamePreMovingIcon').removeClass('fa fa-sort-down');
        $('#sortLnamePreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortLnamePreMovingIcon').removeClass('fa fa-sort-down');
        $('#sortUnitPreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortUnitPreMovingIcon').removeClass('fa fa-sort-down');
        $('#sortModelPreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortModelPreMovingIcon').removeClass('fa fa-sort-down');
        if (sortby == 'FirstName') {
            $('#sortFnamePreMovingIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'LastName') {
            $('#sortLnamePreMovingIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'UnitNo') {
            $('#sortUnitPreMovingIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Model') {
            $('#sortModelPreMovingIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $('#sortFnamePreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortFnamePreMovingIcon').removeClass('fa fa-sort-down');
        $('#sortLnamePreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortLnamePreMovingIcon').removeClass('fa fa-sort-down');
        $('#sortUnitPreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortUnitPreMovingIcon').removeClass('fa fa-sort-down');
        $('#sortModelPreMovingIcon').removeClass('fa fa-sort-up');
        $('#sortModelPreMovingIcon').removeClass('fa fa-sort-down');
        if (sortby == 'FirstName') {
            $('#sortFnamePreMovingIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'LastName') {
            $('#sortLnamePreMovingIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'UnitNo') {
            $('#sortUnitPreMovingIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Model') {
            $('#sortModelPreMovingIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValuePreMoving", sortby);
    localStorage.setItem("OrderByValuePreMoving", orderby);
    count++;
    buildPaganationPreMoving(pNumber, sortby, orderby);
    fillPreMoving(pNumber, sortby, orderby);
};

var searchTable = function () {
    fillRPP_PreMoving();
    $('#ulPagination_PreMoving').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_FL").val(1);
            buildPaganationPreMoving(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_FL").val(page);
            var SortByValuePreMoving = localStorage.getItem("SortByValuePreMoving");
            var OrderByValuePreMoving = localStorage.getItem("OrderByValuePreMoving");
            fillPreMoving(page, SortByValuePreMoving, OrderByValuePreMoving);
        }
    });
}