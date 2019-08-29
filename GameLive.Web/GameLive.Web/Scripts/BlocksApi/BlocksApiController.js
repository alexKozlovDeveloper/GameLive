window.BlocksAPiController = {
    me: null,
    $canvas: null,
    canvasWidth: null,
    canvasHeight: null,
    getStatisticsDiv: function(userName, killCount, deadCount) {
    },
    addNewBlock: function (block) {


        //top: 25px;
        //left: 25px;
        //width: 100px;
        //height: 100px;
        //background - image: url(../../ Content / Arena / Images / Flamingo - Star - Ship.png);

        debugger;

        var blockDiv = "<div id=\"" + block.id + "\"></div>";

        this.me.$canvas.append(blockDiv);

        var $blockDiv = $("#" + block.id);
        $blockDiv.addClass("blocks-block");

        $blockDiv.css("width", block.width + "px");
        $blockDiv.css("height", block.height + "px");

        $blockDiv.css("top", block.y + "px");
        $blockDiv.css("left", block.x + "px");

        $blockDiv.css("background-image", "url(" + block.image + ")");
        $blockDiv.css("transform", "rotate(" + block.angle + "deg)");
        //var style = "style = 'position: relative; bottom: 10px; left: 10px; transform: rotate(15deg)'";

        //var divStr = "<div class='star-ship'>";
        //divStr += "<img src='/Content/Arena/Images/Piranha Star Ship.png' " + style + " />";
        //divStr += "</div>";

        //var blockDiv = "<div id=\"" + blockId + "\"><img id=\"" + blockId + "_img\" src='/Content/Arena/Images/Piranha Star Ship.png'/></div>";

        //this.me.$canvas.append(blockDiv);

        //var $blockDiv = $("#" + blockId);
        //$blockDiv.addClass("block-div");

        //var $blockDivImg = $("#" + blockId + "_img");

        //$blockDivImg.css("position", "relative");

        //$blockDivImg.css("width", "50px");
        //$blockDivImg.css("height", "50px");

        //$blockDivImg.css("bottom", "25px");
        //$blockDivImg.css("left", "25px");

        //$blockDivImg.css("background-color", "white");

        //$blockDivImg.css("transform", "rotate(15deg)");
    },
    init: function (canvasSelector, width, height) {

        debugger;

        window.BlocksAPiController.me = window.BlocksAPiController;

        this.me.$canvas = $(canvasSelector);

        this.me.$canvas.css("background-color", "black");
        this.me.$canvas.css("width", width + "px");
        this.me.$canvas.css("height", height + "px");
        this.me.$canvas.addClass("blocks-canvas");
    }
}