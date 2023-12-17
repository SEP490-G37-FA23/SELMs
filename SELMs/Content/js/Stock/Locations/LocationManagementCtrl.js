var app = angular.module("myApp", []);

app.controller('LocationManagementCtrl', function ($scope, $http, $sce) {

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
    $scope.level = 'All'
    $scope.HandelLocation = function (lc, NewLocation) {
        $scope.text = lc.location_desciption;
        NewLocation.parent_location_id = lc.parent_location_id;

    }

    $scope.UpdateLocation = function (location) {
        var data = {
            Location: {
                location_code: location.location_code,
                location_desciption: location.location_desciption,
                parent_location_id: location.parent_location_id,
                location_level: location.location_level,
                is_active: true
            }
        }
        var partialUrl = origin + '/api/v1/locations/new-location';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật thông tin vị trí thành công!');

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

    $scope.DeleteLocation = function (location) {

        var partialUrl = origin + '/api/v1/locations/delete/' + location.location_id;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Xóa vị trí thành công!');
                $scope.LoadAllLocationList('');
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});