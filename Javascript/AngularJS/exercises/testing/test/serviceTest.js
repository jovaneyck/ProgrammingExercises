app.factory('myService', function(){
	return {
		reverse : function(data) {
			return data.split("").reverse().join("");
		}
	}
});

describe("my service", function(){
	beforeEach(module('app'));

	it('can be tested', inject(function(myService){
		var actual = myService.reverse('wop');
		expect(actual).toBe('pow');
	}));
});