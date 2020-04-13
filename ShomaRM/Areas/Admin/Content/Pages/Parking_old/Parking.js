$(document).ready(function () {
    //fillParkingList();
    onFocusParking();
    fillRPP_Parking();
    $('#ulPagination_Parking').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationParkingList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            var SortByValueParking = localStorage.getItem("SortByValueParking");
            var OrderByValueParking = localStorage.getItem("OrderByValueParking");
            fillParkingSearchGrid(page, SortByValueParking, OrderByValueParking);
        }
    });
   
    btnSaveUpdate();
    getPropertyList();

  
});


var goToParking = function (id) {
    var ID = id;
    if (ID != null) {
        $("#hndParkingID").val(ID);
        window.location.replace("/Admin/Parking/Index/" + ID);
    }
}
var fillRPP_Parking = function () {
    $("#ddlRPP_Parking").empty();
    $("#ddlRPP_Parking").append("<option value='25'>25</option>");
    $("#ddlRPP_Parking").append("<option value='50'>50</option>");
    $("#ddlRPP_Parking").append("<option value='75'>75</option>");
    $("#ddlRPP_Parking").append("<option value='100'>100</option>");
    $("#ddlRPP_Parking").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationParkingList($("#hdnCurrentPage").val());
    });
};
var buildPaganationParkingList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "ParkingName";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        Criteria: $("#txtCriteriaParking").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Parking").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Parking/buildPaganationParkingList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                $('#ulPagination_Parking').pagination('updateItems', response.NOP);
                $('#ulPagination_Parking').pagination('selectPage', 1);
            }
        }
    });
};
var fillParkingSearchGrid = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "ParkingName";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        Criteria: $("#txtCriteriaParking").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Parking").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Parking/FillParkingSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                this.cancelChanges();
            } else {
                $("#tblParking>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ParkingID + '" id="tr_' + elementValue.ParkingID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ParkingID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ParkingName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">$ ' + formatMoney(parseFloat(elementValue.Charges).toFixed(2)) + '</td>';
                    //html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary ParkingEdit" style="padding: 5px 8px !important;margin-right:7px" onclick="goToParking(' + elementValue.ParkingID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delParking(' + elementValue.ParkingID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary ParkingEdit" style="padding: 5px 8px !important;margin-right:7px" onclick="goToParking(' + elementValue.ParkingID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblParking>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
        }
    });
};
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationParkingList(1);
    }
});
var fillParkingList = function () {
    $.ajax({
        url: '/Parking/GetParkingList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblParking>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ParkingID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ParkingID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ParkingName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + formatMoney(elementValue.Charges) + '</td>';
                    html += '</tr>';
                    $("#tblParking>tbody").append(html);
                });
            }
        }
    });
}
//-----------------------------------------------------Add/Edit------------------------------------------//

var clearParkingData = function () {
    $("#hndParkingID").val("0");
    $("#txtParkingName").val("");
    btnSaveUpdate();
}
var saveUpdateParking = function () {
    $("#divLoader").show();
    var msg = "";
    if ($.trim($("#ddlProperty").val()).length == 0) {
        msg = msg + "Property is required.</br>"
    }
    if ($.trim($("#txtParkingName").val()).length <= 0) {
        msg = msg + "Parking Name is required.</br>"
    }
    if ($.trim($("#txtCharges").val()).length <= 0) {
        msg = msg + "Charges is required.</br>"
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
        if ($("#hndParkingID").val() != 0) {
            var model = {
                ParkingID: $("#hndParkingID").val(),
                PropertyID: $("#ddlProperty").val(),
                ParkingName: $("#txtParkingName").val(),
                Charges: unformatText($("#txtCharges").val()),
                Description: $("#txtDescription").val()
            };
            $.ajax({
                url: "/Parking/SaveUpdateParking",
                method: "post",
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    $("#divLoader").hide();
                    //hideProgress('#btnSaveUpdate');
                    if (response.result == "1") {
                        if ($("#hndParkingID").val() == 0) {
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
                        $("#hndParkingID").val(response.ID);
                        $("#spanSaveUpdate").text("Save");
                        fillParkingList();
                        setInterval(function () {
                            window.location.replace("/Admin/Parking/Index/" + 0);
                        }, 1200)
                    }
                    else {
                        //showMessage("Error!", response.error);
                    }
                }
            });
        }
        else {
            $("#divLoader").hide();
            $.alert({
                title: 'Message!',
                content: 'Only Edit Allowed</br>',
                type: 'red',
            });
        }
    }
}
var btnSaveUpdate = function () {

    if ($("#hndParkingID").val() == 0) {
        $("#btnSaveUpdateParking").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdateParking").text(" Save");
    }
    else {
        $("#btnSaveUpdateParking").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdateParking").text(" Update");
    }
}

var fillParkingSearchList = function () {
    var params = { SearchText: $("#txtCriteriaParking").val() };
    $.ajax({
        url: '/Parking/GetParkingSearchList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblParking>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ParkingID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ParkingID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ParkingName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + formatMoney(elementValue.Charges) + '</td>';
                    html += '</tr>';
                    $("#tblParking>tbody").append(html);
                });
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
var NewParking = function () {
    window.location.replace("/Admin/Parking/Index/" + 0);
};

var delParking = function (parkingId) {
    var model = {
        ParkingID: parkingId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Parking?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Parking/DeleteParking",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + parkingId).remove();
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

var onFocusParking = function () {

    $("#txtCharges").focusout(function () { $("#txtCharges").val(formatMoney($("#txtCharges").val())); })
        .focus(function () {
            $("#txtCharges").val(unformatText($("#txtCharges").val()));
        });
}

function unformatText(text) {
    if (text == null)
        text = "";

    return text.replace(/[^\d\.]/g, '');
}

function formatMoney(number) {
    number = number || 0;
    var places = 2;
    var symbol = "";
    var thousand = ",";
    var decimal = ".";
    var negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
}

var count = 0;
var sortTableParking = function (sortby) {
    var orderby = "";
    var pagenumber = $("#hndPageNo").val();
    if (!pagenumber) {
        pagenumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $("#SortIconParking").removeClass('fa fa-sort-up');
        $("#SortIconParking").removeClass('fa fa-sort-down');
        $("#SortIconCharges").removeClass('fa fa-sort-up');
        $("#SortIconCharges").removeClass('fa fa-sort-down');
        if (sortby == 'ParkingName') {
            $("#SortIconParking").addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'ChargesName') {
            $("#SortIconCharges").addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $("#SortIconParking").removeClass('fa fa-sort-up');
        $("#SortIconParking").removeClass('fa fa-sort-down');
        $("#SortIconCharges").removeClass('fa fa-sort-up');
        $("#SortIconCharges").removeClass('fa fa-sort-down');
        if (sortby == 'ParkingName') {
            $("#SortIconParking").addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'ChargesName') {
            $("#SortIconCharges").addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueParking", sortby);
    localStorage.setItem("OrderByValueParking", orderby);
    count++;
    buildPaganationParkingList(pagenumber, sortby, orderby);
    fillParkingSearchGrid(pagenumber, sortby, orderby);
};