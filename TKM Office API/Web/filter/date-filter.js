angular.module('SmartShelve')
    .filter('momentDateFilter', function () {
        return function (input, format) {
            if (input == null) { return ""; }

            format = format || "DD/MM/YYYY";

            var _date = moment(input).format(format);

            return _date;
        };
    }).filter('momentTimeFilter', function () {
        return function (input, format) {
            if (input == null) { return ""; }

            format = format || "HH:mm";

            var _date = moment(input).format(format);

            return _date;
        };
    }).filter('momentDateTimeFilter', function () {
        return function (input, format) {
            if (input == null) { return ""; }

            format = format || "DD/MM/YYYY HH:mm:ss";

            var _date = moment(input).format(format);

            return _date;
        };
    });