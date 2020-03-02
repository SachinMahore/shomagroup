$(document).ready(function () {
    $("#divLoader").show();
    getCommunityActivityList();
    getServiceRequestOnAlarm();
    getEventsList();
    getRatings();
    
    $("#ratingButtons button").click(function () {
        saveRatings(this.innerHTML);
    });
    uploadAttachFileCommunityActivity();
    //colorFunction();
    colorNewFunction();
    //setInterval(function () { getCommunityActivityList(); }, 10000);
    breakdownPaymentFunction();

    $("#ratingButtons button").on("mouseover", function () {
        var value = this.innerHTML;
        $(".btn-add-on").removeClass("ratingpoor").removeClass("ratingavg").removeClass("ratinggood");
        for (var i = 0; i <= value; i++) {
            if (value <= 3) {
                $("#rtbtn" + i).addClass("ratingpoor");
            } else if (value>3 && value <= 7) {
                $("#rtbtn" + i).addClass("ratingavg");
            }
            else if (value> 7) {
                $("#rtbtn" + i).addClass("ratinggood");
            }
           
        }

    });
    $("#ratingButtons button").on("mouseout", function () {
        //var value = this.innerHTML;
        $(".btn-add-on").removeClass("ratingpoor").removeClass("ratingavg").removeClass("ratinggood");
    });

    $("#chkDisclaimer").on('ifChanged', function (event) {
        $("#divLoader").show();
        var checkboxChecked = $(this).is(':checked');
        var status;

        if (checkboxChecked) {
            status = 1;
            $("#btnPostCommunity").attr("disabled", false);
        } else {
            status = 0;
            $("#btnPostCommunity").attr("disabled", true).css('color', '#4D738A');
        }

        saveUpdatePostDisclaimer(status);

    });

});

