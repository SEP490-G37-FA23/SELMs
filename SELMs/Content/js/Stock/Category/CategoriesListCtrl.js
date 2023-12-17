var app = angular.module("myApp", []);

app.controller('CategoriesListCtrl', function ($scope, $http, $sce) {

    var username = $('#username').val();
    var isadmin = $('#isadmin').val();
    var role = $('#role').val();

    $scope.username = $('#username').val();

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
    $scope.text = '';
    $scope.LoadCategorisList = function () {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: $scope.text
        }
        $http.post(origin + '/api/v1/categories-list', data).then(function (response) {
            $scope.ListCategories = response.data;
        });
    }

    $scope.LoadCategorisList();

    $scope.LoadCategoryDetails = function (item) {
        window.location.href = origin +  "Category/CategoryDetail/" + item.category_id;
    }
    $scope.DeleteCategory = function (category) {
        var partialUrl = origin + '/api/v1/categories/deactive/' + category.category_id;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Xóa danh mục thiết bị thành công!');
                $scope.ResetNewCategory();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

});