$(document).ready(function () {
    clearPremiumType();
    getPremiumTypeList();
});

var saveUpdatePremiumType = function () {
    var msg = '';
    var hdnPTID = $('#hndPTID').val();
    var premiumType = $('#txtPremiumType').val();
    var details = $('#txtDetails').val();
    $("#divLoader").show();
    if (!premiumType) {
        msg += 'Fill Premium Type.';
    }
    if (msg != '') {
        $("#divLoader").hide();
        $.alert({
            title: '',
            content: msg,
            type: 'red',
        });
    }
    else {

        var model = { PTID: hdnPTID, PremiumType: premiumType, Details: details };
        $.ajax({
            url: "/PremiumType/SaveUpdatePremiumType",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                $("#divLoader").hide();
                $.alert({
                    title: 'Message!',
                    content: "Data Saved Successfully",
                    type: 'blue',
                });
                getPremiumTypeList();
                clearPremiumType();
            }
        });
    }
};

var getPremiumTypeList = function (sortby, orderby) {
    if (!sortby) {
        sortby = 'PremiumType';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    $("#divLoader").show();
    var params = {
        SearchText: $("#txtCriteriaPremiumType").val(),
        SortBy: sortby,
        OrderBy: orderby };
    $.ajax({
        url: '/PremiumType/GetPremiumTypeList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#tblPremiumType tbody").empty();
            $.each(response.model, function (index, elementValue) {
                html = '<tr id="tr_' + elementValue.PTID + '" data-value="' + elementValue.PTID + '">';
                html += '<td style="color:#3d3939;">' + elementValue.PremiumType + '</td>';
                html += '<td style="color:#3d3939;">' + elementValue.Details + '</td>';
                html += '<td style="color:#3d3939;"><button class="btn btn-primary" style="padding: 5px 8px !important" onclick="goToPremiumType(' + elementValue.PTID + ')"><i class="fa fa-edit"></i></button>&nbsp;&nbsp;<button class="btn btn-danger" style="padding: 5px 8px !important" onclick="deletePremiumType(' + elementValue.PTID + ')"><i class="fa fa-trash"></i></button></td>';
                html += '</tr>';
                $("#tblPremiumType tbody").append(html);
            });
            $("#divLoader").hide();
        }
    });

}

var goToPremiumType = function (ID) {
    if (ID != null) {
        $("#divLoader").show();
        var model = { Id: ID };
        $.ajax({
            url: "/PremiumType/GetPremiumTypeDetails",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                $("#divLoader").hide();
                $('#hndPTID').val(response.model.PTID);
                $('#txtPremiumType').val(response.model.PremiumType);
                $('#txtDetails').val(response.model.Details);
            }
        });
    }
}
var deletePremiumType = function (ID) {
    if (ID != null) {
        $("#divLoader").show();
        var model = { Id: ID };
        $.alert({
            title: "",
            content: "Are you sure to remove Premium Type?",
            type: 'blue',
            buttons: {
                yes: {
                    text: 'Yes',
                    action: function (yes) {
                        $.ajax({
                            url: "/PremiumType/DeletePremiumTypeDetails",
                            type: "post",
                            contentType: "application/json utf-8",
                            data: JSON.stringify(model),
                            dataType: "JSON",
                            success: function (response) {
                                $("#divLoader").hide();
                                $('#tr_' + ID).remove();
                            }
                        });
                    }
                },
                no: {
                    text: 'No',
                    action: function (no) {
                        $("#divLoader").hide();
                    }
                }
            }
        });
    }
}
var clearPremiumType = function () {
    $('#hndPTID').val('0');
    $('#txtPremiumType').val('');
    $('#txtDetails').val('');
    $("#txtCriteriaPremiumType").val('');
}

var count = 0;
var sortTablePremiumType = function (sortby) {

    var orderby = "";

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#sortPremiumTypeIcon').removeClass('fa fa-sort-up');
        $('#sortPremiumTypeIcon').removeClass('fa fa-sort-down');
        if (sortby == 'PremiumType') {
            $('#sortPremiumTypeIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $('#sortPremiumTypeIcon').removeClass('fa fa-sort-up');
        $('#sortPremiumTypeIcon').removeClass('fa fa-sort-down');
        if (sortby == 'PremiumType') {
            $('#sortPremiumTypeIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValuePremiumType", sortby);
    localStorage.setItem("OrderByValuePremiumType", orderby);
    count++;
    getPremiumTypeList(sortby, orderby);
};