var goToMakeAPayment = function () {
    model = {
        StepId: 3,
        PayStepId: 1
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
//piyush
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

var uploadAttachFileCommunityActivity = function () {
    document.getElementById('fileCommunityActivity').onchange = function () {
        attatchFileUpload();
    };
};

var attatchFileUpload = function () {
    $formData = new FormData();

    var uploadAttatch = document.getElementById('fileCommunityActivity');

    for (var i = 0; i < uploadAttatch.files.length; i++) {
        $formData.append('file-' + i, uploadAttatch.files[i]);
    }

    $.ajax({
        url: '/CommunityActivity/uploadAttatchFileCommunityActivity',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndfileCommunityActivity').val(response.model.AttatchFile);
            $('#hndfileCommunityActivityOriginalName').val(response.model.AttachFileOriginalName);

            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

var saveUpdateCommunityPost = function () {

   
    $("#divLoader").show();

    
    var msg = '';
    var model = {
        TenantId: $('#hndTenantID').val(),
        Details: $('#txtCommunityActivity').val(),
        AttatchFile: $('#hndfileCommunityActivity').val(),
        AttachFileOriginalName: $('#hndfileCommunityActivityOriginalName').val(),
    };
    if ($('#txtCommunityActivity').val() == "") {
        if ($('#hndfileCommunityActivity').val() == "") {
            msg += "Enter the Datails</br>";
        }
    }
    if ($('#chkDisclaimer').is(":checked")) {
        chkDisclaimer = true;
    }
    else {
        chkDisclaimer = false;
    }

    if (chkDisclaimer == false) {
        msg += "Please Agree the Disclaimer</br>";
    }
    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        $("#divLoader").hide();
        return;

    }

    $.ajax({
        url: '/CommunityActivity/SaveUpdateCommunityActivity',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#txtCommunityActivity').val('');
            $('#hndfileCommunityActivity').val('');
            $('#hndfileCommunityActivityOriginalName').val('');
            getCommunityActivityList();
            $.alert({
                title: '',
                content: response.model,
                type: 'red'
            });
        }
    });
};

var getCommunityActivityList = function () {
    //var model = { TenantId: $('#hdnUserId').val() };

    $.ajax({
        url: '/CommunityActivity/GetCommunityActivityList',
        type: 'post',
        contentType: 'application/json utf-8',
        //data: JSON.stringify(model),
        dataType: 'json',
        success: function (response) {
            getIsAgreeDisclaimer();
            var attachFile = '';
            $('#DivCommunityActivityList').empty();
            $.each(response.model, function (elementType, elementValue) {
                var fileExist = doesFileExist('/Content/assets/img/CommunityPostFiles/' + elementValue.AttatchFile);
                if (fileExist == true) {
                    attachFile = "<a href='/Content/assets/img/CommunityPostFiles/" + elementValue.AttatchFile + "' target='_blank'><i class='fa fa-paperclip'></i> Attachment</a>";
                }
                else {
                    attachFile = "<a href='javascript:void(0);' onclick='fileNotFound();'><i class='fa fa-paperclip'></i> Attachment</a>";
                }
                var Html = '<div class="col-md-12 m-b15">';
                Html += '<div class="row">';
                Html += '<div class="col-lg-1 col-md-2 col-sm-1 col-xs-4 flot-box">';
                if (elementValue.ProfilePicture == null) {
                    Html += '<img src="/Content/assets/img/circle.png" class="img-circle" height="50" width="50" />';
                }
                else {
                    var fileEx = doesFileExist('/Content/assets/img/tenantProfile/' + elementValue.ProfilePicture);
                    if (fileEx == true) {
                        Html += '<img src="/Content/assets/img/tenantProfile/' + elementValue.ProfilePicture + '" class="img-circle" height="50" width="50" />';
                    }
                    else {
                        Html += '<img src="/Content/assets/img/circle.png" class="img-circle" height="50" width="50" />';
                    }
                }
                Html += '</div>';
                Html += '<div class="col-md-8 col-sm-7 col-xs-8 date-spacing p-l0 flot-box">';
                Html += '<span class="client-name">' + elementValue.TenantName + '</span><br>';
                Html += '<span class="span">' + elementValue.DateString + '</span>';
                Html += '</div>';
                Html += '</div>';
                Html += '<div class="col-md-12 padding-box comment-text p-lr0">' + elementValue.Details;
                Html += '</div>';
                if (elementValue.AttatchFile != null) {
                    Html += '<span >' + "  " + attachFile + '</span>';
                }
                Html += '</div>';

                $('#DivCommunityActivityList').append(Html);
            });
            shorten();
            $("#divLoader").hide();
        }
    });
};
//var getCommunityActivityList = function () {
//    //var model = { TenantId: $('#hdnUserId').val() };

//    $.ajax({
//        url: '/CommunityActivity/GetCommunityActivityList',
//        type: 'post',
//        contentType: 'application/json utf-8',
//        //data: JSON.stringify(model),
//        dataType: 'json',
//        success: function (response) {
//            getIsAgreeDisclaimer();
//            var attachFile = '';
//            $('#DivCommunityActivityList').empty();
//            $.each(response.model, function (elementType, elementValue) {
//                var fileExist = doesFileExist('/Content/assets/img/CommunityPostFiles/' + elementValue.AttatchFile);
//                if (fileExist == true) {
//                    attachFile = "<a href='/Content/assets/img/CommunityPostFiles/" + elementValue.AttatchFile + "' target='_blank'><i class='fa fa-paperclip'></i> Attachment</a>";
//                }
//                else {
//                    attachFile = "<a href='javascript:void(0);' onclick='fileNotFound();'><i class='fa fa-paperclip'></i> Attachment</a>";
//                }
//                var Html = '<div class="col-lg-12" style="margin-bottom: 2% !important;">';
//                Html += '<div class="col-lg-12">';
//                Html += '<div class="col-lg-1">';
//                if (elementValue.ProfilePicture == null) {
//                    Html += '<img src="/Content/assets/img/Circle.png" class="img-circle" height="50" width="50" />';
//                }
//                else {
//                    var fileEx = doesFileExist('/Content/assets/img/tenantProfile/' + elementValue.ProfilePicture);
//                    if (fileEx == true) {
//                        Html += '<img src="/Content/assets/img/tenantProfile/' + elementValue.ProfilePicture + '" class="img-circle" height="50" width="50" />';
//                    }
//                    else {
//                        Html += '<img src="/Content/assets/img/Circle.png" class="img-circle" height="50" width="50" />';
//                    }
//                }
//                Html += '</div>';
//                Html += '<div class="col-lg-8">';
//                Html += '<span style="font-size: 130%;">' + elementValue.TenantName + '</span><br />';
//                Html += '<span style="color:#B7B0A7 !important;font-size: 96%!important;">' + elementValue.DateString + '</span>';
//                Html += '</div>';
//                Html += '</div>';
//                Html += '<div class="col-lg-12 comment" style="margin-left: 20px;">' + elementValue.Details;
//                Html += '</div>';
//                if (elementValue.AttatchFile != null) {
//                    Html += '<span style="margin-left: 11%;">' + "  " + attachFile + '</span>';
//                }
//                Html += '</div>';

//                $('#DivCommunityActivityList').append(Html);
//            });
//            shorten();
//            $("#divLoader").hide();
//        }
//    });
//};

//Amit

var getEventsList = function () {
    $.ajax({
        url: '/Dashboard/GetEventList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var span = '';
            $("#divEvents").empty();
            span += '<div  class="list-group">';
            $.each(response.model, function (elementType, elementValue) {
                if (elementValue.Type == 1) {
                    // span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #00bfff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';

                    span += '<div  id= Event' + elementValue.EventID + '> <div class="Wraper m-b10 "><div class="events-count"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventDateString + '</div><div class="events-text"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventName + '</div></div></div>';
                }
                else if (elementValue.Type == 2) {
                    //span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #a2d900;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<div  id= Event' + elementValue.EventID + '> <div class="Wraper m-b10 "><div class="events-count1"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventDateString + '</div><div class="events-text" onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventName + '</div></div></div>';


                }
                else if (elementValue.Type == 3) {
                    //span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<div  id= Event' + elementValue.EventID + '> <div class="Wraper m-b10 "><div class="events-count2"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventDateString + '</div><div class="events-text"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventName + '</div></div></div>';


                }
                else if (elementValue.Type == 4) {
                    // span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6347;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<div id= Event' + elementValue.EventID + '> <div class="Wraper m-b10 "><div class="events-count3"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventDateString + '</div><div class="events-text" onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventName + '</div></div></div>';


                }
                else if (elementValue.Type == 5) {
                    //span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #daf6ff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + '</li>';
                    span += '<div  id= Event' + elementValue.EventID + '> <div class="Wraper m-b10 "><div class="events-count2"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventDateString + '</div><div class="events-text"  onclick="eventDetailShow(' + elementValue.EventID + ')">' + elementValue.EventName + '</div></div></div>';


                }

            });
            span += '</div>';
            $("#divEvents").append(span);
            $("#divLoader").hide();
        }
    });
}

var getRatings = function () {
    var UserId = $("#hdnUserId").val();
    //alert($("#hdnUserId").val());
    var model = {
        UserId: UserId
    };
    $.ajax({
        url: '/Dashboard/GetRatings',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            //console.log(response.model.Rating);
            if (response.model.Rating == null) {
                $('#popRateOpinion').modal("show");
            }
            else {
                $('#popRateOpinion').modal("hide");
            }

        }
    });
};

var saveRatings = function (rated) {

    var UserId = $("#hdnUserId").val();
    var ratings = rated;
    console.log(UserId + " " + ratings);
    var model = {
        TenantId: UserId,
        Rating: ratings
    };
    $.ajax({
        url: '/Dashboard/SaveRatings',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#popRateOpinion').modal("hide");
        }
    });
};

