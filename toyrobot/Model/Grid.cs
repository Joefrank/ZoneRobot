
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace toyrobot.Model
{
    /// <summary>
    /// This class is the representation of the grid or table on which the robot will be moving
    /// It holds all possible positions as points/coordinates. With this, it is easy to validate
    /// Every position given to the robot 
    /// </summary>
    public class Grid
    {
        public int Xunits { get; }

        public int Yunits { get; }

        public List<Point> AllPositions { get; set; }

        public Grid(int xunits, int yunits)
        {
            Xunits = xunits;
            Yunits = yunits;
            Init();
        }

        public void Init()
        {
            AllPositions = new List<Point>();

            for (var x = 0; x <= Xunits; x++)
            {
                for (var y = 0; y <= Yunits; y++)
                {
                    AllPositions.Add(new Point(x, y));
                }
            }
        }

        public bool IsValidPosition(Position position)
        {
            return AllPositions.Any(x => x.X == position.X && x.Y == position.Y);
        }
    }
}
