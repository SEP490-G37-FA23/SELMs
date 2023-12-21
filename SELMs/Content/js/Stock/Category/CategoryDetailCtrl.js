var app = angular.module("myApp", []);

app.controller('EquipmentsDetailCtrl', function ($scope, $http, $sce) {

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
    $scope.GetDetailCategory = function (category_id) {
        var partialUrl = origin + '/api/v1/categories/' + category_id;
        $http.get(partialUrl)
            .then(function (response) {
                console.log(response.data);
                $scope.DetailCategory = response.data.category;
                $scope.ListEquipsInCategory = response.data.category_equipments;

            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.GetDetailCategory(url);
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
    $scope.ListEquipsInCategory = [];
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
});