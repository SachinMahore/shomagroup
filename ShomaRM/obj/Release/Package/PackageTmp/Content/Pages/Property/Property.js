$(document).ready(function () {
    $("#returnButton").addClass("hidden");
    $("#popUnitDet").removeClass("hidden");
    $("#popPromotion").removeClass("hidden");

    fillStateDDL1();
    $("#ddlState1").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillCityList1(selected);
        }
    });
 
    $("#popFloorPlan").PopupWindow({
        title: "Unit Plan",
        modal: false,
        autoOpen: false,
        top: 120,
        left: 400,
        height: 500,
        
    });

    getPropertyList();

  
    getFloorList();
    getPromotionListDetails();

     //$('.tooltip, .UAarea, .UYarea, .Uarea').tooltipster({
    //    theme: ['tooltipster-noir', 'tooltipster-noir-customized']
    //});

});

var getPropertyList = function () {
    var city = 0;
    if ($("#ddlCity1").val() !=null) {
        city = $("#ddlCity1").val();
    }
   
    var searchText = $("#txtSearch").val();
    var model = { City: city, SearchText: searchText };

    $.ajax({
        url: "/Property/GetPropertyList/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
                $("#list-type").empty();
               
                $.each(response.model, function (elementType, value) {
                    var html = "<div class='col-sm-6 col-md-4 p0'><div class='box-two proerty-item'>";
                    html += "<div class='item-thumb'><a href = '#' onclick='getPropertyDetails(" + value.PID +")'> <img src='../content/assets/img/demo/" + value.Picture+"'></a></div> ";
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
    window.location = "/Property/GetPropertyDetails/" + pid;
}
var getPropertyUnitList = function () {
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        //maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }
   
    var build = $("#hndbuilding").val();
    if (build == 1)
    {
        getPropertyFloorList();
    
    } else {
      
        var pid = $("#hndPID").val();
        var bedroom = 0;
        if (!$("#ddlRoom").val()) {
            bedroom = 0;
        }
        else {
            bedroom = $("#ddlRoom").val();
        }
        var availdate = $("#txtDate").val();
        
      
        var model = { PID: pid, AvailableDate: availdate, Current_Rent: maxrent, Bedroom: bedroom };
        $.ajax({
            url: "/Property/GetPropertyUnitList/",
            type: "post",
            contentType: "application/json utf-8",
            data: JSON.stringify(model),
            dataType: "JSON",
            success: function (response) {
                if (response != null) {
                    $("#listUnit").empty();
                    $.each(response.model, function (elementType, value) {
                        var html = "<div class='col-sm-12 col-md-12 p0'><div class='box-two proerty-item'>";
                        html += " <div class='item-entry overflow' id='unitdiv" + value.UID + "'><a href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'><h5>Floor" + value.FloorNoText + "-" + value.UnitNo + "<span class='proerty-price pull-right'> $ " + value.Current_Rent + "</span></h5> </a> <div class='dot-hr'></div>";
                        html += "<div class='property-icon'> <img src='/content/assets/img/icon/bed.png'>(" + value.Bedroom + ")&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<img src ='/content/assets/img/icon/shawer.png'>(" + value.Bathroom + ")&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;";
                        html += "<img src ='/content/assets/img/icon/room.png'>(" + value.Area + "Sq Ft)";
                        html += "<span class='proerty-price pull-right'>Available " + value.AvailableDateText + "</span>";
                        html += "</div></div></div></div>";
                        $("#listUnit").append(html);
                    });
                }
            }
        });
    }
   
}
var getPropertyUnitDetails = function (uid) {
    setTimeout(function () { $("#returnButton").removeClass("hidden"); }, 1000);
    var model = { UID: uid };
    $.ajax({
        url: "/Property/GetPropertyUnitDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#unitdiv" + $("#hndUID").val()).css("border", "");
            $("#unitdiv" + uid).css("border", "5px solid #4d738a");

            $("#popUnitDet").addClass("hidden");
            $("#popPromotion").addClass("hidden");
            $("#popFloorCoordinate").addClass("hidden");
            $("#divUdet").removeClass("hidden");
            $("#lblUnitNo").text("Floor" + response.model.FloorNo+"- Unit "+response.model.UnitNo);
            $("#lblRent").text("$" + response.model.Current_Rent);
            $("#lblArea").text(response.model.Area);
            $("#lblBed").text( response.model.Bedroom);
            $("#lblBath").text(response.model.Bathroom);
           // $("#lblHall").text( response.model.Hall);
            $("#lblDeposit").text("$" + response.model.Deposit);
            $("#lblLease").text( response.model.Leased);
           // $("#lblDeposit").text(response.model.Rent);
            $("#imgFloorPlan").attr("src", "/content/assets/img/plan/" + response.model.FloorPlan + "");
            $("#imgFloorPlan2").attr("src", "/content/assets/img/plan/" + response.model.FloorPlan + "");
            //$("#lblUnitNo1").text("Selected : Floor" + response.model.FloorNo + "- Unit " + response.model.UnitNo + "  ($" + response.model.Current_Rent + ")");
            $("#hndUID").val(uid);
            $("#UnitListDesc").text("Once you have choosen your new apartment, select Apply Now and we will walk you through a seamless and effortless process into your new home");
        }
    })
}
function displayImg() {    
    $("#popFloorPlan").PopupWindow("open");
}
function showFloor() {
    //getFloorList();
    $("#listUnitDiv").toggleClass();
    if ($("#btnShowfloor").text() == "Show Building") {
        $("#btnShowfloor").text("Show List")
        $("#divFloor").removeClass("hidden");
        $("#listUnitDiv").addClass("hidden");
        $("#returnButton").html("Return to List View");
        $("#UnitListDesc").text("Choose any unit in green to see more information including a video and complete layout of your unit.");
        $("#hndbuilding").val(1);
       
    }
    else {
        $("#btnShowfloor").text("Show Building");
        $("#divFloor").addClass("hidden");
        $("#listUnitDiv").removeClass("hidden");
        $("#returnButton").html("Return to Building View");
        $("#hndbuilding").val(0);
        getPropertyUnitList();
       
        $("#UnitListDesc").text("Click on the unit of your choice to view the information, layout, and interactive video");
    }
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
                    console.log(response)
                    $("#ddlCity1").append("<option value=" + elementValue.ID + ">" + elementValue.CityName + "</option>");
                });
            }
        }
    });
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
                    html += "<div class='col-sm-4'><div class='form-group'><div class='checkbox' style='position: unset'><label><input type='checkbox' id='chkAmenity" + elementValue.ID + "' value=" + elementValue.ID + " style='margin-top: 7px' class='addame' onclick='selectAddAmenity(" + elementValue.ID + ")'>";
                    html += "" + elementValue.Amenity + " </label></div ></div ></div >";
                    $("#tblAminities").append(html);

                });

            }

        }
    });
}
var getFloorList = function () { 
    var bedroom = 0;
    if (!$("#ddlRoom").val()) {
        bedroom = 0;
    }
    else {
        bedroom = $("#ddlRoom").val();
    }
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }
    var availdate = $("#txtDate").val();
    var model = { PID: $("#hndPID").val(), AvailableDate: availdate, Bedroom: bedroom, MaxRent: maxrent  };
    $.ajax({
        url: '/Property/GetFloorList/',
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            if ($.trim(response.error) != "") {
               // this.cancelChanges();
            } else {
          
                $("#ddlFloorNo").empty();
                $("#ddlFloorNo").append("<option value='0' selected>Select Floor</option>");
                $.each(response, function (index, elementValue) {
                    var html = '';                 
                    $("#ddlFloorNo").append("<option value=" + elementValue.FloorID + ">" + elementValue.FloorID + "</option>");
                });
                
                
            }

        }
    });
}

