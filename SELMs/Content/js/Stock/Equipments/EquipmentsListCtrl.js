var app = angular.module("myApp", []);

app.controller('EquipmentsListCtrl', function ($scope, $http, $sce) {

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
    $scope.text1 = '';
    $scope.text2 = '';
    $scope.categoryCode = 'C-More';
    $scope.LoadEquipmentsList = function () {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: $scope.text,
            text1: $scope.text1,
            text2: $scope.text2,
            text3: $scope.categoryCode

        }
        $http.post(origin + '/api/v1/equipments', data).then(function (response) {
            $scope.ListEquips = response.data;
            $scope.sumEquips = response.data.length;
        });
    }

    $scope.LoadEquipmentsList();

    $scope.LoadCategoriesList = function () {

        $http.post(origin + '/api/v1/categories').then(function (response) {
            $scope.ListCategories = response.data;
        });
    }

    $scope.LoadCategoriesList();

    $scope.DeleteEquip = function (equip) {
        var partialUrl = origin + '/api/v1/equipments/delete/' + equip.equipment_id;
        $http.post(partialUrl)
            .then(function (response) {
                $scope.SuccessSystem('Xóa thiết bị thành công');
                $scope.LoadEquipmentsList();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

    $scope.LoadEquipDetails = function (equip) {
        window.location.href = "/Equipments/EquipmentDetails/" + equip.system_equipment_code;
    }
    $scope.ListEquipImport =[]
    $scope.readExcelFile = function () {
        var input = document.getElementById('input_file');
        var file = input.files[0];

        if (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var data = e.target.result;
                var workbook = XLSX.read(data, { type: 'binary' });

                // Assuming there is only one sheet in the Excel file
                var sheetName = workbook.SheetNames[0];
                var sheet = workbook.Sheets[sheetName];

                // Parse sheet data as needed
                var jsonData = XLSX.utils.sheet_to_json(sheet, { header: 1 });

                // Output the parsed data
                console.log(jsonData);

               
                var extractedColumns = extractColumns(jsonData)
                $scope.ListEquipImport = extractedColumns;
                console.log($scope.ListEquipImport);
            };

            reader.readAsBinaryString(file);
        } else {
            console.error("No file selected");
        }
    };

    function extractColumns(jsonData) {
        // Get the header row from jsonData
        var headerRow = jsonData[0];

        // Define the indices of the columns you want to extract
        var indicesToExtract = [0, 1, 2, 3, 4, 5, 6, 7,8]; // Adjust these indices based on your needs

        // Extract columns based on indices and create an array of objects
        var extractedColumns = jsonData.slice(1).map(function (row) {
            var extractedObject = {};
            indicesToExtract.forEach(function (index, i) {
                extractedObject[headerRow[index]] = row[i];
            });
            return extractedObject;
        });

        return extractedColumns;
    }

    $scope.SaveImportListEquip = function () { 
        var data = {
            username: username,
            ListEquipImport:$scope.ListEquipImport
        }
        console.log(data);
        var partialUrl = origin + '/api/v1/equipments/import' ,data;
        $http.post(partialUrl)
            .then(function (response) {
                $scope.SuccessSystem('Nhập danh sách thiết bị thành công');
                $scope.LoadEquipmentsList();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }


});