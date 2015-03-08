using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// User player class
    /// </summary>
    public class UserPlayer : Player
    {
        #region Constructor
		/// <summary>
        /// Default Constructor
        /// </summary>
        public UserPlayer()
            :base()
        {
            PlayerType = Elements.PlayerType.Manual;
            Route.SetPlayerRouteType(new SouthPlayerRouteType());
        }
 
	    #endregion    
    }
}
