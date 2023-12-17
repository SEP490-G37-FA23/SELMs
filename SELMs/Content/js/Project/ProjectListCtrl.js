var app = angular.module("myApp", []);

app.controller('ProjectListCtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    var role = $('#role').val();

    $scope.username = $('#username').val();
    $scope.isadmin = $('#isadmin').val();
    $scope.role = $('#role').val();
    $scope.today = new Date();

    var dd = $scope.today.getDate();
    var mm = $scope.today.getMonth() + 1; //January is 0!

    var yyyy = $scope.today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    $scope.NgayHomNay = dd + '/' + mm + '/' + yyyy;


    $scope.ErrorSystem = function (errorMessage) {
        // This function handles errors and displays the error message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Error: ' + errorMessage;
        notificationElement.style.backgroundColor = '#f5aaaa';
        notificationElement.style.width = '600px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';
        notificationElement.style.zIndex = '10000';
        notificationElement.style.marginLeft = '100px';

        // You can customize the notification style and appearance here.
    }

    $scope.SuccessSystem = function (successMessage) {
        console.log("ok");
        // This function handles success messages and displays the success message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Success: ' + successMessage;
        notificationElement.style.backgroundColor = '#97c797';
        notificationElement.style.width = '600px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';
        notificationElement.style.zIndex = '10000';
        notificationElement.style.marginLeft = '100px';


        // You can customize the notification style and appearance here.
    }


    //===============Danh sách khách hàng=====================
    $scope.project_name = '';
    $scope.location_name = '';
    $scope.manager_name = '';
    $scope.application_code = '';
    $scope.LoadProjectList = function () {
        var data = {
            username: username,
            isadmin: isadmin,
            project_name: $scope.project_name,
            location_name: $scope.location_name,
            manager_name: $scope.manager_name
        }
        $http.post(origin + '/api/v1/projects/search', data).then(function (response) {
            $scope.ListProject = response.data
            console.log($scope.ListProject);
        });
    }
    $scope.LoadProjectList();
    $scope.LoadProjectDetails = function (proj) {
        window.location.href = "/Project/EquipmentDetails/" + proj.project_id;
    }

});