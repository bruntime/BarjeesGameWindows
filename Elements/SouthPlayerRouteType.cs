using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// The south player route type
    /// </summary>
    public sealed class SouthPlayerRouteType : PlayerRouteType
    {
        /// <summary>
        /// Array of cell IDs of south route
        /// </summary>
        private readonly int[] southMap = new int[] { 8, 20, 32, 44, 56, 68, 80, 92, 91, 79, 67, 55, 43, 31, 19, 7, 6, 18, 30, 42, 54, 66, 78, 90, 89, 88, 76, 64, 52, 40, 28, 16, 4, 3, 15, 27, 39, 51, 63, 75, 87, 86, 85, 73, 61, 49, 37, 25, 13, 1, 12, 24, 36, 48, 60, 72, 84, 96, 95, 94, 82, 70, 58, 46, 34, 22, 10, 9, 21, 33, 45, 57, 69, 81, 93, 92, 80, 68, 56, 44, 32, 20, 8, 98 };
        /// <summary>
        /// The south route coordinates map
        /// </summary>
        public override int[] Map
        {
            get
            {
                return southMap;
            }
        }
    }
}
