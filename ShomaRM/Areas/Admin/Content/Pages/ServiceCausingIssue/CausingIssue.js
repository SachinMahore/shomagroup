$(document).ready(function () {
    fillRPP_CausingIssue();
    clearCausingIssue();
    $('#tblCausingIssue tbody').on('click', 'tr', function () {

        $('#tblCausingIssue tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblCausingIssue tbody').on('click', 'tr', function () {
        goTocausing();
    });

    getCausingData($("#hndCausingIssueID").val());

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
            var sortByValue = localStorage.getItem("SortByValue");
            var OrderByValue = localStorage.getItem("OrderByValue");
            fillCausingSearchGrid(page, sortByValue, OrderByValue);
            
        }
    });
});
var buildPaganationCausingIssueList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "ServiceIssue";
    }
    if (!orderby) {
        orderby = "ASC";
    }

    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaCausingIssue").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_CausingIssue").val(),
        SortBy: sortby,
        OrderBy: orderby
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
                //if (response.NOP == 0) {
                //    $('#ddlRPP_CausingIssue').addClass("hidden");
                //    $('#divPagination_CausingIssue').addClass("hidden");
                //    $('#lblRPP_CausingIssue').addClass("hidden");
                //}
                //else {
                //    $('#ddlRPP_CausingIssue').removeClass("hidden");
                //    $('#divPagination_CausingIssue').removeClass("hidden");
                //    $('#lblRPP_CausingIssue').removeClass("hidden");

                    $('#ulPagination_CausingIssue').pagination('updateItems', response.NOP);
                    $('#ulPagination_CausingIssue').pagination('selectPage', 1);
                //}
            }
        }
    });
};
var fillCausingSearchGrid = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "ServiceIssue";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        Criteria: $("#txtCriteriaCausingIssue").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_CausingIssue").val(),
        SortBy: sortby,
        OrderBy: orderby
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

                    html += '<tr data-value="' + elementValue.CausingIssueID + '"  id="tr_' + elementValue.CausingIssueID + '">';

                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.CausingIssueID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ServiceIssue + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.CausingIssue + '</td>';
                   // html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ServiceIssue + '</td>';

                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goTocausing(' + elementValue.CausingIssueID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delCausingIssue(' + elementValue.CausingIssueID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';

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

var goTocausing = function (CausingIssueID) {
    var ID = CausingIssueID;
    if (ID != null) {
        $("#hndCausingIssueID").val(ID);
        getCausingData(ID);

    }
};
var clearCausingIssue = function () {
    $("#hndCausingIssueID").val("0");
    $("#ddlProblemCategory").val("0");
    $("#txtCausingIssue").val("");

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

var getCausingData = function (CausingIssueID) {
    var params = { CausingIssueID: CausingIssueID };
    $.ajax({
        url: "ServiceCausing/getCausingData",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearCausingIssue();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndCausingIssueID").val(response.CausingIssueID);
                $("#ddlProblemCategory").val(response.ServiceIssueID);
                $("#txtCausingIssue").val(response.CausingIssue);
                if ($("#hndCausingIssueID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};

var saveUpdateCausingData = function () {
    ////showProgress('#btnSaveUpdate');

    var msg = "";
    if ($.trim($("#ddlProblemCategory").val()).length == 0) {
        msg = msg + "Service Category is required.\r\n</br>"
    }
    if ($.trim($("#txtCausingIssue").val()).length <= 0) {
        msg = msg + "Causing Issue is required.\r\n</br>"
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'blue',
        });
    }
    else {
        var model = {
            CausingIssueID: $("#hndCausingIssueID").val(),
            CausingIssue: $("#txtCausingIssue").val(),
            ServiceIssueID: $("#ddlProblemCategory").val(),

        };
        alert($("#hndCausingIssueID").val());
        $.ajax({
            url: "ServiceCausing/saveUpdateCausing",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndCausingIssueID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue'
                        });
                        $("#hndCausingIssueID").val(response.CausingIssueID);
                        $("#spanSaveUpdate").text("Update");
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue'
                        });
                    }
                    fillCausingSearchGrid($("#hdnCurrentPage").val());
                }
                else {
                    $.alert({
                        title: 'Message!',
                        content: response.error,
                        type: 'blue'
                    });
                }
            }
        });
    }
};
var delCausingIssue = function (ID) {

    var model = {
        CausingIssueID: ID
    };
    $.alert({
        title: "",
        content: "Are you sure to remove Service Causing Issue?",
        type: 'red',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ServiceCausing/DeleteServiceCausing",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + ID).remove();
                        }
                    });
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                }
            }
        }
    });
};

var count = 0;
var sortTableSC = function (sortby) {
    var orderby = "";
    var pagenumber = $("#hndPageNo").val();
    if (!pagenumber) {
        pagenumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC"; 
        $("#SortIconSC").removeClass('fa fa-sort-up');
        $("#SortIconSC").removeClass('fa fa-sort-down');
        $("#SortIconCS").removeClass('fa fa-sort-up');
        $("#SortIconCS").removeClass('fa fa-sort-down');
        if (sortby == 'ServiceIssue') {
            $("#SortIconSC").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'CausingIssue') {
            $("#SortIconCS").addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $("#SortIconSC").removeClass('fa fa-sort-up');
        $("#SortIconSC").removeClass('fa fa-sort-down');
        $("#SortIconCS").removeClass('fa fa-sort-up');
        $("#SortIconCS").removeClass('fa fa-sort-down');
        if (sortby == 'ServiceIssue') {
            $("#SortIconSC").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'CausingIssue') {
            $("#SortIconCS").addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValue", sortby);
    localStorage.setItem("OrderByValue", orderby);
    count++;
    buildPaganationCausingIssueList(pagenumber, sortby, orderby);
    fillCausingSearchGrid(pagenumber, sortby, orderby);
};
