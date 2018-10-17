using toyrobot.Enums;
using toyrobot.Model.Interfaces;

namespace toyrobot.Model
{
    /// <summary>
    /// This class is used to issue commands to rotate robot in different directions
    /// </summary>
    public class RotationCommand : IRotationCommand
    {
        public string Name { get; set; }

        public RotationalDirection Direction { get; set; }

        public RotationCommand(string name, RotationalDirection direction)
        {
            Name = name;
            Direction = direction;
        }

        /// <summary>
        /// This function actually executes the command by rotating the robot based on given direction
        /// Position doesn't need to validated as long as the robot is already placed on the Grid/Board
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public virtual CommandFeedback Impact(Robot robot)
        {
            //robot must be positioned first
            if (!robot.IsPositioned)
            {
                return new CommandFeedback
                {
                    IsSuccess = false,
                    NewPosition = robot.Position,
                    ErrorCode = CommandErrorCode.RobotNotPositioned
                };
            }

            var result = robot.ChangeDirection(Direction);

            var feeback = new CommandFeedback
            {
                RobotId = robot.RobotId,
                IsSuccess = result,
                NewPosition = robot.Position
            };

            if (!result)
                feeback.ErrorCode = CommandErrorCode.RotationException;

            return feeback;
        }
    }
}
