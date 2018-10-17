using toyrobot.Enums;
using toyrobot.Model.Interfaces;

namespace toyrobot.Model
{
    /// <summary>
    /// This is the first command to run. We must place the robot somewhere on the Grid/Board before issuing any other command
    /// </summary>
    public class PlaceCommand : ICommand
    {
        public string Name => "PLACE";

        public Grid Board { get; }

        public PlaceCommand(Grid board)
        {
            Board = board;
        }

        /// <summary>
        /// This is the function that actually places the robot on the Grid/Board
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="position">intended position for robot</param>
        /// <returns></returns>
        public virtual CommandFeedback Impact(Robot robot, Position position)
        {
            //validate new position that we are attempting to place robot
            if (position == null || !Board.IsValidPosition(position))
            {
                return new CommandFeedback
                {
                    IsSuccess =  false,
                    ErrorCode = (position != null)? CommandErrorCode.PositionOutOfBounds : CommandErrorCode.RobotNotPositioned,
                    NewPosition = robot.Position
                };
            }

            robot.Position = position;

            return new CommandFeedback
            {
                RobotId = robot.RobotId,
                IsSuccess = true,
                NewPosition = robot.Position
            };
        }
    }
}