function showFloorPlan(flid) {
    setTimeout(function () { $("#returnButton").removeClass("hidden"); $("#returnButton").html("Return to List View"); }, 1000);
    $("#UnitListDesc").text("Choose any unit in green to see more information including a video and complete layout of your unit. ");
    var bedroom = 0;
    if (!$("#ddlRoom").val()) {
        bedroom = 0;
    }
    else {
        bedroom = $("#ddlRoom").val();
    }
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }
    var availdate = $("#txtDate").val();
    var model = { FloorID: flid, AvailableDate: availdate, Bedroom: bedroom, MaxRent: maxrent };
    $.ajax({
        url: "/Property/GetPropertyFloorDetails/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $("#popUnitPlan").empty();
            $("#popFloorCoordinate").removeClass("hidden");
            $("#popUnitDet").addClass("hidden");
            $("#popPromotion").addClass("hidden");
            $("#divUdet").addClass("hidden");
            //$("#lblPropFloor").text("Selected : Floor" + response.model.FloorNo);

            var html = "<h3 style='color: #4d738a; text-align:center;'>Selected : Floor " + response.model.FloorNo + "</h3>";
            html += "<div class='col-sm-12' style='background:#fff;text-align:center!important;'><span style='background-color:#006400;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Available</span>&nbsp;&nbsp;<span style='background-color:#ffff00;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Other Options</span>&nbsp;&nbsp;<span style='background-color:#FF0000;width:10px;height:10px'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='color:#4d738a;'>&nbsp;&nbsp;Unavailable</span></div><br/><br/>";
            //<div class='col-sm-4'><div style='background-color: #fa6500; width: 10px; height: 10px'></div><span>Not Available</span></div> <div class='col-sm-4'><div style='background-color:red;width:10px;height:10px'></div > <span>Available</span></div>
            html += "<img src ='/content/assets/img/plan/" + response.model.FloorPlan + "' id='imgFloorCoordinate' class='' usemap='#unitimg'>";
            html += "<map name='unitimg' id='imgFloorCoordinateDiv'>";
            $.each(response.model.lstUnitFloor, function (elementType, value) {
                if (value.IsAvail == 1) {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UAarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'>";
                    //html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Yes</span></div>";
                } else if (value.IsAvail == 2) {

                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips UYarea' coords='" + value.Coordinates + "' href='javascript:void(0);' onclick='getPropertyUnitDetails(" + value.UID + ")'>";
                    // html += "<span class='tooltip-text' style='display: none'>" + value.UnitNo + "</span>";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: Other</span></div>";
                } else {
                    html += "<area shape='poly' id='unId_" + value.UID + "' class='tooltips Uarea' coords='" + value.Coordinates + "' >";
                    html += "<div id='floorunId_" + value.UID + "' class='hidden divtooltipUnit'><span>Unit No: " + value.UnitNo + "<br/>" + value.Bedroom + " bd / " + value.Bathroom + " ba</span><span> Available: No</span></div>";

                }
            });
            html += "</map><br/>";
            $("#popUnitPlan").append(html);
            $("#imgFloorCoordinate").maphilight();

            //$('.active_area').data('maphilight', { alwaysOn: false }).trigger('alwaysOn.maphilight');
            $('.Uarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'FF0000', strokeColor: 'FF0000', }).trigger('alwaysOn.maphilight');
            $('.UAarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: '006400', strokeColor: '006400', }).trigger('alwaysOn.maphilight');
            $('.UYarea').addClass('active_unavil').data('maphilight', { alwaysOn: true, fillColor: 'ffff00', strokeColor: 'ffff00', }).trigger('alwaysOn.maphilight');

            $(".tooltips").mouseout(function () { $(".divtooltipUnit").addClass("hidden"); });
            $(".tooltips").mouseover(function (e) {
                var offset = $(this).offset();
                var X = (e.pageX - offset.left);
                var Y = (e.pageY - offset.top);
                X = X - 30;
                Y = Y + 100;
                var thisId = $(this).attr('id');
                var divID = thisId.split("_");
                $(".divtooltipUnit").addClass("hidden");
                $("#floorunId_" + divID[1]).removeClass("hidden");
                $("#floorunId_" + divID[1]).css({ top: Y, left: X, position: 'absolute' });

            });




        }
    });

}
function applyNow() {
    var unit = $("#hndUID").val();
    var moveInDate = $("#txtDate").val();
    var model = { UnitID: unit, MoveInDate: moveInDate };
    $.ajax({
        url: "/Property/SetSelectedUnitId/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            window.location.href = "/ApplyNow/Index/0";
        }
    });
   
}

