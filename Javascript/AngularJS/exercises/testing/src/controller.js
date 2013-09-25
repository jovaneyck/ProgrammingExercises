app.controller('myController', function($scope){
	$scope.someModel = 'my model';

	$scope.reverse = function(data) {
		return data.split("").reverse().join("");
	}

	$scope.data = 'some data';
});