var selectFieldCampaingDataSource = [
    { text: "First Name", value: "FirstName" },
    { text: "LastName", value: "LastName" },
];
var goToEditProspect = function () {
    var row = $('#tblProspect tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location = "/Admin/ProspectManagement/AddEdit/" + ID;
    }
};
var addNewProspect = function () {
    window.location.href = "../ProspectManagement/AddEdit/0";
};
var fillRPP_ProspectList = function () {
    $("#ddlRPP_ProspectList").empty();
    $("#ddlRPP_ProspectList").append("<option value='25'>25</option>");
    $("#ddlRPP_ProspectList").append("<option value='50'>50</option>");
    $("#ddlRPP_ProspectList").append("<option value='75'>75</option>");
    $("#ddlRPP_ProspectList").append("<option value='100'>100</option>");
    $("#ddlRPP_ProspectList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationProspectList($("#hdnCurrentPage").val());
    });
};
var buildPaganationProspectList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "VisitDateTime";
    }
    if (!orderby) {
        orderby = "DESC";
    }
    $("#divLoader").show();
    var searchtype = $("#hdnSearchType").val();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_ProspectList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Admin/ProspectManagement/BuildPaganationProspectList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_ProspectList').addClass("hidden");
                }
                else {
                    $('#divPagination_ProspectList').removeClass("hidden");
                    $('#ulPagination_ProspectList').pagination('updateItems', response.NOP);
                    $('#ulPagination_ProspectList').pagination('selectPage', 1);
                }
            }
            $("#divLoader").hide();
        }
    });
};
var fillProspectList = function (pagenumber, sortby, orderby) {
   
    if (!sortby) {
        sortby = "VisitDateTime";
    }
    if (!orderby) {
        orderby = "DESC";
    }
    $("#divLoader").show();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ProspectList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Admin/ProspectManagement/FillProspectSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblProspect>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.PID + ">";
                    html += "<td>" + elementValue.FirstName + "</td>";
                    html += "<td>" + elementValue.LastName + "</td>";
                    html += "<td>" + formatPhoneFax(elementValue.PhoneNo) + "</td>";
                    html += "<td>" + elementValue.EmailId + "</td>";
                    html += "<td>" + elementValue.VisitDateTime + "</td>";
                    html += "<td>" + elementValue.AssignAgentName + "</td>";
                    html += "<td>" + elementValue.AppointmentStatusString + "</td>";
                    html += "</tr>";
                    $("#tblProspect>tbody").append(html);
                });
                
            }
            $("#divLoader").hide();
        }
    });
};

$(document).ready(function () {
    fillRPP_ProspectList();   
    $('#ulPagination_ProspectList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage").val(1);
            buildPaganationProspectList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            var sortByValue = localStorage.getItem("SortByValueAppo");
            var OrderByValue = localStorage.getItem("OrderByValueAppo");
            fillProspectList(page, sortByValue, OrderByValue);
            
        }
    });
    $('#tblProspect tbody').on('click', 'tr', function () {
        $('#tblProspect tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblProspect tbody').on('dblclick', 'tr', function () {
        goToEditProspect();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationProspectList(1);
    }
});
var count = 0;
var sortTableAppo = function (sortby) {
    var orderby = "";

    var pagenumber = $("#hndPageNo").val();
    if (!pagenumber) {
        pagenumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        //$("#SortIconFN").removeClass('fa fa-sort-down');
        //$("#SortIconFN").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconLN").removeClass('fa fa-sort-down');
        //$("#SortIconLN").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconPNo").removeClass('fa fa-sort-down');
        //$("#SortIconPNo").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconEmail").removeClass('fa fa-sort-down');
        //$("#SortIconEmail").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconDate").removeClass('fa fa-sort-down');
        //$("#SortIconDate").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconAA").removeClass('fa fa-sort-down');
        //$("#SortIconAA").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconStatus").removeClass('fa fa-sort-down');
        //$("#SortIconStatus").removeClass('fa fa-sort-up fa-lg');
       
        if (sortby == 'FirstName') {
            $("#SortIconFN").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'LastName') {
            $("#SortIconLN").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'PhoneNo') {
            $("#SortIconPNo").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'EmailId') {
            $("#SortIconEmail").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'VisitDateTime') {
            $("#SortIconDate").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'AssignAgentId') {
            $("#SortIconAA").addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'AppointmentStatus') {
            $("#SortIconStatus").addClass('fa fa-sort-up fa-lg');
        }
   
    }
    else {
        orderby = "DESC";
        //$("#SortIconFN").removeClass('fa fa-sort-down');
        //$("#SortIconFN").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconLN").removeClass('fa fa-sort-down');
        //$("#SortIconLN").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconPNo").removeClass('fa fa-sort-down');
        //$("#SortIconPNo").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconEmail").removeClass('fa fa-sort-down');
        //$("#SortIconEmail").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconDate").removeClass('fa fa-sort-down');
        //$("#SortIconDate").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconAA").removeClass('fa fa-sort-down');
        //$("#SortIconAA").removeClass('fa fa-sort-up fa-lg');
        //$("#SortIconStatus").removeClass('fa fa-sort-down');
        //$("#SortIconStatus").removeClass('fa fa-sort-up fa-lg');
        if (sortby == 'FirstName') {
            $("#SortIconFN").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'LastName') {
            $("#SortIconLN").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'PhoneNo') {
            $("#SortIconPNo").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'EmailId') {
            $("#SortIconEmail").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'VisitDateTime') {
            $("#SortIconDate").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'AssignAgentId') {
            $("#SortIconAA").addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'AppointmentStatus') {
            $("#SortIconStatus").addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueAppo", sortby);
    localStorage.setItem("OrderByValueAppo", orderby);
    count++;
    buildPaganationProspectList(pagenumber, sortby, orderby);
    fillProspectList(pagenumber, sortby, orderby);
};



