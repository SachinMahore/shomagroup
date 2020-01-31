var goToState = function (stateID) {
    var ID = stateID;
    if (ID != null) {
        $("#hndStateID").val(ID);
        getStateData(ID);
    }
};
$(document).keypress(function (e) {
    if (e.which == 13) {
        buildPaganationStateList(1);
    }
});
var fillStateList = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaState").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_State").val()
    };
    $.ajax({
        url: 'State/GetStateList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblState>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ID + '" id="tr_' + elementValue.ID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.StateName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Abbreviation + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToState(' + elementValue.ID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delState(' + elementValue.ID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblState>tbody").append(html);
                });
            }
        }
    });
};
var fillRPP_State = function () {
    $("#ddlRPP_State").empty();
    $("#ddlRPP_State").append("<option value='25'>25</option>");
    $("#ddlRPP_State").append("<option value='50'>50</option>");
    $("#ddlRPP_State").append("<option value='75'>75</option>");
    $("#ddlRPP_State").append("<option value='100'>100</option>");
    $("#ddlRPP_State").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationStateList($("#hdnCurrentPage").val());
    });
};
var buildPaganationStateList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaState").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_State").val()
    };
    $.ajax({
        url: 'State/BuildPaganationStateList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#lblRPP_State').addClass("hidden");
                    $('#ddlRPP_State').addClass("hidden");
                    $('#divPagination_State').addClass("hidden");
                }
                else {
                    $('#lblRPP_State').removeClass("hidden");
                    $('#ddlRPP_State').removeClass("hidden");
                    $('#divPagination_State').removeClass("hidden");
                    $('#ulPagination_State').pagination('updateItems', response.NOP);
                    $('#ulPagination_State').pagination('selectPage', 1);
                }
            }
        }
    });
};
//-----------------------------------------------------Add/Edit------------------------------------------//
var getStateData = function (stateID) {
    var params = { StateID: stateID };
    $.ajax({
        url: "State/GetStateInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearStateData();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndStateID").val(response.ID);
                $("#txtStateName").val(response.StateName);
                $("#txtAbbreviation").val(response.Abbreviation);
                if ($("#hndStateID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};
var clearStateData = function () {
    $("#hndStateID").val("0");
    $("#txtStateName").val("");
    $("#txtAbbreviation").val("");
    $("#spanSaveUpdate").text("SAVE");
};
var saveUpdateState = function () {
    var msg = "";
    if ($.trim($("#txtStateName").val()).length <= 0) {
        msg = msg + "State Name is required.\r\n";
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'blue',
        });
    }
    else {
        var model = {
            ID: $("#hndStateID").val(),
            StateName: $("#txtStateName").val(),
            Abbreviation: $("#txtAbbreviation").val(),
        };
        $.ajax({
            url: "State/SaveUpdateState",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndStateID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue'
                        });
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue'
                        });
                    }
                    $("#hndStateID").val(response.ID);
                    $("#spanSaveUpdate").text("UPDATE");
                    fillStateList($("#hdnCurrentPage").val());
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
    fillRPP_State();
    $('#ulPagination_State').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationStateList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillStateList(page);
        }
    });
    $("#selectFieldState").empty();
    $("#selectFieldState").append("<option value='1'>State Name</option>");
    //fillStateList();
    $('#tblState tbody').on('click', 'tr', function () {
        $('#tblState tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblState tbody').on('click', 'tr', function () {
        goToState();
    });
});


var delState = function (stateID) {
    var model = {
        StateID: stateID
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
                        url: "/State/DeleteState",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + stateID).remove();
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