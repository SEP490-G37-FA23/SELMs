var app = angular.module("myApp", []);

app.controller('LocationDetailCtrl', function ($scope, $http, $sce) {

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

    //this gets the full url
    var url = document.location.href;
    //this removes the anchor at the end, if there is one
    url = url.substring(0, (url.indexOf("#") == -1) ? url.length : url.indexOf("#"));
    //this removes the query after the file name, if there is one
    url = url.substring(0, (url.indexOf("?") == -1) ? url.length : url.indexOf("?"));
    //this removes everything before the last slash in the path
    url = url.substring(url.lastIndexOf("/") + 1, url.length);
    //return


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
    $scope.GetDetailLocation(url);
    $scope.showProject = false;
    $scope.showEquip = false;
    $scope.showOverview = false;
    $scope.showSubLocation = false;


    $scope.GetOverview = function () {
        $scope.showProject = false;
        $scope.showEquip = false;
        $scope.showOverview = true;
        $scope.showSubLocation = false;
    }
    $scope.GetEquipment = function () {
        $scope.showProject = false;
        $scope.showEquip = true;
        $scope.showOverview = false;
        $scope.showSubLocation = false;
    }
    $scope.GetSubLocation = function () {
        $scope.showProject = false;
        $scope.showEquip = false;
        $scope.showOverview = false;
        $scope.showSubLocation = true;
    }
    $scope.GetProject = function () {
        $scope.showProject = true;
        $scope.showEquip = false;
        $scope.showOverview = false;
        $scope.showSubLocation = false;
    }
});