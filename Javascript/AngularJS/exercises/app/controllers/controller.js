eventsApp.controller('EventCtrl', function($scope){
	$scope.event = {
		name: 'Building kick-ass web applications, part 1',
		date: '1/1/2014',
		location: {
			address: "AE",
			city: "Haasrode"
		},
		sessions: [
			{
				name: 'Javascript 101',
				description: 'Freshen up your Javascript skills',
				votes: 0	
			},
			{
				name: "Angular for dummies",
				description: "New to angular? This session is for you!",
				votes: 0
			},
			{
				name: "Bootstrap basics",
				description: "Learn how to define slick web pages with Twitter Bootstrap",
				votes: 0
			}
		]
	};

	$scope.voteSession = function(session){
		session.votes++;
	};

})