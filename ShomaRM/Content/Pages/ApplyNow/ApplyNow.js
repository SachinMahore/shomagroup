var remainingday = 0;
var numberOfDays = 0;
var QuoteExpires = "";
$(document).ready(function () {
    onFocusApplyNow();
    localStorage.removeItem("CheckReload");
    fillMarketSourceDDLA();

    $("#popRentalQualification").modal("hide");
    checkExpiry();
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

    document.getElementById('fileUploadTaxReturn1').onchange = function () {
        var fileUploadTaxReturn1Bool = restrictFileUpload($(this).val());
        if (fileUploadTaxReturn1Bool == true) {
            taxReturnFileUpload1();
        }
        else {
            document.getElementById('fileUploadTaxReturn1').value = '';
            $('#fileUploadTaxReturn1Show').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
    document.getElementById('fileUploadTaxReturn2').onchange = function () {
        var fileUploadTaxReturn2Bool = restrictFileUpload($(this).val());
        if (fileUploadTaxReturn2Bool == true) {
            taxReturnFileUpload2();
        }
        else {
            document.getElementById('fileUploadTaxReturn2').value = '';
            $('#fileUploadTaxReturn2Show').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
    document.getElementById('fileUploadTaxReturn3').onchange = function () {
        var fileUploadTaxReturn3Bool = restrictFileUpload($(this).val());
        if (fileUploadTaxReturn3Bool == true) {
            taxReturnFileUpload3();
        }
        else {
            document.getElementById('fileUploadTaxReturn3').value = '';
            $('#fileUploadTaxReturn3Show').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
    document.getElementById('fileUploadPassport').onchange = function () {
        var fileUploadPassportBool = restrictFileUpload($(this).val());
        if (fileUploadPassportBool == true) {
            uploadPassport();
        }
        else {
            document.getElementById('fileUploadPassport').value = '';
            $('#fileUploadPassportShow').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
    document.getElementById('fileUploadIdentity').onchange = function () {
        var fileUploadIdentityBool = restrictFileUpload($(this).val());
        if (fileUploadIdentityBool == true) {
            uploadIdentityDocument();
        }
        else {
            document.getElementById('fileUploadIdentity').value = '';
            $('#fileUploadIdentityShow').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
        
    };

    if ($("#chkAgreeTermsPolicy").is(":checked")) {
        $("#policyStart").attr("disabled", true);
        InnerPolicyCheck();
    }
    else if ($("#chkAgreeTermsPolicy").is(":not(:checked)")) {
        $("#policyStart").attr("disabled", true);
        $("#popRentalQualification").modal("hide");
        InnerPolicyCheck();
    }

    $("#chkAgreeTermsPolicy").on('ifChanged', function (event) {
        if ($("#chkAgreeTermsPolicy").is(":checked")) {            
            InnerPolicyCheck();
            $("#popRentalQualification").modal("show");
        }
        else if ($("#chkAgreeTermsPolicy").is(":not(:checked)")) {
            $("#policyStart").attr("disabled", true);
            $("#popRentalQualification").modal("hide");
            InnerPolicyCheck();
        }
    });

    $("#chkRentalQual,#chkRentalPolicy").on('ifChanged', function (event) {
        InnerPolicyCheck();
    });

    function InnerPolicyCheck() {

        if ($("#chkRentalQual").is(":checked") && $("#chkRentalPolicy").is(":checked")) {
            $("#policyStart").attr("disabled", false);
            $("#popRentalQualification").modal("hide");
        }
        else if ($("#chkRentalQual").is(":not(:checked)") || $("#chkRentalPolicy").is(":not(:checked)")) {
            $("#policyStart").attr("disabled", true);
        }
    }

    if ($("#chkDontHaveVehicle").is(":checked")) {
        $("#btnAddVehicle").attr("disabled", true);
        $("#btnAddVehicle").css("background-color", "#b4ada5");
    }

    else if ($("#chkDontHaveVehicle").is(":not(:checked)")) {
        $("#btnAddVehicle").attr("disabled", false);
        $("#btnAddVehicle").removeAttr("style");
    }

    $('input[type=checkbox]').on('ifChanged', function (event) {
        if ($("#chkDontHaveVehicle").is(":checked")) {
            $("#btnAddVehicle").attr("disabled", true);
            $("#btnAddVehicle").css("background-color", "#b4ada5");
            deleteVehiclesListOnCheck();
            haveVehicle();
        }
        else if ($("#chkDontHaveVehicle").is(":not(:checked)")) {
            $("#btnAddVehicle").attr("disabled", false);
            $("#btnAddVehicle").removeAttr("style");
            haveVehicle();
        }
    });

    if ($("#chkDontHavePet").is(":checked")) {
        $("#btnAddPet").prop("disabled", true);
        $("#btnAddPet").css("background-color", "#b4ada5");
    }

    else if ($("#chkDontHavePet").is(":not(:checked)")) {
        $("#btnAddPet").prop("disabled", false);
        $("#btnAddPet").removeAttr("style");
    }

    $('input[type=checkbox]').on('ifChanged', function (event) {
        if ($("#chkDontHavePet").is(":checked")) {
            $("#btnAddPet").prop("disabled", true);
            $("#btnAddPet").css("background-color", "#b4ada5");
            havePet();
        }
        else if ($("#chkDontHavePet").is(":not(:checked)")) {
            $("#btnAddPet").prop("disabled", false);
            $("#btnAddPet").removeAttr("style");
            havePet();
        }
    });

    $("#ddlRoom").on("change", function () {
        getPropertyModelUnitList();
    });
    $("#ddlPrice").on("change", function () {
        getPropertyModelUnitList();
    });
    $("#txtDate").on("change", function () {
        if ($("#txtDate").val() != "") {
            getPropertyModelUnitList();
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
    //17082019 - start CChange   
    $("#txtCountry").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillStateDDL_Home(selected);
        }
    });
    $("#txtCountryOffice").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillStateDDL_Office(selected);
        }
    });
    $("#txtEmergencyCountry").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillStateDDL_EmeContact(selected);
        }
    });
    //17082019 - end
    $("#txtCountry2").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillStateDDL_Home(selected);
        }
    });


    $("#btnParking").on("click", function (event) {
        fillParkingList();
        addParkingArray = [];
        $("#popParking").PopupWindow("open");
    });
    //$("#popParking").PopupWindow({
    //    title: "Add Parking",
    //    modal: false,
    //    autoOpen: false,
    //    top: 120,
    //    left: 300,
    //    height: 560,

    //});

    //$("#btnFob").on("click", function (event) {
    //    fillStorageList();
    //    $("#popFob").PopupWindow("open");
    //});
    $("#btnFob").on("click", function (event) {
        fillFOBList();
        $("#popFobs").PopupWindow("open");
    });
    //$("#popFob").PopupWindow({
    //    title: "Add FOB",
    //    modal: false,
    //    autoOpen: false,
    //    top: 120,
    //    left: 300,
    //    height: 560,

    //});

    //   $("#btnStorage").on("click", function (event) {
    //    fillStorageList();
    //    $("#popStorage").PopupWindow("open");
    //});

    $("#btnStorage").on("click", function (event) {
        fillStorageList();
        $("#popStorage").PopupWindow("open");
    });


    //$("#popStorage").PopupWindow({
    //    title: "Add Storage",
    //    modal: false,
    //    autoOpen: false,
    //    top: 120,
    //    left: 300,
    //    height: 560,

    //});

    $("#btnPetPlace").on("click", function (event) {
        fillPetPlaceList();
        addPetPlaceArray = [];
        $("#popPetPlace").PopupWindow("open");
    });
    //$("#popPetPlace").PopupWindow({
    //    title: "Add Pet",
    //    modal: false,
    //    autoOpen: false,
    //    top: 120,
    //    left: 300,
    //    height: 560,

    //});
    if ($("#ddlIsInter").val() == 1) {
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
    //Sohan

    //$("#popApplicant").PopupWindow({
    //    title: "Add Applicant",
    //    modal: false,
    //    autoOpen: false,
    //    top: 120,
    //    left: 300,
    //    height: 500,

    //});

    $("#btnAddVehicle").on("click", function (event) {
        clearVehicle();
        $("#popVehicle").PopupWindow("open");
    });

    $("#btnAddPet").on("click", function (event) {
        clearPet();
        $("#popPet").PopupWindow("open");
    });
    //$("#popPet").PopupWindow({
    //    title: "Add Pet",
    //    modal: false,
    //    autoOpen: false,
    //    top: 120,
    //    left: 300,
    //    height: 500,

    //});
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
    
    QuoteExpires = $("#lblFNLQuoteExpires").text();
    $("#getting-startedTimeRemainingClock").countdown(QuoteExpires, function (event) {
        $(this).text(
            event.strftime('Quote Expires in %D days %H hr : %M min')
        );
    });
    //$("#appGenderOther").addClass("hidden");
    $("#ddlApplicantGender").on("change", function () {
        if ($("#ddlApplicantGender").val() == '3') {
            $("#policyStart").attr("disabled", false);
        }
        else {
            //$("#appGenderOther").addClass("hidden");
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
    document.getElementById('fileUploadVehicleRegistation').onchange = function () {
        var fileUploadVehicleRegistationBool = restrictFileUpload($(this).val());
        if (fileUploadVehicleRegistationBool == true) {
            uploadVehicleCertificate();
        }
        else {
            document.getElementById('fileUploadVehicleRegistation').value = '';
            $('#VehicleRegistationShow').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
    document.getElementById('pet-picture').onchange = function () {
        var filepetpictureBool = restrictFileUpload($(this).val());
        if (filepetpictureBool == true) {
            uploadPetPhoto();
        }
        else {
            document.getElementById('pet-picture').value = '';
            $('#filePetPictireShow').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };
    document.getElementById('filePetVaccinationCertificate').onchange = function () {
        var filePetVaccinationCertificateBool = restrictFileUpload($(this).val());
        if (filePetVaccinationCertificateBool == true) {
            uploadPetVaccination();
        }
        else {
            document.getElementById('filePetVaccinationCertificate').value = '';
            $('#filePetVaccinationCertificateShow').html('Choose a file&hellip;');
            $.alert({
                title: "",
                content: "Only the following file extensions are allowed...</br>'gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd'",
                type: 'blue'
            });
        }
    };

    dateIconFunctions();

});
var abcd = function () {
  
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
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li5").removeClass("active");
        $("#li6").removeClass("active");
        $("#li7").removeClass("active");

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
        $("#subMenu").addClass("hidden");
        
    }
    if (stepid == "2") {
        if ($('#txtAvailableDate').text() == '') {
            $('#lblLeaseStartDate').text($('#txtDate').val());
        }
        else {
            $('#lblLeaseStartDate').text($('#txtAvailableDate').text());
        }
        if (id == "2") {
            $("#subMenu").addClass("hidden");
            $("#as2").removeAttr("onclick")
            $("#as2").attr("onclick", "goToStep(2,2)");
             //getPropertyUnitDetails($("#hndUID").val());
            $("#li1").addClass("active");
            $("#li2").addClass("active");

            $("#li3").removeClass("active");
            $("#li4").removeClass("active");
            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
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

        }

    }
    if (stepid == "3") {
        if (id == "3") {
            $("#as3").removeAttr("onclick");
            $("#as3").attr("onclick", "goToStep(3,3)");
            if ($("#hndUID").val() != 0) {
                $("#subMenu").addClass("hidden");
                SaveQuote();
                var checkReload = localStorage.getItem("CheckReload");
                if (checkReload == "Done") {
                    window.location.reload();
                }
                $("#li1").addClass("active");
                $("#li2").addClass("active");
                $("#li3").addClass("active");

                $("#li4").removeClass("active");
                $("#li5").removeClass("active");
                $("#li6").removeClass("active");
                $("#li7").removeClass("active");
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
            } else {
                $.alert({
                    title: "",
                    content: "Please select your Unit",
                    type: 'red'
                });
            }

        }
    }
    if (stepid == "4") {
        if (id == "4") {
            $("#subMenu").addClass("hidden");
            $("#as4").removeAttr("onclick")
            $("#as4").attr("onclick", "goToStep(5,5)");
            $("#li1").addClass("active");
            $("#li2").addClass("active");
            $("#li3").addClass("active");

            $("#li4").removeClass("active");
            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");
            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").removeClass("hidden");
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
        }
    }
    if (stepid == "5") {
        if (id == "5") {
            $("#subMenu").addClass("hidden");
            $("#as5").removeAttr("onclick")
            $("#as5").attr("onclick", "goToStep(6,6)");
            SaveQuote();
            $("#getting-startedTimeRemainingClock").removeClass("hidden");
            $("#li1").addClass("active");
            $("#li2").addClass("active");
            $("#li3").addClass("active");
            $("#li4").addClass("active");

            $("#li5").removeClass("active");
            $("#li6").removeClass("active");
            $("#li7").removeClass("active");

            $("#step2").addClass("hidden");
            $("#step1").addClass("hidden");
            $("#step4").addClass("hidden");
            $("#step3").addClass("hidden");
            $("#step5").removeClass("hidden");
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
        }
    }
    if (stepid == "6") {

        if (id == "6") {
            $("#subMenu").addClass("hidden");
            SaveQuote();
            $('#lblRFPAdditionalParking').text($('#lblMonthly_AditionalParking').text());
            $('#lblRFPStorageUnit').text($('#lblMonthly_Storage').text());
            $('#lblRFPPetRent').text($('#lblMonthly_PetRent').text());
            $("#lblRFPTotalMonthlyPayment").text(formatMoney((parseFloat(unformatText($("#lblRFPMonthlyCharges").text()))) + (parseFloat($("#lblRFPAdditionalParking").text())) + (parseFloat($("#lblRFPStorageUnit").text())) + (parseFloat($("#lblRFPPetRent").text())) + (parseFloat($("#lblRFPTrashRecycling").text())) + (parseFloat($("#lblRFPPestControl").text())) + (parseFloat($("#lblRFPConvergentbillingfee").text()))));
            $("#li1").addClass("active");
            $("#li2").addClass("active");
            $("#li3").addClass("active");
            $("#li4").addClass("active");
            $("#li5").addClass("active");

            $("#li6").removeClass("active");
            $("#li7").removeClass("active");

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

        }
    }
    if (stepid == "7") {
        if (id == "7") {
            $("#subMenu").removeClass("hidden");
            SaveCheckPolicy();
            $("#as6").removeAttr("onclick");
            $("#as6").attr("onclick", "goToStep(7,7)");
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

            $("#li7").addClass("active");
            $("#li8").removeClass("active");
            $("#li9").removeClass("active");
            $("#li10").removeClass("active");
            $("#li11").removeClass("active");
            $("#li12").removeClass("active");
            $("#li13").removeClass("active");
            $("#li14").removeClass("active");
            $("#li15").removeClass("active");
            $("#li16").removeClass("active");
            $("#li17").removeClass("active");          
        }
    }
    if (stepid == "8") {
        if (id == "8") {
            $('#lblRFPAdditionalParking').text($('#lblMonthly_AditionalParking').text());
            $('#lblRFPStorageUnit').text($('#lblMonthly_Storage').text());
            $('#lblRFPPetRent').text($('#lblMonthly_PetRent').text());
            $("#lblRFPTotalMonthlyPayment").text(formatMoney(parseFloat((parseFloat(unformatText($("#lblRFPMonthlyCharges").text()))) + (parseFloat($("#lblRFPAdditionalParking").text())) + (parseFloat($("#lblRFPStorageUnit").text())) + (parseFloat($("#lblRFPPetRent").text())) + (parseFloat($("#lblRFPTrashRecycling").text())) + (parseFloat($("#lblRFPPestControl").text())) + (parseFloat($("#lblRFPConvergentbillingfee").text()))).toFixed(2)));
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

            $("#li8").addClass("active");
            $("#li7").removeClass("active");
            $("#li9").removeClass("active");
            $("#li10").removeClass("active");
            $("#li11").removeClass("active");
            $("#li12").removeClass("active");
            $("#li13").removeClass("active");
            $("#li14").removeClass("active");
            $("#li15").removeClass("active");
            $("#li16").removeClass("active");
            $("#li17").removeClass("active");
           
            //if (paidamt == totpaid) {
            //    $("#carddetails").addClass("hidden");
            //    goToStep(16, 16);
            //    $("#getting-startedTimeRemainingClock").addClass("hidden")
            //}
        }
    }
    if (stepid == "9") {
        var msg = '';
        if (id == "9") {
           // var msg = '';
            var grandPercentage = localStorage.getItem("percentage");
            var grandPercentageMo = localStorage.getItem("percentageMo");
            if (grandPercentage != 100 || grandPercentageMo != 100) {
                msg = "Both Charges Should have 100% to continue";
            }
            else {

                saveupdatePaymentResponsibility();  //Amit's work
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


                $("#li9").addClass("active");
                $("#li8").removeClass("active");
                $("#li7").removeClass("active");
                $("#li10").removeClass("active");
                $("#li11").removeClass("active");
                $("#li12").removeClass("active");
                $("#li13").removeClass("active");
                $("#li14").removeClass("active");
                $("#li15").removeClass("active");
                $("#li16").removeClass("active");
                $("#li17").removeClass("active");
            }

            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });
            }
        }
    }
    if (stepid == "10") {
        if (id == "10") {

            getApplicantHistoryList();
            var msg = '';

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length <9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }

            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });


                ////$("#step2").addClass("hidden");
                ////$("#step1").addClass("hidden");
                ////$("#step4").addClass("hidden");
                ////$("#step3").addClass("hidden");
                ////$("#step5").addClass("hidden");
                ////$("#step6").addClass("hidden");
                ////$("#step7").addClass("hidden");
                ////$("#step8").addClass("hidden");
                ////$("#step9").removeClass("hidden");
                ////$("#step10").addClass("hidden");
                ////$("#step11").addClass("hidden");
                ////$("#step12").addClass("hidden");
                ////$("#step13").addClass("hidden");
                ////$("#step14").addClass("hidden");
                ////$("#step15").addClass("hidden");
                ////$("#step16").addClass("hidden");
                
                return;
            }
            else {
                saveupdateTenantOnline();
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

                $("#li10").addClass("active");
                $("#li8").removeClass("active");
                $("#li9").removeClass("active");
                $("#li7").removeClass("active");
                $("#li11").removeClass("active");
                $("#li12").removeClass("active");
                $("#li13").removeClass("active");
                $("#li14").removeClass("active");
                $("#li15").removeClass("active");
                $("#li16").removeClass("active");
                $("#li17").removeClass("active");
            }
        }
    }
    if (stepid == "11") {
        if (id == "11") {
            $("#divLoader").show();
           getMonthsCountFromApplicantHistory();
            var msg = '';

            //step10

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length < 9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }
          //step10


            if ($("#txtCountry").val() == "0") {
                msg += "Please Select Country </br>";
            }
            if (!$("#txtAddress1").val()) {
                msg += "Please Fill Address 1  </br>";
            }
            if ($("#ddlStateHome").val() == "0") {
                msg += "Please Select State </br>";
            }
            if (!$("#ddlCityHome").val()) {
                msg += "Please Fill City </br>";
            }
            if (!$("#txtZip").val()) {
                msg += "Please Fill Zip </br>";
            }
            if (!$("#txtMoveInDateFrom").val()) {
                msg += "Please Fill Move In Date </br>";
            }
            if (!$("#txtMoveInDateTo").val()) {
                msg += "Please Fill Move Out Date </br>";
            }
            setTimeout(function () {
                
            var appMCount = $("#hndHistory").val();
            if (appMCount < 35) {
                msg += "Please Provide Min 3 Years Of History </br>";
            };
            
            
            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });
                
              
                return;
            }
            else {
                saveupdateTenantOnline();
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

                $("#li11").addClass("active");
                $("#li8").removeClass("active");
                $("#li9").removeClass("active");
                $("#li7").removeClass("active");
                $("#li10").removeClass("active");
                $("#li12").removeClass("active");
                $("#li13").removeClass("active");
                $("#li14").removeClass("active");
                $("#li15").removeClass("active");
                $("#li16").removeClass("active");
                $("#li17").removeClass("active");
            }
            $("#divLoader").hide();
            }, 2000);
        }
    }
    if (stepid == "12") {
        if (id == "12") {
            var msg = '';
            //step10

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length < 9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }
          //step10
            //step11
            if ($("#txtCountry").val() == "0") {
                msg += "Please Select Country </br>";
            }
            if (!$("#txtAddress1").val()) {
                msg += "Please Fill Address 1  </br>";
            }
            if ($("#ddlStateHome").val() == "0") {
                msg += "Please Select State </br>";
            }
            if (!$("#ddlCityHome").val()) {
                msg += "Please Fill City </br>";
            }
            if (!$("#txtZip").val()) {
                msg += "Please Fill Zip </br>";
            }
            if (!$("#txtMoveInDateFrom").val()) {
                msg += "Please Fill Move In Date </br>";
            }
            if (!$("#txtMoveInDateTo").val()) {
                msg += "Please Fill Move Out Date </br>";
            }
           
                //step11

            if (!$("#txtEmployerName").val()) {
                msg += "Please Fill The Employer Name </br>";
            }
            if (!$("#txtStartDate").val()) {
                msg += "Please Fill The Start Date </br>";
            }
            if (!$("#txtAnnualIncome").val()) {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#txtAnnualIncome").val() == '0.00') {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#hndHasTaxReturnFile").val() == "0") {
                if (document.getElementById('fileUploadTaxReturn').files.length == '0') {
                    msg += "Please Upload last 3 paystubs or if self-employed last 2 year's Federal Tax Returns </br>";
                }
            }
            if ($("#rbtnPaystub").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel2 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel2 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile3").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn3').files.length == '0') {
                        var upLabel3 = $('#lblUpload3').text();
                        msg += "Please Upload " + upLabel3 + " </br>";
                    }
                }
            }
            if ($("#rbtnFedralTax").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel4 = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel4 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel5 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel5 + " </br>";
                    }
                }
            }
            if ($("#txtCountryOffice").val() == '0') {
                msg += "Please Select The Country </br>";
            }
            if (!$("#txtofficeAddress1").val()) {
                msg += "Please Fill The Address Line 1 </br>";
            }
            if ($("#ddlStateEmployee").val() == '0') {
                msg += "Please Select The State </br>";
            }
            if (!$("#ddlCityEmployee").val()) {
                msg += "Please Fill The City </br>";
            }
            if (!$("#txtZipOffice").val()) {
                msg += "Please Fill The Zip </br>";
            }
            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });

                //$("#step2").addClass("hidden");
                //$("#step1").addClass("hidden");
                //$("#step4").addClass("hidden");
                //$("#step3").addClass("hidden");
                //$("#step5").addClass("hidden");
                //$("#step6").addClass("hidden");
                //$("#step7").addClass("hidden");
                //$("#step8").addClass("hidden");
                //$("#step9").addClass("hidden");
                //$("#step10").addClass("hidden");
                //$("#step11").removeClass("hidden");
                //$("#step12").addClass("hidden");
                //$("#step13").addClass("hidden");
                //$("#step14").addClass("hidden");
                //$("#step15").addClass("hidden");
                //$("#step16").addClass("hidden");
                return;
            }
            else {
                saveupdateTenantOnline();
                getTenantPetPlaceData();
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

                $("#li12").addClass("active");
                $("#li8").removeClass("active");
                $("#li9").removeClass("active");
                $("#li7").removeClass("active");
                $("#li11").removeClass("active");
                $("#li10").removeClass("active");
                $("#li13").removeClass("active");
                $("#li14").removeClass("active");
                $("#li15").removeClass("active");
                $("#li16").removeClass("active");
                $("#li17").removeClass("active");
            }
        }
    }
    if (stepid == "13") {
        if (id == "13") {

            var msg = '';
            //step10

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length < 9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }
            //step10
            //step11
            if ($("#txtCountry").val() == "0") {
                msg += "Please Select Country </br>";
            }
            if (!$("#txtAddress1").val()) {
                msg += "Please Fill Address 1  </br>";
            }
            if ($("#ddlStateHome").val() == "0") {
                msg += "Please Select State </br>";
            }
            if (!$("#ddlCityHome").val()) {
                msg += "Please Fill City </br>";
            }
            if (!$("#txtZip").val()) {
                msg += "Please Fill Zip </br>";
            }
            if (!$("#txtMoveInDateFrom").val()) {
                msg += "Please Fill Move In Date </br>";
            }
            if (!$("#txtMoveInDateTo").val()) {
                msg += "Please Fill Move Out Date </br>";
            }

            //step11

            if (!$("#txtEmployerName").val()) {
                msg += "Please Fill The Employer Name </br>";
            }
            if (!$("#txtStartDate").val()) {
                msg += "Please Fill The Start Date </br>";
            }
            if (!$("#txtAnnualIncome").val()) {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#txtAnnualIncome").val() == '0.00') {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#hndHasTaxReturnFile").val() == "0") {
                if (document.getElementById('fileUploadTaxReturn').files.length == '0') {
                    msg += "Please Upload last 3 paystubs or if self-employed last 2 year's Federal Tax Returns </br>";
                }
            }
            if ($("#rbtnPaystub").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel2 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel2 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile3").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn3').files.length == '0') {
                        var upLabel3 = $('#lblUpload3').text();
                        msg += "Please Upload " + upLabel3 + " </br>";
                    }
                }
            }
            if ($("#rbtnFedralTax").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel4 = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel4 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel5 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel5 + " </br>";
                    }
                }
            }
            if ($("#txtCountryOffice").val() == '0') {
                msg += "Please Select The Country </br>";
            }
            if (!$("#txtofficeAddress1").val()) {
                msg += "Please Fill The Address Line 1 </br>";
            }
            if ($("#ddlStateEmployee").val() == '0') {
                msg += "Please Select The State </br>";
            }
            if (!$("#ddlCityEmployee").val()) {
                msg += "Please Fill The City </br>";
            }
            if (!$("#txtZipOffice").val()) {
                msg += "Please Fill The Zip </br>";
            }
            //step12

            if (!$("#txtRelationship").val()) {
                msg += "Please Fill The Relationship </br>";
            }
            if (!$("#txtEmergencyFirstName").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtEmergencyLastName").val()) {
                msg += "Please Fill The Last Name </br>";
            }
            if (!unformatText($("#txtEmergencyMobile").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtEmergencyMobile").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }

            if (!$("#txtEmergencyEmail").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmergencyEmail").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }

            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });

                //$("#step2").addClass("hidden");
                //$("#step1").addClass("hidden");
                //$("#step4").addClass("hidden");
                //$("#step3").addClass("hidden");
                //$("#step5").addClass("hidden");
                //$("#step6").addClass("hidden");
                //$("#step7").addClass("hidden");
                //$("#step8").addClass("hidden");
                //$("#step9").addClass("hidden");
                //$("#step10").addClass("hidden");
                //$("#step11").addClass("hidden");
                //$("#step12").removeClass("hidden");
                //$("#step13").addClass("hidden");
                //$("#step14").addClass("hidden");
                //$("#step15").addClass("hidden");
                //$("#step16").addClass("hidden");
                return;
            }
            else {
                saveupdateTenantOnline();
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

                $("#li13").addClass("active");
                $("#li8").removeClass("active");
                $("#li9").removeClass("active");
                $("#li7").removeClass("active");
                $("#li11").removeClass("active");
                $("#li12").removeClass("active");
                $("#li10").removeClass("active");
                $("#li14").removeClass("active");
                $("#li15").removeClass("active");
                $("#li16").removeClass("active");
                $("#li17").removeClass("active");
            }
        }


    }
    if (stepid == "14") {
        if (id == "14") {
            var msg = '';
            //step10

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length < 9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }
            //step10
            //step11
            if ($("#txtCountry").val() == "0") {
                msg += "Please Select Country </br>";
            }
            if (!$("#txtAddress1").val()) {
                msg += "Please Fill Address 1  </br>";
            }
            if ($("#ddlStateHome").val() == "0") {
                msg += "Please Select State </br>";
            }
            if (!$("#ddlCityHome").val()) {
                msg += "Please Fill City </br>";
            }
            if (!$("#txtZip").val()) {
                msg += "Please Fill Zip </br>";
            }
            if (!$("#txtMoveInDateFrom").val()) {
                msg += "Please Fill Move In Date </br>";
            }
            if (!$("#txtMoveInDateTo").val()) {
                msg += "Please Fill Move Out Date </br>";
            }

            //step11

            if (!$("#txtEmployerName").val()) {
                msg += "Please Fill The Employer Name </br>";
            }
            if (!$("#txtStartDate").val()) {
                msg += "Please Fill The Start Date </br>";
            }
            if (!$("#txtAnnualIncome").val()) {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#txtAnnualIncome").val() == '0.00') {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#hndHasTaxReturnFile").val() == "0") {
                if (document.getElementById('fileUploadTaxReturn').files.length == '0') {
                    msg += "Please Upload last 3 paystubs or if self-employed last 2 year's Federal Tax Returns </br>";
                }
            }
            if ($("#rbtnPaystub").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel2 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel2 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile3").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn3').files.length == '0') {
                        var upLabel3 = $('#lblUpload3').text();
                        msg += "Please Upload " + upLabel3 + " </br>";
                    }
                }
            }
            if ($("#rbtnFedralTax").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel4 = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel4 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel5 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel5 + " </br>";
                    }
                }
            }
            if ($("#txtCountryOffice").val() == '0') {
                msg += "Please Select The Country </br>";
            }
            if (!$("#txtofficeAddress1").val()) {
                msg += "Please Fill The Address Line 1 </br>";
            }
            if ($("#ddlStateEmployee").val() == '0') {
                msg += "Please Select The State </br>";
            }
            if (!$("#ddlCityEmployee").val()) {
                msg += "Please Fill The City </br>";
            }
            if (!$("#txtZipOffice").val()) {
                msg += "Please Fill The Zip </br>";
            }
            //step12

            if (!$("#txtRelationship").val()) {
                msg += "Please Fill The Relationship </br>";
            }
            if (!$("#txtEmergencyFirstName").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtEmergencyLastName").val()) {
                msg += "Please Fill The Last Name </br>";
            }
            if (!unformatText($("#txtEmergencyMobile").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtEmergencyMobile").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }

            if (!$("#txtEmergencyEmail").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmergencyEmail").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }

            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });

                return;
            } else {
                getTenantPetPlaceData();
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

                $("#li14").addClass("active");
                $("#li8").removeClass("active");
                $("#li9").removeClass("active");
                $("#li7").removeClass("active");
                $("#li11").removeClass("active");
                $("#li12").removeClass("active");
                $("#li13").removeClass("active");
                $("#li10").removeClass("active");
                $("#li15").removeClass("active");
                $("#li16").removeClass("active");
                $("#li17").removeClass("active");
            }
        }
    }
    if (stepid == "15") {
        if (id == "15") {
            var msg = '';
            //step10

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length < 9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }
            //step10
            //step11
            if ($("#txtCountry").val() == "0") {
                msg += "Please Select Country </br>";
            }
            if (!$("#txtAddress1").val()) {
                msg += "Please Fill Address 1  </br>";
            }
            if ($("#ddlStateHome").val() == "0") {
                msg += "Please Select State </br>";
            }
            if (!$("#ddlCityHome").val()) {
                msg += "Please Fill City </br>";
            }
            if (!$("#txtZip").val()) {
                msg += "Please Fill Zip </br>";
            }
            if (!$("#txtMoveInDateFrom").val()) {
                msg += "Please Fill Move In Date </br>";
            }
            if (!$("#txtMoveInDateTo").val()) {
                msg += "Please Fill Move Out Date </br>";
            }

            //step11

            if (!$("#txtEmployerName").val()) {
                msg += "Please Fill The Employer Name </br>";
            }
            if (!$("#txtStartDate").val()) {
                msg += "Please Fill The Start Date </br>";
            }
            if (!$("#txtAnnualIncome").val()) {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#txtAnnualIncome").val() == '0.00') {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#hndHasTaxReturnFile").val() == "0") {
                if (document.getElementById('fileUploadTaxReturn').files.length == '0') {
                    msg += "Please Upload last 3 paystubs or if self-employed last 2 year's Federal Tax Returns </br>";
                }
            }
            if ($("#rbtnPaystub").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel2 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel2 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile3").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn3').files.length == '0') {
                        var upLabel3 = $('#lblUpload3').text();
                        msg += "Please Upload " + upLabel3 + " </br>";
                    }
                }
            }
            if ($("#rbtnFedralTax").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel4 = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel4 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel5 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel5 + " </br>";
                    }
                }
            }
            if ($("#txtCountryOffice").val() == '0') {
                msg += "Please Select The Country </br>";
            }
            if (!$("#txtofficeAddress1").val()) {
                msg += "Please Fill The Address Line 1 </br>";
            }
            if ($("#ddlStateEmployee").val() == '0') {
                msg += "Please Select The State </br>";
            }
            if (!$("#ddlCityEmployee").val()) {
                msg += "Please Fill The City </br>";
            }
            if (!$("#txtZipOffice").val()) {
                msg += "Please Fill The Zip </br>";
            }
            //step12

            if (!$("#txtRelationship").val()) {
                msg += "Please Fill The Relationship </br>";
            }
            if (!$("#txtEmergencyFirstName").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtEmergencyLastName").val()) {
                msg += "Please Fill The Last Name </br>";
            }
            if (!unformatText($("#txtEmergencyMobile").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtEmergencyMobile").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }

            if (!$("#txtEmergencyEmail").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmergencyEmail").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }

            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });


                return;
            } else {

                if ($("#btnAddPet").is(":disabled")) {
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
                    $("#step15").removeClass("hidden");
                    $("#step16").addClass("hidden");
                    $("#step17").addClass("hidden");

                    $("#li15").addClass("active");
                    $("#li8").removeClass("active");
                    $("#li9").removeClass("active");
                    $("#li7").removeClass("active");
                    $("#li11").removeClass("active");
                    $("#li12").removeClass("active");
                    $("#li13").removeClass("active");
                    $("#li14").removeClass("active");
                    $("#li10").removeClass("active");
                    $("#li16").removeClass("active");
                    $("#li17").removeClass("active");

                    if (paidamt == totpaid) {
                        $("#carddetails").addClass("hidden");
                        goToStep(16, 16);
                        $("#getting-startedTimeRemainingClock").addClass("hidden")
                    }
                }
                else {

                    $.alert({
                        title: "",
                        content: "Please Add Pets",
                        type: 'red'
                    });
                    return;
                }
            }
        }
    }
    if (stepid == "16") {
        if (id == "16") {
            var msg = '';
            //step10

            if (!$("#txtFirstNamePersonal").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtLastNamePersonal").val()) {
                msg += "Please Fill The  Last Name </br>";
            }
            if (!$("#txtDateOfBirth").val()) {
                msg += "Please Fill The  Date Of Birth </br>";
            }
            if ($("#ddlGender").val() == "0") {
                msg += "Please Select The Gender </br>";
            }
            else if ($("#ddlGender").val() == "3") {
                if ($("#txtOtherGender").val() == "") {
                    msg += "Please Fill The Other Gender </br>";
                }
            }
            if ($("#ddlIsInter").val() == "0") {
                if (!$("#txtSSNNumber").data('value')) {
                    msg += "Please Fill The SSN number </br>";
                }
                else {
                    if ($("#txtSSNNumber").data('value').length < 9) {
                        msg += "SSN number must be 9 digit </br>";
                    }
                }

            }
            if (!$("#txtEmailNew").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmailNew").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }
            if (!unformatText($("#txtMobileNumber").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtMobileNumber").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }
            if ($("#ddlIsInter").val() == "1") {
                if (!$("#txtPassportNum").val()) {
                    msg += "Please Fill The Passport number </br>";
                }
                if (!$("#txtCOI").val()) {
                    msg += "Please Fill The Country Of Issuance </br>";
                }
                if (!$("#txtDateOfIssuance").val()) {
                    msg += "Please Fill The Date of Issuance </br>";
                }
                if (!$("#txtDateOfExpiration").val()) {
                    msg += "Please Fill The Date of Expiration </br>";
                }
                if ($("#hndHasPassportFile").val() == "0") {
                    if (document.getElementById('fileUploadPassport').files.length == '0') {
                        msg += "Please Upload The Passport </br>";
                    }
                }
            }
            if ($("#ddlDocumentTypePersonal").val() == "0") {
                msg += "Please Select The Id Type </br>";
            }
            if (!$("#txtIDNumber").data('value')) {
                msg += "Please Fill The  Id Number </br>";
            }
            if ($("#hndHasIdentityFile").val() == "0") {
                if (document.getElementById('fileUploadIdentity').files.length == '0') {
                    var idType = document.getElementById('lblUploadIdentity').innerHTML;
                    msg += "Please Upload The " + idType + " </br>";
                }
            }
            //step10
            //step11
            if ($("#txtCountry").val() == "0") {
                msg += "Please Select Country </br>";
            }
            if (!$("#txtAddress1").val()) {
                msg += "Please Fill Address 1  </br>";
            }
            if ($("#ddlStateHome").val() == "0") {
                msg += "Please Select State </br>";
            }
            if (!$("#ddlCityHome").val()) {
                msg += "Please Fill City </br>";
            }
            if (!$("#txtZip").val()) {
                msg += "Please Fill Zip </br>";
            }
            if (!$("#txtMoveInDateFrom").val()) {
                msg += "Please Fill Move In Date </br>";
            }
            if (!$("#txtMoveInDateTo").val()) {
                msg += "Please Fill Move Out Date </br>";
            }

            //step11

            if (!$("#txtEmployerName").val()) {
                msg += "Please Fill The Employer Name </br>";
            }
            if (!$("#txtStartDate").val()) {
                msg += "Please Fill The Start Date </br>";
            }
            if (!$("#txtAnnualIncome").val()) {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#txtAnnualIncome").val() == '0.00') {
                msg += "Please Fill The Annual Income </br>";
            }
            if ($("#hndHasTaxReturnFile").val() == "0") {
                if (document.getElementById('fileUploadTaxReturn').files.length == '0') {
                    msg += "Please Upload last 3 paystubs or if self-employed last 2 year's Federal Tax Returns </br>";
                }
            }
            if ($("#rbtnPaystub").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel2 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel2 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile3").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn3').files.length == '0') {
                        var upLabel3 = $('#lblUpload3').text();
                        msg += "Please Upload " + upLabel3 + " </br>";
                    }
                }
            }
            if ($("#rbtnFedralTax").is(":checked")) {
                if ($("#hndHasTaxReturnFile1").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn1').files.length == '0') {
                        var upLabel4 = $('#lblUpload1').text();
                        msg += "Please Upload " + upLabel4 + " </br>";
                    }
                }
                if ($("#hndHasTaxReturnFile2").val() == "0") {
                    if (document.getElementById('fileUploadTaxReturn2').files.length == '0') {
                        var upLabel5 = $('#lblUpload2').text();
                        msg += "Please Upload " + upLabel5 + " </br>";
                    }
                }
            }
            if ($("#txtCountryOffice").val() == '0') {
                msg += "Please Select The Country </br>";
            }
            if (!$("#txtofficeAddress1").val()) {
                msg += "Please Fill The Address Line 1 </br>";
            }
            if ($("#ddlStateEmployee").val() == '0') {
                msg += "Please Select The State </br>";
            }
            if (!$("#ddlCityEmployee").val()) {
                msg += "Please Fill The City </br>";
            }
            if (!$("#txtZipOffice").val()) {
                msg += "Please Fill The Zip </br>";
            }
            //step12

            if (!$("#txtRelationship").val()) {
                msg += "Please Fill The Relationship </br>";
            }
            if (!$("#txtEmergencyFirstName").val()) {
                msg += "Please Fill The First Name </br>";
            }
            if (!$("#txtEmergencyLastName").val()) {
                msg += "Please Fill The Last Name </br>";
            }
            if (!unformatText($("#txtEmergencyMobile").val())) {
                msg += "Please Fill The Mobile Number </br>";
            }
            else {
                if (!validatePhone(unformatText($("#txtEmergencyMobile").val()))) {
                    msg += "Please Fill Valid Mobile Number </br>";
                }
            }

            if (!$("#txtEmergencyEmail").val()) {
                msg += "Please Fill The Email </br>";
            }
            else {
                if (!validateEmail($("#txtEmergencyEmail").val())) {
                    msg += "Please Fill Valid Email </br>";
                }
            }

            if (msg != "") {
                $.alert({
                    title: "",
                    content: msg,
                    type: 'red'
                });


                return;
            } else {

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

                $("#li16").addClass("active");
                $("#li8").removeClass("active");
                $("#li9").removeClass("active");
                $("#li7").removeClass("active");
                $("#li11").removeClass("active");
                $("#li12").removeClass("active");
                $("#li13").removeClass("active");
                $("#li14").removeClass("active");
                $("#li15").removeClass("active");
                $("#li10").removeClass("active");
                $("#li17").removeClass("active");
            }
        }
    }
    if (stepid == "17") {
        if (id == "17") {

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
            $("#step17").removeClass("hidden");

            $("#li17").addClass("active");
            $("#li8").removeClass("active");
            $("#li9").removeClass("active");
            $("#li7").removeClass("active");
            $("#li11").removeClass("active");
            $("#li12").removeClass("active");
            $("#li13").removeClass("active");
            $("#li14").removeClass("active");
            $("#li15").removeClass("active");
            $("#li16").removeClass("active");
            $("#li10").removeClass("active");
        }
    }
}

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
    var confirmPassword = $("#txtConfPassword").val();
    var address = $("#txtAddress").val();
    var dob = $("#txtDOB").val();
    var annualIncome = $("#txtAnnualIncome").val();
    var addiAnnualIncome = $("#txtAddiAnnualIncome").val();
    var marketsource = $("#ddlMarketSource").val();
    var moveInDate = $("#txtDate").val();
    var isAgree = $("#chkAgreeTerms").is(":checked") ? "1" : "0";
    var leaseterm = $("#hndLeaseTermID").val();
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
        if (password != confirmPassword) {
            msg += "Password and Confirm Password must be the same</br>";
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
        LeaseTerm: leaseterm
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
var fillMarketSourceDDLA = function () {
    $.ajax({
        url: '/Admin/ProspectManagement/GetDdlMarketSourceList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            $("#ddlMarketSource").empty();
            $("#ddlMarketSource").append("<option value='0'>-- Select Market Source --</option>");
            $.each(response.model, function (index, elementValue) {
                $("#ddlMarketSource").append("<option value=" + elementValue.AdID + ">" + elementValue.Advertiser + "</option>");
            });

        }
    });
}
var SaveQuote = function () {
    $("#divLoader").show();
    var msg = "";
    var ProspectId = $("#hdnOPId").val();
    var ParkingAmt = unformatText($("#lblAdditionalParking").text());
    var StorageAmt = unformatText($("#lblStorageUnit").text());
    var PetPlaceAmt = unformatText($("#lblPetFee").text());
    var PestAmt = $("#lblPestAmt").text();
    var ConvergentAmt = $("#lblConvergentAmt").text();
    var TrashAmt = unformatText($("#lblTrashAmt").text());
    var moveInDate = $("#txtDate").val();
    var moveInCharges = unformatText($("#ftotal").text());
    var monthlyCharges = unformatText($("#lblMonthly_TotalRent").text());

    var petDeposit = unformatText($("#lblPetDeposit").text());
    var fobAmt = $("#lblFobFee").text();
    var deposit = $("#lbdepo6").text();
    var rent = unformatText($("#lblFMRent").text());
    var proratedrent = unformatText($("#lblProrated_TotalRent").text());
    var vehiclefees = $("#lblVehicleFees1").text();
    var adminfees = $("#lblAdminFees").text();
    var leaseterm = $("#hndLeaseTermID").val();
    var petDNAAmt = unformatText($("#lblPetDNAAmt").text());

    var model = {
        ID: ProspectId,
        ParkingAmt: ParkingAmt,
        StorageAmt: StorageAmt,
        PetPlaceAmt: PetPlaceAmt,
        PestAmt: PestAmt,
        ConvergentAmt: ConvergentAmt,
        TrashAmt: TrashAmt,
        MoveInCharges: moveInCharges,
        MonthlyCharges: monthlyCharges,
        MoveInDate: moveInDate,
        PetDeposit: petDeposit,
        FOBAmt: fobAmt,
        Deposit: deposit,
        Rent: rent,
        Prorated_Rent: proratedrent,
        VehicleRegistration: vehiclefees,
        AdminFees: adminfees,
        LeaseTerm: leaseterm,
        PetDNAAmt:petDNAAmt,
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
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();

    var emailId = $("#txtEmail").val();


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
        FirstName: firstName,
        LastName: lastName,
        Email: emailId
    };

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
function savePayment() {
    $("#divLoader").show();
    var msg = "";
    if ($("#hndTransMethod").val() == "0") {
        $("#divLoader").hide();
        $.alert({
            title: "",
            content: "Please Select Payment Method</br>",
            type: 'red'
        });
        return;

    }
    if ($("#hndTransMethod").val() == 2) {
        var paymentMethod = 2;
        var propertyId = $("#hndUID").val();
        var nameonCard = $("#txtNameonCard").val();
        var cardNumber = $("#txtCardNumber").val();
        var cardMonth = $("#ddlcardmonth").val();
        var cardYear = $("#ddlcardyear").val();
        var ccvNumber = $("#txtCCVNumber").val();
        var prospectID = $("#hdnOPId").val();
        var amounttoPay = unformatText($("#totalFinalFees").text()); 
        var description = "Online Application Non Refundable fees";
        var glTrans_Description = $("#payDes").text(); 
        var routingNumber = $("#txtRoutingNumber").val();
        var bankName = $("#txtBankName").val();

        if (!nameonCard) {
            msg += "Please Enter Name on Card</br>";
        }
        if (cardNumber == "" || cardNumber.length != 16) {
            msg += "Please enter your 16 digit Card Number</br>";
        }
        if (cardMonth == "0") {
            msg += "Please enter Card Month</br>";
        }
        if (cardYear == "0") {
            msg += "Please enter Card Year</br>";
        }
        if (ccvNumber < 3) {
            msg += "Please enter CVV Number and It must be 3 digit long</br>";
        }

        var GivenDate = '20' + cardYear + '-' + cardMonth + '-' + new Date().getDate();
        var CurrentDate = new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate();

        GivenDate = new Date(GivenDate);
        CurrentDate = new Date(CurrentDate);
         
        if (GivenDate < CurrentDate) {
            msg += "Your Credit Card Expired..</br>";
        }

        if (msg != "") {
            $("#divLoader").hide();
            $.alert({
                title: "",
                content: msg,
                type: 'red'
            });
            return;
        }
    } else {
        var paymentMethod = 1;
        var propertyId = $("#hndUID").val();
        var nameonCard = $("#txtAccountName").val();
        var cardNumber = $("#txtAccountNumber").val();
        var cardMonth = 0;
        var cardYear = 0;
        var routingNumber = $("#txtRoutingNumber").val();
        var ccvNumber = $("#txtRoutingNumber").val();
        var bankName = $("#txtBankName").val();
        var prospectID = $("#hdnOPId").val();
        var amounttoPay = unformatText($("#totalFinalFees").text()); 
        var description = "Online Application Non Refundable fees";
        var glTrans_Description = $("#payDes").text();
        if (bankName == "") {
            msg += "Please Enter Bank Name</br>";
        }
        if (nameonCard == "") {
            msg += "Please Enter Account Name</br>";
        }
        if (cardNumber == "") {
            msg += "Please Enter Account Number</br>";
        }
        if (ccvNumber == "") {
            msg += "Please Enter Routing Number</br>";
        }
        if (msg != "") {
            $("#divLoader").hide();
            $.alert({
                title: "",
                content: msg,
                type: 'red'
            });
            return;
        }
    }


    var model = {
        PID: propertyId,
        Name_On_Card: nameonCard,
        CardNumber: cardNumber,
        AccountNumber: cardNumber,
        CardMonth: cardMonth,
        CardYear: cardYear,
        CCVNumber: ccvNumber,
        Charge_Amount: amounttoPay,
        ProspectID: prospectID,
        Description: description,
        GL_Trans_Description: glTrans_Description,
        RoutingNumber: routingNumber,
        BankName: bankName,
        PaymentMethod: paymentMethod,
    };
    $.alert({
        title: "",
        content: "You have chosen to pay $" + amounttoPay + " plus a $3.95 processing fee, your total will be $" + parseFloat(parseFloat(amounttoPay) + parseFloat(3.95)).toFixed(2) + ". Do you want to Pay Now?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                $.ajax({
        url: "/ApplyNow/SavePaymentDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#divLoader").hide();
            if (response.Msg == "1") {
                // $("#carddetails").addClass("hidden");
                $(".payNext").removeAttr("disabled");
                $("#ResponseMsg").html("Payment Successfull");
                $.alert({
                    title: "",
                    content: "Payment Successfull",
                    type: 'red'
                });
                getTransationLists($("#hdnUserId").val());
            } else {
                $.alert({
                    title: "",
                    content: "Payment Failed",
                    type: 'red'
                });
            }
        }
                    });
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                    $("#divLoader").hide();
                }
            }
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
                $("#ddlStateHome").empty();
                $("#ddlStateHome2").empty();
                $("#ddlStateHome").append("<option value='0'>--Select State--</option>");
                $("#ddlStateHome2").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlStateHome").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStateHome2").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
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
                $("#ddlState").empty();
                $("#ddlStatePersonal").empty();
                $("#ddlVState").empty();
                $("#ddlState").append("<option value='0'>--Select State--</option>");
                $("#ddlStatePersonal").append("<option value='0'>--Select State--</option>");
                $("#ddlVState").append("<option value='0'>--Select State--</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlStatePersonal").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                    $("#ddlVState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
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
                $("#txtCountry").empty();
                $("#txtCountry2").empty();
                $("#txtCountryOffice").empty();
                $("#txtEmergencyCountry").empty();
                //$("#txtCountry").append("<option value='0'>Select Country</option>");
                $.each(response, function (index, elementValue) {
                    $("#txtCountry").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                    $("#txtCountry2").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                    $("#txtCountryOffice").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                    $("#txtEmergencyCountry").append("<option value=" + elementValue.ID + ">" + elementValue.CountryName + "</option>");
                });
                $("#txtCountry").val(1);
                $("#txtCountry2").val(1);
                $("#txtCountryOffice").val(1);
                $("#txtEmergencyCountry").val(1);
                fillStateDDL_Home(1);
                fillStateDDL_Office(1);
                fillStateDDL_EmeContact(1);
                //setTimeout(function () {
                //    $("#ddlStateHome").find("option[value='0']").attr('selected', 'selected');
                //    $("#ddlStateEmployee").find("option[value='0']").attr('selected', 'selected');
                //    $("#ddlStateContact").find("option[value='0']").attr('selected', 'selected');
                //}, 1500);
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
var addModelArray = [];
var noofcomapre = 0;
var getPropertyModelUnitList = function (stype, pid) {
    addModelArray = [];
    noofcomapre = 0;
    $("#divCompare").addClass("hidden");
    $("#divMainSearch").removeClass("hidden");
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
                $("#listModelUnit").empty();
                $.each(response.model, function (elementType, value) {
                    var html = "<div class='col-sm-4 col-md-4 p0' id='modeldiv" + value.Building + "'><div class='box-two proerty-item'>";
                    //html += " <div class='item-entry overflow'><h5><a href='javascript:void(0);' onclick=getPropertyUnitList(\"" + value.Building + "\")'>" + value.Building + "</a><div class='checkbox pull-right' style='margin-top: -2px;'><label>/*<input type='checkbox' id='chkCompareid" + value.Building + "' class='hidden form-control' style='margin-left: -25px; margin-top: -15px;'>*/<i class='fa fa-check' aria-hidden='true'></i></label></div></h5>  <div class='dot-hr'></div>";
                    html += " <div class='item-entry overflow'><h5><a style='color:black' href='javascript:void(0);' onclick=getPropertyUnitList(\"" + value.Building + "\")'>" + value.Building + "</a><div class='checkbox pull-right' style='margin-top: -2px;'><label id='chkCompareid" + value.Building + "' class='hidden' ><i class='fa fa-check' aria-hidden='true'style='color: #4d738a;'></i></label></div></h5>  <div class='dot-hr'></div>";
                    html += "<div class='item-thumb'><a href = '#' onclick='getPropertyUnitList(\"" + value.Building + "\")'> <img src='/content/assets/img/plan/" + value.FloorPlan + "' style='height:250px';></a></div> ";
                    html += "<div class='property-icon'><center>" + value.Bedroom + " Bed | " + value.Bathroom + "Bath ";
                    html += "| " + value.Area + "Sq Ft <br/><center><span class='proerty-price'>" + value.RentRange + " </span></center></center> ";
                    html += "</br><button class='btn-success' onclick='getPropertyUnitList(\"" + value.Building + "\")' style='width: 100%;'>Available (" + value.NoAvailable + ")</button> <center><u style=''><a onclick='addToCompare(\"" + value.Building + "\")'  href='javascript:void(0);'  id='btnCompare" + value.Building + "' class='custome-item-entry'>Compare</a><a onclick='removeToCompare(\"" + value.Building + "\")'  id='btncompRemove" + value.Building + "'  href='javascript:void(0);' class='hidden'>Remove</a></u></center>";
                    html += "</div></div></div></div>";
                    $("#listModelUnit").append(html);

                });

            }
        }
    });
}

