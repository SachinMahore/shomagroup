$(document).ready(function () {
    getDocumentLists();
    TableClickDocument();
    getTenantList();
});

var SaveUpdateDocument = function () {

    var msg = "";
    var documentId = $("#hdnDocumentId").val();
    var tenantId = $("#ddlTenant").val();
    var documentName = $("#txtDocumentName").val();
    var documentType = $("#ddlDocumentType").val();
    var documentNumber = $("#txtDocumentNumber").val();

    if (tenantId == "0") {
        msg += "Please Select The Tenant </br>";
    }
    if (documentName == "") {
        msg += "Please Fill The DocumentName </br>";
    }
    if (documentType == "0") {
        msg += "Please Select The Document Type </br>";
    }
    if (msg != "") {
        $.alert({
            title: "Alert!",
            content: msg,
            type: 'red'
        })
        return;
    }

    $formData = new FormData();

    $formData.append('DocID', documentId);
    $formData.append('TenantID', tenantId);
    $formData.append('DocumentName', documentName);
    $formData.append('DocumentType', documentType);
    $formData.append('DocumentNumber', documentNumber);
    var photo = document.getElementById('wizard-picture');
    if (photo.files.length > 0) {
        for (var i = 0; i < photo.files.length; i++) {
            $formData.append('file-' + i, photo.files[i]);
        }

        $.ajax({
            url: '/Admin/Document/SaveUpdateDocument',
            type: 'post',
            data: $formData,
            contentType: false,
            processData: false,
            dataType: 'json',
            success: function (response) {
                $.alert({
                    title: 'Alert!',
                    content: response.msg,
                    type: 'blue'
                });
                setInterval(function () {
                    window.location.replace("/Admin/Document")
                }, 3000);
            }
        });
    }
}

var getDocumentLists = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    }
    $.ajax({
        url: "/Document/GetDocumentData",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblDocument>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.DocID + ">";
                html += "<td>" + elementValue.TenantName + "</td>";
                html += "<td>" + elementValue.DocumentName + "</td>";
                html += "<td>" + elementValue.DocumentType + "</td>";
                html += "<td>" + elementValue.DocumentNumber + "</td>";
                html += "</tr>";
                $("#tblDocument>tbody").append(html);
            });

        }
    });
}

var TableClickDocument = function () {

    $('#tblDocument tbody').on('click', 'tr', function () {
        $('#tblDocument tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblDocument tbody').on('dblclick', 'tr', function () {
        goToEditDocument();
    });
}

var goToEditDocument = function () {

    var row = $('#tblDocument tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID != null) {

        window.location.replace("/Admin/Document/Edit/" + ID);
    } else {

    }
}

var addNewDocument = function () {
    window.location.replace("/Admin/Document/Edit/" + 0)
}

var goDocumentList = function () {
    window.location.replace("/Admin/Document/")
};

var getTenantList = function () {
    
    $.ajax({
        url: "/Admin/Tenant/FillTenantDropDownList",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#ddlTenant").empty();
            $.each(response.result, function (elementType, elementValue) {
                var option = "<option value=" + elementValue.ID + ">" + elementValue.FullName + "</option>";
                $("#ddlTenant").append(option);

            });

        }
    });
};

