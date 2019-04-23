// Scripts to be executed on the onload page
$(document).ready(function () {
    // Disable sidebar when size is Extra Small (xs)
    //if ($(window).width() < 450) {
    //    $('#accordionSidebar').addClass('toggled');
    //} 

    // Changing active option from sibe bar menu
    var pageName = window.location.pathname.split("/");
    var newPageName = pageName[2];
    if (newPageName) {
        $.each($('#accordionSidebar').find('li'), function () {
            var hrefVal = $(this).find('a').attr('href');
            if (hrefVal.indexOf(newPageName) >= 0) {
                $(this).addClass('active').siblings().removeClass('active');
            }
        });
    }
});