var remainingday = 0;
$(document).ready(function () {
    onFocus();

    $("#chkAgreePetTerms").on('ifChanged', function (event) {
        if ($(this).is(":checked")) {
            modalPetPolicy.style.display = "block";
        }
        else {
            modalPetPolicy.style.display = "none";
        }
    });
    $("#listUnit tbody").on("click", "tr", function (e) {
        var floorfromplan = localStorage.getItem("floorfromplan");
        var floorid = $(this).data("floorid");
        $("#fa_" + floorid).addClass('active_area', 'active_mouse').data('maphilight', { alwaysOn: true, fillColor: '0af11c', strokeColor: '0af11c' }).trigger('alwaysOn.maphilight');
        if (floorid == floorfromplan) {
            $("#fa_" + floorid).addClass('active_area', 'active_mouse').data('maphilight', { alwaysOn: true, fillColor: '0af11c', strokeColor: '0af11c' }).trigger('alwaysOn.maphilight');
        }
        else {
            $("#fa_" + floorfromplan).removeClass('active_area', 'active_mouse').data('maphilight', { alwaysOn: false }).trigger('alwaysOn.maphilight');
        }
    });

    if ($("#rbtnPaystub").is(":checked")) {
        $('#divUpload3').removeClass('hidden');
        $('#lblUpload1').text('Paystub 1');
        $('#lblUpload2').text('Paystub 2');
        $('#lblUpload3').text('Paystub 3');
    }

    else if ($("#rbtnFedralTax").is(":checked")) {
        $('#divUpload3').addClass('hidden');
        document.getElementById('fileUploadTaxReturn3').value = '';
        $('#lblUpload1').text('Fedral Tax Return 1');
        $('#lblUpload2').text('Fedral Tax Return 2');
        $('#lblUpload3').text('Fedral Tax Return 3');
    }

    $('input[type=radio]').on('ifChanged', function (event) {

        if ($("#rbtnPaystub").is(":checked")) {
            $('#divUpload3').removeClass('hidden');
            $('#lblUpload1').text('Paystub 1');
            $('#lblUpload2').text('Paystub 2');
            $('#lblUpload3').text('Paystub 3');
        }
        else if ($("#rbtnFedralTax").is(":checked")) {
            $('#divUpload3').addClass('hidden');
            document.getElementById('fileUploadTaxReturn3').value = '';
            $('#lblUpload1').text('Fedral Tax Return 1');
            $('#lblUpload2').text('Fedral Tax Return 2');
            $('#lblUpload3').text('Fedral Tax Return 3');
        }
    });

    if ($("#chkAgreeTermsPolicy").is(":checked")) {
        $("#policyStart").attr("disabled", false);

    }

    else if ($("#chkAgreeTermsPolicy").is(":not(:checked)")) {
        $("#policyStart").attr("disabled", true);
    }



    if ($("#chkRentalQual,#chkRentalPolicy").is(":checked")) {
        $("#policyStart").attr("disabled", false);
    }

    else if ($("#chkRentalQual,#chkRentalPolicy").is(":not(:checked)")) {
        $("#policyStart").attr("disabled", true);
    }



    if ($("#chkDontHaveVehicle").is(":checked")) {
        $("#btnAddVehicle").attr("disabled", true);
        $("#btnAddVehicle").css("background-color", "#b4ada5");
    }

    else if ($("#chkDontHaveVehicle").is(":not(:checked)")) {
        $("#btnAddVehicle").attr("disabled", false);
    }

    $('input[type=checkbox]').on('ifChanged', function (event) {
        if ($("#chkDontHaveVehicle").is(":checked")) {
            $("#btnAddVehicle").attr("disabled", true);
            $("#btnAddVehicle").css("background-color", "#b4ada5");
            haveVehicle();
        }
        else if ($("#chkDontHaveVehicle").is(":not(:checked)")) {
            $("#btnAddVehicle").attr("disabled", false);
            haveVehicle();
        }
    });

    if ($("#chkDontHavePet").is(":checked")) {
        $("#btnAddPet").attr("disabled", true);
        $("#btnAddPet").css("background-color", "#b4ada5");
    }

    else if ($("#chkDontHavePet").is(":not(:checked)")) {
        $("#btnAddPet").attr("disabled", false);
    }

    $('input[type=checkbox]').on('ifChanged', function (event) {
        if ($("#chkDontHavePet").is(":checked")) {
            $("#btnAddPet").attr("disabled", true);
            $("#btnAddPet").css("background-color", "#b4ada5");
            havePet();
        }
        else if ($("#chkDontHavePet").is(":not(:checked)")) {
            $("#btnAddPet").attr("disabled", false);
            havePet();
        }
    });

    tenantOnlineID = $("#hdnOPId").val();
    getTenantOnlineList(tenantOnlineID);
    setTimeout(function () {
        fillStateDDL();
    }, 1500);
    $("#ddlState").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityList(selected);
        }
    });

    $("#ddlStateHome").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            //fillCityListHome(selected);
        }
    });
    $("#ddlStateEmployee").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            //fillCityListEmployee(selected);
        }
    });
    $("#ddlStateContact").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            //fillCityListContact(selected);
        }
    });
    
    $("#btnParking").on("click", function (event) {
        fillParkingList();
        addParkingArray = [];
        $("#popParking").PopupWindow("open");
    });
    
    $("#btnFob").on("click", function (event) {
        fillFOBList();
        $("#popFobs").PopupWindow("open");
    });
    
    $("#btnStorage").on("click", function (event) {
        fillStorageList();
        $("#popStorage").PopupWindow("open");
    });
    
    $("#btnPetPlace").on("click", function (event) {
        fillPetPlaceList();
        addPetPlaceArray = [];
        $("#popPetPlace").PopupWindow("open");
    });
    
    if ($("#ddlIsInter").text() == 'Yes') {
        $("#passportDiv").removeClass("hidden");
        $("#divSSNNumber").addClass("col-sm-4 hidden");
    }
    else {
        $("#passportDiv").addClass("hidden");
        $("#divSSNNumber").removeClass("col-sm-4 hidden");
        $("#divSSNNumber").addClass("col-sm-4");
    }
    $("#ddlIsInter").on("change", function () {
        if ($(this).val() == 1) {
            $("#passportDiv").removeClass("hidden");
            $("#divSSNNumber").addClass("col-sm-4 hidden");
        }
        else {
            $("#passportDiv").addClass("hidden");
            $("#divSSNNumber").removeClass("col-sm-4 hidden");
            $("#divSSNNumber").addClass("col-sm-4");
        }
    });

    $("#btnAddVehicle").on("click", function (event) {
        clearVehicle();
        $("#popVehicle").PopupWindow("open");
    });
    
    $("#btnAddPet").on("click", function (event) {
        clearPet();
        $("#popPet").PopupWindow("open");
    });
    
    setTimeout(function () {
        getApplicantLists();
        getVehicleLists();
        getPetLists();
    }, 2000);

    fillCountryDropDownList();


    $("#ddladdHistory").on("change", function () {
        if ($(this).val() == 1) {
            $("#residence2").removeClass("hidden");
        }
        else {
            $("#txtAddress12,#txtAddress22,#txtZip2,#txtMoveInDate2,#txtMonthlyPayment2,#txtReasonforleaving2").empty();
            $("#residence2").addClass("hidden");
        }
    });
    $("#ddlDocumentTypePersonal").on("change", function () {
        var newText = $('option:selected', this).text();
        $("#lblUploadIdentity").text(newText);
    });


    $("#btnAddAHR").on("click", function (event) {

        clearApplicantHistory();
        $("#popApplicantHistory").PopupWindow("open");
    });
    

    var QuoteExpires = $("#lblFNLQuoteExpires").text();

    $("#getting-startedTimeRemainingClock").countdown(QuoteExpires, function (event) {
        $(this).text(
            event.strftime('Quote Expires in %D days %H hr : %M min')
        );
    });
    $("#appGenderOther").addClass("hidden");
    $("#ddlApplicantGender").on("change", function () {
        if ($("#ddlApplicantGender").val() == '3') {
            $("#policyStart").attr("disabled", false);
        }
        else {
            $("#appGenderOther").addClass("hidden");
        }
        $("#txtApplicantOtherGender").val("");
    });

    var d = $('#txtAvailableDate').text();
    var date = new Date(d);
    var daysInMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
    var dayOfMonth = date.getDate() - 1;

    var days = daysInMonth - dayOfMonth;
    remainingday = days;
    numberOfDays = daysInMonth;
    getEmpHistoryListPropVari();
    setTimeout(function () {
        fillUnitParkingList();
    }, 1500);
});
var abcd = function () {
    alert("Hi");
}

var cancel = function () {
    window.location.href = "/home";
}
function checkFormstatus() {

    $("#checkForm").toggleClass("hidden");
}
var totalAmt = 0;
var goToStep = function (stepid, id) {
    if (stepid == "1") {
        $("#subMenu").addClass("hidden");
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");
        $("#li17").removeClass("active");
        $("#li18").removeClass("active");
        $("#li19").removeClass("active");
        $("#step1").removeClass("hidden");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#step7").addClass("hidden");
        $("#step8").addClass("hidden");
        $("#step9").addClass("hidden");
        $("#step10").addClass("hidden");
        $("#step11").addClass("hidden");
        $("#step12").addClass("hidden");
        $("#step13").addClass("hidden");
        $("#step14").addClass("hidden");
        $("#step15").addClass("hidden");
        $("#step16").addClass("hidden");
        $("#step17").addClass("hidden");
        $("#step18").addClass("hidden");
    }
    if (stepid == "2") {

        if (id == "2") {
            $("#subMenu").addClass("hidden");
            $("#as2").removeAttr("onclick");
            $("#as2").attr("onclick", "goToStep(2,2)");
            $("#li2").addClass("active");
            $("#li3").removeClass("active");
            $("#li4").removeClass("active");
            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
            $("#li17").removeClass("active");
            $("#li18").removeClass("active");
            $("#li19").removeClass("active");
            $("#step1").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step2").removeClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");
        }

    }
    if (stepid == "3") {
        if (id == "3") {
            $("#subMenu").addClass("hidden");
            $("#as3").removeAttr("onclick")
            $("#as3").attr("onclick", "goToStep(3,3)");
            $("#li3").addClass("active");

            $("#li4").removeClass("active");
            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
            $("#li17").removeClass("active");
            $("#li18").removeClass("active");
            $("#li19").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").removeClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");
        }

    }
    if (stepid == "4") {
        if (id == "4") {
            $("#subMenu").addClass("hidden");
            $("#as18").removeAttr("onclick");
            $("#as18").attr("onclick", "goToStep(4,4)");
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li19").removeClass("active");
            $("#li17").removeClass("active");
            $("#li4").removeClass("active");
            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
            $("#step4").removeClass("hidden");
            $("#step1").addClass("hidden");
            $("#step2").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");
        }
    }
    if (stepid == "5") {
        if (id == "5") {
            $("#subMenu").addClass("hidden");
            $("#as4").removeAttr("onclick");
            $("#as4").attr("onclick", "goToStep(5,5)");
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li17").removeClass("active");
            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
            $("#li19").removeClass("active");
            $("#step5").removeClass("hidden");
            $("#step1").addClass("hidden");
            $("#step2").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#getting-startedTimeRemainingClock").removeClass("hidden");
            $("#step18").addClass("hidden");
        }
    }
    if (stepid == "6") {

        if (id == "6") {
            $("#subMenu").addClass("hidden");
            $("#li3").addClass("active");
            $("#li5").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");

            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
            $("#li19").removeClass("active");
            $("#li17").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").removeClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");
        }
    }
    if (stepid == "7") {
        if (id == "7") {
            $("#subMenu").removeClass("hidden");
            $("#as6").removeAttr("onclick");
            $("#as6").attr("onclick", "goToStep(7,7)");
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").removeClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li7").addClass("active");
            $("#li8, #li9, #li10, #li11, #li12, #li13, #li14, #li15, #li16, #li17, #li19").removeClass("active");
        }
    }
    if (stepid == "8") {
        if (id == "8") {
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").removeClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li8").addClass("active");
            $("#li7, #li9, #li10, #li11, #li12, #li13, #li14, #li15, #li16, #li19").removeClass("active");

        }
    }
    if (stepid == "9") {
        var msg = '';
        if (id == "9") {
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").removeClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li9").addClass("active");
            $("#li7, #li8, #li10, #li11, #li12, #li13, #li14, #li15, #li16, #li19").removeClass("active");
        }
    }
    if (stepid == "10") {
        if (id == "10") {
            getApplicantHistoryList();
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").removeClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li10").addClass("active");
            $("#li7, #li8, #li9, #li11, #li12, #li13, #li14, #li15, #li16, #li19").removeClass("active");
        }
    }
    if (stepid == "11") {
        if (id == "11") {
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").removeClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li11").addClass("active");
            $("#li7, #li8, #li9, #li10, #li12, #li13, #li14, #li15, #li16, #li19").removeClass("active");
        }
    }
    if (stepid == "12") {
        if (id == "12") {
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").removeClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li12").addClass("active");
            $("#li7, #li8, #li9, #li10, #li11, #li13, #li14, #li15, #li16, #li19").removeClass("active");
        }
    }
    if (stepid == "13") {
        if (id == "13") {
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").removeClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li13").addClass("active");
            $("#li7, #li8, #li9, #li10, #li11, #li12, #li14, #li15, #li16, #li19").removeClass("active");
        }
    }
    if (stepid == "14") {
        if (id == "14") {
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li6").addClass("active");
            $("#li17").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").removeClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li14").addClass("active");
            $("#li7, #li8, #li9, #li10, #li11, #li12, #li13, #li15, #li16, #li19").removeClass("active");
        }
    }
    if (stepid == "16") {
        if (id == "16") {
            $("#subMenu").removeClass("hidden");
            $("#as7").removeAttr("onclick")
            $("#as7").attr("onclick", "goToStep(17,17)");
            $("#li7").addClass("active");
            $("#li6").addClass("active");
            $("#li3").addClass("active");
            $("#li18").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li17").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").removeClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").addClass("hidden");

            $("#li16").addClass("active");
            $("#li7, #li8, #li9, #li10, #li11, #li12, #li13, #li14, #li15, #li19").removeClass("active");
        }
    }
    if (stepid == "17") {
        if (id == "17") {
            refreshStatuses();
            //getTransationLists($("#hdnUserId").val());
            //getSignedLists($("#hdnOPId").val());
            $("#subMenu").addClass("hidden");
            $("#li7").addClass("active");
            $("#li17").addClass("active");
            $("#li6").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li18").addClass("active");
            $("#li19").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step18").addClass("hidden");
            $("#step17").removeClass("hidden");
        }
    }
    if (stepid == "18") {
        if (id == "18") {
            $("#subMenu").addClass("hidden");


            $("#li7").addClass("active");
            $("#li6").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");
            $("#li17").addClass("active");
            $("#li18").addClass("active");
            $("#li19").addClass("active");

            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").addClass("hidden");
            $("#step6").addClass("hidden");
            $("#step7").addClass("hidden");
            $("#step8").addClass("hidden");
            $("#step9").addClass("hidden");
            $("#step10").addClass("hidden");
            $("#step11").addClass("hidden");
            $("#step12").addClass("hidden");
            $("#step13").addClass("hidden");
            $("#step14").addClass("hidden");
            $("#step15").addClass("hidden");
            $("#step16").addClass("hidden");
            $("#step17").addClass("hidden");
            $("#step18").removeClass("hidden");
        }
    }
}
//var goToStep = function (stepid, id) {
//    if (stepid == "1") {
//        $("#subMenu").addClass("hidden");
//        $("#li1").addClass("active");
//        $("#li2").removeClass("active");
//        $("#li3").removeClass("active");
//        $("#li4").removeClass("active");
//        $("#li5").removeClass("active");
//        $("#li6").removeClass("active");
//        $("#li7").removeClass("active");
//        $("#li18").removeClass("active");
//        $("#step1").removeClass("hidden");
//        $("#step2").addClass("hidden");
//        $("#step3").addClass("hidden");
//        $("#step4").addClass("hidden");
//        $("#step5").addClass("hidden");
//        $("#step6").addClass("hidden");
//        $("#step7").addClass("hidden");
//        $("#step8").addClass("hidden");
//        $("#step9").addClass("hidden");
//        $("#step10").addClass("hidden");
//        $("#step11").addClass("hidden");
//        $("#step12").addClass("hidden");
//        $("#step13").addClass("hidden");
//        $("#step14").addClass("hidden");
//        $("#step15").addClass("hidden");
//        $("#step16").addClass("hidden");
//        $("#step17").addClass("hidden");
//        $("#step18").addClass("hidden");
//    }
//    if (stepid == "2") {

//        if (id == "2") {
//            $("#subMenu").addClass("hidden");
//            $("#as2").removeAttr("onclick");
//            $("#as2").attr("onclick", "goToStep(2,2)");
//            $("#li2").addClass("active");
//            $("#li3").removeClass("active");
//            $("#li4").removeClass("active");
//            $("#li5").removeClass("active");
//            $("#li6").removeClass("active");
//            $("#li7").removeClass("active");
//            $("#li18").removeClass("active");
//            $("#step1").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step2").removeClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");
//        }

//    }
//    if (stepid == "3") {
//        if (id == "3") {
//            $("#subMenu").addClass("hidden");
//            $("#as3").removeAttr("onclick")
//            $("#as3").attr("onclick", "goToStep(3,3)");
//                $("#li3").addClass("active");

//                $("#li4").removeClass("active");
//                $("#li5").removeClass("active");
//                $("#li6").removeClass("active");
//                $("#li7").removeClass("active");
//                $("#li18").removeClass("active");
//                $("#step2").addClass("hidden");
//                $("#step1").addClass("hidden");
//                $("#step4").addClass("hidden");
//                $("#step3").removeClass("hidden");
//                $("#step5").addClass("hidden");
//                $("#step6").addClass("hidden");
//                $("#step7").addClass("hidden");
//                $("#step8").addClass("hidden");
//                $("#step9").addClass("hidden");
//                $("#step10").addClass("hidden");
//                $("#step11").addClass("hidden");
//                $("#step12").addClass("hidden");
//                $("#step13").addClass("hidden");
//                $("#step14").addClass("hidden");
//                $("#step15").addClass("hidden");
//                $("#step16").addClass("hidden");
//                $("#step17").addClass("hidden");
//                $("#step18").addClass("hidden");
//            }

//        }
//    if (stepid == "4") {
//        if (id == "4") {
//            $("#subMenu").addClass("hidden");
//            $("#as18").removeAttr("onclick");
//            $("#as18").attr("onclick", "goToStep(4,4)");
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");

            
//            $("#li4").removeClass("active");
//            $("#li5").removeClass("active");
//            $("#li6").removeClass("active");
//            $("#li7").removeClass("active");
//            $("#step4").removeClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step2").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");
//        }
//    }
//    if (stepid == "5") {
//        if (id == "5") {
//            $("#subMenu").addClass("hidden");
//            $("#as4").removeAttr("onclick");
//            $("#as4").attr("onclick", "goToStep(5,5)");
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
            
//            $("#li5").removeClass("active");
//            $("#li6").removeClass("active");
//            $("#li7").removeClass("active");

//            $("#step5").removeClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step2").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#getting-startedTimeRemainingClock").removeClass("hidden");
//            $("#step18").addClass("hidden");
//        }
//    }
//    if (stepid == "6") {

//        if (id == "6") {
//            $("#subMenu").addClass("hidden");
//            $("#li3").addClass("active");
//            $("#li5").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");

//            $("#li6").removeClass("active");
//            $("#li7").removeClass("active");
            

//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").removeClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");
//        }
//    }
//    if (stepid == "7") {
//        if (id == "7") {
//            $("#subMenu").removeClass("hidden");
//            $("#as6").removeAttr("onclick");
//            $("#as6").attr("onclick", "goToStep(7,7)");
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li6").addClass("active");
           
//            $("#li7").removeClass("active");
//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").removeClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li7").addClass("active");
//            $("#li8, #li9, #li10, #li11, #li12, #li13, #li14, #li15, #li16, #li17").removeClass("active");
//        }
//    }
//    if (stepid == "8") {
//        if (id == "8") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li6").addClass("active");
            
//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").removeClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li8").addClass("active");
//            $("#li7, #li9, #li10, #li11, #li12, #li13, #li14, #li15, #li16").removeClass("active");

//        }
//    }
//    if (stepid == "9") {
//        var msg = '';
//        if (id == "9") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li6").addClass("active");
            
//            $("#li7").removeClass("active");
//                $("#step2").addClass("hidden");
//                $("#step1").addClass("hidden");
//                $("#step4").addClass("hidden");
//                $("#step3").addClass("hidden");
//                $("#step5").addClass("hidden");
//                $("#step6").addClass("hidden");
//                $("#step7").addClass("hidden");
//                $("#step8").addClass("hidden");
//                $("#step9").removeClass("hidden");
//                $("#step10").addClass("hidden");
//                $("#step11").addClass("hidden");
//                $("#step12").addClass("hidden");
//                $("#step13").addClass("hidden");
//                $("#step14").addClass("hidden");
//                $("#step15").addClass("hidden");
//                $("#step16").addClass("hidden");
//                $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li9").addClass("active");
//            $("#li7, #li8, #li10, #li11, #li12, #li13, #li14, #li15, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "10") {
//        if (id == "10") {
//            getApplicantHistoryList();
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//                $("#li6").addClass("active");
                
