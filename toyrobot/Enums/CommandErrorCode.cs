
using System.ComponentModel;

namespace toyrobot.Enums
{
    public enum CommandErrorCode
    {
        [Description("")]
        None,
        [Description("You need to place robot before issuing any command.")]
        RobotNotPositioned,
        [Description("Sorry could not rotate robot.")]
        RotationException,
        [Description("Attempt to move robot out of bounds.")]
        MoveOutOfBounds,
        [Description("You must place robot with the bound specified.")]
        PositionOutOfBounds
    }
}
