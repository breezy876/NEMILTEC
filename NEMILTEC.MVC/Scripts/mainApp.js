var mainApp = angular.module('mainApp', ['infinite-scroll', 'ngFileUpload']);

mainApp.controller('MainController', ['$scope', '$http', '$timeout', 'Upload', function ($scope, $http, $timeout, Upload) {

    $scope.trustAsHtml = function (html) {
        return $sce.trustAsHtml(html);
    }

    $scope.init = function (model) {
        initScope($scope, model, $http, $timeout, Upload);
        pageInit($scope, model, $http, $timeout, Upload);
    }

}]);

mainApp.factory('RecursionHelper', ['$compile', function ($compile) {
    var RecursionHelper = {
        compile: function (element) {
            var contents = element.contents().remove();
            var compiledContents;
            return function (scope, element, attr) {
                if (!compiledContents) {
                    compiledContents = $compile(contents);
                }
                compiledContents(scope, function (clone, scope) {
                    element.append(clone);
                });
            };
        }
    };

    return RecursionHelper;
}]);

///begin editors
mainApp.directive('entityEditor', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            container: '=',
            parent: '='
        },
        templateUrl: '/Templates/Containers/Editors/EntityEditor.html',
        link: function(scope, element, attrs) {

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});
//end

mainApp.directive('editorContainer', function (RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            container: '=',
            parent: '='
        },
        templateUrl: '/Templates/Containers/EditorContainer.html',
        link: function (scope, element, attrs) {

        },
        compile: function (element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});


mainApp.directive('tableContainer', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            container: '=',
            parent: '=',
            doLoad: '='
        },
        templateUrl: '/Templates/Containers/TableContainer.html',
        link: function(scope, element, attrs) {

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('tabContainer', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            container: '=',
            parent: '='
        },
        templateUrl: '/Templates/Containers/TabContainer.html',
        link: function(scope, element, attrs) {

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('segmentContainer', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            container: '=',
            parent: '='
        },
        templateUrl: '/Templates/Containers/SegmentContainer.html',
        link: function(scope, element, attrs) {

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('listContainer', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            container: '=',
            parent: '=',
            propName: '=',
            addBlank: '=',
            isFilter: '=',
            item: '='
        },
        templateUrl: '/Templates/Containers/ListContainer.html',
        link: function(scope, element, attrs) {
            //console.log(attrs);

            //var items = scope.container.PropertyLists[scope.propName].Children;
            //console.log(items);


            //if (scope.model.HasChildren) {
            //    element.append("<container></collection>");
            //    $compile(element.contents())(scope)
            //}

            ////scope.$apply();
            //$compile(element.contents())(scope)

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('container', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '=',
            doLoad: '='
        },
        templateUrl: '/Templates/Container.html',
        link: function(scope, element, attrs) {

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});

mainApp.directive('child', function(RecursionHelper) {
    return {
        restrict: "E",
        transclude: true,
        scope: {
            model: '=',
            parent: '=',
            doLoad: '='
        },
        templateUrl: '/Templates/ContainerChild.html',
        link: function(scope, element, attrs) {

            //if (scope.model.HasChildren) {
            //    element.append("<container></collection>");
            //    $compile(element.contents())(scope)
            //}

            //scope.$apply();
            //$compile(element.contents())(scope)

        },
        compile: function(element, attributes) {
            return RecursionHelper.compile(element);
        }
    }
});


//mainApp.filter('reverse', function () {
    //    return function(input, uppercase) {
    //        input = input || '';
    //        var out = "";
    //        for (var i = 0; i < input.length; i++) {
    //            out = input.charAt(i) + out;
    //        }
    //        // conditional based on optional argument
    //        if (uppercase) {
    //            out = out.toUpperCase();
    //        }
    //        return out;
    //    };
    //})





