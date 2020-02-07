$(document).ready(function () {
    onFocus();
    fillModelDDL();
    getPropertyList();
    getAmenityList();
    getModelsList();
    
    TableClickModels();
    //fillRPP_PUList();
    fillStateDDL();
    fillStateDDL1();
    getPremiumTypeList();
    $("#ddlState").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityList(selected);
        }
    });

    $("#ddlRPP_PUList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPUList($("#hdnCurrentPage_PU").val());
    });
    $('#ulPagination_PUList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            $("#hdnCurrentPage_VL").val(1);
            buildPaganationPUList(1, "UnitNo", "ASC");
        },
        onPageClick: function (page, evt) {
            var sortByValue = localStorage.getItem("SortByValue");
            var OrderByValue = localStorage.getItem("OrderByValue");
            $("#hdnCurrentPage_VL").val(page);
            fillPUList(page, sortByValue, OrderByValue);
        }
    });
});
var getPropertyList = function () {   
    var city = 0;
    if ($("#ddlCity1").val() != null) {
        city = $("#ddlCity1").val();
    }
    var searchText = $("#txtSearch").val();
    var model = { City: city, SearchText: searchText };
    $.ajax({
        url: "/Admin/PropertyManagement/SearchPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
                $("#list-type").empty();
                $.each(response.model, function (elementType, value) {
                    var html = "<div class='col-sm-6 col-md-3 p0'><div class='box-two proerty-item'>";
                    html += "<div class='item-thumb'><a href = '#' onclick='getPropertyDetails(" + value.PID +")'> <img src='/content/assets/img/demo/" + value.Picture+"'></a></div> ";
                    html += " <div class='item-entry overflow'><h5><a href = '#' onclick='getPropertyDetails(" + value.PID +")'> " + value.Title + "</a></h5>  <div class='dot-hr'></div>";
                    html += " <span class='pull-left'><b> Units :</b> " + value.NoOfUnits + "</span>";
                    html += "<span class='proerty-price pull-right'><b> Floors :</b>" + value.NoOfFloors + " </span>";
                    html += "<p style='display: none;'>" + value.Description +"</p> </div> </div> </div>";
                    $("#list-type").append(html);
                });
            }
        }
    });
}
var getPropertyDetails = function (pid) {
    // $("#hndPID").val(pid);
    window.location = "/Admin/PropertyManagement/EditProperty/" + pid;
};
//---------------------------------Property Units----------------------------------------//

