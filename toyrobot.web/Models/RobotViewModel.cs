using System;
using toyrobot.Model;

namespace toyrobot.web.Models
{
    /// <summary>
    /// Model used to pass robot positioning values from client to server side.
    /// </summary>
    public class RobotViewModel
    {
        public Guid? RobotId { get; set; }

        public Position Position { get; set; }
    }
}