var addToCompare = function (modelname) {
    if (noofcomapre < 3) {
        //$("#modeldiv" + $("#modelname").val()).css("background-color", "");
        //$("#modeldiv" + modelname).css("background-color", "rebeccapurple");

        $("#chkCompareid" + modelname).removeClass("hidden");
        $("#chkCompareid" + modelname).prop("checked", true);
        $("#divCompare").removeClass("hidden");
        $("#divMainSearch").addClass("hidden");
        $("#btncompRemove" + modelname).removeClass("hidden");
        $("#btnCompare" + modelname).addClass("hidden");
        addModelArray.push(modelname);

        noofcomapre = addModelArray.length;
        $("#btncompare").text("Compare(" + noofcomapre + ")")
        getCompareModelList();
    } else {
        $.alert({
            title: "",
            content: "Please Select up to three to compare ",
            type: 'red'
        })
    }

}
var removeToCompare = function (modelname) {
    //$("#modeldiv" + $("#modelname").val()).css("background-color", "");
    //$("#modeldiv" + modelname).css("background-color", "");

    $("#chkCompareid" + modelname).addClass("hidden");
    $("#btncompRemove" + modelname).addClass("hidden");
    $("#btnCompare" + modelname).removeClass("hidden");
    addModelArray.pop(modelname);
    noofcomapre = addModelArray.length;
    $("#btncompare").text("Compare(" + noofcomapre + ")")
    getCompareModelList();
    if (noofcomapre == 0) {
        $("#divCompare").addClass("hidden");
        $("#divMainSearch").removeClass("hidden");
    }
}
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
var getPropertyUnitList = function (modelname) {

    $("#imgFloorPlan").attr("src", "/content/assets/img/plan/" + modelname + ".jpg");
    $("#imgFloorPlan2").attr("src", "/content/assets/img/plan/" + modelname + "Det.jpg");
    $("#lblUnitNo").text("Model: #" + modelname);
    $("#ModelCompare").modal("hide");

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
    var leasetermid = $("#hndLeaseTermID").val();

    var model = { ModelName: modelname, AvailableDate: availdate, Current_Rent: maxrent, Bedroom: bedroom, LeaseTermID: leasetermid};

    $.ajax({
        url: "/Property/GetPropertyModelUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
                $("#divLoader").hide();
                $("#listUnit>tbody").empty();

                $.each(response.model, function (elementType, value) {
                    var html = " <tr id='unitdiv" + value.UID + "' data-floorid = '" + value.FloorNoText + "'><td><a href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'><h5 style='width: 80px;'>#" + value.UnitNo + " </h5></a> </td><td style='text-align: center;width=100px'>$" + formatMoney(value.Current_Rent) + "</td><td style='text-align: center; width:45%'>" + value.Premium + "</td><td style='text-align: center;width=100px'>" + value.AvailableDateText + "</td></tr>";
                    $("#listUnit>tbody").append(html);

                    $("#lblArea22").text(value.Area);
                    $("#lblBed22").text(value.Bedroom);
                    $("#lblBath22").text(value.Bathroom);

                    $("#lblOccupancy22").text((parseInt(value.Bedroom) * 2).toString());
                });
            }
            goToStep(2, 2);
        }
    });
}
var getPropertyUnitDetails = function (uid) {

    var model = { UID: uid, LeaseTermID:$("#hndLeaseTermID").val() };
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
            $("#lblDeposit").text(formatMoney(response.model.Deposit));
           // $("#lblLease").text(response.model.Leased);

            $("#lblRent22").text("$" + response.model.Current_Rent);
            $("#lblArea22").text(response.model.Area);
            $("#lblBed22").text(response.model.Bedroom);
            $("#lblBath22").text(response.model.Bathroom);

            $("#lblOccupancy22").text((parseInt(response.model.Bedroom) * 2).toString());
            $("#lblOccupancy").text((parseInt(response.model.Bedroom) * 2).toString());

            $("#lblDeposit22").text("$" + response.model.Deposit);
            //$("#lblLease22").text(response.model.Leased);

            //$("#lblLease2").text(response.model.Leased);
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

            $("#lbldeposit1").text(formatMoney(parseFloat(response.model.Deposit).toFixed(2)));
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

            totalAmt = (parseFloat(response.model.Current_Rent) + parseFloat(unformatText($("#lblAdditionalParking").text())) + parseFloat(unformatText($("#lblStorageUnit").text())) + parseFloat(unformatText($("#lblTrashAmt").text())) + parseFloat($("#lblPestAmt").text()) + parseFloat($("#lblConvergentAmt").text()) + parseFloat(unformatText($("#lblPetFee").text()))).toFixed(2);
            $("#lbltotalAmount").text(formatMoney(totalAmt));

            //Amit's Work for final Quotation form 15-10
            $("#lblFNLUnit").text("#" + response.model.UnitNo);
            $("#lblFNLModel").text(response.model.Building);
            //$("#lblFNLTerm").text(response.model.Leased);
            $("#lblMonthly_MonthlyCharge").text(formatMoney(response.model.Current_Rent.toFixed(2)));
            $("#lblProrated_MonthlyCharge").text(formatMoney(parseFloat(parseFloat(response.model.Current_Rent) / parseFloat(numberOfDays) * remainingday).toFixed(2)));

            $("#lblRFPMonthlyCharges").text(formatMoney(response.model.Current_Rent.toFixed(2)));

            $("#fdepo").text((response.model.Deposit).toFixed(2));
            $("#lbdepo6").text((response.model.Deposit).toFixed(2));

            $("#lblMonthly_TotalRent").text(formatMoney(totalAmt));
            $("#lblProrated_TotalRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));
            $("#lblProratedRent6").text(formatMoney(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2)));

            var rfpMonthlyCharge = unformatText($("#lblRFPMonthlyCharges").text());
            var rfpParkingCharge = $("#lblRFPAdditionalParking").text();
            var rfpStorageCharge = $("#lblRFPStorageUnit").text();
            var rfpPetcharge = $("#lblRFPPetRent").text();

            var rfpTotalRentCharge = parseFloat(rfpMonthlyCharge, 10) + parseFloat(rfpParkingCharge, 10) + parseFloat(rfpStorageCharge, 10) + parseFloat(rfpPetcharge, 10);
            //alert(calTotalRentChargefpetd
            //$("#lblRFPTotalMonthlyPayment").text((parseFloat($("#lblRFPMonthlyCharges").text())) + (parseFloat($("#lblRFPAdditionalParking").text())) + (parseFloat($("#lblRFPStorageUnit").text())) + (parseFloat($("#lblRFPPetRent").text())));
            $("#ftotal").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10) + parseFloat($("#lblPetDNAAmt").text(), 10)).toFixed(2)));
            $("#lbtotdueatmov6").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10) + parseFloat($("#lblPetDNAAmt").text(), 10)).toFixed(2)));

            $("#lblRFPTotalMonthlyPayment").text(formatMoney((parseFloat(unformatText($("#lblRFPMonthlyCharges").text()))) + (parseFloat($("#lblRFPAdditionalParking").text())) + (parseFloat($("#lblRFPStorageUnit").text())) + (parseFloat($("#lblRFPPetRent").text())) + (parseFloat($("#lblRFPTrashRecycling").text())) + (parseFloat($("#lblRFPPestControl").text())) + (parseFloat($("#lblRFPConvergentbillingfee").text()))));

            $("#lblProrated_TrashAmt").text(parseFloat(parseFloat($("#lblTrash").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));
            $("#lblProrated_PestAmt").text(parseFloat(parseFloat($("#lblPestControl").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));
            $("#lblProrated_ConvergentAmt").text(parseFloat(parseFloat($("#lblConvergentAmt").text()) / parseFloat(numberOfDays) * remainingday).toFixed(2));



            $("#lblRent2").text(response.model.Current_Rent);
            $("#txtModal").text(response.model.Building);
            $("#lblArea1").text(response.model.Area);
            $("#lblBed1").text(response.model.Bedroom);
            $("#lblBath1").text(response.model.Bathroom);
            $("#lblHall1").text(response.model.Hall);
            $("#lblDeposit1").text(formatMoney(response.model.Deposit));
            //$("#lblLease3").text(response.model.Leased);
            //$("#lblLease4").text(response.model.Leased);
            $("#imgFloorPlanSumm").attr("src", "/content/assets/img/plan/" + response.model.Building + ".jpg");
            $("#lbldeposit2").text(parseFloat(response.model.Deposit).toFixed(2));
            $("#lblFMRent1").text(parseFloat(response.model.Current_Rent).toFixed(2));
            $("#lblUnitTitle3").text("#" + response.model.UnitNo);
            //$("#lblLeaseStartDate").text(response.model.AvailableDateText);
            //$("#lblLeaseStartDate").text($("#hndsummaryDesireMoveIn").val());
            $("#lblSubtotalsumm").text((parseFloat(response.model.Current_Rent) + parseFloat(26.50)).toFixed(2));
            $("#lbltotalAmountSumm").text((parseFloat(response.model.Current_Rent) + parseFloat(26.50)).toFixed(2));
            localStorage.setItem("floorfromplan", response.model.FloorNo);
            showFloorPlan(response.model.FloorNo);

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
var paidamt = 0;
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

            $.each(response.model, function (elementType, elementValue) {
                $("#Divtranslist").removeClass("hidden");
              
                $("#btnpaynext").removeProp("disabled");
                var html = "<tr data-value=" + elementValue.TransID + ">";
                //html += "<td>" + elementValue.TransID + "</td>";
                //html += "<td>" + elementValue.TenantIDString + "</td>";
                html += "<td>" + elementValue.Transaction_DateString + "</td>";
                html += "<td>$" + parseFloat(elementValue.Charge_Amount).toFixed(2)+ "</td>";
                html += "<td>" + elementValue.Transaction_Type + "</td>";
                //html += "<td>" + elementValue.Charge_Type + "</td>";
                html += "<td>" + elementValue.Description + "</td>";
                html += "<td>Paid</td>";
                html += "</tr>";
                $("#tblTransaction>tbody").append(html);
                paidamt += parseFloat(elementValue.Charge_Amount);
            });
          
            setTimeout(function () {
                if (response.model.length >= 1) {
                    if (paidamt == totpaid) {
                        $("#carddetails").addClass("hidden");
                        goToStep(16, 16);
                        $("#getting-startedTimeRemainingClock").addClass("hidden")
                    } else {
                        goToStep(16, 16);
                    }
                }
            }, 1500);
                    
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

    if (unformatText($("#lblStorageUnit").text()) == "50.00") {

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
    var ischeck = $(cont).is(':checked');
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
            totalAmt = parseFloat(totalAmt) - unformatText($("#lblAdditionalParking").text());
            $("#lblVehicleFees").text("0.00")
            $("#lblAdditionalParking").text(formatMoney(parseFloat(response.totalParkingAmt).toFixed(2)));
            $("#lblMonthly_AditionalParking").text(parseFloat(response.totalParkingAmt).toFixed(2));
            $("#lblProrated_AditionalParking").text(parseFloat(parseFloat(response.totalParkingAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2));
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
            //$("#ftotal").text((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(30) * remainingday), 10) + parseFloat(response.model.Deposit, 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10)).toFixed(2));
            $("#ftotal").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat($("#fdepo").text(), 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10) + parseFloat($("#lblPetDNAAmt").text(), 10)).toFixed(2)));
            $("#lbtotdueatmov6").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat($("#fdepo").text(), 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10) + parseFloat($("#lblPetDNAAmt").text(), 10)).toFixed(2)));
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
            totalAmt = parseFloat(totalAmt) - parseFloat(unformatText($("#lblPetFee").text()));
            $("#lblPetDeposit").text("0.00");
            $("#lblPetDNAAmt").text("0.00");
            $("#lbpetdna6").text("0.00");
            
            $("#fpetd").text("0.00");
            $("#lbpetd6").text("0.00");
            $("#fpetdna").text("0.00");
            $("#lblPetFee").text(formatMoney(parseFloat(response.totalPetPlaceAmt).toFixed(2)));
            $("#lblMonthly_PetRent").text(parseFloat(response.totalPetPlaceAmt).toFixed(2));
            $("#lblProrated_PetRent").text(parseFloat(parseFloat(response.totalPetPlaceAmt) / parseFloat(numberOfDays) * remainingday).toFixed(2));

            $("#lblpetplace").text(addPetPlaceArray.length > 0 ? addPetPlaceArray[0].PetPlaceID : 0);
            if (parseFloat(response.totalPetPlaceAmt).toFixed(2) == "20.00") {
                $("#lblPetDeposit").text(formatMoney("500.00"));
                $("#lblPetDNAAmt").text("29.00");
                $("#lbpetdna6").text("29.00");
                $("#fpetdna").text("29.00");
                
                $("#fpetd").text("500.00");
                $("#lbpetd6").text("500.00");
                $("#hndPetPlaceID").val(1);
                $("#btnAddPet").removeAttr("disabled");

            } else if (parseFloat(response.totalPetPlaceAmt).toFixed(2) == "40.00") {
                $("#lblPetDeposit").text(formatMoney("750.00"));
                $("#lblPetDNAAmt").text("58.00");
                $("#lbpetdna6").text("58.00");
                $("#fpetdna").text("58.00");
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
            $("#ftotal").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat($("#fdepo").text(), 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10) + parseFloat($("#lblPetDNAAmt").text(), 10)).toFixed(2)));
            $("#lbtotdueatmov6").text(formatMoney((parseFloat(parseFloat(parseFloat(totalAmt) / parseFloat(numberOfDays) * remainingday), 10) + parseFloat($("#fdepo").text(), 10) + parseFloat($("#fpetd").text(), 10) + parseFloat($("#ffob").text(), 10) + parseFloat($("#lblVehicleFees").text(), 10) + parseFloat($("#lblAdminFees").text(), 10) + parseFloat($("#lblPetDNAAmt").text(), 10)).toFixed(2)));
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
    totalAmt = parseFloat(totalAmt) - unformatText($("#lblStorageUnit").text());

    if ($("#chkAddStorage1").is(":checked")) {
        $("#lblStorageUnit").text(formatMoney(parseFloat(50.00).toFixed(2)));
        $("#lblMonthly_Storage").text(parseFloat(50.00).toFixed(2));
        $("#lblProrated_Storage").text(parseFloat(parseFloat(50.00) / parseFloat(numberOfDays) * remainingday).toFixed(2));

        totalAmt = (parseFloat(50.00) + parseFloat(totalAmt)).toFixed(2);

    } else {
        $("#lblStorageUnit").text(formatMoney(parseFloat(0.00).toFixed(2)));
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
        $("#iconCalenderApplicant").click(function () {
            $("#txtADateOfBirth").focus();
        });
        $('#txtADateOfBirth').removeClass("hidden");
        $('#txtHDateOfBirth').addClass("hidden");
        $('#txtMDateOfBirth').addClass("hidden");
        $('#txtGDateOfBirth').addClass("hidden");

        $('#txtApplicantOtherGender').val('');
        //$('#appGenderOther').addClass('hidden');
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
        $("#iconCalenderApplicant").click(function () {
            $("#txtMDateOfBirth").focus();
        });
        $('#txtApplicantOtherGender').val('');
        //$('#appGenderOther').addClass('hidden');
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
        $("#iconCalenderApplicant").click(function () {
            $("#txtGDateOfBirth").focus();
        });
        $('#txtApplicantOtherGender').val('');
        //$('#appGenderOther').addClass('hidden');
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
    if (relationship == '0') {
        msg += "Select Relationship</br>";
    }
    if (!dob) {
        msg += "Enter Applicant DateOfBirth</br>";
    }
    if (agender == '0') {
        msg += "Select The Gender </br>";
    }
    if (agender == '3') {
        if (!($("#txtApplicantOtherGender").val())) {
            msg += "Enter The Other Gender </br>";
        }
    }
    if (type == 'Primary Applicant') {
        $('#txtDateOfBirth').val(dob);
        $('#ddlGender').val(agender);
        if (agender == '3') {
            $('#txtOtherGender').val(aotherGender);
        }
        else {
            $('#txtOtherGender').val('');
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
        OtherGender: aotherGender
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
var totpaid = 0;
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
            var totalFinalFees = 0;
            $.each(response.model, function (elementType, elementValue) {
                var html = '';
                var prhtml = '';
                var pprhtml = '';
                var emailhtml = '';
                if (elementValue.Type != "Primary Applicant") {
                    html += "<div class='col-sm-4 box-two proerty-item' id='div_" + elementValue.ApplicantID + "'>" +
                        "<div class='form-group col-sm-3'><br>" +
                        "<img src='/Content/assets/img/user.png'></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 10px !important;'><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b><br/>" +
                        "<label> " + elementValue.Type + " </label><br/>" +
                        "<label><a href='javascript:void(0)' onclick='goToEditApplicant(" + elementValue.ApplicantID + ")'>Edit/Complete Information</a></label>&nbsp;&nbsp;&nbsp;&nbsp;" +
                        "<label><a href='javascript:void(0)' onclick='delApplicant(" + elementValue.ApplicantID + ")'><span class='fa fa-trash' ></span></a></label>" +
                        "<div style='border: 2px solid #E6E6E6;'><center><label><b>Status: In progress</b></label></center></div>" +
                        "</div></div>";

                }
                else {
                    html += "<div class='col-sm-4 box-two proerty-item'>" +
                        "<div class='form-group col-sm-3'><br>" +
                        "<img src='/Content/assets/img/user.png'></div>" +
                        "<div class='form-group col-sm-9' style='margin-top: 10px !important;'><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b><br/>" +

                        "<label>Primary Applicant</label><br/>" +
                        "<label><a href='javascript:void(0)' onclick='goToEditApplicant(" + elementValue.ApplicantID + ")'>Edit/Complete Information</a></label><br/><div style='border: 2px solid #E6E6E6;'><center><label><b>Status: In progress</b></label></center></div>" +
                        "</div></div>";
                }
                if (elementValue.Type == "Primary Applicant" || elementValue.Type == "Co-Applicant") {
                    //Amit's work 17-10
                    //prhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:18%; padding:6px;'>" + elementValue.Type + "<br /><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></td><td style='width:30%; padding:6px;'><input type='text' id='txtpayper" + elementValue.ApplicantID + "' style='width:40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MoveInPercentage + "'/>(%)<input type='text' id='txtpayamt" + elementValue.ApplicantID + "' style='width: 40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MoveInCharge + "'/>($)</td><td style='width:30%; padding:6px;'><input type='text' id='txtpayperMo" + elementValue.ApplicantID + "' style='width: 40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MonthlyPercentage + "'/>(%)<input type='text' id='txtpayamtMo" + elementValue.ApplicantID + "' style='width: 40% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MonthlyPayment + "'/>($)</td></tr>";
                    prhtml += "<tr data-id='" + elementValue.ApplicantID + "'>" +
                        "<td style='width:18%; padding:6px;'>" + elementValue.Type + "<br /><b>" + elementValue.FirstName + " " + elementValue.LastName + "</b></td>" +
                        "<td style='width:30%;'>" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search ' type='button'><i class='fa fa-percent'></i></button>" +
                        "<input type='text' class='form-control form-control-small payper' id='txtpayper" + elementValue.ApplicantID + "' style='width:60% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MoveInPercentage + "' />" +
                        "</div >" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search pull-left' type='button'><i class='fa fa-dollar'></i></button>" +
                        "<input type='text' class='form-control form-control-small' id='txtpayamt" + elementValue.ApplicantID + "' style='width: 60% !important; border: 1px solid; padding-left: 5px; text-align:right;' value='" + parseFloat(elementValue.MoveInCharge).toFixed(2) + "'/>" +
                        "</div ></td>" +
                        "<td style='width:30%;'>" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search ' type='button'><i class='fa fa-percent'></i></button>" +
                        "<input type='text' class='form-control form-control-small payperMo' id='txtpayperMo" + elementValue.ApplicantID + "' style='width: 60% !important; border: 1px solid; padding-left: 5px;' value='" + elementValue.MonthlyPercentage + "'/>" +
                        "</div >" +
                        "<div class='input-group input-group-btn'>" +
                        "<button class='btn btn-primary search pull-left' type='button'><i class='fa fa-dollar'></i></button>" +
                        "<input type='text' class='form-control form-control-small' id='txtpayamtMo" + elementValue.ApplicantID + "' style='width: 60% !important; border: 1px solid; padding-left: 5px; text-align:right;' value='" + parseFloat(elementValue.MonthlyPayment).toFixed(2) + "'/>" +
                        "</div >" +
                        "</td></tr>";
                }
              
                if (elementValue.Type == "Primary Applicant" || elementValue.Type == "Co-Applicant" || elementValue.Type == "Guarantor") {
                    adminfess = $("#lblFNLAmount").text();
                    totpaid += parseFloat(adminfess);
                    if (elementValue.Type == "Primary Applicant") {
                        totalFinalFees += parseFloat(adminfess);
                        pprhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:18%; padding:6px;'>" + elementValue.Type + " </td><td style='width:20%; padding:6px;'>" + elementValue.FirstName + " " + elementValue.LastName + "</td><td style='width:14%; padding:6px;'>$" + adminfess + "</td><td style='width:14%; padding:6px;'><input type='checkbox' id='chkPayAppFees' checked disabled/></td><td></td></tr>";
                    } else {
                        pprhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:18%; padding:6px;'>" + elementValue.Type + " </td><td style='width:20%; padding:6px;'>" + elementValue.FirstName + " " + elementValue.LastName + "</td><td style='width:14%; padding:6px;'>$" + adminfess + "</td><td style='width:14%; padding:6px;'><input type='checkbox' class='chkPayAppFees' id='chkPayAppFees1' onclick='addAppFess(" + adminfess + ")'/></td><td><input type='button' id='btnSendPayLink' style='width:150px;' onclick='sendPayLinkEmail(\"" + elementValue.Email + "\")' value='Send Payment Link'/></td></tr>";
                    }

               
                }
                
                if (elementValue.Type == "Co-Applicant" || elementValue.Type == "Guarantor") {
                    //Sachin's work 22-10
                    $("#btnsendemail").removeClass("hidden");
                    if (elementValue.Email != null) {
                        emailhtml += "<tr data-id='" + elementValue.ApplicantID + "'><td style='width:20%; padding:6px;'>" + elementValue.FirstName + " " + elementValue.LastName + "</td><td style='width:18%; padding:6px;'>" + elementValue.Email + " </td><td style='width:30%; padding:6px;'><input type='checkbox' onclick='addEmail(\"" + elementValue.Email + "\")' id='chkEmail" + elementValue.ApplicantID + "' style='width:25%; border:1px solid;' /></td></tr>";
                    }
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
                    console.log($("#txtpayper" + elementValue.ApplicantID).val());
                    console.log($("#txtpayperMo" + elementValue.ApplicantID).val());
                    if ($("#txtpayper" + elementValue.ApplicantID).val() == 0) {
                        $("#txtpayper" + elementValue.ApplicantID).val(100);
                        checkPercentage();
                    }
                    if ($("#txtpayperMo" + elementValue.ApplicantID).val() == 0) {
                        $("#txtpayperMo" + elementValue.ApplicantID).val(100);
                        checkPercentage();
                    }
                }

                function checkPercentage() {
                    var chargesPecentage = $("#txtpayper" + elementValue.ApplicantID).val();

                    var dueatmovein = unformatText($("#lbtotdueatmov6").text());
                    //console.log("Hi");

                    //console.log("DM:" + dueatmovein);

                    var perCharges = ((chargesPecentage * parseFloat(dueatmovein)) / 100);
                    $("#txtpayamt" + elementValue.ApplicantID).val(perCharges.toFixed(2));


                    var sum = parseFloat(0);
                    $(".payper").each(function () {
                        sum += parseFloat(this.value);

                    });
                    localStorage.setItem("percentage", sum);


                    var chargesAmount = $("#txtpayamt" + elementValue.ApplicantID).val();


                    var chargesPer = ((chargesAmount * 100) / parseFloat(dueatmovein));
                    $("#txtpayper" + elementValue.ApplicantID).val(parseFloat(chargesPer).toFixed(2));

                    var monthlyPercentage = $("#txtpayperMo" + elementValue.ApplicantID).val();
                    var monthlyPayment = unformatText($("#lblRFPTotalMonthlyPayment").text());

                    console.log(monthlyPayment);

                    var perMonth = monthlyPercentage * parseFloat(monthlyPayment, 10) / 100.0;
                    $("#txtpayamtMo" + elementValue.ApplicantID).val(parseFloat(perMonth).toFixed(2));

                    var sumMo = parseFloat(0);
                    $(".payperMo").each(function () {
                        sumMo += parseFloat(this.value);

                    });
                    localStorage.setItem("percentageMo", sumMo);
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
                    $("#txtpayamtMo" + elementValue.ApplicantID).val(parseFloat(perMonth).toFixed(2));

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
            $("#tblApplicant").append("<div class='col-sm-3 box-two proerty-item'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><label></br><a href='javascript:void(0)' id='btnAddApplicant' onclick='addApplicant(1)'><i class='fa fa-plus-circle'></i> Add Co-Applicant</a></label></div></div></div></div>");
            $("#tblApplicantMinor").append("<div class='col-sm-3 box-two proerty-item'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><label></br><a href='javascript:void(0)' id='btnAddApplicant' onclick='addApplicant(2)'><i class='fa fa-plus-circle'></i> Add Minor</a></label></div></div></div></div>");
            $("#tblApplicantGuarantor").append("<div class='col-sm-3 box-two proerty-item'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><div class='form-group col-sm-12'><label></br><a href='javascript:void(0)' id='btnAddApplicant' onclick='addApplicant(3)'><i class='fa fa-plus-circle'></i> Add Guarantor</a></label></div></div></div></div>");

        }
    });
}
var addAppFess = function (appFees) {
    var totfees = unformatText($("#totalFinalFees").text());
    if ($(".chkPayAppFees").is(':checked')) {
        $("#btnSendPayLink").addClass("hidden");
        totfees = parseFloat(totfees) + parseFloat(appFees);
        $("#totalFinalFees").text("$" + parseFloat(totfees).toFixed(2));
    } else {
        totfees = parseFloat(totfees) - parseFloat(appFees);
        $("#totalFinalFees").text("$" + parseFloat(totfees).toFixed(2));
        $("#btnSendPayLink").removeClass("hidden");
    }
}
var goToEditApplicant = function (aid) {

    if (aid != null) {

        $("#hndApplicantID").val(aid);
        var model = { id: aid,FromAcc:0 };
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
                    $("#iconCalenderApplicant").click(function () {
                        $("#txtHDateOfBirth").focus();
                    });
                    if (response.model.OtherGender == '3') {
                        $("#txtApplicantOtherGender").val(response.model.OtherGender);
                    }
                    else {
                        $("#txtApplicantOtherGender").val('');
                    }

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
    if (!petName) {
        msg += "Enter Pet Name</br>";
    }
    if (!breed) {
        msg += "Enter Pet Breed</br>";
    }
    if (hiddenPetPicture == '0') {
        if (document.getElementById('pet-picture').files.length == '0') {
            msg += "Please Upload Pet Picture</br>";
        }
    }
    if (hiddenPetVaccinationCertificate == '0') {
        if (document.getElementById('filePetVaccinationCertificate').files.length == '0') {
            msg += "Please Upload Pet Vaccination Certificate</br>";
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
            localStorage.setItem('tenantIds', response.Msg);

            var str = localStorage.getItem('tenantIds');
            var tId = str.split(',');
            document.getElementById('hdnOPId').value = tId[0];
            //$("#hdnOPId").val(tId[0]);

            getPetLists();


            $.alert({
                title: "",
                content: "Progress Saved.",
                type: 'blue'
            });


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

                html += "<td>" + elementValue.PetName + "</td>";
                html += "<td>" + elementValue.Breed + "</td>";
                html += "<td>" + elementValue.Weight + "</td>";
                html += "<td>" + elementValue.VetsName + "</td>";
                html += "<td>";
                html += "<a style='background: transparent; margin-right:10px' href='JavaScript:Void(0);' id='updatePetInfo' onclick='getPetInfo(" + elementValue.PetID + ")'><span class='fa fa-edit' ></span></a>";
                html += "<a style='background: transparent; margin-right:10px' href='JavaScript:Void(0);' onclick='delPet(" + elementValue.PetID + ")'><span class='fa fa-trash' ></span></a>";
                html += "<a href='../../Content/assets/img/pet/" + elementValue.PetVaccinationCertificate + "' download=" + elementValue.PetVaccinationCertificate + " target='_blank'><span class='fa fa-download'></span></a>";
                html += "</td >";
                html += "</tr>";
                $("#tblPet>tbody").append(html);
            });

            getTenantPetPlaceData();

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
            $("#txtpetName").val(response.model.PetName);
            $("#txtpetVetsName").val(response.model.VetsName);
            $("#ddlpetType").val(response.model.PetType);
            $("#txtpetBreed").val(response.model.Breed);
            $("#txtpetWeight").val(response.model.Weight);
            $("#txtpetAge").val(response.model.Age);
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
        }
    });
};
var clearPet = function () {
    $("#txtpetName").val('');
    $("#txtpetBreed").val('');
    $("#txtpetWeight").val('');
    $("#txtpetVetsName").val('');
    document.getElementById('filePetPictireShow').value = '';
    document.getElementById('filePetVaccinationCertificate').value = '';
    $("#filePetPictireShow").html('Choose a file&hellip;');
    $("#filePetVaccinationCertificateShow").html('Choose a file&hellip;');
    $("#hndPetPicture").val('0');
    $("#hndOriginalPetPicture").val('');
    $("#hndPetVaccinationCertificate").val('0');
    $("#hndOriginalPetVaccinationCertificate").val('');
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
    if ($("#hndVehicleRegistation").val() == '0') {
        if (document.getElementById("fileUploadVehicleRegistation").files.length == '0') {
            msg += "Upload The Vehicle Registration Certificate</br>";
        }
    }
    if (vtype == "0") {
        msg += "Enter Vehicle Type</br>";
    }
    if (vyear == '0') {
        msg += "Enter Vehicle Year</br>";
    }
    if (!vmake) {
        msg += "Enter Vehicle Make</br>";
    }
    if (!vlicence) {
        msg += "Enter Vehicle Licence</br>";
    }
    if (vstate == "0") {
        msg += "Select Vehicle State</br>";
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
                html += "<td><a style='background: transparent; margin-right:10px' href='JavaScript:Void(0);' onclick='delVehicle(" + elementValue.Vehicle_ID + ")'><span class='fa fa-trash' ></span></a>";
                html += "<a style='background: transparent;' href='../../Content/assets/img/VehicleRegistration/" + elementValue.VehicleRegistration + "' download=" + elementValue.VehicleRegistration + " target='_blank'><span class='fa fa-download' style='background: transparent;'></span></a></td>";

                html += "</tr>";
                $("#tblVehicle>tbody").append(html);

            });

        }
    });
}

