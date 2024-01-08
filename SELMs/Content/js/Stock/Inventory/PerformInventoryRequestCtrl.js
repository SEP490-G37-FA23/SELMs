var app = angular.module("myApp", []);

app.controller('PerformInventoryRequestCtrl', function ($scope, $http, $sce) {

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

    $scope.LoadLocationsList = function () {
        var data = {
            username: username,
            text: '',
            level: 1,
            id: 0
        }
        $http.post(origin + '/api/v1/locations', data).then(function (response) {
            $scope.ListLocations = response.data;
        });
    }
    $scope.LoadLocationsList();
    $scope.itemSearch = '';
    $scope.GetListIERInLocation = function (location_id) {
        var data = {
            username: username
            }
        $http.post(origin + '/api/v1/inventory/detail-in-location/' + parseInt(location_id),data).then(function (response) {
            $scope.ListInventoryDetail = response.data.Result;
        });
    }
    $scope.location_id = 0;
    $scope.SavePerformInventory = function () {
        var data = {
            username: username,
            listPerform: $scope.ListInventoryDetail
        }
        console.log(data)
        var partialUrl = origin + '/api/v1/inventory-request/perform';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Lưu kết quả thực hiện kiểm kê thành công!');
                $scope.GetListIERInLocation($scope.location_id);
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
})