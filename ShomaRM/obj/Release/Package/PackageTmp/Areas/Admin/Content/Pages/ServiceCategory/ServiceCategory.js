var SCategoryDataSource = [];
var goToServiceData = function (ServiceCategoryID) {
    var ID = ServiceCategoryID;
    if (ID != null) {
        $("#hndServiceCategoryID").val(ID);
        getServiceCategoryData(ID);
    }
};

$(document).ready(function () {
    fillRPP_ServiceCategory();
    clearServiceCategory();
    $('#tblServiceCategory tbody').on('click', 'tr', function () {
        $('#tblServiceCategory tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblServiceCategory tbody').on('click', 'tr', function () {
        goToServiceData();
    });

    getServiceCategoryData($("#hndServiceCategoryID").val());

  
    $("#txtCriteriaServiceCategory").keyup(function () {

    });
    $('#ulPagination_ServiceCategory').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            //console.log("Pagination_Init");
            buildPaganationScategoryList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillSCategorySearchGrid(page);
        }
    });
});

var fillRPP_ServiceCategory = function () {
    $("#ddlRPP_ServiceCategory").empty();
    $("#ddlRPP_ServiceCategory").append("<option value='25'>25</option>");
    $("#ddlRPP_ServiceCategory").append("<option value='50'>50</option>");
    $("#ddlRPP_ServiceCategory").append("<option value='75'>75</option>");
    $("#ddlRPP_ServiceCategory").append("<option value='100'>100</option>");
    $("#ddlRPP_ServiceCategory").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationScategoryList($("#hdnCurrentPage").val());
    });
};
var buildPaganationScategoryList = function (pagenumber) {

    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaServiceCategory").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ServiceCategory").val()
    };
    $.ajax({
        url: 'ServiceCategory/buildPaganationScategoryList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                //if (response.NOP == 0) {
                //    $('#ddlRPP_ServiceCategory').addClass("hidden");
                //    $('#divPagination_ServiceCategory').addClass("hidden");
                //    $('#lblRPP_ServiceCategory').addClass("hidden");
                //}
                //else {
                    //$('#ddlRPP_ServiceCategory').removeClass("hidden");
                    //$('#divPagination_ServiceCategory').removeClass("hidden");
                    //$('#lblRPP_ServiceCategory').removeClass("hidden");

                    $('#ulPagination_ServiceCategory').pagination('updateItems', response.NOP);
                    $('#ulPagination_ServiceCategory').pagination('selectPage', 1);
                //}
            }
        }
    });
};
var fillSCategorySearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaServiceCategory").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ServiceCategory").val()
    };
    $.ajax({
        url: 'ServiceCategory/fillSCategorySearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblServiceCategory>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ServiceIssueID + '"  id="tr_' + elementValue.ServiceIssueID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ServiceIssueID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ServiceIssue + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToServiceData(' + elementValue.ServiceIssueID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delServiceCategory(' + elementValue.ServiceIssueID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblServiceCategory>tbody").append(html);
                    SCategoryDataSource.push({ ServiceIssueID: elementValue.ServiceIssueID, ServiceIssue: elementValue.ServiceIssueID });
                });

            }
        }
    });
};
var clearServiceCategory = function () {
    $("#hndServiceCategoryID").val("0");
    $("#txtServiceCategory").val("");
    $("#spanSaveUpdate").text("SAVE");
};
var getServiceCategoryData = function (ServiceCategoryID) {
    var params = { ServiceCategoryID: ServiceCategoryID };
    $.ajax({
        url: "ServiceCategory/GetServiceCategoryInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearServiceCategory();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
            
                $("#hndServiceCategoryID").val(response.ServiceIssueID);
                $("#txtServiceCategory").val(response.ServiceIssue);
                if ($("#hndServiceCategoryID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};
var saveUpdateServiceCategory = function () {
    ////showProgress('#btnSaveUpdate');

    var msg = "";
    if ($.trim($("#txtServiceCategory").val()).length <= 0) {
        msg = msg + "Service Category is required.\r\n"
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
            ServiceIssueID: $("#hndServiceCategoryID").val(),
            ServiceIssue: $("#txtServiceCategory").val(),

        };
     
        $.ajax({
            url: "ServiceCategory/SaveUpdateServiceCategory",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndServiceCategoryID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue'
                        });
                        $("#hndServiceCategoryID").val(response.ServiceCategoryID);
                        $("#spanSaveUpdate").text("Update");
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue'
                        });
                    }
                    fillSCategorySearchGrid($("#hdnCurrentPage").val());
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
var delServiceCategory = function (ID) {

    var model = {
        ServiceIssueID: ID
    };
    $.alert({
        title: "",
        content: "Are you sure to remove Service Category?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ServiceCategory/DeleteServiceCategory",
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