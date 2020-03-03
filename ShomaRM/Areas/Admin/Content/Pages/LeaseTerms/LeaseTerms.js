$(document).ready(function () {
    clearLeaseTerms();
    getLeaseTermsList();
});

var saveUpdateLeaseTerms = function () {
    $("#divLoader").show();
    var msg = '';
    var hdnLTID = $('#hndLTID').val();
    var leaseTerms = $('#txtLeaseTerms').val();
    var offerTerms = $('#txtOfferTerms').val();
    var chkAgent = false;
    if ($('#chkIsAgent').is(":checked")) {
        chkAgent = true;
    }
    else {
        chkAgent = false;
    }
    if (!leaseTerms) {
        msg += 'Enter the Lease Terms.';
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

        var model = { LTID: hdnLTID, LeaseTerms: leaseTerms, OfferTerms: offerTerms, FormAgent: chkAgent };

        $.ajax({
            url: "/LeaseTerms/SaveUpdateLeaseTerms",
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
                getLeaseTermsList();
                clearLeaseTerms();
            }
        });
    }
};

var getLeaseTermsList = function (sortby, orderby) {
    if (!sortby) {
        sortby = 'LeaseTerms';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var params = {
        SortBy: sortby,
        OrderBy: orderby
    };
    $("#divLoader").show();
    $.ajax({
        url: '/LeaseTerms/GetLeaseTermsList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#tblLeaseTerms tbody").empty();
            $.each(response.model, function (index, elementValue) {
                html = '<tr id="tr_' + elementValue.LTID + '" data-value="' + elementValue.LTID + '">';
                html += '<td style="color:#3d3939;">' + elementValue.LeaseTerms + ' Month</td>';
                html += '<td style="color:#3d3939;">' + elementValue.OfferTerms + ' Month</td>';
                html += '<td style="color:#3d3939;">' + elementValue.FormAgentString + '</td>';
                html += '<td style="color:#3d3939;"><button class="btn btn-primary" style="padding: 5px 8px !important" onclick="goToLeaseTerms(' + elementValue.LTID + ')"><i class="fa fa-edit"></i></button>&nbsp;&nbsp;<button class="btn btn-danger" style="padding: 5px 8px !important" onclick="deleteLeaseTerms(' + elementValue.LTID + ')"><i class="fa fa-trash"></i></button></td>';
                html += '</tr>';
                $("#tblLeaseTerms tbody").append(html);
            });
            $("#divLoader").hide();
        }
    });

}

var goToLeaseTerms = function (ID) {
    if (ID != null) {
        $("#divLoader").show();
        var model = { Id: ID };
        $.ajax({
            url: "/LeaseTerms/GetLeaseTermsDetails",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                $("#divLoader").hide();
                $('#hndLTID').val(response.model.LTID);
                $('#txtLeaseTerms').val(response.model.LeaseTerms);
                $('#txtOfferTerms').val(response.model.OfferTerms);

                if (response.model.FormAgent == true) {
                    $("#chkIsAgent").iCheck('check');
                }
                else {
                    $("#chkIsAgent").iCheck('uncheck');
                }
            }
        });
    }
}
var deleteLeaseTerms = function (ID) {
    if (ID != null) {
        $("#divLoader").show();
        var model = { Id: ID };
        $.alert({
            title: "",
            content: "Are you sure to remove Lease Tearm?",
            type: 'blue',
            buttons: {
                yes: {
                    text: 'Yes',
                    action: function (yes) {
                        $.ajax({
                            url: "/LeaseTerms/DeleteLeaseTermsDetails",
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
var clearLeaseTerms = function () {
    $('#hndLTID').val('0');
    $('#txtLeaseTerms').val('');
    $('#txtOfferTerms').val('');
    $("#chkIsAgent").iCheck('uncheck');
}

var count = 0;
var sortTableLeaseTerms = function (sortby) {

    var orderby = "";

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#sortLeaseTermsIcon').removeClass('fa fa-sort-up');
        $('#sortLeaseTermsIcon').removeClass('fa fa-sort-down');
        $('#sortOfferTermsIcon').removeClass('fa fa-sort-up');
        $('#sortOfferTermsIcon').removeClass('fa fa-sort-down');
        $('#sortAgentIcon').removeClass('fa fa-sort-up');
        $('#sortAgentIcon').removeClass('fa fa-sort-down');
        if (sortby == 'LeaseTerms') {
            $('#sortLeaseTermsIcon').addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'OfferTerms') {
            $('#sortOfferTermsIcon').addClass('fa fa-sort-up fa-lg');
        }
        if (sortby == 'Agent') {
            $('#sortAgentIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $('#sortLeaseTermsIcon').removeClass('fa fa-sort-up');
        $('#sortLeaseTermsIcon').removeClass('fa fa-sort-down');
        $('#sortOfferTermsIcon').removeClass('fa fa-sort-up');
        $('#sortOfferTermsIcon').removeClass('fa fa-sort-down');
        $('#sortAgentIcon').removeClass('fa fa-sort-up');
        $('#sortAgentIcon').removeClass('fa fa-sort-down');
        if (sortby == 'LeaseTerms') {
            $('#sortLeaseTermsIcon').addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'OfferTerms') {
            $('#sortOfferTermsIcon').addClass('fa fa-sort-down fa-lg');
        }
        if (sortby == 'Agent') {
            $('#sortAgentIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueLeaseTerms", sortby);
    localStorage.setItem("OrderByValueLeaseTerms", orderby);
    count++;
    getLeaseTermsList(sortby, orderby);
};