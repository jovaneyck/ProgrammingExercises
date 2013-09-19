describe('sinon matchers', function(){
	var spy;
	beforeEach(function(){
		spy = sinon.spy();
	})

	it('can match on argument types', function(){
		spy(1);
		sinon.assert.calledWithMatch(spy, sinon.match.number);
	});

	it('can match on substrings', function(){
		spy('a long hello message');
		sinon.assert.calledWithMatch(spy, sinon.match('hell'));
	});

	it('can ignore the specifics of an argument', function(){
		spy('blabla');
		sinon.assert.calledWithMatch(spy, sinon.match.any);
	})

	it('can check on reference equality', function(){
		var myObject = {val : 1};
		var myOtherObject = {val : 1};

		spy(myObject);

		expect(spy.calledWithMatch(sinon.match.same(myObject))).toBe(true);
		expect(spy.calledWithMatch(sinon.match.same(myOtherObject))).toBe(false);
	});

	it('can use custom matchers', function(){
		var lessThan100 = sinon.match(function(val){
			return val < 100;
		}, "less than 100");

		spy(99);

		expect(spy.calledWithMatch(lessThan100)).toBe(true);
	});

	it('can combine multiple matchers', function(){
		spy('hello world!');

		sinon.assert.calledWithMatch(spy, sinon.match('hello').and(sinon.match('world')));
	});
});