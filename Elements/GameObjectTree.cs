using Barjees.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// Tree of all game objects
    /// </summary>
    public class GameObjectTree
    {
        /// <summary>
        /// List of all game objects
        /// </summary>
        private List<GameObject> objects;
        /// <summary>
        /// List of all game objects
        /// </summary>
        public List<GameObject> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameObjectTree()
        {
            objects = new List<GameObject>();
        }
        
    }
}
