var selectFieldCampaingDataSource = [
    { text: "State Name", value: "StateName" },
];
var goToState = function () {
    var row = $('#StateGrid tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {
        //showProgress('#btnGoCountReq');
        window.location.href = "state/edit/" + ID;
    } else {
        //showError("Error!", "Please select a campaign!");
        //hideProgress('#btnGoCountReq');
    }
}
var newSearchState = function () {
    window.location.href = "state/new/0";
}
var fillStateList = function () {
    $.ajax({
        url: '../Admin/State/GetStateList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#StateGrid>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.ID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.StateName + '</td>';
                    html += '<td class="pds-lastname" style="color:#3d3939;">' + elementValue.Abbreviation + '</td>';
                    html += '</tr>';
                    $("#StateGrid>tbody").append(html);
                });
            }
        }
    });
}

$(document).ready(function () {
    fillStateList();

    $('#StateGrid tbody').on('click', 'tr', function () {
        $('#StateGrid tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#StateGrid tbody').on('dblclick', 'tr', function () {
        goToState();
    });
});
$(document).keypress(function (e) {
    if (e.which == 13) {
        fillStateList();
    }
});