//                $("#li7").removeClass("active");
//                $("#step2").addClass("hidden");
//                $("#step1").addClass("hidden");
//                $("#step4").addClass("hidden");
//                $("#step3").addClass("hidden");
//                $("#step5").addClass("hidden");
//                $("#step6").addClass("hidden");
//                $("#step7").addClass("hidden");
//                $("#step8").addClass("hidden");
//                $("#step9").addClass("hidden");
//                $("#step10").removeClass("hidden");
//                $("#step11").addClass("hidden");
//                $("#step12").addClass("hidden");
//                $("#step13").addClass("hidden");
//                $("#step14").addClass("hidden");
//                $("#step15").addClass("hidden");
//                $("#step16").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li10").addClass("active");
//            $("#li7, #li8, #li9, #li11, #li12, #li13, #li14, #li15, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "11") {
//        if (id == "11") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//                $("#li6").addClass("active");
                
//                $("#li7").removeClass("active");
//                $("#step2").addClass("hidden");
//                $("#step1").addClass("hidden");
//                $("#step4").addClass("hidden");
//                $("#step3").addClass("hidden");
//                $("#step5").addClass("hidden");
//                $("#step6").addClass("hidden");
//                $("#step7").addClass("hidden");
//                $("#step8").addClass("hidden");
//                $("#step9").addClass("hidden");
//                $("#step10").addClass("hidden");
//                $("#step11").removeClass("hidden");
//                $("#step12").addClass("hidden");
//                $("#step13").addClass("hidden");
//                $("#step14").addClass("hidden");
//                $("#step15").addClass("hidden");
//                $("#step16").addClass("hidden");
//                $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li11").addClass("active");
//            $("#li7, #li8, #li9, #li10, #li12, #li13, #li14, #li15, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "12") {
//        if (id == "12") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//                $("#li6").addClass("active");
                
//                $("#li7").removeClass("active");
//                $("#step2").addClass("hidden");
//                $("#step1").addClass("hidden");
//                $("#step4").addClass("hidden");
//                $("#step3").addClass("hidden");
//                $("#step5").addClass("hidden");
//                $("#step6").addClass("hidden");
//                $("#step7").addClass("hidden");
//                $("#step8").addClass("hidden");
//                $("#step9").addClass("hidden");
//                $("#step10").addClass("hidden");
//                $("#step11").addClass("hidden");
//                $("#step12").removeClass("hidden");
//                $("#step13").addClass("hidden");
//                $("#step14").addClass("hidden");
//                $("#step15").addClass("hidden");
//                $("#step16").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li12").addClass("active");
//            $("#li7, #li8, #li9, #li10, #li11, #li13, #li14, #li15, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "13") {
//        if (id == "13") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//                $("#li6").addClass("active");
                
//                $("#li7").removeClass("active");
//                $("#step2").addClass("hidden");
//                $("#step1").addClass("hidden");
//                $("#step4").addClass("hidden");
//                $("#step3").addClass("hidden");
//                $("#step5").addClass("hidden");
//                $("#step6").addClass("hidden");
//                $("#step7").addClass("hidden");
//                $("#step8").addClass("hidden");
//                $("#step9").addClass("hidden");
//                $("#step10").addClass("hidden");
//                $("#step11").addClass("hidden");
//                $("#step12").addClass("hidden");
//                $("#step13").removeClass("hidden");
//                $("#step14").addClass("hidden");
//                $("#step15").addClass("hidden");
//                $("#step16").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li13").addClass("active");
//            $("#li7, #li8, #li9, #li10, #li11, #li12, #li14, #li15, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "14") {
//        if (id == "14") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li6").addClass("active");
            
//            $("#li7").removeClass("active");
//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").removeClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li14").addClass("active");
//            $("#li7, #li8, #li9, #li10, #li11, #li12, #li13, #li15, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "15") {
//        if (id == "15") {
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li6").addClass("active");
            
//            $("#li7").removeClass("active");
//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").removeClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li15").addClass("active");
//            $("#li7, #li8, #li9, #li10, #li11, #li12, #li13, #li14, #li16").removeClass("active");
//        }
//    }
//    if (stepid == "16") {
//        if (id == "16") {
//            $("#as7").removeAttr("onclick")
//            $("#as7").attr("onclick", "goToStep(16,16)");
//            $("#li7").addClass("active");
//            $("#li6").addClass("active");
//            $("#li3").addClass("active");
//            $("#li18").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
            
//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").removeClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").addClass("hidden");

//            $("#li16").addClass("active");
//            $("#li7, #li8, #li9, #li10, #li11, #li12, #li13, #li14, #li15").removeClass("active");
//        }
//    }
//    if (stepid == "17") {
//        if (id == "17") {
//            refreshStatuses();
//            //getTransationLists($("#hdnUserId").val());
//            //getSignedLists($("#hdnOPId").val());
//            $("#subMenu").addClass("hidden");
//            $("#li7").addClass("active");
//            $("#li17").addClass("active");
//            $("#li6").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li18").addClass("active");
//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step18").addClass("hidden");
//            $("#step17").removeClass("hidden");
//        }
//    }
//    if (stepid == "18") {
//        if (id == "18") {
//            $("#subMenu").addClass("hidden");
           
           
//            $("#li7").addClass("active");
//            $("#li6").addClass("active");
//            $("#li4").addClass("active");
//            $("#li5").addClass("active");
//            $("#li18").addClass("active");
//            $("#li17").addClass("active");

//            $("#step2").addClass("hidden");
//            $("#step1").addClass("hidden");
//            $("#step4").addClass("hidden");
//            $("#step3").addClass("hidden");
//            $("#step5").addClass("hidden");
//            $("#step6").addClass("hidden");
//            $("#step7").addClass("hidden");
//            $("#step8").addClass("hidden");
//            $("#step9").addClass("hidden");
//            $("#step10").addClass("hidden");
//            $("#step11").addClass("hidden");
//            $("#step12").addClass("hidden");
//            $("#step13").addClass("hidden");
//            $("#step14").addClass("hidden");
//            $("#step15").addClass("hidden");
//            $("#step16").addClass("hidden");
//            $("#step17").addClass("hidden");
//            $("#step18").removeClass("hidden");
//        }
//    }
//}
var SaveOnlineProspect = function () {
    $("#divLoader").show();
    var msg = "";
    var propertyId = $("#hndUID").val();
    var onlineProspectId = $("#hdnOPId").val();
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();
    var phoneNumber = unformatText($("#txtPhoneNumber").val());
    var emailId = $("#txtEmail").val();
    var password = $("#txtPassword").val();
    var address = $("#txtAddress").val();
    var dob = $("#txtDOB").val();
    var annualIncome = $("#txtAnnualIncome").val();
    var addiAnnualIncome = $("#txtAddiAnnualIncome").val();
    var marketsource = $("#ddlMarketSource").val();
    var moveInDate = $("#txtAvailableDate").val();
    var isAgree = $("#chkAgreeTerms").is(":checked") ? "1" : "0";

    //if (isAgree == 0) {
    //    msg += "Please agree with Sanctuary's terms and conditions</br>";
    //}
    if (!firstName) {
        msg += "Please fill the First Name </br>";
    }
    if (!lastName) {
        msg += "Please fill the  Last Name </br>";
    }
    if (!phoneNumber) {
        msg += "Please fill Mobile Number </br>";
    }
    else {
        if (!validatePhone(unformatText($("#txtPhoneNumber").val()))) {
            msg += "Please fill Valid Mobile Number </br>"
        }
    }
    if (!emailId) {
        msg += "Please fill the Email </br>";
    } else {
        if (!validateEmail($("#txtEmail").val())) {
            msg += "Please fill Valid Email </br>"
        }
    }

    if (!password) {
        msg += "Please fill the Password </br>";
    } else {
        if (password.length < 8) {
            msg += "Password should have atleast 8 digits long</br>";
        }
    }

    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return;
    }

    var model = {
        ID: onlineProspectId,
        PropertyId: propertyId,
        FirstName: firstName,
        LastName: lastName,
        Email: emailId,
        Phone: phoneNumber,
        Address: address,
        Password: password,
        DateofBirth: dob,
        AnnualIncome: annualIncome,
        AddiAnnualIncome: addiAnnualIncome,
        Marketsource: marketsource,
        MoveInDate: moveInDate,
    }

    $.ajax({
        url: '/ApplyNow/SaveOnlineProspect',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $("#divLoader").hide();
            var idmsg = response.msg.split('|');
            $("#hdnOPId").val(idmsg[0]);
            $("#lblQuoteID").text("#" + idmsg[0]);
            getApplyNowList(idmsg[0]);
            getTenantOnlineList(idmsg[0]);
            getApplicantLists(idmsg[0]);


            window.location = "/ApplyNow/Index/" + idmsg[2];

            goToStep(4, 4);

            // goToStep(3, 3);
        }
    });
}

var SaveQuote = function () {
    $("#divLoader").show();
    var msg = "";
    var ProspectId = $("#hdnOPId").val();
    var ParkingAmt = $("#lblAdditionalParking").text();
    var StorageAmt = $("#lblStorageUnit").text();
    var PetPlaceAmt = $("#lblPetFee").text();
    var PestAmt = $("#lblPestAmt").text();
    var ConvergentAmt = $("#lblConvergentAmt").text();
    var TrashAmt = $("#lblTrashAmt").text();
    var moveInDate = $("#txtAvailableDate").val();
    var totalAmt = unformatText($("#lbltotalAmount").text());
    var petDeposit = $("#lblPetDeposit").text();
    var fobAmt = $("#lblFobFee").text();

    var model = {
        ID: ProspectId,
        ParkingAmt: ParkingAmt,
        StorageAmt: StorageAmt,
        PetPlaceAmt: PetPlaceAmt,
        PestAmt: PestAmt,
        ConvergentAmt: ConvergentAmt,
        TrashAmt: TrashAmt,
        TotalAmt: totalAmt,
        MoveInDate: moveInDate,
        PetDeposit: petDeposit,
        FOBAmt: fobAmt
    }

    $.ajax({
        url: '/ApplyNow/UpdateOnlineProspect',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $("#divLoader").hide();
            var idmsg = response.msg.split('|');
            $("#lblFNLQuote").text(idmsg[0]);
            //goToStep(5, 5)
        }
    });
}
var SaveCheckPolicy = function () {
    $("#divLoader").show();
    var msg = "";
    var ProspectId = $("#hdnOPId").val();
    var isRentalPolicy = $("#chkRentalPolicy").is(":checked") ? "1" : "0";
    var isRentalQualification = $("#chkRentalQual").is(":checked") ? "1" : "0";
    if (isRentalPolicy == 0) {
        msg += "Please agree Rental Policy";
    }
    if (isRentalQualification == 0) {
        msg += "Please agree Rental Qualification";
    }

    var model = {
        ID: ProspectId,
        IsRentalPolicy: isRentalPolicy,
        IsRentalQualification: isRentalQualification,

    }

    $.ajax({
        url: '/ApplyNow/SaveCheckPolicy',
        type: 'post',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            $("#divLoader").hide();
        }
    });
}

var getApplyNowList = function (id) {
    var model = {
        id: id
    }
    $.ajax({
        url: '/ApplyNow/GetApplyNowList',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#txtBVFirstName").val(response.model.FirstName);
            $("#txtBVLastName").val(response.model.LastName);
            $("#txtBVPhone").val(response.model.Phone);
            $("#txtBVEmail").val(response.model.Email);
            $("#txtBVAddress").val(response.model.Address);
        }
    });
}
var monthsAndYear = function () {
    $("#ddlcardmonth").empty();
    $("#ddlcardmonth").append("<option value='1'>January</option>");
    $("#ddlcardmonth").append("<option value='2'>February</option>");

    var currentYear = (new Date()).getFullYear();
    $("#ddlcardyear").empty();
    $("#ddlcardyear").append("<option value='" + currentYear + "'>" + currentYear + "</option>");

    for (var i = 1; i <= 4; i++) {
        $("#ddlcardyear").append("<option value='" + (parseInt(currentYear) + i) + "'>" + (parseInt(currentYear) + i) + "</option>");
    }
}
//17082019-code changed
var fillStateDDL_Home = function (countryid) {
    var param = { CID: countryid };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                //$("#ddlStateHome").empty();
                //$("#ddlStateHome2").empty();
                //$("#ddlStateHome").append("<option value='0'>--Select State--</option>");
                //$("#ddlStateHome2").append("<option value='0'>--Select State--</option>");
                //$.each(response, function (index, elementValue) {
                //    $("#ddlStateHome").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                //    $("#ddlStateHome2").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                //});
            }
        }
    });
}
var fillStateDDL_Office = function (countryid) {
    var param = { CID: countryid };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlStateEmployee").empty();
                $("#ddlStateEmployee").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStateEmployee").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillStateDDL_EmeContact = function (countryid) {
    var param = { CID: countryid };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlStateContact").empty();
                $("#ddlStateContact").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStateContact").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillStateDDL = function () {
    var param = { CID: 1 };
    $.ajax({
        url: '/City/FillStateDropDownListByCountryID',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                //$("#ddlState").empty();
                //$("#ddlStatePersonal").empty();
                //$("#ddlVState").empty();
                //$("#ddlState").append("<option value='0'>--Select State--</option>");
                //$("#ddlStatePersonal").append("<option value='0'>--Select State--</option>");
                //$("#ddlVState").append("<option value='0'>--Select State--</option>");
                //$.each(response, function (index, elementValue) {
                //    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                //    $("#ddlStatePersonal").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                //    $("#ddlVState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                //});
            }
        }
    });
}
var fillCountryDropDownList = function () {
    $.ajax({
        url: '/City/FillCountryDropDownList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                //$("#txtCountry").empty();
                //$("#txtCountry2").empty();
                //$("#txtCountryOffice").empty();
                //$("#txtEmergencyCountry").empty();
                //$.each(response, function (index, elementValue) {
                //    $("#txtCountry").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                //    $("#txtCountry2").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                //    $("#txtCountryOffice").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                //    $("#txtEmergencyCountry").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                //});
                //$("#txtCountry").val(1);
                //$("#txtCountry2").val(1);
                //$("#txtCountryOffice").val(1);
                //$("#txtEmergencyCountry").val(1);
                //fillStateDDL_Home(1);
                //fillStateDDL_Office(1);
                //fillStateDDL_EmeContact(1);
            }
        }
    });
}
//17082019-fillStateDDL code changed
var fillCityList = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCity").empty();
                $("#ddlCity").append("<option value='0'>--Select City--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var fillCityListHome = function (stateid) {

    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCityHome").empty();
                $("#ddlCityHome").append("<option value='0'>--Select City--</option>");
                $.each(response, function (index, elementValue) {

                    $("#ddlCityHome").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");

                });
            }
        }
    });
}
var fillCityListEmployee = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCityEmployee").empty();
                $("#ddlCityEmployee").append("<option value='0'>--Select City--</option>");
                $.each(response, function (index, elementValue) {

                    $("#ddlCityEmployee").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");

                });
            }
        }
    });
}
var fillCityListContact = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlCityContact").empty();
                $("#ddlCityContact").append("<option value='0'>--Select City--</option>");
                $.each(response, function (index, elementValue) {

                    $("#ddlCityContact").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");


                });
            }
        }
    });
}
var noofcomapre = 0;


