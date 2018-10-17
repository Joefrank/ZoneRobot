
using System;
using toyrobot.Enums;

namespace toyrobot.Model
{
    /// <summary>
    /// This is the key class of the application
    /// It encapsulates functionality of our robot
    /// </summary>
    public class Robot
    {
        public Guid RobotId { get; set; }

        public bool IsPositioned => Position != null;

        public Position Position { get; set; }

        public Robot()
        {
            RobotId = Guid.NewGuid();
        }

        /// <summary>
        /// this changes the direction in which robot is facing
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool ChangeDirection(RotationalDirection direction)
        {
            if (Position == null)
                return false;

            Position.Direction = (Face)(((int)Position.Direction + (int)direction) % 360);
            return true;
        }

        /// <summary>
        /// Returns robot coordinates in a string format
        /// </summary>
        /// <returns></returns>
        public virtual string Report()
        {
            if (!IsPositioned)
                return "";

            var retVal = $"Output: {Position.X},{Position.Y},{Position.Direction}";

            //reset this robot position
            Position = null;

            return retVal;
        }
    }
}
