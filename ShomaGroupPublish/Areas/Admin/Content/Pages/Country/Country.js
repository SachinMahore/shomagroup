var goToCountry = function (countryID) {
    var ID = countryID;
    if (ID != null) {
        $("#hndCountryID").val(ID);
        getCountryData(ID);
    }
};
$(document).keypress(function (e) {
    if (e.which == 13) {
        buildPaganationCountryList(1);
    }
});
var fillCountryList = function (pagenumber, sortbyCountry, orderbyCountry) {
    if (!sortbyCountry) {
        sortbyCountry = 'CountryName';
    }
    if (!orderbyCountry) {
        orderbyCountry = 'ASC';
    }
    var model = {
        Criteria: $("#txtCriteriaCountry").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Country").val(),
        SortBy: sortbyCountry,
        OrderBy: orderbyCountry
    };
    $.ajax({
        url: 'Country/GetCountryList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblCountry>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ID + '" id="tr_' + elementValue.ID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.CountryName + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToCountry(' + elementValue.ID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delCountry(' + elementValue.ID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblCountry>tbody").append(html);
                });
            }
            $("#hndPageNoCountry").val(pagenumber);
        }
    });
};
var fillRPP_Country = function () {
    $("#ddlRPP_Country").empty();
    $("#ddlRPP_Country").append("<option value='25'>25</option>");
    $("#ddlRPP_Country").append("<option value='50'>50</option>");
    $("#ddlRPP_Country").append("<option value='75'>75</option>");
    $("#ddlRPP_Country").append("<option value='100'>100</option>");
    $("#ddlRPP_Country").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationCountryList($("#hdnCurrentPage").val());
    });
};
var buildPaganationCountryList = function (pagenumber, sortbyCountry, orderbyCountry) {
    if (!sortbyCountry) {
        sortbyCountry = 'CountryName';
    }
    if (!orderbyCountry) {
        orderbyCountry = 'ASC';
    }
    var searchtype = $("#hdnSearchType").val();
    var model = {
        Criteria: $("#txtCriteriaCountry").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Country").val(),
        SortBy: sortbyCountry,
        OrderBy: orderbyCountry
    };
    $.ajax({
        url: 'Country/BuildPaganationCountryList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#lblRPP_Country').addClass("hidden");
                    $('#ddlRPP_Country').addClass("hidden");
                    $('#divPagination_Country').addClass("hidden");
                }
                else {
                    $('#lblRPP_Country').removeClass("hidden");
                    $('#ddlRPP_Country').removeClass("hidden");
                    $('#divPagination_Country').removeClass("hidden");
                    $('#ulPagination_Country').pagination('updateItems', response.NOP);
                    $('#ulPagination_Country').pagination('selectPage', 1);
                }
            }
        }
    });
};
//-----------------------------------------------------Add/Edit------------------------------------------//
var getCountryData = function (countryID) {
    var params = { CountryId: countryID };
    $.ajax({
        url: "Country/GetCountryInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearCountryData();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndCountryID").val(response.ID);
                $("#txtCountryName").val(response.CountryName);
                if ($("#hndCountryID").val() != "0") {
                    $("#spanSaveUpdate").text("UPDATE");
                }
                else {
                    $("#spanSaveUpdate").text("SAVE");
                }
            }
        }
    });
};
var clearCountryData = function () {
    $("#hndCountryID").val("0");
    $("#txtCountryName").val("");
    $("#spanSaveUpdate").text("SAVE");
};
var saveUpdateCountry = function () {
    var msg = "";
    if ($.trim($("#txtCountryName").val()).length <= 0) {
        msg = msg + "Country Name is required.\r\n";
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
            ID: $("#hndCountryID").val(),
            CountryName: $("#txtCountryName").val(),
        };
        $.ajax({
            url: "Country/SaveUpdateCountry",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndCountryID").val() == 0) {
                        
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
                    $("#hndCountryID").val(response.ID);
                    $("#spanSaveUpdate").text("UPDATE");
                    fillCountryList($("#hdnCurrentPage").val());
                    clearCountryData();
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
    fillRPP_Country();
    $('#ulPagination_Country').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            console.log("Pagination_Init");
            buildPaganationCountryList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            var SortByValueCountry = localStorage.getItem("SortByValueCountry");
            var OrderByValueCountry = localStorage.getItem("OrderByValueCountry");
            fillCountryList(page, SortByValueCountry, OrderByValueCountry);
        }
    });
    $("#selectFieldCountry").empty();
    $("#selectFieldCountry").append("<option value='1'>Country Name</option>");

    $('#tblCountry tbody').on('click', 'tr', function () {
        $('#tblCountry tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblCountry tbody').on('click', 'tr', function () {
        goToCountry();
    });
});

var delCountry = function (countryID) {
    var model = {
        CountryId: countryID
    };

    $.alert({
        title: "",
        content: "Are you sure to remove this Country?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Country/DeleteCountry",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + countryID).remove();
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
var sortTableCountry = function (sortbyCountry) {

    var orderbyCountry = "";
    var pNumberCountry = $("#hndPageNoCountry").val();
    if (!pNumberCountry) {
        pNumberCountry = 1;
    }

    if (count % 2 == 1) {
        orderbyCountry = "ASC";
        $('#sortCountryIcon').removeClass('fa fa-sort-down');
        $('#sortCountryIcon').addClass('fa fa-sort-up fa-lg');
    }
    else {
        orderbyCountry = "DESC";
        $('#sortCountryIcon').removeClass('fa fa-sort-up');
        $('#sortCountryIcon').addClass('fa fa-sort-down fa-lg');
    }
    localStorage.setItem("SortByValueCountry", sortbyCountry);
    localStorage.setItem("OrderByValueCountry", orderbyCountry);
    count++;
    buildPaganationCountryList(pNumberCountry, sortbyCountry, orderbyCountry);
    fillCountryList(pNumberCountry, sortbyCountry, orderbyCountry);
};