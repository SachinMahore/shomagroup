$(document).ready(function () {
    //fillStorageList();
    fillRPP_Storage();
    $('#ulPagination_Storage').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationStorageList(1);
        },
        onPageClick: function (page, evt) {
            $("#hdnCurrentPage").val(page);
            var SortByValueStorageManagement = localStorage.getItem("SortByValueStorageManagement");
            var OrderByValueStorageManagement = localStorage.getItem("OrderByValueStorageManagement");
            fillStorageSearchGrid(page, SortByValueStorageManagement, OrderByValueStorageManagement);
        }
    });
    
    btnSaveUpdate();
    getPropertyList();
});


var goToStorage = function (id) {
    var ID = id;
    if (ID != null) {
        $("#hndStorageID").val(ID);

        window.location.replace("/Admin/Storage/Index/" + ID);

    }
}
var fillRPP_Storage = function () {
    $("#ddlRPP_Storage").empty();
    $("#ddlRPP_Storage").append("<option value='25'>25</option>");
    $("#ddlRPP_Storage").append("<option value='50'>50</option>");
    $("#ddlRPP_Storage").append("<option value='75'>75</option>");
    $("#ddlRPP_Storage").append("<option value='100'>100</option>");
    $("#ddlRPP_Storage").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationStorageList($("#hdnCurrentPage").val());
    });
};
var buildPaganationStorageList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'StorageName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        Criteria: $("#txtCriteriaStorage").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Storage").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Storage/buildPaganationStorageList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                alert(response.error);
            } else {
                $('#ulPagination_Storage').pagination('updateItems', response.NOP);
                $('#ulPagination_Storage').pagination('selectPage', 1);
            }
        }
    });
};
var fillStorageSearchGrid = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = 'StorageName';
    }
    if (!orderby) {
        orderby = 'ASC';
    }
    var model = {
        Criteria: $("#txtCriteriaStorage").val(),
        PageNumber: pagenumber,
        NumberOfRows: $("#ddlRPP_Storage").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: '/Storage/FillStorageSearchGrid',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) !== "") {
                this.cancelChanges();
            } else {
                $("#tblStorage>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.StorageID + '" id="tr_' + elementValue.StorageID +'">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.StorageID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.StorageName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">$ ' + parseFloat(elementValue.Charges).toFixed(2) + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Description + '</td>';
                    html += '<td class="" style="color:#3d3939;" ><button class="btn btn-primary" style="padding: 5px 8px !important;margin-right:7px" onclick="goToStorage(' + elementValue.StorageID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delStorage(' + elementValue.StorageID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';
                    html += '</tr>';
                    $("#tblStorage>tbody").append(html);
                });
            }
            $("#hndPageNo").val(pagenumber);
        }
    });
};
$(document).keypress(function (e) {
    if (e.which === 13) {
        buildPaganationStorageList(1);
    }
});
var fillStorageList = function () {

    $.ajax({
        url: '/Storage/GetStorageList',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblStorage>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.StorageID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.StorageID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.StorageName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Charges + '</td>';
                    html += '</tr>';
                    $("#tblStorage>tbody").append(html);
                });
            }
        }
    });
}
//-----------------------------------------------------Add/Edit------------------------------------------//

