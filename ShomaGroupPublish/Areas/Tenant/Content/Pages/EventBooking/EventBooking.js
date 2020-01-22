$(document).ready(function () {
    getEventBookingLists();
    TableClickEventBooking();
    EventList();
});


var SaveUpdateEventBooking = function () {

    var msg = "";
    var eventBookingId = $("#hdnEventBookingId").val();
    var tenantId = $("#hdnTenantId").val();
    var eventId = $("#ddlEvent").val();
    var bookingDate = $("#txtBookingDate").val();
    var numberOfGuest = $("#txtNoOfGuest").val();
    var bookingDetails = $("#txtBookingDetails").val();

    if (event == "0") {
        msg += "Please Select The Event </br>";
    }

    if (bookingDate == "") {
        msg += "Please Fill The Booking Date </br>";
    }

    if (numberOfGuest == "") {
        msg += "Please Fill The Number Of Guest </br>";
    }
    else {
        if (!validateNumber(numberOfGuest)) {
            msg += "Invalid Number Of Guest </br>";
        }
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
        EventBookingID: eventBookingId,
        TenantID: tenantId,
        EventID: eventId,
        BookingDate: bookingDate,
        NoOfGuest: numberOfGuest,
        BookingDetails: bookingDetails
    }

    $.ajax({
        url: '/Tenant/EventBooking/SaveUpdateEventBooking',
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
                window.location.replace("/Tenant/EventBooking")
            }, 3000);
        }
    });
}

var getEventBookingLists = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    }
    $.ajax({
        url: "/EventBooking/GetEventBookingData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblEventBooking>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.EventBookingID + ">";
                html += "<td>" + elementValue.EventName + "</td>";
                html += "<td>" + elementValue.BookingDateString + "</td>";
                html += "<td>" + elementValue.NoOfGuest + "</td>";
                html += "</tr>";
                $("#tblEventBooking>tbody").append(html);
            });

        }
    });
}

var TableClickEventBooking = function () {

    $('#tblEventBooking tbody').on('click', 'tr', function () {
        $('#tblEventBooking tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblEventBooking tbody').on('dblclick', 'tr', function () {
        goToEditEventBooking();
    });
}

var goToEditEventBooking = function () {

    var row = $('#tblEventBooking tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {

        window.location.replace("/Tenant/EventBooking/Edit/" + ID);
    } else {

    }
}

var addNewEventBooking = function () {
    window.location.replace("/Tenant/EventBooking/Edit/" + 0)
}

var goEventBookingList = function () {
    window.location.replace("/Tenant/EventBooking/")
}

var EventList = function () {
    $.ajax({
        url: '/Event/EventDataList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $("#ddlEvent").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = '<option value=' + elementValue.EventID + '>' + elementValue.EventName + '</option>';
                $("#ddlEvent").append(option);
            })
        }
    });
}