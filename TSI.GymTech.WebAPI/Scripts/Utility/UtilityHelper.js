// Change nav-item active 
//$(document).ready(function () {
//    var pageName = window.location.pathname;
//    var newPageName = pageName;
//    if (pageName.indexOf('/') == 0) {
//        newPageName = pageName.substring(1, pageName.length);
//        $.each($('#accordionSidebar').find('li'), function () {
//            var hrefVal = $(this).find('a').attr('href');

//            if (hrefVal.indexOf(newPageName) >= 0) {
//                $(this).addClass('active').siblings().removeClass('active');
//            }
//        });
//    }
//});

function ChangeNavItemActive(linkVal) {
    var pageName = linkVal;
    var newPageName = pageName.split('/');
    console.log(newPageName);
    if (newPageName) {
        console.log('log here');
        $.each($('#accordionSidebar').find('li'), function () {
            var hrefVal = $(this).find('a').attr('href');
            if (hrefVal.indexOf(newPageName) >= 0) {
                $(this).addClass('active').siblings().removeClass('active');
            }
        });
    }
}