//Zabuto

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
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #00bfff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + "    " + '<a href="javascript:void(0)" class="pull-right" onclick="tenantEventsJoin(' + elementValue.EventID +')" style="margin-right:30px;color:#4d738a;">Join Event</a></li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">'+ elementValue.EventName + '</button></li>';

                }
                else if (elementValue.Type == 2) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #a2d900;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + "    " + '<a href="javascript:void(0)" class="pull-right" onclick="tenantEventsJoin(' + elementValue.EventID +')" style="margin-right:30px;color:#4d738a;">Join Event</a></li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';

                }
                else if (elementValue.Type == 3) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + "    " + '<a href="javascript:void(0)" class="pull-right" onclick="tenantEventsJoin(' + elementValue.EventID +')" style="margin-right:30px;color:#4d738a;">Join Event</a></li>';
                    //span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';


                }
                else if (elementValue.Type == 4) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #ff6347;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + "    " + '<a href="javascript:void(0)" class="pull-right" onclick="tenantEventsJoin(' + elementValue.EventID +')" style="margin-right:30px;color:#4d738a;">Join Event</a></li>';
                    // span += '<li  id= Event' + elementValue.EventID + '><button class="button-square">' + elementValue.EventDateString + '</button><button class="button-pill">' + elementValue.EventName + '</button></li>';


                }
                else if (elementValue.Type == 5) {
                    span += '<li  id= Event' + elementValue.EventID + '><i class="fa fa-square fa-lg" style="color: #daf6ff;"></i> ' + elementValue.EventDateString + "  " + elementValue.EventName + "    " + '<a href="javascript:void(0)" class="pull-right" onclick="tenantEventsJoin(' + elementValue.EventID +')" style="margin-right:30px;color:#4d738a;">Join Event</a></li>';
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
    var duplicates_list = [];
    var unique_list = [];
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

                //if ($.inArray(elementValue.EventDateText, unique_list) == -1) {
                //    unique_list.push(elementValue.EventDateText);
                if (elementValue.Type == 1) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorNeigh" });
                }
                else if (elementValue.Type == 2) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorCommu" });
                }
                else if (elementValue.Type == 3) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName + "\r\nABC Event", classname: "colorOther" });
                }
                else if (elementValue.Type == 4) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorMulti" });
                }
                else if (elementValue.Type == 5) {
                    eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorCurrDate" });
                }

                //} else {
                //    if ($.inArray(elementValue.EventDateText, duplicates_list) == -1) {
                //        duplicates_list.push(elementValue.EventDateText);

                //        if (elementValue.Type == 1) {
                //            ///eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorNeigh" });
                //            $('.colorCommu,.colorOther,.colorMulti,.colorCurrDate').css({
                //              //  background: "-webkit-gradient(linear, left top, left bottom, from(#ccc), to(#000))",
                //                background: "linear-gradient(110deg, #00bfff 60%, #00bfff 60%);"
                //            });
                //        }
                //        else if (elementValue.Type == 2) {
                //            //eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorCommu" });
                //            $('.colorNeigh,.colorOther,.colorMulti,.colorCurrDate').css({
                //                //  background: "-webkit-gradient(linear, left top, left bottom, from(#ccc), to(#000))",
                //                background: "linear-gradient(110deg, #a2d900 60%, #a2d900 60%);"
                //            });
                //        }
                //        else if (elementValue.Type == 3) {
                //           // eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorOther" });
                //            $('.colorNeigh,.colorCommu,.colorMulti,.colorCurrDate').css({
                //                //  background: "-webkit-gradient(linear, left top, left bottom, from(#ccc), to(#000))",
                //                background: "linear-gradient(110deg, #a2d900 60%, #a2d900 60%);"
                //            });
                //        }
                //        else if (elementValue.Type == 4) {
                //            eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorMulti" });
                //        }
                //        else if (elementValue.Type == 5) {
                //            eventData.push({ date: testDAte, badge: true, title: elementValue.EventName, classname: "colorCurrDate" });
                //        }
                //        //$('#colorNeigh').css({
                //        //    background: "-webkit-gradient(linear, left top, left bottom, from(#ccc), to(#000))"
                //        //});
                //    }
                //}



            });

            console.log(duplicates_list);

            $("#spanEventCalender")({
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
            var modalCloseDate1 = document.getElementById("closeDate1");
            modalCloseDate.onclick = function () { modalOpenDate.style.display = "none"; };
            modalCloseDate1.onclick = function () { modalOpenDate.style.display = "none"; };
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
                var eventColors = "";
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
                        eventColors += '<div class="event-box" style="background-color:#00BFF3"></div>';
                    }
                    else if ($.trim(types[0]) == "2") {
                        eventColors += '<div class="event-box" style="background-color:#8DB954"></div>';
                    }
                    else if ($.trim(types[0]) == "3") {
                        eventColors += '<div class="event-box" style="background-color:#ED1C24"></div>';
                    }
                    else if ($.trim(types[0]) == "4") {
                        eventColors += '<div class="event-box" style="background-color:#FFF200"></div>';
                    }
                    else if ($.trim(types[0]) == "5") {
                        eventColors += '<div class="event-box" style="background-color:#B3ADA6"></div>';
                    }
                }
                else {
                    for (var i = 0; i <= types.length; i++) {
                        if ($.trim(types[i]) == "1") {

                            eventColors += '<div class="event-box" style="background-color:#00BFF3"></div>';
                        }
                        else if ($.trim(types[i]) == "2") {
                            eventColors += '<div class="event-box" style="background-color:#8DB954"></div>';
                        }
                        else if ($.trim(types[i]) == "3") {
                            eventColors += '<div class="event-box" style="background-color:#ED1C24"></div>';
                        }
                        else if ($.trim(types[i]) == "4") {
                            eventColors += '<div class="event-box" style="background-color:#FFF200"></div>';
                        }
                        else if ($.trim(types[i]) == "5") {
                            eventColors += '<div class="event-box" style="background-color:#B3ADA6"></div>';
                        }
                    }
                }
                console.log(eventColors + " " + types.length);
              
                eventData.push({ date: eventDate, badge: false, title: eventName, lineargradient: eventColors });
            });
            console.log(eventData);

            $("#spanEventCalender").zabuto_calendar({
                data: eventData,
                cell_border: true,
                today: true,
                show_today: true,
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
            var modalCloseDate1 = document.getElementById("closeDate1");
            modalCloseDate.onclick = function () { modalOpenDate.style.display = "none"; };
            modalCloseDate1.onclick = function () { modalOpenDate.style.display = "none"; };
        }
    });
}

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
                var Html = '<div class="bell-text" id="spanServiceRequestlabelAlarm">' + elementValue.ProblemCategoryName + '</div>';              
               
                if (elementValue.PermissionComeDateString == 'Any Time') {
                    Html += '<div class="bell-sub-text" id="spanServiceRequestDateTimeAlarm">' + elementValue.PermissionComeDateString + '</div>';
                }
                else {
                    Html += '<div class="bell-sub-text" id="spanServiceRequestDateTimeAlarm">' + elementValue.PermissionComeDateString + ' at ' + elementValue.PermissionComeTime +'</div>';                   
                }                
                $('#divServiceRequestAlarm').append(Html);
            });

            //if (response.model.length == '0') {
            //    var Html = '<div class="center">';
            //    Html += '<span id="spanServiceRequestlabelAlarm" style="font-size:08px;">No Service Request Today</span>';
            //    Html += '</div>';
            //    Html += '<div class="center">';
            //    Html += '<span id="spanServiceRequestDateTimeAlarm" style="font-size:08pt;"></span>';
            //    Html += '</div>';
            //    $('#divServiceRequestAlarm').append(Html);
            //}
        }
    });
};

