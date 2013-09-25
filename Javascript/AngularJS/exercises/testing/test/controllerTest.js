describe("my controller", function(){
	var scope, myController;

	//import your app module
	beforeEach(module('app'));

	//angular-mocks.js should be included in your karma config to gain access to the inject function
	//$controller and $rootScope get injected by angular
	beforeEach(inject(function($controller, $rootScope){
		//$rootScope allows you to create mock scopes
		scope = $rootScope.$new();
		//$controller allows you to instantiate controllers with custom dependencies
		myController = $controller('myController', 
			{$scope : scope});
	}));

	it("initializes model properties", function(){
		expect(scope.someModel).toBe('my model');
	});

	it("has testable functions (but that's not the greatest idea...)", function(){
		var actual = scope.reverse('hello');
		expect(actual).toBe('olleh');
	});
});