var getCompareModelList = function () {
    $("#divLoader").show();
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        //maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }

    var bedroom = 0;
    if (!$("#ddlRoom").val()) {
        bedroom = 0;
    }
    else {
        bedroom = $("#ddlRoom").val();
    }
    var availdate = $("#txtDate").val();


    var model = { PID: 8, AvailableDate: availdate, Current_Rent: maxrent, Bedroom: bedroom };
    $.ajax({
        url: "/Property/GetPropertyModelList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
                $("#divLoader").hide();
                $("#listModelCompare").empty();
                var chtml = "<div class='col-sm-3'><div class='col-sm-12'><span><br><br><br><br><br></span></div><div class='col-sm-12'><span><br /> </span></div><div class='col-sm-12'><span> </span></div> <div class='col-sm-12'><span>Monthly Rent: </span></div><div class='col-sm-12'><span>Square feet: </span></div><div class='col-sm-12'><span id=''>Bedrooms: </span></div><div class='col-sm-12'><span id=''>Bathrooms: </span></div><div class='col-sm-12'><span>Available: </span></div> <div class='col-sm-12'><span id=''>Occupancy: </span></div></div>";
                $.each(response.model, function (elementType, value) {
                    for (var j = 0; j < addModelArray.length; j++) {
                        if (addModelArray[j] == value.Building) {
                            chtml += " <div class='col-sm-3'><div class='col-sm-12 center'><span><a href = '#' onclick='displayCompImg(\"" + value.Building + "\")'> <img src='/content/assets/img/plan/" + value.FloorPlan + "'></a> </span></div><div class='col-sm-12 center'><span><b>" + value.Building + "</b> </span></div><div class='col-sm-12 center'><span><button type='button' class='btn-default btn' onclick=getPropertyUnitList(\"" + value.Building + "\") style='width:100%;padding:5px;'>Select</button> <br /> </span></div> <div class='col-sm-12 center'><span>" + value.RentRange + " </span></div><div class='col-sm-12 center'><span>" + value.Area + "</span></div><div class='col-sm-12 center'><span id=''>" + value.Bedroom + " </span></div><div class='col-sm-12 center'><span id=''>" + value.Bathroom + " </span></div><div class='col-sm-12 center'><span>" + value.NoAvailable + " </span></div> <div class='col-sm-12 center'><span id=''>" + value.Bedroom + "</span></div></div>";

                        }
                    }
                });
                $("#listModelCompare").append(chtml);
            }
        }
    });
}
var getPropertyUnitDetails = function (uid) {

    var model = { UID: uid, LeaseTermID: $("#hndLeaseTermID").val()};
    $.ajax({
        url: "/Property/GetPropertyUnitDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ModelCompare").modal("hide");

            $("#popUnitDet").addClass("hidden");
            $("#popFloorCoordinate").addClass("hidden");
            $("#lblUnitNo").text("#" + response.model.UnitNo);
            $("#lblUnitTitle").text("#" + response.model.UnitNo);
            $("#lblUnitTitle2").text("#" + response.model.UnitNo);
            $("#lblFNLPreparedFor").text("#" + response.model.UnitNo);
            $("#txtAvailableDate").val(response.model.AvailableDateText);

            $("#unitdiv" + $("#hndUID").val()).removeClass("select-unit");
            $("#unitdiv" + uid).addClass("select-unit");

            $("#lblRent").text(formatMoney(response.model.Current_Rent));
            $("#lblArea").text(response.model.Area);
            $("#lblBed").text(response.model.Bedroom);
            $("#lblBath").text(response.model.Bathroom);
            $("#lblHall").text(response.model.Hall);
            $("#lblDeposit").text("$" + response.model.Deposit);
            //$("#lblLease").text(response.model.LeaseTerm);

            $("#lblRent22").text("$" + response.model.Current_Rent);
            $("#lblArea22").text(response.model.Area);
            $("#lblBed22").text(response.model.Bedroom);
            $("#lblBath22").text(response.model.Bathroom);

            $("#lblOccupancy22").text((parseInt(response.model.Bedroom) * 2).toString());
            $("#lblOccupancy").text((parseInt(response.model.Bedroom) * 2).toString());

            $("#lblDeposit22").text("$" + response.model.Deposit);
            //$("#lblLease22").text(response.model.LeaseTerm);

            //$("#lblLease2").text(response.model.LeaseTerm);

            // $("#lblDeposit").text(response.model.Rent);
            $("#imgFloorPlan").attr("src", "/content/assets/img/plan/" + response.model.Building + ".jpg");
            $("#imgFloorPlan1").attr("src", "/content/assets/img/plan/" + response.model.Building + ".jpg");
            $("#imgFloorPlan2").attr("src", "/content/assets/img/plan/" + response.model.Building + "Det.jpg");
            $("#lblUnitNo1").text("Selected : Floor" + response.model.FloorNo + "- Unit " + response.model.UnitNo + "  ($" + response.model.Current_Rent + ")");
            $("#hndUID").val(uid);

            $("#txtRooms").val(response.model.Rooms);
            $("#txtBedroom").val(response.model.Bedroom);
            $("#txtBathroom").val(response.model.Bathroom);
            $("#txtHall").val(response.model.Hall);

            $("#txtPetDetails").val(response.model.PetDetails);
            $("#ddlFloorNo").val(response.model.FloorNo);
            $("#txtArea").val(response.model.Area);

            $("#txtCarpet_Color").val(response.model.Carpet_Color);
            $("#txtWall_Paint_Color").val(response.model.Wall_Paint_Color);

            $("#txtOccupancyDate").val(response.model.OccupancyDateText);
            //$("#chkPendingMoveIn").is(":checked") ? "1" : "0";
            $("#txtVacancyLoss_Date").val(response.model.VacancyLoss_DateText);
            $("#txtIntendedMoveIn_Date").val(response.model.IntendedMoveIn_Date);
            $("#txtIntendMoveOutDate").val(response.model.IntendMoveOutDate);
            $("#txtActualMoveInDate").val(response.model.ActualMoveInDateText);

            $("#lbldeposit1").text(parseFloat(response.model.Deposit).toFixed(2));
            $("#lbdepo6").text(parseFloat(response.model.Deposit).toFixed(2));

            $("#lblFMRent").text(formatMoney(parseFloat(response.model.Current_Rent).toFixed(2)));

            if (response.model.Furnished == 0) {
                $("#chkFurnished").css("text-decoration", "line-through");
                $("#chkFurnished").css("text-decoration-color", "red");
            }
            if (response.model.Washer == 0) {
                $("#chkWasher").css("text-decoration", "line-through");
                $("#chkWasher").css("text-decoration-color", "red");
            }
            if (response.model.Refrigerator == 0) {
                $("#chkRefrigerator").css("text-decoration", "line-through");
                $("#chkRefrigerator").css("text-decoration-color", "red");
            }
            if (response.model.Drapes == 0) {
                $("#chkDrapes").css("text-decoration", "line-through");
                $("#chkDrapes").css("text-decoration-color", "red");
            }
            if (response.model.Dryer == 0) {
                $("#chkDryer").css("text-decoration", "line-through");
                $("#chkDryer").css("text-decoration-color", "red");
            }
            if (response.model.Dishwasher == 0) {
                $("#chkDishwasher").css("text-decoration", "line-through");
                $("#chkDishwasher").css("text-decoration-color", "red");
            }
            if (response.model.Disposal == 0) {
                $("#chkDisposal").css("text-decoration", "line-through");
                $("#chkDisposal").css("text-decoration-color", "red");
            }
            if (response.model.Elec_Range == 0) {
                $("#chkElec_Range").css("text-decoration", "line-through");
                $("#chkElec_Range").css("text-decoration-color", "red");
            }
            if (response.model.Gas_Range == 0) {
                $("#chkGas_Range").css("text-decoration", "line-through");
                $("#chkGas_Range").css("text-decoration-color", "red");
            }
            if (response.model.Air_Conditioning == 0) {
                $("#chkAir_Conditioning").css("text-decoration", "line-through");
                $("#chkAir_Conditioning").css("text-decoration-color", "red");
            }
            if (response.model.Fireplace == 0) {
                $("#chkFireplace").css("text-decoration", "line-through");
                $("#chkFireplace").css("text-decoration-color", "red");
            }
            if (response.model.Den == 0) {
                $("#chkDen").css("text-decoration", "line-through");
                $("#chkDen").css("text-decoration-color", "red");
            }
            if (response.model.Carpet == 0) {
                $("#chkCarpet").css("text-decoration", "line-through");
                $("#chkCarpet").css("text-decoration-color", "red");
            }

            totalAmt = (parseFloat(response.model.Current_Rent) + parseFloat($("#lblAdditionalParking").text()) + parseFloat($("#lblStorageUnit").text()) + parseFloat($("#lblTrashAmt").text()) + parseFloat($("#lblPestAmt").text()) + parseFloat($("#lblConvergentAmt").text()) + parseFloat($("#lblPetFee").text())).toFixed(2);
            $("#lbltotalAmount").text(formatMoney(totalAmt));


            //Amit's Work for final Quotation form 15-10
            $("#lblFNLUnit").text("#" + response.model.UnitNo);
            $("#lblFNLModel").text(response.model.Building);

            //$("#lblFNLTerm").text(response.model.LeaseTerm);

            $("#lblMonthly_MonthlyCharge").text(formatMoney(parseFloat(response.model.Current_Rent).toFixed(2)));

            $("#lblProrated_MonthlyCharge").text(formatMoney(parseFloat(parseFloat(response.model.Current_Rent) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblRFPMonthlyCharges").text(formatMoney(response.model.Current_Rent.toFixed(2)));

            $("#fdepo").text(response.model.Deposit);
            $("#lbdepo6").text((response.model.Deposit).toFixed(2));

            $("#lblMonthly_TotalRent").text(formatMoney(totalAmt));
            $("#lblProrated_TotalRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent6").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));

            var rfpMonthlyCharge = unformatText($("#lblRFPMonthlyCharges").text());
            var rfpParkingCharge = $("#lblRFPAdditionalParking").text();
            var rfpStorageCharge = $("#lblRFPStorageUnit").text();
            var rfpPetcharge = $("#lblRFPPetRent").text();
            var rfpTrashCharge = $("#lblRFPTrashRecycling").text();
            var rfpPestControl = $("#lblRFPPestControl").text();
            var rfpConvergentBillFee = $("#lblRFPConvergentbillingfee").text();

            var rfpTotalRentCharge = parseFloat(rfpMonthlyCharge, 10) + parseFloat(rfpParkingCharge, 10) + parseFloat(rfpStorageCharge, 10) + parseFloat(rfpPetcharge, 10) + parseFloat(rfpTrashCharge, 10) + parseFloat(rfpPestControl, 10) + parseFloat(rfpConvergentBillFee, 10);
            //alert(calTotalRentChargefpetd
            $("#lblRFPTotalMonthlyPayment").text(formatMoney(rfpTotalRentCharge.toFixed(2)));
            //$("#ftotal").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10)).toFixed(2)));
            $("#lbtotdueatmov6").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) ).toFixed(2)));


            $("#lblProrated_TrashAmt").text(parseFloat(parseFloat($("#lblTrash").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));
            $("#lblProrated_PestAmt").text(parseFloat(parseFloat($("#lblPestControl").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));
            $("#lblProrated_ConvergentAmt").text(parseFloat(parseFloat($("#lblConvergentAmt").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));



            $("#lblRent2").text(response.model.Current_Rent);
            $("#txtModal").text(response.model.Building);
            $("#lblArea1").text(response.model.Area);
            $("#lblBed1").text(response.model.Bedroom);
            $("#lblBath1").text(response.model.Bathroom);
            $("#lblHall1").text(response.model.Hall);
            $("#lblDeposit1").text("$" + response.model.Deposit);

            //$("#lblLease3").text(response.model.LeaseTerm);
            //$("#lblLease4").text(response.model.LeaseTerm);

            $("#imgFloorPlanSumm").attr("src", "/content/assets/img/plan/" + response.model.Building + ".jpg");
            $("#lbldeposit2").text(parseFloat(response.model.Deposit).toFixed(2));
            $("#lblFMRent1").text(parseFloat(response.model.Current_Rent).toFixed(2));
            $("#lblUnitTitle3").text("#" + response.model.UnitNo);
            $("#lblLeaseStartDate").text(response.model.AvailableDateText);

            $("#lblSubtotalsumm").text((parseFloat(response.model.Current_Rent) + parseFloat(26.50)).toFixed(2));
            $("#lbltotalAmountSumm").text((parseFloat(response.model.Current_Rent) + parseFloat(26.50)).toFixed(2));
          

        }
    })
}
function displayImg() {
    $("#popFloorPlan").modal("show");
}
function displayCompImg(modname) {
    $("#imgFloorPlan2").attr("src", "/content/assets/img/plan/" + modname + "Det.jpg");
    $("#popFloorPlan").modal("show");
}
var SaveUpdateDocumentVerification = function () {
    $("#divLoader").show();
    var msg = "";
    var documentVerificationID = $("#hdnDocumentVerificationId").val();
    var prospectID = $("#hdnOPId").val();
    var documentType = $("#ddlDocumentType").val();
    var documentName = $("#txtDocumentName").val();
    var documentState = $("#ddlState").val();
    var documentIdNumber = $("#txtBVIDNumber").val();


    if (documentType == "0") {
        msg += "Please Select The Document Type </br>";
    }
    if (!documentIdNumber) {
        msg += "Please enter ID Number </br>";
    }
    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return;
    }

    $formData = new FormData();

    $formData.append('DocID', documentVerificationID);
    $formData.append('ProspectusID', prospectID);
    $formData.append('DocumentType', documentType);
    $formData.append('DocumentName', documentName);
    $formData.append('DocumentState', documentState);
    $formData.append('DocumentIDNumber', documentIdNumber);
    var photo = document.getElementById('wizard-picture');
    if (photo.files.length > 0) {
        for (var i = 0; i < photo.files.length; i++) {
            $formData.append('file-' + i, photo.files[i]);
        }

        $.ajax({
            url: '/ApplyNow/SaveDocumentVerification',
            type: 'post',
            data: $formData,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                $("#divLoader").hide();


            }
        });
    } else {
        $.alert({
            content: "Please Upload Proof Document",
            type: 'blue'
        });
    }
}
var saveLeaseDoc = function () {
    var userid = $("#hdnOPId").val();
    $("#divLoader").show();
    var param = { TenantID: userid };
    $.ajax({
        url: "/ProspectVerification/PDFBuilder",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            window.open("/Content/assets/img/Document/" + response.result, "popupWindow", "width=900,height=600,scrollbars=yes");
            //signLeaseDoc(response.result);
        }
    });
}

var SaveLeaseDocumentVerification = function () {
    $("#divLoader").show();

    var PID = $("#hdnOPId").val();

    $formData = new FormData();

    $formData.append('ProspectusID', PID);
    var photo = document.getElementById('upleasedoc');
    if (photo.files.length > 0) {
        for (var i = 0; i < photo.files.length; i++) {
            $formData.append('file-' + i, photo.files[i]);
        }

        $.ajax({
            url: '/Verification/SaveLeaseDocumentVerification',
            type: 'post',
            data: $formData,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                $("#divLoader").hide();
                $.alert({
                    title: "",
                    content: "Progress Saved.",
                    type: 'blue'
                });
            }
        });
    }
    else {
        $("#divLoader").hide();
        $.alert({
            title: "",
            content: "Please Upload Lease Agreement",
            type: 'blue'
        });
    }
}
var totPaid = 0;
var getTransationLists = function (userid) {
    var model = {

        TenantID: userid,
    }
    $.ajax({
        url: "/Transaction/GetOnlineTransactionList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblTransaction>tbody").empty();
            $("#tblAdminFee>tbody").empty();
            var adminFeesPaid = 0;
            $.each(response.model, function (elementType, elementValue) {
                totPaid = totPaid+parseFloat(elementValue.Credit_Amount);
                var html = "<tr data-value=" + elementValue.TransID + ">";
              
                //html += "<td>" + elementValue.TenantIDString + "</td>";
                html += "<td>" + elementValue.Transaction_DateString + "</td>";
                html += "<td>$" + formatMoney(elementValue.Charge_Amount) + "</td>";
                html += "<td>$" + formatMoney(elementValue.Credit_Amount) + "</td>";
                html += "<td>" + elementValue.Transaction_Type + "</td>";
              
                html += "<td>" + elementValue.Description + "</td>";
                //html += "<td>" + elementValue.CreatedDateString + "</td>";
                html += "</tr>";
                if (elementValue.Transaction_Type == "Administrative Fee") {
                    adminFeesPaid = 1;
                    var adhtml = "<tr data-value=" + elementValue.TransID + ">";
                    adhtml += "<td>" + $("#hndPriAppFullName").val() + "</td>";
                    adhtml += "<td>" + elementValue.Description + "</td>";
                    adhtml += "<td style='text-align: center;'>$" + formatMoney(elementValue.Charge_Amount) + "</td>";
                    adhtml += "<td style='text-align: center;'>" + elementValue.Transaction_DateString + "</td>";
                    adhtml += "<td><button type='button' class='btn btn-primary' id='btnSendRemtr' onclick='SendReminderEmail(1,0)' disabled='disabled'><span>Send Reminder to Pay Administration Fees </span></button></td>";
                    $("#tblAdminFee>tbody").append(adhtml);

                    $("#btnSendRemSign").removeAttr("disabled");
                    
                }
                $("#tblTransaction>tbody").append(html);
            });
            if (adminFeesPaid == 0) {
                var adhtml = "<tr data-value='0'>";
                adhtml += "<td>" + $("#hndPriAppFullName").val() + "</td>";
                adhtml += "<td>Admin Fees</td>";
                adhtml += "<td style='text-align: center;'>$" + formatMoney($("#lblAdminFees").text()) + "</td>";
                adhtml += "<td style='text-align: center;'></td>";
                adhtml += "<td><button type='button' class='btn btn-primary' id='btnSendRemtr' onclick='SendReminderEmail(1,0)'><span>Send Reminder to Pay Administration Fees </span></button></td>";
                $("#tblAdminFee>tbody").append(adhtml);
            }
            if (totPaid >= parseFloat($("#txtPayment").val()) + parseFloat(totalFinalFees))
            {
                $("#btnC2Tenant").removeAttr("disabled");
            }
        }
    });
}
var clearBank = function () {
    $("#hndTransMethod").val(1);
    $("#chkBank").addClass("active");
    $("#chkACH").removeClass("active");
    $("#divCard").addClass("hidden");
    $("#divBank").removeClass("hidden");

    $("#lblPaymentDet").text("Enter Bank Account Details.");
}
var clearCard = function () {
    $("#hndTransMethod").val(2);
    $("#chkACH").addClass("active");
    $("#chkBank").removeClass("active");
    $("#divCard").removeClass("hidden");
    $("#divBank").addClass("hidden");

    $("#lblPaymentDet").text("Enter Credit Card Details.");
}

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
                addParkingArray = [];
                $("#tblParking>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    if ($("#lblBed").text() == "1" || $("#lblBed").text() == "2") {
                        if (elementValue.ParkingID == "1") {
                            html += '<tr data-value="' + elementValue.ParkingID + '">';
                            html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ParkingID + '</td>';
                            html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ParkingName + '</td>';
                            html += '<td class="pds-firstname" style="color:#3d3939;">$' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';

                            html += '<td class="pds-firstname" style="color:#3d3939;"><input type="checkbox" id="chkAddParking"  class="addame" value=' + elementValue.ParkingID + ' onclick="selectAddParking(this)" ' + ($("#lblparkingplace").text() == elementValue.ParkingID ? "checked='checked'" : "") + ' ></td>';
                            html += '</tr>';
                            if ($("#lblparkingplace").text() == elementValue.ParkingID) {
                                addParkingArray.push({ PArkingID: elementValue.ParkingID });
                            }
                        }

                    }
                    else {
                        html += '<tr data-value="' + elementValue.ParkingID + '">';
                        html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ParkingID + '</td>';
                        html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.ParkingName + '</td>';
                        html += '<td class="pds-firstname" style="color:#3d3939;">$' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';

                        html += '<td class="pds-firstname" style="color:#3d3939;"><input type="checkbox" id="chkAddParking"  class="addame" value=' + elementValue.ParkingID + ' onclick="selectAddParking(this)" ' + ($("#lblparkingplace").text() == elementValue.ParkingID ? "checked='checked'" : "") + ' ></td>';
                        html += '</tr>';
                        if ($("#lblparkingplace").text() == elementValue.ParkingID) {
                            addParkingArray.push({ PArkingID: elementValue.ParkingID });
                        }
                    }


                    $("#tblParking>tbody").append(html);
                });
            }
        }
    });
}
var fillFOBList = function () {

    $.ajax({
        url: '/Storage/GetStorageList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                addStorageArray = [];
                $("#tblStorage>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.StorageID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.StorageID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.StorageName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;"><input type="checkbox" id="chkAddStorage"  class="addstorage" value=' + elementValue.StorageID + ' onclick="selectAddStorage(this)" ' + ($("#lblstorageplace").text() == elementValue.StorageID ? "checked='checked'" : "") + '></td>';
                    html += '</tr>';
                    if ($("#lblstorageplace").text() == elementValue.StorageID) {
                        addStorageArray.push({ StorageID: elementValue.StorageID });
                    }
                    $("#tblStorage>tbody").append(html);
                });
            }
        }
    });
}
var fillStorageList = function () {

    $("#tblStorage1>tbody").empty();
    var html = '';
    html += '<tr data-value="1">';
    html += '<td class="pds-id hidden" style="color:#3d3939;">1</td>';
    html += '<td class="pds-firstname" style="color:#3d3939;">Storage</td>';
    html += '<td class="pds-firstname" style="color:#3d3939;">$50.00</td>';
    html += '<td class="pds-firstname" style="color:#3d3939;"><input type="checkbox" id="chkAddStorage1"  class="addstorage1" value="1"></td>';
    html += '</tr>';

    $("#tblStorage1>tbody").append(html);

    if ($("#lblStorageUnit").text() == "50.00") {

        $("#chkAddStorage1").attr("checked", true);
    }
}
var fillPetPlaceList = function () {
    $.ajax({
        url: '/PetManagement/GetPetPlaceList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                addPetPlaceArray = [];
                $("#tblPetPlace>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.PetPlaceID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.PetPlaceID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.PetPlace + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">$' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;"><input type="checkbox" name="chkpet id="chkAddPetPlace"  class="addpet" value=' + elementValue.PetPlaceID + ' onclick="selectAddPetPlace(this)" ' + ($("#lblpetplace").text() == elementValue.PetPlaceID ? "checked='checked'" : "") + '></td>';
                    html += '</tr>';
                    if ($("#lblpetplace").text() == elementValue.PetPlaceID) {
                        addPetPlaceArray.push({ PetPlaceID: elementValue.PetPlaceID });
                    }

                    $("#tblPetPlace>tbody").append(html);
                });
            }
        }
    });
}
var addParkingArray = [];
function selectAddParking(cont) {
    var ischeck = $(cont).is(':checked')
    $('.addame').removeAttr("checked");
    $(cont).prop("checked", ischeck);
    addParkingArray = [];
    $('.addame').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var pkid = $(obj).attr("value");
            addParkingArray.push({ PArkingID: pkid });
        }
    });

}
var addStorageArray = [];
function selectAddStorage(cont) {
    var ischeck = $(cont).is(':checked')
    $('.addstorage').removeAttr("checked");
    $(cont).prop("checked", ischeck);

    addStorageArray = [];
    $('.addstorage').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var pkid = $(obj).attr("value");
            addStorageArray.push({ StorageID: pkid });
        }
    });

}
var addPetPlaceArray = [];
function selectAddPetPlace(cont) {
    var ischeck = $(cont).is(':checked')
    $('.addpet').removeAttr("checked");
    $(cont).prop("checked", ischeck);
    addPetPlaceArray = [];
    $('.addpet').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var pkid = $(obj).attr("value");
            addPetPlaceArray.push({ PetPlaceID: pkid });
        }
    });
}