//Shorten

var shorten = function () {
    $(".comment").shorten();
    //$(".comment").shorten({
    //    "showChars": 20
    //});
};

var fileNotFound = function () {
    $.alert({
        title: "",
        content: 'File Not Found!',
        type: 'red'
    });
};

var getIsAgreeDisclaimer = function () {
    var model = {id: $("#hndTenantID").val()};

    $.ajax({
        url: "/Admin/Tenant/getTenantOnlineData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
           // console.log(response.model.IsAgreePostDisclaimer);

            if(response.model.IsAgreePostDisclaimer == 0) {
                $("#chkDisclaimer").iCheck('uncheck');
                $("#btnPostCommunity").attr("disabled", true).css('background-color', '#4D738A');
            }
            else {
                
                $("#chkDisclaimer").iCheck('check');
                $("#btnPostCommunity").attr("disabled", false);
            }
        }
    });

};


var saveUpdatePostDisclaimer = function (status) {
   // alert($("#hndTenantID").val());
    var model = {
        TenantID: $("#hndTenantID").val(),
        IsAgreePostDisclaimer: status
    };

    $.ajax({
        url: "/Admin/Tenant/SaveUpdatePostDisclaimer",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#divLoader").hide();
            //getIsAgreeDisclaimer();
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
            //$("#spanCurrentAmountDue").text('$' + formatMoney(response.modal.TotalMonthlyCharges));
            $("#lblCurrentPrePayAmount").text('$' + formatMoney(response.modal.TotalMonthlyCharges));
            localStorage.setItem('currentAmountDue', response.modal.TotalMonthlyCharges);
        }
    });
    $("#divLoader").hide();
};

