//--------------------------------------------Search------------------------------------------------------//
var amenitiesDataSource = [];
var goToUtility = function (utilityID) {
    var UtilityID = utilityID;
    if (UtilityID != null) {
        $("#hndUtilityID").val(UtilityID);
        getAmenityData(UtilityID);
    }
}
$(document).keypress(function (e) {
    if (e.which == 13) {
        buildPaganationUtilityList(1);
    }
});
var fillUtilityList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'UtilityName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        Criteria: $("#txtCriteriaUtility").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Utility").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: 'Utility/GetUtilityList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblUtility>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.UtilityID + '" id="tr_' + elementValue.UtilityID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.UtilityID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.UtilityTitle + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToUtility(' + elementValue.UtilityID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delUtility(' + elementValue.UtilityID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblUtility>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
        }
    });
};
var fillRPP_Utility = function () {
    $("#ddlRPP_Utility").empty();
    $("#ddlRPP_Utility").append("<option value='25'>25</option>");
    $("#ddlRPP_Utility").append("<option value='50'>50</option>");
    $("#ddlRPP_Utility").append("<option value='75'>75</option>");
    $("#ddlRPP_Utility").append("<option value='100'>100</option>");
    $("#ddlRPP_Utility").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationUtilityList($("#hdnCurrentPage").val());
    });
};
var buildPaganationUtilityList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'UtilityName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaUtility").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Utility").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: 'Utility/BuildPaganationUtilityList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#lblRPP_Utility').addClass("hidden");
                    $('#ddlRPP_Utility').addClass("hidden");
                    $('#divPagination_Utility').addClass("hidden");
                }
                else {
                    $('#lblRPP_Utility').removeClass("hidden");
                    $('#ddlRPP_Utility').removeClass("hidden");
                    $('#divPagination_Utility').removeClass("hidden");
                    $('#ulPagination_Utility').pagination('updateItems', response.NOP);
                    $('#ulPagination_Utility').pagination('selectPage', 1);
                }
            }
        }
    });
};
//--------------------------------------------Add/Edit------------------------------------------------------//
var getAmenityData = function (utilityID) {
    var params = { UtilityID: utilityID };
    $.ajax({
        url: "Utility/GetUtilityInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearAmenities();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndUtilityID").val(response.UtilityID);
                $("#txtUtility").val(response.UtilityTitle);
                $("#txtUtilityDetails").val(response.UtilityDetails);
                if ($("#hndUtilityID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
}
var clearAmenities = function () {
    $("#hndUtilityID").val("0");
    $("#txtUtility").val("");
    $("#txtUtilityDetails").val("");
    $("#spanSaveUpdate").text("SAVE");
}
var saveUpdateUtility = function () {
    ////showProgress('#btnSaveUpdate');
    var msg = "";
    if ($.trim($("#txtUtility").val()).length <= 0) {
        msg = msg + "Utility is required.\r\n";
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;

        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'blue'
        });
    }
    else {
        var model = {
            UtilityID: $("#hndUtilityID").val(),
            UtilityTitle: $("#txtUtility").val(),
            UtilityDetails: $("#txtUtilityDetails").val(),
        };
        $.ajax({
            url: "Utility/SaveUpdateUtility",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndUtilityID").val() == 0) {
                        $.alert({
                            title: 'Alert!',
                            content: 'Data Saved Successfully',
                            type: 'blue'
                        });
                        $("#hndUtilityID").val(response.UtilityID);
                        $("#spanSaveUpdate").text("UPDATE");
                    }
                    else {
                        $.alert({
                            title: 'Alert!',
                            content: 'Data Updated Successfully',
                            type: 'blue'
                        });
                    }
                    fillUtilityList($("#hdnCurrentPage").val());
                }
                else {
                    $.alert({
                        title: 'Message!',
                        content: response.error,
                        type: 'blue'
                    });
                }
            }
        });
    }
};
$(document).ready(function () {
    $("#selectFieldAmenity").empty();
    $("#selectFieldAmenity").append("<option value='1'>Amenity</option>");
    fillRPP_Utility();
    $('#ulPagination_Utility').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationUtilityList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            var SortByValueUtility = localStorage.getItem("SortByValueUtility");
            var OrderByValueUtility = localStorage.getItem("OrderByValueUtility");
            fillUtilityList(page, SortByValueUtility, OrderByValueUtility);
        }
    });
    $('#tblUtility tbody').on('click', 'tr', function () {
        $('#tblUtility tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblUtility tbody').on('click', 'tr', function () {
        goToUtility();
    });
    getAmenityData($("#hndUtilityID").val());
    $("#txtCriteriaAmenities").keyup(function () {
    });
});

var delUtility = function (utilityID) {
    var model = {
        UtilityID: utilityID
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Market State?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Utility/DeleteUtility",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + utilityID).remove();
                        }
                    });
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                }
            }
        }
    });
};

var count = 0;
var sortTableUtility = function (sortby) {

    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#SortIconUtility').removeClass('fa fa-sort-down');
        $('#SortIconUtility').addClass('fa fa-sort-up fa-lg');
    }
    else {
        orderby = "DESC";
        $('#SortIconUtility').removeClass('fa fa-sort-up');
        $('#SortIconUtility').addClass('fa fa-sort-down fa-lg');
    }
    localStorage.setItem("SortByValueUtility", sortby);
    localStorage.setItem("OrderByValueUtility", orderby);
    count++;
    buildPaganationUtilityList(pNumber, sortby, orderby);
    fillUtilityList(pNumber, sortby, orderby);
};
