$(document).ready(function () { getPropertyList(); });
var gotoParkingList = function () {
    window.location = "/Admin/Parking/Index";
};

var saveUpdateParking = function () {

    $("#divLoader").show();
    var msg = "";
    if ($("#ddlProperty").val().length == 0) {
        msg += "Property is required.</br>";
    }
    if ($("#ddlLocation").val() == 0) {
        msg += "Location is required.</br>";
    }
    if ($.trim($("#txtParkingName").val()).length <= 0) {
        msg += "Parking Name is required.</br>";
    }
    if ($.trim($("#txtCharges").val()).length <= 0) {
        msg += "Charges is required.</br>";
    }
    if ($.trim($("#txtParkingName").val()).length <= 0) {
        msg += msg + "Space No. is required.</br>";
    }

    if (msg != "") {
        $("#divLoader").hide();
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'red'
        });
        return;
    }
    else {
        if ($("#hndParkingID").val() != 0) {
            var model = {
                ParkingID: $("#hndParkingID").val(),
                PropertyID: $("#hndPrportyUnit").val(),
                ParkingName: $("#txtParkingName").val(),
                Charges: unformatText($("#txtCharges").val()),
                Description: $("#ddlLocation").find(':selected').data("search"),
                OwnerName: $("#txtOwnerName").val(),
                UnitNo: $("#txtUnitNo").val(),
                VehicleTag: $("#txtVehicleTag").val(),
                VehicleMake: $("#txtVehicleMake").val(),
                VehicleModel: $("#txtVehicleModel").val()
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
                    $.alert({
                        title: 'Message!',
                        content: response.models,
                        type: 'blue',
                    });
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

var getParkingList = function () {
    $.ajax({
        url: "/Parking/GetParkingNewList",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            // console.log(JSON.stringify(response));
            $("#ddlLocation").empty();
            $.each(response, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.ParkingID + " data-search=" + elementValue.Description + ">" + elementValue.Description + "</option>";
                $("#ddlLocation").append(option);
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
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }
    $('#sortFirstNameIcon').removeClass('fa fa-sort-up');
    $('#sortFirstNameIcon').removeClass('fa fa-sort-down');
    $('#sortLastNameIcon').removeClass('fa fa-sort-up');
    $('#sortLastNameIcon').removeClass('fa fa-sort-down');
    $('#sortPhoneNoIcon').removeClass('fa fa-sort-up');
    $('#sortPhoneNoIcon').removeClass('fa fa-sort-down');
    $('#sortEmailAddressIcon').removeClass('fa fa-sort-up');
    $('#sortEmailAddressIcon').removeClass('fa fa-sort-down');
    $('#sortApplyDateIcon').removeClass('fa fa-sort-up');
    $('#sortApplyDateIcon').removeClass('fa fa-sort-down');
    $('#sortUnitNoIcon').removeClass('fa fa-sort-up');
    $('#sortUnitNoIcon').removeClass('fa fa-sort-down');
    if (count % 2 == 1) {
        orderby = "ASC";
        if (sortby == 'FirstName') {
            $('#sortFirstNameIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'LastName') {
            $('#sortLastNameIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'PhoneNo') {
            $('#sortPhoneNoIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'EmailAddress') {
            $('#sortEmailAddressIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'ApplyDate') {
            $('#sortApplyDateIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'UnitNo') {
            $('#sortUnitNoIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        if (sortby == 'FirstName') {
            $('#sortFirstNameIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'LastName') {
            $('#sortLastNameIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'PhoneNo') {
            $('#sortPhoneNoIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'EmailAddress') {
            $('#sortEmailAddressIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'ApplyDate') {
            $('#sortApplyDateIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'UnitNo') {
            $('#sortUnitNoIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueProsVerification", sortby);
    localStorage.setItem("OrderByValueProsVerification", orderby);
    count++;
    buildPaganationProspectVerifyList(pNumber, sortby, orderby);
    fillProspectVerifyList(pNumber, sortby, orderby);
};