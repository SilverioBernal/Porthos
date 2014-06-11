var urlServicio = '/api/ProjectApi/';

var app = angular.module('projectList', ['ngTable']).controller('projectCtrl', function ($scope, $http,$filter, ngTableParams) {

    $http.get(urlServicio).success(function (data) {
        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                // initial filter
            }
        }, {
            total: data.length, // length of data

            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.filter() ?
                       $filter('filter')(data, params.filter()) :
                       data;

                $scope.projects = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve($scope.projects);
            }
        });
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    
});
