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
        var partialUrl = origin + '/api/v1/equipments/import-equipments';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Nhập danh sách thiết bị thành công');
                $scope.LoadEquipmentsList();
                $scope.ListEquipImport = [];
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

    $scope.DeleteImport = function (index, ListEquipImport) {
        ListEquipImport.splice(index, 1);
    }

    $scope.tableToExcel = function (tableId) { // ex: '#my-table'
        var tab_text = "<table border='2px' style='width:100%'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        tab = document.getElementById(tableId); // id of table

        for (j = 0; j < tab.rows.length; j++) {
            tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            //tab_text=tab_text+"</tr>";
        }

        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE");
        var dt = new Date();
        var day = dt.getDate();
        var month = dt.getMonth() + 1;
        var year = dt.getFullYear();
        var hour = dt.getHours();
        var mins = dt.getMinutes();
        var postfix = day + "." + month + "." + year + "_" + hour + "." + mins;

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            txtArea1.document.open("txt/html", "replace");
            txtArea1.document.write(tab_text);
            txtArea1.document.close();
            txtArea1.focus();
            sa = txtArea1.document.execCommand("SaveAs", true, "DataTableExport.xls");
        }
        else // For Chrome and firefox (Other broswers not tested)
        {


            var myBlob = new Blob([tab_text], {
                type: 'application/vnd.ms-excel'
            });
            var url = window.URL.createObjectURL(myBlob);
            var a = document.createElement("a");
            document.body.appendChild(a);
            a.href = url;
            a.download = tableId + postfix + ".xls";
            a.click();
            //adding some delay in removing the dynamically created link solved the problem in FireFox
            setTimeout(function () {
                window.URL.revokeObjectURL(url);
            }, 0);
        }
        return (sa);
    }
});