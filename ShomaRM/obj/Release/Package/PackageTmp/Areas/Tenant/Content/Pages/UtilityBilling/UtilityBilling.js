$(document).ready(function () {
    getUtilityBilling();
    fillUtilityList();
    TableClick();
});
var fillUtilityList = function () {
    var params = { Utility: "" };
    $.ajax({
        url: '/Admin/Utility/GetUtilityDDLList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#ddlUtilityId").empty();
                $.each(response, function (elementType, elementValue) {
                    
                    var option = "<option value=" + elementValue.UtilityID + ">" + elementValue.UtilityTitle + "</option>";
                    $("#ddlUtilityId").append(option);

                });
            }
        }
    });
}
var SaveUpdateUtilityBilling = function () {

    var msg = "";
    var ubid = $("#hdnUBID").val();
    var utilityid = $("#ddlUtilityId").val();
    var leaseid = $("#txtLeaseID").val();
    var revisionNum = $("#txtRevisionNum").val();
    var unit = $("#txtUnit").val();
    var chargeType = $("#txtChargeType").val();
    var effectiveDate = $("#txtEffectiveDate").val();
    var meterReading = $("#txtMeterReading").val();
    var pricePerUnit = $("#txtPricePerUnit").val();
    var posted = $("#txtPosted").val();
  

    if (utilityid == '0') {
        msg += "Please Select The Utility </br>";
    }
    if (leaseid == '') {
        msg += "Please Enter The Lease </br>";
    }
    if (revisionNum == '') {
        msg += "Please Enter The Revision Number </br>";
    }
    if (unit == '') {
        msg += "Please Enter The Unit </br>";
    }
    if (chargeType == '') {
        msg += "Please Enter The Charge Type </br>";
    }
    if (effectiveDate == '') {
        msg += "Please Enter The Effective Date </br>";
    }
    if (meterReading == '') {
        msg += "Please Enter The Meter Reading </br>";
    }
    if (pricePerUnit == '') {
        msg += "Please Select The Price Per Unit </br>";
    }
    if (posted == '') {
        msg += "Please Enter The Posted Date </br>";
    }
    if (tenantId == '') {
        msg += "Please Enter The Tenant </br>";
    }

    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type:'red'
        })
        return;
    }

    var model = {
        UBID: ubid,
        UtilityID: utilityid,
        LeaseID: leaseid,
        Revision_Num: revisionNum,
        Unit: unit,
        ChargeType: chargeType,
        EffectiveDate: effectiveDate,
        MeterReading: meterReading,
        PricePerUnit: pricePerUnit,
        Posted: posted,
        TenantID: tenantId
    }

    $.ajax({
        url: "/UtilityBilling/SaveUpdateUtilityBilling",
        type: "POST",
        data: JSON.stringify(model),
        contentType: "application/json;charset = utf-8",
        dataType: "json",
        success: function (response) {

            $.alert({
                title: 'Alert!',
                content: response.result,
                type: 'blue'
            });
            setInterval(function () {
                window.location.replace("/Tenant/UtilityBilling/");
            }, 3000);
            
        }

    });
}

var Clear = function () {
    var ubid = $("#hdnUBID").val().Clear();
    var utilityid = $("#ddlUtilityId").val().Clear();
    var leaseid = $("#txtLeaseID").val().Clear();
    var revisionNum = $("#txtRevisionNum").val().Clear();
    var unit = $("#txtUnit").val().Clear();
    var chargeType = $("#txtChargeType").val().Clear();
    var effectiveDate = $("#txtEffectiveDate").val().Clear();
    var meterReading = $("#txtMeterReading").val().Clear();
    var pricePerUnit = $("#txtPricePerUnit").val().Clear();
    var posted = $("#txtPosted").val().Clear();
    var tenantId = $("#txtTanantId").val().Clear();
}

var getUtilityBilling = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    }
    $.ajax({
        url: "/UtilityBilling/GetUtilityBillingList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblUtilityBilling>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.UBID + ">";
                html += "<td>" + elementValue.UtilityTitle + "</td>";
                html += "<td>" + elementValue.LeaseID + "</td>";
                html += "<td>" + elementValue.Revision_Num + "</td>";
                html += "<td>" + elementValue.Unit + "</td>";
                html += "<td>" + elementValue.ChargeType + "</td>";
                html += "<td>" + elementValue.EffectiveDateString + "</td>";
                html += "<td>" + elementValue.MeterReading + "</td>";
                html += "<td>" + elementValue.PricePerUnit + "</td>";
                html += "</tr>";
                $("#tblUtilityBilling>tbody").append(html);
            });

        }
    });
}

var TableClick = function () {

    $('#tblUtilityBilling tbody').on('click', 'tr', function () {
        $('#tblUtilityBilling tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblUtilityBilling tbody').on('dblclick', 'tr', function () {
        goToEditProspect();
    });
}

var goToEditProspect = function () {

    var row = $('#tblUtilityBilling tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {
        
        window.location.replace("/Tenant/UtilityBilling/Edit/" + ID);
    } else {
        
    }
}

var addUtilityBilling = function () {
    window.location.replace("/Tenant/UtilityBilling/Edit/" + 0)
}
