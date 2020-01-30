$(document).ready(function () {
    getFacilityBookingLists();
    TableClickFacilityBooking();
    FacilityList();
});


var SaveUpdateFacilityBooking = function () {

    var msg = "";
    var facilityBookingId = $("#hdnFacilityBookingId").val();
    var tenantId = $("#hdnTenantId").val();
    var facility = $("#ddlFacility").val();
    var bookingDate = $("#txtBookingDate").val();
    var requiredFromDate = $("#txtRequiredFromDate").val();
    var requiredToDate = $("#txtRequiredToDate").val();
    var bookingDetails = $("#txtBookingDetails").val();

    if (facility == "0") {
        msg += "Please Select The Facility </br>";
    }

    if (bookingDate == "") {
        msg += "Please Fill The Booking Date </br>";
    }
    
    if (requiredFromDate == "") {
        msg += "Please Fill The Required From Date </br>";
    }

    if (requiredToDate == "") {
        msg += "Please Fill The Required To Date </br>";
    }

    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        })
        return;
    }

    var model = {
        FacilityBookingID: facilityBookingId,
        TenantID: tenantId,
        FacilityID: facility,
        BookingDate: bookingDate,
        RequiredFromDate: requiredFromDate,
        RequiredToDate: requiredToDate,
        BookingDetails: bookingDetails
    }

    $.ajax({
        url: '/Tenant/FacilityBooking/SaveUpdateFacilityBooking',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.msg,
                type: 'blue'
            })
            setInterval(function () {
                window.location.replace("/Tenant/FacilityBooking")
            }, 3000);
        }
    });
}

var getFacilityBookingLists = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    }
    $.ajax({
        url: "/FacilityBooking/GetFacilityBookingData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblFacilityBooking>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.FacilityBookingID + ">";
                html += "<td>" + elementValue.FacilityName + "</td>";
                html += "<td>" + elementValue.BookingDateString + "</td>";
                html += "<td>" + elementValue.RequiredFromDateString + "</td>";
                html += "<td>" + elementValue.RequiredToDateString + "</td>";
                html += "</tr>";
                $("#tblFacilityBooking>tbody").append(html);
            });

        }
    });
}

var TableClickFacilityBooking = function () {

    $('#tblFacilityBooking tbody').on('click', 'tr', function () {
        $('#tblFacilityBooking tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblFacilityBooking tbody').on('dblclick', 'tr', function () {
        goToEditFacilityBooking();
    });
}

var goToEditFacilityBooking = function () {

    var row = $('#tblFacilityBooking tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {

        window.location.replace("/Tenant/FacilityBooking/Edit/" + ID);
    } else {

    }
}

var addNewFacilityBooking = function () {
    window.location.replace("/Tenant/FacilityBooking/Edit/" + 0)
}

var goFacilityBookingList = function () {
    window.location.replace("/Tenant/FacilityBooking/")
}

var FacilityList = function () {
    $.ajax({
        url: '/Facility/FacilityDataList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $("#ddlFacility").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = '<option value=' + elementValue.FacilityID + '>' + elementValue.FacilityName + '</option>';
                $("#ddlFacility").append(option);
            })
        }
    });
}