var clearVehicle = function () {
    $("#hndVehicleID").val(0);
    $("#txtVehicleOwnerName").val("");
    document.getElementById('fileUploadVehicleRegistation').value = '';
    $("#VehicleRegistationShow").html("Choose a file&hellip;");
    $("#ddlVehicleType").val("0");
    $("#ddlVehicleyear").val("0");
    $("#txtVehicleMake").val("");
    $("#txtVehicleModel").val("");
    $("#txtVehicleColor").val("");
    $("#txtVehicleLicence").val("");
    $("#ddlVState").val(0);
    $("#txtVehicleNote").val("");
    $("#hndVehicleRegistation").val("0");
    $("#hndOriginalVehicleRegistation").val("0");
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

    var fileUpload1 = $("#hndFileUploadName1").val();
    var originalFileUpload1 = $("#hndOriginalFileUploadName1").val();
    var fileUpload2 = $("#hndFileUploadName2").val();
    var originalFileUpload2 = $("#hndOriginalFileUploadName2").val();
    var fileUpload3 = $("#hndFileUploadName3").val();
    var originalFileUpload3 = $("#hndOriginalFileUploadName3").val();
    var filePassport = $("#hndPassportUploadName").val();
    var originalFilePassport = $("#hndOriginalPassportUploadName").val();
    var fileIdentity = $("#hndIdentityUploadName").val();
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

            //$("#ddlIsInter").val(response.model.IsInternational).change();
            //$("#ddladdHistory").val(response.model.IsAdditionalRHistory).change();
            $("#txtFirstNamePersonal").val(response.model.FirstName);
            $("#txtMiddleInitial").val(response.model.MiddleInitial);
            $("#txtLastNamePersonal").val(response.model.LastName);
            $("#txtDateOfBirth").val(response.model.DateOfBirthTxt);
            $("#ddlGender").val(response.model.Gender).change();
            $("#txtEmailNew").val(response.model.Email);
            $("#txtMobileNumber").val(formatPhoneFax(response.model.Mobile));
            $("#txtPassportNum").val(response.model.PassportNumber);
            $("#txtCOI").val(response.model.CountryIssuance);
            $("#txtDateOfIssuance").val(response.model.DateIssuanceTxt);
            $("#txtDateOfExpiration").val(response.model.DateExpireTxt);
            $("#ddlDocumentTypePersonal").val(response.model.IDType).change();
            setTimeout(function () {
                $("#ddlStatePersonal").find("option[value='" + response.model.State + "']").attr('selected', 'selected');
            }, 1500);
            //$("#txtIDNumber").val(response.model.IDNumber);
            //$("#txtSSNNumber").val(response.model.SSN);
            //$("#hndIDNumber").val(response.model.IDNumber);
            //$("#hndSSNNumber").val(response.model.SSN);
            //if (response.model.IDNumber) {
            //    var mainStr = response.model.IDNumber,
            //        vis = mainStr.slice(-4),
            //        countNum = '';

            //    for (var i = (mainStr.length) - 4; i > 0; i--) {
            //        countNum += '*';
            //    }

            //    $("#txtIDNumber").val(countNum + vis);
            //}
            //if (response.model.SSN) {
            //    var mainStr = response.model.SSN,
            //        vis = mainStr.slice(-4),
            //        countNum = '';

            //    for (var i = (mainStr.length) - 4; i > 0; i--) {
            //        countNum += '*';
            //    }

            //    $("#txtSSNNumber").val(countNum + vis);
            //}
            //$("#txtCountry").val(response.model.Country);
            //console.log(response.model.SSN);
            if (!response.model.SSN) {
                $("#txtSSNNumber").val('');
                response.model.SSN = "";
            }
            else {
                $("#txtSSNNumber").val("***-**-" + response.model.SSN.substr(response.model.SSN.length - 5, 4));
            }
            $("#txtSSNNumber").data("value", response.model.SSN);
            $("#hndSSNNumber").val(response.model.SSN);
            if (!response.model.IDNumber) {
                $("#txtIDNumber").val('');
                response.model.IDNumber = "";
            } else {
                if (response.model.IDNumber.length > 4) {
                    $("#txtIDNumber").val("*".repeat(response.model.IDNumber.length - 4) + response.model.IDNumber.substr(response.model.IDNumber.length - 4, 4));
                }
                else {
                    $("#txtIDNumber").val(response.model.IDNumber);
                }
            }
            $("#txtIDNumber").data("value", response.model.IDNumber);

            $("#txtAddress1").val(response.model.HomeAddress1);
            $("#txtAddress2").val(response.model.HomeAddress2);
            //fillCityListHome(response.model.StateHome);
            setTimeout(function () {
                $("#ddlStateHome").find("option[value='" + response.model.StateHome + "']").attr('selected', 'selected');
            }, 1500);

            $("#txtZip").val(response.model.ZipHome);
            $("#ddlRentOwn").val(response.model.RentOwn);
            //$("#txtMoveInDate").val(response.model.MoveInDateTxt);
            $("#txtMonthlyPayment").val(formatMoney(response.model.MonthlyPayment));
            $("#txtReasonforleaving").val(response.model.Reason);
            $("#txtEmployerName").val(response.model.EmployerName);
            $("#txtJobTitle").val(response.model.JobTitle);
            $("#ddlJobType").val(response.model.JobType);
            $("#txtStartDate").val(response.model.StartDateTxt);
            $("#txtAnnualIncome").val(formatMoney(response.model.Income));
            $("#txtAddAnnualIncome").val(formatMoney(response.model.AdditionalIncome));
            $("#txtSupervisiorName").val(response.model.SupervisorName);
            $("#txtSupervisiorPhone").val(formatPhoneFax(response.model.SupervisorPhone));
            $("#txtSupervisiorEmail").val(response.model.SupervisorEmail);
            //$("#txtCountryOffice").val(response.model.OfficeCountry);
            $("#txtofficeAddress1").val(response.model.OfficeAddress1);
            $("#txtofficeAddress2").val(response.model.OfficeAddress2);
            //fillCityListEmployee(response.model.OfficeState);
            setTimeout(function () {
                $("#ddlStateEmployee").find("option[value='" + response.model.OfficeState + "']").attr('selected', 'selected');
            }, 1500);
            setTimeout(function () {
                //$("#ddlCityEmployee").find("option[value='" + response.model.OfficeCity + "']").attr('selected', 'selected');

            }, 2000);
            $("#txtZipOffice").val(response.model.OfficeZip);
            $("#txtRelationship").val(response.model.Relationship);
            $("#txtEmergencyFirstName").val(response.model.EmergencyFirstName);
            $("#txtEmergencyLastName").val(response.model.EmergencyLastName);
            $("#txtEmergencyMobile").val(formatPhoneFax(response.model.EmergencyMobile));
            $("#txtEmergencyHomePhone").val(formatPhoneFax(response.model.EmergencyHomePhone));
            $("#txtEmergencyWorkPhone").val(formatPhoneFax(response.model.EmergencyWorkPhone));
            $("#txtEmergencyEmail").val(response.model.EmergencyEmail);
            //$("#txtEmergencyCountry").val(response.model.EmergencyCountry);
            $("#txtEmergencyAddress1").val(response.model.EmergencyAddress1);
            $("#txtEmergencyAddress2").val(response.model.EmergencyAddress2);
            //fillCityListContact(response.model.EmergencyStateHome);
            setTimeout(function () {
                $("#ddlStateContact").find("option[value='" + response.model.EmergencyStateHome + "']").attr('selected', 'selected');
            }, 1500);

            $("#txtEmergencyZip").val(response.model.EmergencyZipHome);
            //17082019 - start
            $("#ddlCityHome").val(response.model.CityHome);
            $("#ddlCityContact").val(response.model.EmergencyCityHome);
            $("#ddlCityEmployee").val(response.model.OfficeCity);
            if (response.model.Gender == '3') {
                $("#txtOtherGender").val(response.model.OtherGender);
            }
            else {
                $("#txtOtherGender").val('');
            }

            $("#txtMoveInDateFrom").val(response.model.MoveInDateFromTxt);
            $("#txtMoveInDateTo").val(response.model.MoveInDateToTxt);

            setTimeout(function () {
                $("#txtCountry").find("option[value='" + response.model.Country + "']").attr('selected', 'selected');
                $("#txtCountry2").find("option[value='" + response.model.Country2 + "']").attr('selected', 'selected');
            }, 2000);
            setTimeout(function () {
                $("#txtCountryOffice").find("option[value='" + response.model.OfficeCountry + "']").attr('selected', 'selected');
            }, 2000);
            setTimeout(function () {
                $("#txtEmergencyCountry").find("option[value='" + response.model.EmergencyCountry + "']").attr('selected', 'selected');
            }, 2000);
            //17082019 - end

            document.getElementById("ddlStatePersonal").selectedIndex = response.model.State;
            document.getElementById("ddlStateHome").selectedIndex = response.model.StateHome;
            document.getElementById("ddlStateEmployee").selectedIndex = response.model.OfficeState;
            document.getElementById("ddlStateContact").selectedIndex = response.model.EmergencyStateHome;
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
                if (response.model.TaxReturn == "0") {
                    $("#hndHasTaxReturnFile1").val("0");
                }
                else {
                    $("#hndHasTaxReturnFile1").val("1");
                }
            } else {
                $("#hndHasTaxReturnFile1").val("0");
            }
            if (response.model.TaxReturn2 != "") {
                if (response.model.TaxReturn2 == "0") {
                    $("#hndHasTaxReturnFile2").val("0");
                }
                else {
                    $("#hndHasTaxReturnFile2").val("1");
                }
            } else {
                $("#hndHasTaxReturnFile2").val("0");
            }
            if (response.model.TaxReturn3 != "") {
                if (response.model.TaxReturn3 == "0") {
                    $("#hndHasTaxReturnFile3").val("0");
                }
                else {
                    $("#hndHasTaxReturnFile3").val("1");
                }
            } else {
                $("#hndHasTaxReturnFile3").val("0");
            }
            //alert(response.model.HaveVehicle + "  " + response.model.HavePet);
            if (response.model.HaveVehicle == true) {
                $("#chkDontHaveVehicle").iCheck('check');
            }
            else {
                $("#chkDontHaveVehicle").iCheck('uncheck');
            }
            if (response.model.HavePet == true) {
                $("#chkDontHavePet").iCheck('check');
                $("#chkDontHavePet").prop('disabled', 'disabled');
            }
            else {
                $("#chkDontHavePet").iCheck('uncheck');
                $("#chkDontHavePet").prop('disabled', 'disabled');
            }

            $("#hndPassportUploadName").val(response.model.PassportDocument);
            $("#hndOriginalPassportUploadName").val(response.model.UploadOriginalPassportName);
            $("#hndIdentityUploadName").val(response.model.IdentityDocument);
            $("#hndOriginalIdentityUploadName").val(response.model.UploadOriginalIdentityName);
            $("#hndFileUploadName1").val(response.model.TaxReturn);
            $("#hndOriginalFileUploadName1").val(response.model.UploadOriginalFileName1);
            $("#hndFileUploadName2").val(response.model.TaxReturn2);
            $("#hndOriginalFileUploadName2").val(response.model.UploadOriginalFileName2);
            $("#hndFileUploadName3").val(response.model.TaxReturn3);
            $("#hndOriginalFileUploadName3").val(response.model.UploadOriginalFileName3);

            //To Display File Name On Uploader
            if (response.model.UploadOriginalPassportName != '') {
                $("#fileUploadPassportShow").text(response.model.UploadOriginalPassportName);
            }
            if (response.model.UploadOriginalIdentityName != '') {
                $("#fileUploadIdentityShow").text(response.model.UploadOriginalIdentityName);
            }
            if (response.model.UploadOriginalFileName1 != '') {
                if (response.model.UploadOriginalFileName1 != '0') {
                    $("#fileUploadTaxReturn1Show").text(response.model.UploadOriginalFileName1);
                }
            }
            if (response.model.UploadOriginalFileName2 != '') {
                if (response.model.UploadOriginalFileName2 != '0') {
                    $("#fileUploadTaxReturn2Show").text(response.model.UploadOriginalFileName2);
                }
            }
            if (response.model.UploadOriginalFileName3 != '') {
                if (response.model.UploadOriginalFileName3 != '0') {
                    $("#fileUploadTaxReturn3Show").text(response.model.UploadOriginalFileName3);
                }
            }
            //alert(response.model.IsPaystub);

            if (response.model.IsPaystub == true) {

                $("#rbtnPaystub").iCheck('check');
            }
            else {
                $("#rbtnFedralTax").iCheck('check');
            }

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

