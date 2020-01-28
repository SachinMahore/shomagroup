var goToEdit = function () {
    var row = $('#tblServiceRequest tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href=("../../ServicesManagement/Edit/" + ID);
    }
};
var addNewUser = function () {
    window.location.href = "users/new";
};
var fillRPP_UserList = function () {
    $("#ddlRPP_UserList").empty();
    $("#ddlRPP_UserList").append("<option value='25'>25</option>");
    $("#ddlRPP_UserList").append("<option value='50'>50</option>");
    $("#ddlRPP_UserList").append("<option value='75'>75</option>");
    $("#ddlRPP_UserList").append("<option value='100'>100</option>");
    $("#ddlRPP_UserList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationUserList($("#hdnCurrentPage_PVL").val());
    });
};
var buildPaganationUserList = function (pagenumber) {
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        Piority: 0,
        Status: $("#ddlStatus").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_UserList").val()
    };
    $.ajax({
        url: "/ServicesManagement/BuildPaganationUserList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_UserList').addClass("hidden");
                    $("#tblServiceRequest>tbody").empty();
                }
                else {
                    $('#divPagination_UserList').removeClass("hidden");
                    $('#ulPagination_UserList').pagination('updateItems', response.NOP);
                    $('#ulPagination_UserList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillUserList = function (pagenumber) {
   
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        Piority:0,
        Status: $("#ddlStatus").val(),	
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_UserList").val()
    };
   
    $.ajax({
        url: '/ServicesManagement/FillServicesSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert('hi');
            } else {
                $("#tblServiceRequest>tbody").empty();
                $.each(response.model, function (elementType, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ServiceID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ServiceID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.TenantName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Problemcategory + '</td>';
                    html += '<td class="pds-lastname" style="color:#3d3939;">' + elementValue.ItemCaussing + '</td>';
                    html += '<td class="pds-username" style="color:#3d3939;">' + elementValue.ItemIssue + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.Location + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.PriorityString + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;">' + elementValue.StatusString + '</td>';
                    html += '<td class="pds-usertype"style="color:#3d3939;"><button class="btn btn-primary " style="padding: 5px 8px !important;margin-right:7px" onclick="goToServiceDetails(' + elementValue.ServiceID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button></button></td>';
                    html += '</tr>';
                    $("#tblServiceRequest>tbody").append(html);
                });
               
                if (response.model.length == 0) {
                    $("#tblServiceRequest>tbody").empty();
                }
            }
        }
    });
};
$(document).ready(function () {
    fillRPP_UserList();
    $('#ulPagination_UserList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_PVL").val(1);
            buildPaganationUserList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_PVL").val(page);
            fillUserList(page);
        }
    });
    $('#tblServiceRequest tbody').on('click', 'tr', function () {
        $('#tblUser tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblServiceRequest tbody').on('dblclick', 'tr', function () {
        //goToEdit();
    });
   
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationUserList(1);
    }
});

var goToServiceDetails = function (ServiceID) {
    $("#divLoader").show();
    var model = {
        ServiceID: ServiceID,
    };
     $.ajax({
         url: '/ServicesManagement/goToServiceDetails',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
           
            $("#hndServiceID").val(response.model.ServiceID);
            $('#lbltenantName').text(response.model.TenantName);
            $('#ProblemCatrgory').text(response.model.ProblemCategorystring);
            $('#lblProbleOther').text(response.model.Details);
            $('#lblcaussingIssue').text(response.model.CausingIssue);
            $('#lblIssue').text(response.model.Issue);
            $('#lblLocation').text(response.model.LocationString); 
            $('#lblUnitNo').text(response.model.Unit);
            $('#lblContactNo').text(response.model.Phone); 
            $('#lblCurrentStatus').text(response.model.StatusString);
            $('#lblEnteryNote').text(response.model.Notes);
            $('#lblEmergencyNo').text(response.model.EmergencyMobile); 
           
            if (response.model.TempServiceFile != null) {
                var fileExist = doesFileExist('/Content/assets/img/Document/' + response.model.TempServiceFile);
                if (fileExist) {
                    $('#wizardPicturePreview').attr('src', '/Content/assets/img/Document/' + response.model.TempServiceFile);
                }
                else {
                    $('#wizardPicturePreview').attr('src', '/Content/assets/img/aaa.png');
                    
                }
            }
            else {
                $('#wizardPicturePreview').attr('src', '/Content/assets/img/aaa.png');
            }
            $("#ddlStatus1").val('0');
            $('#popSDetails').modal('show');
          
        }
    });
    $("#divLoader").hide();
};

var StatusUpdateServiceRequest = function (id) {
    $("#divLoader").show();
    var msg = '';
    var id = $("#hndServiceID").val();
    var status = $('#ddlStatus1').val(); 

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
        ServiceID: id,
        Status: status
    };
    $.ajax({
        url: '/ServicesManagement/StatusUpdateServiceRequest',
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
            $("#ddlStatus1").val('0');
            $('#popSDetails').modal('hide');
        }
    });
    $("#divLoader").hide();
}