var fillRPP_PUList = function () {
    //$("#ddlRPP_PUList").empty();
    //$("#ddlRPP_PUList").append("<option value='10'>10</option>");
    //$("#ddlRPP_PUList").append("<option value='25' selected>25</option>");
    //$("#ddlRPP_PUList").append("<option value='50'>50</option>");
    //$("#ddlRPP_PUList").append("<option value='75'>75</option>");
    //$("#ddlRPP_PUList").append("<option value='100'>100</option>");
    $("#ddlRPP_PUList").on('change', function (evt, params) {
        var selected = $(this).val();
        buildPaganationPUList($("#hdnCurrentPage_PU").val());
    });
};
var buildPaganationPUList = function (pagenumber, sortby, orderby) {
    if (!sortby) {
        sortby = "UnitNo";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    $("#divLoader").show();
    var model = {
        PID: $("#hndPID").val(),
        PN: pagenumber,
        NOR: $("#ddlRPP_PUList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };

    $.ajax({
        url: "/PropertyManagement/BuildPaganationPUList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) !== "") {
               
            } else {
                $('#ulPagination_PUList').pagination('updateItems', response.NOP);
                $('#ulPagination_PUList').pagination('selectPage', 1);
            }
        }
    });
};
var fillPUList = function (pagenumber, sortby, orderby) {
    $("#divLoader").show();
    if (!sortby) {
        sortby = "UnitNo";
    }
    if (!orderby) {
        orderby = "ASC";
    }
    var model = {
        PID: $("#hndPID").val(),
        PN: pagenumber,
        NOR: $("#ddlRPP_PUList").val(),
        SortBy: sortby,
        OrderBy: orderby
    };
    $.ajax({
        url: "/PropertyManagement/FillPUSearchGrid",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
           
            if ($.trim(response.error) !== "") {
                //this.cancelChanges();
            } else {
                $("#listUnit>tbody").empty();
                $("#tblPUList>tbody").empty();
                $.each(response, function (elementType, value) {
                    var html = "<tr id='tru_" + value.UID + "'>";
                    html += "<td style='width:5%;'><a href = 'javascript:void(0)' onclick='goToStep(6," + value.UID + ")'> " + value.UnitNo + "</a></td>";
                    html += "<td style='width:4%;'>" + value.Building + "</td>";
                    
                    html += "<td style='width:6%;cursor:pointer!important;' id='avUnitRent_" + value.UID + "' onclick='editUnitRent(" + value.UID + ")' data-udate='" + value.Current_Rent + "'> " + formatMoney(parseFloat(value.Current_Rent).toFixed(2)) + " <i class='fa fa-edit  pull-right' style='margin: 6px;'></i> </td>";
                    html += "<td style='width:2%;'>" + value.Bathroom + "</td>";
                    html += "<td style='width:2%;'>" + value.Bedroom + "</td>";
                    html += "<td style='width:6%;'>" + value.InteriorArea + "</td>";
                    html += "<td style='width:6%;'>" + value.BalconyArea + "</td>";
                  
                    html += "<td style='width:5%;'>" + value.Area + "</td>";
                    html += "<td style='width:8%;cursor:pointer!important;' id='avUnitDate_" + value.UID + "' onclick='editUnitDate(" + value.UID + ")' data-udate='" + value.AvailableDateText + "'> " + value.AvailableDateText + " <i class='fa fa-calendar pull-right' style='margin: 6px;'></i> </td>";
                    html += "<td style='width:8%;cursor:pointer!important;' id='avUnitMoveInDate_" + value.UID + "' onclick='editUnitMoveInDate(" + value.UID + ")' data-udate='" + value.MoveInDateText + "'> " + value.MoveInDateText + " <i class='fa fa-calendar pull-right' style='margin: 6px;'></i> </td>";
                    html += "<td style='width:8%;cursor:pointer!important;' id='avUnitMoveOutDate_" + value.UID + "' onclick='editUnitMoveOutDate(" + value.UID + ")' data-udate='" + value.MoveOutDateText + "'> " + value.MoveOutDateText + " <i class='fa fa-calendar pull-right' style='margin: 6px;'></i> </td>";
                    html += "<td style='width:20%;'>" + value.PremiumType+ "</td>";

                    html += "<td style='padding:20px;cursor:pointer!important;' id='avUnitNotes_" + value.UID + "' onclick='editUnitNotes(" + value.UID + ")' data-udate='" + value.Notes + "'> " + value.Notes + " <i class='fa fa-edit pull-right' style='margin: 6px;'></i> </td>";
                    //html += '<td class="" style="color:#3d3939;width:10%;" ><button class="btn btn-primary ParkingEdit" style="padding: 5px 8px !important;margin-right:7px" onclick="goToStep(6, ' + value.UID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delUnit(' + value.UID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';

                    html += " </tr>";
                    $("#listUnit>tbody").append(html);
                    $("#tblPUList>tbody").append(html);
                });
                
            }
            $("#tru_" + $("#hndUID").val()).addClass("select-unit");
          
            $("#hndPageNo").val(pagenumber);
            $("#divLoader").hide();
        }
    });
};
var getModelsList = function () {
    $("#divLoader").show();
    $.ajax({
        url: "/Models/getModelsList",
        type: "post",
        contentType: "application/json utf-8",
        dataType: "JSON",
        success: function (response) {
            $("#tblModels>tbody").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<tr id='trm_" + elementValue.ModelID +"' data-value=" + elementValue.ModelID + ">";

                html += "<td style = 'width:10%;'><a href = 'javascript:void(0)' onclick='goToStep(4," + elementValue.ModelID + ")'><img src='/content/assets/img/plan/" + elementValue.FloorPlan + "'Style = 'max-width:60%;'/></a></td>";
                html += "<td style = 'width:10%;'><a href = 'javascript:void(0)' onclick='goToStep(4," + elementValue.ModelID + ")'> " + elementValue.ModelName + "</a></td>";
                html += "<td style='width:10%;'>" + elementValue.InteriorArea + "</td>";
                html += "<td style='width:10%;'>" + elementValue.BalconyArea + "</td>";
                html += "<td Style = 'width:10%;'>" + elementValue.Area + "</td>";
                html += "<td Style = 'width:10%;'>" + elementValue.RentRange + "</td>";
                html += "<td Style = 'width:10%;'>" + elementValue.Bedroom + "</td>";
                html += "<td Style = 'width:10%;'>" + elementValue.Bathroom + "</td>";
      
                html += '<td class="" style="color:#3d3939;width:10%;" ><button class="btn btn-primary ParkingEdit" style="padding: 5px 8px !important;margin-right:7px" onclick="goToStep(4, ' + elementValue.ModelID + ')"><i class="fa fa-edit" aria-hidden="true"></i></button><button class="btn btn-danger" style="padding: 5px 8px !important;" onclick="delModel(' + elementValue.ModelID + ')"><i class="fa fa-trash" aria-hidden="true"></i></button></td>';

                html += "</tr>";
                $("#tblModels>tbody").append(html);
            });
            $("#divLoader").hide();
        }
    });
};
var TableClickModels = function () {
    $('#tblModels tbody').on('click', 'tr', function () {
        $('#tblModels tr').removeClass("pds-selected-row");
        $(this).addClass("pds-selected-row")
    });
    $('#tblModels tbody').on('dblclick', 'tr', function () {
        goToEditModels();
    });
}
var goToEditModels = function () {
    var row = $('#tblModels tbody tr.pds-selected-row').closest('tr');
    var ID = $(row).attr("data-value");
}

var addNewModels = function () {
    //clearModels();
    gotoStep(4, 0);
    //window.location.replace("/Admin/Models/Edit/" + 0)
}
//---------------------------------Property Units----------------------------------------//
var getPropertyUnitDetails = function (uid) {
    $("#divLoader").show();
    var model = { UID: uid };
    $.ajax({
        url: "/Property/GetPropertyUnitDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $(".popup-overlay, .popup-content").removeClass("active");
            $("#lblUnitNo").text("Floor" + response.model.FloorNo + "- Unit " + response.model.UnitNo);
            $("#lblRent").text("$" + formatMoney(response.model.Current_Rent));
            $("#lblArea").text(response.model.Area);
            $("#lblBed").text( response.model.Bedroom);
            $("#lblBath").text(response.model.Bathroom);
            $("#lblHall").text(response.model.Hall);
            $("#lblDeposit").text("$" + formatMoney(response.model.Deposit));
            $("#lblLease").text( response.model.Leased);
           // $("#lblDeposit").text(response.model.Rent);
            $("#imgFloorPlan").attr("src", "/content/assets/img/property-" + response.model.PID + "/" + response.model.FloorPlan + "");
            $("#imgFloorPlan2").attr("src", "/content/assets/img/property-" + response.model.PID + "/" + response.model.FloorPlan + "");
            $("#lblUnitNo1").text("Selected : Floor" + response.model.FloorNo + "- Unit " + response.model.UnitNo + "  ($" + formatMoney(response.model.Current_Rent) + ")");
            $("#hndUID").val(uid);
            $("#divLoader").hide();
        }
    })
}
function displayImg() {
    $("#popFloorPlan").PopupWindow("open");
}
var getAmenityList = function () {
    var params = { Amenity: "" };
    $.ajax({
        url: '/Admin/Amenities/GetAmenityList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $("#tblAminities").empty();
                $.each(response, function (index, elementValue) {
                    
                    var html = '';
                    html += "<div class='col-sm-3'><div class='form-group'><div class='checkbox' style='position: unset'><label><input type='checkbox' id='chkAmenity" + elementValue.ID + "' value=" + elementValue.ID + " style='margin-top: 7px' class='addame' onclick='selectAddAmenity(" + elementValue.ID + ")'>";
                    html += "" + elementValue.Amenity + " </label></div ></div ></div >";                
                    $("#tblAminities").append(html);
                });
            }
        }
    });
}
var addAmenityArray = [];
function selectAddAmenity(id) {
    addAmenityArray = [];
    $('.addame').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var pkid = $(obj).attr("value");
            addAmenityArray.push(parseFloat(pkid));
        }
    });
}
var getPremiumTypeList = function () {
  
    var params = { SearchText: "" };
    $.ajax({
        url: '/PremiumType/GetPremiumTypeList',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlPremium").empty();
                $("#ddlPremium").append("<option value='0'>Select Premium Type</option>");
                $.each(response.model, function (index, elementValue) {
                    $("#ddlPremium").append("<option value=" + elementValue.PTID + ">" + elementValue.PremiumType + "</option>");
                });
            }
           
        }
    });

}