var saveupdateParking = function () {
    $("#divLoader").show();
    var tenantID = $("#hdnOPId").val();
    var param = { TenantID: tenantID, lstTParking: addParkingArray };
    $.ajax({
        url: "/Parking/SaveUpdateTenantParking",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'red'
            })
            //$("#popParking").PopupWindow("close");
            $('#popParking').modal('hide');
            $("#divLoader").hide();
            totalAmt = parseFloat(totalAmt) - $("#lblAdditionalParking").text() - $("#lblVehicleFees").text();
            $("#lblAdditionalParking").text(parseFloat(response.totalParkingAmt).toFixed(2));
            $("#lblMonthly_AditionalParking").text(parseFloat(response.totalParkingAmt).toFixed(2));
            $("#lblProrated_AditionalParking").text(parseFloat(parseFloat($("#lblMonthly_AditionalParking").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));
            
            $("#lblparkingplace").text(addParkingArray.length > 0 ? addParkingArray[0].PArkingID : 0);

            if (parseFloat(response.totalParkingAmt).toFixed(2) == "75.00") {
                $("#lblVehicleFees").text("15.00");
                $("#lblVehicleFees1").text("15.00");
                $("#hndStorageID").val(1);

            } else if (parseFloat(response.totalParkingAmt).toFixed(2) == "150.00") {
                $("#lblVehicleFees").text("30.00");
                $("#lblVehicleFees1").text("30.00");
                $("#hndStorageID").val(2);
            }
            else {
                $("#hndStorageID").val(0);

            }
            $("#lbltotalAmount").text(formatMoney((parseFloat(response.totalParkingAmt) + parseFloat(totalAmt)).toFixed(2)))
            totalAmt = (parseFloat(response.totalParkingAmt) + parseFloat(totalAmt)).toFixed(2);
            $("#lblMonthly_TotalRent").text(formatMoney(totalAmt));
            $("#lblProrated_TotalRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#ftotal").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10)).toFixed(2)));



        }
    });
}
var saveupdateFOB = function () {
    $("#divLoader").show();
    var tenantID = $("#hdnOPId").val();
    var param = { TenantID: tenantID, lstTStorage: addStorageArray };
    $.ajax({
        url: "/Storage/SaveUpdateTenantStorage",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue'
            })
            $("#popFobs").modal("hide");
            $("#divLoader").hide();
            //totalAmt = parseFloat(totalAmt) - $("#lblFobFee").text();
            $("#lblFobFee").text(parseFloat(response.totalStorageAmt).toFixed(2));
            $("#ffob").text(parseFloat(response.totalStorageAmt).toFixed(2));
            $("#keyfobsamt").text(parseFloat(response.totalStorageAmt).toFixed(2));

            //$("#lbltotalAmount").text((parseFloat(response.totalStorageAmt) + parseFloat(totalAmt)).toFixed(2))
            // totalAmt = (parseFloat(response.totalStorageAmt) + parseFloat(totalAmt)).toFixed(2);

            $("#lblstorageplace").text(addStorageArray.length > 0 ? addStorageArray[0].StorageID : 0);
        }
    });
}
var saveupdatePetPlace = function () {
    $("#divLoader").show();
    var isAgree = $("#chkAgreePetTerms").is(":checked") ? "1" : "0";
    if (isAgree == "0") {
        if (addPetPlaceArray.length > 0) {
            $.alert({
                title: "",
                content: "Please agree to the pet policy",
                type: 'red'
            })
            return;
        }
    }
    var tenantID = $("#hdnOPId").val();
    var param = { TenantID: tenantID, lstTPetPlace: addPetPlaceArray };
    $.ajax({
        url: "/PetManagement/SaveUpdateTenantPetPlace",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue'
            });
            //$("#popPetPlace").PopupWindow("close");
            $("#popPetPlace").modal("hide");
            $("#divLoader").hide();
            totalAmt = parseFloat(totalAmt) - parseFloat($("#lblPetFee").text());
            $("#lblPetDeposit").text("0.00");
            $("#fpetd").text("0.00");
            $("#lbpetd6").text("0.00");

            $("#lblPetFee").text(parseFloat(response.totalPetPlaceAmt).toFixed(2));
            $("#lblMonthly_PetRent").text(parseFloat(response.totalPetPlaceAmt).toFixed(2));
            $("#lblProrated_PetRent").text(parseFloat(parseFloat(response.totalPetPlaceAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2));

            $("#lblpetplace").text(addPetPlaceArray.length > 0 ? addPetPlaceArray[0].PetPlaceID : 0);
            if (parseFloat(response.totalPetPlaceAmt).toFixed(2) == "20.00") {
                $("#lblPetDeposit").text("500.00");
                $("#fpetd").text("500.00");
                $("#lbpetd6").text("500.00");
                $("#hndPetPlaceID").val(1);
                $("#btnAddPet").removeAttr("disabled");

            } else if (parseFloat(response.totalPetPlaceAmt).toFixed(2) == "40.00") {
                $("#lblPetDeposit").text("750.00");
                $("#fpetd").text("750.00");
                $("#lbpetd6").text("750.00");
                $("#hndPetPlaceID").val(2);
                $("#btnAddPet").removeAttr("disabled");
            }
            else {
                $("#hndPetPlaceID").val(0);
                $("#btnAddPet").css("background-color", "#B4ADA5").attr("disabled", "disabled");
            }


            // $("#lbltotalAmount").text((parseFloat(response.totalPetPlaceAmt) + parseFloat(totalAmt)).toFixed(2) + parseFloat($("#lblPetDeposit").text()).toFixed(2));
            totalAmt = (parseFloat(response.totalPetPlaceAmt) + parseFloat(totalAmt)).toFixed(2);
            $("#lbltotalAmount").text(formatMoney(totalAmt));
            $("#lblMonthly_TotalRent").text(formatMoney(totalAmt));
            $("#lblProrated_TotalRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent6").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#ftotal").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) ).toFixed(2)));
        }
    });
}
var saveupdateStorage = function () {
    $("#divLoader").show();
    $.alert({
        title: "",
        content: "Progress Saved.",
        type: 'blue'
    })
    $("#popStorage").modal("hide");
    $("#divLoader").hide();
    totalAmt = parseFloat(totalAmt) - $("#lblStorageUnit").text();

    if ($("#chkAddStorage1").is(":checked")) {
        $("#lblStorageUnit").text(parseFloat(50.00).toFixed(2));
        $("#lblMonthly_Storage").text(parseFloat(50.00).toFixed(2));
        $("#lblProrated_Storage").text(parseFloat(parseFloat($("#lblMonthly_Storage").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));
        
        totalAmt = (parseFloat(50.00) + parseFloat(totalAmt)).toFixed(2);

    } else {
        $("#lblStorageUnit").text(parseFloat(0.00).toFixed(2));
        $("#lblMonthly_Storage").text(parseFloat(0.00).toFixed(2));
        $("#lblProrated_Storage").text("0.00");

    }

    $("#lblMonthly_TotalRent").text(formatMoney(parseFloat(totalAmt).toFixed(2)));
    $("#lbltotalAmount").text(formatMoney(parseFloat(totalAmt).toFixed(2)));

    $("#lblProrated_TotalRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
    $("#lblProratedRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
    // $("#ftotal").text((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(30) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat(365, 10)).toFixed(2));
    $("#lblProratedRent6").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
    // $("#lblstorageplace").text(addStorageArray.length > 0 ? addStorageArray[0].StorageID : 0);
    //alert($("#lblProrated_Storage").text());
}

//Sohan
var addApplicant = function (at) {

    if (at == 1) {

        $("#ddlApplicantType").text("Co-Applicant");
        //$("#popApplicant").PopupWindow("setTitle", "Add Applicant");
        var modal = $(popApplicant);
        modal.find('.modal-title').text('Add Applicant');
        $("#appphone").removeClass("hidden");
        $("#appemail").removeClass("hidden");
        $("#apprelationship").addClass("hidden");
        $//("#ddlGRelationship").removeClass("hidden");
        //$("#ddlGRelationship").val(response.model.Relationship).change();
        //$("#ddlARelationship").addClass("hidden");
        $("#ddlARelationship").empty();
        var opt = "<option value='0'>Select Relationship</option>";
        opt += "<option value='1'>Spouse</option>";
        opt += "<option value='2'>Partner</option>";
        opt += "<option value='3'>Adult Child</option>";
        opt += "<option value='4'>Friend/Roommate</option>";

        $("#ddlARelationship").append(opt);
        $("#ddlARelationship").val(0).change();
        var dtApp = new Date();
        dtApp.setFullYear(new Date().getFullYear() - 18);
        $('#txtADateOfBirth').datepicker({ endDate: dtApp, autoclose: true });

        $('#txtADateOfBirth').removeClass("hidden");
        $('#txtHDateOfBirth').addClass("hidden");
        $('#txtMDateOfBirth').addClass("hidden");
        $('#txtGDateOfBirth').addClass("hidden");

        $('#txtApplicantOtherGender').val('');
        $('#appGenderOther').addClass('hidden');
    }
    else if (at == 2) {
        $("#ddlApplicantType").text("Minor");
        //$("#popApplicant").PopupWindow("setTitle", "Add Minor");
        var modal = $(popApplicant);
        modal.find('.modal-title').text('Add Minor');
        $("#appphone").addClass("hidden");
        $("#appemail").addClass("hidden");
        $("#apprelationship").removeClass("hidden");
        $('#txtMDateOfBirth').removeClass("hidden");
        $('#txtADateOfBirth').addClass("hidden");
        $('#txtHDateOfBirth').addClass("hidden");
        $('#txtGDateOfBirth').addClass("hidden");
        $//("#ddlGRelationship").removeClass("hidden");
        //$("#ddlGRelationship").val(response.model.Relationship).change();
        //$("#ddlARelationship").addClass("hidden");
        $("#ddlARelationship").empty();
        var opt = "<option value='0'>Select Relationship</option>";
        opt += "<option value='1'>Family Member</option>";
        opt += "<option value='2'>Child</option>";
        $("#ddlARelationship").append(opt);
        $("#ddlARelationship").val(0).change();
        var dtMin = new Date();
        dtMin.setFullYear(new Date().getFullYear() - 18);
        var dtEnd = new Date();
        $('#txtMDateOfBirth').datepicker({ viewMode: "years", startDate: dtMin, endDate: dtEnd, autoclose: true });

        $('#txtApplicantOtherGender').val('');
        $('#appGenderOther').addClass('hidden');
    }
    else if (at == 3) {
        $("#ddlApplicantType").text("Guarantor");
        //$("#popApplicant").PopupWindow("setTitle", "Add Guarantor");
        var modal = $(popApplicant);
        modal.find('.modal-title').text('Add Guarantor');
        $("#appphone").addClass("hidden");
        $("#appemail").addClass("hidden");
        $("#apprelationship").removeClass("hidden");
        $('#txtGDateOfBirth').removeClass("hidden");
        $('#txtMDateOfBirth').addClass("hidden");
        $('#txtADateOfBirth').addClass("hidden");
        $('#txtHDateOfBirth').addClass("hidden");
        $//("#ddlGRelationship").removeClass("hidden");
        //$("#ddlGRelationship").val(response.model.Relationship).change();
        //$("#ddlARelationship").addClass("hidden");
        $("#ddlARelationship").empty();
        var opt = "<option value='0'>Select Relationship</option>";
        opt += "<option value='1'>Family Member</option>";
        opt += "<option value='2'>Friend</option>";
        $("#ddlARelationship").append(opt);
        $("#ddlARelationship").val(0).change();
        var dtGApp = new Date();
        dtGApp.setFullYear(new Date().getFullYear() - 18);
        $('#txtGDateOfBirth').datepicker({ endDate: dtGApp, autoclose: true });

        $('#txtApplicantOtherGender').val('');
        $('#appGenderOther').addClass('hidden');
    }

    clearApplicant();

    //$("#popApplicant").PopupWindow("open");
    $("#popApplicant").modal("show");
};
var saveupdateApplicant = function () {

    var msg = "";
    var aid = $("#hndApplicantID").val();
    var prospectID = $("#hdnOPId").val();
    var fname = $("#txtApplicantFirstName").val();
    var lname = $("#txtApplicantLastName").val();
    var aphone = unformatText($("#txtApplicantPhone").val());
    var aemail = $("#txtApplicantEmail").val();
    var agender = $("#ddlApplicantGender").val();
    var type = $("#ddlApplicantType").text();

    var aotherGender = $("#txtApplicantOtherGender").val();

    if (agender == '3') {
        if (!($("#txtApplicantOtherGender").val())) {
            msg += "Enter The Other Gender </br>";
        }
    }
    var dob = "";
    if (type == "Co-Applicant") {
        dob = $("#txtADateOfBirth").val();
    } else if (type == "Minor") {
        dob = $("#txtMDateOfBirth").val();
    }
    else if (type == "Guarantor") {
        dob = $("#txtGDateOfBirth").val();
    }
    else {
        dob = $("#txtHDateOfBirth").val();
    }


    var relationship = $("#ddlARelationship").val();

    if (!fname) {
        msg += "Enter Applicant First Name</br>";
    }
    if (!lname) {
        msg += "Enter Applicant Last Name</br>";
    }

    //if (agender == "0") {
    //    msg += "Select The Gender</br>";
    //}

    //if (type != "Minor" && type != "Guarantor") {
    //    if (!aphone) {
    //        msg += "Enter Phone Number</br>";
    //    }
    //    else {
    //        if (!validatePhone(aphone)) {
    //            msg += "Enter Valid Phone Number</br>";
    //        }
    //    }
    //}

    //if (aemail.length > 0) {
    //    if (!validateEmail(aemail)) {
    //        msg += "Enter Valid Email</br>";
    //    }
    //}
    //if ($('#ddlARelationship').val() == '0') {
    //    msg += "Select The Relationship </br>";
    //}
    //if (!dob) {
    //    msg += "Enter The Date Of Birth </br>";
    //}
    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        ApplicantID: aid,
        FirstName: fname,
        LastName: lname,
        Phone: aphone,
        Email: aemail,
        Gender: agender,
        DateOfBirth: dob,
        TenantID: prospectID,
        Type: type,
        Relationship: relationship,
    };

    $.ajax({
        url: "/Tenant/Applicant/SaveUpdateApplicant/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue',
            });

            getApplicantLists();
            //$("#popApplicant").PopupWindow("close");
            $("#popApplicant").modal("hide");
        }


    });

}
var totalFinalFees = 0;
var getApplicantLists = function () {
    var model = {

        TenantID: $("#hdnOPId").val(),
    }
    $.ajax({
        url: "/Applicant/GetApplicantList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblApplicant").empty();
            $("#tblApplicantFinal").empty();
            $("#tblApplicantMinor").empty();
            $("#tblApplicantGuarantor").empty();
            $("#tblResponsibilityPay>tbody").empty();
            $("#tblPayment>tbody").empty();
            $("#tblEmailCoapplicant>tbody").empty();
       
            $.each(response.model, function (elementType, elementValue) {
                var html = '';
                var prhtml = '';
                var pprhtml = '';
                var emailhtml = '';

                if (elementValue.Type == "Primary Applicant") {
                    $("#hndPriAppFullName").val(elementValue.FirstName + " " + elementValue.LastName + "(Primary Applicant)");
                }

                if (elementValue.Type != "Primary Applicant") {

                    if (elementValue.Type == "Co-Applicant") {
                        html += "<div class='col-sm-4 box-two proerty-item' id='div_" + elementValue.ApplicantID + "'>" +
                            "<div class='form-group col-sm-3'><br>" +
                            "<img src='/Content/assets/img/user.png'><br>" +
                            "<label> " + elementValue.Type + " </label> <br/>" +
                            "</div > " +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;'><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Relationship : <b>" + elementValue.RelationshipString + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Phone : <b>" + formatPhoneFax(elementValue.Phone) + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Email : <b>" + elementValue.Email + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Date Of Birth : <b>" + elementValue.DateOfBirthTxt + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Gender : <b>" + elementValue.GenderString + "</b></div>" +
                            "</div>";
                    }
                    else if (elementValue.Type == "Minor") {
                        html += "<div class='col-sm-4 box-two proerty-item' id='div_" + elementValue.ApplicantID + "'>" +
                            "<div class='form-group col-sm-3'><br>" +
                            "<img src='/Content/assets/img/user.png'><br>" +
                            "<label> " + elementValue.Type + " </label> <br/>" +
                            "</div > " +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;'><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Relationship : <b>" + elementValue.RelationshipString + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Date Of Birth : <b>" + elementValue.DateOfBirthTxt + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Gender : <b>" + elementValue.GenderString + "</b></div>" +
                            "</div>";
                    }
                    else if (elementValue.Type == "Guarantor") {
                        html += "<div class='col-sm-4 box-two proerty-item' id='div_" + elementValue.ApplicantID + "'>" +
                            "<div class='form-group col-sm-3'><br>" +
                            "<img src='/Content/assets/img/user.png'><br>" +
                            "<label> " + elementValue.Type + " </label> <br/>" +
                            "</div > " +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;'><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Relationship : <b>" + elementValue.RelationshipString + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Date Of Birth : <b>" + elementValue.DateOfBirthTxt + "</b></div>" +
                            "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Gender : <b>" + elementValue.GenderString + "</b></div>" +
                            "</div>";
                    }
                }
                else {
                    html += "<div class='col-sm-4 box-two proerty-item'>" +
                        "<div class='form-group col-sm-3'><br>" +
                        "<img src='/Content/assets/img/user.png'><br>" +
                        "<label>Pri-Applicant</label><br/>" +
                        "</div>"+
                        "<div class='form-group col-sm-9' style='margin-top: 0px !important;'><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Relationship : <b>" + elementValue.RelationshipString + "</b></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Phone : <b>" + formatPhoneFax(elementValue.Phone) + "</b></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Email : <b>" + elementValue.Email + "</b></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Date Of Birth : <b>" + elementValue.DateOfBirthTxt + "</b></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 0px !important;margin-bottom: 0px !important;'> Gender : <b>" + elementValue.GenderString + "</b></div>" +
                        
                       // "<label><a href='javascript:void(0)' onclick='goToEditApplicant(" + elementValue.ApplicantID + ")'>Edit/Complete Information</a></label><br/><div style='border: 2px solid #E6E6E6;'><center><label><b>Status: In progress</b></label></center></div>" +
                        "</div>";
                }
                if (elementValue.Type == "Primary Applicant" || elementValue.Type == "Co-Applicant") {
                   
                    //Amit's work 17-10
                    //prhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:18%; padding:6px;'>" + elementValue.Type + "<br /><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></td><td style='width:30%; padding:6px;'><input type='text' id='txtpayper" + elementValue.ApplicantID + "' style='width:40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MoveInPercentage + "'/>(%)<input type='text' id='txtpayamt" + elementValue.ApplicantID + "' style='width: 40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MoveInCharge + "'/>($)</td><td style='width:30%; padding:6px;'><input type='text' id='txtpayperMo" + elementValue.ApplicantID + "' style='width: 40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MonthlyPercentage + "'/>(%)<input type='text' id='txtpayamtMo" + elementValue.ApplicantID + "' style='width: 40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MonthlyPayment + "'/>($)</td></tr>";
                    prhtml += "<tr data-id='" + elementValue.ApplicantID + "'>" +
                        "<td style='width:18%; padding:6px;'>" + elementValue.Type + "<br /><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></td>" +
                        "<td style='width:30%;'>" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search ' type='button'><i class='fa fa-percent'></i></button>" +
                        "<input type='text' class='form-control form-control-small payper' id='txtpayper" + elementValue.ApplicantID + "' style='width:60% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MoveInPercentage + "' disabled='disabled' />" +
                        "</div >" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search pull-left' type='button'><i class='fa fa-dollar'></i></button>" +
                        "<input type='text' class='form-control form-control-small' id='txtpayamt" + elementValue.ApplicantID + "' style='width: 60% !important; border: 1px solid; padding-left: 5px; text-align:right;' value='" + parseFloat(elementValue.MoveInCharge).toFixed(2) + "' disabled='disabled'/>" +
                        "</div ></td>" +
                        "<td style='width:30%;'>" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search ' type='button'><i class='fa fa-percent'></i></button>" +
                        "<input type='text' class='form-control form-control-small payperMo' id='txtpayperMo" + elementValue.ApplicantID + "' style='width: 60% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MonthlyPercentage + "' disabled='disabled'/>" +
                        "</div >" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search pull-left' type='button'><i class='fa fa-dollar'></i></button>" +
                        "<input type='text' class='form-control form-control-small' id='txtpayamtMo" + elementValue.ApplicantID + "' style='width: 60% !important; border: 1px solid; padding-left: 5px; text-align:right;' value='" + parseFloat(elementValue.MonthlyPayment).toFixed(2) + "' disabled='disabled'/>" +
                        "</div >" +
                        "</td></tr>";
                }
                if (elementValue.Type == "Primary Applicant" || elementValue.Type == "Co-Applicant" || elementValue.Type == "Guarantor") {
                    //Amit's work 17-10
                    //adminfess = $("#lblAdminFees").text();
                    adminfess = $("#lblFNLAmount").text();
                    totalFinalFees += parseFloat(adminfess);
                    pprhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:18%; padding:6px;'>" + elementValue.Type + " </td><td style='width:20%; padding:6px;'>" + elementValue.FirstName + " " + elementValue.LastName + "</td><td style='width:30%; padding:6px;'>$" + adminfess + "</tr>";
                }

                if (elementValue.Type == "Co-Applicant" || elementValue.Type == "Guarantor") {
                    //Sachin's work 22-10

                    emailhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:20%; padding:6px;'>" + elementValue.FirstName + " " + elementValue.LastName + "</td><td style='width:18%; padding:6px;'>" + elementValue.Email + " </td><td style='width:30%; padding:6px;'><input type='checkbox' onclick='addEmail(\"" + elementValue.Email + "\")' id='chkEmail" + elementValue.ApplicantID + "' style='width:25%; border:1px solid;' /></td></tr>";
                }

                if (elementValue.Type == "Minor") {
                    $("#tblApplicantMinor").append(html);
                }
                else if (elementValue.Type == "Guarantor") {
                    $("#tblApplicantGuarantor").append(html);
                }
                else {
                    $("#tblApplicant").append(html);
                    $("#tblApplicantFinal").append(html);
                }
                $("#tblResponsibilityPay>tbody").append(prhtml);
                $("#tblPayment>tbody").append(pprhtml);
                $("#tblEmailCoapplicant>tbody").append(emailhtml);

                if (elementValue.Type == "Primary Applicant") {
                    if ($("#txtpayper" + elementValue.ApplicantID).val() == "") {
                        $("#txtpayper" + elementValue.ApplicantID).val(100);
                    }
                    if ($("#txtpayperMo" + elementValue.ApplicantID).val() == "") {
                        $("#txtpayperMo" + elementValue.ApplicantID).val(100);
                    }
                }


                $("#txtpayper" + elementValue.ApplicantID).keyup(function () {
                    var chargesPecentage = $("#txtpayper" + elementValue.ApplicantID).val();
                    var perCharges = ((chargesPecentage * parseFloat(unformatText($("#lbtotdueatmov6").text()))) / 100);
                    $("#txtpayamt" + elementValue.ApplicantID).val(perCharges.toFixed(2));


                    var sum = parseFloat(0);
                    $(".payper").each(function () {
                        sum += parseFloat(this.value);

                    });
                    localStorage.setItem("percentage", sum);
                });
                $("#txtpayamt" + elementValue.ApplicantID).keyup(function () {
                    var chargesAmount = $("#txtpayamt" + elementValue.ApplicantID).val();
                    var chargesPer = ((chargesAmount * 100) / parseFloat(unformatText($("#lbtotdueatmov6").text())));
                    $("#txtpayper" + elementValue.ApplicantID).val(chargesPer.toFixed(2));
                });

                $("#txtpayperMo" + elementValue.ApplicantID).keyup(function () {
                    var monthlyPercentage = $("#txtpayperMo" + elementValue.ApplicantID).val();
                    var monthlyPayment = unformatText($("#lblRFPTotalMonthlyPayment").text());
                    var perMonth = ((monthlyPercentage * parseFloat(monthlyPayment, 10)) / 100);
                    $("#txtpayamtMo" + elementValue.ApplicantID).val(perMonth.toFixed(2));

                    var sumMo = parseFloat(0);
                    $(".payperMo").each(function () {
                        sumMo += parseFloat(this.value);

                    });
                    localStorage.setItem("percentageMo", sumMo);
                });

                $("#txtpayamtMo" + elementValue.ApplicantID).keyup(function () {
                    var perMonth = $("#txtpayamtMo" + elementValue.ApplicantID).val();
                    var monthlyPayment = unformatText($("#lblRFPTotalMonthlyPayment").text());
                    var monthlyPercentage = ((perMonth * 100) / parseFloat(monthlyPayment, 10));
                    $("#txtpayperMo" + elementValue.ApplicantID).val(monthlyPercentage.toFixed(2));
                });

                //$(":input").on("keyup", function (e) {
                //    var id = this.id;
                //    var totalPercentage = 100;
                //    if (id == "txtpayper" + elementValue.ApplicantID) {
                //        if ($(":input").hasClass("payper")) {
                //            var payperLength = $(this).length;
                //            if (payperLength == 1) {
                //                $(id).val(totalPercentage);
                //            }
                //        }
                //    }
                //});

                var sum = parseFloat(0);
                $(".payper").each(function () {
                    sum += parseFloat(this.value);

                });
                localStorage.setItem("percentage", sum);
                var sumMo = parseFloat(0);
                $(".payperMo").each(function () {
                    sumMo += parseFloat(this.value);

                });
                localStorage.setItem("percentageMo", sumMo);
            });

            $("#totalFinalFees").text("$" + parseFloat(totalFinalFees).toFixed(2));
            //$("#tblApplicant").append("<div class='col-sm-3 box-two proerty-item'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><label></br><a href='javascript:void(0)' id='btnAddApplicant' onclick='addApplicant(1)'><i class='fa fa-plus-circle'></i> Add Co-Applicant</a></label></div></div></div></div>");
            //$("#tblApplicantMinor").append("<div class='col-sm-3 box-two proerty-item'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><label></br><a href='javascript:void(0)' id='btnAddApplicant' onclick='addApplicant(2)'><i class='fa fa-plus-circle'></i> Add Minor</a></label></div></div></div></div>");
            //$("#tblApplicantGuarantor").append("<div class='col-sm-3 box-two proerty-item'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><label></br><a href='javascript:void(0)' id='btnAddApplicant' onclick='addApplicant(3)'><i class='fa fa-plus-circle'></i> Add Guarantor</a></label></div></div></div></div>");

        }
    });
}

