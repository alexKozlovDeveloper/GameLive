window.BlocksAPiController = {
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
    clear: function() {
        this.$canvas.empty();
    },
    init: function (canvasSelector, width, height) {
        this.$canvas = $(canvasSelector);

        this.canvasWidth = width;
        this.canvasHeight = height;

        //TODO: delete
        this.$canvas.css("background-color", "black");

        this.$canvas.css("width", width + "px");
        this.$canvas.css("height", height + "px");
        this.$canvas.addClass("blocks-canvas");
    }
}