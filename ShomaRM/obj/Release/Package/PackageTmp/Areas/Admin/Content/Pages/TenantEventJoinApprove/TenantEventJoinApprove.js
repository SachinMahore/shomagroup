$(document).ready(function () {
    getTenantEventJoinList();
    ddlEventDatewiseList();
});

var getTenantEventJoinList = function () {
    $("#divLoader").show();
    var model = {
        TenantEventListStatus: $('#ddlEventDatewise').val()
    };
    $.ajax({
        url: '/TenantEventJoinApprove/GetTenantEventJoinList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#tblTenantEventJoinApprove>tbody").empty();
            $.each(response.model, function (index, elementValue) {
                var html = '';
                var newHtml = '';
                if (elementValue.TenantEventJoinStatus == '1') {
                    newHtml = '<label>'
                }
                html += '<tr data-value="' + elementValue.TEID + '">';
                html += '<td style="color:#3d3939;">' + elementValue.EventName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.DateString + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.TimeString + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.TenantName + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Description + '</td>';
                if (elementValue.TenantEventJoinStatus == '1') {
                    html += '<td style="color:#3d3939;">Approve</td>';
                    html += '<td style="color:#3d3939;"><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="deleteEventJoinApprove(' + elementValue.TEJAID +')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                }
                else if (elementValue.TenantEventJoinStatus == '2') {
                    html += '<td style="color:#3d3939;">Decline</td>';
                    html += '<td style="color:#3d3939;"><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="deleteEventJoinApprove(' + elementValue.TEJAID +')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                }
                else {
                    html += '<td style="color:#3d3939;">';
                    html += '<select id= "ddlEventApproveStatus' + elementValue.TEID + '" class="form-control form-control-small" name="EventApproveStatus" >';
                    html += '<option value="1" selected="selected">Approve</option>';
                    html += '<option value= "2">Decline</option>';
                    html += '</select ></td >';
                    html += '<td style="color:#3d3939;">';
                    html += '<a href="javascript:void(0)" onclick="saveTenantEventJoinApprove(' + elementValue.TEID + ')">Save</a >';
                    html += '</td >';
                }
                html += '</tr>';
                $("#tblTenantEventJoinApprove>tbody").append(html);
            });
            $("#divLoader").hide();
        }
    });
};

var saveTenantEventJoinApprove = function (id) {
    $("#divLoader").show();
    selectEventStatusValue(id);
    var model = {
        TEID: id,
        TenantEventListStatus: $('#hdnEventStatusValue').val()
    };

    $.ajax({
        url: '/TenantEventJoinApprove/SaveTenantEventJoinApprove',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            getTenantEventJoinList();
            $.alert({
                title: "",
                content: response.model,
                type: 'red'
            });
            $("#divLoader").hide();
        }
    });
};

var selectEventStatusValue = function (id) {

    var tbl = $('#tblTenantEventJoinApprove').find('tbody').find('tr');
    for (var i = 0; i < tbl.length; i++) {
        var trid = $(tbl[i]).closest('tr').attr('data-value');
        console.log(trid);
        var l = '';
        if (id == trid) {
            $('#hdnEventStatusValue').val($(tbl[i]).find('td:eq(5)').find('select[name="EventApproveStatus"]').find('option:selected').val());
            return
        }
    }
};

var deleteEventJoinApprove = function (id) {
    $("#divLoader").show();
    var model = {
        TEJAID: id
    };

    $.ajax({
        url: '/TenantEventJoinApprove/DeleteTenantEventJoinApprove',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            getTenantEventJoinList();
            $.alert({
                title: "",
                content: response.model,
                type: 'red'
            });
            $("#divLoader").hide();
        }
    });
};