﻿var app = angular.module("myApp", []);

app.controller('MemberListCtrl', function ($scope, $http,$sce) {

    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    $scope.username = $('#username').val();
    $scope.tukhoa1 = "";
    $scope.tukhoa2 = "";
    $scope.tukhoa3 = "";
    $scope.tukhoa4 = "";
    $scope.tukhoa5 = "";
    $scope.tukhoa6 = "";
    $scope.tukhoa7 = "";

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
        notificationElement.style.width = '300px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';

        // You can customize the notification style and appearance here.
    }

    $scope.SuccessSystem = function (successMessage) {
        console.log("ok");
        // This function handles success messages and displays the success message as a notification.
        var notificationElement = document.getElementById('notification');
        notificationElement.textContent = 'Success: ' + successMessage;
        notificationElement.style.backgroundColor = '#97c797';
        notificationElement.style.width = '300px';
        notificationElement.style.height = '50px';
        notificationElement.style.textAlign = 'center';
        notificationElement.style.paddingTop = '15px';


        // You can customize the notification style and appearance here.
    }


    //===============Danh sách khách hàng=====================
    $scope.fullname = '';
    $scope.memberCode = '';
    $scope.LoadMembersList = function () {
        var data = {
            username: username,
            isadmin: 0,
            fullname: $scope.fullname,
            memberCode: $scope.memberCode
        }
        $http.post(origin + '/api/Members/List', data).then(function (response) {
            $scope.ListMembers = response.data;
        });
    }
    
    $scope.LoadMembersList();
  

    $scope.LoadMemberDetails = function (item) {

        var partialUrl = origin +  '/api/Member/' + item.user_id;

        $http.get(partialUrl)
            .then(function (response) {
                $scope.DetailMember = response.data.Result;
                //$("#user-details .modal-body").html(response.data);
            })
            .catch(function (error) {
                $scope.ErrorSystem('fail' + error);
            });
    }

    $scope.MarkMemberQuit = function (item) {
        var partialUrl = origin + '/api/Member/MarkMemberQuit' + item.user_id;
        $http.post(partialUrl)
            .then(function (response) {
                if (response.status == 200) {
                    $scope.SuccessSystem(response.data);
                } else {
                    $scope.ErrorSystem(response.data.Message);
                }
            })
            .catch(function (error) {
                $scope.ErrorSystem('fail' + error);
            });
    }

    
});