var goToEditApplicant = function (aid) {

    if (aid != null) {

        $("#hndApplicantID").val(aid);
        var model = { id: aid };
        $.ajax({
            url: "/Tenant/Applicant/GetApplicantDetails",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {

                $("#txtApplicantFirstName").val(response.model.FirstName);
                $("#txtApplicantLastName").val(response.model.LastName);

                if (response.model.Type == "Primary Applicant") {
                    $("#ddlApplicantGender").val(response.model.Gender);
                    $("#ddlApplicantGender").trigger('change');
                    $("#ddlApplicantType").text("Primary Applicant");
                    //$("#popApplicant").PopupWindow("setTitle", "Edit Primary Applicant");
                    //$("#popApplicant").PopupWindow("open");
                    var modal = $(popApplicant);
                    modal.find('.modal-title').text('Edit Primary Applicant');
                    $("#popApplicant").modal("show");
                    $("#appphone").removeClass("hidden");
                    $("#appemail").removeClass("hidden");
                    $("#apprelationship").addClass("hidden");
                    $("#txtApplicantPhone").val(formatPhoneFax(response.model.Phone));
                    $("#txtApplicantEmail").val(response.model.Email);
                    //$("#ddlARelationship").removeCs("hidden");
                    $("#ddlARelationship").empty();
                    var opt = "<option value='0'>Select Relationship</option>";
                    opt += "<option value='1' selected='selected'>Self</option>";
                    $("#ddlARelationship").append(opt);
                    $("#ddlARelationship").val(response.model.Relationship).change();

                    //$("#ddlGRelationship").addClass("hidden");
                    $('#txtADateOfBirth').addClass("hidden");
                    $('#txtHDateOfBirth').removeClass("hidden");
                    $("#txtHDateOfBirth").val(response.model.DateOfBirthTxt);
                    $('#txtMDateOfBirth').addClass("hidden");
                    $('#txtGDateOfBirth').addClass('hidden');
                    var dtHApp = new Date();
                    dtHApp.setFullYear(new Date().getFullYear() - 18);
                    $('#txtHDateOfBirth').datepicker({ endDate: dtHApp, autoclose: true });
                    $("#txtApplicantOtherGender").val(response.model.OtherGender);

                }
                else if (response.model.Type == "Co-Applicant") {
                    $("#ddlApplicantGender").val(response.model.Gender);
                    $("#ddlApplicantGender").trigger('change');
                    $("#ddlApplicantType").text("Co-Applicant");
                    //$("#popApplicant").PopupWindow("setTitle", "Edit Applicant");
                    //$("#popApplicant").PopupWindow("open");
                    var modal = $(popApplicant);
                    modal.find('.modal-title').text('Edit Applicant');
                    $("#popApplicant").modal("show");
                    $("#appphone").removeClass("hidden");
                    $("#apprelationship").addClass("hidden");
                    $("#txtApplicantPhone").val(formatPhoneFax(response.model.Phone));
                    $("#txtApplicantEmail").val(response.model.Email);
                    //$("#ddlARelationship").removeClass("hidden");
                    $("#ddlARelationship").empty();
                    var opt = "<option value='0'>Select Relationship</option>";
                    opt += "<option value='1'>Spouse</option>";
                    opt += "<option value='2'>Partner</option>";
                    opt += "<option value='3'>Adult Child</option>";
                    opt += "<option value='4'>Friend/Roommate</option>";
                    $("#ddlARelationship").append(opt);
                    $("#ddlARelationship").val(response.model.Relationship).change();
                    //$("#ddlGRelationship").addClass("hidden");
                    $('#txtADateOfBirth').removeClass("hidden");
                    $("#txtADateOfBirth").val(response.model.DateOfBirthTxt);
                    $('#txtHDateOfBirth').addClass("hidden");
                    $('#txtMDateOfBirth').addClass("hidden");
                    $('#txtGDateOfBirth').addClass('hidden');
                    var dtApp = new Date();
                    dtApp.setFullYear(new Date().getFullYear() - 18);
                    $('#txtADateOfBirth').datepicker({ endDate: dtApp, autoclose: true });
                    $("#txtApplicantOtherGender").val(response.model.OtherGender);

                }
                else if (response.model.Type == "Minor") {
                    $("#ddlApplicantGender").val(response.model.Gender);
                    $("#ddlApplicantGender").trigger('change');
                    $("#ddlApplicantType").text("Minor");
                    //$("#popApplicant").PopupWindow("setTitle", "Edit Minor");
                    //$("#popApplicant").PopupWindow("open");
                    var modal = $(popApplicant);
                    modal.find('.modal-title').text('Edit Minor');
                    $("#popApplicant").modal("show");
                    $("#appphone").addClass("hidden");
                    $("#appemail").addClass("hidden");
                    $('#txtMDateOfBirth').removeClass("hidden");
                    $('#txtADateOfBirth').addClass("hidden");
                    $('#txtHDateOfBirth').addClass("hidden");
                    $('#txtGDateOfBirth').addClass('hidden');
                    $("#txtMDateOfBirth").val(response.model.DateOfBirthTxt);
                    //$("#ddlARelationship").removeClass("hidden");
                    $("#ddlARelationship").empty();
                    var opt = "<option value='0'>Select Relationship</option>";
                    opt += "<option value='1'>Family Member</option>";
                    opt += "<option value='2'>Child</option>";
                    $("#ddlARelationship").append(opt);
                    $("#ddlARelationship").val(response.model.Relationship).change();
                    //$("#ddlGRelationship").addClass("hidden");
                    // $("#ddlGRelationship").addClass("hidden");
                    var dtMin = new Date();
                    dtMin.setFullYear(new Date().getFullYear() - 18);
                    var dtEnd = new Date();
                    $('#txtMDateOfBirth').datepicker({ viewMode: "years", startDate: dtMin, endDate: dtEnd, autoclose: true });
                    $("#txtApplicantOtherGender").val(response.model.OtherGender);

                }
                else if (response.model.Type == "Guarantor") {

                    $("#ddlApplicantGender").val(response.model.Gender);
                    $("#ddlApplicantGender").trigger('change');
                    $("#ddlApplicantType").text("Guarantor");
                    //$("#popApplicant").PopupWindow("setTitle", "Edit Guarantor");
                    //$("#popApplicant").PopupWindow("open");
                    var modal = $(popApplicant);
                    modal.find('.modal-title').text('Edit Guarantor');
                    $("#popApplicant").modal("show");
                    $("#appphone").addClass("hidden");
                    $("#appemail").removeClass("hidden");
                    // $("#apprelationship").removeClass("hidden");
                    $('#txtGDateOfBirth').removeClass("hidden");
                    $('#txtMDateOfBirth').addClass("hidden");
                    $('#txtADateOfBirth').addClass("hidden");
                    $('#txtHDateOfBirth').addClass("hidden");
                    $("#txtGDateOfBirth").val(response.model.DateOfBirthTxt);
                    $//("#ddlGRelationship").removeClass("hidden");
                    //$("#ddlGRelationship").val(response.model.Relationship).change();
                    //$("#ddlARelationship").addClass("hidden");
                    $("#ddlARelationship").empty();
                    var opt = "<option value='0'>Select Relationship</option>";
                    opt += "<option value='1'>Family</option>";
                    opt += "<option value='2'>Friend</option>";
                    $("#ddlARelationship").append(opt);
                    $("#ddlARelationship").val(response.model.Relationship).change();
                    var dtGApp = new Date();
                    dtGApp.setFullYear(new Date().getFullYear() - 18);
                    $('#txtGDateOfBirth').datepicker({ endDate: dtGApp, autoclose: true });
                    $("#txtApplicantOtherGender").val(response.model.OtherGender);
                }
            }
        });

    }
};
var clearApplicant = function () {
    $("#hndApplicantID").val(0);
    $("#txtApplicantFirstName").val("");
    $("#txtApplicantLastName").val("");

    $("#txtApplicantPhone").val("");
    $("#txtApplicantEmail").val("");
    $("#ddlApplicantGender").val(0);
    $('#txtMDateOfBirth').val("");
    $('#txtADateOfBirth').val("");
    $('#txtHDateOfBirth').val("");
    $('#txtGDateOfBirth').val("");
}
var saveupdatePet = function () {

    var msg = "";
    var petId = $("#hndPetID").val();
    //var petType = $("#ddlpetType").val();
    var breed = $("#txtpetBreed").val();
    var weight = $("#txtpetWeight").val();
    //var age = $("#txtpetAge").val();
    var prospectID = $("#hdnOPId").val();
    var photo = document.getElementById('pet-picture');
    var petVaccinationCertificate = document.getElementById('filePetVaccinationCertificate');
    var petName = $("#txtpetName").val();
    var vetsName = $("#txtpetVetsName").val();
    var hiddenPetPicture = $("#hndPetPicture").val();
    var hiddenPetVaccinationCertificate = $("#hndPetVaccinationCertificate").val();
    var hiddenOriginalPetPicture = $("#hndOriginalPetPicture").val();
    var hiddenOriginalPetVaccinationCertificate = $("#hndOriginalPetVaccinationCertificate").val();
    //if (petType == '0') {
    //    msg += "Select Pet Type</br>";
    //}
    if (!breed) {
        msg += "Enter Pet Breed</br>";
    }
    if (hiddenPetPicture.length == '0') {
        if (document.getElementById('pet-picture').files.length == '0') {
            msg += "Please Upload Pet Picture</br>";
        }
    }
    if (hiddenPetVaccinationCertificate.length == '0') {
        if (document.getElementById('filePetVaccinationCertificate').files.length == '0') {
            msg += "Please Upload Pet Vaccination Certificate</br>";
        }
    }
    if (msg !== "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        });
        return;
    }
    $formData = new FormData();

    $formData.append('PetID', petId);
    //$formData.append('PetType', petType);
    $formData.append('Breed', breed);
    $formData.append('Weight', weight);
    //$formData.append('Age', age);
    $formData.append('TenantID', prospectID);
    $formData.append('PetName', petName);
    $formData.append('VetsName', vetsName);

    $formData.append('Photo', hiddenPetPicture);
    $formData.append('PetVaccinationCertificate', hiddenPetVaccinationCertificate);
    $formData.append('OriginalPetNameFile', hiddenOriginalPetPicture);
    $formData.append('OriginalPetVaccinationCertificateFile', hiddenOriginalPetVaccinationCertificate);

    $.ajax({
        url: "/Tenant/Pet/SaveUpdatePet/",
        type: "post",
        data: $formData,
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue'
            });

            getPetLists();
            //$("#popPet").PopupWindow("close");
            $("#popPet").modal("hide");
        }
    });
};

var getPetLists = function () {

    var model = {
        TenantID: $("#hdnOPId").val()
    };
    $.ajax({
        url: "/Pet/GetPetList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblPet>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.PetID + "' data-value='" + elementValue.PetID + "'>";
                html += "<td align='center'><img src='/content/assets/img/pet/" + elementValue.Photo + "' class='picture-src' title='' style='height:70px;width:70px;'/></td>";
                //if (elementValue.PetType == '1') {
                //    html += "<td>Cat</td>";
                //}
                //else {
                //    html += "<td>Dog</td>";
                //}
                html += "<td>" + elementValue.PetName + "</td>";
                html += "<td>" + elementValue.Breed + "</td>";
                html += "<td>" + elementValue.Weight + "</td>";
                html += "<td>" + elementValue.VetsName + "</td>";
                html += "<td class='text-center'>";
                html += "<button style='background: transparent;' id='updatePetInfo' onclick='getPetInfo(" + elementValue.PetID + ")'><span class='fa fa-eye'></span></button>";
                //html += "<button style='background: transparent;' onclick='delPet(" + elementValue.PetID + ")'><span class='fa fa-trash' ></span></button>";
                //html += "<a href='/Content/assets/img/pet/" + elementValue.PetVaccinationCertificate + "' download=" + elementValue.PetVaccinationCertificate + " target='_blank'><span class='fa fa-download'></span></a>";
                html += "</td >";
                html += "</tr>";
                $("#tblPet>tbody").append(html);
            });
        }
    });
};

var getPetInfo = function (id) {
    //$("#popPet").PopupWindow("open");
    $("#popPet").modal("show");
    var model = {
        id: id
    };
    $.ajax({
        url: '/Tenant/Pet/GetPetDetails/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#hndPetID").val(response.model.PetID);
            $("#txtpetName").text(response.model.PetName);
            $("#txtpetVetsName").text(response.model.VetsName);
            $("#ddlpetType").text(response.model.PetType);
            $("#txtpetBreed").text(response.model.Breed);
            $("#txtpetWeight").text(response.model.Weight);
            $("#txtpetAge").text(response.model.Age);
            $("#hndPetPicture").val(response.model.Photo);
            $("#hndOriginalPetPicture").val(response.model.OriginalPetNameFile);
            $("#hndPetVaccinationCertificate").val(response.model.PetVaccinationCertificate);
            $("#hndOriginalPetVaccinationCertificate").val(response.model.OriginalPetVaccinationCertificateFile);
            if (response.model.OriginalPetVaccinationCertificateFile != '') {
                $("#filePetPictireShow").text(response.model.OriginalPetNameFile);
            }
            if (response.model.OriginalPetVaccinationCertificateFile != '') {
                $("#filePetVaccinationCertificateShow").text(response.model.OriginalPetVaccinationCertificateFile);
            }

            var result = doesFileExist('/Content/assets/img/pet/' + response.model.Photo);

            if (result == true) {
                $('#dwnldPetPhoto').attr('href', '/Content/assets/img/pet/' + response.model.Photo);
                $('#dwnldPetPhoto').attr('download', response.model.OriginalPetNameFile);
                $('#dwnldPetPhoto').html("<i class='fa fa-download'></i> " + response.model.OriginalPetNameFile);
            } else {
                $('#dwnldPetPhoto').html("<i class='fa fa-download'></i> " + response.model.OriginalPetNameFile);
                $('#dwnldPetPhoto').attr('href', 'JavaScript:Void(0)');
                $('#dwnldPetPhoto').attr('onclick', 'fileNotExist()');
            }

            var result1 = doesFileExist('/Content/assets/img/pet/' + response.model.PetVaccinationCertificate);

            if (result1 == true) {
                $('#dwnldVaccinationCertificate').attr('href', '/Content/assets/img/pet/' + response.model.PetVaccinationCertificate);
                $('#dwnldVaccinationCertificate').attr('download', response.model.OriginalPetVaccinationCertificateFile);
                $('#dwnldVaccinationCertificate').html("<i class='fa fa-download'></i> " + response.model.OriginalPetVaccinationCertificateFile);
            } else {
                $('#dwnldVaccinationCertificate').html("<i class='fa fa-download'></i> " + response.model.OriginalPetVaccinationCertificateFile);
                $('#dwnldVaccinationCertificate').attr('href', 'JavaScript:Void(0)');
                $('#dwnldVaccinationCertificate').attr('onclick', 'fileNotExist()');
            }
        }
    });
};
var clearPet = function () {
    var petId = $("#hndPetID").val("0");
    var petType = $("#ddlpetType").val("0");
    var breed = $("#txtpetBreed").val("");
    var weight = $("#txtpetWeight").val("");
    var age = $("#txtpetAge").val("");
};
var saveupdateVehicle = function () {

    var msg = "";
    var vid = $("#hndVehicleID").val();

    var vtype = $("#ddlVehicleType").val();
    var vmake = $("#txtVehicleMake").val();
    var vmodel = $("#txtVehicleModel").val();
    var vyear = $("#ddlVehicleyear").val();
    var vcolor = $("#txtVehicleColor").val();
    var vlicence = $("#txtVehicleLicence").val();
    var vstate = $("#ddlVState").val();
    var vfileUploadVehicleRegistation = $("#fileUploadVehicleRegistation").val();
    var prospectID = $("#hdnOPId").val();
    var vehicleRegistration = document.getElementById("fileUploadVehicleRegistation");
    var vOwnerName = $("#txtVehicleOwnerName").val();
    var vNotes = $("#txtVehicleNote").val();
    var vRegistationCert = $("#hndVehicleRegistation").val();
    var vOriginalRegistationCert = $("#hndOriginalVehicleRegistation").val();
    if (vtype == "0") {
        msg += "Enter Vehicle Type</br>";
    }
    if (!vmake) {
        msg += "Enter Vehicle Make</br>";
    }
    if (!vlicence) {
        msg += "Enter Vehicle Licence</br>";
    }
    if (!vyear) {
        msg += "Enter Vehicle Year</br>";
    }
    if (vstate == "0") {
        msg += "Select Vehicle State</br>";
    }
    if ($("#hndVehicleRegistation").val() == '0') {
        if (document.getElementById("fileUploadVehicleRegistation").files.length == '0') {
            msg += "Upload The Vehicle Registration Certificate</br>";
        }
    }
    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        });
        return;
    }


    $formData = new FormData();
    $formData.append('Vehicle_ID', vid);
    $formData.append('Make', vmake);
    $formData.append('VModel', vmodel);
    $formData.append('Year', vyear);
    $formData.append('Color', vcolor);
    $formData.append('State', vstate);
    $formData.append('License', vlicence);
    $formData.append('TenantID', prospectID);
    $formData.append('OwnerName', vOwnerName);
    $formData.append('Notes', vNotes);
    $formData.append('VehicleRegistration', vRegistationCert);
    $formData.append('OriginalVehicleRegistation', vOriginalRegistationCert);
    $.ajax({
        url: "/Tenant/Vehicle/SaveUpdateVehicle/",
        type: "post",
        contentType: "application/json utf-8",
        data: $formData,
        contentType: false,
        processData: false,
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue',
            });

            getVehicleLists();
            //$("#popVehicle").PopupWindow("close");
            $("#popVehicle").modal("hide");
        }


    });

}
var getVehicleLists = function () {
    var model = {

        TenantID: $("#hdnOPId").val(),
    }
    $.ajax({
        url: "/Tenant/Vehicle/GetVehicleList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblVehicle>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.Vehicle_ID + "' data-value='" + elementValue.Vehicle_ID + "'>";
                html += "<td>" + elementValue.OwnerName + "</td>";
                html += "<td>" + elementValue.Make + "</td>";
                html += "<td>" + elementValue.VModel + "</td>";
                html += "<td>" + elementValue.Year + "</td>";
                html += "<td>" + elementValue.Color + "</td>";
                html += "<td>" + elementValue.License + "</td>";
                html += "<td class='text-center'><button style='background: transparent;' onclick='getVehicleInfo(" + elementValue.Vehicle_ID + ")'><span class='fa fa-eye' ></span></button>";
                //html += "<td class='text-center'><button style='background: transparent;' onclick='delVehicle(" + elementValue.Vehicle_ID + ")'><span class='fa fa-trash' ></span></button>";
                //html += "<a style='background: transparent;' href='/Content/assets/img/VehicleRegistration/" + elementValue.VehicleRegistration + "' download=" + elementValue.VehicleRegistration + " target='_blank'><span class='fa fa-download' style='background: transparent;'></span></a></td>";

                html += "</tr>";
                $("#tblVehicle>tbody").append(html);
            });

        }
    });
}

