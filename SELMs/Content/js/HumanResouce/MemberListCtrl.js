var app = angular.module("myApp", []);

app.controller('MemberListCtrl', function ($scope, $http, $sce) {

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

    $scope.show = false;
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
    }

    $scope.ValidateDataInput = function (detailMember) {
        var regex1 = /\S/;
        var regex2 = /^\d{10}$/;
        var regex3 = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        var regex4 = /^[\p{L}\s]+$/u;

        console.log('newMember.fullname:', detailMember.fullname);
        console.log('newMember.dateOfBirth:', detailMember.dateOfBirth);
        console.log('newMember.hotline:', detailMember.hotline);
        console.log('check hotline:', regex2.test(detailMember.hotline));
        console.log('newMember.email:', detailMember.email);
        console.log('newMember.work_term:', detailMember.work_term);

        if (!regex2.test(detailMember.hotline)) {
            $scope.ErrorSystem('Số điện thoại không hợp lệ.');
            console.log('phone');
        }
        else if (!regex3.test(detailMember.email)) {
            $scope.ErrorSystem('Địa chỉ email không hợp lệ.');
            console.log('mail');
        }
        else if (!regex1.test(detailMember.work_term)) {
            $scope.ErrorSystem('Vui lòng nhập thời gian hợp đồng.');
            console.log('time');
        }
        else {
            $scope.UpdateMember(detailMember);
            console.log('update');
        }
    }

    //===============Danh sách khách hàng=====================
    $scope.text = '';
    $scope.LoadMembersList = function () {
        var data = {
            username: username,
            isadmin: isadmin,
            role: role,
            text: $scope.text
        }
        $http.post(origin + '/api/v1/members', data).then(function (response) {
            $scope.ListMembers = response.data;
        });
    }

    $scope.LoadMembersList();

    $scope.LoadRolesList = function () {

        $http.post(origin + '/api/v1/members/roles').then(function (response) {
            $scope.ListRoles = response.data;
        });
    }

    $scope.LoadRolesList();

    $scope.LoadMemberDetails = function (item) {
        $scope.DetailMember = item;
    }

    $scope.UpdateMember = function (member) {
        var dateOfBirth = $("#dateOfBirth").val();
        var hireDate = $("#hireDate").val();

        console.log(member);
        var data = {
            username: member.username,
            user_id: member.user_id,
            user_code: member.user_code,
            fullname: member.fullname,
            role_code: member.role_code,
            is_admin: member.is_admin,
            date_of_birth: dateOfBirth,
            hotline: member.hotline,
            email: member.email,
            gender: member.gender == 1 ? true : false,
            address: member.address,
            avatar_img: member.avatar_img,
            hire_date: hireDate,
            work_term: member.work_term,
            skill: member.skill,
            job_description: member.job_description,
            is_active: true,
            is_admin: member.is_admin
        }
        var partialUrl = origin + '/api/v1/members/update/' + member.user_id;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thành viên ' + member.fullname + ' đã cập nhật thông tin thành công!');
                $scope.LoadMembersList();
            },function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.MarkMemberQuit = function (item) {
        var partialUrl = origin + '/api/v1/members/resign/' + item.user_id;
        $http.post(partialUrl)
            .then(function (response) {
                $scope.SuccessSystem('Thành viên ' + item.fullname + ' đã dừng hoạt động');
                $scope.LoadMembersList();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }
    $scope.inputOldPass = '';
    $scope.inputNewPass = '';
    $scope.inputReNewPass = '';
    $scope.ChangePassword = function (item) {
        var data = {
            text: $scope.inputOldPass,
            text1: $scope.inputNewPass,
            text2: $scope.inputReNewPass,

        }
        var partialUrl = origin + '/api/v1/members/change-password/' + item.user_id;
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem(response.data);
                $scope.LoadMembersList();
                }
                , function (error) {
                    console.log(error.data.Message);
                    $scope.ErrorSystem('fail' + error.data.Message);
                })
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