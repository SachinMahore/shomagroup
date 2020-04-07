function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}

function validatePhone(txtPhone) {
    var filter = /[1-9]{1}[0-9]{9}/;
    if (filter.test(txtPhone)) {
        return true;
    }
    else {
        return false;
    }
}

function validateNumber(txtNumber) {
    var filter = /^[0-9]*$/;
    if (filter.test(txtNumber)) {
        return true;
    }
    else {
        return false;
    }
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function isOnlyNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 32 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
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

function doesFileExist(urlToFile) {
    var xhr = new XMLHttpRequest();
    xhr.open('HEAD', urlToFile, false);
    xhr.send();

    if (xhr.status == "404") {
        return false;
    } else {
        return true;
    }
}

function restrictFileUpload(uploaderId) {
    var ext = uploaderId.split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'psd', 'xls', 'doc', 'docx', 'pdf', 'rtf', 'tex', 'txt', 'wpd']) == -1) {
        return false;
    }
    else {
        return true;
    }
};

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

function isDecimalWithPlace(evt, cont, decplace) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode != 45 && charCode != 8 && charCode != 9 && (charCode != 46 || $(cont).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
        return false;
    if ($(cont).val().indexOf('.') != -1 && charCode != 8) {
        var contval = $(cont).val();
        var valsplit = contval.split('.');
        if (valsplit[1].length + 1 > decplace)
            return false;
    }
    return true;
}

function isDecimalAll(evt, cont) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 45 && charCode != 8 && charCode != 9 && (charCode != 46 || $(cont).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function nonNegDecimal(evt, cont) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 8 && charCode != 9 && (charCode != 46 || $(cont).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function checkStrength(password) {

    var validated = true;
    var msg = "";

    if (password.length < 8) { validated = false; msg += "The Password should have a minimum of 8 characters <br/>"; }
    if (!/\d/.test(password)) { validated = false; msg += "Password should contain at least one digit <br/>"; }
    if (!/[a-z]/.test(password)) { validated = false; msg += "Password should contain at least one lower case <br/>"; }
    if (!/[A-Z]/.test(password)) { validated = false; msg += "Password should contain at least one upper case <br/>"; }
    if (/[^0-9a-zA-Z]/.test(password)) { validated = false; msg += "Password should contain at least 8 from the mentioned characters <br/>"; }

    if (msg != "") {
        $.alert({
            title: "",
            content: msg,
            type: 'red'
        });
        validated= false;
    }
    return validated;
}