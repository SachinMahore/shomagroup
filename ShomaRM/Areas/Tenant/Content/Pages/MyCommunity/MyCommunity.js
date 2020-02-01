$(document).ready(function () {
    getServiceRequestOnAlarm();
    colorNewFunction();
    getEventsList();
    getAmenitiesList();
    breakdownPaymentFunction();
    $('#ClubSubmit').attr('disabled', 'disabled');
});
function SubmitClub() {
    var Json = {
        Id: $("#txtClubTitle").val(),
        ClubTitle: $("#txtClubTitle").val(),
        ActivityId: $("#ActivityId").val(),
        StartDate: $("#txtStartDate").val(),
        Venue: $("#txtVenue").val(),
        DayId: $("#DayId").val(),
        Time: $("#txtMeetingTime").val(),
        Contact: $("#txtContact").val(),
        Email: $("#txtEmail").val(),
        PhoneNumber: $("#txtPhoneNumber").val(),
        PhoneCheck: $("#rbtnPhoneCheck").is(":checked"),
        EmailCheck: $("#rbtnEmailCheck").is(":checked"),
        LevelId: $("#LevelId").val(),
        SpecialInstruction: $("#txtSpecialInstruction").val(),
        Description: $("#txtDescription").val(),
        BriefDescription: $("#txtBriefDescription").val(),
        TermsAndCondition: $("#chkTermsConditions").val(),
        TenantID: $("#hndTenantID").val(),
        UserId: $("#hdnUserId").val(),
        IsDeleted: false

    };
    $.post("/MyCommunity/CreateClub", Json, function (Data) {
        debugger
        $.alert({
            title: '',
            content: response.modal,
            type: 'blue'
        });
    });
}


var goToStep = function (stepid, id) {

    if (stepid == "1") {
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");

        $("#step1").removeClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
    }
    if (stepid == "2") {
        $("#li1").removeClass("active");
        $("#li2").addClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");

        $("#step1").addClass("hidden");
        $("#step2").removeClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
    }
    if (stepid == "3") {
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").addClass("active");
        $("#li4").removeClass("active");

        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").removeClass("hidden");
        $("#step4").addClass("hidden");
    }
    if (stepid == "4") {
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").addClass("active");

        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").removeClass("hidden");
    }
};

function myDateFunction(id) {
    var date = $("#" + id).data("date");

    var model = { dt: date };
    $.ajax({
        url: '/Dashboard/GetCalEventList',
        type: 'post',
        dataType: 'json',
        data: JSON.stringify(model),
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var span = '';
            var eventData = [];
            var customEvent = {};
            $("#divEventsCal").empty();
            span += '<ul class="list-group" style="list-style: none;">';
            $.each(response.model, function (elementType, elementValue) {
                if (elementValue.Type == 1) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #00bfff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">'+ elementValue.EventName + '</button></li>';

                }
                else if (elementValue.Type == 2) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #a2d900;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';

                }
                else if (elementValue.Type == 3) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    //span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';


                }
                else if (elementValue.Type == 4) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6347;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';


                }
                else if (elementValue.Type == 5) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #daf6ff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';


                }

            });
            if (response.model.length == '0') {
                span += '<li>No Community Events on this date.</li>';
            }
            span += '</ul>';
            $("#divEventsCal").append(span);
        }
    });
    $("#date-popover").show();
    return true;
}

function colorFunction() {
    var eventData = [];
    var customEvent = {};
    $.ajax({
        url: '/Dashboard/GetDateEventList',
        type: 'post',
        dataType: 'json',
        // data: JSON.stringify(model),
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $.each(response.model, function (elementType, elementValue) {
                var dateTest = elementValue.EventDateText;
                var d = new Date(dateTest),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2) month = '0' + month;
                if (day.length < 2) day = '0' + day;

                var testDAte = [year, month, day].join('-');

                if (elementValue.Type == 1) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorNeigh" });
                }
                else if (elementValue.Type == 2) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorCommu" });
                }
                else if (elementValue.Type == 3) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorOther" });
                }
                else if (elementValue.Type == 4) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorMulti" });
                }
                else if (elementValue.Type == 5) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorCurrDate" });
                }

            });
            $("#spanEventCalender").zabuto_calendar({
                data: eventData,
                cell_border: true,
                today: true,
                show_days: false,
                weekstartson: 0,
                nav_icon: {
                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                    next: '<i class="fa fa-chevron-circle-right"></i>'
                },
                action: function () {
                    return myDateFunction(this.id);
                }
            });
            $("#spanEventCalender2").zabuto_calendar({
                data: eventData,
                cell_border: true,
                today: true,
                show_days: false,
                weekstartson: 0,
                nav_icon: {
                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                    next: '<i class="fa fa-chevron-circle-right"></i>'
                },
                action: function () {
                    return myDateFunction(this.id);
                }
            });

            var modalOpenDate = document.getElementById("date-popover");
            var modalCloseDate = document.getElementById("closeDate");
            modalCloseDate.onclick = function () { modalOpenDate.style.display = "none"; }
        }
    });
}

