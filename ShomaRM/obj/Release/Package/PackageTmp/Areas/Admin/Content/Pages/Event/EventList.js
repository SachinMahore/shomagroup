var goToEditEvent = function () {
    var row = $('#tblEvent tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
    if (ID !== null) {
        window.location.href = "/Admin/Event/Edit/" + ID;
    }
};
var addNewEvent = function () {
    window.location.href = "/Admin/Event/Edit/0";
};
var fillRPP_EventList = function () {
    $("#ddlRPP_EventList").empty();
    $("#ddlRPP_EventList").append("<option value='25'>25</option>");
    $("#ddlRPP_EventList").append("<option value='50'>50</option>");
    $("#ddlRPP_EventList").append("<option value='75'>75</option>");
    $("#ddlRPP_EventList").append("<option value='100'>100</option>");
    $("#ddlRPP_EventList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationEventList($("#hdnCurrentPage_FL").val());
    });
};
var buildPaganationEventList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'EventName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: 1,
        NumberOfRows: $("#ddlRPP_EventList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: "/Event/BuildPaganationEventList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                if (response.NOP == 0) {
                    $('#divPagination_EventList').addClass("hidden");
                }
                else {
                    $('#divPagination_EventList').removeClass("hidden");
                    $('#ulPagination_EventList').pagination('updateItems', response.NOP);
                    $('#ulPagination_EventList').pagination('selectPage', 1);
                }
            }
        }
    });
};
var fillEventList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'EventName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        ToDate: $("#txtToDate").val(),
        FromDate: $("#txtFromDate").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_EventList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Event/FillEventSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#tblEvent>tbody").empty();
                $.each(response, function (elementType, elementValue) {
                    var html = "<tr data-value=" + elementValue.EventID + ">";
                    html += "<td>" + elementValue.EventName + "</td>";
                    html += "<td>" + elementValue.PropertyName + "</td>";
                    html += "<td>" + elementValue.EventDate + "</td>";
                    if (elementValue.Type == 1) {
                        html += "<td> Neighborhood </td>";
                    }
                    else if (elementValue.Type == 2) {
                        html += "<td> Community </td>";
                    }
                    else if (elementValue.Type == 3) {
                        html += "<td> Other </td>";
                    }
                    else if (elementValue.Type == 4) {
                        html += "<td> Multiple </td>";
                    }
                    else if (elementValue.Type == 5) {
                        html += "<td> Current Date </td>";
                    }
                    html += "<td align='center'><img src='/content/assets/img/Event/" + elementValue.Photo + "' class='picture-src' title='' style='height:70px;width:70px;'/></td>";
                    html += "</tr>";
                    $("#tblEvent>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
        }
    });
};
$(document).ready(function () {
    fillRPP_EventList();
    $('#ulPagination_EventList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_FL").val(1);
            buildPaganationEventList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage_FL").val(page);
            var SortByValueEvent = localStorage.getItem("SortByValueEvent");
            var OrderByValueEvent = localStorage.getItem("OrderByValueEvent");
            fillEventList(page, SortByValueEvent, OrderByValueEvent);
        }
    });
    $('#tblEvent tbody').on('click', 'tr', function () {
        $('#tblEvent tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row");
    });
    $('#tblEvent tbody').on('dblclick', 'tr', function () {
        goToEditEvent();
    });
});
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationEventList(1);
    }
});

var count = 0;
var sortTableEvent = function (sortby) {

    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#sortEventIcon').removeClass('fa fa-sort-up');
        $('#sortEventIcon').removeClass('fa fa-sort-down');
        $('#sortPropertyIcon').removeClass('fa fa-sort-up');
        $('#sortPropertyIcon').removeClass('fa fa-sort-down');
        $('#sortEventDateIcon').removeClass('fa fa-sort-up');
        $('#sortEventDateIcon').removeClass('fa fa-sort-down');
        $('#sortTypeIcon').removeClass('fa fa-sort-up');
        $('#sortTypeIcon').removeClass('fa fa-sort-down');
        if (sortby == 'EventName') {
            $('#sortEventIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'PropertyName') {
            $('#sortPropertyIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'EventDateName') {
            $('#sortEventDateIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'TypeName') {
            $('#sortTypeIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $('#sortEventIcon').removeClass('fa fa-sort-up');
        $('#sortEventIcon').removeClass('fa fa-sort-down');
        $('#sortPropertyIcon').removeClass('fa fa-sort-up');
        $('#sortPropertyIcon').removeClass('fa fa-sort-down');
        $('#sortEventDateIcon').removeClass('fa fa-sort-up');
        $('#sortEventDateIcon').removeClass('fa fa-sort-down');
        $('#sortTypeIcon').removeClass('fa fa-sort-up');
        $('#sortTypeIcon').removeClass('fa fa-sort-down');
        if (sortby == 'EventName') {
            $('#sortEventIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'PropertyName') {
            $('#sortPropertyIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'EventDateName') {
            $('#sortEventDateIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'TypeName') {
            $('#sortTypeIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueEvent", sortby);
    localStorage.setItem("OrderByValueEvent", orderby);
    count++;
    buildPaganationEventList(pNumber, sortby, orderby);
    fillEventList(pNumber, sortby, orderby);
};