var clearVehicle = function () {
    $("#hndVehicleID").val(0);
    $("#txtVehicleMake").val("");
    $("#txtVehicleModel").val("");

    $("#txtVehicleColor").val("");
    $("#txtVehicleLicence").val("");
    $("#ddlVState").val(0);
}
var saveupdateTenantOnline = function () {
    var msg = "";
    var ProspectID = $("#hdnOPId").val();
    var isInternational = $("#ddlIsInter").val();
    var FirstName = $("#txtFirstNamePersonal").val();
    var MiddleInitial = $("#txtMiddleInitial").val();
    var LastName = $("#txtLastNamePersonal").val();
    var DateOfBirth = $("#txtDateOfBirth").val();
    var Gender = $("#ddlGender").val();
    var Email = $("#txtEmailNew").val();
    var Mobile = unformatText($("#txtMobileNumber").val());
    var PassportNumber = $("#txtPassportNum").val();
    var CountryIssuance = $("#txtCOI").val();
    var DateIssuance = $("#txtDateOfIssuance").val();
    var DateExpire = $("#txtDateOfExpiration").val();
    var IDType = $("#ddlDocumentTypePersonal").val();
    var State = $("#ddlStatePersonal").val();
    var IDNumber = $("#txtIDNumber").data("value");
    var SSNNumber = $("#txtSSNNumber").data("value");
    var Country = $("#txtCountry").val();
    var HomeAddress1 = $("#txtAddress1").val();
    var HomeAddress2 = $("#txtAddress2").val();
    var StateHome = $("#ddlStateHome").val();
    var CityHome = $("#ddlCityHome").val();
    var ZipHome = $("#txtZip").val();
    var RentOwn = $("#ddlRentOwn").val();
    var MoveInDateFrom = $("#txtMoveInDateFrom").val();
    var MoveInDateTo = $("#txtMoveInDateTo").val();
    var MonthlyPayment = unformatText($("#txtMonthlyPayment").val());
    var Reason = $("#txtReasonforleaving").val();

    var Country2 = $("#txtCountry2").val();
    var HomeAddress12 = $("#txtAddress12").val();
    var HomeAddress22 = $("#txtAddress22").val();
    var StateHome2 = $("#ddlStateHome2").val();
    var CityHome2 = $("#ddlCityHome2").val();
    var ZipHome2 = $("#txtZip2").val();
    var RentOwn2 = $("#ddlRentOwn2").val();
    var MoveInDateFrom2 = $("#txtMoveInDateFrom2").val();
    var MoveInDateTo2 = $("#txtMoveInDateTo2").val();
    var MonthlyPayment2 = unformatText($("#txtMonthlyPayment2").val());
    var Reason2 = $("#txtReasonforleaving2").val();

    var isAdditionalRHistory = $("#ddladdHistory").val();

    var EmployerName = $("#txtEmployerName").val();
    var JobTitle = $("#txtJobTitle").val();
    var JobType = $("#ddlJobType").val();
    var StartDate = $("#txtStartDate").val();
    var Income = unformatText($("#txtAnnualIncome").val());
    var AdditionalIncome = unformatText($("#txtAddAnnualIncome").val());
    var SupervisorName = $("#txtSupervisiorName").val();
    var SupervisorPhone = unformatText($("#txtSupervisiorPhone").val());
    var SupervisorEmail = $("#txtSupervisiorEmail").val();
    var OfficeCountry = $("#txtCountryOffice").val();
    var OfficeAddress1 = $("#txtofficeAddress1").val();
    var OfficeAddress2 = $("#txtofficeAddress2").val();
    var OfficeState = $("#ddlStateEmployee").val();
    var OfficeCity = $("#ddlCityEmployee").val();
    var OfficeZip = $("#txtZipOffice").val();
    var Relationship = $("#txtRelationship").val();
    var EmergencyFirstName = $("#txtEmergencyFirstName").val();
    var EmergencyLastName = $("#txtEmergencyLastName").val();
    var EmergencyMobile = unformatText($("#txtEmergencyMobile").val());
    var EmergencyHomePhone = unformatText($("#txtEmergencyHomePhone").val());
    var EmergencyWorkPhone = unformatText($("#txtEmergencyWorkPhone").val());
    var EmergencyEmail = $("#txtEmergencyEmail").val();
    var EmergencyCountry = $("#txtEmergencyCountry").val();
    var EmergencyAddress1 = $("#txtEmergencyAddress1").val();
    var EmergencyAddress2 = $("#txtEmergencyAddress2").val();
    var EmergencyStateHome = $("#ddlStateContact").val();
    var EmergencyCityHome = $("#ddlCityContact").val();
    var EmergencyZipHome = $("#txtEmergencyZip").val();
    var OtherGender = $("#txtOtherGender").val();

    var fileUpload1 = $("#hndFileUploadName1").data('value');
    var originalFileUpload1 = $("#hndOriginalFileUploadName1").val();
    var fileUpload2 = $("#hndFileUploadName2").data('value');
    var originalFileUpload2 = $("#hndOriginalFileUploadName2").val();
    var fileUpload3 = $("#hndFileUploadName3").data('value');
    var originalFileUpload3 = $("#hndOriginalFileUploadName3").val();
    var filePassport = $("#hndPassportUploadName").data('value');
    var originalFilePassport = $("#hndOriginalPassportUploadName").val();
    var fileIdentity = $("#hndIdentityUploadName").data('value');
    var originalFileIdentity = $("#hndOriginalIdentityUploadName").val();


    if (!OtherGender) {
        OtherGender = $("#txtOtherGender").val(" ");
    }

    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return;
    }
    $formData = new FormData();
    $formData.append('ProspectID', ProspectID);
    $formData.append('FirstName', FirstName);
    $formData.append('MiddleInitial', MiddleInitial);
    $formData.append('LastName', LastName);
    $formData.append('DateOfBirth', DateOfBirth);
    $formData.append('Gender', Gender);
    $formData.append('Email', Email);
    $formData.append('Mobile', Mobile);
    $formData.append('PassportNumber', PassportNumber);
    $formData.append('CountryIssuance', CountryIssuance);
    $formData.append('DateIssuance', DateIssuance);
    $formData.append('DateExpire', DateExpire);
    $formData.append('IDType', IDType);
    $formData.append('State', State);
    $formData.append('IDNumber', IDNumber);
    $formData.append('Country', Country);
    $formData.append('HomeAddress1', HomeAddress1);
    $formData.append('HomeAddress2', HomeAddress2);
    $formData.append('StateHome', StateHome);
    $formData.append('CityHome', CityHome);
    $formData.append('ZipHome', ZipHome);
    $formData.append('RentOwn', RentOwn);
    $formData.append('MoveInDateFrom', MoveInDateFrom);
    $formData.append('MoveInDateTo', MoveInDateTo);
    $formData.append('MonthlyPayment', MonthlyPayment);
    $formData.append('Reason', Reason);
    $formData.append('Country2', Country2);
    $formData.append('HomeAddress12', HomeAddress12);
    $formData.append('HomeAddress22', HomeAddress22);
    $formData.append('StateHome2', StateHome2);
    $formData.append('CityHome2', CityHome2);
    $formData.append('ZipHome2', ZipHome2);

    $formData.append('RentOwn2', RentOwn2);
    $formData.append('MoveInDateFrom2', MoveInDateFrom2);
    $formData.append('MoveInDateTo2', MoveInDateTo2);
    $formData.append('MonthlyPayment2', MonthlyPayment2);
    $formData.append('Reason2', Reason2);
    $formData.append('EmployerName', EmployerName);
    $formData.append('JobTitle', JobTitle);
    $formData.append('JobType', JobType);
    $formData.append('StartDate', StartDate);
    $formData.append('Income', Income);
    $formData.append('AdditionalIncome', AdditionalIncome);
    $formData.append('SupervisorName', SupervisorName);
    $formData.append('SupervisorPhone', SupervisorPhone);
    $formData.append('SupervisorEmail', SupervisorEmail);
    $formData.append('OfficeCountry', OfficeCountry);
    $formData.append('OfficeAddress1', OfficeAddress1);
    $formData.append('OfficeAddress2', OfficeAddress2);
    $formData.append('OfficeState', OfficeState);
    $formData.append('OfficeCity', OfficeCity);
    $formData.append('OfficeZip', OfficeZip);
    $formData.append('Relationship', Relationship);
    $formData.append('EmergencyFirstName', EmergencyFirstName);
    $formData.append('EmergencyLastName', EmergencyLastName);
    $formData.append('EmergencyMobile', EmergencyMobile);
    $formData.append('EmergencyHomePhone', EmergencyHomePhone);
    $formData.append('EmergencyWorkPhone', EmergencyWorkPhone);
    $formData.append('EmergencyEmail', EmergencyEmail);
    $formData.append('EmergencyCountry', EmergencyCountry);
    $formData.append('EmergencyAddress1', EmergencyAddress1);
    $formData.append('EmergencyAddress2', EmergencyAddress2);
    $formData.append('EmergencyStateHome', EmergencyStateHome);
    $formData.append('EmergencyCityHome', EmergencyCityHome);

    $formData.append('EmergencyZipHome', EmergencyZipHome);
    $formData.append('IsInternational', isInternational);
    $formData.append('IsAdditionalRHistory', isAdditionalRHistory);
    $formData.append('OtherGender', OtherGender);
    $formData.append('SSN', SSNNumber);

    $formData.append('TaxReturn', fileUpload1);
    $formData.append('UploadOriginalFileName1', originalFileUpload1);
    $formData.append('TaxReturn2', fileUpload2);
    $formData.append('UploadOriginalFileName2', originalFileUpload2);
    $formData.append('TaxReturn3', fileUpload3);
    $formData.append('UploadOriginalFileName3', originalFileUpload3);

    $formData.append('PassportDocument', filePassport);
    $formData.append('UploadOriginalPassportName', originalFilePassport);
    $formData.append('IdentityDocument', fileIdentity);
    $formData.append('UploadOriginalIdentityName', originalFileIdentity);

    if ($("#rbtnPaystub").is(":checked")) {
        $formData.append('IsPaystub', true);
    }
    else if ($("#rbtnFedralTax").is(":checked")) {
        $formData.append('IsPaystub', false);
    }

    $.ajax({
        url: '/ApplyNow/SaveTenantOnline',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            //getApplyNowList(idmsg[0]);

        }
    });
};
var getTenantOnlineList = function (id) {
    var model = {
        id: id
    };
    $.ajax({
        url: '/ApplyNow/GetTenantOnlineList',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#ddlIsInter").text(response.model.IsInternational == '1' ? 'Yes' : 'No');
            $("#ddladdHistory").val(response.model.IsAdditionalRHistory).change();
            $("#txtFirstNamePersonal").text(response.model.FirstName);
            $("#txtMiddleInitial").text(response.model.MiddleInitial);
            $("#txtLastNamePersonal").text(response.model.LastName);
            $("#txtDateOfBirth").text(response.model.DateOfBirthTxt);
            $("#ddlGender").text(response.model.Gender == 1 ? 'Male' : response.model.Gender == 2 ? 'Female' : response.model.Gender == 3 ? 'Other :' : '');
            $("#txtEmailNew").text(response.model.Email);
            $("#txtMobileNumber").text(formatPhoneFax(response.model.Mobile));
            $("#txtPassportNum").text(response.model.PassportNumber);
            $("#txtCOI").text(response.model.CountryIssuance);
            $("#txtDateOfIssuance").text(response.model.DateIssuanceTxt);
            $("#txtDateOfExpiration").text(response.model.DateExpireTxt);
            $("#ddlDocumentTypePersonal").text(response.model.IDType == '1' ? "Driver's License" : response.model.IDType == '2' ? "Military ID" : response.model.IDType == '3' ? "Passport" : response.model.IDType == '4' ? "State Issued ID" : "");
            $("#ddlStatePersonal").text(response.model.StatePersonalString);
            if (response.model.SSN == '' || response.model.SSN == null) {
                $("#txtSSNNumber").text('');
            }
            else {
                $("#txtSSNNumber").text("***-**-" + response.model.SSN.substr(response.model.SSN.length - 5, 4));
            }
            $("#txtSSNNumber").data("value", response.model.SSN);
            $("#hndSSNNumber").val(response.model.SSN);

            if (response.model.IDNumber == '' || response.model.IDNumber == null) {
                $("#txtIDNumber").text('');
            }
            else {
                $("#txtIDNumber").text(("*".repeat(response.model.IDNumber.length - 4) + response.model.IDNumber.substr(response.model.IDNumber.length - 4, 4)));
            }
            $("#txtIDNumber").data("value", response.model.IDNumber);

            $("#txtAddress1").text(response.model.HomeAddress1);
            $("#txtAddress2").text(response.model.HomeAddress2);
            $("#ddlStateHome").text(response.model.StateHomeString);
            $("#txtZip").text(response.model.ZipHome);
            $("#ddlRentOwn").text(response.model.RentOwn == '1' ? 'Rent' : response.model.RentOwn == '2' ? 'Own' : '');
            $("#txtMonthlyPayment").text(formatMoney(response.model.MonthlyPayment));
            $("#txtReasonforleaving").text(response.model.Reason);
            $("#txtEmployerName").text(response.model.EmployerName);
            $("#txtJobTitle").text(response.model.JobTitle);
            $("#ddlJobType").text(response.model.JobType == '1' ? 'Permanent' : response.model.JobType == '2' ? 'Contract Basis' : '');
            $("#txtStartDate").text(response.model.StartDateTxt);
            $("#txtAnnualIncome").text(formatMoney(response.model.Income));
            $("#txtAddAnnualIncome").text(formatMoney(response.model.AdditionalIncome));
            $("#txtSupervisiorName").text(response.model.SupervisorName);
            $("#txtSupervisiorPhone").text(formatPhoneFax(response.model.SupervisorPhone));
            $("#txtSupervisiorEmail").text(response.model.SupervisorEmail);
            //$("#txtCountryOffice").val(response.model.OfficeCountry);
            $("#txtofficeAddress1").text(response.model.OfficeAddress1);
            $("#txtofficeAddress2").text(response.model.OfficeAddress2);
            //fillCityListEmployee(response.model.OfficeState);
            setTimeout(function () {
                $("#ddlStateEmployee").find("option[value='" + response.model.OfficeState + "']").attr('selected', 'selected');
            }, 1500);
            setTimeout(function () {
                //$("#ddlCityEmployee").find("option[value='" + response.model.OfficeCity + "']").attr('selected', 'selected');

            }, 2000);
            $("#txtZipOffice").text(response.model.OfficeZip);
            $("#txtRelationship").text(response.model.Relationship);
            $("#txtEmergencyFirstName").text(response.model.EmergencyFirstName);
            $("#txtEmergencyLastName").text(response.model.EmergencyLastName);
            $("#txtEmergencyMobile").text(formatPhoneFax(response.model.EmergencyMobile));
            $("#txtEmergencyHomePhone").text(formatPhoneFax(response.model.EmergencyHomePhone));
            $("#txtEmergencyWorkPhone").text(formatPhoneFax(response.model.EmergencyWorkPhone));
            $("#txtEmergencyEmail").text(response.model.EmergencyEmail);
            $("#txtEmergencyAddress1").text(response.model.EmergencyAddress1);
            $("#txtEmergencyAddress2").text(response.model.EmergencyAddress2);
            $("#ddlStateContact").text(response.model.EmergencyStateHomeString);

            $("#txtEmergencyZip").text(response.model.EmergencyZipHome);
            //17082019 - start
            $("#ddlCityHome").text(response.model.CityHome);
            $("#ddlCityContact").text(response.model.EmergencyCityHome);
            $("#ddlCityEmployee").text(response.model.OfficeCity);
            $("#txtOtherGender").val(response.model.OtherGender);

            $("#txtMoveInDateFrom").text(response.model.MoveInDateFromTxt);
            $("#txtMoveInDateTo").text(response.model.MoveInDateToTxt);
            $("#txtCountry").text(response.model.CountryString);
            $("#txtCountryOffice").text(response.model.OfficeCountryString);
            $("#txtEmergencyCountry").text(response.model.EmergencyCountryString);
            setTimeout(function () {
                $("#txtCountry2").find("option[value='" + response.model.Country2 + "']").attr('selected', 'selected');
            }, 2000);
            //17082019 - end
            if (response.model.IdentityDocument != "") {
                $("#hndHasIdentityFile").val("1");
            } else {
                $("#hndHasIdentityFile").val("0");
            }
            if (response.model.PassportDocument != "") {
                $("#hndHasPassportFile").val("1");
            } else {
                $("#hndHasPassportFile").val("0");
            }
            if (response.model.TaxReturn != "") {
                $("#hndHasTaxReturnFile1").val("1");
            } else {
                $("#hndHasTaxReturnFile1").val("0");
            }
            if (response.model.TaxReturn2 != "") {
                $("#hndHasTaxReturnFile2").val("1");
            } else {
                $("#hndHasTaxReturnFile2").val("0");
            }
            if (response.model.TaxReturn3 != "") {
                $("#hndHasTaxReturnFile3").val("1");
            } else {
                $("#hndHasTaxReturnFile3").val("0");
            }
            //alert(response.model.HaveVehicle + "  " + response.model.HavePet);
            //if (response.model.HaveVehicle == true) {
            //    $("#chkDontHaveVehicle").iCheck('check');
            //}
            //else {
            //    $("#chkDontHaveVehicle").iCheck('uncheck');
            //}
            //if (response.model.HavePet == true) {
            //    $("#chkDontHavePet").iCheck('check');
            //}
            //else {
            //    $("#chkDontHavePet").iCheck('uncheck');
            //}
            //document.getElementById('chkDontHaveVehicle').disabled = true;
            //document.getElementById('chkDontHavePet').disabled = true;
           // $("#hndPassportUploadName").val(response.model.PassportDocument);
            $("#hndPassportUploadName").text(response.model.UploadOriginalPassportName);
            $("#hndOriginalPassportUploadName").val(response.model.UploadOriginalPassportName);
            $("#hndPassportUploadName").data('value', response.model.PassportDocument);
            var resultPassExist = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.PassportDocument);

            if (resultPassExist == true) {
                $("#hndPassportUploadName").prop("href", "/Content/assets/img/PersonalInformation/" + response.model.PassportDocument);
                $("#hndPassportUploadName").prop("download", response.model.UploadOriginalPassportName);
            } else {
                $('#hndPassportUploadName').attr('href', 'JavaScript:Void(0)');
                $('#hndPassportUploadName').attr('onclick', 'fileNotExist()');
            }
            
            //$("#hndIdentityUploadName").text(response.model.UploadOriginalIdentityName);
            $("#hndIdentityUploadName").html("<i class='fa fa-download fa-lg'></i>" + " " + response.model.UploadOriginalIdentityName);
            $("#hndOriginalIdentityUploadName").val(response.model.UploadOriginalIdentityName);
            $("#hndIdentityUploadName").data('value', response.model.IdentityDocument);
            var resultIdentityExist = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.IdentityDocument);

            if (resultIdentityExist == true) {
                $("#hndIdentityUploadName").prop("href", "/Content/assets/img/PersonalInformation/" + response.model.IdentityDocument);
                $("#hndIdentityUploadName").prop("download", response.model.UploadOriginalIdentityName);
            } else {
                $('#hndIdentityUploadName').attr('href', 'JavaScript:Void(0)');
                $('#hndIdentityUploadName').attr('onclick', 'fileNotExist()');
            }
            //$("#hndFileUploadName1").val(response.model.TaxReturn);
            //$("#hndFileUploadName1").text(response.model.UploadOriginalFileName1);
            $("#hndFileUploadName1").html("<i class='fa fa-download fa-lg'></i>" + " " + response.model.UploadOriginalFileName1);
            $("#hndOriginalFileUploadName1").val(response.model.UploadOriginalFileName1);
            $("#hndFileUploadName1").data('value', response.model.TaxReturn);
            var resultTaxReturnExist = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturn);

            if (resultTaxReturnExist == true) {
                $("#hndFileUploadName1").prop("href", "/Content/assets/img/PersonalInformation/" + response.model.TaxReturn);
                $("#hndFileUploadName1").prop("download", response.model.UploadOriginalFileName1);
            } else {
                $('#hndFileUploadName1').attr('href', 'JavaScript:Void(0)');
                $('#hndFileUploadName1').attr('onclick', 'fileNotExist()');
            }
            
           // $("#hndFileUploadName2").val(response.model.TaxReturn2);
            //$("#hndFileUploadName2").text(response.model.UploadOriginalFileName2);
            $("#hndFileUploadName2").html("<i class='fa fa-download fa-lg'></i>" + " " + response.model.UploadOriginalFileName2);
            $("#hndOriginalFileUploadName2").val(response.model.UploadOriginalFileName2);
            $("#hndFileUploadName2").data('value', response.model.TaxReturn2);
            var resultTaxReturn2Exist = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturn2);

            if (resultTaxReturn2Exist == true) {
                $("#hndFileUploadName2").prop("href", "/Content/assets/img/PersonalInformation/" + response.model.TaxReturn2);
                $("#hndFileUploadName2").prop("download", response.model.UploadOriginalFileName2);
            } else {
                $('#hndFileUploadName2').attr('href', 'JavaScript:Void(0)');
                $('#hndFileUploadName2').attr('onclick', 'fileNotExist()');
            }
            
           // $("#hndFileUploadName3").val(response.model.TaxReturn3);
            $("#hndFileUploadName3").html("<i class='fa fa-download fa-lg'></i>" + " " +response.model.UploadOriginalFileName3);
            $("#hndOriginalFileUploadName3").val(response.model.UploadOriginalFileName3);
            $("#hndFileUploadName3").data('value', response.model.TaxReturn3);
            var resultTaxReturn3Exist = doesFileExist('/Content/assets/img/PersonalInformation/' + response.model.TaxReturn3);

            if (resultTaxReturn3Exist == true) {
                $("#hndFileUploadName3").prop("href", "/Content/assets/img/PersonalInformation/" + response.model.TaxReturn3);
                $("#hndFileUploadName3").prop("download", response.model.UploadOriginalFileName3);
            } else {
                $('#hndFileUploadName3').attr('href', 'JavaScript:Void(0)');
                $('#hndFileUploadName3').attr('onclick', 'fileNotExist()');
            }
            
            //To Display File Name On Uploader
            if (response.model.UploadOriginalPassportName != '') {
                $("#fileUploadPassportShow").text(response.model.UploadOriginalPassportName);
            }
            if (response.model.UploadOriginalIdentityName != '') {
                $("#fileUploadIdentityShow").text(response.model.UploadOriginalIdentityName);
            }
            if (response.model.UploadOriginalFileName1 != '') {
                $("#fileUploadTaxReturn1Show").text(response.model.UploadOriginalFileName1);
            }
            if (response.model.UploadOriginalFileName2 != '') {
                $("#fileUploadTaxReturn2Show").text(response.model.UploadOriginalFileName2);
            }
            if (response.model.UploadOriginalFileName3 != '') {
                $("#fileUploadTaxReturn3Show").text(response.model.UploadOriginalFileName3);
            }

            if (response.model.IsPaystub == true) {

                $("#rbtnPaystub").iCheck('check');
            }
            else {
                $("#rbtnFedralTax").iCheck('check');
            }
            $("#ddlIsInter").text(response.model.IsInternational == '0' ? 'Yes' : 'No');

            if ($("#ddlIsInter").text() == 'No') {
                $("#divCountryOfOrigin").removeClass('hidden');
                $("#divSSNNumber").addClass('hidden');
                $("#passportDiv").removeClass('hidden');
            }
            else {
                $("#divCountryOfOrigin").addClass('hidden');
                $("#divSSNNumber").removeClass('hidden');
                $("#passportDiv").addClass('hidden');
            }
            $("#txtCountryOfOrigin").text(response.model.CountryOfOriginString);

            $("#ddlEverBeenEvicted").text(response.model.Evicted == '2' ? 'Yes' : 'No');
            if ($("#ddlEverBeenEvicted").text() == 'Yes') {
                $("#divEverBeenEvictedDetails").removeClass('hidden');
                $("#divEverBeenConvicted").removeClass('hidden');
                $("#divAnyCriminalCharges").removeClass('hidden');
            }
            else {
                $("#divEverBeenEvictedDetails").addClass('hidden');
                $("#divEverBeenConvicted").addClass('hidden');
                $("#divAnyCriminalCharges").addClass('hidden');
            }
            $("#txtEverBeenEvictedDetails").text(response.model.EvictedDetails);
            $("#ddlEverBeenConvicted").text(response.model.ConvictedFelony == '2' ? 'Yes' : 'No');
            if ($("#ddlEverBeenConvicted").text() == 'Yes') {
                $("#divEverBeenConvictedDetails").removeClass('hidden');
            }
            else {
                $("#divEverBeenConvictedDetails").addClass('hidden');
            }
            $("#txtEverBeenConvictedDetails").text(response.model.ConvictedFelonyDetails);
            $("#ddlAnyCriminalCharges").text(response.model.CriminalChargPen == '2' ? 'Yes' : 'No');
            if ($("#ddlAnyCriminalCharges").text() == 'Yes') {
                $("#divAnyCriminalChargesDetails").removeClass('hidden');
            }
            else {
                $("#divAnyCriminalChargesDetails").addClass('hidden');
            }
            $("#txtAnyCriminalChargesDetails").text(response.model.CriminalChargPenDetails);
            $("#ddlDoYouSmoke").text(response.model.DoYouSmoke == '2' ? 'Yes' : 'No');
            $("#ddlReferredByAnotherResident").text(response.model.ReferredResident == '2' ? 'Yes' : 'No');
            if ($("#ddlReferredByAnotherResident").text() == 'Yes') {
                $("#divReferredByAnotherResidentName").removeClass('hidden');
            }
            else {
                $("#divReferredByAnotherResidentName").addClass('hidden');
            }
            $("#txtReferredByAnotherResidentName").text(response.model.ReferredResidentName);
            $("#ddlBrokerOrMerchantReff").text(response.model.ReferredBrokerMerchant == '2' ? 'Yes' : 'No');

            //History of Residence
            $("#txtApartmentCommunity").text(response.model.ApartmentCommunity);
            $("#txtManagementCompany").text(response.model.ManagementCompany);
            $("#txtManagementCompanyPhone").text(formatPhoneFax(response.model.ManagementCompanyPhone));
            $("#ddlProperNoticeLeaseAgreement").text(response.model.IsProprNoticeLeaseAgreement == '1' ? 'Yes' : 'No');
        }
    });
};

