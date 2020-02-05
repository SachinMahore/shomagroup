$(document).ready(function () {
    getGuestRegistrationList();
});

var getGuestRegistrationList = function () {
    $("#divLoader").show();
    var model = {
        FromDate: $('#txtFromDate').val(),
        ToDate: $('#txtToDate').val()

    }
    $.ajax({
        url: '/GuestManagement/GetGuestRegistrationList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(model),
        success: function (response) {
            $("#tblGuestRegistration>tbody").empty();

            $.each(response.model, function (index, elementValue) {
                var guestIdTag = '';
                var guestVehicleRegTag = '';
                var isIdentityFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + elementValue.DriverLicence);
                if (isIdentityFileExist) {
                    guestIdTag = '<a href="/Content/assets/img/TenantGuestInformation/' + elementValue.DriverLicence + '" target="_blank" class="fa fa-eye"></a>';
                }
                else {
                    guestIdTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();" class="fa fa-eye"></a>';
                }
                var isVehicleRegFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + elementValue.VehicleRegistration);
                if (isVehicleRegFileExist) {
                    guestVehicleRegTag = '<a href="/Content/assets/img/TenantGuestInformation/' + elementValue.VehicleRegistration + '" target="_blank" class="fa fa-eye"></a>';
                }
                else {
                    guestVehicleRegTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();" class="fa fa-eye"></a>';
                }
                var html = '';
                html += '<tr data-value="' + elementValue.GuestID + '">';
                html += '<td style="color:#3d3939;">' + elementValue.FirstName + ' ' + elementValue.LastName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.TenantName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Phone + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Email + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.VisitStartDateString + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.VisitEndDateString + '</td>';
                html += '<td style="color:#3d3939;">' + guestIdTag + '</td>';
                html += '<td style="color:#3d3939;">' + guestVehicleRegTag + '</td>';
                html += '<td style="color:#3d3939;"><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delGuestRegistration(' + elementValue.GuestID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                html += '</tr>';
                $("#tblGuestRegistration>tbody").append(html);
            });
        }
    });
    $("#divLoader").hide();
};

var fileDoesNotExist = function () {
    $.alert({
        title: '',
        content: "File does not exist!",
        type: 'blue',
    });
};

var delGuestRegistration = function (id) {
    $("#divLoader").show();
    $.alert({
        title: "",
        content: "Are you sure to remove Vehicle?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    var model = { GuestID: id };
                    $.ajax({
                        url: '/GuestManagement/DeleteGuestRegistrationList',
                        method: "post",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            getGuestRegistrationList();
                            $.alert({
                                title: '',
                                content: response.model,
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
    $("#divLoader").hide();
};