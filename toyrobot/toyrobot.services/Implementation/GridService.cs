
using toyrobot.Model;
using toyrobot.services.Interfaces;

namespace toyrobot.services.Implementation
{
    /// <summary>
    /// This service creates our Grid/Board.
    /// Grid provides valid positions for our robot 
    /// </summary>
    public class GridService : IGridService
    {
        public Grid CreateGrid(int xunits, int yunits)
        {
            return new Grid(xunits, yunits);
        }
    }
}
