//--------------------------------------------Search------------------------------------------------------//
var amenitiesDataSource = [];
var goToAmenities = function (amenitiesID) {
    var ID = amenitiesID;
    if (ID != null) {
        $("#hndAmenityID").val(ID);
        getAmenityData(ID);
    }
};
$(document).keypress(function (e) {
    if (e.which == 13) {
        buildPaganationAmenityList(1);
    }
});
var fillAmenitySearchGrid = function (pagenumber) {
    var model = {
        Criteria: $("#txtCriteriaAmenities").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Amenities").val()
    };
    $.ajax({
        url: 'Amenities/FillAmenitySearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblAminities>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ID + '"  id="tr_' + elementValue.ID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Amenity + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToAmenities(' + elementValue.ID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger hidden" style="padding: 5px 8px !important;" onclick="delAmenities(' + elementValue.ID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblAminities>tbody").append(html);
                    amenitiesDataSource.push({ ID: elementValue.ID, Amenity: elementValue.Amenity });
                });
                console.log(amenitiesDataSource);
            }
        }
    });
};
//--------------------------------------------Add/Edit------------------------------------------------------//
var getAmenityData = function (amenityID) {
    var params = { AmenityID: amenityID };
    $.ajax({
        url: "Amenities/GetAmenityInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearAmenities();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndAmenityID").val(response.ID);
                $("#txtAmenity").val(response.Amenity);
                $("#txtAmenityDetails").val(response.AmenityDetails);
                if ($("#hndAmenityID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};
var clearAmenities = function () {
    $("#hndAmenityID").val("0");
    $("#txtAmenity").val("");
    $("#txtAmenityDetails").val("");
    $("#spanSaveUpdate").text("SAVE");
};
var saveUpdateAmenity = function () {
    ////showProgress('#btnSaveUpdate');
    var msg = "";
    if ($.trim($("#txtAmenity").val()).length <= 0) {
        msg = msg + "Amenity is required.\r\n"
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
            ID: $("#hndAmenityID").val(),
            Amenity: $("#txtAmenity").val(),
            AmenityDetails: $("#txtAmenityDetails").val(),
        };
        $.ajax({
            url: "Amenities/SaveUpdateAmenity",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndAmenityID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue'
                        });
                        $("#hndAmenityID").val(response.ID);
                        $("#spanSaveUpdate").text("Update");
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Updated Successfully",
                            type: 'blue'
                        });
                    }
                    fillAmenitySearchGrid($("#hdnCurrentPage").val());
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
var fillRPP_Amenities = function () {
    $("#ddlRPP_Amenities").empty();
    $("#ddlRPP_Amenities").append("<option value='25'>25</option>");
    $("#ddlRPP_Amenities").append("<option value='50'>50</option>");
    $("#ddlRPP_Amenities").append("<option value='75'>75</option>");
    $("#ddlRPP_Amenities").append("<option value='100'>100</option>");
    $("#ddlRPP_Amenities").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationAmenityList($("#hdnCurrentPage").val());
    });
};
var buildPaganationAmenityList = function (pagenumber) {
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaAmenities").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Amenities").val()
    };
    $.ajax({
        url: 'Amenities/BuildPaganationAmenityList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#ddlRPP_Amenities').addClass("hidden");
                    $('#divPagination_Amenity').addClass("hidden");
                    $('#lblRPP_Amenity').addClass("hidden");
                }
                else {
                    $('#ddlRPP_Amenities').removeClass("hidden");
                    $('#divPagination_Amenity').removeClass("hidden");
                    $('#lblRPP_Amenity').removeClass("hidden");

                    $('#ulPagination_Amenities').pagination('updateItems', response.NOP);
                    $('#ulPagination_Amenities').pagination('selectPage', 1);
                }
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_Amenities();
    $("#selectFieldAmenity").empty();
    $("#selectFieldAmenity").append("<option value='1'>Amenity</option>");
    $('#tblAminities tbody').on('click', 'tr', function () {
        $('#tblAminities tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblAminities tbody').on('click', 'tr', function () {
        goToAmenities();
    });

    getAmenityData($("#hndAmenityID").val());

    $("#txtCriteriaAmenities").keyup(function () {
        
    });
    $('#ulPagination_Amenities').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationAmenityList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillAmenitySearchGrid(page);
        }
    });
});

var delAmenities = function (amenitiesID) {
    var model = {
        AmenitiesID: amenitiesID
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
                        url: "/Amenities/DeleteAmenities",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + amenitiesID).remove();
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