function colorNewFunction() {
    var eventData = [];
    var duplicates_list = [];
    var unique_list = [];
    $.ajax({
        url: '/Dashboard/GetNewDateEventList',
        type: 'post',
        dataType: 'json',
        // data: JSON.stringify(model),
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $.each(response.model, function (elementType, elementValue) {

                var types = elementValue.TypeText.split(',');
                var eventName = elementValue.EventName;
                var eventDate = elementValue.EventDateText;
                var eventColors = "to right";
                var colorPercentage = 0;
                if (types.length == 2) {
                    colorPercentage = 50;
                }
                else if (types.length == 3) {
                    colorPercentage = 33.33;
                }
                else if (types.length == 4) {
                    colorPercentage = 25;
                }
                else if (types.length == 5) {
                    colorPercentage = 20;
                }

                if (types.length == 1) {
                    if ($.trim(types[0]) == "1") {
                        eventColors += ",#00bfff 100%,#00bfff 100%";
                    }
                    else if ($.trim(types[0]) == "2") {
                        eventColors += ",#a2d900 100%,#a2d900 100%";
                    }
                    else if ($.trim(types[0]) == "3") {
                        eventColors += ",#ff6 100%,#ff6 100%";
                    }
                    else if ($.trim(types[0]) == "4") {
                        eventColors += ",#ff6347 100%,#ff6347 100%";
                    }
                    else if ($.trim(types[0]) == "5") {
                        eventColors += ",#daf6ff 100%,#daf6ff 100%";
                    }
                }
                else {
                    for (var i = 0; i <= types.length; i++) {
                        if ($.trim(types[i]) == "1") {

                            eventColors += ",#00bfff " + colorPercentage * (i) + "%,#00bfff " + colorPercentage * (i + 1) + "%";
                        }
                        else if ($.trim(types[i]) == "2") {
                            eventColors += ",#a2d900 " + colorPercentage * (i) + "%,#a2d900 " + colorPercentage * (i + 1) + "%";
                        }
                        else if ($.trim(types[i]) == "3") {
                            eventColors += ",#ff6 " + colorPercentage * (i) + "%,#ff6 " + colorPercentage * (i + 1) + "%";
                        }
                        else if ($.trim(types[i]) == "4") {
                            eventColors += ",#ff6347 " + colorPercentage * (i) + "%,#ff6347 " + colorPercentage * (i + 1) + "%";
                        }
                        else if ($.trim(types[i]) == "5") {
                            eventColors += ",#daf6ff " + colorPercentage * (i) + "%,#daf6ff " + colorPercentage * (i + 1) + "%";
                        }
                    }

                }
                console.log(eventColors + " " + types.length);
                $('.neighbor').css({
                    background: "linear-gradient(" + eventColors + ")!important;"
                });
                eventData.push({ date: eventDate, badge: false, title: eventName, lineargradient: eventColors });
            });
            console.log(eventData);

            $("#spanEventCalender").zabuto_calendar({
                data: eventData,
                cell_border: true,
                today: true,
                show_days: false,
                weekstartson: 0,
                nav_icon: {
                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                    next: '<i class="fa fa-chevron-circle-right"></i>'
                },
                action: function () {
                    return myDateFunction(this.id);
                }
            });
            $("#spanEventCalender2").zabuto_calendar({
                data: eventData,
                cell_border: true,
                today: true,
                show_days: false,
                weekstartson: 0,
                nav_icon: {
                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                    next: '<i class="fa fa-chevron-circle-right"></i>'
                },
                action: function () {
                    return myDateFunction(this.id);
                }
            });

            var modalOpenDate = document.getElementById("date-popover");
            var modalCloseDate = document.getElementById("closeDate");
            modalCloseDate.onclick = function () { modalOpenDate.style.display = "none"; }
        }
    });
}

