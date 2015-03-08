using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// The route of the player
    /// </summary>
    public class PlayerRoute
    {
        /// <summary>
        /// Linked list of cell nodes
        /// </summary>
        private LinkedList<Cell> cellsNodes;
        /// <summary>
        /// Linked list of cell nodes
        /// </summary>
        public LinkedList<Cell> CellNodes
        {
            get { return cellsNodes; }
            set { cellsNodes = value; }
        }
       
        /// <summary>
        /// The player route type
        /// </summary>
        protected PlayerRouteType routeType;
        /// <summary>
        /// The player route type
        /// </summary>
        public PlayerRouteType RouteType
        {
            get { return routeType; }            
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerRoute()
        {
            cellsNodes = new LinkedList<Cell>();
        }
        /// <summary>
        /// Set the route type
        /// </summary>
        /// <param name="prt"></param>
        public void SetPlayerRouteType(PlayerRouteType prt)
        {
            routeType = prt;           
            BuildNodes();
        }
        /// <summary>
        /// Build the cell nodes list
        /// </summary>
        private void BuildNodes()
        {
            cellsNodes.Clear();
            if (routeType == null)
                return;
            
            LinkedListNode<Cell> node = new LinkedListNode<Cell>(GameApp.Instance.Board.Cells[routeType.Map[0]]);

            for (int i = 0; i < routeType.Map.Length; i++ )
            {
                if(i==0)
                {
                    cellsNodes.AddFirst(node);        
                }
                else
                {
                    LinkedListNode<Cell> newNode = new LinkedListNode<Cell>(GameApp.Instance.Board.Cells[routeType.Map[i]]);
                    CellNodes.AddAfter(node, newNode);
                    node = newNode;
                }
            }
        }

    }
}
