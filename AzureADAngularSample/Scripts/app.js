angular.module('AzureADAngularSample', []);

angular.module('AzureADAngularSample').directive('test', function() {
	return {
		restrict: 'E',
		templateUrl: 'test',
		controller: function($http) {
			var vm = this;

			vm.results = [];

			vm.callService = function() {
				$http.get(vm.url).then(function(results) {
					vm.results = results.data;
				});
				//vm.results = ['hello'];
			};
		},
		bindToController: true,
		controllerAs: 'vm',
		scope: {
			title: '@',
			url: '@'
		}

	}
});