var getEventsList = function () {
    $.ajax({
        url: '/Dashboard/GetEventList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var span = '';
            $("#divEvents").empty();
            span += '<ul class="list-group">';
            $.each(response.model, function (elementType, elementValue) {
                if (elementValue.Type == 1) {
                    span += '<li  id= Event' + elementValue.EventID + ' onclick="alreadyJoinTenantEvent(' + elementValue.EventID + ')" style="cursor:pointer;"><button class="button-square" style="background-color: #00bfff;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';
                }
                else if (elementValue.Type == 2) {
                    span += '<li  id= Event' + elementValue.EventID + ' onclick="alreadyJoinTenantEvent(' + elementValue.EventID + ')" style="cursor:pointer;"><button class="button-square" style="background-color: #a2d900;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';
                }
                else if (elementValue.Type == 3) {
                    span += '<li  id= Event' + elementValue.EventID + ' onclick="alreadyJoinTenantEvent(' + elementValue.EventID + ')" style="cursor:pointer;"><button class="button-square" style="background-color: #ff6;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';
                }
                else if (elementValue.Type == 4) {
                    span += '<li  id= Event' + elementValue.EventID + ' onclick="alreadyJoinTenantEvent(' + elementValue.EventID + ')" style="cursor:pointer;"><button class="button-square" style="background-color: #ff6347;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';
                }
                else if (elementValue.Type == 5) {
                    span += '<li  id= Event' + elementValue.EventID + ' onclick="alreadyJoinTenantEvent(' + elementValue.EventID + ')" style="cursor:pointer;"><button class="button-square" style="background-color: #00bfff;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';
                }
            });
            span += '</ul>';
            $("#divEvents").append(span);
        }
    });
}

var getAmenitiesList = function () {
    $.ajax({
        url: '/MyCommunity/GetAmenitiesList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $('#DivCommunityAmenities').empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<span id='" + elementValue.AmenityID + "' style='font-size: 8pt;color: #898279;'>" + elementValue.Amenity + "</span></br>";

                $('#DivCommunityAmenities').append(html);
            });
        }
    });
};

var goToMakeAPayment = function () {
    model = {
        StepId: 3,
        PayStepId: 2
    };

    $.ajax({
        url: '/Dashboard/SetSessionMakePayments',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            window.location.href = '/Tenant/MyAccount/Index/' + response.model;
        }
    });
};

var goToSetUpRecurringPayment = function () {
    model = {
        StepId: 3,
        PayStepId: 4
    };

    $.ajax({
        url: '/Dashboard/SetSessionSetRecurringPayments',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            window.location.href = '/Tenant/MyAccount/Index/' + response.model;
        }
    });
};
var goToReserveAmenities = function () {
    model = {
        StepId: 5,
        PayStepId: 2
    };

    $.ajax({
        url: '/Dashboard/SetSessionSetReserveAmenities',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            window.location.href = '/Tenant/MyAccount/Index/' + response.model;
        }
    });
};
var goToRegisterGuest = function () {
    model = {
        StepId: 7

    };

    $.ajax({
        url: '/Dashboard/SetSessionSetRegisterGuest',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            window.location.href = '/Tenant/MyAccount/Index/' + response.model;
        }
    });
};
var goToSubmitServiceRequest = function () {
    model = {
        StepId: 4,
        ServiceRequestId: 2
    };

    $.ajax({
        url: '/Dashboard/SetSessionSetSubmitServiceRequest',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            window.location.href = '/Tenant/MyAccount/Index/' + response.model;
        }
    });
};

var getServiceRequestOnAlarm = function () {
    var model = { TenantId: $("#hndTenantID").val() };

    $.ajax({
        url: '/ServiceRequest/GetServiceRequestForAlarm',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#divServiceRequestAlarm').empty();
            $.each(response.model, function (elementType, elementValue) {
                var Html = '<div class="center">';
                Html += '<span id="spanServiceRequestlabelAlarm" style="font-size:12pt;">' + elementValue.ProblemCategoryName + '</span>';
                Html += '</div>';
                Html += '<div class="center">';
                if (elementValue.PermissionComeDateString == 'Any Time') {
                    Html += '<span id="spanServiceRequestDateTimeAlarm" style="font-size:08pt;">' + elementValue.PermissionComeDateString + '</span>';
                }
                else {

                    Html += '<span id="spanServiceRequestDateTimeAlarm" style="font-size:08pt;">' + elementValue.PermissionComeDateString + ' at ' + elementValue.PermissionComeTime + '</span>';
                }
                Html += '</div>';
                $('#divServiceRequestAlarm').append(Html);
            });

            //if (response.model.length == '0') {
            //    var Html = '<div class="center">';
            //    Html += '<span id="spanServiceRequestlabelAlarm" style="font-size:08pt;">No Service Request Today</span>';
            //    Html += '</div>';
            //    Html += '<div class="center">';
            //    Html += '<span id="spanServiceRequestDateTimeAlarm" style="font-size:08pt;"></span>';
            //    Html += '</div>';
            //    $('#divServiceRequestAlarm').append(Html);
            //}
        }
    });
};
var openPaymentBreakdown = function () {
    $('#popPaymentBreakdown').modal('show');
};

