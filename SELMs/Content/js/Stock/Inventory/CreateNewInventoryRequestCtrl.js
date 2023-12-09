var app = angular.module("myApp", []);

app.controller('CreateNewInventoryRequestCtrl', function ($scope, $http, $sce) {

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
            $scope.ListLocations = response.data.Result;
        });
    }
    $scope.LoadLocationsList();
    $scope.ResetNewInventory = function () {
        $scope.requesterSearch = '';
        $scope.NewInventory = {
            requester: '',
            location_id: 0

            }
    }

    $scope.ResetNewInventory();

    $scope.LoadMembersList = function (text) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: text
        }
        $http.post(origin + '/api/v1/members', data).then(function (response) {
            $scope.ListMembers = response.data;
        });
    }
    $scope.HandelMemberRequest = function (mb, NewInventory) {
        NewInventory.requester = mb.user_code;
        $scope.requesterSearch = mb.fullname;
    }
    $scope.GetDetailLocation = function (location_id) {
        new QRCode(document.getElementById('id_qrcode'), 'https://localhost:44335/Locations/LocationDetails/' + location_id);
        var partialUrl = origin + '/api/v1/locations/detail/' + location_id;
        $http.post(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailLocation = response.data.location_info;
                $scope.ListSubLocation = response.data.ListSubLocation;
                $scope.ListProjectInLocation = response.data.ListProjectInLocation;
                $scope.ListEquipmentInLocation = response.data.ListEquipmentInLocation;
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
   
});