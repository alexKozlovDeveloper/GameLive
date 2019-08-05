window.gameController = {
    user: {
        id: "",
        name: ""
    },
    clearUser: function() {
        window.gameController.user.id = "";
        window.gameController.user.name = "";

        localStorage.setItem('user', null);
    },
    getStarShipDiv: function (shipName, userId, x, y) {
        var style = "style = 'position: relative; bottom: " + (-500 + y) + "px; left: " + x + "px;'";

        var divStr = "<div class='star-ship'>";
        divStr += "<img id='" + userId + "' src='/Content/Arena/Images/" + shipName + ".png' " + style + " />";
        divStr += "</div>";
        return divStr;
    },
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
                //debugger;

                //window.gameController.currentUserId = data[0].Id;

                var userInfoDiv = "";

                for (var i = 0; i < data.length; i++) {
                    userInfoDiv += "<div>" + data[i].Name + "[" + data[i].Position.X + "x" + data[i].Position.Y + "] [" + data[i].Id + "]</div>";
                }

                var usersDiv = $("#users");

                usersDiv.empty();
                usersDiv.append(userInfoDiv);


                $(".starships-arena").empty();


                //position: relative;
                //margin - top: 100px;
                //margin - left: 100px;

                for (var i = 0; i < data.length; i++) {
                    var starShip = window.gameController.getStarShipDiv("Arasari Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y);

                    if (i === 1) {
                        starShip = window.gameController.getStarShipDiv("Butterfly Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y);
                    }

                    if (i === 2) {
                        starShip = window.gameController.getStarShipDiv("Flamingo Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y);
                    }

                    if (i === 3) {
                        starShip = window.gameController.getStarShipDiv("Piranha Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y);
                    }

                    if (i === 4) {
                        starShip = window.gameController.getStarShipDiv("Skat Star Ship", data[i].Id, data[i].Position.X, data[i].Position.Y);
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
        //debugger;

        var keyStateArray = [];

        if ($(".keystate-right")[0].innerText === "right true") {
            keyStateArray.push("Right");
        }

        if ($(".keystate-left")[0].innerText === "left true") {
            keyStateArray.push("Left");
        }

        if ($(".keystate-up")[0].innerText === "up true") {
            keyStateArray.push("Up");
        }

        if ($(".keystate-down")[0].innerText === "down true") {
            keyStateArray.push("Down");
        }

        var keyState = keyStateArray.join();

        if (keyStateArray.length === 0) {
            keyState = "";
        }

        $("#keyState").text(keyState);
        //

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
        'Down': false,
        'Up': false,
        'Left': false,
        'Right': false
    },
    setupKeyListener: function () {
        //debugger;
        //window.keyState = {
        //    'Down': false,
        //    'Up': false,
        //    'Left': false,
        //    'Right': false
        //}

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
            //debugger;
            $(".keystate-right").text('right ' + window.gameController.keyState.Right);
            $(".keystate-left").text('left ' + window.gameController.keyState.Left);
            $(".keystate-up").text('up ' + window.gameController.keyState.Up);
            $(".keystate-down").text('down ' + window.gameController.keyState.Down);
        }, 20);
    },
    init: function () {
        window.setInterval(function () {
            window.gameController.getUsers();
        }, 50);
 
        window.setInterval(function () {
            window.gameController.move(window.gameController.user.id);
        }, 50);

        window.gameController.setupKeyListener();
    }
};