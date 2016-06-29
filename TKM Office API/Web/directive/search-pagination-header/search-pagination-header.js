angular.module('SmartShelve').directive('searchPaginationHeader', function() {
	return {
		restrict: 'AE',
		replace: true,
		scope: {
            searchQuery: "=",
            onSearchFunction: '&'
		},
		templateUrl: 'directive/search-pagination-header/search-pagination-header.html',
		link: function(scope, element, attrs, fn) {
            scope.perPagesOptions = [10, 20, 50];

            scope.onKeyPress = function ($event) {
                if ($event.keyCode === 13) {
                    scope.onSearchFunction();
                }
            };
		}
	};
});
