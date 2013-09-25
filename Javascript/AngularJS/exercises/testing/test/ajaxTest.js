var myApp = angular.module('myApp', ['ngResource']); //register resource dependency!

myApp.factory('ajaxService', function($resource){
	return $resource('some/random/url/:id', {id:'@id'}, {});
});

beforeEach(module('myApp'));

describe('a service that uses ajax', function(){
	it('bla',inject(function(ajaxService, $httpBackend){
		//$httpBackend provides a mock http server

		//set stub behavior
		$httpBackend.when('GET', 'some/random/url/12').respond({result : 'res'});
		var actual = ajaxService.get({id : 12});

		$httpBackend.flush(); //trigger ajax callbacks

		expect(actual.result).toBe('res');
	}));
});