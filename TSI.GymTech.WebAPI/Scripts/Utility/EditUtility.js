// Cache selectors
var navMenu = $(".nav-menu"),
    navMenuHeight = navMenu.outerHeight() - 45,
    // All list items
    menuItems = navMenu.find("a"),
    // Anchors corresponding to menu items
    scrollItems = menuItems.map(function () {
        var item = $(this).attr("href");
        if (item.length) { return item; }
    });

// Bind click handler to menu items
// so we can get a fancy scroll animation
menuItems.click(function (e) {
    var href = $(this).attr("href"),
        offsetTop = href === "#" ? 0 : $(href).offset().top - navMenuHeight + 1;
    $('html, body').stop().animate({
        scrollTop: offsetTop
    }, 250);
    e.preventDefault();
});