var clearStorageData = function () {
    $("#hndStorageID").val("0");
    $("#txtStorageName").val("");
    btnSaveUpdate();
}
var saveUpdateStorage = function () {
    var msg = "";
    if ($.trim($("#ddlProperty").val()).length == 0) {
        msg = msg + "Property is required.</br>"
    }
    if ($.trim($("#txtStorageName").val()).length <= 0) {
        msg = msg + "Storage Name is required.</br>"
    }
    if ($.trim($("#txtCharges").val()).length <= 0) {
        msg = msg + "Charges is required.</br>"
    }
    if (msg != "") {
        msg = "Following field(s) is/are required</br>" + msg;
        $.alert({
            title: 'Message!',
            content: msg,
            type: 'red',
        });
    }
    else {
        var model = {
            StorageID: $("#hndStorageID").val(),
            PropertyID: $("#ddlProperty").val(),
            StorageName: $("#txtStorageName").val(),
            Charges: $("#txtCharges").val(),
            Description: $("#txtDescription").val()
        };
        $.ajax({
            url: "/Storage/SaveUpdateStorage",
            method: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                //hideProgress('#btnSaveUpdate');
                if (response.result == "1") {
                    if ($("#hndStorageID").val() == 0) {
                        $.alert({
                            title: 'Message!',
                            content: "Data Saved Successfully",
                            type: 'blue',
                        });
                    }
                    else {
                        $.alert({
                            title: 'Message!',
                            content: "Data Update Successfully",
                            type: 'blue',
                        });
                    }
                    $("#hndStorageID").val(response.ID);
                    $("#spanSaveUpdate").text("Save");
                    fillStorageList();
                    setInterval(function () {
                        window.location.replace("/Admin/Storage/Index/" + 0);
                    }, 1200)
                }
                else {
                    //showMessage("Error!", response.error);
                }
            }
        });
    }
}
var btnSaveUpdate = function () {

    if ($("#hndStorageID").val() == 0) {
        $("#btnSaveUpdateStorage").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdateStorage").text(" Save");
    }
    else {
        $("#btnSaveUpdateStorage").addClass("fa fa-save btn btn-success");
        $("#btnSaveUpdateStorage").text(" Update");
    }
}

var fillStorageSearchList = function () {
    var params = { SearchText: $("#txtCriteriaStorage").val() };
    $.ajax({
        url: '/Storage/GetStorageSearchList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblStorage>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.StorageID + '">';
                    html += '<td class="pds-id hidden" style="color:#3d3939;">' + elementValue.StorageID + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.StorageName + '</td>';
                    html += '<td class="pds-firstname" style="color:#3d3939;">' + elementValue.Charges + '</td>';
                    html += '</tr>';
                    $("#tblStorage>tbody").append(html);
                });
            }
        }
    });

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
var NewStorage = function () {
    window.location.replace("/Admin/Storage/Index/" + 0);
};

var delStorage = function (storageId) {
    var model = {
        StorageID: storageId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Storage?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Storage/DeleteStorage",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#tr_' + storageId).remove();
                        }
                    });
                }
            },
            no: {
                text: 'No',
                action: function (no) {
                }
            }
        }
    });
};

var count = 0;
var sortTableStorage = function (sortby) {

    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
        $('#sortStorageIcon').removeClass('fa fa-sort-up');
        $('#sortStorageIcon').removeClass('fa fa-sort-down');
        $('#sortChargesIcon').removeClass('fa fa-sort-up');
        $('#sortChargesIcon').removeClass('fa fa-sort-down');
        if (sortby == 'StorageName') {
            $('#sortStorageIcon').addClass('fa fa-sort-up fa-lg');
        }
        else if (sortby == 'ChargesName') {
            $('#sortChargesIcon').addClass('fa fa-sort-up fa-lg');
        }
    }
    else {
        orderby = "DESC";
        $('#sortStorageIcon').removeClass('fa fa-sort-up');
        $('#sortStorageIcon').removeClass('fa fa-sort-down');
        $('#sortChargesIcon').removeClass('fa fa-sort-up');
        $('#sortChargesIcon').removeClass('fa fa-sort-down');
        if (sortby == 'StorageName') {
            $('#sortStorageIcon').addClass('fa fa-sort-down fa-lg');
        }
        else if (sortby == 'ChargesName') {
            $('#sortChargesIcon').addClass('fa fa-sort-down fa-lg');
        }
    }
    localStorage.setItem("SortByValueStorageManagement", sortby);
    localStorage.setItem("OrderByValueStorageManagement", orderby);
    count++;
    buildPaganationStorageList(pNumber, sortby, orderby);
    fillStorageSearchGrid(pNumber, sortby, orderby);
};
