
$(document).ready(function () {

    var boxWidth = $('#hdnBoxWidth').val();
    var boxHeight = $('#hdnBoxHeight').val();
    var rows = $('#hdnRows').val();
    var cols = $('#hdnColumns').val();
    var imageWidth = $('#robotimg').width();
    var imageHeight = $('#robotimg').height();

    var standarErrorMessage = "Sorry could not execute command.";
    var robotNotPlacedMessage = "You need to place robot before issuing any command.";

    /*
     * This function creates the visual representation
     * of robot details that come from server side
     */
    function mapRobotWithImage(obj) {
        if (!obj.IsSuccess) {
            alert(obj.ErrorMessage);
            return;
        }

        var elem = document.getElementById("robotimg");
        var newLeft = obj.NewPosition.X * boxWidth;
        var newTop = obj.NewPosition.Y * boxHeight;

        //keep image inside the grid
        if (newLeft >= cols * boxWidth) {
            newLeft = (cols * boxWidth) - imageWidth; 
        }
       
        //make sure y coordinate doens't take robot out of box
        if (newTop >= rows * boxHeight) {
            newTop = (rows * boxHeight) - imageHeight;
        }
        //position our robot image based on calculated dimensions and angle
        elem.style.left = newLeft + 'px';
        elem.style.bottom = newTop + 'px';
        elem.style.transform = 'rotate(' + obj.NewPosition.Direction + 'deg)';
        $('#robotimg').show();
    }
    /*
     * This function initializes our robot when we call the Place command
     */
    function initRobot(data) {
        var obj = JSON.parse(data);
        $('#hdnRobotId').val(obj.RobotId);
        mapRobotWithImage(obj);
    }

    /*
     * This is function call after a MOVE command returns from server
     */
    function moveRobotImage(data) {
        var obj = JSON.parse(data);
        mapRobotWithImage(obj);
    }

    /*
    * This  function reports on our robot current position
    */
    function reportRobotPosition(data) {
        $('#pfeedback').html(JSON.stringify(data));
    }
    /*
    * This is makes api call to issue a Place command on our robot
    */
    function placeRobot() {

        var x = $('#txtX').val();
        var y = $('#txtY').val();
        var f = $('#slFace').val();

        //do some validation
        if (!$.isNumeric(x)) {
            alert('Please enter a valid X coordinate');
            return;
        }

        //do some validation
        if (!$.isNumeric(y)) {
            alert('Please enter a valid Y coordinate');
            return;
        }

        var json = '{"RobotId": "","Position":{"X":' + x + ',"Y":' + y + ',"Direction":"' + f + '" }}';
       
        $.ajax({
            type: "POST",
            url: "/api/command/place-robot",
            data: json,
            dataType: 'json',
            contentType: 'application/json',
            success: initRobot,
            error: function(err) {
                console.log(err);
                alert(standarErrorMessage);
            }
        });
    }

    /*
   * This is makes api call to issue a Move command on our robot
   */
    function moveRobot() {
        var robotId = $('#hdnRobotId').val();

        if (!robotId) {
            alert(robotNotPlacedMessage);
            return;
        }

        $.ajax({
            type: "GET",
            url: "/api/command/move-robot/" + robotId,
            success: moveRobotImage,
            error: function (err) {
                console.log(err);
                alert(standarErrorMessage);
            }
        });
    }

    /*
   * This is makes api call to issue a Left command on our robot
   */
    function turnRobotLeft() {
        var robotId = $('#hdnRobotId').val();

        if (!robotId) {
            alert(robotNotPlacedMessage);
            return;
        }

        $.ajax({
            type: "GET",
            url: "/api/command/turn-robot-left/" + robotId,
            success: moveRobotImage,
            error: function (err) {
                console.log(err);
                alert(standarErrorMessage);
            }
        });
    }

    /*
   * This is makes api call to issue a Right command on our robot
   */
    function turnRobotRight() {
        var robotId = $('#hdnRobotId').val();

        if (!robotId) {
            alert(robotNotPlacedMessage);
            return;
        }

        $.ajax({
            type: "GET",
            url: "/api/command/turn-robot-right/" + robotId,
            success: moveRobotImage,
            error: function (err) {
                console.log(err);
                alert(standarErrorMessage);
            }
        });
    }
    /*
   * This is makes api call to retrieve our robot current position
   */
    function robotReport() {
        var robotId = $('#hdnRobotId').val();

        if (!robotId) {
            alert(robotNotPlacedMessage);
            return;
        }

        $.ajax({
            type: "GET",
            url: "/api/command/robot-report/" + robotId,
            success: reportRobotPosition,
            error: function (err) {
                console.log(err);
                alert(standarErrorMessage);
            }
        });
    }

    /*
   * attach events to buttons
   */
    $("#btnPlace").on("click", placeRobot);
    $('#btnMove').on("click", moveRobot);
    $('#btnLeft').on("click", turnRobotLeft);
    $('#btnRight').on("click", turnRobotRight);
    $('#btnReport').on("click", robotReport);
});