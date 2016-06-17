'use strict';

angular.module('ServerlessApp', ['ServerlessApp.services','ServerlessApp.controllers'])
.config(function ($routeProvider, $httpProvider) {
  
  $routeProvider.when('/home', {templateUrl: 'views/home.html', controller: 'MainCtrl'});
  $routeProvider.when('/relic-list', {templateUrl: 'views/relic-list.html', controller: 'RelicListCtrl'});
  $routeProvider.when('/relic-detail/:id', {templateUrl: 'views/relic-detail.html', controller: 'RelicDetailCtrl'});
  $routeProvider.when('/relic-creation', {templateUrl: 'views/relic-creation.html', controller: 'RelicCreationCtrl'});
  $routeProvider.otherwise({redirectTo: '/home'});

  $httpProvider.defaults.useXDomain = true;
  delete $httpProvider.defaults.headers.common['X-Requested-With'];
});
