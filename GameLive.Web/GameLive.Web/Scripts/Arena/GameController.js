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
    clearUser: function () {
        window.gameController.user.id = "";
        window.gameController.user.name = "";

        localStorage.setItem("user", null);
    },
    getStarShipDiv: function (shipName, userId, x, y, angle) {
        var style = "style = 'position: relative; bottom: " + (-500 + y) + "px; left: " + x + "px; transform: rotate(" + angle + "deg)'";

        var divStr = "<div class='star-ship'>";
        divStr += "<img id='user-" + userId + "' src='/Content/Arena/Images/" + shipName + ".png' " + style + " />";
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

                var userInfoDiv = "";

                for (var i = 0; i < data.length; i++) {
                    //userInfoDiv += "<div>" + data[i].Name + "[" + data[i].Position.X + "x" + data[i].Position.Y + "] [" + data[i].Id + "]</div>";
                    userInfoDiv += "<div>" + JSON.stringify(data[i]) + "</div>";
                }

                var usersDiv = $("#users");

                usersDiv.empty();
                usersDiv.append(userInfoDiv);


                $(".starships-arena").empty();


                for (var i = 0; i < data.length; i++) {
                    if (data[i].Id === window.gameController.user.id) {
                        window.gameController.user.position = {
                            x: data[i].Position.X,
                            y: data[i].Position.Y,
                            angle: data[i].Position.Angle
                        }
                    }

                    var starShip = window.gameController.getStarShipDiv("Arasari Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y, data[i].Position.Angle);

                    if (i === 1) {
                        starShip = window.gameController.getStarShipDiv("Butterfly Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y, data[i].Position.Angle);
                    }

                    if (i === 2) {
                        starShip = window.gameController.getStarShipDiv("Flamingo Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y, data[i].Position.Angle);
                    }

                    if (i === 3) {
                        starShip = window.gameController.getStarShipDiv("Piranha Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y, data[i].Position.Angle);
                    }

                    if (i === 4) {
                        starShip = window.gameController.getStarShipDiv("Skat Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y, data[i].Position.Angle);
                    }

                    $(".starships-arena").append(starShip);
                }
            },
            error: function (e) {
                //debugger;
            }
        });
    },
    move: function (userId) {

        var keyStateArray = [];

        if (window.gameController.keyState.Right === true) {
            keyStateArray.push("Right");
        }

        if (window.gameController.keyState.Left === true) {
            keyStateArray.push("Left");
        }

        if (window.gameController.keyState.Up === true) {
            keyStateArray.push("Up");
        }

        if (window.gameController.keyState.Down === true) {
            keyStateArray.push("Down");
        }

        debugger;
        var change = window.gameController.keyState.MouseAngle - window.gameController.user.position.angle;

        if (change < 0) {
            change *= -1;
        }

        if (change < 2) {

        } else {
            if (window.gameController.keyState.MouseAngle > window.gameController.user.position.angle) {
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



        //if (change > 180) {
        //    keyStateArray.push("ClockwiseRotation");
        //} else {
        //    keyStateArray.push("CounterclockwiseRotation");
        //}
        //keyStateArray.push("ClockwiseRotation");

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
    keyState: {
        Down: false,
        Up: false,
        Left: false,
        Right: false,
        MouseX: "0",
        MouseY: "0",
        MouseAngle: "0"
    },
    setupKeyListener: function () {

        $(document).keydown(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);

            if (keycode === 68) {
                window.gameController.keyState.Right = true;
            }
            if (keycode === 87) {
                window.gameController.keyState.Up = true;
            }
            if (keycode === 83) {
                window.gameController.keyState.Down = true;
            }
            if (keycode === 65) {
                window.gameController.keyState.Left = true;
            }
        });

        $(document).keyup(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);

            if (keycode === 68) {
                window.gameController.keyState.Right = false;
            }
            if (keycode === 87) {
                window.gameController.keyState.Up = false;
            }
            if (keycode === 83) {
                window.gameController.keyState.Down = false;
            }
            if (keycode === 65) {
                window.gameController.keyState.Left = false;
            }
        });

        window.setInterval(function () {
            $(".keystate-right").text('right ' + window.gameController.keyState.Right);
            $(".keystate-left").text('left ' + window.gameController.keyState.Left);
            $(".keystate-up").text('up ' + window.gameController.keyState.Up);
            //$(".keystate-down").text('down ' + JSON.stringify(window.gameController.keyState)); 
            $(".keystate-down").text('down ' + window.gameController.keyState.Down); 
        }, 20);
    },
    init: function () {
        window.setInterval(function () {

            var x1 = window.gameController.user.position.x + 25;
            var y1 = window.gameController.user.position.y - 25;

            var x2 = window.gameController.keyState.MouseX;
            var y2 = window.gameController.keyState.MouseY;

            var part1 = (y2 - y1);
            var part2 = (x2 - x1);

            var cof = part1 / part2;
            var angle = Math.atan(cof) * 57.2958;

            if (part2 < 0) {
                angle += 180;
            }

            if (part1 < 0 && part2 > 0) {
                angle += 360;
            }

            angle -= 90;
            angle = 360 - angle;

            while (angle > 360) {
                angle -= 360;
            }

            while (angle < 0) {
                angle += 360;
            }

            window.gameController.keyState.MouseAngle = angle;

            $("#user-angle").text("(" + angle + ") [" + x1 + ":" + y1 + "] [" + x2 + ":" + y2 + "]");
        }, 10);

        window.setInterval(function () {
            window.gameController.getUsers();
        }, 50);

        window.setInterval(function () {
            window.gameController.move(window.gameController.user.id);
        }, 10);

        window.gameController.setupKeyListener();

        $("#userNameLogin").click(function () {
            var input = $("#userNameInput")[0];

            if (input.value !== "") {
                window.gameController.addUser(input.value);

                window.gameController.hideLogin();
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

        $(".starships-arena").mousemove(function (event) {
            var offset = $(this).offset();
            var x = event.pageX - offset.left;
            var y = event.pageY - offset.top;

            window.gameController.keyState.MouseX = x;
            window.gameController.keyState.MouseY = 500 - y;

            $("#mouse-position").html("(X: " + x + ", Y: " + (500 - y) + ")");
        });
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