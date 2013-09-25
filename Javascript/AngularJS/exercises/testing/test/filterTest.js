var filterApp = angular.module('filterApp', []);

filterApp.filter(
	'reverse',
	function(){
		return function(input){
			return input.split("").reverse().join("");
		};
	}
);

describe('my filter', function(){
	beforeEach(module('filterApp'));

	//!!!Note: your filter name must be suffixed with Filter!!!
	it('is testable', inject(function(reverseFilter){
		var actual = reverseFilter('kablam');

		expect(actual).toBe('malbak');
	}));
});