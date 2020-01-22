var goToEditProperty = function () {
    var row = $('#tblProperty tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location = "/Admin/PropertyManagement/EditProperty/" + ID;
    } 
};
var addNewProperty = function () {
    window.location = "/Admin/PropertyManagement/AddProperty";
};
var fillRPP_PropList = function () {
    $("#ddlRPP_PropertyList").empty();
    $("#ddlRPP_PropertyList").append("<option value='25'>25</option>");
    $("#ddlRPP_PropertyList").append("<option value='50'>50</option>");
    $("#ddlRPP_PropertyList").append("<option value='75'>75</option>");
    $("#ddlRPP_PropertyList").append("<option value='100'>100</option>");
    $("#ddlRPP_PropertyList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPropertyList($("#hdnCurrentPage").val());
    });
};
var buildPaganationPropertyList = function (pagenumber) {
    $("#divLoader").show();
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Title: $("#txtSearch").val(),
        State:$("#ddlStatePropertySearch").val(),
        City:$("#ddlCityPropertySearch").val(),
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_PropertyList").val()
    };
    $.ajax({
        url: '/Admin/PropertyManagement/BuildPaganationPropList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_PropertyList').addClass("hidden");
                }
                else {
                    $('#divPagination_PropertyList').removeClass("hidden");
                    $('#ulPagination_PropertyList').pagination('updateItems', response.NOP);
                    $('#ulPagination_PropertyList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillPropertyList = function (pagenumber) {
    $("#divLoader").show();
    var model = {
        Title: $("#txtSearch").val(),
        State: $("#ddlStatePropertySearch").val(),
        City: $("#ddlCityPropertySearch").val(),
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_PropertyList").val()
    };
    $.ajax({
        url: '/Admin/PropertyManagement/FillPropertySearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblProperty>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.PID + '">';
                 
                    html += '<td class="pds-2" style="color:#3d3939;">' + elementValue.Title + '</td>';
                    html += '<td class="pds-3" style="color:#3d3939;">' + elementValue.NoOfUnits + '</td>';
                    html += '<td class="pds-4" style="color:#3d3939;">' + elementValue.NoOfFloors + '</td>';
                    html += '<td class="pds-5"style="color:#3d3939;">' + elementValue.Location + '</td>';
          
                    html += '</tr>';
                    $("#tblProperty>tbody").append(html);
                });
            }
            $("#divLoader").hide();
        }
    });
};
var fillStateDropDownList = function () {
    $.ajax({
        url: '../City/FillStateDropDownList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlStatePropertySearch").empty();
                $("#ddlStatePropertySearch").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStatePropertySearch").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
};
var fillCityDropDownList = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '../City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlCityPropertySearch").empty();
                $("#ddlCityPropertySearch").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlCityPropertySearch").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_PropList();
    fillStateDropDownList();
    fillCityDropDownList();
    $('#ulPagination_PropertyList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage").val(1);
            buildPaganationPropertyList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillPropertyList(page);
        }
    });
    $('#tblProperty tbody').on('click', 'tr', function () {
        $('#tblProperty tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblProperty tbody').on('dblclick', 'tr', function () {
        goToEditProperty();
    });
    $("#ddlStatePropertySearch").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityDropDownList(selected);
        }
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationPropertyList(1);
    }
});

