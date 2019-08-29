window.BlocksAPiController = {
    me: null,
    $canvas: null,
    canvasWidth: null,
    canvasHeight: null,
    addNewBlock: function (block) {
        this.addNewBlockRec(block, this.$canvas, 2);
    },
    addNewBlockRec: function (block, $parent, zIndex) {

        var blockDiv = "<div id=\"" + block.id + "\"></div>";

        $parent.append(blockDiv);

        var $blockDiv = $("#" + block.id);
        $blockDiv.addClass("blocks-block");

        $blockDiv.css("width", block.width + "px");
        $blockDiv.css("height", block.height + "px");

        $blockDiv.css("top", block.y + "px");
        $blockDiv.css("left", block.x + "px");

        $blockDiv.css("z-index", zIndex);

        $blockDiv.css("background-image", "url(" + block.image + ")");
        $blockDiv.css("transform", "rotate(" + block.angle + "deg)");

        if (block.children !== null && block.children !== undefined) {
            for (var i = 0; i < block.children.length; i++) {
                this.addNewBlockRec(block.children[i], $blockDiv, zIndex + 1);
            }
        }
    },
    init: function (canvasSelector, width, height) {

        window.BlocksAPiController.me = window.BlocksAPiController;

        this.me.$canvas = $(canvasSelector);

        this.me.canvasWidth = width;
        this.me.canvasHeight = height;

        this.me.$canvas.css("background-color", "black");
        this.me.$canvas.css("width", width + "px");
        this.me.$canvas.css("height", height + "px");
        this.me.$canvas.addClass("blocks-canvas");
    }
}