using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using toyrobot.Enums;
using toyrobot.Model;
using toyrobot.web.Models;
using toyrobot.services.Interfaces;

namespace toyrobot.web.Controllers
{
    /// <summary>
    /// Api with custom routes is used to receive commands from client in a form of REST calls
    /// </summary>
    [System.Web.Http.RoutePrefix("api/command")]
    public class CommandController : ApiController
    {
        public IRobotService RobotService => DependencyResolver.Current.GetService<IRobotService>();
        public IGridService GridService => DependencyResolver.Current.GetService<IGridService>();

        private readonly MoveCommand _moveCommand;
        private readonly PlaceCommand _placeCommand;
        private readonly RotationCommand _rotateLeft;
        private readonly RotationCommand _rotateRight;
      
        private const int GridUnitsX = 5;
        private const int GridUnitsY = 5;

        public CommandController()
        {
            var grid = GridService.CreateGrid(GridUnitsX, GridUnitsY);
            _moveCommand = new MoveCommand(grid);
            _placeCommand = new PlaceCommand(grid);
            _rotateLeft = new RotationCommand("LEFT", RotationalDirection.Left);
            _rotateRight = new RotationCommand("RIGHT", RotationalDirection.Right);
        }

        /// <summary>
        /// handles our Place command
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("place-robot")]
        public IHttpActionResult PlaceRobot(RobotViewModel model)
        {
            try
            {
                //do validation first here.
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    var feedback= new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Data:" + message
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                Robot robot = null;

                //check if we have robot id
                if (model.RobotId != null)
                {
                    robot = RobotService.GetRobot(model.RobotId.ToString());
                }
                //create the robot if it doesn't exist
                if(model.RobotId == null || robot == null)
                {
                    robot = RobotService.CreateRobot();
                }
               
                var result = _placeCommand.Impact(robot, 
                    new Position(model.Position.X, model.Position.Y, model.Position.Direction));

                if(result.IsSuccess)
                    RobotService.PersistRobot(robot);
                
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Handles our Move command
        /// </summary>
        /// <param name="robotId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("move-robot/{robotId}")]
        public IHttpActionResult MoveRobot(string robotId)
        {
            try
            {
                //do validation first here.
                if (string.IsNullOrEmpty(robotId))
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    var feedback = new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorMessage = message
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                var robot = RobotService.GetRobot(robotId); 

                //if we cannot find robot, we need to return error message.
                if (robot == null)
                {
                    var feedback = new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorCode = CommandErrorCode.RobotNotPositioned
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                var result = _moveCommand.Impact(robot);

                if (result.IsSuccess)
                    RobotService.PersistRobot(robot);

                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Handles our Left command
        /// </summary>
        /// <param name="robotId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("turn-robot-left/{robotId}")]
        public IHttpActionResult TurnRobotLeft(string robotId)
        {
            return TurnRobot(robotId, RotationalDirection.Left);
        }

        /// <summary>
        /// Handles our Right command
        /// </summary>
        /// <param name="robotId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("turn-robot-right/{robotId}")]
        public IHttpActionResult TurnRobotRight(string robotId)
        {
            return TurnRobot(robotId, RotationalDirection.Right);
        }

        /// <summary>
        /// Handles the Report command
        /// </summary>
        /// <param name="robotId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("robot-report/{robotId}")]
        public IHttpActionResult RobotReport(string robotId)
        {
            try
            {
                //do validation first here.
                if (string.IsNullOrEmpty(robotId))
                {
                    var feedback = new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorMessage = "Robot identifier not provided."
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                var robot = RobotService.GetRobot(robotId);

                //if we cannot find robot, we need to return error message.
                if (robot == null)
                {
                    var feedback = new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorCode = CommandErrorCode.RobotNotPositioned
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                return Ok(robot.Report());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Util function for Right and Left commands
        /// </summary>
        /// <param name="robotId"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private IHttpActionResult TurnRobot(string robotId, RotationalDirection direction)
        { 
            try
            {
                //do validation first here.
                if (string.IsNullOrEmpty(robotId))
                {
                    var feedback = new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorCode = CommandErrorCode.RobotNotPositioned
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                var robot = RobotService.GetRobot(robotId);

                //if we cannot find robot, we need to return error message.
                if (robot == null)
                {
                    var feedback = new CommandFeedback
                    {
                        IsSuccess = false,
                        ErrorCode = CommandErrorCode.RobotNotPositioned
                    };
                    return Ok(JsonConvert.SerializeObject(feedback));
                }

                CommandFeedback result;

                if(direction == RotationalDirection.Left)
                    result = _rotateLeft.Impact(robot);
                else if (direction == RotationalDirection.Right)
                    result = _rotateRight.Impact(robot);
                else
                {
                    throw new Exception("Invalid direction/Face provided for robot move.");
                }

                if (result.IsSuccess)
                    RobotService.PersistRobot(robot);

                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
