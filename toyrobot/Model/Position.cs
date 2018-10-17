
using System;
using System.ComponentModel.DataAnnotations;
using toyrobot.Enums;

namespace toyrobot.Model
{
    /// <summary>
    /// This class holds details of the current or intended position of our robot
    /// </summary>
    public class Position : ICloneable
    {
        [Required(ErrorMessage = "You need to provide X coordinate for robot.")]
        public int X { get; set; }
        [Required(ErrorMessage = "You need to provide Y coordinate for robot.")]
        public int Y { get; set; }
        [Required(ErrorMessage = "You need to provide a Face Direction for robot.")]
        public Face Direction { get; set; }

        public Position(int x, int y, Face dir)
        {
            X = x;
            Y = y;
            Direction = dir;
        }
        
        public virtual object Clone() { return new Position(X, Y, Direction); }
    }
}
