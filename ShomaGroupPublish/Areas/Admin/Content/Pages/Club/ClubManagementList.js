$(document).ready(function () {
    getGuestRegistrationList();
});
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}
var getGuestRegistrationList = function (sortby, orderby) {
    if (!sortby) {
        sortby = 'ClubTitle';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    $("#divLoader").show();
    var model = {
        FromDate: $('#txtFromDate').val(),
        ToDate: $('#txtToDate').val(),
        SortBy: sortby,
        OrderBy: orderby
    }
    $.ajax({
        url: '/ClubManagement/GetClubManagementList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(model),
        success: function (response) {
            $("#tblClubManagement>tbody").empty();
            debugger
            $.each(response.model, function (index, elementValue) {
                var DateTime = ToJavaScriptDate(elementValue.StartDate); 
                var html = '';
                html += '<tr data-value="' + elementValue.Id + '">';
                html += '<td style="color:#3d3939;">' + elementValue.ClubTitle + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Venue + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.PhoneNumber + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Email + '</td>';
                html += '<td style="color:#3d3939;">' + DateTime + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Time + '</td>';
                html += '<td style="color:#3d3939;"><div class="checkbox"><label><input id="activeConditions" class="form-control-sm" type="checkbox" ' + (elementValue.Active==true? "checked='checked'":"") + ' onchange="ActiveDeactive(' + elementValue.Id + ',this)" name="TermsAndCondition" /> </label></div></td>';
                html += '</tr>';
                $("#tblClubManagement>tbody").append(html);
            });
        }
    });
    $("#divLoader").hide();
};

function ActiveDeactive(id, val) {
    debugger
    var type = false;
    if (val.checked == true) {
        type = true;
    }
    
    var data = {
        Id: id,
        Active: type
    };
    $.ajax({
        url: '/ClubManagement/UpdateClubManagement',
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data:data,
        success: function (response) {
            if (response.model == true) {
                $.alert({
                    title: '',
                    content: "Status Updated Successfully.",
                    type: 'blue'
                });
            }
        }
    });
}

var count = 0;
var sortTableClubManagement = function (sortby) {

    var orderby = "";
    $('#sortClubTitleIcon').removeClass('fa fa-sort-up');
    $('#sortClubTitleIcon').removeClass('fa fa-sort-down');
    $('#sortVenueIcon').removeClass('fa fa-sort-up');
    $('#sortVenueIcon').removeClass('fa fa-sort-down');
    $('#sortPhoneIcon').removeClass('fa fa-sort-up');
    $('#sortPhoneIcon').removeClass('fa fa-sort-down');
    $('#sortEmailIcon').removeClass('fa fa-sort-up');
    $('#sortEmailIcon').removeClass('fa fa-sort-down');
    $('#sortDateIcon').removeClass('fa fa-sort-up');
    $('#sortDateIcon').removeClass('fa fa-sort-down');
    $('#sortTimeIcon').removeClass('fa fa-sort-up');
    $('#sortTimeIcon').removeClass('fa fa-sort-down');
    $('#sortActiveIcon').removeClass('fa fa-sort-up');
    $('#sortActiveIcon').removeClass('fa fa-sort-down');
    if (count % 2 == 1) {
        orderby = "ASC";
        if (sortby == 'ClubTitle') {
            $('#sortClubTitleIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Venue') {
            $('#sortVenueIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Phone') {
            $('#sortPhoneIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Email') {
            $('#sortEmailIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Date') {
            $('#sortDateIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Time') {
            $('#sortTimeIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'Active') {
            $('#sortActiveIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        if (sortby == 'ClubTitle') {
            $('#sortClubTitleIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Venue') {
            $('#sortVenueIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Phone') {
            $('#sortPhoneIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Email') {
            $('#sortEmailIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Date') {
            $('#sortDateIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Time') {
            $('#sortTimeIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'Active') {
            $('#sortActiveIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    count++;
    getGuestRegistrationList(sortby, orderby);
};
