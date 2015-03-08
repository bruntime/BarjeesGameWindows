using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Represents the roll result
    /// </summary>
    public class RollResult
    {
        /// <summary>
        /// List of moves inside the roll result
        /// </summary>
        public List<PawnMove> Moves { get; set; }
        /// <summary>
        /// The total value of moves in the roll result regardless the usage of moves
        /// </summary>
        public int TotalValue
        {
            get { return Moves.Sum(x => x.Value); }
        }
        /// <summary>
        /// The total value of moves that is not used
        /// </summary>
        public int ActiveValue
        {
            get { return Moves.Where(x => !x.IsUsed).Sum(x => x.Value); }
        }
        /// <summary>
        /// Returns true of the moves include 1
        /// </summary>
        public bool IsKhal
        {
            get { return Moves.Count(x => x.Value == 1 && !x.IsUsed) > 0; }
        }
        /// <summary>
        /// Checks if the roll result is used or not
        /// </summary>
        public bool IsUsed
        {
            get
            {
                return (ActiveValue == 0);
            }
        }
        /// <summary>
        /// Returns true if roll result is Daast, Banj, Baraa or Shakkeh
        /// </summary>
        public bool IsRepeatable
        {
            get 
            {
                return TotalValue > 4;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rollValue"></param>
        public RollResult(int rollValue)
        {
            Moves = new List<PawnMove>();
            CreateMoves(rollValue);
        }

        /// <summary>
        /// Create moves based on the roll result
        /// </summary>
        /// <param name="rollValue"></param>
        private void CreateMoves(int rollValue)
        {
            switch(rollValue)
            {
                case 2: // Duaa
                    Moves.Add(new PawnMove() { Parent = this, Value = 2 });
                    break;
                case 3: // Three
                    Moves.Add(new PawnMove() { Parent = this, Value = 3 });
                    break;
                case 4: // Four
                    Moves.Add(new PawnMove() { Parent = this, Value = 4 });
                    break;
                case 6: // Shakkeh
                    Moves.Add(new PawnMove() { Parent = this, Value = 6 });
                    break;
                case 11: // Daast
                    Moves.Add(new PawnMove() { Parent = this, Value = 10 });
                    Moves.Add(new PawnMove() { Parent = this, Value = 1 });
                    break;
                case 12: // Baraa
                    Moves.Add(new PawnMove() { Parent = this, Value = 12 });
                    break;
                case 26: // Banj
                    Moves.Add(new PawnMove() { Parent = this, Value = 25 });
                    Moves.Add(new PawnMove() { Parent = this, Value = 1 });
                    break;
            }
        }

        /// <summary>
        /// Convert the roll result to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string name = null;

            switch(TotalValue)
            {
                case 2:
                    name = "Duaa/" + ActiveValue.ToString();
                    break;
                case 3:
                    name = "Three/" + ActiveValue.ToString();
                    break;
                case 4:
                    name = "Four/" + ActiveValue.ToString();
                    break;
                case 6:
                    name = "Shakkeh/" + ActiveValue.ToString();
                    break;
                case 11:
                    name = "Daast/" + ActiveValue.ToString();
                    break;
                case 12:
                    name = "Baraa/" + ActiveValue.ToString();
                    break;
                case 26:
                    name = "Banj/" + ActiveValue.ToString();
                    break;
            }

            return name;
        }
    }
}
