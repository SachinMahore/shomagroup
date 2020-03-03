$(document).ready(function () {
    fillRPP_PetPlace();
    $('#ulPagination_PetPlace').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationPetPlaceList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            var SortByValuePetManagement = localStorage.getItem("SortByValuePetManagement");
            var OrderByValuePetManagement = localStorage.getItem("OrderByValuePetManagement");
            fillPetPlaceSearchGrid(page, SortByValuePetManagement, OrderByValuePetManagement);
        }
    });
    btnSaveUpdate();
    getPropertyList();
});

var goToPetPlace = function (ID) {
    if (ID != null) {
        $("#hndPetPlaceID").val(ID);
        window.location.replace("/Admin/PetManagement/Index/" + ID);
    }
}
var fillRPP_PetPlace = function () {
    $("#ddlRPP_PetPlace").empty();
    $("#ddlRPP_PetPlace").append("<option value='25'>25</option>");
    $("#ddlRPP_PetPlace").append("<option value='50'>50</option>");
    $("#ddlRPP_PetPlace").append("<option value='75'>75</option>");
    $("#ddlRPP_PetPlace").append("<option value='100'>100</option>");
    $("#ddlRPP_PetPlace").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPetPlaceList($("#hdnCurrentPage").val());
    });
};
var buildPaganationPetPlaceList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'PetName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        Criteria: $("#txtCriteriaPetPlace").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_PetPlace").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/PetManagement/buildPaganationPetPlaceList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                $('#ulPagination_PetPlace').pagination('updateItems', response.NOP);
                $('#ulPagination_PetPlace').pagination('selectPage', 1);
            }
        }
    });
};
var fillPetPlaceSearchGrid = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'PetName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    $("#divLoader").show();
    var model = {
        Criteria: $("#txtCriteriaPetPlace").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_PetPlace").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/PetManagement/FillPetPlaceSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                this.cancelChanges();
            } else {
                $("#tblPetPlace>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr id="tr_' + elementValue.PetPlaceID + '" data-value="' + elementValue.PetPlaceID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.PetPlaceID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.PetPlace + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">$' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';
                    html += '<td style="color:#3d3939;"><button class="btn btn-primary" style="padding: 5px 8px !important" onclick="goToPetPlace(' + elementValue.PetPlaceID + ')"><i class="fa fa-edit"></i></button>&nbsp;&nbsp;<button class="btn btn-danger" style="padding: 5px 8px !important" onclick="deletePetPlaces(' + elementValue.PetPlaceID + ')"><i class="fa fa-trash"></i></button></td>';
                    html += '</tr>';
                    $("#tblPetPlace>tbody").append(html);
                });
                $("#divLoader").hide();
            }
            $("#hndPageNo").val(pagenumber);
        }
    });
};
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationPetPlaceList(1);
    }
});
var fillPetPlaceList = function () {
    $("#divLoader").show();
    $.ajax({
        url: '/PetManagement/GetPetPlaceList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblPetPlace>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr id="tr_' + elementValue.PetPlaceID + '" data-value="' + elementValue.PetPlaceID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.PetPlaceID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.PetPlace + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">$' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';
                    html += '<td style="color:#3d3939;"><button class="btn btn-primary" style="padding: 5px 8px !important" onclick="goToPetPlace(' + elementValue.PetPlaceID + ')"><i class="fa fa-edit"></i></button>&nbsp;&nbsp;<button class="btn btn-danger" style="padding: 5px 8px !important" onclick="deletePetPlaces(' + elementValue.PetPlaceID + ')"><i class="fa fa-trash"></i></button></td>';
                    html += '</tr>';
                    $("#tblPetPlace>tbody").append(html);
                });
                $("#divLoader").hide();
            }
        }
    });
}
//-----------------------------------------------------Add/Edit------------------------------------------//

