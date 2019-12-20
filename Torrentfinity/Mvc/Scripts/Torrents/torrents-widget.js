(function ($) {
    var initializeTorrentsWidget = function (element) {
        var sf_appPath = window.sf_appPath || "/";
        var widget = $(element);

        widget.find('a[data-role=vote-link]').each(function (index, value) {
            var link = $(value);
            var id = index + (currentPage - 1) * 5; // PageSize is a constant 5
            var idx = index;
            link.click(function () {
                $.post(sf_appPath + 'web-interface/books/vote/' + id, function (data) {
                    $(pointSpans[idx]).html(data);
                });

                return false;
            });
        });
    };

    $(function () {
        $('div[data-role=books-widget]').each(function (index, value) {
            initializeTorrentsWidget(value);
        });
    });
})(jQuery);