var breakdownPaymentFunction = function () {
    $("#divLoader").show();
    var model = { UserId: $("#hdnUserId").val() };

    $.ajax({
        url: '/MonthlyPayment/GetMonthlyPayment',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#lblMonthlyChargesBreakdown").text('$' + formatMoney(response.modal.MonthlyCharges));
            $("#lblAdditionalParkingBreakdown").text('$' + formatMoney(response.modal.AdditionalParking));
            $("#lblStorageChargesBreakdown").text('$' + formatMoney(response.modal.StorageCharges));
            $("#lblPetRentBreakdown").text('$' + formatMoney(response.modal.PetRent));
            $("#lblTrashRecycleBreakdown").text('$' + formatMoney(response.modal.TrashRecycle));
            $("#lblPestControlBreakdown").text('$' + formatMoney(response.modal.PestControl));
            $("#lblConvergentBillingBreakdown").text('$' + formatMoney(response.modal.ConvergentBilling));
            $("#lblTotalMonthlyChargesBreakdown").text('$' + formatMoney(response.modal.TotalMonthlyCharges));
            $("#spanCurrentAmountDue").text('$' + formatMoney(response.modal.TotalMonthlyCharges));
            $("#lblCurrentPrePayAmount").text('$' + formatMoney(response.modal.TotalMonthlyCharges));
            localStorage.setItem('currentAmountDue', response.modal.TotalMonthlyCharges);
        }
    });
    $("#divLoader").hide();
};

var tenantEventsJoin = function (id) {
    clearTenantEventJoin();

    var model = { EventID: id };
    $('#hdnEventId').val(id);
    $.ajax({
        url: '/MyCommunity/GetEventDataForTenant',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#lblEventName").text(response.modal.EventName);
            $("#txtTenantEventDate").val(response.modal.EventDateString);
            $("#txtTenantEventDate").attr('disabled', 'disabled');
            $("#txtTenantEventTime").val(response.modal.EventTimeString);
            $("#txtTenantEventTime").attr('disabled', 'disabled');
            if (response.modal.IsFree == true) {
                $("#chkTenantEventFees").iCheck('check');
            }
            else {
                $("#chkTenantEventFees").iCheck('uncheck');
            }
            $("#chkTenantEventFees").attr('disabled', 'disabled');
            $("#txtTenantEventFees").attr('disabled', 'disabled');
            $("#txtTenantEventFees").val(formatMoney(response.modal.Fees));
            if (response.modal.IsFree == false) {
                ddlPaymentMethod();
                $('#DivPaymentMethod').removeClass('hidden');
                $('#DivCvvNumber').removeClass('hidden');
                $('#txtCVVNumber').val('');
                $('#btnTenantEventJoinPay').removeClass('hidden');
                $('#btnTenantEventJoinSave').attr('disabled', 'disabled');
            }
            else {
                $('#DivPaymentMethod').addClass('hidden');
                $('#DivCvvNumber').addClass('hidden');
                $('#txtCVVNumber').val('');
                $('#btnTenantEventJoinPay').addClass('hidden');
                $('#btnTenantEventJoinSave').removeAttr('disabled', 'disabled');
            }
            $('#popEventTenantJoin').modal('show');
        }
    });
};

var saveTenantEventJoin = function () {
    $("#divLoader").show();
    var model = { TenantID: $('#hndTenantID').val(), EventID: $('#hdnEventId').val(), Fees: $('#txtTenantEventFees').val(), Description: $('#txtTenantEventDescription').val() };

    $.ajax({
        url: '/MyCommunity/SaveTenantEventJoin',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#popEventTenantJoin').modal('hide');
            $.alert({
                title: '',
                content: response.modal,
                type: 'blue'
            });
        }
    });
    clearTenantEventJoin();
    $("#divLoader").hide();
};

var clearTenantEventJoin = function () {
    $('#hdnEventId').val('0');
    $('#txtTenantEventFees').val('');
    $('#txtTenantEventDescription').val('');
};

var ddlPaymentMethod = function () {
    var model = { TenantId: $("#hndTenantID").val() };
    $.ajax({
        url: '/PaymentAccounts/GetPaymentMethods',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#ddlPaymentMethod').empty();
            var html = '';
            $.each(response.model, function (elementType, elementValue) {
                if (elementValue.Default == '1') {
                    html = "<option value='" + elementValue.PAID + "' selected='selected' data-value='" + elementValue.PayMethod + "'>" + elementValue.AccountName + "</option>";
                }
                else {
                    html = "<option value='" + elementValue.PAID + "' data-value='" + elementValue.PayMethod + "'>" + elementValue.AccountName + "</option>";
                }
                $('#ddlPaymentMethod').append(html);
            });
            ddlPaymentMethodSelectFunction();
            ddlPayMethodPageLoadFunction();
        }
    });
};

