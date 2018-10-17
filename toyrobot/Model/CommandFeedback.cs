using System;
using toyrobot.Enums;
using toyrobot.utils;

namespace toyrobot.Model
{
    /// <summary>
    /// this class is used to send feedback to application that issues commands against robot.
    /// </summary>
    public class CommandFeedback
    {
        public Guid? RobotId { get; set; }

        private string _message;

        public bool IsSuccess { get; set; }

        public CommandErrorCode ErrorCode { get; set; }

        public string ErrorMessage
        {
            set => _message = value;
            get => string.IsNullOrEmpty(_message) ? EnumExtensions.GetEnumDescription(ErrorCode)
                : _message;
        }

        public Position NewPosition { get; set; }
    }

}