var delVehicle = function (vehId) {

    var model = {
        VID: vehId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Vehicle?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Vehicle/DeleteTenantVehicle",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + vehId).remove();
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

var delPet = function (petId) {
    var model = {
        PetID: petId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Pet?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Tenant/Pet/DeleteTenantPet/",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            //$.alert({
                            //    title: "",
                            //    content: "Progress Saved.",
                            //    type: 'blue',
                            //});
                            $('#tr_' + petId).remove();
                            //if ($("#tblPet>tbody tr").length == 1 && $("#hndPetPlaceID").val() == 1) {
                            //    $("#btnAddPet").css("background-color","#B4ADA5").attr("disabled", "disabled");
                            //}
                            //else if ($("#tblPet>tbody tr").length == 2 && $("#hndPetPlaceID").val() == 2) {
                            //    $("#btnAddPet").css("background-color","#B4ADA5").attr("disabled", "disabled");
                            //}
                            //else if ($("#hndPetPlaceID").val() == 0) {
                            //    $("#btnAddPet").css("background-color","#B4ADA5").attr("disabled", "disabled");
                            //}
                            //else {
                            //    $("#btnAddPet").removeAttr("disabled");
                            //}
                            getTenantPetPlaceData();
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

var delApplicant = function (appliId) {
    var model = {
        AID: appliId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Tenant/Applicant/DeleteApplicant/",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#div_' + appliId).remove();
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

}

function saveupdatePaymentResponsibility() {
    var model = new Array();
    $("#tblResponsibilityPay TBODY TR").each(function () {
        var row = $(this);
        var customer = {};
        customer.applicantID = row.attr("data-id");
        customer.moveInPercentage = row.find("td:eq(1) input[type='text']").val();
        customer.moveInCharge = $("#txtpayamt" + customer.applicantID).val();
        customer.monthlyPercentage = row.find("td:eq(2) input[type='text']").val();
        customer.monthlyPayment = $("#txtpayamtMo" + customer.applicantID).val();

        var applicantID = customer.applicantID;
        var moveInPercentage = customer.moveInPercentage;
        var moveInCharge = customer.moveInCharge;
        var monthlyPercentage = customer.monthlyPercentage;
        var monthlyPayment = customer.monthlyPayment;

        model.push({
            ApplicantID: applicantID,
            MoveInPercentage: moveInPercentage,
            MoveInCharge: moveInCharge,
            MonthlyPercentage: monthlyPercentage,
            MonthlyPayment: monthlyPayment
        });
    });
    // console.log(JSON.stringify(model));
    $.ajax({
        url: "/Tenant/Applicant/SaveUpdatePaymentResponsibility/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {


            getApplicantLists();
            //$("#popApplicant").PopupWindow("close");
            $("#popApplicant").modal("hide");
        }


    });

};

var saveupdateApplicantHistory = function () {
    var ahID = $("#hdnAHRId").val();
    var country2 = $("#txtCountry2").val();
    var homeAddress12 = $("#txtAddress12").val();
    var homeAddress22 = $("#txtAddress22").val();
    var stateHome2 = $("#ddlStateHome2").val();
    var cityHome2 = $("#ddlCityHome2").val();
    var zipHome2 = $("#txtZip2").val();
    var rentOwn2 = $("#ddlRentOwn2").val();
    var moveInDateFrom2 = $("#txtMoveInDateFrom2").val();
    var moveInDateTo2 = $("#txtMoveInDateTo2").val();
    var monthlyPayment2 = $("#txtMonthlyPayment2").val();
    var reason2 = $("#txtReasonforleaving2").val();
    var tenantId = $("#hdnOPId").val();

    var model = {
        Country: country2,
        HomeAddress1: homeAddress12,
        HomeAddress2: homeAddress22,
        StateHome: stateHome2,
        CityHome: cityHome2,
        ZipHome: zipHome2,
        RentOwn: rentOwn2,
        MoveInDateFrom: moveInDateFrom2,
        MoveInDateTo: moveInDateTo2,
        MonthlyPayment: monthlyPayment2,
        Reason: reason2,
        TenantID: tenantId,
        AHID: ahID
    };

    $.ajax({
        url: "/ApplyNow/SaveUpdateApplicantHistory",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#popApplicantHistory").modal("hide");
            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue'
            });

            getApplicantHistoryList();
            clearApplicantHistory();
        }
    });
};

var getApplicantHistoryList = function () {

    var model = {
        TenantID: $("#hdnOPId").val()
    };
    $.ajax({
        url: "/ApplyNow/GetApplicantHistoryList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            //console.log(JSON.stringify(response))
            $("#tblAHR>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.AHID + "' data-value='" + elementValue.AHID + "'>";
                html += "<td>" + elementValue.Country + "</td>";
                html += "<td>" + elementValue.HomeAddress1 + "</td>";
                html += "<td>" + elementValue.HomeAddress2 + "</td>";
                html += "<td>" + elementValue.StateHomeTxt + "</td>";
                html += "<td>" + elementValue.CityHome + "</td>";
                html += "<td>" + elementValue.ZipHome + "</td>";

                html += "<td>" + elementValue.MoveInDateFromTxt + "</td>";
                html += "<td>" + elementValue.MoveInDateToTxt + "</td>";
                html += "<td>" + elementValue.MonthlyPayment + "</td>";

                html += "<td class='text-center'>";
                html += "<button style='background: transparent;' id='updateAHRInfo' onclick='getApplicantHistoryInfo(" + elementValue.AHID + ")'><span class='fa fa-eye' ></span></button>";
                html += "</tr>";
                $("#tblAHR>tbody").append(html);
            });
        }
    });
};

var getApplicantHistoryInfo = function (id) {
    var model = {
        AHID: id
    };
    $.ajax({
        url: '/ApplyNow/GetApplicantHistoryDetails/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#popApplicantHistory").modal("show");
            $("#hdnAHRId").val(response.model.AHID);
            $("#txtCountry2").text(response.model.CountryString);

            $("#txtAddress12").text(response.model.HomeAddress1);
            $("#txtAddress22").text(response.model.HomeAddress2);
            $("#ddlStateHome2").text(response.model.StateString);
            $("#ddlCityHome2").text(response.model.CityHome);
            $("#txtZip2").text(response.model.ZipHome);
            $("#ddlRentOwn2").text(response.model.RentOwnString);
            $("#txtMoveInDateFrom2").text(response.model.MoveInDateFromTxt);
            $("#txtMoveInDateTo2").text(response.model.MoveInDateToTxt);
            $("#txtMonthlyPayment2").text(response.model.MonthlyPayment);
            $("#txtReasonforleaving2").text(response.model.Reason);
            $("#txtApartmentCommunity2").text(response.model.ApartmentCommunity);
            $("#txtManagementCompany2").text(response.model.ManagementCompany);
            $("#txtManagementCompanyPhone2").text(response.model.ManagementCompanyPhone);
            $("#ddlProperNoticeLeaseAgreement2").text(response.model.IsProprNoticeLeaseAgreement == '2' ? 'Yes' : 'No');

        }
    });
};

var clearApplicantHistory = function () {
    $("#hdnAHRId").val(0);
    var country2 = $("#txtCountry2").val();
    var homeAddress12 = $("#txtAddress12").val("");
    var homeAddress22 = $("#txtAddress22").val("");
    var stateHome2 = $("#ddlStateHome2").val(0);
    var cityHome2 = $("#ddlCityHome2").val("");
    var zipHome2 = $("#txtZip2").val("");
    var rentOwn2 = $("#ddlRentOwn2").val(0);
    var moveInDateFrom2 = $("#txtMoveInDateFrom2").val("");
    var moveInDateTo2 = $("#txtMoveInDateTo2").val("");
    var monthlyPayment2 = $("#txtMonthlyPayment2").val("");
    var reason2 = $("#txtReasonforleaving2").val("");
};