function SaveUpdateProperty() {
    $("#divLoader").show();
    var msg = "";
    var pid = $("#hndPID").val();
    var title = $("#txtPropTitle").val();
    var description = $("#txtDesc").val();
    var units = $("#txtUnits").val();
    var floors = $("#txtNoOfFloor").val();
    var builtin = $("#ddlBuiltIn").val();
    var status = $("#ddlStatus").val();
    var parking = $("#txtParking").val();
    var waterfront = $("#txtWaterfront").val();
    var address = $("#txtAddress").val();
    var googleloc = $("#txtLocationGoogle").val();
    var utube = $("#txtYouTube").val();
    var state = $("#ddlState").val();
    var city = $("#ddlCity").val();
    var applicationFees = unformatText($("#txtApplicationFees").val());
    var adminFees = unformatText($("#txtAdminFees").val());
    var guarantorFees = unformatText($("#txtGuarantorFees").val());
    var pestControlFess = unformatText($("#txtPestControlFees").val());
    var trashFees = unformatText($("#txtTrashFees").val());
    var conversionFees = unformatText($("#txtConversionBill").val());
    if (title == "") {
        msg += " Please enter Property Title .<br />";
    }
    if (floors == "0") {
        msg += " Please enter No. of floors .<br />";
    }
    if (description == "") {
        msg += " Please enter Property description .<br />";
    }
    if (units == "") {
        msg += " Please enter No. of units .<br />";
    }
    if (builtin == "0") {
        msg += " Please select built in year.<br />";
    }
    if (status == "0") {
        msg += " Please select status .<br />";
    }
    //if (state == "0") {
    //    msg += " Please select state .<br />";
    //}
    if (address == "") {
        msg += " Please enter address .<br />";
    }
  
    if (!applicationFees) {
        msg += " Please enter Application Fees.<br />";
    }
    if (!adminFees) {
        msg += " Please enter Admin Fees.<br />";
    }
    if (!guarantorFees) {
        msg += " Please enter Guarantor Fees.<br />";
    }
    if (!pestControlFess) {
        msg += " Please enter Pest Control.<br />";
    }
    if (!trashFees) {
        msg += " Please enter Trash Fees.<br />";
    }
    if (!conversionFees) {
        msg += " Please enter Conversion Fees.<br />";
    }
    if (msg != "") {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    $formData = new FormData();

    $formData.append('PID', pid);
    $formData.append('Title', title);
    $formData.append('Location', address);
    $formData.append('LocationGoogle', googleloc);
    $formData.append('BuiltIn', builtin);
    $formData.append('Waterfront', waterfront);
    $formData.append('Description', description);
    $formData.append('NoOfFloors', floors);
    $formData.append('NoOfUnits', units);
    $formData.append('Parking', parking);
    $formData.append('City', 2);
    $formData.append('State',2);
    $formData.append('YouTube', utube);
    $formData.append('Status', status);
    $formData.append('ApplicationFees', applicationFees);
    $formData.append('AdminFees', adminFees);
    $formData.append('GuarantorFees', guarantorFees);
    $formData.append('PestControlFees', pestControlFess);
    $formData.append('TrashFees', trashFees);
    $formData.append('ConversionBillFees', conversionFees);

    var $file = document.getElementById('wizard-picture');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }

    var addAmenity = "";
    if (addAmenityArray.length > 0) {
        for (var j = 0; j < addAmenityArray.length; j++) {
            if (addAmenity == "") {
                addAmenity = addAmenityArray[j];
            } else {
                addAmenity += "," + addAmenityArray[j];
            }
        }
    } else {
        addAmenity = "0";
    }
 
    $formData.append('Amenities', addAmenity);

    $.ajax({
        url: "/Admin/PropertyManagement/SaveUpdateProperty/",
        type: "post",
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $("#divLoader").show();
            $.alert({
                title: 'Message!',
                content: response.Msg,
                type: 'blue',
            });
            $("#btnGoToProp").removeAttr("disabled");
            $("#btnAddUnit").removeAttr("disabled");
        }
    });

}

