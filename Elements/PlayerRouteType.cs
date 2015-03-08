using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// Defines which route the user is on
    /// </summary>
    public abstract class PlayerRouteType
    {
        /// <summary>
        /// Returns the map coordinates of the cells of this route
        /// </summary>
        public virtual int[] Map
        {
            get { return null; }
        }
    }
}