var delApplicantHistory = function (aHRID) {
    var model = {
        AHID: aHRID
    };

    $.alert({
        title: "",
        content: "Are you sure to Applicant History?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: '/ApplyNow/DeleteApplicantHistory/',
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {

                            $('#tr_' + aHRID).remove();
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
var addEmailArray = [];
var addEmail = function (email) {

    addEmailArray.push({ AppEmail: email });
}
var sendCoappEmail = function () {
    console.log(addEmailArray)
    var model = {
        lstemailsend: addEmailArray
    };

    $.alert({
        title: "",
        content: "Are you sure to send Email?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ApplyNow/SendCoappEmail",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: "Email Send Successfully",
                                type: 'blue',
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
};

var haveVehicle = function () {
    var ProspectID = $("#hdnOPId").val();
    var vehicleValue = '';

    if ($("#chkDontHaveVehicle").is(":checked")) {
        vehicleValue = true;
    }

    else if ($("#chkDontHaveVehicle").is(":not(:checked)")) {
        vehicleValue = false;
    }

    var model = {
        id: ProspectID,
        vehicleValue: vehicleValue
    };
    $.ajax({
        url: '/ApplyNow/HaveVehicle',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

        }
    });
};

var havePet = function () {
    var ProspectID = $("#hdnOPId").val();
    var petValue = '';

    if ($("#chkDontHavePet").is(":checked")) {
        petValue = true;
    }

    else if ($("#chkDontHavePet").is(":not(:checked)")) {
        petValue = false;
    }

    var model = {
        id: ProspectID,
        PetValue: petValue
    };
    $.ajax({
        url: '/ApplyNow/HavePet',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

        }
    });
};

var onFocus = function () {

    $("#txtApplicantPhone").focusout(function () { $("#txtApplicantPhone").val(formatPhoneFax($("#txtApplicantPhone").val())); })
        .focus(function () {
            $("#txtApplicantPhone").val(unformatText($("#txtApplicantPhone").val()));
        });

    $("#txtMobileNumber").focusout(function () { $("#txtMobileNumber").val(formatPhoneFax($("#txtMobileNumber").val())); })
        .focus(function () {
            $("#txtMobileNumber").val(unformatText($("#txtMobileNumber").val()));
        });

    $("#txtSupervisiorPhone").focusout(function () { $("#txtSupervisiorPhone").val(formatPhoneFax($("#txtSupervisiorPhone").val())); })
        .focus(function () {
            $("#txtSupervisiorPhone").val(unformatText($("#txtSupervisiorPhone").val()));
        });

    $("#txtEmergencyMobile").focusout(function () { $("#txtEmergencyMobile").val(formatPhoneFax($("#txtEmergencyMobile").val())); })
        .focus(function () {
            $("#txtEmergencyMobile").val(unformatText($("#txtEmergencyMobile").val()));
        });

    $("#txtEmergencyHomePhone").focusout(function () { $("#txtEmergencyHomePhone").val(formatPhoneFax($("#txtEmergencyHomePhone").val())); })
        .focus(function () {
            $("#txtEmergencyHomePhone").val(unformatText($("#txtEmergencyHomePhone").val()));
        });

    $("#txtEmergencyWorkPhone").focusout(function () { $("#txtEmergencyWorkPhone").val(formatPhoneFax($("#txtEmergencyWorkPhone").val())); })
        .focus(function () {
            $("#txtEmergencyWorkPhone").val(unformatText($("#txtEmergencyWorkPhone").val()));
        });

    $("#txtMonthlyPayment").focusout(function () { $("#txtMonthlyPayment").val(formatMoney($("#txtMonthlyPayment").val())); })
        .focus(function () {
            $("#txtMonthlyPayment").val(unformatText($("#txtMonthlyPayment").val()));
        });

    $("#txtMonthlyPayment2").focusout(function () { $("#txtMonthlyPayment2").val(formatMoney($("#txtMonthlyPayment2").val())); })
        .focus(function () {
            $("#txtMonthlyPayment2").val(unformatText($("#txtMonthlyPayment2").val()));
        });

    $("#txtAnnualIncome").focusout(function () { $("#txtAnnualIncome").val(formatMoney($("#txtAnnualIncome").val())); })
        .focus(function () {
            $("#txtAnnualIncome").val(unformatText($("#txtAnnualIncome").val()));
        });

    $("#txtAddAnnualIncome").focusout(function () { $("#txtAddAnnualIncome").val(formatMoney($("#txtAddAnnualIncome").val())); })
        .focus(function () {
            $("#txtAddAnnualIncome").val(unformatText($("#txtAddAnnualIncome").val()));
        });

    $("#txtSSNNumber").focusout(function () {
        var ssn = $(this).val();
        $(this).data("value", ssn);
        if (ssn.length > 4) {
            $(this).val("***-**-" + ssn.substr(ssn.length - 5, 4))
        }
    })
        .focus(function () {
            $(this).val($(this).data("value"));
        });
}

function formatPhoneFax(phonefax) {
    if (phonefax == null)
        phonefax = "";
    phonefax = phonefax.replace(/[^0-9]/g, '');
    if (phonefax.length == 0)
        return phonefax;

    return '(' + phonefax.substring(0, 3) + ') ' + phonefax.substring(3, 6) + (phonefax.length > 6 ? '-' : '') + phonefax.substring(6);
}

function unformatText(text) {
    if (text == null)
        text = "";

    return text.replace(/[^\d\.]/g, '');
}

function formatSSN(ssn) {
    if (ssn == null)
        ssn = "";
    ssn = ssn.replace(/[^0-9]/g, '');
    if (ssn.length == 0)
        return ssn;

    return ssn.substring(0, 3) + (ssn.length > 3 ? '-' : '') + ssn.substring(3, 5) + (ssn.length > 5 ? '-' : '') + ssn.substring(5);
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

$(function () {
    $("#txtIDNumber").blur(function () {
        cCardNum = $(this).val();
        $(this).data("value", cCardNum);
        if (cCardNum.length > 4) {
            $(this).val(("*".repeat(cCardNum.length - 4) + cCardNum.substr(cCardNum.length - 4, 4)));
        }
    }).focus(function () {
        $(this).val($(this).data("value"));
    });

});


var getDocuDoc = function (envelopeID) {

  
    var param = { LeaseId: envelopeID };
    $.ajax({
        url: "/CheckList/GetLeaseDocBlumoonAdm",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
           // $("#divLoader").hide();
           // window.open("/Content/assets/img/Document/" + response.result, "popupWindow", "width=900,height=600,scrollbars=yes");
            //signLeaseDoc(response.result);
        }
    });
}
function gotoList() {
   
    window.location.href = "/Admin/ProspectVerification";
}
var fillMarketSourceDDL = function () {
    $.ajax({
        url: '/Admin/ProspectManagement/GetDdlMarketSourceList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //$("#ddlMarketSource").text(elementValue.Advertiser);
            //$("#ddlMarketSource").empty();
            //$("#ddlMarketSource").append("<option value='0'>-- Select Market Source --</option>");
            //$.each(response.model, function (index, elementValue) {
            //    $("#ddlMarketSource").append("<option value=" + elementValue.AdID + ">" + elementValue.Advertiser + "</option>");
            //});

        }
    });
}
var onlinePToTenant = function () {
    var msg = "";
    var tenantID = $("#hndTenantID").val();
    var ProspectID = $("#hdnOPId").val();
    var propertyID = "8";
    var unitID = $("#hndUID").val();
    var isInternational = $("#ddlIsInter").text();
    var FirstName = $("#txtFirstNamePersonal").text();
    var MiddleInitial = $("#txtMiddleInitial").text();
    var LastName = $("#txtLastNamePersonal").text();
    var DateOfBirth = $("#txtDateOfBirth").text();
    var Gender = $("#ddlGender").text();
    var Email = $("#txtEmailNew").text();
    var Mobile = unformatText($("#txtMobileNumber").text());
    var PassportNumber = $("#txtPassportNum").text();
    var CountryIssuance = $("#txtCOI").text();
    var DateIssuance = $("#txtDateOfIssuance").text();
    var DateExpire = $("#txtDateOfExpiration").text();
    var IDType = $("#ddlDocumentTypePersonal").text();
    var State = $("#ddlStatePersonal").text();
    var IDNumber = $("#txtIDNumber").data("value");
    var SSNNumber = $("#txtSSNNumber").data("value");
    var Country = $("#txtCountry").text();
    var HomeAddress1 = $("#txtAddress1").text();
    var HomeAddress2 = $("#txtAddress2").text();
    var StateHome = $("#ddlStateHome").text();
    var CityHome = $("#ddlCityHome").text();
    var ZipHome = $("#txtZip").text();
    var RentOwn = $("#ddlRentOwn").text();
    var MoveInDateFrom = $("#txtMoveInDateFrom").text();
    var MoveInDateTo = $("#txtMoveInDateTo").text();
    var MonthlyPayment = unformatText($("#txtMonthlyPayment").text());
    var Reason = $("#txtReasonforleaving").text();

    var Country2 = $("#txtCountry2").text();
    var HomeAddress12 = $("#txtAddress12").text();
    var HomeAddress22 = $("#txtAddress22").text();
    var StateHome2 = $("#ddlStateHome2").text();
    var CityHome2 = $("#ddlCityHome2").text();
    var ZipHome2 = $("#txtZip2").text();
    var RentOwn2 = $("#ddlRentOwn2").text();
    var MoveInDateFrom2 = $("#txtMoveInDateFrom2").text();
    var MoveInDateTo2 = $("#txtMoveInDateTo2").text();
    var MonthlyPayment2 = unformatText($("#txtMonthlyPayment2").text());
    var Reason2 = $("#txtReasonforleaving2").text();

    var isAdditionalRHistory = $("#ddladdHistory").text();

    var EmployerName = $("#txtEmployerName").text();
    var JobTitle = $("#txtJobTitle").text();
    var JobType = $("#ddlJobType").text();
    var StartDate = $("#txtStartDate").text();
    var Income = unformatText($("#txtAnnualIncome").text());
    var AdditionalIncome = unformatText($("#txtAddAnnualIncome").text());
    var SupervisorName = $("#txtSupervisiorName").text();
    var SupervisorPhone = unformatText($("#txtSupervisiorPhone").text());
    var SupervisorEmail = $("#txtSupervisiorEmail").text();
    var OfficeCountry = $("#txtCountryOffice").text();
    var OfficeAddress1 = $("#txtofficeAddress1").text();
    var OfficeAddress2 = $("#txtofficeAddress2").text();
    var OfficeState = $("#ddlStateEmployee").text();
    var OfficeCity = $("#ddlCityEmployee").text();
    var OfficeZip = $("#txtZipOffice").text();
    var Relationship = $("#txtRelationship").text();
    var EmergencyFirstName = $("#txtEmergencyFirstName").text();
    var EmergencyLastName = $("#txtEmergencyLastName").text();
    var EmergencyMobile = unformatText($("#txtEmergencyMobile").text());
    var EmergencyHomePhone = unformatText($("#txtEmergencyHomePhone").text());
    var EmergencyWorkPhone = unformatText($("#txtEmergencyWorkPhone").text());
    var EmergencyEmail = $("#txtEmergencyEmail").text();
    var EmergencyCountry = $("#txtEmergencyCountry").text();
    var EmergencyAddress1 = $("#txtEmergencyAddress1").text();
    var EmergencyAddress2 = $("#txtEmergencyAddress2").text();
    var EmergencyStateHome = $("#ddlStateContact").text();
    var EmergencyCityHome = $("#ddlCityContact").text();
    var EmergencyZipHome = $("#txtEmergencyZip").text();
    var OtherGender = $("#txtOtherGender").text();

    var fileUpload1 = $("#hndFileUploadName1").data('value');
    var originalFileUpload1 = $("#hndOriginalFileUploadName1").val();
    var fileUpload2 = $("#hndFileUploadName2").data('value');
    var originalFileUpload2 = $("#hndOriginalFileUploadName2").val();
    var fileUpload3 = $("#hndFileUploadName3").data('value');
    var originalFileUpload3 = $("#hndOriginalFileUploadName3").val();
    var filePassport = $("#hndPassportUploadName").data('value');
    var originalFilePassport = $("#hndOriginalPassportUploadName").val();
    var fileIdentity = $("#hndIdentityUploadName").data('value');
    var originalFileIdentity = $("#hndOriginalIdentityUploadName").val();
    var proratedRent = unformatText($('#lblProratedRent').text());
    var fTotal = unformatText($('#ftotal').text());
    if (!OtherGender) {
        OtherGender = $("#txtOtherGender").val(" ");
    }

    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return;
    }
    $formData = new FormData();
    $formData.append('ProspectID', ProspectID);
    $formData.append('PropertyID', propertyID);
    $formData.append('UnitID', unitID);
    $formData.append('FirstName', FirstName);
    $formData.append('MiddleInitial', MiddleInitial);
    $formData.append('LastName', LastName);
    $formData.append('DateOfBirth', DateOfBirth);
    $formData.append('Gender', Gender);
    $formData.append('Email', Email);
    $formData.append('Mobile', Mobile);
    $formData.append('PassportNumber', PassportNumber);
    $formData.append('CountryIssuance', CountryIssuance);
    $formData.append('DateIssuance', DateIssuance);
    $formData.append('DateExpire', DateExpire);
    $formData.append('IDType', IDType);
    $formData.append('State', State);
    $formData.append('IDNumber', IDNumber);
    $formData.append('Country', Country);
    $formData.append('HomeAddress1', HomeAddress1);
    $formData.append('HomeAddress2', HomeAddress2);
    $formData.append('StateHome', StateHome);
    $formData.append('CityHome', CityHome);
    $formData.append('ZipHome', ZipHome);
    $formData.append('RentOwn', RentOwn);
    $formData.append('MoveInDateFrom', MoveInDateFrom);
    $formData.append('MoveInDateTo', MoveInDateTo);
    $formData.append('MonthlyPayment', MonthlyPayment);
    $formData.append('Reason', Reason);
    $formData.append('Country2', Country2);
    $formData.append('HomeAddress12', HomeAddress12);
    $formData.append('HomeAddress22', HomeAddress22);
    $formData.append('StateHome2', StateHome2);
    $formData.append('CityHome2', CityHome2);
    $formData.append('ZipHome2', ZipHome2);

    $formData.append('RentOwn2', RentOwn2);
    $formData.append('MoveInDateFrom2', MoveInDateFrom2);
    $formData.append('MoveInDateTo2', MoveInDateTo2);
    $formData.append('MonthlyPayment2', MonthlyPayment2);
    $formData.append('Reason2', Reason2);
    $formData.append('EmployerName', EmployerName);
    $formData.append('JobTitle', JobTitle);
    $formData.append('JobType', JobType);
    $formData.append('StartDate', StartDate);
    $formData.append('Income', Income);
    $formData.append('AdditionalIncome', AdditionalIncome);
    $formData.append('SupervisorName', SupervisorName);
    $formData.append('SupervisorPhone', SupervisorPhone);
    $formData.append('SupervisorEmail', SupervisorEmail);
    $formData.append('OfficeCountry', OfficeCountry);
    $formData.append('OfficeAddress1', OfficeAddress1);
    $formData.append('OfficeAddress2', OfficeAddress2);
    $formData.append('OfficeState', OfficeState);
    $formData.append('OfficeCity', OfficeCity);
    $formData.append('OfficeZip', OfficeZip);
    $formData.append('Relationship', Relationship);
    $formData.append('EmergencyFirstName', EmergencyFirstName);
    $formData.append('EmergencyLastName', EmergencyLastName);
    $formData.append('EmergencyMobile', EmergencyMobile);
    $formData.append('EmergencyHomePhone', EmergencyHomePhone);
    $formData.append('EmergencyWorkPhone', EmergencyWorkPhone);
    $formData.append('EmergencyEmail', EmergencyEmail);
    $formData.append('EmergencyCountry', EmergencyCountry);
    $formData.append('EmergencyAddress1', EmergencyAddress1);
    $formData.append('EmergencyAddress2', EmergencyAddress2);
    $formData.append('EmergencyStateHome', EmergencyStateHome);
    $formData.append('EmergencyCityHome', EmergencyCityHome);

    $formData.append('EmergencyZipHome', EmergencyZipHome);
    $formData.append('IsInternational', isInternational);
    $formData.append('IsAdditionalRHistory', isAdditionalRHistory);
    $formData.append('OtherGender', OtherGender);
    $formData.append('SSN', SSNNumber);

    $formData.append('TaxReturn', fileUpload1);
    $formData.append('UploadOriginalFileName1', originalFileUpload1);
    $formData.append('TaxReturn2', fileUpload2);
    $formData.append('UploadOriginalFileName2', originalFileUpload2);
    $formData.append('TaxReturn3', fileUpload3);
    $formData.append('UploadOriginalFileName3', originalFileUpload3);

    $formData.append('PassportDocument', filePassport);
    $formData.append('UploadOriginalPassportName', originalFilePassport);
    $formData.append('IdentityDocument', fileIdentity);
    $formData.append('UploadOriginalIdentityName', originalFileIdentity);

    $formData.append('MoveInCharges', fTotal);
    $formData.append('Prorated_Rent', proratedRent);
    $formData.append('AdministrationFee', $('#lblAdminFees').text());
    $formData.append('VehicleRegistration', $('#lblVehicleFees').text());
    $formData.append('LeaseTerm', $('#lblLease4').text());
    $formData.append('MonthlyRent', $('#lbltotalAmountSumm').text());
    $formData.append('ProRemainingday', remainingday);
    $formData.append('ProNumberOfDays', numberOfDays);

    
    if ($("#rbtnPaystub").is(":checked")) {
        $formData.append('IsPaystub', true);
    }
    else if ($("#rbtnFedralTax").is(":checked")) {
        $formData.append('IsPaystub', false);
    }
    if ($("#chkDontHaveVehicle").is(":checked")) {
        $formData.append('HaveVehicle', true);
    }
    else {
        $formData.append('HaveVehicle', false);
    }
    if ($("#chkDontHavePet").is(":checked")) {
        $formData.append('HavePet', true);
    }
    else {
        $formData.append('HavePet', false);
    }


    $.ajax({
        url: '/Admin/Tenant/OnlinePToTenant',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: 'Tenant Created Successfully',
                type: 'blue'
            })
           
            window.location.href = "/Admin/ProspectVerification/";
        }
    });
};

var fileNotExist = function () {
    $.alert({
        title: "Alert!",
        content: 'File Not Found',
        type: 'red'
    })
}

var getVehicleInfo = function (id) {
    $("#popVehicle").modal("show");
    var model = {
        VehicleId: id
    };
    $.ajax({
        url: '/Tenant/Vehicle/GetVehicleInfo/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#hndVehicleID").val(response.model.Vehicle_ID);
            $("#txtVehicleOwnerName").text(response.model.OwnerName);
            $("#txtVehicleMake").text(response.model.Make);
            $("#txtVehicleModel").text(response.model.VModel);
            $("#txtVehicleColor").text(response.model.Color);
            $("#txtVehicleLicence").text(response.model.License);
            $("#txtVehicleNote").text(response.model.Notes);
            $("#ddlVehicleyear").text(response.model.Year);
            $("#ddlVState").text(response.model.StateString);
            $("#txtVehicleTag").text(response.model.Tag);
            $("#txtVehicleParkingSpace").text(response.model.ParkingName);
            var result = doesFileExist('/Content/assets/img/VehicleRegistration/' + response.model.VehicleRegistration);

            if (result == true) {
                $('#dwnldVehicleRegistration').attr('href', '/Content/assets/img/VehicleRegistration/' + response.model.VehicleRegistration);
                $('#dwnldVehicleRegistration').attr('download', response.model.OriginalVehicleRegistation);
                $('#dwnldVehicleRegistration').html("<i class='fa fa-download'></i> " + response.model.OriginalVehicleRegistation);
            } else {
                $('#dwnldVehicleRegistration').html("<i class='fa fa-download'></i> " + response.model.OriginalVehicleRegistation);
                $('#dwnldVehicleRegistration').attr('href', 'JavaScript:Void(0)');
                $('#dwnldVehicleRegistration').attr('onclick', 'fileNotExist()');
            }
        }
    });
};

var SaveScreeningStatus = function () {
    var emil = $("#txtEmail").text();
    var model = {
        Email: emil, ProspectId: $("#hdnOPId").val(), Status: $("#ddlStatus").val()
    };

    $.alert({
        title: "",
        content: "Are you sure to send Confirmation?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ProspectVerification/SaveScreeningStatus",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: "Confirmation Saved and Email Send Successfully",
                                type: 'blue',
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
};

var getEmpHistoryListPropVari = function () {
    $("#divLoader").show();
    var model = {
        TenantID: $("#hdnOPId").val()
    };
    $.ajax({
        url: "/ApplyNow/GetEmployerHistory",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            //console.log(JSON.stringify(response))
            $("#tblEIHEmpDetailAdm>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='tr_" + elementValue.HEIID + "' data-value='" + elementValue.HEIID + "'>";
                html += "<td>" + elementValue.EmployerName + "</td>";
                html += "<td>" + elementValue.JobTitle + "</td>";
                html += "<td>" + elementValue.JobTypeName + "</td>";
                html += "<td>" + elementValue.StartDateString + "</td>";
                html += "<td>" + elementValue.TerminationDateString + "</td>";
                html += "<td>" + formatMoney(elementValue.AnnualIncome) + "</td>";
                html += "<td>" + elementValue.CountryName + "</td>";
                html += "<td>" + elementValue.StateName + "</td>";
                html += "<td>" + elementValue.City + "</td>";
                html += "<td class='text-center'>";
                html += "<button style='background: transparent;' id='updateAHRInfo' onclick='getEmpHistoryInfoProsVerif(" + elementValue.HEIID + ")'><span class='fa fa-eye' ></span></button>";
                html += "</tr>";
                $("#tblEIHEmpDetailAdm>tbody").append(html);
            });
            $("#divLoader").hide();
        }
    });
};

var getEmpHistoryInfoProsVerif = function (id) {
    $("#divLoader").show();
    var model = {
        HEIID: id
    };
    $.ajax({
        url: "/ApplyNow/EditEmployerHistory",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $('#lblEmployerNameHEI').text(response.model.EmployerName);
            $('#lblJobTitleHEI').text(response.model.JobTitle);
            $('#lblJobTypeHEI').text(response.model.JobTypeName);
            $('#lblStartDateHEI').text(response.model.StartDateString);
            $('#lblTerminationDateHEI').text(response.model.TerminationDateString);
            var anuInc = formatMoney(response.model.AnnualIncome);
            $('#lblAnnualIncomeHEI').text(anuInc);
            var addAnuInc = formatMoney(response.model.AddAnnualIncome);
            $('#lblAddAnnualIncomeHEI').text(addAnuInc);
            $('#lblReasonOfTerminationHEI').text(response.model.TerminationReason);
            $('#lblSupervisiorNameHEI').text(response.model.SupervisorName);
            var svPhone = formatPhoneFax(response.model.SupervisorPhone);
            $('#lblSupervisiorPhoneHEI').text(svPhone);
            $('#lblSupervisiorEmailHEI').text(response.model.SupervisorEmail);
            $('#lblCountryOfficeHEI').text(response.model.CountryName);
            $('#lblofficeAddress1HEI').text(response.model.Address1);
            $('#lblofficeAddress2HEI').text(response.model.Address2);
            $('#lblStateEmployeeHEI').text(response.model.StateName);
            $('#lblCityEmployeeHEI').text(response.model.City);
            $('#lblZipOfficeHEI').text(response.model.Zip);
            $('#popHistoryEmpAndIncomeProspVerif').modal('show');
            $("#divLoader").hide();
        }
    });
};

var downloadLeaseDocument = function () {
    $("#divLoader").show();
    var param = { uid: $("#hdnUserId").val() };
    $.ajax({
        url: "/CheckList/DownloadLeaseDocBlumoon",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            var hyperlink = document.createElement('a');
            hyperlink.href = "/Content/assets/img/Document/LeaseDocument_" + response.LeaseId + ".pdf";
            hyperlink.target = '_blank';
            hyperlink.download = "LeaseDocument_" + response.LeaseId + ".pdf";

            (document.body || document.documentElement).appendChild(hyperlink);
            hyperlink.onclick = function () {
                (document.body || document.documentElement).removeChild(hyperlink);
            };
            var mouseEvent = new MouseEvent('click', {
                view: window,
                bubbles: true,
                cancelable: true
            });
            hyperlink.dispatchEvent(mouseEvent);
            if (!navigator.mozGetUserMedia) {
                window.URL.revokeObjectURL(hyperlink.href);
            }

        }
    });
}
var SaveSignedStatus = function () {
    var emil = $("#txtEmail").text();
    var model = {
        Email: emil, ProspectId: $("#hdnOPId").val(), Status:"Signed"
    };

    $.alert({
        title: "",
        content: "Are you sure to send Confirmation?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ProspectVerification/SaveScreeningStatus",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: "Confirmation Saved and Email Send Successfully",
                                type: 'blue',
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
};
var getSignedLists = function (userid) {
    var model = {
        TenantID: userid
    };
    $.ajax({
        url: "/CheckList/GetSignedList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#tblLeasedSign>tbody").empty();
            var isSignedAll = 0;
            var isLeaseExecuted = 0;
            $("#hndIsSignedByAll").val(0);
            if (response.model.length > 0) {
                $.each(response.model, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.ESID + ">";
                    html += "<td>" + elementValue.ApplicantName + "</td>";
                    html += "<td>" + elementValue.Email + "</td>";
                    html += "<td>" + elementValue.DateSigned + "</td>";
                    html += "<td><button type='button' class='btn btn-primary' onclick='SendReminderEmail(2, " + elementValue.ApplicantID + ")' " + (elementValue.IsSigned == 1 ? "disabled='disabled'" : "") + ">Send Reminder to Sign Lease Document</button></td>";
                    html += "</tr>";
                    isSignedAll = elementValue.IsSignedAll;
                    isLeaseExecuted = elementValue.IsLeaseExecuted;
                    $("#tblLeasedSign>tbody").append(html);
                });
                if (isSignedAll == 1) {
                    $("#hndIsSignedByAll").val(1);
                    if (isLeaseExecuted == 0) {
                        $("#btnExecuteLease").removeAttr("disabled");
                        $("#btnupSignSts").attr("disabled", "disabled");
                        $("#btnExecuteLease").attr("data-target","#popExecuteLease");
                    }
                    else {
                        $("#btnupSignSts").removeAttr("disabled");
                        $("#btnExecuteLease").attr("disabled", "disabled");
                        $("#btnExecuteLease").removeAttr("data-target");
                    }
                    $("#btnleaseDownl").removeAttr("disabled");
                    $("#btnSendRemSign").attr("disabled", "disabled");
                }
            }
            else {
                var html = "<tr><td colspan='4'><h3 class='panel-title'>Application Feed Not Paid</h3></td></tr>    ";
                $("#tblLeasedSign>tbody").append(html);
            }
        }
    });
}
var SendReminderEmail = function (remtype, applicantID) {

    var model = {
        ProspectId: $("#hdnOPId").val(), RemType: remtype, ApplicantID: applicantID

    };

    $.alert({
        title: "",
        content: "Are you sure to send Reminder Email?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $("#divLoader").show();
                    $.ajax({
                        url: "/ProspectVerification/SendReminderEmail",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $("#divLoader").hide();
                            $.alert({
                                title: "",
                                content: "Reminder Email Sent Successfully",
                                type: 'blue',
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
};

var refreshStatuses = function () {
    var model = {
        ProspectId: $("#hdnOPId").val()
    };
    $("#divLoader").show();
    $.ajax({
        url: "/ProspectVerification/RefreshStatuses",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#divLoader").hide();
            getTransationLists($("#hdnUserId").val());
            getSignedLists($("#hdnOPId").val());
        }
    });
};
var fillUnitParkingList = function () {
    $("#divLoader").show();
    var model = { UID: $("#hndUID").val() };
    $.ajax({
        url: '/Parking/GetUnitParkingList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        dataType: "json",
        success: function (response) {

            if ($.trim(response.error) != "") {
            } else {
                $("#lblParkSpace").text("");
                $.each(response, function (index, elementValue) {
                    var html = "";
                    html += "<span style='text-decoration:underline; font - weight:bold;'>  #" + elementValue.ParkingName + " </span>";
                    $("#lblParkSpace").append(html);
                });
            }
            $("#divLoader").hide();
        }
    });
}