function showFloorPlan(flid) {
    setTimeout(function () { $("#returnButton").removeClass("hidden"); $("#returnButton").html("Return to List View"); }, 1000);
    $("#UnitListDesc").text("Choose any unit in green to see more information including a video and complete layout of your unit. ");
    var bedroom = 0;
    if (!$("#ddlRoom").val()) {
        bedroom = 0;
    }
    else {
        bedroom = $("#ddlRoom").val();
    }
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }
    var availdate = $("#txtDate").val();
    var leaseterm = $("#hndLeaseTermID").val();
    var model = { FloorID: flid, AvailableDate: availdate, Bedroom: bedroom, MaxRent: maxrent, LeaseTermID: leaseterm };
    $.ajax({
        url: "/Property/GetPropertyFloorDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#popUnitPlan").empty();
            $("#popFloorCoordinate").removeClass("hidden");
            $("#popUnitDet").addClass("hidden");
            $("#popPromotion").addClass("hidden");
            $("#divUdet").addClass("hidden");
            //$("#lblPropFloor").text("Selected : Floor" + response.model.FloorNo);

            var html = "<h3 style='color: #4d738a; text-align:center;'>Selected : Floor " + response.model.FloorNo + "</h3>";
            html += "<div class='col-sm-12' style='background:#fff;text-align:center!important;'><span style='background-color:#006400;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Available</span>&nbsp;&nbsp;<span style='background-color:#ffff00;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Other Options</span>&nbsp;&nbsp;<span style='background-color:#FF0000;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Unavailable</span></div><br/><br/>";
            //<div class='col-sm-4'><div style='background-color: #fa6500; width: 10px; height: 10px'></div><span>Not Available</span></div> <div class='col-sm-4'><div style='background-color:red;width:10px;height:10px'></div > <span>Available</span></div>
            html += "<img src ='/content/assets/img/plan/" + response.model.FloorPlan + "' id='imgFloorCoordinate' class='' usemap='#unitimg'>";
            html += "<map name='unitimg' id='imgFloorCoordinateDiv'>";
            $.each(response.model.lstUnitFloor, function (elementType, value) {
                if (value.IsAvail == 1) {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UAarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'>";
                    //html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Yes</span></div>";
                } else if (value.IsAvail == 2) {

                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UYarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")' >";
                    // html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Other</span></div>";
                } else {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips Uarea' coords='" + value.Coordinates + "' >";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: No</span></div>";

                }

                if (value.UID == $("#hndUID").val()) {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UUUarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")' >";
                    //html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Yes</span></div>";
                }
            });
            html += "</map><br/>";
            $("#popUnitPlan").append(html);
            $("#imgFloorCoordinate").maphilight();

            //$('.active_area').data('maphilight', { alwaysOn: false }).trigger('alwaysOn.maphilight');
            $('.Uarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'FF0000', strokeColor: 'FF0000', }).trigger('alwaysOn.maphilight');
            $('.UAarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: '006400', strokeColor: '006400', }).trigger('alwaysOn.maphilight');
            $('.UYarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'ffff00', strokeColor: 'ffff00', }).trigger('alwaysOn.maphilight');


            //$('.UUUarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'white', strokeColor: '#fff', }).trigger('alwaysOn.maphilight');
            $('.UUUarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: '4d738a', strokeColor: '4d738a', fillOpacity: 0.9 }).trigger('alwaysOn.maphilight');

            $(".tooltips").mouseout(function () { $(".divtooltipUnit").addClass("hidden"); });
            $(".tooltips").mouseover(function (e) {
                var offset = $(this).offset();
                var X = (e.pageX - offset.left);
                var Y = (e.pageY - offset.top);
                X = X - 25;//X = X - 30
                if (Y < 0) {
                    Y = Y + 615;//Y = Y + 525
                }
                else {
                    Y = Y + 390;//Y = Y + 300
                }
                var thisId = $(this).attr('id');
                var divID = thisId.split("_");
                $(".divtooltipUnit").addClass("hidden");
                $("#floorunId_" + divID[1]).removeClass("hidden");
                $("#floorunId_" + divID[1]).css({ top: Y, left: X, position: 'absolute' });
            });


        }
    });

}
function getPropertyUnitListByFloor(flid) {

    var bedroom = 0;
    if (!$("#ddlRoom").val()) {
        bedroom = 0;
    }
    else {
        bedroom = $("#ddlRoom").val();
    }
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }
    var availdate = $("#txtDate").val();
    var leaseterm = $("#hndLeaseTermID").val();

    var model = { FloorID: flid, AvailableDate: availdate, Bedroom: bedroom, MaxRent: maxrent, LeaseTermID: leaseterm };
    $.ajax({
        url: "/Property/GetPropertyFloorDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#popUnitPlan").empty();
            $("#popFloorCoordinate").removeClass("hidden");
            $("#popUnitDet").addClass("hidden");
            $("#popPromotion").addClass("hidden");
            $("#divUdet").addClass("hidden");
            //$("#lblPropFloor").text("Selected : Floor" + response.model.FloorNo);

            var html = "<h3 style='color: #4d738a; text-align:center;'>Selected : Floor " + response.model.FloorNo + "</h3>";
            html += "<div class='col-sm-12' style='background:#fff;text-align:center!important;'><span style='background-color:#006400;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Available</span>&nbsp;&nbsp;<span style='background-color:#ffff00;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Other Options</span>&nbsp;&nbsp;<span style='background-color:#FF0000;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Unavailable</span></div><br/><br/>";
            //<div class='col-sm-4'><div style='background-color: #fa6500; width: 10px; height: 10px'></div><span>Not Available</span></div> <div class='col-sm-4'><div style='background-color:red;width:10px;height:10px'></div > <span>Available</span></div>
            html += "<img src ='/content/assets/img/plan/" + response.model.FloorPlan + "' id='imgFloorCoordinate' class='' usemap='#unitimg'>";
            html += "<map name='unitimg' id='imgFloorCoordinateDiv'>";
            $("#listUnit>tbody").empty();
            $.each(response.model.lstUnitFloor, function (elementType, value) {
                if (value.IsAvail == 1) {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UAarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'>";
                    //html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Yes</span></div>";
                } else if (value.IsAvail == 2) {

                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UYarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")' >";
                    // html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Other</span></div>";
                } else {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips Uarea' coords='" + value.Coordinates + "' >";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: No</span></div>";

                }

                if (value.UID == $("#hndUID").val()) {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UUUarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")' >";
                    //html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Yes</span></div>";
                }

                var ulhtml = " <tr id='unitdiv" + value.UID + "' data-floorid = '" + value.FloorNoText + "'><td><a href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'><h5 style='width: 80px;'>#" + value.UnitNo + " </h5></a> </td><td style='text-align: center;width=100px'>$" + value.Current_Rent + "</td><td style='text-align: center;width=80px'>" + value.Premium + "</td><td style='text-align: center;width=100px'>" + value.AvailableDateText + "</td></tr>";
                $("#listUnit>tbody").append(ulhtml);
            });
            html += "</map><br/>";
            $("#popUnitPlan").append(html);
            $("#imgFloorCoordinate").maphilight();

            //$('.active_area').data('maphilight', { alwaysOn: false }).trigger('alwaysOn.maphilight');
            $('.Uarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'FF0000', strokeColor: 'FF0000', }).trigger('alwaysOn.maphilight');
            $('.UAarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: '006400', strokeColor: '006400', }).trigger('alwaysOn.maphilight');
            $('.UYarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'ffff00', strokeColor: 'ffff00', }).trigger('alwaysOn.maphilight');


            //$('.UUUarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'white', strokeColor: '#fff', }).trigger('alwaysOn.maphilight');
            $('.UUUarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: '4d738a', strokeColor: '4d738a', fillOpacity: 0.9 }).trigger('alwaysOn.maphilight');

            $(".tooltips").mouseout(function () { $(".divtooltipUnit").addClass("hidden"); });
            $(".tooltips").mouseover(function (e) {
                var offset = $(this).offset();
                var X = (e.pageX - offset.left);
                var Y = (e.pageY - offset.top);
                X = X - 30;
                if (Y < 0) {
                    Y = Y + 525;
                }
                else {
                    Y = Y + 300;
                }
                var thisId = $(this).attr('id');
                var divID = thisId.split("_");
                $(".divtooltipUnit").addClass("hidden");
                $("#floorunId_" + divID[1]).removeClass("hidden");
                $("#floorunId_" + divID[1]).css({ top: Y, left: X, position: 'absolute' });
            });


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
    var msg = '';
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

    if ($("#txtAddress12").val() == '') {
        msg += 'Please Fill Address 1</br>'
    }
    if ($("#ddlStateHome2").val() == '0') {
        msg += 'Please Select State</br>'
    }
    if ($("#ddlCityHome2").val() == '') {
        msg += 'Please Fill City</br>'
    }
    if ($("#txtZip2").val() == '') {
        msg += 'Please Fill Zip Code</br>'
    }
    if ($("#txtMoveInDateFrom2").val() == '') {
        msg += 'Please Fill Move In Date</br>'
    }
    if ($("#txtMoveInDateTo2").val() == '') {
        msg += 'Please Fill Move Out Date</br>'
    }
    if (msg != '') {
        $.alert({
            title: "",
            content: msg,
            type: 'blue'
        });
        return
    }
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
                html += "<a style='background: transparent; margin-right:10px' id='updateAHRInfo' href='JavaScript:Void(0)' onclick='getApplicantHistoryInfo(" + elementValue.AHID + ")'><span class='fa fa-edit' ></span></a>";
                html += "<a style='background: transparent;' href='JavaScript:Void(0)' onclick='delApplicantHistory(" + elementValue.AHID + ")'><span class='fa fa-trash' ></span></a></td>";
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
            $("#txtCountry2").find("option[value='" + response.model.Country2 + "']").attr('selected', 'selected');

            $("#txtAddress12").val(response.model.HomeAddress1);
            $("#txtAddress22").val(response.model.HomeAddress2);
            $("#ddlStateHome2").find("option[value='" + response.model.StateHome + "']").attr('selected', 'selected');
            $("#ddlCityHome2").val(response.model.CityHome);
            $("#txtZip2").val(response.model.ZipHome);
            $("#ddlRentOwn2").val(response.model.RentOwn);
            $("#txtMoveInDateFrom2").val(response.model.MoveInDateFromTxt);
            $("#txtMoveInDateTo2").val(response.model.MoveInDateToTxt);
            $("#txtMonthlyPayment2").val(response.model.MonthlyPayment);
            $("#txtReasonforleaving2").val(response.model.Reason);
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
        lstemailsend: addEmailArray, ProspectId: $("#hdnOPId").val()
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
                                content: "Email Sent Successfully",
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

var sendPayLinkEmail = function (emil) {

    var model = {
        Email: emil, ProspectId: $("#hdnOPId").val()
    };

    $.alert({
        title: "",
        content: "Are you sure to send Payment Link Email?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/ApplyNow/SendPayLinkEmail",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $.alert({
                                title: "",
                                content: "Email Sent Successfully",
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
    $("#divLoader").show();
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
            getVehicleLists();
            $("#divLoader").hide();
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

//Upload 1,2,3
var taxReturnFileUpload1 = function () {
    $formData = new FormData();

    var upload1 = document.getElementById('fileUploadTaxReturn1');

    for (var i = 0; i < upload1.files.length; i++) {
        $formData.append('file-' + i, upload1.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/TaxFileUpload1',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndFileUploadName1').val(response.model.tempUpload1);
            $('#hndOriginalFileUploadName1').val(response.model.UploadOriginalFileName1);

            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};
var taxReturnFileUpload2 = function () {
    $formData = new FormData();

    var upload2 = document.getElementById('fileUploadTaxReturn2');

    var fileNameUp2 = $('#hndFileUploadName2').val();
    var orginalFileNameUp2 = $('#hndOriginalFileUploadName2').val();

    for (var i = 0; i < upload2.files.length; i++) {
        $formData.append('file-' + i, upload2.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/TaxFileUpload2',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndFileUploadName2').val(response.model.tempUpload2);
            $('#hndOriginalFileUploadName2').val(response.model.UploadOriginalFileName2);

            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};
var taxReturnFileUpload3 = function () {
    $formData = new FormData();

    var upload3 = document.getElementById('fileUploadTaxReturn3');

    for (var i = 0; i < upload3.files.length; i++) {
        $formData.append('file-' + i, upload3.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/TaxFileUpload3',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndFileUploadName3').val(response.model.tempUpload3);
            $('#hndOriginalFileUploadName3').val(response.model.UploadOriginalFileName3);

            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

//Upload Passport
var uploadPassport = function () {
    $formData = new FormData();

    var passportFile = document.getElementById('fileUploadPassport');

    for (var i = 0; i < passportFile.files.length; i++) {
        $formData.append('file-' + i, passportFile.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/UploadPassport',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndPassportUploadName').val(response.model.tempPassportUpload);
            $('#hndOriginalPassportUploadName').val(response.model.UploadOriginalPassportName);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

//Upload Identity Documents
var uploadIdentityDocument = function () {
    $formData = new FormData();

    var identityFile = document.getElementById('fileUploadIdentity');

    for (var i = 0; i < identityFile.files.length; i++) {
        $formData.append('file-' + i, identityFile.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/UploadIdentity',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndIdentityUploadName').val(response.model.tempIdentityUpload);
            $('#hndOriginalIdentityUploadName').val(response.model.UploadOriginalIdentityName);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

//Upload Pet Documents
var uploadPetPhoto = function () {
    $formData = new FormData();

    var petPhotoFile = document.getElementById('pet-picture');

    for (var i = 0; i < petPhotoFile.files.length; i++) {
        $formData.append('file-' + i, petPhotoFile.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/UploadPetPhoto',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndPetPicture').val(response.model.TempPetNameFile);
            $('#hndOriginalPetPicture').val(response.model.OriginalPetNameFile);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

var uploadPetVaccination = function () {
    $formData = new FormData();

    var petVaccinationCertificateFile = document.getElementById('filePetVaccinationCertificate');

    for (var i = 0; i < petVaccinationCertificateFile.files.length; i++) {
        $formData.append('file-' + i, petVaccinationCertificateFile.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/UploadPetVaccinationCertificate',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndPetVaccinationCertificate').val(response.model.TempPetVaccinationCertificateFile);
            $('#hndOriginalPetVaccinationCertificate').val(response.model.OriginalPetVaccinationCertificateFile);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

//Upload Vehicle Documents

var uploadVehicleCertificate = function () {
    $formData = new FormData();

    var vehicleCertificate = document.getElementById('fileUploadVehicleRegistation');

    for (var i = 0; i < vehicleCertificate.files.length; i++) {
        $formData.append('file-' + i, vehicleCertificate.files[i]);
    }

    $.ajax({
        url: '/ApplyNow/UploadVehicleRegistation',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $('#hndVehicleRegistation').val(response.model.TempVehicleRegistation);
            $('#hndOriginalVehicleRegistation').val(response.model.OriginalVehicleRegistation);
            $.alert({
                title: "",
                content: "File uploaded Successfully.",
                type: 'blue'
            });
        }
    });
};

var getTenantPetPlaceData = function () {

    var ProspectID = $("#hdnOPId").val();

    var model = {
        id: ProspectID
    };
    $.ajax({
        url: '/ApplyNow/GetTenantPetPlaceData',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            var valueCount = response.model.PetPlaceID;
            var rowCount = $("#tblPet >tbody").children().length;
            $("#hndPetPlaceCount").val(valueCount);

            if ($("#chkDontHavePet").is(':checked')) {
                $("#btnAddPet").attr("disabled", true);
                $("#btnAddPet").css("background-color", "#b4ada5");
            }
            if (!valueCount) { valueCount = 0; }

            if (valueCount == 0 && rowCount == 0) {
                $("#tblPet>tbody").empty()
                $("#chkDontHavePetDiv").removeClass("hidden");
                $("#chkDontHavePet").iCheck('check');
                $("#btnAddPet").attr("disabled", true);
                $("#btnAddPet").css("background-color", "#b4ada5");
            }
            else if (rowCount == 1 && valueCount == 1) {
                $("#btnAddPet").attr("disabled", true);
                $("#btnAddPet").css("background-color", "#b4ada5");
                $("#chkDontHavePetDiv").addClass("hidden");
            }
            else if (rowCount == 2 && valueCount == 2) {
                $("#btnAddPet").attr("disabled", true);
                $("#btnAddPet").css("background-color", "#b4ada5");
                $("#chkDontHavePetDiv").addClass("hidden");
            }
            else {
                $("#chkDontHavePetDiv").addClass("hidden");
                $("#chkDontHavePet").iCheck('uncheck');
                $("#btnAddPet").attr("disabled", false);
            }
        }
    });
};


var onFocusApplyNow = function () {

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

    $("#txtPhoneNumber").focusout(function () { $("#txtPhoneNumber").val(formatPhoneFax($("#txtPhoneNumber").val())); })
        .focus(function () {
            $("#txtPhoneNumber").val(unformatText($("#txtPhoneNumber").val()));
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

$(function () {
    $("#txtIDNumber").blur(function () {
        cCardNum = $(this).val();
        $(this).data("value", cCardNum);
        if (cCardNum.length > 4) {
            $(this).val(("*".repeat(cCardNum.length - 4) + cCardNum.substr(cCardNum.length - 4, 4)))
        }
    }).focus(function () {
        $(this).val($(this).data("value"));
    });

});


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

var getDocuDoc = function (envelopeID) {

    $("#divLoader").show();
    var param = { EnvelopeID: envelopeID };
    $.ajax({
        url: "/EmbeddedSigning/GetDocuDocLease",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            window.open("/Content/assets/img/Document/" + response.result, "popupWindow", "width=900,height=600,scrollbars=yes");
           
        }
    });
}
function gotoLogin() {
    window.location.href = "../../../Home/Index";
}

function checkExpiry() {
    QuoteExpires = $("#lblFNLQuoteExpires").text();
    var countDownDate = new Date(QuoteExpires).getTime();
    var x = setInterval(function () {

        var now = new Date().getTime();
        var expiredDate = countDownDate - now;
        var quotationTenantId = $("#hdnOPId").val();
        if (expiredDate < 0) {
            $("#popRentalQualification").modal("hide");
            clearInterval(x);
            document.getElementById("getting-startedTimeRemainingClock").innerHTML = "QUOTE EXPIRED";
            var model = {
                TenantID: quotationTenantId
            };
            $.alert({
                title: "",
                content: "Your Quotation is Expired. Please register again..",
                type: 'red',
                buttons: {
                    yes: {
                        text: 'OK',
                        action: function (yes) {

                            $("#divLoader").show();

                            $.ajax({
                                url: "/ApplyNow/DeleteApplicantTenantID/",
                                type: "post",
                                contentType: "application/json utf-8",
                                data: JSON.stringify(model),
                                dataType: "JSON",
                                success: function (response) {
                                    window.location.href = '/Home';
                                    localStorage.setItem("CheckReload", "Done");
                                    $("#divLoader").hide();
                                    return;
                                }
                            });
                        }

                    }
                }
            });
        }

    }, 1000);

}

var checkEmailAreadyExist = function () {

    var model = { EmailId: $('#txtEmail').val() };
    $("#divLoader").show();
    $.ajax({
        url: "/ApplyNow/CheckEmailAreadyExist",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.model == "Yes Tenant") {
                $.alert({
                    title: "",
                    content: "This email Id is already exist please press Yes to Sign In.",
                    type: 'blue',
                    buttons: {
                        yes: {
                            text: 'Yes',
                            action: function (yes) {
                                localStorage.setItem("userName", $('#txtEmail').val());
                                $('#txtEmail').val('');
                                window.location.replace("/Account/Login");
                                $('#UserName').val(localStorage.getItem("userName"));
                                $('#password').focus();
                            }
                        },
                        no: {
                            text: 'No',
                            action: function (no) {
                                $('#txtEmail').val('');
                                $('#txtEmail').focus();
                            }
                        }
                    }
                });
            }
            else if (response.model == "Yes But Not Tenant") {
                $.alert({
                    title: "",
                    content: "This email Id is already exist please press Yes to Login.",
                    type: 'blue',
                    buttons: {
                        yes: {
                            text: 'Yes',
                            action: function (yes) {
                                var modals = document.getElementById("popSignIn");
                                modals.style.display = "block";
                                $('#UserName').val($('#txtEmail').val());
                                $('#txtEmail').val('');
                                $('#password').focus();
                            }
                        },
                        no: {
                            text: 'No',
                            action: function (no) {
                                $('#txtEmail').val('');
                                $('#txtEmail').focus();
                            }
                        }
                    }
                });
            }
        }
    });
    $("#divLoader").hide();
}

var dateIconFunctions = function () {
    $('#DOBdate').click(function () {
        $("#txtDateOfBirth").focus();
    });
    $('#SPMoveInDateF').click(function () {
        $("#txtMoveInDateFrom").focus();
    });
    $('#SPMoveInDateT').click(function () {
        $("#txtMoveInDateTo").focus();
    });
    $('#SPtxtMoveInDateFrom2').click(function () {
        $("#txtMoveInDateFrom2").focus();
    });
    $('#SPtxtMoveInDateTo2').click(function () {
        $("#txtMoveInDateTo2").focus();
    });
    $('#SPStartDate').click(function () {
        $("#txtStartDate").focus();
    });
    $('#IconDateOfBirth').click(function () {
        $("#txtDateOfBirth").focus();
    });
};
var createLeaseDocument = function () {
    if ($("#btnDownloadLeaseDocument").attr("disabled")) {
        return;
    }
    $("#divLoader").show();
    var param = { UserID: $("#hndUID").val() };
    $.ajax({
        url: "/CheckList/LeaseDocument",
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            $("#btnDownloadLeaseDocument").attr("disabled", "disabled");
        }
    });
}
var downloadLeaseDocument = function () {
    $("#divLoader").show();
    var param = { UserID: $("#hndUID").val() };
    $.ajax({
        url: "/CheckList/GetLeaseDocBlumoon",
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
};

var getMonthsCountFromApplicantHistory = function () {
    var tenantId = $("#hdnOPId").val();
    var fromDateAppHis = $('#txtMoveInDateFrom').val();
    var toDateAppHis = $('#txtMoveInDateTo').val();
    
    var model = {
        TenantId: tenantId,
        FromDateAppHis: fromDateAppHis,
        ToDateAppHis: toDateAppHis
    };
    $.ajax({
        url: '/ApplyNow/GetMonthsFromApplicantHistory',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            
            $("#hndHistory").val(response.model.TotalMonthsApplicantHistory);
        }
    });
};

var deleteVehiclesListOnCheck = function () {

    var ProspectID = $("#hdnOPId").val();

    var model = {
        TenantId: ProspectID
    };
    $.ajax({
        url: '/ApplyNow/DeleteVehicleListOnCheck',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            
        }
    });
};

var saveupdateSSN = function () {

    var prospectID = $("#hdnOPId").val();
    var sSNNumber = $("#txtSSNNumber").data('value');
    if (sSNNumber != "") {
        var model = {
            ProspectID: prospectID,
            SSN: sSNNumber
        };

        $.ajax({
            url: '/ApplyNow/SaveUpdateSSN',
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {

            }
        });
    }

};

var saveupdateIDNumber = function () {
    var prospectID = $("#hdnOPId").val();
    var idNumber = $("#txtIDNumber").data("value");

    if (idNumber != "") {
        var model = {
            ProspectID: prospectID,
            IDNumber: idNumber
        };

        $.ajax({
            url: '/ApplyNow/SaveUpdateIDNumber',
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {

            }
        });
    }

};

var saveupdatePassportNumber = function () {

    var prospectID = $("#hdnOPId").val();
    var passportNumber = $("#txtPassportNum").val();

    if (passportNumber != "") {
        var model = {
            ProspectID: prospectID,
            PassportNumber: passportNumber
        };

        $.ajax({
            url: '/ApplyNow/SaveUpdatePassportNumber',
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {

            }
        });
    }

};