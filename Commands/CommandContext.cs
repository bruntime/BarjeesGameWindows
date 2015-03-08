using Barjees.Common;
using Barjees.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Commands
{
    /// <summary>
    /// The context of command
    /// </summary>
    public class CommandContext
    {
        /// <summary>
        /// Current turn player
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Returns true if the the current player is a user
        /// </summary>
        public bool IsManual
        {
            get { return Player is UserPlayer; }
        }
        /// <summary>
        /// Returns true if the current player is computer
        /// </summary>
        public bool IsAutomatic
        {
            get { return Player is ComputerPlayer; }
        }
        /// <summary>
        /// List of roll results
        /// </summary>
        public List<RollResult> RollResults { get; set; }
        /// <summary>
        /// Gets or sets the roll again status based on last roll result
        /// </summary>
        public bool RollAgain { get; set; }
        /// <summary>
        /// A dictionary lists the possible moves per pawn
        /// </summary>
        public Dictionary<int, List<PawnMove>> PossibleMoves { get; set; }

        /// <summary>
        /// Priorities for each pawn to play with
        /// </summary>
        public Dictionary<int, int> PriorityPawns { get; set; }

        /// <summary>
        /// The selected pawn to play with
        /// </summary>
        public Pawn SelectedPawn { get; set; }

        /// <summary>
        /// Selected cell to move the selected pawn to
        /// </summary>
        public Cell SelectedCell { get; set; }

        /// <summary>
        /// Possible cells that the selected pawn can use as destination
        /// </summary>
        public List<Cell> PossibleCells { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CommandContext()
        {
            RollResults = new List<RollResult>();
            PossibleMoves = new Dictionary<int, List<PawnMove>>();
            PriorityPawns = new Dictionary<int, int>();
            PossibleCells = new List<Cell>();
            RollAgain = false;
        }
    }
}
