//// Scripts to be executed on the onload page
//$(document).ready(function () {
//    // Disable sidebar when size is Extra Small (xs)
//    //if ($(window).width() < 450) {
//    //    $('#accordionSidebar').addClass('toggled');
//    //} 

//    // Changing active option from sibe bar menu
//    var pageName = window.location.pathname.split("/");
//    var newPageName = pageName[2];
//    if (newPageName) {
//        $.each($('#accordionSidebar').find('li'), function () {
//            var hrefVal = $(this).find('a').attr('href');
//            if (hrefVal.indexOf(newPageName) >= 0) {
//                $(this).addClass('active').siblings().removeClass('active');
//            }
//        });
//    }
//});

function DisplayValidationErrors(errors) {
    $.each(errors, function (idx, validationError) {
        $("span[data-valmsg-for='" + validationError.PropertyName + "']").text(validationError.ErrorMessage);
    });
}


$('#navSideBar .nav-menu a').on('click', function () {
    $('#navSideBar .nav-menu').find('li.active').removeClass('active');
    $(this).parent('li').addClass('active');
});

// Show or hide section panel
function ShowOrHideSection(element) {
    $("section").each(function () {
        if ($(this).attr('id') != $(element).attr('id')) {
            $(this).hide();
        };
    });
    $(element).show();
}