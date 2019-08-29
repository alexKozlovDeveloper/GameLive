window.KeyStateController = {
    me: null,
    keyState: {
        Down: false,
        Up: false,
        Left: false,
        Right: false,
        IsAttack: false,
        MouseX: "0",
        MouseY: "0",
        //MouseAngle: "0"
    },
    setupKeyListener: function () {

        $(document).keydown(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);

            if (keycode === 68) {
                window.KeyStateController.keyState.Right = true;
            }
            if (keycode === 87) {
                window.KeyStateController.keyState.Up = true;
            }
            if (keycode === 83) {
                window.KeyStateController.keyState.Down = true;
            }
            if (keycode === 65) {
                window.KeyStateController.keyState.Left = true;
            }
        });

        $(document).keyup(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);

            if (keycode === 68) {
                window.KeyStateController.keyState.Right = false;
            }
            if (keycode === 87) {
                window.KeyStateController.keyState.Up = false;
            }
            if (keycode === 83) {
                window.KeyStateController.keyState.Down = false;
            }
            if (keycode === 65) {
                window.KeyStateController.keyState.Left = false;
            }
        });

        $(document).mousedown(function () {
            window.KeyStateController.keyState.IsAttack = true;
        }).mouseup(function () {
            window.KeyStateController.keyState.IsAttack = true;
        });

        $(".starships-arena").mousemove(function (event) {
            var offset = $(this).offset();
            var x = event.pageX - offset.left;
            var y = event.pageY - offset.top;
            debugger;
            window.KeyStateController.keyState.MouseX = x;
            window.KeyStateController.keyState.MouseY = 500 - y;
        });
    },
    GetAngle: function (x, y) {

        var x1 = x;
        var y1 = y;

        var x2 = window.KeyStateController.keyState.MouseX;
        var y2 = window.KeyStateController.keyState.MouseY;

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

        return angle;
    }
}

window.KeyStateController.me = window.KeyStateController;

//window.gameController.setupKeyListener();