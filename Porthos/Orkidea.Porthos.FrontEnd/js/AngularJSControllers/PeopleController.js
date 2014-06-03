var urlServicio = '/api/peopleapi/';


//function Hello($scope, $http) {

//    $http.get(urlServicio).success(function (data) {
//            $scope.peoples = data;
//        });
//}


var app = angular.module('userList', ['ngTable']).controller('userCtrl', function ($scope, $http,$filter, ngTableParams) {

    $http.get(urlServicio).success(function (data) {
        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                // initial filter
            }
        }, {
            total: data.length, // length of data
            //getData: function ($defer, params) {
            //    $defer.resolve(data.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            //}

            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.filter() ?
                       $filter('filter')(data, params.filter()) :
                       data;

                $scope.users = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve($scope.users);
            }
        });
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    //$.getJSON('/api/peopleapi/', function (data) {
    //    $scope.tableParams = new ngTableParams({
    //        page: 1,            // show first page
    //        count: 10           // count per page
    //    }, {
    //        total: data.length, // length of data
    //        getData: function ($defer, params) {
    //            $defer.resolve(data.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //        }
    //    });
    //});

    //$scope.tableParams = new ngTableParams({
    //    page: 1,            // show first page
    //    count: 10           // count per page
    //}, {
    //    total: data.length, // length of data
    //    getData: function ($defer, params) {
    //        $defer.resolve(data.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //    }
    //});
});


//var app = angular.module('main', ['ngTable']).controller('DemoCtrl', function ($scope, ngTableParams) {
//    var data = [{ name: "Moroni", age: 50 },
//                { name: "Tiancum", age: 43 },
//                { name: "Jacob", age: 27 },
//                { name: "Nephi", age: 29 },
//                { name: "Enos", age: 34 },
//                { name: "Tiancum", age: 43 },
//                { name: "Jacob", age: 27 },
//                { name: "Nephi", age: 29 },
//                { name: "Enos", age: 34 },
//                { name: "Tiancum", age: 43 },
//                { name: "Jacob", age: 27 },
//                { name: "Nephi", age: 29 },
//                { name: "Enos", age: 34 },
//                { name: "Tiancum", age: 43 },
//                { name: "Jacob", age: 27 },
//                { name: "Nephi", age: 29 },
//                { name: "Enos", age: 34 }];

//    $scope.tableParams = new ngTableParams({
//        page: 1,            // show first page
//        count: 10           // count per page
//    }, {
//        total: data.length, // length of data
//        getData: function ($defer, params) {
//            $defer.resolve(data.slice((params.page() - 1) * params.count(), params.page() * params.count()));
//        }
//    });
//});