using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using toyrobot.Model;
using toyrobot.services.Implementation;
using toyrobot.services.Interfaces;

namespace toyrobot.test
{
    [TestClass]
    public class RobotServiceTest
    {
        private Mock<ICacheService> _mockCache;
        private IRobotService _robotService;

        [TestInitialize]
        public void SetupTests()
        {
            _mockCache = new Mock<ICacheService>();
            _mockCache.Setup(x => x.Get<Robot>(It.IsAny<string>())).Returns(new Robot());
            _robotService = new RobotService(_mockCache.Object);
        }

        [TestMethod]
        public void CanCreateRobot()
        {
            var robot = _robotService.CreateRobot(); 
            Assert.IsTrue(robot.RobotId != Guid.Empty);
        }

        [TestMethod]
        public void CanPersistAndRetrieveRobotFromCache()
        {
            var robot = _robotService.CreateRobot();

            _robotService.PersistRobot(robot);

            var savedRobot = _robotService.GetRobot(robot.RobotId.ToString());

            Assert.IsNotNull(savedRobot);
        }

       
    }
}