var fillStateDDL = function () {

    $.ajax({
        url: '/City/FillStateDropDownList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlState").empty();
                $("#ddlState").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillCityList = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlCity").empty();
                $("#ddlCity").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlCity").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var fillStateDDL1 = function () {

    $.ajax({
        url: '/City/FillStateDropDownList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlState1").empty();
                $("#ddlState1").append("<option value='0'>Select State</option>");
                $.each(response, function (index, elementValue) {
                    $("#ddlState1").append("<option value=" + elementValue.ID + ">" + elementValue.StateName + "</option>");
                });
            }
        }
    });
}
var fillCityList1 = function (stateid) {
    var params = { StateID: stateid };
    $.ajax({
        url: '/City/GetCityListbyState',
        method: "post",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlCity1").empty();
                $("#ddlCity1").append("<option value='0'>Select City</option>");
                $.each(response, function (index, elementValue) {
                    
                    $("#ddlCity1").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
}
var gotoPropList = function () {
    window.location = "/Admin/PropertyManagement/";
}

var addPropUnit = function () {
    // $("#hndPID").val(pid);
    window.location = "/Admin/PropertyManagement/EditPropUnit/0";
}

var saveUpdateFloor = function () {
    $("#divLoader").show();
    var msg = "";
    var pid = $("#hndPID").val();
    var floorno = $("#ddlFloorList").val();
    var cord = $("#txtcord").val();

    if (cord == "") {
        msg += " Please enter cordinates .<br />";
    }
    if (floorno == "0") {
        msg += " Please select floor no .<br />";
    }

    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
    //var model = { PID: pid, FloorNo: floorno, Coordinates: cord };
    $formData = new FormData();

    $formData.append('PID', pid);
    $formData.append('FloorNo', floorno);
    $formData.append('Coordinates', cord);
   
    var $file = document.getElementById('UnitPlan-picture');

    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    } else {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: "Upload Floor Plan Image",
            type: 'red'
        });
        return;
    }

    $.ajax({
        url: "/Admin/PropertyManagement/SaveUpdateFloor/",
        type: "post",
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $("#divLoader").hide();
            $.alert({
                title: 'Message!',
                content: "Floor Added Successfully",
                type: 'blue',
            });
            resetcords();
        }


    });
}

var goToStep = function (stepid, id) {
    if (stepid == "1") {
        $("#divLoader").show();
        $("#li1").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#li6").removeClass("active");
        $("#li5").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step1").removeClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#divLoader").hide();
    }
    if (stepid == "2") {
        $("#divLoader").show();
        var noffloor = $("#txtNoOfFloor").val();
        
        $("#ddlFloorList").empty();
        $("#ddlFloorList").append("<option value='0'>Select Floor</option>");
        for (var i = 1; i <= noffloor; i++) {
            $("#ddlFloorList").append("<option value=" + i + ">" + i + "</option>");
        }
        $("#li6").removeClass("active");
        $("#li5").removeClass("active");
        $("#li2").addClass("active");
        $("#li1").removeClass("active");
        $("#li3").removeClass("active");
        $("#li4").removeClass("active");
        $("#step1").addClass("hidden");      
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step2").removeClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#divLoader").hide();
    }
    if (stepid == "3") {
        $("#divLoader").show();
        getModelsList();
        //getPropertyUnitList($("#hndPID").val());
       // buildPaganationPUList($("#hdnCurrentPage_PU").val());
        $("#li6").removeClass("active");
        $("#li5").removeClass("active");
        $("#li3").addClass("active");
        $("#li2").removeClass("active");
        $("#li1").removeClass("active");
        $("#li4").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step3").removeClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
        $("#divLoader").hide();
    }
    if (stepid == "4") {
        $("#divLoader").show();
        //clearUnit();
        $.get("../../PropertyManagement/EditPropModel/?id=" + id, function (data) {
            $("#step4").html(data);
            $("#divLoader").hide();
        });

        $("#li6").removeClass("active");
        $("#li5").removeClass("active");
        $("#li4").addClass("active");
        $("#li2").removeClass("active");
        $("#li3").addClass("active");
        $("#li1").removeClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").removeClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").addClass("hidden");
    }
    if (stepid == "5") {
     
        //getPropertyUnitList($("#hndPID").val());
        buildPaganationPUList($("#hdnCurrentPage_PU").val(),'UnitNo','ASC');
        $("#tru_" + $("#hndUID").val()).addClass("select-unit");
        $("#li3").removeClass("active");
        $("#li2").removeClass("active");
        $("#li1").removeClass("active");
        $("#li4").removeClass("active");
        $("#li6").removeClass("active");
        $("#li5").addClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step5").removeClass("hidden");
        $("#step6").addClass("hidden");
    }
    if (stepid == "6") {
        $("#hndUID").val(id);
        
       // $("#tru_" + $("#hndUID").val()).removeClass("select-unit");
        //$("#tru_" + $("#hndUID").val()).addClass("select-unit");

        $("#divLoader").show();
        clearUnit();
        $.get("../../PropertyManagement/EditPropUnit/?id=" + id, function (data) {
            $("#step6").html(data);
            $("#divLoader").hide();
        });
        $("#li4").removeClass("active");
        $("#li2").removeClass("active");
        $("#li3").removeClass("active");
        $("#li1").removeClass("active");
        $("#li6").addClass("active");
        $("#li5").addClass("active");
        $("#step2").addClass("hidden");
        $("#step1").addClass("hidden");
        $("#step3").addClass("hidden");
        $("#step4").addClass("hidden");
        $("#step5").addClass("hidden");
        $("#step6").removeClass("hidden");
    }
}

