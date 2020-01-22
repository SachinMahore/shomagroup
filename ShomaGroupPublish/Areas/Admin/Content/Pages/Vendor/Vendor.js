$(document).ready(function () {
    fillStateDDL();
    $("#ddlState").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityList(selected);
        }
    });
});
var saveUpdateVendor = function () {
    var msg = '';
    var vendorId = $("#hdnVendorId").val();
    var vendorName = $("#txtVendorName").val();
    var mobileNumber = $("#txtMobileNumber").val();
    var emailId = $("#txtEmailId").val();
    var address = $("#txtAddress").val();
    var state = $("#ddlState").val();
    var city = $("#ddlCity").val();

    if (vendorName == '') {
        msg += 'Enter Vendor Name </br>';
    }
    if (mobileNumber == '') {
        msg += 'Enter Mobile Number </br>';
    }
    else {
        if (!validatePhone(mobileNumber)) {
            msg += 'Invalid Mobile No. address.<br/>';
        }
    }
    if (emailId == '') {
        msg += 'Enter EmailId </br>';
    }
    else {
        if (!validateEmail(emailId)) {
            msg += 'Invalid email address.<br/>';
        }
    }
    if (state ==0) {
        msg += 'Enter State </br>';
    }
    if (city ==0) {
        msg += 'Enter City </br>';
    }
    if (msg != '') {
        $.alert({
            title: 'Alert!',
            content: msg,
            type:'red'
        })
        return;
    }
    var model = {
        Vendor_ID: vendorId,
        Vendor_Name: vendorName,
        Mobile_Number: mobileNumber,
        Email_Id: emailId,
        Address: address,
        State: state,
        City: city
    }

    $.ajax({
        url: '/Admin/Vendor/SaveUpdateVendor',
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
            //setInterval(function () {
            //    window.location.replace("/Admin/Vendor")
            //},3000)
        }
    });
}
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
                  
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var govendorList = function () {
    window.location.replace("/Admin/Vendor/")
}
var vendorList = function () {
    $.ajax({
        url: '/Vendor/VendorDataList',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            $("#ddlVendor").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = '<option value=' + elementValue.Vendor_ID + '>' + elementValue.Vendor_Name + '</option>';
                $("#ddlVendor").append(option);
            })
        }
    });
}