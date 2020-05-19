$("document").ready(function () {
    $("#frmLogin").on('submit', function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            var info = {
                Email: $("#Email").val(),
                Password: $("#Password").val(),
                RememberMe: $("#RememberMe").prop('checked')
            };
            $.ajax({
                url: 'https://localhost:44353/api/account/login',
                type: 'post',
                data: JSON.stringify(info),
                dataType: 'text',
                contentType: 'application/json',
                success: function (data) {
                    console.log(data);
                    localStorage.setItem("Token", data);
                    window.location.href = "/home/HiddenContent";
                },
                error: function (xhr) {
                    console.log("in xhr:" + xhr);
                }
            });
            console.log(info);
        }
    });

    $("#frmRegister").on('submit', function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            var info = {
                Email: $("#Email").val(),
                Password: $("#Password").val(),
                UserName: $("#UserName").val(),
                ConfirmPassword: $("#ConfirmPassword").val()
            };
            $.ajax({
                url: 'https://localhost:44353/api/account/CreateAccount',
                type: 'post',
                data: JSON.stringify(info),
                dataType: 'text',
                contentType: 'application/json',
                success: function (data) {
                    console.log("data is : " + data);
                    
                },
                error: function (xhr) {
                    console.log(xhr);
                }
            });
            console.log(info);
        }
    });


    $("#btnSignOut").on('click', function () {
        var token = localStorage.getItem("Token");

        if (token !== null) {
            $.ajax({
                url: 'https://localhost:44353/api/user/logout',
                type: 'post',
                dataType: 'text',
                contentType: 'application/json',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                success: function (data) {
                    console.log(data);
                    localStorage.removeItem("Token");
                    window.location.href = "/Home/Login";
                },
                error: function (xhr) {
                    console.log("in xhr:" + xhr);
                }
            });
        }
        else
        {
            window.location.href = "/Home/Login";
        }
    });
});

function GetHiddenContent() {
    var token = localStorage.getItem("Token");
    if (token !== null) {
        $.ajax({
            url: 'https://localhost:44353/api/user/getallregistereduser',
            type: 'get',
            dataType: 'json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (data) {
                console.log(data);
                $.each(data, function (index, element) {
                    console.log(element);
                    var el = "<li>" + element.normalizedUserName + " (<a href='mailto:" + element.email + "'>" + element.email + "</a>) </li>";
                    $("#olUser").append(el);
                });
            },
            error: function (xhr) {
                console.log("in xhr:" + xhr);
            }
        });
    }
    else {
        $("#noAccess").removeClass("hidden");
        setTimeout(function () {
            window.location.href = "/Home/Login";
        }, 2000);
    }
}