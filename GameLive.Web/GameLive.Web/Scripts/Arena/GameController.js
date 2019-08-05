window.gameController = {
    addUser: function (name) {
        var result = "";

        $.ajax({
            type: "POST",
            url: '/Arena/AddUser',
            async: false,
            data: "{'name':'" + name + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                result = data;
            },
            error: function (e) {
                result = e.responseText;
            }
        });

        return result;
    },
    getUsers: function () {
        $.ajax({
            type: "POST",
            url: '/Arena/GetUsers',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {

                var userInfoDiv = "";

                for (var i = 0; i < data.length; i++) {
                    userInfoDiv += "<div>" + data[i].Name + " [" + data[i].Id + "]</div>";
                }

                var usersDiv = $("#users");

                usersDiv.empty();
                usersDiv.append(userInfoDiv);
            },
            error: function (e) {
                debugger;
            }
        });
    },
    move: function (userId) {
        $.ajax({
            type: "POST",
            url: '/Arena/Move',
            data: "{'userId':'" + userId + "', 'keyStateString':'Up, Down, Left, Right, IsAttack'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                //debugger;

            },
            error: function (e) {
                //debugger;

            }
        });
    }
};