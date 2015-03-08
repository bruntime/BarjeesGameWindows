using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// Computer player class
    /// </summary>
    public class ComputerPlayer : Player
    {
        #region Constructor
	    /// <summary>
        /// Default constructor
        /// </summary>
        public ComputerPlayer()
        {
            PlayerType = Elements.PlayerType.Automatic;
            Route.SetPlayerRouteType(new NorthPlayerRouteType());
        }
 
	#endregion    
    }
}
