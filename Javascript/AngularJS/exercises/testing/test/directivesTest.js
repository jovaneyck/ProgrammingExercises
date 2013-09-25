describe('my directive', function(){
	var el, clickedEventFired = false;

	beforeEach(module('app'));

	/*tricky business here and in the karma config!!!*/
	//1. load up the template used in the directive
	beforeEach(module('templates/template.html'));

	beforeEach(inject(function($compile, $rootScope){
		//2. set up scope
		//when testing directives, only use $rootScope, not $rootScope.$new()
		var scope = $rootScope;
		scope.data = "testData";

		//3. create and compile directive
		el = angular.element('<div my-directive my-data="data"></div>');
		$compile(el)(scope);
		//update html (simulate scope lifecycle)
		scope.$digest();

	}));

	it('renders the correct html with bound data', function(){
		//Debugging tests: to test whether or not your directive is getting compiled:
		//console.log(el.html());
		expect(el.text()).toContain('testData');
	});
})