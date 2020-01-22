$(document).ready(function () {
    getPropertyList();
   // getPropertyUnitList(1);

    $("#ddlProperty").on('change', function (evt, params) {        
        var selected = $(this).val();
       
        if (selected != null) {         
            getPropertyUnitList(selected);
        }
    });
});
var getPropertyList = function () {
    $.ajax({
        url: "/PropertyManagement/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlProperty").empty();
            $("#ddlProperty").append("<option value='0'>Select Property</option>");
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                $("#ddlProperty").append(option);

            });

        }
    });

};

var getPropertyUnitList = function (pid) {
    var model = { PID: pid }
    $.ajax({
        url: "/PropertyManagement/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#ddlUnit").empty();
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.UID + ">" + elementValue.UnitNo + "</option>";
                $("#ddlUnit").append(option);

            });

        }
    });

};

function saveupdateLease() {
    var msg = "";
    var lid = $("#hndLID").val();
    var property = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var revision_number = $("#txtRevision").val();
    var status = $("#ddlStatus").val();
    var being_terminated = $("#ddlBeingTerminated").val();
    var phase = $("#txtPhase").val();

    var noticedate = $("#txtNoticeDate").val();
    var termination_reason = $("#txtTerminateReason").val();
    var balanced = $("#txtBalance").val();
    var previous_balance = $("#txtPreviousbalance").val();
    var number_return_check = $("#txtNRChecks").val();

    var last_return_check_date = $("#txtLastReturnDate").val();
    var last_return_payment_id = $("#txtLastReturnPmt").val();
    var last_return_check_number = $("#txtLastReturnCheckNum").val();
    var original_lease_start_date = $("#txtOriginalStart").val();
    var actual_lease_start_date = $("#txtActualStart").val();

    var original_lease_end_date = $("#txtOriginalEnd").val();
    var actual_lease_end_date = $("#txtActualEnd").val();
    var intended_movein_date = $("#txtIntendedMoveIn").val();
    var actual_movein_date = $("#txtActualMoveIn").val();
    var intended_moveout_date = $("#txtIntendedMoveOut").val();

    var actual_moveout_date = $("#txtActualMoveOut").val();
    var statement_type = $("#txtStatementType").val();
    var last_rent_roll_date = $("#txtLastRentDate").val();
    var created_by = $("#txtCreatedBy").val();
    var created_date = $("#txtCreatedDate").val();

    var recounciled_date = $("#txtReconciledDate").val();
    var terms = $("#txtTerms").val();



    if (revision_number == "") {
        msg += "Plz enter Revision Number</br>";
    }


    if (balanced == "") {
        msg += "Plz enter Balance</br>";
    }
    if (previous_balance == "") {
        msg += "Plz enter Previous Balance</br>";
    }

    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        LID: lid,
        PID: property,
        UID: unitid,
        Revision_Num: revision_number,
        Status: status,
        Being_Terminated: being_terminated,
        Phase: phase,
        Notice_Date: noticedate,
        Termination_Reason: termination_reason,
        Balance: balanced,
        Previous_Balance: previous_balance,
        Number_Returned_Checks: number_return_check,

        Last_Return_Ck_Date: last_return_check_date,
        Last_Return_Pmt_ID: last_return_payment_id,
        Last_Return_Ck_Num: last_return_check_number,
        Original_Start: original_lease_start_date,
        Actual_Start: actual_lease_start_date,
        Original_Lease_End: original_lease_end_date,
        Actual_Lease_End: actual_lease_end_date,
        Intended_MoveIn_Date: intended_movein_date,
        Actual_MoveIn_Date: actual_movein_date,
        Intend_MoveOut_Date: intended_moveout_date,
        Actual_MoveOut_Date: actual_moveout_date,
        Statement_Type: statement_type,
        Last_Rent_Roll_Date: last_rent_roll_date,
        CreatedBy: created_by,
        CreatedDate: created_date,
        Reconciled: recounciled_date,
        Term: terms,
    };

    $.ajax({
        url: "/LeaseManagement/SaveUpdateLease/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: 'Message!',
                content: response.Msg,
                type: 'blue',
            });
        }


    });

}