$(document).ready(function () {
    //getPromotionList();
    fillRPP_Promotion();
    $('#ulPagination_Promotion').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationPromotionList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            fillPromotionSearchGrid(page);
        }
    });
    getPropertyList();
    $('#tblPromotion tbody').on('click', 'tr', function () {
        $('#tblPromotion tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblPromotion tbody').on('dblclick', 'tr', function () {
        goToEditPromotion();
    });
});
var fillRPP_Promotion = function () {
    $("#ddlRPP_Promotion").empty();
    $("#ddlRPP_Promotion").append("<option value='25'>25</option>");
    $("#ddlRPP_Promotion").append("<option value='50'>50</option>");
    $("#ddlRPP_Promotion").append("<option value='75'>75</option>");
    $("#ddlRPP_Promotion").append("<option value='100'>100</option>");
    $("#ddlRPP_Promotion").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPromotionList($("#hdnCurrentPage").val());
    });
};
var buildPaganationPromotionList = function (pagenumber) {
    var model = {
        StartDate: $("#txtFromDate").val(),
        EndDate: $("#txtToDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Promotion").val()
    };
    $.ajax({
        url: '/Promotion/buildPaganationPromotionList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                $('#ulPagination_Promotion').pagination('updateItems', response.NOP);
                $('#ulPagination_Promotion').pagination('selectPage', 1);
            }
        }
    });
};
var fillPromotionSearchGrid = function (pagenumber) {
    var model = {
        StartDate: $("#txtFromDate").val(),
        EndDate: $("#txtToDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Promotion").val()
    };
    $.ajax({
        url: '/Promotion/FillPromotionSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                this.cancelChanges();
            } else {
                $("#tblPromotion>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.PromotionID + ">";
                    html += "<td>" + elementValue.PromotionID + "</td>";
                    html += "<td>" + elementValue.PropertyName + "</td>";
                    html += "<td>" + elementValue.PromotionTitle + "</td>";
                    html += "<td>" + elementValue.StartDate + "</td>";
                    html += "<td>" + elementValue.EndDate + "</td>";
                    html += "</tr>";
                    $("#tblPromotion>tbody").append(html);
                });
            }
        }
    });
};
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationPromotionList(1);
    }
});
var getPromotionList = function () {
    var model = {
        FromDate: $("#txtFromDate").val(),
        ToDate: $("#txtToDate").val()
    };
    $.ajax({
        url: "/Promotion/GetPromotionList",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#tblPromotion>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr data-value=" + elementValue.PromotionID + ">";
                html += "<td>" + elementValue.PromotionID + "</td>";
                html += "<td>" + elementValue.PropertyIDString + "</td>";
                html += "<td>" + elementValue.PromotionTitle + "</td>";
                html += "<td>" + elementValue.StartDateText + "</td>";
                html += "<td>" + elementValue.EndDateText + "</td>";
                html += "</tr>";
                $("#tblPromotion>tbody").append(html);
            });

        }
    });
}


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
var goPromotionList = function () {
    window.location.href = "/Admin/Promotion/";
};
var goToEditPromotion = function () {
    var row = $('#tblPromotion tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Promotion/Edit/" + ID;
    }
};

var addPromotion = function () {
    window.location.href = "/Admin/Promotion/Edit/0";
};

function saveupdatePromotion() {
    var msg = "";
    var promotionID = $("#hndPromotionID").val();
    var propertyid = $("#ddlProperty").val();
    var promotionTitle = $("#txtPromotionTitle").val();
    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();

    if (propertyid === "0") {
        msg += "Please Select Property</br>";
    }
    if (promotionTitle === "") {
        msg += "Please Enter Promotion Title</br>";
    }
    if (startDate === "") {
        msg += "Please Enter Start Date</br>";
    }
    if (endDate === "") {
        msg += "Please Enter End Date</br>";
    }
    if (msg !== "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
    var model = {
        PromotionID: promotionID,
        PropertyID: propertyid,
        PromotionTitle: promotionTitle,
        StartDate: startDate,
        EndDate: endDate,
        CreatedDate: "",
        CreatedById:""
    };

    $.ajax({
        url: "/Admin/Promotion/SaveUpdatePromotion/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: 'Message!',
                content: response.Msg,
                type: 'blue'
            });
        }
    });
}