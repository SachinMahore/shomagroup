var fillRPP_CommunityList = function () {
    $("#ddlRPP_CommunityList").empty();
    $("#ddlRPP_CommunityList").append("<option value='25'>25</option>");
    $("#ddlRPP_CommunityList").append("<option value='50'>50</option>");
    $("#ddlRPP_CommunityList").append("<option value='75'>75</option>");
    $("#ddlRPP_CommunityList").append("<option value='100'>100</option>");
    $("#ddlRPP_CommunityList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationCommunityList($("#hdnCurrentPage_FL").val());
    });
};
var buildPaganationCommunityList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_CommunityList").val()
    };
    $.ajax({
        url: "/Admin/CommunityActivity/BuildPaganationCommunityList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_CommunityList').addClass("hidden");
                }
                else {
                    $('#divPagination_CommunityList').removeClass("hidden");
                    $('#ulPagination_CommunityList').pagination('updateItems', response.NOP);
                    $('#ulPagination_CommunityList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillCommunityList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_CommunityList").val()
    };
    $.ajax({
        url: '/Admin/CommunityManagment/FillCommunitySearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblCommunityActivity>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.CID + " data-nop=" + elementValue.NumberOfPages + ">";
                    html += "<td>" + elementValue.PostedBy + "</td>";
                    html += "<td>" + elementValue.Details + "</td>";
                    html += "<td>" + elementValue.PostedDate + "</td>";
                    html += "<td align='center'><button onclick='deletePost(" + elementValue.CID + ", " + elementValue.NumberOfPages +")'><i class='fa fa-trash'></i></button></td>";
                    html += "</tr>";
                    $("#tblCommunityActivity>tbody").append(html);
                });
                $("#divLoader").hide();
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_CommunityList();
    
    $('#ulPagination_CommunityList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_FL").val(1);
            buildPaganationCommunityList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_FL").val(page);
            fillCommunityList(page);
        }
    });
});

$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationCommunityList(1);
    }
});


var deletePost = function (id, pageNo) {
    $("#divLoader").show();
    var model = {
        CID: id
    };

    $.ajax({
        url: '/Admin/CommunityActivity/DeleteCommunityPost',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            fillCommunityList(pageNo);
        }
    });

};