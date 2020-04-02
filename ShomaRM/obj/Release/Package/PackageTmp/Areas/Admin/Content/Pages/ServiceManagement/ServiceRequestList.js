var goToEdit = function () {
    var row = $('#tblServiceRequest tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {

        $('#tblServiceRequest tbody tr').removeClass('pds-selected-row');
        window.location.href=("../../ServicesManagement/Edit/" + ID);
    }
};
var addNewUser = function () {
    window.location.href = "users/new";
};
var ServiceList = function () {
    window.location.href = ("../../ServicesManagement/Index/");
};
var fillRPP_UserList = function () {
    $("#ddlRPP_UserList").empty();
    $("#ddlRPP_UserList").append("<option value='25'>25</option>");
    $("#ddlRPP_UserList").append("<option value='50'>50</option>");
    $("#ddlRPP_UserList").append("<option value='75'>75</option>");
    $("#ddlRPP_UserList").append("<option value='100'>100</option>");
    $("#ddlRPP_UserList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationUserList($("#hdnCurrentPage_PVL").val());
    });
};
var buildPaganationUserList = function (pagenumber, sortbyL, orderbyL) {
    if (!sortbyL) {
        sortbyL = "PermissionComeDate";
    }
    if (!orderbyL) {
        orderbyL = "ASC";
    }
    $("#divLoader").show();
    var selected = $("#ddlCriteria").find(":Selected").val();
    if (selected == 1) {
        var criteria = $("#txtCriteria").val()
    } else {
        var criteria = 'Unit' + ' ' + $("#txtCriteria").val()
    }
    
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        Piority: 0,
        Status: $("#ddlStatus").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_UserList").val(),
        Criteria: criteria,
        SortBy: sortbyL,
        OrderBy: orderbyL
    };
    $.ajax({
        url: "/ServicesManagement/BuildPaganationUserList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
               var erro=response.error;
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_UserList').addClass("hidden");
                    $("#tblServiceRequest>tbody").empty();
                }
                else {
                    $('#divPagination_UserList').removeClass("hidden");
                    $('#ulPagination_UserList').pagination('updateItems', response.NOP);
                    $('#ulPagination_UserList').pagination('selectPage', 1);
                }
            }
            $("#divLoader").hide();
        }
         
    });
};
var fillUserList = function (pagenumber, sortbyL, orderbyL) {
    if (!sortbyL) {
        sortbyL = "PermissionComeDate";
    }
    if (!orderbyL) {
        orderbyL = "ASC";
    }
    $("#divLoader").show();
    var selectedValue = $("#ddlCriteria").find(":selected").data("value");
    var selected = $("#ddlCriteria").find(":Selected").val();
    if (selected == 1) {
        var criteria = $("#txtCriteria").val();
    } else {
        var criteria = 'Unit' + ' ' + $("#txtCriteria").val();
    }
    
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        Piority:0,
        Status: $("#ddlStatus").val(),	
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_UserList").val(),
        Criteria: criteria,
        SortBy: sortbyL,
        OrderBy: orderbyL
    };
 
    $.ajax({
        url: '/ServicesManagement/FillServicesSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            $("#tblServiceRequest>tbody").empty();
            if ($.trim(response.error) !== "") {
                var error = response.error;
            } else {
                $("#tblServiceRequest>tbody").empty();
                $.each(response.model, function (elementType, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ServiceID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ServiceID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.TenantName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.UnitNo+ '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Problemcategory + '</td>';
                    html += '<td class="pds-lastname" style="color:#3d3939;">' + elementValue.ItemCaussing + '</td>';
                    html += '<td class="pds-username" style="color:#3d3939;">' + elementValue.ItemIssue + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.Location + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.PriorityString + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.StatusString + '</td>';
                   // html += '<td class="pds-usertype"style="color:#3d3939;"><button class="btn btn-primary " style="padding: 5px 8px !important;margin-right:7px" onclick="goToServiceDetails(' + elementValue.ServiceID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button></button></td>';
                    html += '</tr>';
                    $("#tblServiceRequest>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_UserList();
    $('#ulPagination_UserList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_PVL").val(1);
            buildPaganationUserList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_PVL").val(page);
            var sortByValue = localStorage.getItem("SortByValueSerReqA");
            var OrderByValue = localStorage.getItem("OrderByValueSerReqA");
            fillUserList(page, sortByValue, OrderByValue);
          
        }
    });
    $('#tblServiceRequest tbody').on('click', 'tr', function () {
        $('#tblUser tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblServiceRequest tbody').on('dblclick', 'tr', function () {
       goToEdit();
    });
    $("#txtCriteria").val('');
    $("#ddlCriteria").val('1');
    fillDdlUser();
    //$("#ddlCriteria").on('change', function (evt, params) {
    //    var selected = $(this).find(":Selected").val();
    //    var selectedData = $(this).find(":selected").data("value");
    //    if (selected != null) {
    //        if (selected == 1) {
                
    //        }
    //        else 
    //        {
              
    //        }
    //    }
    //});

    $("#ddlCriteria").on('change', function ()
    {
        $("#txtCriteria").val('');
        if ($(this).val() == 2) {
            $("#txtCriteria").keypress(function () {
                if ($("#ddlCriteria").val() == 2) {
                var k = isOnlyNumber($(this).event);
                if (k == false) {
                    return false;
                }
                else {
                    return true;
                }
                }
            });
        }
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationUserList(1);
    }
});






var fillDdlUser = function () {
    var param = { UserType: 7 };
    $.ajax({
        url: '/Users/GetUserListByType',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlUser").empty();
                $("#ddlUser").append("<option value='0'>Select Service Person</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlUser").append("<option value=" + elementValue.UserID + ">" + elementValue.FirstName + ' ' + elementValue.LastName + "</option>");
                });
            }
        }
    });
}

var count = 0;
var sortTableSerReqA = function (sortby) {
    var orderby = "";
    var pagenumber = $("#hndPageNo").val();
    if (!pagenumber) {
        pagenumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $("#SortTname").removeClass('fa fa-sort-up');
        $("#SortTname").removeClass('fa fa-sort-down');
        $("#SortUnNo").removeClass('fa fa-sort-up');
        $("#SortUnNo").removeClass('fa fa-sort-down');
        $("#SortPrCat").removeClass('fa fa-sort-up');
        $("#SortPrCat").removeClass('fa fa-sort-down');
        $("#SortPrCau").removeClass('fa fa-sort-up');
        $("#SortPrCau").removeClass('fa fa-sort-down');
        $("#SortProIssue").removeClass('fa fa-sort-up');
        $("#SortProIssue").removeClass('fa fa-sort-down');
        $("#SortLoc").removeClass('fa fa-sort-up');
        $("#SortLoc").removeClass('fa fa-sort-down');
        $("#SortPrio").removeClass('fa fa-sort-up');
        $("#SortPrio").removeClass('fa fa-sort-down');
        $("#SortStatu").removeClass('fa fa-sort-up');
        $("#SortStatu").removeClass('fa fa-sort-down');
        if (sortby == 'TenantName') {
            $("#SortTname").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'UnitNo') {
            $("#SortUnNo").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'ProblemCategory') {
            $("#SortPrCat").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'ItemCaussing') {
            $("#SortPrCau").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'ItemIssue') {
            $("#SortProIssue").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Location') {
            $("#SortLoc").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Priority') {
            $("#SortPrio").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Status') {
            $("#SortStatu").addClass('fa fa-sort-up fa-lg');
        }
       
    }
    else {
        orderby = "DESC";
        $("#SortTname").removeClass('fa fa-sort-up');
        $("#SortTname").removeClass('fa fa-sort-down');
        $("#SortUnNo").removeClass('fa fa-sort-up');
        $("#SortUnNo").removeClass('fa fa-sort-down');
        $("#SortPrCat").removeClass('fa fa-sort-up');
        $("#SortPrCat").removeClass('fa fa-sort-down');
        $("#SortPrCau").removeClass('fa fa-sort-up');
        $("#SortPrCau").removeClass('fa fa-sort-down');
        $("#SortProIssue").removeClass('fa fa-sort-up');
        $("#SortProIssue").removeClass('fa fa-sort-down');
        $("#SortLoc").removeClass('fa fa-sort-up');
        $("#SortLoc").removeClass('fa fa-sort-down');
        $("#SortPrio").removeClass('fa fa-sort-up');
        $("#SortPrio").removeClass('fa fa-sort-down');
        $("#SortStatu").removeClass('fa fa-sort-up');
        $("#SortStatu").removeClass('fa fa-sort-down');
        if (sortby == 'TenantName') {
            $("#SortTname").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'UnitNo') {
            $("#SortUnNo").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'ProblemCategory') {
            $("#SortPrCat").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'ItemCaussing') {
            $("#SortPrCau").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'ItemIssue') {
            $("#SortProIssue").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Location') {
            $("#SortLoc").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Priority') {
            $("#SortPrio").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Status') {
            $("#SortStatu").addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueSerReqA", sortby);
    localStorage.setItem("OrderByValueSerReqA", orderby);
    count++;
    buildPaganationUserList(pagenumber, sortby, orderby);
    fillUserList(pagenumber, sortby, orderby);
};