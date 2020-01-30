var goToEditUser = function () {
    var row = $('#tblUser tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "users/edit/" + ID;
    }
};
var addNewUser = function () {
    window.location.href = "users/new";
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
var buildPaganationUserList = function (pagenumber) {
    var model = {
        Filter: $("#ddlFilter_UL").val(),
        Criteria: $("#txtCriteria_UL").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_UserList").val()
    };
    $.ajax({
        url: "/Users/BuildPaganationUserList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_UserList').addClass("hidden");
                }
                else {
                    $('#divPagination_UserList').removeClass("hidden");
                    $('#ulPagination_UserList').pagination('updateItems', response.NOP);
                    $('#ulPagination_UserList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillUserList = function (pagenumber) {
    var model = {
        Filter: $("#ddlFilter_UL").val(),
        Criteria: $("#txtCriteria_UL").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_UserList").val()
    };
    $.ajax({
        url: '/Users/FillUserSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(JSON.stringify(response));
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                var html = "";
                $("#tblUser>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    
                    var html = '';
                    html += '<tr data-value="' + elementValue.UserID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.UserID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.FirstName + '</td>';
                    html += '<td class="pds-lastname" style="color:#3d3939;">' + elementValue.LastName + '</td>';
                    html += '<td class="pds-username" style="color:#3d3939;">' + elementValue.Username + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.UserType + '</td>';
                    html += '<td class="pds-userstatus"style="color:#3d3939;">' + elementValue.Status + '</td>';
                    html += '</tr>';
                    $("#tblUser>tbody").append(html);
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
            fillUserList(page);
        }
    });
    $('#tblUser tbody').on('click', 'tr', function () {
        $('#tblUser tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblUser tbody').on('dblclick', 'tr', function () {
        goToEditUser();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationUserList(1);
    }
});
