
using toyrobot.Enums;

namespace toyrobot.Model.Interfaces
{
    public interface IRotationCommand
    {
        string Name { get; }
        
        RotationalDirection Direction { get; }

        CommandFeedback Impact(Robot robot);

    }
}
