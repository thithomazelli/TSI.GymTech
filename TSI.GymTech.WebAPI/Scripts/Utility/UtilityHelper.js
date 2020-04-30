// Scripts to be executed on the onload page
$(document).ready(function () {
    
    toastr.options = {
        closeButton: true
    };

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

﻿// Cache selectors
var navMenu = $(".nav-menu"),
    navMenuHeight = navMenu.outerHeight() - 45,
    // All list items
    menuItems = navMenu.find("a"),
    // Anchors corresponding to menu items
    scrollItems = menuItems.map(function () {
        var item = $(this).attr("href");
        if (item.length) { return item; }
    });

    // Bind click handler to menu items so we can get a fancy scroll animation
    menuItems.click(function (e) {
        var href = $(this).attr("href"),
            offsetTop = href === "#" ? 0 : $(href).offset().top - navMenuHeight + 1;
        $('html, body').stop().animate({
            scrollTop: offsetTop
        }, 250);
        e.preventDefault();
    });

// 
function DisplayValidationSuccess() {
    var requiredFields = $('input,textarea,select').filter('[required]:visible');
    $.each(requiredFields, function (idx, element) {
        var parent = element.closest('div').closest('.form-group');
        $(parent).addClass('has-success');
    });
}

// 
function DisplayValidationErrors(errors) {
    $.each(errors, function (idx, validationError) {
        var validatedSpan = $("small[data-bv-validator][data-bv-for='" + validationError.PropertyName + "']");
        if (validatedSpan && validatedSpan.attr('style') == "") {
            var validatedMessage = validatedSpan[0].innerText;
            if (validatedMessage == validationError.ErrorMessage) {
                return;
            }
        }

        var element = $("span[data-valmsg-for='" + validationError.PropertyName + "']");
        var parent = $(element).closest('div');
        $(parent).addClass('has-error');
        element.text(validationError.ErrorMessage);
    });
}
                
$('#navSideBar .nav-menu a').on('click', function () {
    $('#navSideBar .nav-menu').find('li.active').removeClass('active');
    $(this).parent('li').addClass('active');
});

// Show or hide section panel
function ShowOrHideSection(element) {
    // Hide all sections
    $("section").each(function () {
        if ($(this).attr('id') != $(element).attr('id') && $(this).attr('id') != 'buttons') {
            $(this).hide();
        };
    });

    // Display selected section
    $(element).show();

    // Reload table resposinve
    var table = element.getElementsByClassName('stripe row-border');
    if (table.length > 0) {
        $(table).DataTable().responsive.recalc();
    }
}
                             
// Enable input to receive only number
function OnlyNumber(e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
}

// Validate SocialSecurityCard
function SocialSecurityCard(cpf) {
    var result;
    var message;

    if (cpf.length == 0) {
        result = true;
    } else if (cpf.length == 11) {
        var v = [];

        //Calcula o primeiro dígito de verificação.
        v[0] = 1 * cpf[0] + 2 * cpf[1] + 3 * cpf[2];
        v[0] += 4 * cpf[3] + 5 * cpf[4] + 6 * cpf[5];
        v[0] += 7 * cpf[6] + 8 * cpf[7] + 9 * cpf[8];
        v[0] = v[0] % 11;
        v[0] = v[0] % 10;

        //Calcula o segundo dígito de verificação.
        v[1] = 1 * cpf[1] + 2 * cpf[2] + 3 * cpf[3];
        v[1] += 4 * cpf[4] + 5 * cpf[5] + 6 * cpf[6];
        v[1] += 7 * cpf[7] + 8 * cpf[8] + 9 * v[0];
        v[1] = v[1] % 11;
        v[1] = v[1] % 10;

        //Retorna Verdadeiro se os dígitos de verificação são os esperados.
        if ((v[0] != cpf[9]) || (v[1] != cpf[10])) {
            result = false;
            message= 'CPF inválido.'
        }
    }
    else {
        result = false;
        message = 'CPF inválido.'
    }

    if (!result) {
        var errors = new Array();
        errors.push({ PropertyName: "SocialSecurityCard", ErrorMessage: message });
        DisplayValidationErrors(errors)
    }

    return result;
}