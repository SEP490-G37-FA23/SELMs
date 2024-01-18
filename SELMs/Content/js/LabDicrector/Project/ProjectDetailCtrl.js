
var app = angular.module("myApp", []);

app.controller('ProjectDetailCtrl', function ($scope, $http, $sce) {

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

        // Hiển thị thông báo trong 5 giây
        var displayTime = 5000; // 5 giây
        setTimeout(function () {
            notificationElement.textContent = '';
            notificationElement.style.display = 'none';
        }, displayTime);
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

        // Hiển thị thông báo trong 5 giây
        var displayTime = 5000; // 5 giây
        setTimeout(function () {
            notificationElement.textContent = '';
            notificationElement.style.display = 'none';
        }, displayTime);
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

    $scope.ValidateDataInput = function (project) {
        var regex = /\S/;

        var data = {
            start_date: $('#startDate').val(),
            end_date: $('#endDate').val(),
        }
        console.log('Start Date:', data.start_date);
        console.log('End Date:', data.end_date);
        console.log('Comparison Result:', data.end_date < data.start_date);
        if (data.end_date < data.start_date) {
            $scope.ErrorSystem('Ngày bắt đầu phải nhỏ hơn ngày kết thúc dự án.');
        }
        else {
            $scope.UpdateProject(project);
        }
    }

    $scope.GetDetailProject = function (project_id) {
        var partialUrl = origin + '/api/v1/projects/' + project_id;
        $http.get(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailProject = response.data.Project;
                $scope.text = $scope.DetailProject.manager_name;
                $scope.ListMembersJoinProject = response.data.ListMemberInProject;
                $scope.ListEquipmentProject = response.data.ListEquipmentInProject;
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.GetDetailProject(url);


    $scope.Resetproject = function () {
        $scope.project = {
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
    $scope.Resetproject();
    $scope.check = false;
    if (parseInt(url) > 0) {
        $scope.project.location_id = url;
        $scope.check = true;
    }

    $scope.HandelMember = function (mb, project) {
        $scope.project.manager = mb.user_code;
        $scope.text = mb.fullname;
        $scope.ListMembersJoinProject.push({
            text: mb.user_code,
            user_code: mb.user_code,
            fullname: mb.fullname,
            status: 'Đang tham gia',
            note: 'Người quản lý'
        });
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
            $scope.ListLocations = response.data;
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
        // Kiểm tra nếu system_equipment_code đã tồn tại trong ListEquipmentProject
        var existingItemIndex = $scope.ListEquipmentProject.findIndex(function (existingItem) {
            return existingItem.system_equipment_code === eq.system_equipment_code;
        });

        if (existingItemIndex !== -1) {
            // Nếu tồn tại, xóa dòng cũ trước khi thêm dòng mới
            $scope.ListEquipmentProject.splice(existingItemIndex, 1);
        }

        // Thêm dòng mới
        item.textEquip = eq.system_equipment_code;
        item.system_equipment_code = eq.system_equipment_code;
        item.standard_equipment_code = eq.standard_equipment_code;
        item.equipment_name = eq.equipment_name;
        item.unit = eq.unit;
        item.responsibler = eq.responsibler;
        item.usage_status = eq.usage_status;
    };
    $scope.DeleteEquipProject = function (list, index) {
        list.splice(index, 1);
    }
    $scope.UpdateProject = function (project) {
        $scope.Project = {
            acronym: project.acronym,
            description: project.description,
            manager: project.manager,
            start_date: $('#startDate').val(),
            end_date: $('#endDate').val(),
            project_name: project.project_name,
            location_id: project.location_id,
            status: true,
            creater: username
        }
        var data = {
            Project: $scope.Project,
            ProjectMembers: $scope.ListMembersJoinProject.map(function (member) {
                return member.user_code;
            }),
            ProjectEquipments: $scope.ListEquipmentProject.map(function (member) {
                return member.system_equipment_code;
            })
        }
        var partialUrl = origin + '/api/v1/projects/update-project/' + project.project_id;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật dự án thành công!');
                $scope.Resetproject();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});