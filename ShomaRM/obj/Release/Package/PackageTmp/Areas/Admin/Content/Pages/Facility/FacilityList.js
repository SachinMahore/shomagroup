var goToEditFacility = function () {
    var row = $('#tblFacility tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Facility/Edit/" + ID;
    }
};
var addNewFacility = function () {
    window.location.href = "/Admin/Facility/Edit/0";
};
var fillRPP_FacilityList = function () {
    $("#ddlRPP_FacilityList").empty();
    $("#ddlRPP_FacilityList").append("<option value='25'>25</option>");
    $("#ddlRPP_FacilityList").append("<option value='50'>50</option>");
    $("#ddlRPP_FacilityList").append("<option value='75'>75</option>");
    $("#ddlRPP_FacilityList").append("<option value='100'>100</option>");
    $("#ddlRPP_FacilityList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationFacilityList($("#hdnCurrentPage_EL").val());
    });
};
var buildPaganationFacilityList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_FacilityList").val()
    };
    $.ajax({
        url: "/Facility/BuildPaganationFacilityList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_FacilityList').addClass("hidden");
                }
                else {
                    $('#divPagination_FacilityList').removeClass("hidden");
                    $('#ulPagination_FacilityList').pagination('updateItems', response.NOP);
                    $('#ulPagination_FacilityList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillFacilityList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_FacilityList").val()
    };
    $.ajax({
        url: '/Facility/FillFacilitySearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblFacility>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.FacilityID + ">";
                    html += "<td>" + elementValue.FacilityName + "</td>";
                    html += "<td>" + elementValue.PropertyName + "</td>";
                    html += "<td align='center'><img src='/content/assets/img/Facility/" + elementValue.Photo+"' class='picture-src' title='' style='height:70px;width:70px;'/></td>";
                    html += "</tr>";
                    $("#tblFacility>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_FacilityList();
    $('#ulPagination_FacilityList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_EL").val(1);
            buildPaganationFacilityList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_EL").val(page);
            fillFacilityList(page);
        }
    });
    $('#tblFacility tbody').on('click', 'tr', function () {
        $('#tblFacility tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblFacility tbody').on('dblclick', 'tr', function () {
        goToEditFacility();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationFacilityList(1);
    }
});

