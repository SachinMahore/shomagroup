$(document).ready(function () {
    fillRPP_CausingIssue();
    clearCausingIssue();
    $('#tblCausingIssue tbody').on('click', 'tr', function () {
        $('#tblLocation tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblCausingIssue tbody').on('click', 'tr', function () {
        goToLocation();
    });

    //getLocationData($("#hndCausingIssueID").val());
    fillDdlService();
    fillCausgIssue();

    $("#txtCriteriaCausingIssue").keyup(function () {

    });
    $('#ulPagination_CausingIssue').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            //console.log("Pagination_Init");
            buildPaganationCausingIssueList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillCausingSearchGrid(page);
        }
    });
});
var buildPaganationCausingIssueList = function (pagenumber) {

    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaCausingIssue").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_CausingIssue").val()
    };
    $.ajax({
        url: 'ServiceCausing/BuildPaganationSLList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#ddlRPP_CausingIssue').addClass("hidden");
                    $('#divPagination_CausingIssue').addClass("hidden");
                    $('#lblRPP_CausingIssue').addClass("hidden");
                }
                else {
                    $('#ddlRPP_CausingIssue').removeClass("hidden");
                    $('#divPagination_CausingIssue').removeClass("hidden");
                    $('#lblRPP_CausingIssue').removeClass("hidden");

                    $('#ulPagination_CausingIssue').pagination('updateItems', response.NOP);
                    $('#ulPagination_CausingIssue').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillCausingSearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaCausingIssue").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_CausingIssue").val()
    };
    $.ajax({
        url: 'ServiceCausing/fillCausingSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblCausingIssue>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.LocationID + '"  id="tr_' + elementValue.CausingIssueID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.CausingIssueID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ServiceIssue + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.CausingIssue + '</td>';
                   // html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ServiceIssue + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToLocation(' + elementValue.CausingIssueID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delLocation(' + elementValue.CausingIssueID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblCausingIssue>tbody").append(html);
                    CausingIssueDataSource.push({ CausingIssueID: elementValue.CausingIssueID, CausingIssue: elementValue.CausingIssue, ServiceIssue: elementValue.ServiceIssue  });
                });

            }
        }
    });
};
var fillRPP_CausingIssue = function () {
    $("#ddlRPP_CausingIssue").empty();
    $("#ddlRPP_CausingIssue").append("<option value='25'>25</option>");
    $("#ddlRPP_CausingIssue").append("<option value='50'>50</option>");
    $("#ddlRPP_CausingIssue").append("<option value='75'>75</option>");
    $("#ddlRPP_CausingIssue").append("<option value='100'>100</option>");
    $("#ddlRPP_CausingIssue").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationCausingIssueList($("#hdnCurrentPage").val());
    });
};
var CausingIssueDataSource = [];
var clearCausingIssue = function () {
    $("#hndLocationID").val("0");
    $("#txtLocation").val("");
    $("#spanSaveUpdate").text("SAVE");
};
var fillDdlService = function () {
    $.ajax({
        url: '/ServiceRequest/GetDdlServiceCategory',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            $("#ddlProblemCategory").empty();
            $("#ddlProblemCategory").append("<option value='0'>Select Problem Category</option>");
            $.each(response.model, function (index, elementValue) {
                $("#ddlProblemCategory").append("<option value=" + elementValue.ServiceIssueID + ">" + elementValue.ServiceIssueString + "</option>");
            });
           
        }
    });
}
var fillCausgIssue = function (ServiceIssueID) {
    var params = { ServiceIssueID: ServiceIssueID };
    $.ajax({
        url: '/ServiceRequest/GetDdlCausingIssue',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                if (response.length != '0') {
                    $("#ddlProblemCategory1").empty();
                    $("#ddlProblemCategory1").append("<option value='0'>What Item Is causing The Issue?</option>");
                    $.each(response, function (index, elementValue) {
                        $("#ddlProblemCategory1").append("<option value=" + elementValue.CausingIssueID + ">" + elementValue.CausingIssue + "</option>");
                    });
                } else {
                    $("#CausingIssue").addClass("hidden");
                    $("#ddlProblemCategory1").empty();

                }
            }
        }
    });
}
