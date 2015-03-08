using Barjees.Commands;
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
    /// Represents a player pawn
    /// </summary>
    public class Pawn : VisualGameObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the pawn living status
        /// </summary>
        private bool isLive;
        /// <summary>
        /// Gets or sets the pawn living status
        /// </summary>
        public bool IsLive
        {
            get { return isLive; }
            set 
            { 
                isLive = value;
                OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsLive"));
            }
        }

        /// <summary>
        /// Gets or sets the achieving status of pawn
        /// </summary>
        private bool isAchieved;
        
        /// <summary>
        /// Gets or sets the achieving status of pawn
        /// </summary>
        public bool IsAchieved
        {
            get { return isAchieved; }
            set 
            { 
                isAchieved = value;
                OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsAchieved"));
            }
        }

        /// <summary>
        /// Gets or sets pawn selection mode
        /// </summary>
        private bool isSelected;
        /// <summary>
        /// Gets or sets pawn selection mode
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;
                OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsSelected"));
            }
        }

        /// <summary>
        /// Checks if pawn can be selected
        /// </summary>
        public bool CanSelected
        {
            get { return !IsAchieved; }
        }
        /// <summary>
        /// Stores the current cell in route
        /// </summary>
        private LinkedListNode<Cell> currentCellNode;
        /// <summary>
        /// Stores the current cell in route
        /// </summary>
        public LinkedListNode<Cell> CurrentCellNode
        {
            get { return currentCellNode; }
            set 
            { 
                currentCellNode = value;
                OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CurrentCellNode"));
            }
        }
        /// <summary>
        /// Checks if the current pawn cell is cooking
        /// </summary>
        public bool IsCooking
        {
            get
            {
                if (CurrentCellNode == null)
                    return false;
                else
                    return (CurrentCellNode.Next == null || DistanceToEnd() <= 8); 
                    //Owner.Route.RouteType.Map.Reverse().Take(7).Contains(CurrentCellNode.Next.Value.GlobalID));
            }
        }
        /// <summary>
        /// Returns the owner player
        /// </summary>
        public Player Owner
        {
            get { return Parent as Player; }
        }
        /// <summary>
        /// Rectangular pawn boundary field
        /// </summary>
        private GRectangle bound;
        /// <summary>
        /// Rectangular pawn boundary property
        /// </summary>
        public GRectangle Bound
        {
            get { return bound; }
            set { bound = value; }
        } 
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public Pawn() :base()
        {
            IsLive = false;
            IsSelected = false;
            IsAchieved = false;
        }
        /// <summary>
        /// Render the pawn shape
        /// </summary>
        /// <param name="g"></param>
        public override void Render(Graphics g)
        {
            base.Render(g);
            if (Bound != null)
            {
                // Bound.Draw(g);
                g.FillEllipse(GetBrush(), Bound);
            }
        }

        /// <summary>
        /// Returns a reference to cell after number of steps
        /// </summary>
        /// <param name="steps">Number of steps to move</param>
        /// <returns></returns>
        public Cell GetCellAfter(int steps)
        {
            if (IsAchieved)
                return null;

            Cell c = null;
            bool result = false;
            int forwardSteps = steps;

            LinkedListNode<Cell> node = CurrentCellNode;
            if (node == null)
            {
                node = Owner.Route.CellNodes.First;
                forwardSteps--;
                if (steps == 1)
                    result = true;
            }

            for (int i = 1; i <= forwardSteps; i++)
            {
                if (node.Next == null && i < forwardSteps)
                {
                    result = false;
                }
                else if (node.Next == null && i == forwardSteps)
                {
                    result = true;
                }
                else
                {
                    node = node.Next;
                    result = true;
                }
            }

            if (IsLive && steps > DistanceToEnd() )
            {
                result = false;
            }
            else if (IsLive && steps == DistanceToEnd() )
            {
                result = true;
            }
            else if (result)
            {
                if (node.Value.CanAddPawn(this))
                {
                    c = node.Value;
                }
                else
                    result = false;
            }
            return c;
        }
        /// <summary>
        /// Move the current pawn position forward in the route
        /// </summary>
        /// <param name="steps">Number of steps to move</param>
        /// <returns></returns>
        public bool Move(int steps)
        {
            if (IsAchieved)
                return false;

            bool result = false;
            int forwardSteps = steps;

            LinkedListNode<Cell> node = CurrentCellNode;
            if (node == null)
            {
                node = Owner.Route.CellNodes.First;
                forwardSteps--;
                if(steps == 1)
                    result = true;
            }

            for (int i = 1; i <= forwardSteps; i++)
            {
                if (node.Next == null && i < forwardSteps)
                {
                    result = false;
                }
                else if (node.Next == null && i == forwardSteps)
                {
                    result = true;
                }
                else
                {
                    node = node.Next;
                    result = true;
                }
            }

            if (IsLive && steps > DistanceToEnd())
            {
                result = false;
            }
            else if (IsLive && steps == DistanceToEnd())
            {
                result = true;
                IsAchieved = true;
            }
            else if (result)
            {
                if (node.Value.CanAddPawn(this))
                {
                    if (CurrentCellNode != null)
                        CurrentCellNode.Value.RemovePawn(this);
                    CurrentCellNode = node;
                    Logging.Logger.Log("Move", Owner.Name, ID.ToString(), CurrentCellNode.Value.GlobalID.ToString(), "Pawn moved to cell", steps.ToString() + " steps");
                }
                else
                    result = false;
            }
            return result;
        }

        /// <summary>
        /// Returns true if the pawn can move forward 
        /// </summary>
        /// <param name="steps">Number of steps tp move</param>
        /// <returns></returns>
        public bool CanMove(int steps)
        {
            if (IsAchieved)
                return false;

            bool result = false;
            int forwardSteps = steps;
            LinkedListNode<Cell> node = CurrentCellNode;
            if (node == null)
            {
                node = Owner.Route.CellNodes.First;
                forwardSteps--;
                if (steps == 1)
                    result = true;
            }

            for (int i = 1; i <= forwardSteps; i++)
            {
                if (node.Next == null && i < forwardSteps)
                {
                    result = false;
                }
                else if(node.Next == null && i == forwardSteps)
                {
                    result = true;
                }
                else
                {
                    node = node.Next;
                    result = true;
                }
            }
            if(result)
            {
                if (IsLive && steps > DistanceToEnd())
                    result = false;
                else if (IsLive && steps == DistanceToEnd())
                {
                    result = true;
                }
                else if (node.Value != null)
                    result = node.Value.CanAddPawn(this);
            }
            return result;
        }

        /// <summary>
        /// Kill the pawn
        /// </summary>
        public void Kill()
        {
            IsLive = false;
            IsSelected = false;
            IsAchieved = false;
            CurrentCellNode = null;
            Update();
        }

        /// <summary>
        /// Returns how many steps are still required for the pawn to achieve the end
        /// </summary>
        /// <returns></returns>
        public int DistanceToEnd()
        {
            if (IsAchieved || !IsLive || this.currentCellNode == null)
                return 0;
            else
            {
                int steps = 1;
                LinkedListNode<Cell> node = this.currentCellNode;
                while(node.Next != null)
                {
                    steps++;
                    node = node.Next;
                }
                return steps;
            }
        }

        /// <summary>
        /// Checks of the pawn can still play with the unused moves
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CanPlay(CommandContext context)
        {
            if (IsAchieved)
                return false;

            bool result = false;
            if (context.PossibleMoves.Keys.Contains(this.ID))
                context.PossibleMoves.Remove(this.ID);
            List<PawnMove> moves = new List<PawnMove>();
            foreach (var rr in context.RollResults)
            {
                foreach (var move in rr.Moves.Where(x => !x.IsUsed))
                {
                    if (isLive)
                    {
                        if (CanMove(move.Value))
                        {
                            moves.Add(move);
                            result = true;
                        }
                    }
                    else
                    {
                        if(move.Value == 1)
                        {
                            moves.Add(move);
                            result = true;
                        }
                    }
                }
            }
            context.PossibleMoves.Add(this.ID, moves);
            return result;
        }


        /// <summary>
        /// Fires when property has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLive")
            {
                Update();
            }

            if(e.PropertyName == "IsAchieved")
            {
                if (IsAchieved)
                {
                    //if (CurrentCellNode != null)
                    //    CurrentCellNode.Value.RemovePawn(this);

                    //IsLive = false;
                    
                }
            }

            if(e.PropertyName == "IsSelected")
            {
                if(IsSelected)
                {
                    // Owner.Pawns.Where(x => x.ID != ID).ToList().ForEach(x => x.IsSelected = false);
                    GameApp.Instance.MainForm.RenderForm();
                }
            }

            if(e.PropertyName == "CurrentCellNode")
            {
                if (CurrentCellNode == null)
                {
                    IsLive = false;
                    Update();

                }
                else
                {
                    IsLive = true;
                    CurrentCellNode.Value.AddPawn(this);
                    Update();
                }
            }
        }

        /// <summary>
        /// Update the pawn state
        /// </summary>
        public override void Update()
        {
            base.Update();
            //if (IsAchieved)
            //{
            //    if (Owner != null)
            //    {
            //        int playerIndex = (Owner.ID == GameApp.Instance.Players[0].ID) ? 0 : 1;

            //        for (int i = Owner.Pawns.Count(x=> x.IsAchieved) - 1; i < Owner.Pawns.Length; i++)
            //        {
            //            if (ID == Owner.Pawns[i].ID)
            //            {
            //                Bound = GameApp.Instance.Board.AchievedPawnRects[playerIndex, i];
            //                break;
            //            }
            //        }
            //    }
            //}
            //else 
            if (!IsLive)
            {
                if (Owner != null)
                {
                    int playerIndex = (Owner.ID == GameApp.Instance.Players[0].ID) ? 0 : 1;
                    
                    for (int i = 0; i < Owner.Pawns.Length; i++)
                    {
                        if (ID == Owner.Pawns[i].ID)
                        {
                            Bound = GameApp.Instance.Board.DefaultPawnRects[playerIndex, i];
                            break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Get the pawn brush
        /// </summary>
        /// <returns></returns>
        public Brush GetBrush()
        {
            if (Owner != null)
            {
                if(IsAchieved)
                {
                    if (Owner is UserPlayer)
                        return Brushes.Cyan;
                    else
                        return Brushes.Orange;

                }
                else if (IsSelected)
                    return Brushes.Green;
                else if(IsCooking)
                {
                    if (Owner is UserPlayer)
                        return Brushes.Cyan;
                    else
                        return Brushes.Orange;
                }
                else
                    return Owner.GetBrush();
            }
            else
                return null;
        }

        /// <summary>
        /// The pawn size
        /// </summary>
        public static int SIZE
        {
            get
            {
                if (GameApp.Instance.Board != null)
                    return (int)GameApp.Instance.Board.Cells[1].Bound.R.Width / 4;
                else
                    return 10;
            }
        }
    }
}
