(function ($) {
    var initializeTorrentsWidget = function (element) {
        const sf_appPath = window.sf_appPath || "/";
        const widget = $(element);      
    };

    $(function () {
        $('[data-role=torrents-widget]').each(function (index, value) {
            initializeTorrentsWidget(value);
        });
    });
})(jQuery);
