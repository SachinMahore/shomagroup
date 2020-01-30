$(document).ready(function () {
    getPropertyList();
    getPropertyUnitList(1);

    getWorkOrderList();
    $('#tblWorkOrder tbody').on('click', 'tr', function () {
        $('#tblWorkOrder tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblWorkOrder tbody').on('dblclick', 'tr', function () {
        goToEditProspect();
    });
});

var getWorkOrderList = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    }
    $.ajax({
        url: "/WorkOrder/GetWorkOrderList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblWorkOrder>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.WOID + ">";
                html += "<td>" + elementValue.PropertyIDString + "</td>";
                html += "<td>" + elementValue.UnitIDString + "</td>";
                html += "<td>" + elementValue.ProblemID + "</td>";
                html += "<td>" + elementValue.DateOpenedString + "</td>";
                html += "<td>" + elementValue.DateClosedString + "</td>";
                html += "<td>" + elementValue.ReportedBy + "</td>";
                html += "<td>" + elementValue.AssignedTo + "</td>";
                html += "<td>" + elementValue.VendorID + "</td>";
               
                html += "</tr>";
                $("#tblWorkOrder>tbody").append(html);
            });

        }
    });
}

var goToEditProspect = function () {

    var row = $('#tblWorkOrder tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {
        //showProgress('#btnGoCountReq');
        window.location.href = "../WorkOrder/Edit/" + ID;
    } else {
        //showError("Error!", "Please select a campaign!");
        //hideProgress('#btnGoCountReq');
    }
}

var getPropertyList = function () {
    $.ajax({
        url: "/WorkOrder/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlProperty").empty();
            $.each(response.model, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.PID + ">" + elementValue.Title + "</option>";
                $("#ddlProperty").append(option);

            });

        }
    });

};

var getPropertyUnitList = function (pid) {
    var model={PID:pid}
    $.ajax({
        url: "/WorkOrder/GetPropertyUnitList/",
        type: "post",
        contentType: "application/json utf-8",
        data:JSON.stringify(model),
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

function saveupdateWorkOrder()
{
    var msg = "";
    var woid = $("#hndWOID").val();
    var propertyid = $("#ddlProperty").val();
    var unitid = $("#ddlUnit").val();
    var problemid = $("#ddlProblem").val();
    var opendate = $("#txtOpendate").val();
    var closedate = $("#txtClosedate").val();
    var desc = $("#txtDesc").val();
    var reportedby = $("#txtReport").val();
    var contact = $("#txtContact").val();
    var assignto = $("#txtAssign").val();
    var vendor = $("#txtVendor").val();
    var resolution = $("#txtResolution").val();
   
    if (problemid == "0")
    {
        msg += "Plz select Problem</br>";
    }
    if (opendate == "") {
        msg += "Plz enter Open Date</br>";
    }
    if (reportedby == "") {
        msg += "Plz enter Reported By</br>";
    }
    if (contact == "") {
        msg += "Plz enter Contact</br>";
    }
    if (assignto == "") {
        msg += "Plz enter Assign To</br>";
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
        WOID:woid,
        PropertyID: propertyid,
        UnitID: unitid,
        ProblemID: problemid,
        DateOpened: opendate,
        DateClosed: closedate,
        Description: desc,
        ReportedBy: reportedby,
        ContactPhone: contact,
        AssignedTo: assignto,
        VendorID: vendor,
        Resolution: resolution
    };

    $.ajax({
        url: "/WorkOrder/SaveUpdateWorkOrder/",
        type: "post",
        contentType: "application/json utf-8",
        data:JSON.stringify(model),
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

var addNewWorkOrder = function () {
    window.location.href = "../WorkOrder/Edit/0";
}