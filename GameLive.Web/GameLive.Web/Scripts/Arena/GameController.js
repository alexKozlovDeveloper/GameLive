window.gameController = {
    user: {
        id: "",
        name: "",
        position: {
            x: "0",
            y: "0",
            angle: "0"
        }
    },
    users: null,
    clearUser: function () {
        window.gameController.user.id = "";
        window.gameController.user.name = "";

        localStorage.setItem("user", null);
    },
    getStatisticsDiv: function (userName, killCount, deadCount) {

        var divStr = "<div >";
        divStr += "<div>" + userName + " Kills: '" + killCount + "' Deads: '" + deadCount + "' </div>";
        divStr += "</div>";

        return divStr;
    },
    getStarShipDiv: function (shipName, userId, x, y, angle) {
        var style = "style = 'position: relative; bottom: " + (-500 + y) + "px; left: " + x + "px; transform: rotate(" + angle + "deg)'";

        var divStr = "<div class='star-ship'>";
        divStr += "<img id='user-" + userId + "' src='/Content/Arena/Images/" + shipName + ".png' " + style + " />";
        divStr += "</div>";

        return divStr;
    },
    getExplosionDiv: function (userId, x, y, angle) {
        var style = "style = 'position: relative; bottom: " + (-500 + y) + "px; left: " + x + "px; transform: rotate(" + angle + "deg)'";

        var divStr = "<div class='explosion'>";
        divStr += "<img id='user-" + userId + "' src='/Content/Arena/Images/explosion.gif' " + style + " />";
        divStr += "</div>";

        return divStr;
    },
    getBulletDiv: function (bulletName, userId, x, y, angle) {
        var style = "style = 'position: relative; bottom: " + (-500 + y) + "px; left: " + x + "px; transform: rotate(" + angle + "deg)'";

        var divStr = "<div class='bullet'>";
        divStr += "<img id='bullet-" + userId + "' src='/Content/Arena/Images/" + bulletName + ".png' " + style + " />";
        divStr += "</div>";

        return divStr;
    },
    addUser: function (name) {
        var result = "";

        $.ajax({
            type: "POST",
            url: "/Arena/AddUser",
            async: false,
            data: "{'name':'" + name + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {

            },
            error: function (e) {
                result = e.responseText;

                window.gameController.user.id = result;
                window.gameController.user.name = name;

                localStorage.setItem('user', JSON.stringify(window.gameController.user));
            }
        });
    },
    getUsers: function () {
        $.ajax({
            type: "POST",
            url: '/Arena/GetUsers',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {

                window.gameController.users = data;

                var userInfoDiv = "";

                var usersStatistics = "";

                for (var i = 0; i < data.length; i++) {
                    //userInfoDiv += "<div>" + data[i].Name + "[" + data[i].Position.X + "x" + data[i].Position.Y + "] [" + data[i].Id + "]</div>";
                    userInfoDiv += "<div>" + JSON.stringify(data[i]) + "</div>";

                    usersStatistics += window.gameController.getStatisticsDiv(data[i].Name,
                        data[i].KillCount,
                        data[i].DeadCount);
                }

                var usersDiv = $("#users");

                var statisticsDiv = $(".statistics");

                usersDiv.empty();
                usersDiv.append(userInfoDiv);


                statisticsDiv.empty();
                statisticsDiv.append(usersStatistics);

                $(".starships-arena").empty();


                for (var i = 0; i < data.length; i++) {
                    if (data[i].Id === window.gameController.user.id) {
                        window.gameController.user.position = {
                            x: data[i].Position.X,
                            y: data[i].Position.Y,
                            angle: data[i].Position.Angle
                        }
                    }

                    var starShip = window.gameController.getStarShipDiv("Arasari Star Ship", data[i].Id, data[i].Position.X - 25, data[i].Position.Y + 25, data[i].Position.Angle);

                    if (i === 1) {
                        starShip = window.gameController.getStarShipDiv("Butterfly Star Ship", data[i].Id, data[i].Position.X - 25, data[i].Position.Y + 25, data[i].Position.Angle);
                    }

                    if (i === 2) {
                        starShip = window.gameController.getStarShipDiv("Flamingo Star Ship", data[i].Id, data[i].Position.X - 25, data[i].Position.Y + 25, data[i].Position.Angle);
                    }

                    if (i === 3) {
                        starShip = window.gameController.getStarShipDiv("Piranha Star Ship", data[i].Id, data[i].Position.X - 25, data[i].Position.Y + 25, data[i].Position.Angle);
                    }

                    if (i === 4) {
                        starShip = window.gameController.getStarShipDiv("Skat Star Ship", data[i].Id, data[i].Position.X - 25, data[i].Position.Y + 25, data[i].Position.Angle);
                    }

                    if (data[i].UserState === 1) {
                        starShip = window.gameController.getExplosionDiv(
                            data[i].Id,
                            data[i].Position.X - 25,
                            data[i].Position.Y + 25,
                            data[i].Position.Angle);
                    }

                    $(".starships-arena").append(starShip);
                }
            },
            error: function (e) {
                //debugger;
            }
        });
    },
    getBullets: function () {
        $.ajax({
            type: "POST",
            url: '/Arena/GetBullets',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {

                //debugger;
               
                for (var i = 0; i < data.length; i++) {
                    var bullet = window.gameController.getBulletDiv("Bullet2", data[i].UserId, data[i].Position.X, data[i].Position.Y, data[i].Position.Angle);
                    
                    $(".starships-arena").append(bullet);
                }
            },
            error: function (e) {
                //debugger;
            }
        });
    },
    move: function (userId) {

        var keyStateArray = [];

        if (window.KeyStateController.keyState.Right === true) {
            keyStateArray.push("Right");
        }

        if (window.KeyStateController.keyState.Left === true) {
            keyStateArray.push("Left");
        }

        if (window.KeyStateController.keyState.Up === true) {
            keyStateArray.push("Up");
        }

        if (window.KeyStateController.keyState.Down === true) {
            keyStateArray.push("Down");
        }

        if (window.KeyStateController.keyState.IsAttack === true) {
            keyStateArray.push("IsAttack");
        }

        //debugger;
        var mouseAngle = window.KeyStateController.GetAngle(window.gameController.user.position.x + 25,
            window.gameController.user.position.y - 25);
        debugger;
        var change = mouseAngle - window.gameController.user.position.angle;

        if (change < 0) {
            change *= -1;
        }

        if (change < 4) {

        } else {
            if (mouseAngle > window.gameController.user.position.angle) {
                if (change > 180) {
                    keyStateArray.push("CounterclockwiseRotation");
                } else {
                    keyStateArray.push("ClockwiseRotation");
                }
            } else {
                if (change > 180) {
                    keyStateArray.push("ClockwiseRotation");
                } else {
                    keyStateArray.push("CounterclockwiseRotation");
                }
            }
        }

        var keyState = keyStateArray.join();

        if (keyStateArray.length === 0) {
            keyState = "";
        }

        $("#keyState").text(keyState);

        $.ajax({
            type: "POST",
            url: '/Arena/Move',
            data: "{'userId':'" + userId + "', 'keyStateString':'" + keyState + "'}",//Up, Down, Left, Right, IsAttack
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                //debugger;

            },
            error: function (e) {
                //debugger;

            }
        });
    },
    
    init: function () {

        window.setInterval(function () {
            window.gameController.getUsers();
        }, 50);
        
        window.setInterval(function () {
            window.gameController.getBullets();
        }, 50);

        window.setInterval(function () {
            window.gameController.move(window.gameController.user.id);
        }, 10);
        
        $("#userNameLogin").click(function () {
            var input = $("#userNameInput")[0];

            if (input.value !== "") {
                debugger;

                window.gameController.addUser(input.value);

                window.gameController.hideLogin();

                //$(".login-container").css("display", "none");
                $(".title-container").css("display", "none");
            }
        });

        $("#userNameLogout").click(function () {
            gameController.clearUser();

            window.gameController.showLogin();
        });

        var user = JSON.parse(localStorage.getItem('user'));

        if (user !== null) {
            window.gameController.user = user;
            window.gameController.hideLogin();
        }

        window.setInterval(function () {

            var keyStateInfo = JSON.stringify(window.KeyStateController.keyState);
            var usersInfo = JSON.stringify(window.gameController.users);

            $(".debug-div").text(keyStateInfo + usersInfo);
        }, 50);
    },
    hideLogin: function() {
        $("#userNameDiscription").css("visibility", "hidden");
        $("#userNameInput").css("visibility", "hidden");
        $("#userNameLogin").css("visibility", "hidden");
        $("#userNameLogout").css("visibility", "visible");
    },
    showLogin: function () {
        $("#userNameDiscription").css("visibility", "visible");
        $("#userNameInput").css("visibility", "visible");
        $("#userNameLogin").css("visibility", "visible");
        $("#userNameLogout").css("visibility", "hidden");
    }
};