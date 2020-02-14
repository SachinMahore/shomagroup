$(document).ready(function () {

});

var saveUpdateModels = function () {
    $("#divLoader").show();
    var msg = '';
    var modelsId = $("#hndModelsId").val();
    var modelsName = $("#txtModelName").val();
    var area = $("#txtArea").val();
    //var rent = $("#txtRent").val();
    var rent ="0.00";
    var bedroom = $("#ddlmBedroom").val();
    var bathroom = $("#ddlmBathroom").val();
    var uploadModelsPicture = document.getElementById('modalPicture');
    var hiddenModelsFloorPlan = $("#hndModelsPicture").val();

    var minrent = unformatText($("#txtMinRent").val());
    var maxrent = unformatText($("#txtMaxRent").val());
    var deposit = unformatText($("#txtModelDeposit").val());
    var balconyArea = $("#txtBalconyArea").val();
    var interiorArea = $("#txtInteriorArea").val();

    if (!modelsName) {
        msg += 'Enter The Models Name</br>';
    }
    if (!area) {
        msg += 'Enter The Area</br>';
    }
    if (!minrent) {
        msg += 'Enter The Min Rent For Model</br>';
    }
    if (!maxrent) {
        msg += 'Enter The Max Rent For Model</br>';
    }
    if ($("#ddlmBedroom").val() == '0') {
        msg += 'Select The Bedroom</br>';
    }
    if ($("#ddlmBathroom").val() == '0') {
        msg += 'Select The Bathroom</br>';
    }
    if (!hiddenModelsFloorPlan) {
        msg += 'Upload The Floor Plan</br>';
    }
    if (msg != "") {
        $("#divLoader").hide();
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        })
        $("#divLoader").hide();
        return;
    }
    $formData = new FormData();

    $formData.append('ModelID', modelsId);
    $formData.append('Area', area);
    $formData.append('Rent', rent);
    $formData.append('Bedroom', bedroom);
    $formData.append('Bathroom', bathroom);
    $formData.append('ModelName', modelsName);
    $formData.append('MinRent', minrent);
    $formData.append('MaxRent', maxrent);
    $formData.append('Deposit', deposit);
    $formData.append('FloorPlan', hiddenModelsFloorPlan);
    $formData.append('BalconyArea', balconyArea);
    $formData.append('InteriorArea', interiorArea);
    
        $.ajax({
            url: '/Admin/Models/SaveUpdateModels',
            type: 'post',
            data: $formData,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                $("#divLoader").hide();
                $.alert({
                    title: '',
                    content: 'Progress Saved',
                    type: 'blue',
                });
                setInterval(function () {
                    //clearModels();
                    //window.location.replace("/Admin/Models")
                }, 3000);
            }
        });
    
};

var goModelsList = function () {
    clearModels();
    window.location.replace("/Admin/Models/")
}

var uploadFloorPlan = function () {
    $("#divLoader").show();
    $formData = new FormData();

    var uploadModelsPicture = document.getElementById('modalPicture');
    var modelName = $('#txtModelName').val();
    for (var i = 0; i < uploadModelsPicture.files.length; i++) {
        $formData.append('file-' + i, uploadModelsPicture.files[i]);
    }
    $formData.append('ModelName', modelName);
    $.ajax({
        url: '/Admin/Models/UploadModelsFloorPlan',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $("#divLoader").hide();
            $('#hndModelsPicture').val(response.model.TempFileName);
            $("#divModalPicture").empty();
            var img = '<img src="/content/assets/img/plan/' + response.model.TempFileName + '" class="picture-src" id="wizardPicturePreview" title="" />';
            $("#divModalPicture").append(img);
        }
    });
};


var uploadFloorDetails = function () {
    $("#divLoader").show();
    $formData = new FormData();

    var uploadFloorDet = document.getElementById('fileUploadFloorDetailss');
    var modelName = $('#txtModelName').val();

    for (var i = 0; i < uploadFloorDet.files.length; i++) {
        $formData.append('file-' + i, uploadFloorDet.files[i]);

    }
    $formData.append('ModelName', modelName);
    $.ajax({
        url: '/Admin/Models/UploadModelsFloorPlanDetails',
        type: 'post',
        data: $formData,
        contentType: 'application/json; charset=utf-8',
        contentType: false,
        processData: false,
        dataType: 'json',
        success: function (response) {
            $("#divLoader").hide();
            $('#hndFloorDetailsFile').val(response.model.TempFloorPlanDetailsFileName);

            $("#divModalPictureFloorDetails").empty();
            var img = '<img src="/content/assets/img/plan/' + response.model.TempFloorPlanDetailsFileName + '" class="picture-src" id="wizardPicturePreviewFloorDetails" title="" />';
            $("#divModalPictureFloorDetails").append(img);
        }
    });
};

var clearModels = function () {
    $("#wizardPicturePreview").val('');
    $("#modalPicture").val('');
    $("#txtModelName").val('');
    $("#txtArea").val('');
    $("#txtRent").val('');
    $("#ddlmBedroom").val('0');
    $("#ddlmBathroom").val('0');
    $("#hndModelsPicture").val('0');
    $("#txtBalconyArea").val('');
    $("#txtInteriorArea").val('');
}

var areaAddition = function () {
    var balcony = $('#txtBalconyArea').val();
    var interior = $('#txtInteriorArea').val();
    var area = $('#txtArea').val();
    if ($('#txtBalconyArea').val().trim() == '') {
        $('#txtBalconyArea').val('0');
    }
    if ($('#txtInteriorArea').val().trim() == '') {
        $('#txtInteriorArea').val('0');
    }
    var add = parseFloat($('#txtBalconyArea').val()) + parseFloat($('#txtInteriorArea').val());
    $('#txtArea').val(add);

}

var onFocusModels = function () {

    $("#txtModelDeposit").focusout(function () { $("#txtModelDeposit").val(formatMoney($("#txtModelDeposit").val())); })
        .focus(function () {
            $("#txtModelDeposit").val(unformatText($("#txtModelDeposit").val()));
        });

    $("#txtMinRent").focusout(function () { $("#txtMinRent").val(formatMoney($("#txtMinRent").val())); })
        .focus(function () {
            $("#txtMinRent").val(unformatText($("#txtMinRent").val()));
        });

    $("#txtMaxRent").focusout(function () { $("#txtMaxRent").val(formatMoney($("#txtMaxRent").val())); })
        .focus(function () {
            $("#txtMaxRent").val(unformatText($("#txtMaxRent").val()));
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