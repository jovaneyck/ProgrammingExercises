describe('low-level ajax stubbing', function(){
	var xhr, request;

	beforeEach(function(){
		xhr = sinon.useFakeXMLHttpRequest();

		xhr.onCreate = function(req){
			request = req;
		}
	});

	afterEach(function(){
		request = null;
		xhr.restore();
	});

	it('can stub the low-level XHR', function(){
		var spy = sinon.spy();
		$.get('some/url', spy);
		
		//provide dummy response
		request.respond(200);

		expect(request.url).toBe('some/url');
	});
});

describe('higher-level ajax stubbing', function(){
	var server;
	beforeEach(function(){
		server = sinon.fakeServer.create();
	});
	afterEach(function(){
		server.restore();
	});

	it('can stub on a "server" level', function(){
		server.respondWith("Hello");

		var spy = sinon.spy();
		$.get('some/url', spy);

		server.respond(); //trigger callbacks

		sinon.assert.called(spy);
	});
});