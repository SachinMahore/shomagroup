//--------------------------------------------Search------------------------------------------------------//
var amenitiesDataSource = [];
var goToChargeType = function () {
    var row = $('#tblChargeType tbody tr.pds-selected-row').closest('tr');
    var CTID = $(row).attr("data-value");
    if (CTID != null) {
        $("#hndCTID").val(CTID);
        getAmenityData(CTID);
    }
}
$(document).keypress(function (e) {
    if (e.which == 13) {
        fillChargeTypeList();
    }
});
var fillChargeTypeList = function () {
    var params = { ChargeType: $("#txtCriteriaChargeType").val() };
    $.ajax({
        url: 'ChargeType/GetChargeTypeList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblChargeType>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.CTID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.CTID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Charge_Type + '</td>';
                    html += '</tr>';
                    $("#tblChargeType>tbody").append(html);
                    amenitiesDataSource.push({ CTID: elementValue.CTID, Charge_Type: elementValue.Charge_Type });
                });
                console.log(amenitiesDataSource);
            }
        }
    });
}

//--------------------------------------------Add/Edit------------------------------------------------------//
var getAmenityData = function (CTID) {
    var params = { CTID: CTID };
    $.ajax({
        url: "ChargeType/GetChargeTypeInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearAmenities();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#hndCTID").val(response.CTID);
                $("#txtChargeType").val(response.Charge_Type);
                $("#txtCharge_Description").val(response.Charge_Description);
                $("#txtChargeSummary").val(response.Summary_Charge_Type);
                $("#txtRevenueAcct").val(response.Revenue_Account);
                $("#txtPayment_Description").val(response.Payment_Description);
               
                if ($("#hndCTID").val() != "0") {
                    $("#spanSaveUpdate").text("Update");
                }
                else {
                    $("#spanSaveUpdate").text("Save");
                }
            }
        }
    });
}
var clearAmenities = function () {
    $("#hndCTID").val("0");
    $("#txtChargeType").val("");
    $("#txtCharge_Description").val("");
    $("#spanSaveUpdate").text("Save");
}
var saveUpdateChargeType = function () {
    ////showProgress('#btnSaveUpdate');

    var msg = "";
    if ($.trim($("#txtChargeType").val()).length <= 0) {
        msg = msg + "ChargeType is required.\r\n"
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;

        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'blue'
        })
    }
    else {
        var model = {
            CTID: $("#hndCTID").val(),
            Charge_Type: $("#txtChargeType").val(),
            Charge_Description: $("#txtCharge_Description").val(),
        };
        $.ajax({
            url: "ChargeType/SaveUpdateChargeType",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndCTID").val() == 0) {
                        alert("Data Saved Successfully");
                        $("#hndCTID").val(response.CTID);
                        $("#spanSaveUpdate").text("Update");
                    }
                    else {

                        $.alert({
                            title: 'Alert!',
                            content: 'Data Added Successfully',
                            type: 'blue'
                        })
                    }
                    fillAmenityList();
                }
                else {
                }
            }
        });
    }
}
$(document).ready(function () {
    $("#selectFieldAmenity").empty();
    $("#selectFieldAmenity").append("<option value='1'>Amenity</option>");
    fillChargeTypeList();
    $('#tblChargeType tbody').on('click', 'tr', function () {
        $('#tblChargeType tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblChargeType tbody').on('click', 'tr', function () {
        goToChargeType();
    });

    getAmenityData($("#hndCTID").val());

    $("#txtCriteriaAmenities").keyup(function () {

    });
});
