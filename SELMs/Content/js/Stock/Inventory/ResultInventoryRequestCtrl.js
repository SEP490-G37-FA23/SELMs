var app = angular.module("myApp", []);

app.controller('ResultInventoryRequestCtrl', function ($scope, $http, $sce) {

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
    $scope.itemSearch = '';
    $scope.GetResultIER = function () {
        var data = {
            username: username
        }
        $http.post(origin + '/api/v1/inventory/result', data).then(function (response) {
            $scope.ListResultInventory = response.data.Result;
        });
    }
    $scope.GetResultIER();
    $scope.UpdateUsageEquip = function () {
        var data = {
            username: username,
            listUpdate: $scope.ListEquipUpdate
        }
        console.log(data)
        var partialUrl = origin + '/api/v1/inventory-request/update-equipment-result'; 
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Cập nhật tình trạng thiết bị theo kiểm kê thành công!');
                $scope.GetResultIER();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

    $scope.DeleteUsageEquip = function () {
        var data = {
            username: username,
            listUpdate: $scope.ListEquipUpdate
        }
        console.log(data)
        var partialUrl = origin + '/api/v1/inventory-request/delete-equipment-result';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Xóa các kết quả kiểm kê thành công!');
                $scope.GetResultIER();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

    $scope.ListEquipUpdate = [];
    $scope.SelectEquip = function (item) {
        const existingItemIndex = $scope.ListEquipUpdate.findIndex(e => e.system_equipment_code === item.system_equipment_code);

        if (item.selected) {
            // Check if an item with the same system_equipment_code exists in $scope.ListEquipUpdate
            if (existingItemIndex == -1) {
                $scope.ListEquipUpdate.push({
                    application_detail_id: item.application_detail_id,
                    actual_usage_status: item.actual_usage_status, 
                    equipment_id: item.equipment_id,
                    inventory_results: 'Đã cập nhật thiết bị'
                });
            }
            // Add the new item to $scope.ListEquipUpdate
          
        } else {
            if (existingItemIndex !== -1) {
                // If exists, remove the existing item
                $scope.ListEquipUpdate.splice(existingItemIndex, 1);
            }
        }
    }

})