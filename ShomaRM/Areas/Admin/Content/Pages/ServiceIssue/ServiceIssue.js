$(document).ready(function () {
    fillRPP_Issue();
    clearIssue();
    $('#tblIssue tbody').on('click', 'tr', function () {
        $('#tblIssue tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblIssue tbody').on('click', 'tr', function () {
        goToIssue();
    });

    getIssueData($("#hndIssueID").val());
    fillDdlService();

    $("#ddlProblemCategory").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            if ( selected == 0) {
                $("#ddlProblemCategory").empty();
            }
            else {
                fillCausingIssue(selected);
                $("#txtIssue").val();
            }
        }
    });

    $("#txtCriteriaIssue").keyup(function () {

    });
    $('#ulPagination_Issue').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            //console.log("Pagination_Init");
            buildPaganationIssueList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillCausingSearchGrid(page);
        }
    });
});
var buildPaganationIssueList = function (pagenumber) {

    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaIssue").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Issue").val()
    };
    $.ajax({
        url: 'ServiceIssue/BuildPaganationSLList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                //if (response.NOP == 0) {
                //    $('#ddlRPP_Issue').addClass("hidden");
                //    $('#divPagination_Issue').addClass("hidden");
                //    $('#lblRPP_Issue').addClass("hidden");
                //}
                //else {
                //    $('#ddlRPP_Issue').removeClass("hidden");
                //    $('#divPagination_Issue').removeClass("hidden");
                //    $('#lblRPP_Issue').removeClass("hidden");

                    $('#ulPagination_Issue').pagination('updateItems', response.NOP);
                    $('#ulPagination_Issue').pagination('selectPage', 1);
                }
            //}
        }
    });
};
var fillCausingSearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaIssue").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Issue").val()
    };
    $.ajax({
        url: 'ServiceIssue/fillServiceIssueSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblIssue>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.IssueID + '"  id="tr_' + elementValue.IssueID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.IssueID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ServiceIssue + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.CausingIssue + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Issue + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToIssue(' + elementValue.IssueID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delIssue(' + elementValue.IssueID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblIssue>tbody").append(html);
                    IssueDataSource.push({ IssueID: elementValue.IssueID, ServiceIssue: elementValue.ServiceIssue, CausingIssueID: elementValue.CausingIssueID, Issue: elementValue.Issue });
                });

            }
        }
    });
};
var fillRPP_Issue = function () {
    $("#ddlRPP_Issue").empty();
    $("#ddlRPP_Issue").append("<option value='25'>25</option>");
    $("#ddlRPP_Issue").append("<option value='50'>50</option>");
    $("#ddlRPP_Issue").append("<option value='75'>75</option>");
    $("#ddlRPP_Issue").append("<option value='100'>100</option>");
    $("#ddlRPP_Issue").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationIssueList($("#hdnCurrentPage").val());
    });
};
var IssueDataSource = [];
var goToIssue = function (IssueID) {
    var ID = IssueID;
    if (ID != null) {
        $("#hndIssueID").val(ID);
        getIssueData(ID);
        

    }
};
var clearIssue = function () {
    $("#hndIssueID").val("0");
    $("#ddlProblemCategory").val("0");
    $("#ddlCausingIssue").val("0");
    $("#txtIssue").val("");
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
var fillCausingIssue = function (ServiceIssueID) {
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
                    $("#ddlCausingIssue").empty();
                    //$("#ddlProblemCategory1").append("<option value='0'>What Item Is causing The Issue?</option>");
                    $.each(response, function (index, elementValue) {
                        $("#ddlCausingIssue").append("<option value=" + elementValue.CausingIssueID + ">" + elementValue.CausingIssue + "</option>");
                    });
                } else {
                  
                   
                }
            }
        }
    });
}
var getIssueData = function (IssueID) {
    var params = {IssueID:IssueID };
    $.ajax({
        url: "ServiceIssue/getIssueData",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearIssue();
            
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndIssueID").val(response.IssueID);
                $("#ddlProblemCategory").val(response.ServiceIssueID);
                //setTimeout(function () {
                    fillCausingIssue(response.ServiceIssueID);
                //}, 300);
                $("#ddlCausingIssue").val(response.CausingIssueID);
                $("#txtIssue").val(response.Issue);
                if ($("#hndIssueID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};

var saveUpdateIssueData = function () {
    ////showProgress('#btnSaveUpdate');

    var msg = "";
    if ($.trim($("#ddlProblemCategory").val()).length == 0) {
        msg = msg + "Service Category is required.\r\n"
    }
    if ($.trim($("#ddlCausingIssue").val()).length <= 0) {
        msg = msg + "Causing Issue is required.\r\n"
    }
    if ($.trim($("#txtIssue").val()).length <= 0) {
        msg = msg + " Issue is required.\r\n"
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
            IssueID: $("#hndIssueID").val(),
            Issue: $("#txtIssue").val(),
            ServiceIssueID: $("#ddlProblemCategory").val(),
            CausingIssueID: $("#ddlCausingIssue").val(),
        };
        alert($("#hndIssueID").val());
        $.ajax({
            url: "ServiceIssue/saveUpdateIssue",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndIssueID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue'
                        });
                        $("#hndIssueID").val(response.IssueID);
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
var delIssue = function (ID) {

    var model = {
        IssueID: ID
    };
    $.alert({
        title: "",
        content: "Are you sure to remove Service Issue?",
        type: 'red',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ServiceIssue/DeleteService",
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