// Unit
function SaveUpdatePropertyUnit() {
    var msg = "";
    var pID = $("#hndPID").val();
    var uID = $("#hndUID").val();
    var unitNo = $("#txtPropUnit").val();
    //var rooms = $("#txtRooms").val();
    var rooms = "1";
    var bedroom = $("#txtBedroom").val();
    var bathroom = $("#txtBathroom").val();
    var premium = $("#ddlPremium").val();
    //var hall = $("#txtHall").val();
    var hall = 1;
    var deposit = $("#txtDeposit").val();
    var current_Rent = unformatText($("#txtCurrentRent").val());
    //var previous_Rent = $("#txtPreviousRent").val();
    var previous_Rent = "0.00";
    //var market_Rent = $("#txtMarketRent").val();
    var market_Rent = "0.00";
    //var wing = $("#txtWing").val();
    var wing = "A";
    //var building = $("#txtBuilding").val();
    var building = $("#ddlModel").val();;
    //var leased = $("#txtLeased").val();
    var leased = "12";
    //var petDetails = $("#txtPetDetails").val();
    var petDetails = "Pet Details";
    var floorNo = $("#ddlFloorNo").val();
    var area = $("#txtArea").val();
    var floorPlanCord = $("#txtunitcord").val();
    //var carpet_Color = $("#txtCarpet_Color").val();
    var carpet_Color ="Colour";
    //var wall_Paint_Color = $("#txtWall_Paint_Color").val();
    var wall_Paint_Color = "wall_Paint";
    //var furnished = $("#chkFurnished").is(":checked") ? "1" : "0";
    var furnished = "1";
    //var washer = $("#chkWasher").is(":checked") ? "1" : "0";
    var washer = "0";
    //var refrigerator = $("#chkRefrigerator").is(":checked") ? "1" : "0";
    var refrigerator = "0";
    //var drapes = $("#chkTakeOffList").is(":checked") ? "1" : "0";
    var drapes =  "0";
    //var dryer = $("#chkDryer").is(":checked") ? "1" : "0";
    var dryer ="0";
    //var dishwasher = $("#chkDishwasher").is(":checked") ? "1" : "0";
    var dishwasher =  "0";
    //var disposal = $("#chkDisposal").is(":checked") ? "1" : "0";
    var disposal =  "0";
    //var elec_Range = $("#chkElec_Range").is(":checked") ? "1" : "0";
    var elec_Range =  "0";
    //var Gas_Range = $("#chkGas_Range").is(":checked") ? "1" : "0";
    var Gas_Range = "0";
    //var carpet = $("#chkCarpet").is(":checked") ? "1" : "0";
    var carpet =  "0";
    //var air_Conditioning = $("#chkAir_Conditioning").is(":checked") ? "1" : "0";
    var air_Conditioning =  "0";
    //var fireplace = $("#chkFireplace").is(":checked") ? "1" : "0";
    var fireplace = "0";
    //var den = $("#chkDen").is(":checked") ? "1" : "0";
    var den = "0";
    var availableDate = $("#txtAvailableDate").val();
    var occupancyDate = $("#txtOccupancyDate").val();
    //var pendingMoveIn = $("#chkPendingMoveIn").is(":checked") ? "1" : "0";
    var pendingMoveIn =  "0";
   
    var intendedMoveIn_Date = $("#txtIntendedMoveIn_Date").val();
    var intendMoveOutDate = $("#txtIntendMoveOutDate").val();

    var actualMoveInDate = $("#txtActualMoveInDate").val();
    var actualMoveOutDate = $("#txtActualMoveOutDate").val();
    var leaseendDate = $("#txtLeaseEndDate").val();
    var intarea = $("#txtInteriorArea").val();
    var balcarea = $("#txtBalconyArea").val();
    var notes = $("#txtUnitNotes").val();

    if (unitNo == "") {
        msg += " Please enter Unit Title .<br />";
    }

    if (bedroom == "") {
        msg += " Please enter No. of bedroom .<br />";
    }
    if (bathroom == "") {
        msg += " Please enter No. of bathroom .<br />";
    }
    //if (hall == "") {
    //    msg += " Please enter No. of hall .<br />";
    //}
    if (deposit == "0") {
        msg += " Please enter deposit .<br />";
    }
    if (current_Rent == "0") {
        msg += " Please enter current_Rent .<br />";
    }

    if (floorNo == "0") {
        msg += " Please select floorNo .<br />";
    }

    if (area == "") {
        msg += " Please enter area .<br />";
    }

    if (floorPlanCord == "") {
        msg += " Please enter Floor Coordinates .<br />";
    }
    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    $formData = new FormData();

    $formData.append('PID', pID);
    $formData.append('UID', uID);
    $formData.append('UnitNo', unitNo);
    $formData.append('Rooms', rooms);
    $formData.append('Bedroom', bedroom);
    $formData.append('Bathroom', bathroom);
    $formData.append('Hall', hall);
    $formData.append('Deposit', deposit);
    $formData.append('Current_Rent', current_Rent);
    $formData.append('Previous_Rent', previous_Rent);
    $formData.append('Market_Rent', market_Rent);
    $formData.append('Wing', 'A');
    $formData.append('Building', building);
    $formData.append('Leased', leased);
    $formData.append('PetDetails', petDetails);
    $formData.append('FloorNo', floorNo);
    $formData.append('Area', area);
    $formData.append('Carpet_Color', carpet_Color);
    $formData.append('Wall_Paint_Color', wall_Paint_Color);
    $formData.append('Furnished', furnished);
    $formData.append('Washer', washer);
    $formData.append('Refrigerator', refrigerator);
    $formData.append('Drapes', drapes);
    $formData.append('Dryer', dryer);
    $formData.append('Dishwasher', dishwasher);
    $formData.append('Disposal', disposal);
    $formData.append('Elec_Range', elec_Range);
    $formData.append('Gas_Range', Gas_Range);
    $formData.append('Carpet', carpet);
    $formData.append('Air_Conditioning', air_Conditioning);
    $formData.append('Fireplace', fireplace);
    $formData.append('Den', den);
    $formData.append('AvailableDate', availableDate);
    $formData.append('OccupancyDate', occupancyDate);
    $formData.append('PendingMoveIn', pendingMoveIn);
    $formData.append('VacancyLoss_Date', leaseendDate);
    $formData.append('IntendedMoveIn_Date', intendedMoveIn_Date);
    $formData.append('IntendMoveOutDate', intendMoveOutDate);
    $formData.append('ActualMoveInDate', actualMoveInDate);
    $formData.append('ActualMoveOutDate', actualMoveOutDate);
    $formData.append('Coordinates', floorPlanCord);
    $formData.append('Premium', premium);
    $formData.append('InteriorArea', intarea);
    $formData.append('BalconyArea', balcarea);
    $formData.append('Notes', notes);

    var $file = document.getElementById('unit-picture');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }

    $.ajax({
        url: "/Admin/PropertyManagement/SaveUpdatePropertyUnit/",
        type: "post",
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.Msg > 0) {
                $.alert({
                    title: 'Message!',
                    content: "Property Unit Saved Succefully",
                    type: 'blue',
                });
                $("#hndUID").val(response.Msg)
            }
            else {
                $.alert({
                    title: 'Message!',
                    content: "Property Unit Updated Succefully",
                    type: 'blue',
                });
            }
            
        }
    });

}
function getFloorList() {
    var pid = $("#hndPID").val();
    var model = { PID: pid };
    $.ajax({
        url: '/Admin/PropertyManagement/GetFloorList',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
               

                $("#ddlFloorNo").empty();
                $("#ddlFloorNo").append("<option value='0'>Select Floor</option>");
                $.each(response, function (elementType, elementValue) {
                    $("#ddlFloorNo").append("<option value=" + elementValue.FloorID + ">" + elementValue.FloorNo + "</option>");
                });
            }
        }
    });
}

