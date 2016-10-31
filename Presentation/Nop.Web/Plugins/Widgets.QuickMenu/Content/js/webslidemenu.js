$(function () {
    var items = $('.overlapblackbg, .slideLeft');
    var wsmenucontent = $('.wsmenucontent');

    var menuopen = function () {
        $(items).removeClass('menuclose').addClass('menuopen');
    }
    var menuclose = function () {
        $(items).removeClass('menuopen').addClass('menuclose');
    }

    $('#navToggle').click(function () {
        if (wsmenucontent.hasClass('menuopen')) { $(menuclose) }
        else { $(menuopen) }
    });
    wsmenucontent.click(function () {
        if (wsmenucontent.hasClass('menuopen')) { $(menuclose) }
    });

    $('#navToggle,.overlapblackbg').on('click', function () {
        $('.wsmenucontainer').toggleClass("mrginleft");
        $('.master-wrapper-page').toggleClass("mrginleft");
    });

    $('.wsmenu-list li').has('.wsmenu-submenu, .wsmenu-submenu-sub, .wsmenu-submenu-sub-sub').prepend('<span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>');

    $('.wsmenu-list li').has('.megamenu').prepend('<span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>');

    $('.wsmenu-mobile').click(function () {
        $('.wsmenu-list').slideToggle('slow');
    });
    $('.wsmenu-click').click(function () {

        $(this).siblings('.wsmenu-submenu:not(#ulnoMobile)').slideToggle('slow');
        $(this).children('.wsmenu-arrow').toggleClass('wsmenu-rotate');
        $(this).siblings('.wsmenu-submenu-sub').slideToggle('slow');
        $(this).siblings('.wsmenu-submenu-sub-sub').slideToggle('slow');
        $(this).siblings('.megamenu').slideToggle('slow', function () {
            if ($(this).is(':visible'))
                $(this).css('display', 'inline-block');
        });

    });

});