var app = angular.module("myApp", []);

app.controller('CreateNewMemberCtrl', function ($scope, $http, $sce) {

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

    $scope.ValidateDataInput = function (newMember) {
        var regex1 = /\S/;
        var regex2 = /^\d{10}$/;
        var regex3 = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        var regex4 = /^[\p{L}\s]+$/u;

        console.log('newMember.fullname:', newMember.fullname);
        console.log('newMember.dateOfBirth:', newMember.dateOfBirth);
        console.log('newMember.hotline:', newMember.hotline);
        console.log('check hotline:', regex2.test(newMember.hotline));
        console.log('newMember.email:', newMember.email);
        console.log('newMember.work_term:', newMember.work_term);

        if (!regex4.test(newMember.fullname)) {
            $scope.ErrorSystem('Tên thành viên mới không đúng định dạng.');
            console.log('name');
        }
        else if (!regex2.test(newMember.hotline)) {
            $scope.ErrorSystem('Số điện thoại không hợp lệ.');
            console.log('phone');
        }
        else if (!regex3.test(newMember.email)) {
            $scope.ErrorSystem('Địa chỉ email không hợp lệ.');
            console.log('mail');
        }
        else if (!regex1.test(newMember.work_term)) {
            $scope.ErrorSystem('Vui lòng nhập thời gian hợp đồng.');
            console.log('time');
        }
        else {
            $scope.CreateNewMember(newMember);
            console.log('add');
        }
    }

    $scope.NewMember = {
        fullname: '',
        role_code: 'MB',
        is_admin: false,
        date_of_birth: '',
        hotline: '',
        email: '',
        gender: true,
        address: '',
        avatar_img: '',
        hire_date: '',
        work_term: '',
        skill: '',
        job_description: '',
        is_active: true
    }

    $scope.LoadRolesList = function () {

        $http.post(origin + '/api/v1/members/roles').then(function (response) {
            $scope.ListRoles = response.data;
        });
    }

    $scope.LoadRolesList();

    $scope.CreateNewMember = function (member) {
        console.log(member);
        var dateOfBirth = $("#dateOfBirth").val();
        var hireDate = $("#hireDate").val();
        var data = {
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
        var partialUrl = origin + '/api/v1/members/new-member';
        $http.post(partialUrl, data)
            .then(function (response) {
                $scope.SuccessSystem('Thêm mới thành viên có  thành công!');
                $scope.LoadMembersList();
            }, function (error) {
                $scope.ErrorSystem(error.data.Message);
            });
    }

});