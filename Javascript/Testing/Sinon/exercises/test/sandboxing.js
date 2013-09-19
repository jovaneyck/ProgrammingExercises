describe('a sinon sandbox', function(){
	var myConsole = {
		lastMessage : '',
		log : function(message) {
			this.lastMessage = message;
		}
	}
	beforeEach(function(){
		myConsole.lastMessage = '';
	})

	it('should be able to create a sandbox within a test', function(){
		var sandbox = sinon.sandbox.create();

		sandbox.stub(myConsole); //you have to create stubs on the SANDBOX in order for this to work!

		myConsole.log('does not go to console, since it has been stubbed in this scope');
		expect(myConsole.lastMessage).toBe('');

		sandbox.restore();

		myConsole.log('this does get logged!');
		expect(myConsole.lastMessage).toBe('this does get logged!');
	});

	it('is still easy to corrupt the global namespace when using a sandbox inside a test', function(){
		var sandbox = sinon.sandbox.create();

		//I'm NOT calling sandbox.stub -> global namespace is affected!
		sinon.stub(myConsole);

		//do stuff within a sandbox

		sandbox.restore();

		myConsole.log('a message after restore is still not logged :(');

		expect(myConsole.lastMessage).toBe(''); // sadface
	});
});