var getUnitCord = function (e, cont) {
    var offset = $(cont).offset();
    var X = (e.pageX - offset.left);
    var Y = (e.pageY - offset.top);
    $('#txtunitcord').append(X + ',' + Y + ',');

}
function showFloorPlan(flid) {
    var model = { FloorID: flid };
    $.ajax({
        url: "/Admin/PropertyManagement/GetPropertyFloorDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            // $("#unit_img").attr("src", "/content/assets/img/plan/" + response.model.FloorPlan + "");
            $("#popUnitPlan").empty();
            //$("#lblPropFloor").text("Selected : Floor" + response.model.FloorNo);
            var html = "<h2>Selected : Floor" + $("#ddlFloorNo").val() + "</h2><hr style='border:medium'/>";
            html += "<img src ='/content/assets/img/plan/" + response.model.FloorPlan + "' id='imgFloorCoordinate' class='img-responsive' usemap='#unitimg' onclick='getUnitCord(event,this)'>";
            html += "<map name='unitimg' id='imgFloorCoordinateDiv'>";
            $.each(response.model.lstPropertyUnit, function (elementType, value) {
                html += "<area shape='poly' id='ua" + value.UID + "' class='tooltip' title='" + value.UnitNo + "'  coords='" + value.Coordinates + "'>";
                html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
            });
            html += "</map>";
            $("#popUnitPlan").append(html);
            $("#imgFloorCoordinate").maphilight();

            $.each(response.model.lstPropertyUnit, function (elementType, value) {
                if ($("#hndUID").val() == value.UID) {
                    console.log("dcod:" + value.Coordinates);
                    console.log("did:" + value.UID);
                    var cont = $("#hndUID").val();
                    $("#ua" + cont).addClass('active_area').data('maphilight', { alwaysOn: true, fillOpacity: 1, fillColor: 'f62030', strokeColor: '6f0e0e', }).trigger('alwaysOn.maphilight');
                } else {
                    if (value.Coordinates != "") {
                        console.log("in");
                        $("#ua" + value.UID).addClass('active_mouse').data('maphilight', { alwaysOn: true, fillColor: 'FF6347', strokeColor: 'FF6347', }).trigger('alwaysOn.maphilight');
                    }
                }
            });

            $("area").mouseover(function () {
                $(this).addClass('active_mouse').data('maphilight', { alwaysOn: true, fillColor: 'FF6347', strokeColor: 'FF6347', }).trigger('alwaysOn.maphilight');
                if ($("#hndUID").val()) {
                    var cont = $("#hndUID").val();
                    $("#ua" + cont).addClass('active_area').data('maphilight', { alwaysOn: true, fillOpacity:1, fillColor: 'f62030', strokeColor: '6f0e0e', }).trigger('alwaysOn.maphilight');
                }
            });
            
            //$('#imgFloorCoordinate').click(function (e) {

            //    var offset = $(this).offset();
            //    var X = (e.pageX - offset.left);
            //    var Y = (e.pageY - offset.top);

            //    $('#txtunitcord').append(X + ',' + Y + ',');
            //});
        }
    });

}

