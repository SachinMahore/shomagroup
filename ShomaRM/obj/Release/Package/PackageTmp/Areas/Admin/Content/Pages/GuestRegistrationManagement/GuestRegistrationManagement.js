$(document).ready(function () {
    getGuestRegistrationList();
    goToGuestDetails($("#hndGuestID").val());
    $('#tblGuestRegistration tbody').on('click', 'tr', function () {
        $('#tblUser tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblGuestRegistration tbody').on('dblclick', 'tr', function () {
        goToEdit();
    });

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
                html += '<td style="color:#3d3939;">' + elementValue.StatusString + '</td>';
                //html += '<td style="color:#3d3939;"><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delGuestRegistration(' + elementValue.GuestID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
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
        content: "Are you sure to remove Guest Registration?",
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

var goToEdit = function () {
    var row = $('#tblGuestRegistration tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
   
    if (ID !== null) {
        window.location.replace("/Admin/GuestManagement/Edit/" + ID);
      
    }
};

var goToGuestDetails = function () {
    $("#divLoader").show();
    var guestID = $("#hndGuestID").val();
   
    var model = {
        GuestID: guestID,

    };
    $.ajax({
        url: '/GuestManagement/goToGuestDetails',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#TagUnitNo").text(response.msg.UnitNo);
            $("#TagTenantName").text(response.msg.TenantName);
            $("#TagGuesttName").text(response.msg.GuestName);
            $("#TagVehicleMake").text(response.msg.VehicleMake);
            $("#TagVehicleModel").text(response.msg.VehicleModel);
            $("#Tag").text(response.msg.Tag);


            $("#PrintGuestFname").text(response.msg.FirstName);
            $("#PrintGuestLname").text(response.msg.LastName);
            $("#PrintGuestAddress").text(response.msg.Address);
            $("#PrintGuestPhone").text(formatPhoneFax(response.msg.Phone));
            $("#PrintGuestEmail").text(response.msg.Email);
            $("#PrintGuestVisitSdate").text(response.msg.VisitStartDateString);
            $("#PrintGuestVisitEnddate").text(response.msg.VisitEndDateString);
            $("#PrintGuesHaveVehicle").text(response.msg.HaveVehicleString);
            if (response.msg.HaveVehicleString == 'Yes') {
                $("#Pvehicle").removeClass('hidden'); 
               
                $("#PrintGuestVMake").text(response.msg.VehicleMake);
                $("#PrintGuestVModel").text(response.msg.VehicleModel);
                $("#PrintGuestTag").text(response.msg.Tag);
                ////$("#PrintGuestRegistration").text(response.msg.OriginalDriverLicence);
                //$("#PrintGuestriverLicence").text(response.msg.OriginalVehicleRegistration);

                var guestIdTag = '';
                var guestVehicleRegTag = '';
                var isIdentityFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + response.msg.DriverLicence);
                if (isIdentityFileExist) {
                    guestIdTag = '<a href="/Content/assets/img/TenantGuestInformation/' + response.msg.DriverLicence + '" target="_blank" class="fa fa-eye"></a>';
                }
                else {
                    guestIdTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();" class="fa fa-eye"></a>';
                }
                var isVehicleRegFileExist = doesFileExist('/Content/assets/img/TenantGuestInformation/' + response.msg.VehicleRegistration);
                if (isVehicleRegFileExist) {
                    guestVehicleRegTag = '<a href="/Content/assets/img/TenantGuestInformation/' + response.msg.VehicleRegistration + '" target="_blank" class="fa fa-eye"></a>';
                }
                else {
                    guestVehicleRegTag = '<a href="javascript:void(0);" onclick="fileDoesNotExist();" class="fa fa-eye"></a>';
                }
               
                $("#PrintGuestRegistration").html(guestIdTag);
                $("#PrintGuestriverLicence").html(guestVehicleRegTag);
            } else {
                $("#Pvehicle").addClass('hidden');
            }
        
            if (response.msg.StatusString == 'Approved' && response.msg.HaveVehicleString == 'Yes') {
                $("#tag").removeClass('hidden');
            } else {
                $("#tag").addClass('hidden');
            }
            $("#PspanGuestCertGName").text(response.msg.GuestName);
            $("#PspanTenantSignName").text(response.msg.TenantName);
            $("#PspanGuestSignName").text(response.msg.GuestName);
            $("#ddlStatus").val(response.msg.Status);
           
        }
    });
    $("#divLoader").hide();
}

var openTag = function () {
    $('#popTag').modal('show');
   
};
var StatusUpdate = function (id) {
    $("#divLoader").show();
    var msg = '';
    var id = $("#hndGuestID").val();
    var status = $('#ddlStatus').val();
    var guestStartDate = $('#PrintGuestVisitSdate').text();
    var guestEnddate = $('#PrintGuestVisitEnddate').text();
    var guestHaveVehicle = $('#PrintGuesHaveVehicle').text();
    var guestName = $('#TagGuesttName').text(); 
    var TenantName = $('#TagTenantName').text();

    if (status == '0') {
        msg += 'Select The Status</br>'
    }
   
    if (msg!="") {
        $.alert({
            title: '',
            content: msg,
            type: 'red'
        });
        $("#divLoader").hide();
        return
    }

    var model = {
        GuestID: id,
        Status: status,
        GuestName: guestName,
        TenantName:TenantName,
        HaveVehicleString: guestHaveVehicle,
        VisitStartDateString: guestStartDate,
        VisitEndDateString: guestEnddate,
    };
    $.ajax({
        url: '/GuestManagement/StatusUpdate',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: '',
                content: response.model,
                type: 'red'
            });
            $("#ddlStatus").val('0');
            goToGuestDetails();
        }
       
    });
    $("#divLoader").hide();
}

var goGuestRequestList = function () {
    window.location.replace( "/Admin/GuestManagement/Index");
};
