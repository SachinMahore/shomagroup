$(document).ready(function () {
    $('#image-gallery').lightSlider({
        gallery: true,
        item: 1,
        thumbItem: 9,
        slideMargin: 0,
        speed: 500,
        auto: true,
        loop: true,
        onSliderLoad: function () {
            $('#image-gallery').removeClass('cS-hidden');
        }
    });

});



var popGallary=function(pid) {
    var model = { PID: pid };
    $.ajax({
        url: "/Property/GetPropertyGallary/",
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {           
            //$("#lblUnitNo").text("Floor" + response.model.FloorNo + "- Unit " + response.model.UnitNo);         
            //$("#imgFloorPlan").attr("src", "/content/assets/img/property-" + response.model.PID + "/" + response.model.FloorPlan + "");
          
            $("#image-gallery").empty();
        
            $.each(response.model, function (elementType, value) { 
                var ulhtml = "<li data-thumb='../content/assets/img/property-" + value.PID + "/" + value.PhotoPath + "'><img src='/content/assets/img/property-" + value.PID + "/" + value.PhotoPath + "'/></li>";
                $("#image-gallery").append(ulhtml);
            });
            $('#image-gallery').lightSlider({
                gallery: true,
                item: 1,
                thumbItem: 9,
                slideMargin: 0,
                speed: 500,
                auto: true,
                loop: true,
                onSliderLoad: function () {
                    $('#image-gallery').removeClass('cS-hidden');
                }
            });
           
        }
    })
  
}