var getPromotionListDetails = function () {

    var currentdate = new Date();
    var datetime = currentdate.getDate() + "/"
        + (currentdate.getMonth() + 1) + "/"
        + currentdate.getFullYear() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();

    var model = {
        FromDate: datetime,
        ToDate: datetime
    };
    $.ajax({
        url: "../../Admin/Promotion/GetPromotionListDetails",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            $("#promoList").empty();
            $.each(response.model, function (elementType, elementValue) {
                var html = "<ul class='list-unstyled' style='padding: 5px;'>";
                html += "<li >" + elementValue.PromotionTitle + "</li>";
                html += "</ul>";
                $("#promoList").append(html);

            });

        }
    });
}
var getPropertyFloorList = function () {
    $("#divLoader").show();
    
    var bed = $("#ddlRoom").val();
    var midate = $("#txtDate").val();
    var maxrent = 0;
    if ($("#txtMaxRent").val() == "0") {
        maxrent = "10000";
    }
    else {
        maxrent = $("#txtMaxRent").val();
    }
    var model = { Bedroom: bed, MoveInDate: midate, MaxRent: maxrent };

    $.ajax({
        url: "/Property/SetSearchFromHome/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {

            window.location.href = "/Property/GetPropertyDetails/8";
            $("#divLoader").hide();
        }
    });

}