var goToEditNotice = function () {
    var row = $('#tblNotice tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Notice/Edit/" + ID;
    }
};
var addNewNotice = function () {
    window.location.href = "/Admin/Notice/Edit/0";
};
var fillRPP_NoticeList = function () {
    $("#ddlRPP_NoticeList").empty();
    $("#ddlRPP_NoticeList").append("<option value='25'>25</option>");
    $("#ddlRPP_NoticeList").append("<option value='50'>50</option>");
    $("#ddlRPP_NoticeList").append("<option value='75'>75</option>");
    $("#ddlRPP_NoticeList").append("<option value='100'>100</option>");
    $("#ddlRPP_NoticeList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationNoticeList($("#hdnCurrentPage_NL").val());
    });
};
var buildPaganationNoticeList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_NoticeList").val()
    };
    $.ajax({
        url: "/Notice/BuildPaganationNoticeList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_NoticeList').addClass("hidden");
                }
                else {
                    $('#divPagination_NoticeList').removeClass("hidden");
                    $('#ulPagination_NoticeList').pagination('updateItems', response.NOP);
                    $('#ulPagination_NoticeList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillNoticeList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_NoticeList").val()
    };
    $.ajax({
        url: '/Notice/FillNoticeSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblNotice>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.NoticeID + ">";
                    html += "<td>" + elementValue.PropertyName + "</td>";
                    html += "<td>" + elementValue.UnitName + "</td>";
                    html += "<td>" + elementValue.TenantName + "</td>";
                    html += "<td>" + elementValue.Revision_Num + "</td>";
                    html += "<td>" + elementValue.NoticeDate + "</td>";
                    html += "</tr>";
                    $("#tblNotice>tbody").append(html);
                });
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_NoticeList();
    $('#ulPagination_NoticeList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_NL").val(1);
            buildPaganationNoticeList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_NL").val(page);
            fillNoticeList(page);
        }
    });
    $('#tblNotice tbody').on('click', 'tr', function () {
        $('#tblNotice tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblNotice tbody').on('dblclick', 'tr', function () {
        goToEditNotice();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationNoticeList(1);
    }
});

