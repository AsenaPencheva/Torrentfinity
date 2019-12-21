(function ($) {
    var initializeTorrentsWidget = function (element) {
        const sf_appPath = window.sf_appPath || "/";
        const widget = $(element);
        console.log(f_appPath);
        //widget.find('a[data-role=vote-link]').each(function (index, value) {
        //    var link = $(value);
        //    var id = index + (currentPage - 1) * 5; // PageSize is a constant 5
        //    var idx = index;
        //    link.click(function () {
        //        $.post(sf_appPath + 'web-interface/books/vote/' + id, function (data) {
        //            $(pointSpans[idx]).html(data);
        //        });

        //        return false;
        //    });
        //});
        //widget.submit(function (e) {
        //    e.preventDefault();

           
         
        //});
    };

    $(function () {
        $('[data-role=torrents-widget]').each(function (index, value) {
            initializeTorrentsWidget(value);
            console.log('fsdfad');

            var designerModule = angular.module('designer');
            console.log(designerModule);
            angular.module('designer').requires.push('sfFields');
            angular.module('designer').requires.push('sfSelectors');

            //// NOTE: Use this code only with Sitefinity version 9.1 or above. Otherwise the "ngSanitize" module should no be included. 
            angular.module('designer').requires.push('ngSanitize');
        });
    });
})(jQuery);
