using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using toyrobot.Enums;
using toyrobot.Model;

namespace toyrobot.desktop
{
    public partial class Form1 : Form
    {
        private int _originX;
        private int _originY;

        private const int GridWidth = 500;
        private const int GridHeight = 500;
        private const int WinWidth = 1000;
        private const int WinHeight = 800;
        private const int NoLines = 6;
        private const int GridUnits = 5;
        private const int CellWidth = 100;
        private const int GridLeft = 50;
        private const int GridTop = 50;
        private const int RobotImageWidth = 12;
        private const int RobotImageHeight = 12;

       
        private PictureBox _robotPicture;
        private readonly Robot _robot;
        private readonly MoveCommand _moveCommand;
        private readonly PlaceCommand _placeCommand;
        private readonly RotationCommand _rotateLeft;
        private readonly RotationCommand _rotateRight;
        private Grid _grid;

        public Form1()
        {
            InitializeComponent();
            _robot = new Robot();
            _grid = new Grid(GridUnits, GridUnits);
            _moveCommand = new MoveCommand(_grid);
            _placeCommand = new PlaceCommand(_grid);
            _rotateLeft = new RotationCommand("LEFT", RotationalDirection.Left);
            _rotateRight = new RotationCommand("RIGHT", RotationalDirection.Right);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            MinimizeBox = false;
            // Set the start position of the form to the center of the screen.
            StartPosition = FormStartPosition.CenterScreen;
            MaximumSize = new Size(WinWidth, WinHeight);

            var faces = Enum.GetValues(typeof(Face)).Cast<Face>();
            foreach (var f in faces)
            {
                Faces.Items.Add(f);
            }

        }

        private void BuildGrid()
        {
            var linePen = new Pen(Color.CornflowerBlue);
            var gx = CreateGraphics();
            gx.Clear(BackColor);

            for (var x = 0; x < NoLines; x++)
            {
                if (x == 0)
                {
                    _originX = GridLeft;
                    _originY = GridTop + GridHeight;
                }
                var verticalOffset = GridTop + (CellWidth * x);
                var horizontalOffset = GridLeft + (CellWidth * x);
                DrawLine(gx, linePen, new Point(GridLeft, verticalOffset), new Point(GridLeft + GridWidth, verticalOffset));
                DrawLine(gx, linePen, new Point(horizontalOffset, GridTop), new Point(horizontalOffset, GridTop + GridHeight));
            }

            linePen.Dispose();
        }

        private void DrawLine(Graphics g, Pen p, Point p1, Point p2)
        {
            g.DrawLine(p,p1, p2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BuildGrid();

            //Continues the paint of other elements and controls
            base.OnPaint(e);
        }

        /// <summary>
        /// Place robot at specific location and points it to direction Face
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Place(object sender, EventArgs e)
        {
            //validate X var
            if(!int.TryParse(txtX.Text, out var x))
            {
                MessageBox.Show(Properties.Resources.Error_Invalid_PositionX);
                return;
            }

            //validate Y var
            if (!int.TryParse(txtY.Text, out var y))
            {
                MessageBox.Show(Properties.Resources.Error_Invalid_PositionY);
                return;
            }

            //check face
            if (Faces.SelectedIndex < 0)
            {
                MessageBox.Show(Properties.Resources.Error_Invalid_Face);
                return;
            }

            var face = (Face)Faces.SelectedItem;

            var result = _placeCommand.Impact(_robot, new Position(x, y, face));

            if (!result.IsSuccess)
            {
                MessageBox.Show(Properties.Resources.Error_Invalid_PositionY);
                return;
            }

            //apply rotation and validation
            var image = RotateImage((int) face);
            
            var newPosition = new Point(_originX - (RobotImageWidth/ 2) + (x * CellWidth), _originY - (RobotImageHeight / 2) - (y* CellWidth));
            
            if (_robotPicture == null)
            {
                _robotPicture = new PictureBox
                {
                    Name = "pictureBox",
                    Size = new Size(RobotImageWidth, RobotImageHeight),
                    Location = newPosition,
                    Image = image
                };

                Controls.Add(_robotPicture);
            }
            else //just place in new position
            {
                _robotPicture.Location = newPosition;
                _robotPicture.Image = image;
            }
            //_robot.Position = new Position(x, y, face);
        }

        /// <summary>
        /// Moves robot to next unit in faced direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveByFace(object sender, EventArgs e)
        {
            if (!_robot.IsPositioned)
            {
                MessageBox.Show(Properties.Resources.Error_Robot_NotPlaced);
                return;
            }
            
            var result = _moveCommand.Impact(_robot);
            if (!result.IsSuccess)
            {
                MessageBox.Show(Properties.Resources.Error_Invalid_Move);
                return;
            }

            var x = result.NewPosition.X;
            var y = result.NewPosition.Y;

            _robotPicture.Location = new Point(_originX - (RobotImageWidth / 2) +
                                               (x * CellWidth), _originY - (RobotImageHeight / 2) - (y * CellWidth));
            
        }

        /// <summary>
        /// Rotates the robot image to the left.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TurnLeft(object sender, EventArgs e)
        {
            if (!_robot.IsPositioned)
            {
                MessageBox.Show(Properties.Resources.Error_Robot_NotPlaced);
                return;
            }

            var result = _rotateLeft.Impact(_robot); //run command on robot

            if (result.IsSuccess)
            {
                RefreshRobotOnScreen(); //refresh screen to see impact, validation not needed for rotational commands
                return;
            }
            //if we get here then there is a problem
            MessageBox.Show(Properties.Resources.Error_CouldNot_Rotate_Robot);
        }

        /// <summary>
        /// Rotates robot to the right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TurnRight(object sender, EventArgs e)
        {
            if (!_robot.IsPositioned)
            {
                MessageBox.Show(Properties.Resources.Error_Robot_NotPlaced);
                return;
            }

            var result = _rotateRight.Impact(_robot);//run command on robot

            if (result.IsSuccess)
            {
                RefreshRobotOnScreen(); //refresh screen to see impact, validation not needed for rotational commands
                return;
            }
            //if we get here then there is a problem
            MessageBox.Show(Properties.Resources.Error_CouldNot_Rotate_Robot);
        }

        /// <summary>
        /// Reports robot current position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportPostion(object sender, EventArgs e)
        {
            if (!_robot.IsPositioned)
            {
                MessageBox.Show(Properties.Resources.Error_Robot_NotPlaced);
                return;
            }

            lblReport.Text = _robot.Report();
        }

        #region util functions

        private Image RotateImage(float angle)
        {
            var image = Properties.Resources.robot_arrow;
            return RotateImage(image, angle);
        }

        private Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            var gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void RefreshRobotOnScreen()
        {
            var currentFace = (int)_robot.Position.Direction;
           
            _robotPicture.Image = RotateImage(currentFace);
        }

        #endregion

       
    }
}
