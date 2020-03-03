var goToEditProspectVerify = function () {
    var row = $('#tblProspectVerify tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "../Admin/ProspectVerification/EditProspect/" + ID;
    }
};
var fillRPP_ProspectVerifyList = function () {
    $("#ddlRPP_ProspectVerifyList").empty();
    $("#ddlRPP_ProspectVerifyList").append("<option value='25'>25</option>");
    $("#ddlRPP_ProspectVerifyList").append("<option value='50'>50</option>");
    $("#ddlRPP_ProspectVerifyList").append("<option value='75'>75</option>");
    $("#ddlRPP_ProspectVerifyList").append("<option value='100'>100</option>");
    $("#ddlRPP_ProspectVerifyList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationProspectVerifyList($("#hdnCurrentPage_PVL").val());
    });
};
var buildPaganationProspectVerifyList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "FirstName";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_ProspectVerifyList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: "/ProspectVerification/BuildPaganationProspectVerifyList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_ProspectVerifyList').addClass("hidden");
                }
                else {
                    $('#divPagination_ProspectVerifyList').removeClass("hidden");
                    $('#ulPagination_ProspectVerifyList').pagination('updateItems', response.NOP);
                    $('#ulPagination_ProspectVerifyList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillProspectVerifyList = function (pagenumber, sortby, orderby) {
    $("#divLoader").show();
    if (!sortby) {
        sortby = "FirstName";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_ProspectVerifyList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/ProspectVerification/FillProspectVerifySearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
          //  console.log(JSON.stringify(response));
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                var html = "";
                $("#tblProspectVerify>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    html = "<tr data-value=" + elementValue.UserID + ">";
                    html += "<td>" + elementValue.FirstName + "</td>";
                    html += "<td>" + elementValue.LastName + "</td>";
                    html += "<td>" + formatPhoneFax(elementValue.Phone) + "</td>";
                    html += "<td>" + elementValue.Email + "</td>";
                    html += "<td>" + elementValue.CreatedDate + "</td>";
                    html += "<td>" + elementValue.UnitNo + "</td>";
                    html += "</tr>";
                    $("#tblProspectVerify>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
            $("#divLoader").hide();
        }
    });
};
$(document).ready(function () {
    fillRPP_ProspectVerifyList();
    $('#ulPagination_ProspectVerifyList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_PVL").val(1);
            buildPaganationProspectVerifyList(1);
        },
        onPageClick: function (page, evt) {
            var SortByValueProsVerification = localStorage.getItem("SortByValueProsVerification");
            var OrderByValueProsVerification = localStorage.getItem("OrderByValueProsVerification");
            $("#hdnCurrentPage_PVL").val(page);
            fillProspectVerifyList(page, SortByValueProsVerification, OrderByValueProsVerification);
        }
    });
    $('#tblProspectVerify tbody').on('click', 'tr', function () {
        $('#tblProspectVerify tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblProspectVerify tbody').on('dblclick', 'tr', function () {
        goToEditProspectVerify();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationProspectVerifyList(1);
    }
});
var createDropDown = function (docid, selectedid) {
    var ddl = "<select id='ddlVerificationStatus_" + docid + "' class='form-control form-control-small " + (docid == 0 ? "hidden" : "") + "' disabled>";
    ddl += "<option value='0'>Pending</option>";
    ddl += "<option value='1' " + (selectedid == 1 ? "selected='selected'" : "") + ">Qualified</option>";
    ddl += "<option value='2' " + (selectedid == 2 ? "selected='selected'" : "") + ">Disqualified</option>";
    ddl += "</select>"
    return ddl;
};


var count = 0;
var sortTableProspectVerification = function (sortby) {
   
    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }
    $('#sortFirstNameIcon').removeClass('fa fa-sort-up');
    $('#sortFirstNameIcon').removeClass('fa fa-sort-down');
    $('#sortLastNameIcon').removeClass('fa fa-sort-up');
    $('#sortLastNameIcon').removeClass('fa fa-sort-down');
    $('#sortPhoneNoIcon').removeClass('fa fa-sort-up');
    $('#sortPhoneNoIcon').removeClass('fa fa-sort-down');
    $('#sortEmailAddressIcon').removeClass('fa fa-sort-up');
    $('#sortEmailAddressIcon').removeClass('fa fa-sort-down');
    $('#sortApplyDateIcon').removeClass('fa fa-sort-up');
    $('#sortApplyDateIcon').removeClass('fa fa-sort-down');
    $('#sortUnitNoIcon').removeClass('fa fa-sort-up');
    $('#sortUnitNoIcon').removeClass('fa fa-sort-down');
    if (count % 2 == 1) {
        orderby = "ASC";
        if (sortby == 'FirstName') {
            $('#sortFirstNameIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'LastName') {
            $('#sortLastNameIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'PhoneNo') {
            $('#sortPhoneNoIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'EmailAddress') {
            $('#sortEmailAddressIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'ApplyDate') {
            $('#sortApplyDateIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'UnitNo') {
            $('#sortUnitNoIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        if (sortby == 'FirstName') {
            $('#sortFirstNameIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'LastName') {
            $('#sortLastNameIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'PhoneNo') {
            $('#sortPhoneNoIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'EmailAddress') {
            $('#sortEmailAddressIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'ApplyDate') {
            $('#sortApplyDateIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'UnitNo') {
            $('#sortUnitNoIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueProsVerification", sortby);
    localStorage.setItem("OrderByValueProsVerification", orderby);
    count++;
    buildPaganationProspectVerifyList(pNumber, sortby, orderby);
    fillProspectVerifyList(pNumber, sortby, orderby);
};