var eventDetailShow = function (id) {
    getEventJoiningStatus(id);
    $('#hndEventID').val(id);
    var model = { EventID: id };

    $.ajax({
        url: '/Tenant/Dashboard/GetEventDetail',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#lblEventName").text(response.model.EventName);
            $("#lblEventDate").text(response.model.EventDateString);
            $("#lblEventTime").text(response.model.EventTimeString);
            $("#lblEventDetail").text(response.model.Description);
        }
    });
    $("#popEventDetails").modal('show');
};

var saveTenantEventsJoin = function () {
    $("#divLoader").show();
    var model = { EventID: $('#hndEventID').val(), TenantID: $('#hndTenantID').val() };

    $.alert({
        title: "",
        content: "Are you sure to join this Event?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: '/Dashboard/SaveTenantEventJoin',
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: response.model,
                                type: 'blue'
                            });
                            getEventJoiningStatus($('#hndEventID').val());
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
    $("#divLoader").hide();
};

var tenantEventsJoin = function (id) {
    $("#divLoader").show();
    var model = { EventID: id, TenantID: $('#hndTenantID').val() };

    $.alert({
        title: "",
        content: "Are you sure to join this Event?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: '/Dashboard/SaveTenantEventJoin',
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: response.model,
                                type: 'blue'
                            });
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
    $("#divLoader").hide();
};

var getEventJoiningStatus = function (id) {
    $("#divLoader").show();
    var model = { EventID: id, TenantID: $('#hndTenantID').val() };

    $.ajax({
        url: '/Dashboard/GetEventJoiningStatus',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            console.log(JSON.stringify(response));
            if (response.model == "join") {
                $("#requestToJoin").removeClass("hidden");
                $("#requestToUnjoin").addClass("hidden");
            }
            else if (response.model == "unjoin") {
                $("#requestToUnjoin").removeClass("hidden");
                $("#requestToJoin").addClass("hidden");
            }
        }
    });
        $("#divLoader").hide();
};

var saveTenantEventsUnjoin = function () {
    $("#divLoader").show();
    var model = { EventID: $('#hndEventID').val(), TenantID: $('#hndTenantID').val() };

    $.alert({
        title: "",
        content: "Are you sure to un-join this Event?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: '/Dashboard/SaveTenantEventUnJoin',
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: response.model,
                                type: 'blue'
                            });
                            getEventJoiningStatus($('#hndEventID').val());
                        }
                    });
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                    return;
                }
            }
        }
    });
    $("#divLoader").hide();
};