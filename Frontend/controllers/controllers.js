'use strict';

/* Controllers */

var app = angular.module('ServerlessApp.controllers', ['ngRoute']);

app.controller('MainCtrl',function($scope) {
});


app.controller('RelicListCtrl', ['$scope', 'RelicsFactory', 'RelicFactory', '$location',
  function ($scope, RelicsFactory, RelicFactory, $location) {

    /* callback for ng-click 'editRelic': */
    $scope.editRelic = function (relicId) {
      $location.path('/relic-detail/' + relicId);
    };

    /* callback for ng-click 'deleteRelic': */
    $scope.deleteRelic = function (relic) {
      
      RelicFactory.delete.execute({ id: relic.id });
      
      $scope.relics.splice( $scope.relics.indexOf(relic), 1);
      $location.path('/relic-list');
      //$scope.relics = RelicsFactory.query.execute();
    };

    /* callback for ng-click 'createRelic': */
    $scope.createNewRelic = function () {
      $location.path('/relic-creation');
    };

    $scope.relics = RelicsFactory.query.execute();
  }]);
  
  app.controller('RelicDetailCtrl', ['$scope', '$routeParams', 'RelicFactory', '$location',
  function ($scope, $routeParams, RelicFactory, $location) {

    /* callback for ng-click 'updateRelic': */
    $scope.updateRelic = function () {
      RelicFactory.update.execute($scope.relic).$promise.then(function(){
        $location.path('/relic-list');
      });
    };

    /* callback for ng-click 'cancel': */
    $scope.cancel = function () {
      $location.path('/relic-list');
    };

    $scope.relic = RelicFactory.show.execute({id: $routeParams.id});
  }]);

app.controller('RelicCreationCtrl', ['$scope', 'RelicsFactory', '$location',
  function ($scope, RelicsFactory, $location) {

    /* callback for ng-click 'createNewRelic': */
    $scope.createNewRelic = function () {
      RelicsFactory.create.execute($scope.relic).$promise.then(function(){
        $location.path('/relic-list');
      });
    }
    
    /* callback for ng-click 'cancel': */
    $scope.cancel = function () {
      $location.path('/relic-list');
    };
  }]);