var clearPetPlaceData = function () {
    $("#hndPetPlaceID").val("0");
    $("#txtPetPlaceName").val("");
    btnSaveUpdate();
}
var saveUpdatePetPlace = function () {
    $("#divLoader").show();
    var msg = "";
    if ($.trim($("#ddlProperty").val()).length == 0) {
        msg = msg + "Property is required.</br>";
    }
    if ($.trim($("#txtPetPlaceName").val()).length <= 0) {
        msg = msg + "PetPlace Name is required.</br>";
    }
    if ($.trim($("#txtCharges").val()).length <= 0) {
        msg = msg + "Charges is required.</br>";
    }
    if (msg != "") {
        $("#divLoader").hide();
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'red',
        });
    }
    else {
        var model = {
            PetPlaceID: $("#hndPetPlaceID").val(),
            PropertyID: $("#ddlProperty").val(),
            PetPlace: $("#txtPetPlaceName").val(),
            Charges: $("#txtCharges").val(),
            Description: $("#txtDescription").val()
        };
        $.ajax({
            url: "/PetManagement/SaveUpdatePetPlace",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                $("#divLoader").hide();
                if (response.result == "1") {
                    if ($("#hndPetPlaceID").val() == 0) {
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
                    $("#hndPetPlaceID").val(response.ID);
                    $("#spanSaveUpdate").text("Save");
                    fillPetPlaceList();
                    setInterval(function () {
                        window.location.replace("/Admin/PetManagement/Index/" + 0);
                    }, 1200);
                }
                else {
                    //showMessage("Error!", response.error);
                }
            }
        });
    }
}
var btnSaveUpdate = function () {

    if ($("#hndPetPlaceID").val() == 0) {
        $("#btnSaveUpdatePetPlace").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdatePetPlace").text(" Save");
    }
    else {
        $("#btnSaveUpdatePetPlace").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdatePetPlace").text(" Update");
    }
}

var fillPetPlaceSearchList = function () {
    $("#divLoader").show();
    var params = { SearchText: $("#txtCriteriaPetPlace").val() };
    $.ajax({
        url: '/PetManagement/GetPetPlaceSearchList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblPetPlace>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr id="tr_' + elementValue.PetPlaceID + '" data-value="' + elementValue.PetPlaceID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.PetPlaceID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.PetPlace + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">$' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';
                    html += '<td style="color:#3d3939;"><button class="btn btn-primary" style="padding: 5px 8px !important" onclick="goToPetPlace(' + elementValue.PetPlaceID + ')"><i class="fa fa-edit"></i></button>&nbsp;&nbsp;<button class="btn btn-danger" style="padding: 5px 8px !important" onclick="deletePetPlaces(' + elementValue.PetPlaceID + ')"><i class="fa fa-trash"></i></button></td>';
                    html += '</tr>';
                    $("#tblPetPlace>tbody").append(html);
                });
                $("#divLoader").hide();
            }
        }
    });

}
var getPropertyList = function () {
    $.ajax({
        url: "/WorkOrder/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlProperty").empty();
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                $("#ddlProperty").append(option);
            });

        }
    });
};
var NewPetPlace = function () {
    window.location.replace("/Admin/PetManagement/Index/" + 0);
};
var deletePetPlaces = function (ID) {
    if (ID != null) {
        $("#divLoader").show();
        var model = { Id: ID };
        $.alert({
            title: "",
            content: "Are you sure to remove Pet Detail?",
            type: 'blue',
            buttons: {
                yes: {
                    text: 'Yes',
                    action: function (yes) {
                        $.ajax({
                            url: "/PetManagement/DeletePetPlacesDetails",
                            type: "post",
                            contentType: "application/json utf-8",
                            data: JSON.stringify(model),
                            dataType: "JSON",
                            success: function (response) {
                                $("#divLoader").hide();
                                $('#tr_' + ID).remove();
                            }
                        });
                    }
                },
                no: {
                    text: 'No',
                    action: function (no) {
                        $("#divLoader").hide();
                    }
                }
            }
        });
    }
}

var count = 0;
var sortTablePet = function (sortby) {

    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#sortPetIcon').removeClass('fa fa-sort-up');
        $('#sortPetIcon').removeClass('fa fa-sort-down');
        $('#sortChargesIcon').removeClass('fa fa-sort-up');
        $('#sortChargesIcon').removeClass('fa fa-sort-down');
        if (sortby == 'PetName') {
            $('#sortPetIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'ChargesName') {
            $('#sortChargesIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $('#sortPetIcon').removeClass('fa fa-sort-up');
        $('#sortPetIcon').removeClass('fa fa-sort-down');
        $('#sortChargesIcon').removeClass('fa fa-sort-up');
        $('#sortChargesIcon').removeClass('fa fa-sort-down');
        if (sortby == 'PetName') {
            $('#sortPetIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'ChargesName') {
            $('#sortChargesIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValuePetManagement", sortby);
    localStorage.setItem("OrderByValuePetManagement", orderby);
    count++;
    buildPaganationPetPlaceList(pNumber, sortby, orderby);
    fillPetPlaceSearchGrid(pNumber, sortby, orderby);
};