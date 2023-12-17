var app = angular.module("myApp", []);

app.controller('CreateNewLocationCtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var fullname = $('#fullname').val();
    var isadmin = $('#isadmin').val();
    var role = $('#role').val();

    $scope.username = $('#username').val();
    $scope.ErrorSystem = function (errorMessage) {
        // This function handles errors and displays the error message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Error: ' + errorMessage;
        notificationElement.style.backgroundColor = '#f5aaaa';
        notificationElement.style.width = '1000px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';

        // You can customize the notification style and appearance here.
    }

    $scope.SuccessSystem = function (successMessage) {
        // This function handles success messages and displays the success message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Success: ' + successMessage;
        notificationElement.style.backgroundColor = '#97c797';
        notificationElement.style.width = '1000px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';
    };


    $scope.LoadAllLocationList = function (text) {
        data = {
            username: username,
            id: -1,
            text: text
            }
        $http.post(origin + '/api/v1/locations', data).then(function (response) {
            $scope.ListAllLocation = response.data.Result;
        });
    }

    $scope.LoadAllLocationList('');

    $scope.text = '';
    $scope.HandelLocation = function (lc, NewLocation) {
        $scope.text = lc.location_desciption;
        NewLocation.parent_location_id = lc.location_id;

    }
    $scope.NewLocation = {
        location_code: '',
        location_desciption: '',
        parent_location_id: 0,
        location_level: 1,
        is_active: true
        }
    $scope.SaveNewLocation = function (newLocation) {
        var data = {
            Location :{
                location_code: newLocation.location_code,
                location_desciption: newLocation.location_desciption,
                parent_location_id: newLocation.parent_location_id,
                location_level: newLocation.location_level,
                is_active: true
            },
            SubLocations :[]
           
        }
        var partialUrl = origin + '/api/v1/locations/new-location';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới vị trí thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});