
namespace toyrobot.Model.Interfaces
{
    public interface ICommand
    {
        Grid Board { get; }

        string Name { get;}

        CommandFeedback Impact(Robot robot, Position position = null);
        
    }
}