var clearUnit = function () {

    $("#hndUID").val(0);
    $("#txtPropUnit").val("");
    $("#txtRooms").val("");
    $("#txtBedroom").val("");
    $("#txtBathroom").val("");
    $("#txtHall").val("");
    $("#txtDeposit").val("");
    $("#txtCurrentRent").val("");
    $("#txtPreviousRent").val("");
    $("#txtMarketRent").val("");
   // $("#txtWing").val("");
    $("#txtBuilding").val("");
    $("#txtLeased").val("");
    $("#txtPetDetails").val("");
    $("#ddlFloorNo").val(0);
    $("#txtArea").val("");
    $("#txtunitcord").val("");
    $("#txtCarpet_Color").val("");
    $("#txtWall_Paint_Color").val("");
    $("#chkFurnished").removeAttr("checked");
    $("#chkWasher").removeAttr("checked");
    $("#chkRefrigerator").removeAttr("checked");
    $("#chkTakeOffList").removeAttr("checked");
    $("#chkDryer").removeAttr("checked");
    $("#chkDishwasher").removeAttr("checked");
    $("#chkDisposal").removeAttr("checked");
    $("#chkElec_Range").removeAttr("checked");
    $("#chkGas_Range").removeAttr("checked");
    $("#chkCarpet").removeAttr("checked");
    $("#chkAir_Conditioning").removeAttr("checked");
    $("#chkFireplace").removeAttr("checked");
    $("#chkDen").removeAttr("checked");
    $("#txtAvailableDate").val("");
    $("#txtOccupancyDate").val("");
    $("#chkPendingMoveIn").removeAttr("checked");
    $("#txtVacancyLoss_Date").val("");
    $("#txtIntendedMoveIn_Date").val("");
    $("#txtIntendMoveOutDate").val("");
    $("#txtActualMoveInDate").val("");
    $("#IntendMoveOutDate").val("");

    $("#txtMinfRent").val("");
    $("#txtMaxfRent").val("");

    $("#txtInteriorArea").val("");
    $("#txtBalconyArea").val("");
    $("#txtActualMoveOutDate").val("");
    $("#txtLeaseEndDate").val("");
    $("#txtUnitNotes").val("");
    $("#ddlModel").val(0);
    $("#ddlModel").trigger('change');
}
var fillModelDDL = function () {

    $.ajax({
        url: '/Models/getModelsList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#ddlModel").empty();
                $("#ddlModel").append("<option value='0'>Select Model</option>");
                $.each(response.model, function (index, elementValue) {
                    $("#ddlModel").append("<option value=" + elementValue.ModelName + ">" + elementValue.ModelName + "</option>");
                });
            }
        }
    });
}
var getPropertyModelDetails = function (modelname) {

    var model = { ModelName: modelname };
    $.ajax({
        url: "/Models/GetPropertyModelDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#modelPicturePreview").attr("src", "/content/assets/img/plan/" + modelname + ".jpg");
            //$("#unit-picture").val(modelname);
            $("#txtArea").val(response.model.Area);
            $("#txtBalconyArea").val(response.model.BalconyArea);
            $("#txtInteriorArea").val(response.model.InteriorArea);
            $("#txtBedroom").val(response.model.Bedroom);
            $("#txtBathroom").val(response.model.Bathroom);

            $("#txtMinfRent").val(formatMoney(parseFloat(response.model.MinRent).toFixed(2)));
            $("#txtMaxfRent").val(formatMoney(parseFloat(response.model.MaxRent).toFixed(2)));
            $("#txtDeposit").val(formatMoney(parseFloat(response.model.Deposit).toFixed(2)));
}
    })
}
var delModel= function (mId) {

    var model = {
        ModelID: mId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Model?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Admin/PropertyManagement/DeleteModel",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            $('#trm_' + mId).remove();
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
var delUnit = function (uId) {

    var model = {
        UID: uId
    };

    $.alert({
        title: "",
        content: "Are you sure to remove Unit?",
        type: 'blue',
        buttons: {
            yes: {
                text: 'Yes',
                action: function (yes) {
                    $.ajax({
                        url: "/Admin/PropertyManagement/DeleteUnit",
                        type: "post",
                        contentType: "application/json utf-8",
                        data: JSON.stringify(model),
                        dataType: "JSON",
                        success: function (response) {
                            if (response.model =="Unit Unable to remove")
                            {
                                $.alert({
                                    title: 'Message!',
                                    content: "Unit Unable to remove",
                                    type: 'blue',
                                });
                            } else {
                                $('#tru_' + uId).remove();
                                $.alert({
                                    title: 'Message!',
                                    content: "Unit Removed Successfully",
                                    type: 'blue',
                                });
                            }
                           
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

function formatPhoneFax(phonefax) {
    if (phonefax == null)
        phonefax = "";
    phonefax = phonefax.replace(/[^0-9]/g, '');
    if (phonefax.length == 0)
        return phonefax;

    return '(' + phonefax.substring(0, 3) + ') ' + phonefax.substring(3, 6) + (phonefax.length > 6 ? '-' : '') + phonefax.substring(6);
}

function unformatText(text) {
    if (text == null)
        text = "";

    return text.replace(/[^\d\.]/g, '');
}

function formatMoney(number) {
    number = number || 0;
    var places = 2;
    var symbol = "";
    var thousand = ",";
    var decimal = ".";
    var negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
}

var onFocus = function () {

    $("#txtApplicationFees").focusout(function () { $("#txtApplicationFees").val(formatMoney($("#txtApplicationFees").val())); })
        .focus(function () {
            $("#txtApplicationFees").val(unformatText($("#txtApplicationFees").val()));
        });

    $("#txtAdminFees").focusout(function () { $("#txtAdminFees").val(formatMoney($("#txtAdminFees").val())); })
        .focus(function () {
            $("#txtAdminFees").val(unformatText($("#txtAdminFees").val()));
        });

    $("#txtGuarantorFees").focusout(function () { $("#txtGuarantorFees").val(formatMoney($("#txtGuarantorFees").val())); })
        .focus(function () {
            $("#txtGuarantorFees").val(unformatText($("#txtGuarantorFees").val()));
        });

    $("#txtPestControlFees").focusout(function () { $("#txtPestControlFees").val(formatMoney($("#txtPestControlFees").val())); })
        .focus(function () {
            $("#txtPestControlFees").val(unformatText($("#txtPestControlFees").val()));
        });

    $("#txtTrashFees").focusout(function () { $("#txtTrashFees").val(formatMoney($("#txtTrashFees").val())); })
        .focus(function () {
            $("#txtTrashFees").val(unformatText($("#txtTrashFees").val()));
        });

    $("#txtConversionBill").focusout(function () { $("#txtConversionBill").val(formatMoney($("#txtConversionBill").val())); })
        .focus(function () {
            $("#txtConversionBill").val(unformatText($("#txtConversionBill").val()));
        });
}
var editUnitDate = function (uid) {

    $("#avUnitDate_" + uid).removeAttr("onclick");
    var unitDate = $("#avUnitDate_" + uid).attr("data-udate");
    $("#avUnitDate_" + uid).empty();

    $("#avUnitDate_" + uid).append("<input class='form-control'  style='width:100px' type='text' id='editUDate_" + uid + "'  value='" + unitDate + "'/>");
    $("#editUDate_" + uid).datepicker({
        autoclose: true
    }).on('changeDate', function (e) {
        var availDate = $("#editUDate_" + uid).val();
        // alert(availDate);
        var pID = $("#hndPID").val();

        var model = {
            PID: pID,
            UID: uid,
            AvailableDate: availDate
        };

        $.ajax({
            url: "/Admin/PropertyManagement/UpdateAvailDate/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#avUnitDate_" + uid).empty().append(availDate + " <i class='fa fa-calendar pull-right' style='margin: 6px;'></i>");
                $("#avUnitDate_" + uid).attr("data-udate", availDate);
                $("#avUnitDate_" + uid).attr("onclick", "editUnitDate(" + uid + ")");
            }
        });
    });
    $("#editUDate_" + uid).focus();
};

var editUnitMoveInDate = function (uid) {

    $("#avUnitMoveInDate_" + uid).removeAttr("onclick");
    var unitDate = $("#avUnitMoveInDate_" + uid).attr("data-udate");
    $("#avUnitMoveInDate_" + uid).empty();

    $("#avUnitMoveInDate_" + uid).append("<input class='form-control' style='width:100px'  type='text' id='editUMoveInDate_" + uid + "'  value='" + unitDate + "'/>");
    $("#editUMoveInDate_" + uid).datepicker({
        autoclose: true
    }).on('changeDate', function (e) {
        var availDate = $("#editUMoveInDate_" + uid).val();
        // alert(availDate);
        var pID = $("#hndPID").val();

        var model = {
            PID: pID,
            UID: uid,
            ActualMoveInDate: availDate
        };

        $.ajax({
            url: "/Admin/PropertyManagement/UpdateMoveInDate/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#avUnitMoveInDate_" + uid).empty().append(availDate + " <i class='fa fa-calendar pull-right' style='margin: 6px;'></i>");
                $("#avUnitMoveInDate_" + uid).attr("data-udate", availDate);
                $("#avUnitMoveInDate_" + uid).attr("onclick", "editUnitMoveInDate(" + uid + ")");
            }
        });
    });
    $("#editUMoveInDate_" + uid).focus();
};
var editUnitMoveOutDate = function (uid) {

    $("#avUnitMoveOutDate_" + uid).removeAttr("onclick");
    var unitDate = $("#avUnitMoveOutDate_" + uid).attr("data-udate");
    $("#avUnitMoveOutDate_" + uid).empty();

    $("#avUnitMoveOutDate_" + uid).append("<input class='form-control' style='width:100px'  type='text' id='editUMoveOutDate_" + uid + "'  value='" + unitDate + "'/>");
    $("#editUMoveOutDate_" + uid).datepicker({
        autoclose: true
    }).on('changeDate', function (e) {
        var availDate = $("#editUMoveOutDate_" + uid).val();
        // alert(availDate);
        var pID = $("#hndPID").val();

        var model = {
            PID: pID,
            UID: uid,
            ActualMoveOutDate: availDate
        };

        $.ajax({
            url: "/Admin/PropertyManagement/UpdateMoveOutDate/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#avUnitMoveOutDate_" + uid).empty().append(availDate + " <i class='fa fa-calendar pull-right' style='margin: 6px;'></i>");
                $("#avUnitMoveOutDate_" + uid).attr("data-udate", availDate);
                $("#avUnitMoveOutDate_" + uid).attr("onclick", "editUnitMoveOutDate(" + uid + ")");
            }
        });
    });
    $("#editUMoveOutDate_" + uid).focus();
};
var editUnitRent = function (uid) {

    $("#avUnitRent_" + uid).removeAttr("onclick");
    var unitDate = $("#avUnitRent_" + uid).attr("data-udate");
    $("#avUnitRent_" + uid).empty();

    $("#avUnitRent_" + uid).append("<input class='form-control' type='text' id='editURent_" + uid + "'  value='" + unitDate + "'/>");
    $("#editURent_" + uid).focusout(function (e) {
        var availDate = $("#editURent_" + uid).val();
        // alert(availDate);
        var pID = $("#hndPID").val();

        var model = {
            PID: pID,
            UID: uid,
            Current_Rent: availDate
        };

        $.ajax({
            url: "/Admin/PropertyManagement/UpdateRent/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#avUnitRent_" + uid).empty().append(availDate + " <i class='fa fa-edit pull-right' style='margin: 6px;'></i>");
                $("#avUnitRent_" + uid).attr("data-udate", availDate);
                $("#avUnitRent_" + uid).attr("onclick", "editUnitRent(" + uid + ")");
            }
        });
    });
    $("#editURent_" + uid).focus();
};
var editUnitNotes = function (uid) {   
    $("#avUnitNotes_" + uid).removeAttr("onclick");
    var notes = $("#avUnitNotes_" + uid).attr("data-udate");
    $("#txtNotes").val(notes);
    //$("#ModelNotes").modal("show");
    $("#avUnitNotes_" + uid).empty();

    $("#avUnitNotes_" + uid).append("<input class='form-control' type='text' id='editUNotes_" + uid + "'  value='" + notes + "'/>");
    $("#editUNotes_" + uid).focusout(function (e) {
        var editnotes = $("#editUNotes_" + uid).val();
        // alert(editnotes);
        var pID = $("#hndPID").val();

        var model = {
            PID: pID,
            UID: uid,
            Notes: editnotes
        };

        $.ajax({
            url: "/Admin/PropertyManagement/UpdateUnitNotes/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                $("#avUnitNotes_" + uid).empty().append(editnotes + " <i class='fa fa-edit pull-right' style='margin: 6px;'></i>");
                $("#avUnitNotes_" + uid).attr("data-udate", editnotes);
                $("#avUnitNotes_" + uid).attr("onclick", "editUnitNotes(" + uid + ")");
            }
        });
    });
    $("#editUNotes_" + uid).focus();
};
function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("tblPUList");
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}
var areaAddition = function () {
    var balcony = $('#txtBalconyArea').val();
    var interior = $('#txtInteriorArea').val();
    var area = $('#txtArea').val();
    if ($('#txtBalconyArea').val().trim() == '') {
        $('#txtBalconyArea').val('0');
    }
    if ($('#txtInteriorArea').val().trim() == '') {
        $('#txtInteriorArea').val('0');
    }
    var add = parseFloat($('#txtBalconyArea').val()) + parseFloat($('#txtInteriorArea').val());
    $('#txtArea').val(add);

}
var count = 0;
var sortTable = function (sortby) {
    var orderby = "";
    var pNumber = $("#hndPageNo").val();
    if (!pNumber) {
        pNumber = 1;
    }

    if (count % 2 == 1) {
        orderby = "ASC";
    }
    else {
        orderby = "DESC";
    }
    localStorage.setItem("SortByValue", sortby);
    localStorage.setItem("OrderByValue", orderby);
    count++;
    buildPaganationPUList(pNumber, sortby, orderby);
    fillPUList(pNumber, sortby, orderby);
};