//app.js

var donApp = angular.module("donHomeIndex", ["ngRoute"]); //for ng-app

donApp.config(function($routeProvider) {

    $routeProvider
        .when("/", {
            controller: "topicsController",
            controllerAs: "topicsCtrl",
            templateUrl: "/Js/templates/topicsView.html"
        })
        .when("/newmessage", {
            controller: "newTopicController",
            controllerAs: "newTopicCtrl",
            templateUrl: "/Js/templates/newTopicView.html"
        })
        .otherwise({
            redirectTo: "/"
        });
});

donApp.controller("topicsController", topicsController); //for topicsView.html

function topicsController($http) {
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

/*Window will be useful for us when we want to be able to programmatically 
 *  change the window location back to the home-view if nessacary */
donApp.controller("newTopicController", newTopicController); //for newTopicView.html

function newTopicController($http, $window) {
    var vm = this;
    vm.newTopic = {};

    vm.save = function() {
        alert(vm.newTopic.Title);
    }
}

