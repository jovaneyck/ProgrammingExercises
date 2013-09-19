describe("sinon's custom assertions", function(){
	it("can be checked using jasmine's native assertions", function(){
		var spy = sinon.spy();
		spy();

		//expected 'false' to be 'true' does not say much :(
		expect(spy.called).toBe(true);
	});

	it("can use sinon's specific assertions", function(){
		var spy = sinon.spy();
		spy();

		//expected spy to have been called but was never called is a whole lot more useful!
		sinon.assert.called(spy);
	});
});