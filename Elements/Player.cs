using Barjees.Commands;
using Barjees.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{

    /// <summary>
    /// Represents the player
    /// </summary>
    public abstract class Player : GameObject
    {
        /// <summary>
        /// The player route
        /// </summary>
        private PlayerRoute route;
        /// <summary>
        /// The player route
        /// </summary>
        public PlayerRoute Route
        {
            get { return route; }
            set { route = value; }
        }

        /// <summary>
        /// Player name
        /// </summary>
        private string name;
        /// <summary>
        /// Player name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Player type
        /// </summary>
        private PlayerType playerType;
        /// <summary>
        /// Player type
        /// </summary>
        public PlayerType PlayerType
        {
            get { return playerType; }
            set { playerType = value; }
        }
        /// <summary>
        /// Player pawns
        /// </summary>
        private Pawn[] pawns;
        /// <summary>
        /// Player pawns
        /// </summary>
        public Pawn[] Pawns
        {
            get { return pawns; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player()
        {
            Route = new PlayerRoute();
        }

        /// <summary>
        /// Create player four pawns
        /// </summary>
        public void CreatePawns()
        {
            pawns = new Pawn[] {
                GameObject.CreateGameObject<Pawn>(this),
                GameObject.CreateGameObject<Pawn>(this),
                GameObject.CreateGameObject<Pawn>(this),
                GameObject.CreateGameObject<Pawn>(this)
            };
            foreach (var p in pawns)
                p.IsLive = false;
        }

        /// <summary>
        /// Get the player brush
        /// </summary>
        /// <returns></returns>
        public Brush GetBrush()
        {
            if (this is UserPlayer)
            {
                return Brushes.Blue;
            }
            else
                return Brushes.Red;
        }

        /// <summary>
        /// Checks if the player can still play using the remaining unused moves
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CanPlay(CommandContext context)
        {
            bool result = false;
            foreach(var pawn in Pawns)
            {
                if (pawn.CanPlay(context))
                    result = true;
            }

            return result;
        }
        
    }
}
