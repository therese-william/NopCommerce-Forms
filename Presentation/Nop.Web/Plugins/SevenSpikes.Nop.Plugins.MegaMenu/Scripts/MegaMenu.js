$(document).ready(function () {
    var previousWidth = $.getSpikesViewPort().width;
    var checkBreakpointChange = function (isInitialLoad) {
        var viewport = $.getSpikesViewPort().width;

        if (isInitialLoad || (viewport > breakPointWidth && previousWidth <= breakPointWidth) || (viewport <= breakPointWidth && previousWidth > breakPointWidth)) {
            menu_prepareTopMenu(viewport);
        }

        previousWidth = viewport;
    };

    checkBreakpointChange(true);

    $(window).on("resize orientationchange", function () { checkBreakpointChange(false); });
});