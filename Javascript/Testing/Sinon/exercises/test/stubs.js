describe('sinon stub', function(){
	it('', function(){
		var stub = sinon.stub();
		stub.returns('test');

		expect(stub()).toBe('test');
	});

	it('can stub all methods of an object', function(){
		var myObject = {
			foo : function(){
				throw {};
			},
			bar : function(){
				throw {};
			}
		};

		var myStub = sinon.stub(myObject);

		myStub.foo(); //actual method was not called, nice!
	});

	it('can easily throw exceptions', function(){
		var stub = sinon.stub();
		stub.throws('boom');
		
		expect(stub).toThrow();
	});

	it('can stub return values of mehtod objects', function(){
		function Character(){
			return {
				fullName : function(){
					throw 'implement';
				}
			};
		};

		var hans = sinon.stub(new Character()); //I need to instantiate a real object :(

		//set return values of a stubbed object method
		//i.e, you have access to all the stubbed functions
		hans.fullName.returns('hans');
		
		expect(hans.fullName()).toBe('hans');
	});

});