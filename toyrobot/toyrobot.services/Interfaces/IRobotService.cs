
using toyrobot.Model;

namespace toyrobot.services.Interfaces
{
    public interface IRobotService
    {
        Robot GetRobot(string key);

        Robot CreateRobot();

        void PersistRobot(Robot robot);
    }
}
