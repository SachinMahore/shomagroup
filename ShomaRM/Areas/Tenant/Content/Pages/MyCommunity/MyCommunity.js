$(document).ready(function () {
    getAllDues();
    getServiceRequestOnAlarm();
    colorNewFunction();
    getEventsList();
    getAmenitiesList();
});

var goToStep = function (stepid, id) {

    if (stepid == "1") {
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");

        $("#step1").removeClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
    }
    if (stepid == "2") {
        $("#li1").removeClass("active");
        $("#li2").addClass("active");
        $("#li3").removeClass("active");

        $("#step1").addClass("hidden");
        $("#step2").removeClass("hidden");
        $("#step3").addClass("hidden");
    }
    if (stepid == "3") {
        $("#li1").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").addClass("active");

        $("#step1").addClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").removeClass("hidden");
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
                    // span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #00bfff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<li  id= Event' + elementValue.EventID + '><button class="button-square" style="background-color: #00bfff;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';

                }
                else if (elementValue.Type == 2) {
                    //span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #a2d900;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<li  id= Event' + elementValue.EventID + '><button class="button-square" style="background-color: #a2d900;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';


                }
                else if (elementValue.Type == 3) {
                    //span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<li  id= Event' + elementValue.EventID + '><button class="button-square" style="background-color: #ff6;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';


                }
                else if (elementValue.Type == 4) {
                    // span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6347;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<li  id= Event' + elementValue.EventID + '><button class="button-square" style="background-color: #ff6347;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';


                }
                else if (elementValue.Type == 5) {
                    //span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #daf6ff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<li  id= Event' + elementValue.EventID + '><button class="button-square" style="background-color: #00bfff;z-index:999;">' + elementValue.EventDateString + '</button><button class="button-square" style="background-color:#4D738A">' + elementValue.EventName + '</button><button class="button-pill" style="background-color:#4D738A">&nbsp;</button></li>';


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
                var html = "<span id='" + elementValue.AmenityID +"' style='font-size: 8pt;color: #898279;'>" + elementValue.Amenity +"</span></br>";
               
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
var getAllDues = function () {
    var UserId = $("#hdnUserId").val();
    var model = {
        UserId: UserId,
    };
    $.ajax({
        url: '/ApplyNow/GetAllTotalDues',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#spanCurrentAmountDue').text(response.model.TotalAmountString);
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