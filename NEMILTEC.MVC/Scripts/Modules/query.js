function pageInit($scope, model, $http, $timeout, Upload) {

    var cmdCallbacks = {
        1: function(data, parent) {
            if (data != null && data.Children.length > 0) {
                parent.Children.Children.push(data);
            }
        }
    };

    $scope.invokeCommand = function (parent, cmd) {

        $http.post(
            cmd.Url,
            cmd.Data
        ).then(function successCallback(resp) {
            cmdCallbacks[cmd.Id](resp.data, parent);
        }, function errorCallback(data) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

}

