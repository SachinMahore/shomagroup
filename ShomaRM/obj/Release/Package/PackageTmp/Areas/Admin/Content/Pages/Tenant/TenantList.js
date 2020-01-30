$(document).ready(function () {
    fillRPP_TenantList();
    $('#ulPagination_TenantList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationTenantList(1);
        },
        onPageClick: function (page, evt) {
            fillTenantList(page, $("#hdnSearchType").val());
            $("#hdnCurrentPage").val(page);
        }
    });
    //fillTenantList(1, $("#hdnSearchType").val());
    $('#tblTenant tbody').on('click', 'tr', function () {
        $('#tblTenant tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblTenant tbody').on('dblclick', 'tr', function () {
        goToEditTenant();
    });

    $("#ddlProperty_TL").on('change', function (evt, params) {
        var propertyID = $(this).val();
        if (propertyID !== null) {
            fillUnitsByPropertyIdDDL_TL(propertyID);
        }
    });

    $("#ddlState_TL").on('change', function (evt, params) {
        var stateID = $(this).val();
        if (stateID !== null) {
            fillCityDDL_TL(stateID, "homestate");
        }
    });

    $("#ddlOfficeState_TL").on('change', function (evt, params) {
        var stateID = $(this).val();
        if (stateID !== null) {
            fillCityDDL_TL(stateID, "officestate");
        }
    });

    $("#ddlMaritalStatus_TL").empty();
    $("#ddlMaritalStatus_TL").append("<option value='0'>Both</option>");
    $("#ddlMaritalStatus_TL").append("<option value='1'>Married</option>");
    $("#ddlMaritalStatus_TL").append("<option value='2'>Unmarried</option>");

    $("#ddlGender_TL").empty();
    $("#ddlGender_TL").append("<option value='0'>Both</option>");
    $("#ddlGender_TL").append("<option value='1'>Male</option>");
    $("#ddlGender_TL").append("<option value='2'>Female</option>");
    $('#tblTenant tbody').on('click', 'tr', function () {
        $('#tblTenant tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    //$('#tblTenant tbody').on('dblclick', 'tr', function () {
    //    goToEditTenant();
    //});
    fillStateDDL_TL();
    fillCityDDL_TL(0, '');
    fillPropertyDDL_TL();
    fillUnitsByPropertyIdDDL_TL(0);
    
});
var selectFieldCampaingDataSource = [
    { text: "First Name", value: "FirstName" },
    { text: "LastName", value: "LastName" }
];
var goToEditTenant = function () {
    var row = $('#tblTenant tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    
    if (ID !== null) {
        window.location.href = "../Tenant/Edit/" + ID;
        localStorage.setItem("NewTenantID", ID);
    }
};
var addNewTenant = function () {
    window.location.href = "../Tenant/New";
};
var fillTenantList = function (pagenumber, searchtype) {
    var model = {
        FirstName: (searchtype == "1" ? "" : $("#txtFirstName_TL").val()),
        LastName: (searchtype == "1" ? "" : $("#txtLastName_TL").val()),
        Gender: (searchtype == "1" ? "0" : $("#ddlGender_TL").val()),
        MaritalStatus: (searchtype == "1" ? "0" : $("#ddlMaritalStatus_TL").val()),
        State: (searchtype == "1" ? "0" : $("#ddlState_TL").val()),
        City: (searchtype == "1" ? "0" : $("#ddlCity_TL").val()),
        PropertyID: (searchtype == "1" ? "0" : $("#ddlProperty_TL").val()),
        UnitID: (searchtype == "1" ? "0" : $("#ddlUnit_TL").val()),
        SocialSecurityNum: (searchtype == "1" ? "" : $("#txtSocialSecurity_TL").val()),
        Occupation: (searchtype == "1" ? "" : $("#txtOccupation_TL").val()),
        OfficeState: (searchtype == "1" ? "0" : $("#ddlOfficeState_TL").val()),
        OfficeCity: (searchtype == "1" ? "0" : $("#ddlOfficeCity_TL").val()),
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_TenantList").val()
    };
    $.ajax({
        url: '../Tenant/GetTenantList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblTenant>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.TenantID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.TenantID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.FirstName + '</td>';
                    html += '<td class="pds-lastname" style="color:#3d3939;">' + elementValue.LastName + '</td>';
                    html += '<td class="pds-Tenantname" style="color:#3d3939;">' + elementValue.Property + '</td>';
                    html += '<td class="pds-Tenanttype"style="color:#3d3939;">' + elementValue.Unit + '</td>';
                    html += '</tr>';
                    $("#tblTenant>tbody").append(html);
                });
            }
        }
    });
};
var fillRPP_TenantList = function () {
    $("#ddlRPP_TenantList").empty();
    $("#ddlRPP_TenantList").append("<option value='25'>25</option>");
    $("#ddlRPP_TenantList").append("<option value='50'>50</option>");
    $("#ddlRPP_TenantList").append("<option value='75'>75</option>");
    $("#ddlRPP_TenantList").append("<option value='100'>100</option>");
    $("#ddlRPP_TenantList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationTenantList($("#hdnCurrentPage").val());
    });
};
var buildPaganationTenantList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        FirstName: (searchtype === "1" ? "" : $("#txtFirstName_TL").val()),
        LastName: (searchtype === "1" ? "" : $("#txtLastName_TL").val()),
        Gender: (searchtype === "1" ? "0" : $("#ddlGender_TL").val()),
        MaritalStatus: (searchtype === "1" ? "0" : $("#ddlMaritalStatus_TL").val()),
        State: (searchtype === "1" ? "0" : $("#ddlState_TL").val()),
        City: (searchtype === "1" ? "0" : $("#ddlCity_TL").val()),
        PropertyID: (searchtype === "1" ? "0" : $("#ddlProperty_TL").val()),
        UnitID: (searchtype === "1" ? "0" : $("#ddlUnit_TL").val()),
        SocialSecurityNum: (searchtype === "1" ? "" : $("#txtSocialSecurity_TL").val()),
        Occupation: (searchtype === "1" ? "" : $("#txtOccupation_TL").val()),
        OfficeState: (searchtype === "1" ? "0" : $("#ddlOfficeState_TL").val()),
        OfficeCity: (searchtype === "1" ? "0" : $("#ddlOfficeCity_TL").val()),
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_TenantList").val()
    };
    $.ajax({
        url: '../Tenant/BuildPaganationTenantList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPaginationControl_TenantList').addClass("hidden");
                }
                else {
                    $('#ulPagination_TenantList').pagination('updateItems', response.NOP);
                    $('#ulPagination_TenantList').pagination('selectPage', 1);
                    $('#divPaginationControl_TenantList').removeClass("hidden");
                }
            }
        }
    });
};
var openAdvSearchPopup_TL = function () {
    $("#divAdvanceSearch_TL").modal('show');
};
var closeTenantAdvSearch = function () {
    $("#divAdvanceSearch_TL").modal('hide');
};
var fillStateDDL_TL = function () {
    $.ajax({
        url: '/City/FillStateDropDownList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#ddlState_TL").empty();
                $("#ddlState_TL").append("<option value='0'>All States</option>");
                $("#ddlOfficeState_TL").empty();
                $("#ddlOfficeState_TL").append("<option value='0'>All States</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState_TL").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlOfficeState_TL").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
};
var fillCityDDL_TL = function (stateid, statetype) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                if (stateid === 0) {
                    $("#ddlCity_TL").empty();
                    $("#ddlOfficeCity_TL").empty();
                    $("#ddlCity_TL").append("<option value='0'>All Cities</option>");
                    $("#ddlOfficeCity_TL").append("<option value='0'>All Cities</option>");
                }
                if (statetype === "homestate") {
                    $("#ddlCity_TL").empty();
                    $("#ddlCity_TL").append("<option value='0'>All Cities</option>");
                }
                if (statetype === "officestate") {
                    $("#ddlOfficeCity_TL").empty();
                    $("#ddlOfficeCity_TL").append("<option value='0'>All Cities</option>");
                }
                $.each(response, function (index, elementValue) {
                    if (statetype === "homestate") {
                        $("#ddlCity_TL").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                    }
                    if (statetype === "officestate") {
                        $("#ddlOfficeCity_TL").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                    }
                });
            }
        }
    });
};
var fillPropertyDDL_TL = function () {
    $.ajax({
        url: "/PropertyManagement/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlProperty_TL").empty();
            $("#ddlProperty_TL").append("<option value='0'>All Properties</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                $("#ddlProperty_TL").append(option);
            });
        }
    });
};
var fillUnitsByPropertyIdDDL_TL = function (pid) {
    var model = { PID: pid };
    $.ajax({
        url: "/PropertyManagement/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlUnit_TL").empty();
            $("#ddlUnit_TL").append("<option value='0'>All Units</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                $("#ddlUnit_TL").append(option);
            });
        }
    });
};
var btnAdvSearchTLClick = function () {
    $("#hdnSearchType").val(0);
    buildPaganationTenantList();
    $("#divAdvanceSearch_TL").modal('hide');
};
var btnSeachClick = function () {
    $("#hdnSearchType").val(1);
    buildPaganationTenantList();
};

$(document).keypress(function (e) {
    if (e.which === 13) {
        //fillTenantList();
        buildPaganationTenantList();
    }
});