var ddlPaymentMethodSelectFunction = function () {
    $('#ddlPaymentMethod').on('change', function () {

        if ($(this).find(':selected').data('value') == '1') {
            $('#DivCvvNumber').removeClass('hidden');
        }
        else {
            $('#DivCvvNumber').addClass('hidden');
        }
        $('#txtCVVNumber').val('');
    });
};

var ddlPayMethodPageLoadFunction = function () {
    if ($('#ddlPaymentMethod').find(':selected').data('value') == '1') {
        $('#DivCvvNumber').removeClass('hidden');
    }
    else {
        $('#DivCvvNumber').addClass('hidden');
    }
    $('#txtCVVNumber').val('');
};

function PayMethod() {
    var m = '';
    
    if ($('#ddlPaymentMethod').find(':selected').data('value') == '1') {
        if ($("#txtCVVNumber").val() == '') {
            m = "Enter CVV Number</br>";
        }
        else if ($("#txtCVVNumber").val().length < '3') {
            m = "Enter Valid CVV Number</br>";
        }
    }
    if (m != "") {
        $.alert({
            title: 'Alert!',
            content: m,
            type: 'red'
        });
       
        return
    }
    var model = {
        PAID: $('#ddlPaymentMethod').val()
    };

    $.ajax({
        url: '/PaymentAccounts/EditPaymentsAccounts',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#hdnCardName').val(response.model.NameOnCard),
                $('#hdnCardNumber').val(response.model.CardNumber),
                $('#hdnCardMonth').val(response.model.Month),
                $('#hdnCardYar').val(response.model.Year),
                $('#hdnAccountName').val(response.model.AccountName),
                $('#hdnBankName').val(response.model.BankName),
                $('#hdnAccountnumber').val(response.model.AccountNumber),
                $('#hdnRoutingNumber').val(response.model.RoutingNumber)
        }
    });
    
    var msg = "";
    var amount = $("#txtTenantEventFees").val();
    var tenantid = $("#hndTenantID").val();
    var cardName = $('#hdnCardName').val();
    var cardNumber = $('#hdnCardNumber').val();
    var cardMonth = $('#hdnCardMonth').val();
    var cardYear = $('#hdnCardYar').val();
    var accountName = $('#hdnAccountName').val();
    var bankName = $('#hdnBankName').val();
    var accountNumber = $('#hdnAccountName').val();
    var routingNumber = $('#hdnRoutingNumber').val();
    var mod = {
        Charge_Amount: amount,
        TenantID: tenantid,
        NameOnCardString: cardName,
        NumberOnCardString: cardNumber,
        ExpirationMonthOnCardString: cardMonth,
        ExpirationYearOnCardString: cardYear,
        BankName: bankName,
        RoutingNumber: routingNumber
    };
    $.ajax({
        url: "/MyTransaction/SaveUpdateTransaction/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(mod),
        dataType: "JSON",
        success: function (response) {
            if (response.Msg == "Transaction Saved Successfully") {
                $('#btnTenantEventJoinSave').removeAttr('disabled', 'disabled');
                $('#btnTenantEventJoinPay').attr('disabled', 'disabled');
            }
            $.alert({
                title: 'Message!',
                content: response.Msg,
                type: 'blue',
            });
        }
    });
    $('#hdnCardName').val('0');
    $('#hdnCardNumber').val('0');
    $('#hdnCardMonth').val('0');
    $('#hdnCardYar').val('0');
    $('#hdnAccountName').val('0');
    $('#hdnBankName').val('0');
    $('#hdnAccountnumber').val('0');
    $('#hdnRoutingNumber').val('0');
}

var alreadyJoinTenantEvent = function (id) {
    $("#divLoader").show();
    var model = { TenantID: $('#hndTenantID').val(), EventID: id /*$('#hdnEventId').val()*/ };

    $.ajax({
        url: '/MyCommunity/AlreadyJoinTenantEvent',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response.modal == 'You Already Registered For This Event') {
                $('#popEventTenantJoin').modal('hide');
                $.alert({
                    title: '',
                    content: response.modal,
                    type: 'blue'
                });
                return
            }
            else {
                tenantEventsJoin(id)
            }
        }
    });
    $("#divLoader").hide();
};