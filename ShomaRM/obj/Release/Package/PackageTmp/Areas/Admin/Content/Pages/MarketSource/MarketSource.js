$(document).ready(function () {
    //fillMarketSourceList();
    fillRPP_MS();
    $('#ulPagination_MS').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationMSList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillMSSearchGrid(page);
        }
    });
    TableClickEvent();
    btnSaveUpdate();
});

var goToMarketSource = function (Adid) {
    var ID = Adid;
    if (ID !== null) {
        $("#hndMarketSourceID").val(ID);
        window.location.replace("/Admin/MarketSource/Index/" + ID);
    }
}
var fillRPP_MS = function () {
    $("#ddlRPP_MS").empty();
    $("#ddlRPP_MS").append("<option value='25'>25</option>");
    $("#ddlRPP_MS").append("<option value='50'>50</option>");
    $("#ddlRPP_MS").append("<option value='75'>75</option>");
    $("#ddlRPP_MS").append("<option value='100'>100</option>");
    $("#ddlRPP_MS").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationMSList($("#hdnCurrentPage").val());
    });
};
var buildPaganationMSList = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaMarketSource").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_MS").val()
    };
    $.ajax({
        url: '/MarketSource/BuildPaganationMSList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                $('#ulPagination_MS').pagination('updateItems', response.NOP);
                $('#ulPagination_MS').pagination('selectPage', 1);
            }
        }
    });
};
var fillMSSearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaMarketSource").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_MS").val()
    };
    $.ajax({
        url: '/MarketSource/FillMSSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                this.cancelChanges();
            } else {
                $("#tblMarketSource>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.AdID + '" id="tr_' + elementValue.AdID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.AdID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Advertiser + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToMarketSource(' + elementValue.AdID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delMarketSource(' + elementValue.AdID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblMarketSource>tbody").append(html);
                });
            }
        }
    });
};
$(document).keypress(function (e) {
    if (e.which === 13) {
        //fillMarketSourceList();
        buildPaganationMSList(1);
    }
});
var fillMarketSourceList = function () {
    $.ajax({
        url: '/MarketSource/GetMarketSourceList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblMarketSource>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.AdID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.AdID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Advertiser + '</td>';
                    html += '</tr>';
                    $("#tblMarketSource>tbody").append(html);
                });
            }
        }
    });
}
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
            if ($.trim(response.error) !== "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndStateID").val(response.ID);
                $("#txtStateName").val(response.StateName);
                $("#txtAbbreviation").val(response.Abbreviation);
                if ($("#hndStateID").val() !== "0") {
                    $("#spanSaveUpdate").text("Update");
                }
                else {
                    $("#spanSaveUpdate").text("Save");
                }
            }
        }
    });
}
var clearMarketSourceData = function () {
    $("#hndMarketSourceID").val("0");
    $("#txtMarketSource").val("");
    btnSaveUpdate();
}
var saveUpdateMarketSource = function () {
    var msg = "";
    if ($.trim($("#txtMarketSource").val()).length <= 0) {
        msg = msg + "Market Source Name is required.\r\n"
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'red',
        });
    }
    else {
        var model = {
            AdID: $("#hndMarketSourceID").val(),
            Advertiser: $("#txtMarketSource").val(),
        };
        $.ajax({
            url: "/MarketSource/SaveUpdateMarketSource",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndMarketSourceID").val() === 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue',
                        });
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Update Successfully",
                            type: 'blue',
                        });
                    }
                    $("#hndMarketSourceID").val(response.ID);
                    $("#spanSaveUpdate").text("Save");
                    fillMarketSourceList();
                    setInterval(function () {
                        window.location.replace("/Admin/MarketSource/Index/" + 0);
                    },1200)
                }
                else {
                    //showMessage("Error!", response.error);
                }
            }
        });
    }
}
var btnSaveUpdate = function () {
    if ($("#hndMarketSourceID").val() === 0) {
        $("#btnSaveUpdateMarketSource").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdateMarketSource").text(" Save");
    }
    else {
        $("#btnSaveUpdateMarketSource").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdateMarketSource").text(" Update");
    }
}
var fillMarketSourceSearchList = function () {
    var params = { SearchText: $("#txtCriteriaMarketSource").val() };
    $.ajax({
        url: '/MarketSource/GetMarketSourceSearchList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblMarketSource>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.AdID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.AdID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Advertiser + '</td>';
                    html += '</tr>';
                    $("#tblMarketSource>tbody").append(html);
                });
            }
        }
    });
}
var newMarketSource = function () {
    window.location.replace("/Admin/MarketSource/Index/" + 0);
};

var delMarketSource = function (ADId) {
    var model = {
        ADID: ADId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Market Source?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/MarketSource/DeleteMarketSource",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + ADId).remove();
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