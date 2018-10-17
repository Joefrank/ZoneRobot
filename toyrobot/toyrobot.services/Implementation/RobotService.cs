
using System;
using toyrobot.Model;
using toyrobot.services.Interfaces;

namespace toyrobot.services.Implementation
{
    /// <summary>
    /// This is the service used to initialise and manipulate our robot
    /// </summary>
    public class RobotService : IRobotService
    {
        private ICacheService CacheService { get; set; }

        public RobotService(ICacheService cacheService)
        {
            CacheService = cacheService;
        }

        /// <summary>
        /// Attempts to retrieve our robot object from Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Robot GetRobot(string key)
        {
            return CacheService.Get<Robot>(key);
        }

        /// <summary>
        /// Initialises our robot and puts it in the cache
        /// </summary>
        /// <returns></returns>
        public Robot CreateRobot()
        {
            var robot = new Robot {RobotId = Guid.NewGuid()};

            CacheService.Add(robot, robot.RobotId.ToString());

            return robot;
        }

        /// <summary>
        /// Updates robot object in the cache
        /// </summary>
        /// <param name="robot"></param>
        public void PersistRobot(Robot robot)
        {
            CacheService.Update(robot, robot.RobotId.ToString());
        }
    }
}
