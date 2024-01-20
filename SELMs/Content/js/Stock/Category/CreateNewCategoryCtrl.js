var app = angular.module("myApp", []);

app.controller('CreateNewCategoryCtrl', function ($scope, $http, $sce) {

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
    $scope.ResetNewCategory = function () {
        $scope.NewCategory = {
            category_code: '',
            category_name: '',
            number_equipment: 0,
            desciption: '',
            is_active: true,
            category_level: 1,
            category_parent_id: 0
        }
        $scope.ListEquipInCategory = [];
    }
    $scope.ResetNewCategory();

    $scope.HandelEquip = function (eq, item) {
        item.standard_equipment_code = eq.standard_equipment_code;
        item.equipment_name = eq.equipment_name;
        item.number_equip = eq.number_equip
    }
    $scope.LoadStandardEquipmentsList = function (text) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: text
        }
        $http.post(origin + '/api/v1/standard-equipments', data).then(function (response) {
            $scope.ListEquips = response.data;
        });
    }
    $scope.DeleteEquip = function (ListEquipInCategory, index) {
        ListEquipInCategory.splice(index, 1);
    }
    $scope.HandleNewEquipInCategory = function (ListEquipInCategory) {
        ListEquipInCategory.push({
            standard_equipment_code: '',
            equipment_name: '',
            number_equip: 0
        });
    }
    $scope.LoadListParentCategories = function (category_level) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            level: category_level
        }
        $http.post(origin + '/api/v1/parent-categories', data).then(function (response) {
            $scope.ListParentCategories = response.data;
        });
    }

    $scope.ValidateDataInput = function (category) {
        var regex = /\S/;
        console.log('category code: ', category.category_code);
        console.log('category name: ', category.category_name);
        console.log('category level: ', category.category_level);
        if (!regex.test(category.category_code) || category.category_code == '') {
            $scope.ErrorSystem('Vui lòng nhập mã danh mục.');
            console.log('category code not valid');
        }
        else if (!regex.test(category.category_name) || category.category_name == '') {
            $scope.ErrorSystem('Vui lòng nhập tên danh mục');
            console.log('category name not valid');
        }
        else {
            console.log('no invalid data');
            $scope.SaveNewCategory(category);
        }
    }

    $scope.SaveNewCategory = function (category) {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            category: {
                category_code: category.category_code,
                category_name: category.category_name,
                number_equipment: $scope.ListEquipInCategory.length,
                desciption: category.desciption,
                is_active: true,
                category_level: parseInt(category.category_level),
                category_parent_id: parseInt(category.category_parent_id)
            },
            equipments: $scope.ListEquipInCategory
        }
        var partialUrl = origin + '/api/v1/categories/new-category';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới danh mục thiết bị thành công!');
                $scope.ResetNewCategory();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
});