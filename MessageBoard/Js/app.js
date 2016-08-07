//home-index.js

var donApp = angular.module("donApp", []);

donApp.controller("homeIndexCtrl", homeIndexController);

function homeIndexController($http) {
    var vm = this;
    vm.name = "Anton";
    vm.dataCount = 0;
    vm.data = [];
    vm.isBusy = true;

    $http.get("/api/v1/topics/?includeReplies=true")
        .then(function(result) {
                //successful
                //vm.data = result; for collections better angular.copy
                //reason = when data is copied into a scope var like this
                //angular will update the scope automatically, re-executing 
                //the repeat directives 
            angular.copy(result.data, vm.data);
                vm.dataCount = result.data.length;

            },
            function() {
                //error
                alert("could not load topics");
            })
        .then(function() {
            //think of the second then as a kind of finally clause
            vm.isBusy = false;
        });
};