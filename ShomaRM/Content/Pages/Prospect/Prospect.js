$(document).ready(function(){

    fillStateDDL();
    $("#ddlState").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityList(selected);
        }
    });
});



var fillStateDDL = function () {

    $.ajax({
        url: '/City/FillStateDropDownList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlState").empty();
                $("#ddlState").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
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
                $("#ddlCity").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {
                    console.log(response)
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var saveProspectForm = function () {
   
    var msg = "";
    var fname = $("#txtFirstName").val();
    var lname = $("#txtLastName").val();
    var phone = $("#txtPhone").val();
    var email = $("#txtEmail").val();
    var address = $("#txtAddress").val();
    var message = $("#txtMessage").val();
    var pets = $(".icheckbox_square-yellow").hasClass("checked") ? 1 : 0;
    var unit = $("#hndUID").val();
    var state = $("#ddlState").val();
    var city = $("#ddlCity").val();
    var visittime = $("#txtMoveDate").val();
    if (!fname) {
        msg += "Enter First Name</br>";
    }
    if (!lname) {
        msg += "Enter Last Name</br>";
    }
    if (phone=='') {
        msg += "Enter Phone</br>";
    }
    else {
        if (!validatePhone(phone)) {
            msg += 'Invalid Phone No.<br/>';
        }
    }
    if (email=='') {
        msg += "Enter email Id</br>";
    }
    else {
        if (!validateEmail(email)) {
            msg += "Invalid email address.<br/>"
        }
    }
    if (state == 0) {
        msg += "Select State</br>";
    }
    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
    var model = { FirstName: fname, LastName: lname, PhoneNo: phone, EmailId: email, Address: address, Message: message, HavingPets: pets, UnitID: unit, State: state, City: city, VisitDateTime: visittime };
    $.ajax({
        url: "/Prospect/SaveProspectForm/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: 'Message!',
                content: response.model,
                type: 'blue',
            });
            clearProspectForm();
            $("#popContactForm").PopupWindow("close");
        }
    });
}

function clearProspectForm() {
    $("#txtFirstName").val("");
    $("#txtLastName").val("");
    $("#txtPhone").val("");
    $("#txtEmail").val("");
    $("#txtAddress").val("");
    $("#txtMessage").val("");
    $(".icheckbox_square-yellow").removeClass("checked");
    $("#hndUID").val(0);
    $("#ddlState").val(0);
    $("#ddlCity").val(0);
    $("#txtMoveDate").val("");
}
