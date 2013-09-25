app.directive('myDirective', function(){
	return {
		restrict: 'A',
		templateUrl: 'templates/template.html',
		scope : {
			data : "=myData"
		}
	};
})