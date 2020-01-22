var goToCity = function (cityID) {
    var ID = cityID;
    if (ID != null) {
        $("#hndCityID").val(ID);
        getCityData(ID);
    }
};
$(document).keypress(function (e) {
    if (e.which == 13) {
        buildPaganationCityList(1);
        fillStateDDL();
    }
});
var fillCityList = function (stateID) {
    var model = {
        StateID: stateID
    };
    $.ajax({
        url: 'City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#ddlCity").empty();
                $("#ddlCity").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
};
var fillCitySearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaCity").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_City").val()
    };
    $.ajax({
        url: 'City/GetCityList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblCity>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ID + '" id="tr_' + elementValue.ID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.CityName + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToCity(' + elementValue.ID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delCity(' + elementValue.ID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblCity>tbody").append(html);
                });
            }
        }
    });
};

var fillRPP_City = function () {
    $("#ddlRPP_City").empty();
    $("#ddlRPP_City").append("<option value='25'>25</option>");
    $("#ddlRPP_City").append("<option value='50'>50</option>");
    $("#ddlRPP_City").append("<option value='75'>75</option>");
    $("#ddlRPP_City").append("<option value='100'>100</option>");
    $("#ddlRPP_City").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationCityList($("#hdnCurrentPage").val());
    });
};
var buildPaganationCityList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaCity").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_City").val()
    };
    $.ajax({
        url: 'City/BuildPaganationCityList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#lblRPP_City').addClass("hidden");
                    $('#divPagination_City').addClass("hidden");
                    $('#ddlRPP_City').addClass("hidden");
                }
                else {
                    $('#lblRPP_City').removeClass("hidden");
                    $('#divPagination_City').removeClass("hidden");
                    $('#ddlRPP_City').removeClass("hidden");

                    $('#ulPagination_City').pagination('updateItems', response.NOP);
                    $('#ulPagination_City').pagination('selectPage', 1);
                }
            }
        }
    });
};
//-------------------------------------------------------------Add/Edit--------------------------------------------//
var getCityData = function (cityID) {
    var params = { CityID: cityID };
    $.ajax({
        url: "City/GetCityInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearCityData();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndCityID").val(response.ID);
                $("#txtCityName").val(response.CityName);
                $("#ddlState").val(response.StateID);
                if ($("#hndCityID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};
var clearCityData = function () {
    $("#hndCityID").val("0");
    $("#txtCityName").val("");
    $("#ddlState").val(0);
    $("#spanSaveUpdate").text("SAVE");
};
var saveUpdateCity = function () {
    var msg = "";
    if ($.trim($("#txtCityName").val()).length <= 0) {
        msg = msg + "City Name is required.\r\n";
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
            ID: $("#hndCityID").val(),
            CityName: $("#txtCityName").val(),
            StateID: $("#ddlState").val(),
        };
        $.ajax({
            url: "City/SaveUpdateCity",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndCityID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue',
                        });
                        $("#spanSaveUpdate").text("UPDATE");
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue',
                        });

                    }
                    $("#hndCityID").val(response.ID);
                    fillCitySearchGrid($("#hdnCurrentPage").val());
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
var fillStateDDL = function () {
    $.ajax({
        url: 'City/FillStateDropDownList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlState").empty();
                $("#ddlState").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
};
$(document).ready(function () {
    $("#selectFieldCity").empty();
    $("#selectFieldCity").append("<option value='1'>City Name</option>");
    $("#selectFieldCity").append("<option value='2'>State Name</option>");
    fillRPP_City();
    $('#ulPagination_City').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationCityList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillCitySearchGrid(page);
        }
    });    
    $('#tblCity tbody').on('click', 'tr', function () {
        $('#tblCity tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblCity tbody').on('click', 'tr', function () {
        goToCity();
    });

    fillStateDDL();
    getCityData($("#hndCityID").val());
});

var delCity = function (cityID) {
    var model = {
        CityID: cityID
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
                        url: "/City/DeleteCity",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + cityID).remove();
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