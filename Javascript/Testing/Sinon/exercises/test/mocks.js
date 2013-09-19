describe('sinon mocks', function(){
	function MyObject(){
		return {
			mockMe : function(argument){ throw '';},
			anotherFunction : function() { throw '';}
		};
	}

	function touchMyObject(object, argument) {
		object.mockMe(argument);
	}

	it('can set up expectations', function(){
		var actualObject = new MyObject(); //again, I need an instantiation :(
		var mock = sinon.mock(actualObject); 

		mock.expects("mockMe").once().withArgs(1337); //magical string :(

		touchMyObject(actualObject, 1337); //pass in the OBJECT, not the MOCK!

		mock.verify();
	});
});