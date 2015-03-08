using Barjees.Common;
using Barjees.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// Board cell class
    /// </summary>
    public sealed class Cell : VisualGameObject
    {
        /// <summary>
        /// Returns the player occupying the cell 
        /// </summary>
        public Player Owner
        {
            get
            {
                if (OccupiedBy.Count() == 0)
                    return null;
                else
                    return OccupiedBy[0].Owner;
            }
        }
        /// <summary>
        /// Returns list of pawns occupying the cell
        /// </summary>
        private List<Pawn> occupiedBy;
        /// <summary>
        /// Returns list of pawns occupying the cell
        /// </summary>
        public List<Pawn> OccupiedBy
        {
            get { return occupiedBy; }
            set { occupiedBy = value; }
        }

        /// <summary>
        /// True if the cell is highlighted
        /// </summary>
        private bool isHighlighted;
        /// <summary>
        /// True if the cell is highlighted
        /// </summary>
        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; }
        }

        /// <summary>
        /// Sets the cell as horizontal cell
        /// </summary>
        private bool isHorizontal = true;
        /// <summary>
        /// Returns or sets cell horizontal state
        /// </summary>
        public bool IsHorizontal
        {
            get { return isHorizontal; }
            set { isHorizontal = value; }
        }
        /// <summary>
        /// Returns or sets cell vertical state
        /// </summary>
        public bool IsVertical
        {
            get { return !IsHorizontal; }
            set { IsHorizontal = !value; }
        }
        /// <summary>
        /// Cell bound
        /// </summary>
        private GRectangle bound;
        /// <summary>
        /// Cell bound
        /// </summary>
        public GRectangle Bound
        {
            get { return bound; }
            set 
            { 
                bound = value;
                OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Bound"));
            }
        }

        /// <summary>
        /// The cell global ID (1-96)
        /// </summary>
        private int globalID;
        /// <summary>
        /// The cell global ID (1-96)
        /// </summary>
        public int GlobalID
        {
            get { return globalID; }
            set { globalID = value; }
        }

        /// <summary>
        /// Checks if the cell is a shield cell
        /// </summary>
        private bool isShield;
        /// <summary>
        /// Checks if the cell is a shield cell
        /// </summary>
        public bool IsShield
        {
            get { return isShield; }
            set { isShield = value; }
        }

        /// <summary>
        /// Checks if the cell is a banj cell
        /// </summary>
        private bool isBanj;
        /// <summary>
        /// Checks if the cell is a banj cell
        /// </summary>
        public bool IsBanj
        {
            get { return isBanj; }
            set { isBanj = value; }
        }


        /// <summary>
        /// Default constructor
        /// </summary>
        public Cell()
        {
            Bound = new GRectangle();
            IsHighlighted = false;
            OccupiedBy = new List<Pawn>();
            IsShield = false;
            IsBanj = false;
        }

        /// <summary>
        /// Checks if the pawn can move to cell
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public bool CanAddPawn(Pawn pawn)
        {
            return (OccupiedBy.Count() == 0 || OccupiedBy[0].Owner.ID == pawn.Owner.ID || !IsShield);
        }   

        /// <summary>
        /// Add pawn to the cell occupants 
        /// </summary>
        /// <param name="pawn"></param>
        public void AddPawn(Pawn pawn)
        {
            if(CanAddPawn(pawn))
            {
                if (GlobalID == 97 || GlobalID == 98)
                    pawn.IsAchieved = true;

                KillOpponentPawns(pawn);
                OccupiedBy.Add(pawn);
                Update();
            }
        }
        /// <summary>
        /// Remove pawn from the occupied pawns
        /// </summary>
        /// <param name="pawn"></param>
        public void RemovePawn(Pawn pawn)
        {
            if (OccupiedBy.Count > 0 && OccupiedBy[0].Owner.ID == pawn.Owner.ID)
            {
                OccupiedBy.RemoveAll(x => x.ID == pawn.ID);
                Update();
            }
        }

        /// <summary>
        /// Update cell status
        /// </summary>
        public override void Update()
        {
            base.Update();
            if(OccupiedBy != null)
            {
                int hw = (int)((Bound.R.Width - (OccupiedBy.Count() * Pawn.SIZE)) / (OccupiedBy.Count() + 1));
                int hh = (int)(Bound.R.Height - Pawn.SIZE) / 2;
                int vw = (int)(Bound.R.Width - Pawn.SIZE) / 2;
                int vh = (int)((Bound.R.Height - (OccupiedBy.Count() * Pawn.SIZE)) / (OccupiedBy.Count() + 1));
                if (IsHorizontal)
                {
                    for (int i = 0; i < OccupiedBy.Count(); i++)
                    {
                        OccupiedBy[i].Bound.SetRect(new System.Drawing.Rectangle(
                            Bound.R.Left + hw + (i * (Pawn.SIZE + hw)),
                            Bound.R.Top + hh,
                            Pawn.SIZE,
                            Pawn.SIZE
                            ));
                    }
                }
                else
                {
                    for (int i = 0; i < OccupiedBy.Count(); i++)
                    {
                        OccupiedBy[i].Bound.SetRect(new System.Drawing.Rectangle(
                            Bound.R.Left + vw,
                            Bound.R.Top + vh + (i * (Pawn.SIZE + vh)),
                            Pawn.SIZE,
                            Pawn.SIZE
                            ));
                    }
                }
            }
            // GameApp.Instance.Update();
        }

        /// <summary>
        /// Kill the opponent pawns occupying the cell
        /// </summary>
        /// <param name="pawn"></param>
        private void KillOpponentPawns(Pawn pawn)
        {
            if (OccupiedBy == null || OccupiedBy.Count == 0)
                return;

            if(OccupiedBy[0].Owner.ID != pawn.Owner.ID)
            {
                foreach(Pawn p in OccupiedBy)
                {
                    p.Kill();
                }
                OccupiedBy.Clear();
            }
        }

        /// <summary>
        /// Render the cell
        /// </summary>
        /// <param name="g"></param>
        public override void Render(System.Drawing.Graphics g)
        {
            if (GlobalID == 97 || GlobalID == 98)
            {
                if (IsHighlighted)
                {
                    g.FillRectangle(System.Drawing.Brushes.Silver, Bound);
                }
            }
            else
            {
                if (IsHighlighted)
                {
                    g.FillRectangle(System.Drawing.Brushes.Silver, Bound);
                }
                Bound.Draw(g);
                DrawShield(g);
                DrawBanj(g);
            }

        }
        /// <summary>
        /// Render the Banj line
        /// </summary>
        /// <param name="g"></param>
        private void DrawBanj(Graphics g)
        {
            if(IsBanj)
            {
                g.DrawLine(Pens.Black, Bound.R.Left, Bound.R.Top, Bound.R.Right, Bound.R.Bottom);
            }
        }
        /// <summary>
        /// Render the shield lines
        /// </summary>
        /// <param name="g"></param>
        private void DrawShield(System.Drawing.Graphics g)
        {
            if(IsShield)
            {
                g.DrawLine(Pens.Black, Bound.R.Left, Bound.R.Top, Bound.R.Right, Bound.R.Bottom);
                g.DrawLine(Pens.Black, Bound.R.Right, Bound.R.Top, Bound.R.Left, Bound.R.Bottom);
            }
        }

        /// <summary>
        /// Fires on bound changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Bound")
            {
                
            }
        }
    }
}
