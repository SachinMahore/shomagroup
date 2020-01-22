var fillUserTypes = function () {
    $("#ddlUserType").empty();
    $("#ddlUserType").append("<option value='0'>Select User Type</option>");
    $("#ddlUserType").append("<option value='1'>Administrator</option>");
    $("#ddlUserType").append("<option value='2'>User</option>");
}
var getUserData = function (userID) {
    //alert(userID);
    var params = { UserID: userID };
    $.ajax({
        url: "../GetUserInfo",
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            clearUserData();
            if ($.trim(response.error) != "") {
                //showMessage("Error!", response.error);
            } else {
                $("#txtFirstName").val(response.FirstName);
                $("#txtLastName").val(response.LastName);
                $("#txtCellPhone").val(formatMoney(response.CellPhone));
                $("#txtEmail").val(response.Email);
                $("#txtUserName").val(response.Username);
                $("#txtPassword").val(response.Password);
                $("#txtConfirmPassword").val(response.Password);
                $('#chkIsActive').prop('checked', (response.IsActive == 1 ? true : false));
                if ($("#hndUserID").val() != "0") {
                    $("#spanSaveUpdate").text("Update");
                }
                else {
                    $("#spanSaveUpdate").text("Save");
                }
                console.log(response.UserType); console.log(response.IsActive);
            }
        }
    });
}
//$("#txtWorkPhone").val(formatPhoneFax(response.WorkPhone));
                //$("#txtExtension").val(formatPhoneFax(response.Extension));
                //$("#txtCellPhone").val(formatPhoneFax(response.CellPhone));
var clearUserData = function () {
    $("#txtFirstName").val("");
    $("#txtLastName").val("");
    $("#txtCellPhone").val("");
    $("#txtEmail").val("");
    $("#txtUserName").val("");
    $("#txtPassword").val("");
    $("#txtConfirmPassword").val("");
    $('#checkbox').prop('checked', true);
    $("#spanSaveUpdate").text("Save");
}
var newUser = function () {
    window.location.href = "../users/new";
}
var saveUpdateUser = function () {
    ////showProgress('#btnSaveUpdate');

    var msg = "";
    if ($.trim($("#txtFirstName").val()).length <= 0) {
        msg = msg + "First Name is required.</br>"
    }
    if ($.trim($("#txtLastName").val()).length <= 0) {
        msg = msg + "Last Name is required.</br>"
    }

    if ($("#ddlUserType").val() == 0) {
        msg = msg + "User Type is required.</br>"
    }

    if ($.trim($("#txtUserName").val()).length <= 0) {
        msg = msg + "User Name is required.</br>"
    }
    if ($.trim($("#txtPassword").val()).length <= 0) {
        msg = msg + "Password is required.</br>"
    }
    if ($.trim($("#txtPassword").val()) != $.trim($("#txtConfirmPassword").val())) {
        msg = msg + "Password and Confirm Passwrod not matched.</br>"
    }
    if ($.trim($("#txtEmail").val()).length > 0) {
        //if (!validateEmail($("#txtEmail").val())) {
        //    msg = msg + "Invalid email address.\r\n"
        //}
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'blue',
        });
    }
    else {
        var model = {
            UserID: $("#hndUserID").val(),
            FirstName: $("#txtFirstName").val(),
            LastName: $("#txtLastName").val(),
            CellPhone: unformatText($("#txtCellPhone").val()),
            Email: $("#txtEmail").val(),
            Username: $("#txtUserName").val(),
            Password: $("#txtPassword").val(),
            IsActive: $("#chkIsActive").is(':checked') ? 1 : 0
        };
        var saveurl = "";
        if ($("#hndUserID").val() == "0") {
            saveurl= "../users/SaveUpdateUser";
        }
        else {
            saveurl=  "../SaveUpdateUser";
        }
        $.ajax({
            url: saveurl,
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndUserID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue',
                        });
                        $("#hndUserID").val(response.ID);
                        window.location.href = "../users/edit/" + response.ID;
                       
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue',
                        });
                    }
                }
                else {
                    //showMessage("Error!", response.error);
                }
            }
        });
    }
}
var searchUser = function () {
    if ($("#hndUserID").val() == "0") {
        window.location.href = "../../admin/users";
    }
    else {
        window.location.href = "../../../admin/users";
    }
}

$(document).ready(function () {
    fillUserTypes();
    getUserData($("#hndUserID").val());
    onFocus();
});

var onFocus = function () {

    $("#txtCellPhone").focusout(function () { $("#txtCellPhone").val(formatPhoneFax($("#txtCellPhone").val())); })
        .focus(function () {
            $("#txtCellPhone").val(unformatText($("#txtCellPhone").val()));
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
