var selectFieldCampaingDataSource = [
    { text: "First Name", value: "FirstName" },
    { text: "LastName", value: "LastName" },
];
var goToEditProspect = function () {
    var row = $('#tblProspect tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "../ProspectManagement/AddEdit/" + ID;
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
var buildPaganationProspectList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_ProspectList").val()
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
        }
    });
};
var fillProspectList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ProspectList").val()
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
                    html += "</tr>";
                    $("#tblProspect>tbody").append(html);
                });
            }
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
            fillProspectList(page);
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



