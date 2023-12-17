/// <reference path="../../../../scripts/angular.min.js" />
/// <reference path="../../../../scripts/angular-route.min.js" />

var app = angular.module("myApp", []);

app.controller('CreateNewProjectCtrl', function ($scope, $http, $sce) {

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

    $scope.ResetNewProject = function () {
        $scope.NewProject = {
            project_name: '',
            acronym: '',
            description: '',
            manager: '',
            location_id: 0,
            status: 'Pending'
        }
        $scope.ListMembersJoinProject = [];
        $scope.ListEquipmentProject = [];
    }
    $scope.ResetNewProject();
    $scope.check = false;
    if (parseInt(url) > 0) {
        $scope.NewProject.location_id = url;
        $scope.check = true;
    }

    $scope.HandelMember = function (mb, NewProject) {
        $scope.NewProject.manager = mb.user_code;
        $scope.text = mb.fullname;
    }
    $scope.text = '';
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
    //$scope.LoadMembersList();


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

    $scope.ListMembersJoinProject = [];
    $scope.HandleNewMember = function () {
        $scope.ListMembersJoinProject.push({
            user_code: '',
            fullname: '',
            status: 'Đang tham gia',
            note: ''
        })
    }
    $scope.HandelMemberJoinProject = function (mb, item) {
        item.text = mb.user_code;
        item.user_code = mb.user_code;
        item.fullname = mb.fullname;

    }
    $scope.DeleteMemberProject = function (list, index) {
        list.splice(index, 1);
    }

    $scope.textEquip = '';
    $scope.LoadEquipmentsList = function (text) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: '',
            text1: '',
            text2: text,
            text3: ''

        }
        $http.post(origin + '/api/v1/equipments', data).then(function (response) {
            $scope.ListEquips = response.data;
        });
    }
    $scope.ListEquipmentProject = [];
    $scope.HandleNewEquip = function () {
        $scope.ListEquipmentProject.push({
            system_equipment_code: '',
            standard_equipment_code: '',
            equipment_name: '',
            unit: '',
            responsibler: '',
            usage_status: ''
        })
    }
    $scope.HandelEquipJoinProject = function (eq, item) {
        item.textEquip = eq.system_equipment_code;
        item.system_equipment_code = eq.system_equipment_code;
        item.standard_equipment_code = eq.standard_equipment_code;
        item.equipment_name = eq.equipment_name;
        item.unit = eq.unit;
        item.responsibler = eq.responsibler;
        item.usage_status = eq.usage_status;
    }
    $scope.DeleteEquipProject = function (list, index) {
        list.splice(index, 1);
    }
    $scope.CreateNewProject = function (newProject) {
        $scope.Project = {
            project_name: newProject.project_name,
            acronym: newProject.acronym,
            description: newProject.description,
            manager: newProject.manager,
            start_date: $('#startDate').val(),
            end_date: $('#endDate').val(),
            project_name: newProject.project_name,
            location_id: newProject.location_id,
            status: true,
            creater:username
        }
        var data = {
            Project: $scope.Project,
            ProjectMembers: $scope.ListMembersJoinProject,
            ProjectEquipments: $scope.ListEquipmentProject
        }
        var partialUrl = origin + '/api/v1/projects/new-project';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới dự án thành công!');
                $scope.ResetNewProject();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});