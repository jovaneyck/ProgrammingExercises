var myDependency = {
	someMethod : function() {
		return 'dependency';
	}
}

var myService = (function(dependency){
	return {
		callCB: function(callBack) {
			callBack();
		},

		callCBTwice: function(callBack) {
			this.callCB(callBack);
			this.callCB(callBack);
		},

		returnCB: function(callBack) {
			return callBack();
		},

		callDependency: function() {
			return dependency.someMethod();
		},

		callMultipleCBs : function(cb1, cb2){
			cb1();
			cb2();
		},

		callWithArgument: function(callback, value1, value2) {
			callback(value1, value2);
		}
	};
})(myDependency); //inject dependency

describe("sinon's features", function(){
	it('can create spies and check whether it was called', function(){
		var spy = sinon.spy();

		myService.callCB(spy);
		expect(spy.called).toBe(true);
	});

	it("can call through real implementations", function(){
		//don't see why you should use this after reading #GOOS
		var calledMe = false;
		function realCallback(){
			calledMe = true;
			return 666;
		}

		var spy = sinon.spy(realCallback);
		var actual = myService.returnCB(spy);

		expect(spy.called).toBe(true);
		expect(calledMe).toBe(true);
		expect(actual).toBe(666);
	});

	it("can spy on methods of an object", function(){
		var spy = sinon.spy(myDependency, 'someMethod') //magical strings :(
		var actual = myService.callDependency();

		expect(spy.called).toBe(true);
		expect(actual).toBe('dependency');

	});

	it("can verify expectations on the exact number of calls", function(){
		var spy = sinon.spy();

		myService.callCBTwice(spy);

		expect(spy.calledTwice).toBe(true);
	});

	it("can verify temporal coupling of functions", function(){
		//again, don't know why you would go there...
		var spy1 = sinon.spy();
		var spy2 = sinon.spy();

		myService.callMultipleCBs(spy1, spy2)

		expect(spy1.calledBefore(spy2)).toBe(true);
	});

	it("can verify argument values", function(){
		var spy1 = sinon.spy();

		myService.callWithArgument(spy1, 1337, 'second arg');

		expect(spy1.calledWithExactly(1337, 'second arg')).toBe(true);
	});
});