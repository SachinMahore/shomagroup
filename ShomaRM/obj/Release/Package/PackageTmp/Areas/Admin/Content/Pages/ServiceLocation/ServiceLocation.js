$(document).ready(function () {
    fillRPP_Locations();
    clearLocation();
    $('#tblLocation tbody').on('click', 'tr', function () {
        $('#tblLocation tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblLocation tbody').on('click', 'tr', function () {
        goToLocation();
    });

    getLocationData($("#hndLocationID").val());
  
    $("#txtCriteriaLocation").keyup(function () {

    });
    $('#ulPagination_Location').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            //console.log("Pagination_Init");
            buildPaganationLocationList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillLocationSearchGrid(page);
        }
    });
});

var fillRPP_Locations = function () {
    $("#ddlRPP_Location").empty();
    $("#ddlRPP_Location").append("<option value='25'>25</option>");
    $("#ddlRPP_Location").append("<option value='50'>50</option>");
    $("#ddlRPP_Location").append("<option value='75'>75</option>");
    $("#ddlRPP_Location").append("<option value='100'>100</option>");
    $("#ddlRPP_Location").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationLocationList($("#hdnCurrentPage").val());
    });
};
var buildPaganationLocationList = function (pagenumber) {
   
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaLocation").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Location").val()
    };
    $.ajax({
        url: 'ServiceLocation/BuildPaganationSLList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#ddlRPP_Location').addClass("hidden");
                    $('#divPagination_Location').addClass("hidden");
                    $('#lblRPP_Location').addClass("hidden");
                }
                else {
                    $('#ddlRPP_Location').removeClass("hidden");
                    $('#divPagination_Location').removeClass("hidden");
                    $('#lblRPP_Location').removeClass("hidden");

                    $('#ulPagination_Location').pagination('updateItems', response.NOP);
                    $('#ulPagination_Location').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillLocationSearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaLocation").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Location").val()
    };
    $.ajax({
        url: 'ServiceLocation/fillLocationSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblLocation>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.LocationID + '"  id="tr_' + elementValue.LocationID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.LocationID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Location + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToLocation(' + elementValue.LocationID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delLocation(' + elementValue.LocationID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblLocation>tbody").append(html);
                    locationDataSource.push({ LocationID: elementValue.LocationID, Location: elementValue.Location });
                });
               
            }
        }
    });
};
var locationDataSource = [];
var goToLocation = function (LocationID) {
    var ID = LocationID;
    if (ID != null) {
        $("#hndLocationID").val(ID);
        getLocationData(ID);
       
    }
};
var clearLocation = function () {
    $("#hndLocationID").val("0");
    $("#txtLocation").val("");
    $("#spanSaveUpdate").text("SAVE");
};

var delLocation = function (ID) {
  
    var model = {
        LocationID: ID
    };
    $.alert({
        title: "",
        content: "Are you sure to remove Service Location?",
        type: 'red',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ServiceLocation/DeleteServiceLocation",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + ID).remove();
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
var getLocationData = function (LocationID) {
    var params = { LocationID: LocationID };
    $.ajax({
        url: "ServiceLocation/GetServiceLocationInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearLocation();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndLocationID").val(response.LocationID);
                $("#txtLocation").val(response.Location);
                if ($("#hndLocationID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};
var saveUpdateLocation = function () {
    ////showProgress('#btnSaveUpdate');
   
    var msg = "";
    if ($.trim($("#txtLocation").val()).length <= 0) {
        msg = msg + "Location is required.\r\n"
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
            LocationID: $("#hndLocationID").val(),
            Location: $("#txtLocation").val(),
           
        };
       
        $.ajax({
            url: "ServiceLocation/saveUpdateLocation",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndLocationID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue'
                        });
                        $("#hndLocationID").val(response.LocationID);
                        $("#spanSaveUpdate").text("Update");
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue'
                        });
                    }
                    fillLocationSearchGrid($("#hdnCurrentPage").val());
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