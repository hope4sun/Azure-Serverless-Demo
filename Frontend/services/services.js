'use strict';

var services = angular.module('ServerlessApp.services', ['ngResource']);

var baseUrl = 'https://serverless.azurewebsites.net/api/';

services.factory('RelicsFactory', ['$resource',
function ($resource) {
  
    return {
        query: $resource(baseUrl + 'GetAll', {}, { execute : { method: 'GET', isArray: true }}),
        create: $resource(baseUrl + 'Create', {}, { execute :{ method: 'POST' , params: {name: '@name', epitaph: '@epitaph'} }}),
    }
  }
]);

services.factory('RelicFactory', ['$resource',
  function($resource){
    return {
      show: $resource(baseUrl + 'Get', {}, { execute : { method:'GET', params: {id: '@id'}, isArray: false }}),
      update: $resource(baseUrl + 'Update', {}, { execute : { method:'PUT', params: {id: '@id', name: '@name', epitaph: '@epitaph'} }}),
      delete: $resource(baseUrl + 'Delete', {}, { execute : { method:'DELETE', params: {id: '@id'} } }),
    };
  }
]);

