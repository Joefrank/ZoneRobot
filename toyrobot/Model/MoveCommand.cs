using toyrobot.Enums;
using toyrobot.Model.Interfaces;

namespace toyrobot.Model
{
    /// <summary>
    /// This class is one implement of the ICommand
    /// It implements the Move Command
    /// </summary>
    public class MoveCommand : ICommand
    {
        public string Name => "MOVE";

        public Grid Board { get;}

        public MoveCommand(Grid board)
        {
            Board = board;
        }

        /// <summary>
        /// This function is the one that move our robot in the direction it is facing
        /// which implies robot must be placed in a specific position first.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual CommandFeedback Impact(Robot robot, Position position = null)
        {
            //robot must be position first before moving it
            if (!robot.IsPositioned)
            {
                return new CommandFeedback
                {
                    IsSuccess = false,
                    NewPosition = robot.Position,
                    ErrorCode = CommandErrorCode.RobotNotPositioned
                };
            }

            //let's make a clone of our robot so we don't move the original to the wrong postion
            var originalPosition = (Position)robot.Position.Clone();

            //make robot has been positioned
            switch (originalPosition.Direction)
            {
                case Face.North:
                    originalPosition.Y++;
                    break;
                case Face.East:
                    originalPosition.X++;
                    break;
                case Face.South:
                    originalPosition.Y--;
                    break;
                case Face.West:
                    originalPosition.X--;
                    break;
            }

            //validate move
            var success = Board.IsValidPosition(originalPosition);

            if (success)
            {
                robot.Position = originalPosition;
            }

            var feedback = new CommandFeedback
            {
                RobotId = robot.RobotId,
                IsSuccess = success,
                NewPosition = robot.Position
            };

            if (!feedback.IsSuccess)
                feedback.ErrorCode = CommandErrorCode.MoveOutOfBounds;

            return feedback;
        }
    }
}
