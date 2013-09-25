module('app');

app.factory('myServiceWithDependencies', function(someOtherService){
	return {
		proxy : function(data){
			someOtherService.process(data);
		}
	};
});

//test code
//mock, preferrably use some mocking framework to avoid boilerplating (sinon!)
var mockService = {
	called : false,
	process : function(data){
		this.called = true;
	}
};

beforeEach(function(){
	module('app');

	//this is where the magic happens
	module(
		function($provide) {
			//register the mock with the angular provider, so DI resolves to the mock
			$provide.value('someOtherService', mockService);
		}
	);
})


describe("a service with dependencies", function(){
	it('can be tested in isolation', inject(function(myServiceWithDependencies){
		var input = 'heyho';

		myServiceWithDependencies.proxy(input);

		expect(mockService.called).toBe(true);
	}));
});
