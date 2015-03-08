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
    /// Checks if the current player can still play with the roll results
    /// </summary>
    public class PlayCommand : Command
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The command context</param>
        public PlayCommand(CommandContext context)
            :base(context)
        {

        }

        /// <summary>
        /// Execute the play command
        /// </summary>
        public override void Execute()
        {
            if(Status == CommandStatus.Active)
            {
                Status = CommandStatus.Running;
                GameApp.Instance.Turn.InvokeRollCommand();
                GameApp.Instance.Turn.Update();
            }
            else if(Status == CommandStatus.Running)
            {
                if(!Context.Player.CanPlay(Context))
                {
                    Status = CommandStatus.Completed;
                    GameApp.Instance.Turn.Update();
                }
                else
                {
                    if(Context.IsAutomatic)
                    {
                        ExecuteAutomatic();
                    }
                    else
                    {
                        ExecuteManual();
                    }
                }
            }
        }

        /// <summary>
        /// Execute the manual play flow
        /// </summary>
        private void ExecuteManual()
        {
            // Manual play
            if (Context.SelectedPawn != null && Context.SelectedCell != null)
            {
                bool isMoved = false;
                // Play the  move
                foreach (var move in Context.PossibleMoves[Context.SelectedPawn.ID].Where(x => !x.IsUsed))
                {
                    Cell c = Context.SelectedPawn.GetCellAfter(move.Value);
                    if (c != null && c.GlobalID == Context.SelectedCell.GlobalID)
                    {
                        move.IsUsed = true;
                        //if (!Context.SelectedPawn.IsLive)
                        //    Context.SelectedPawn.IsLive = true;
                        Context.SelectedPawn.IsSelected = false;
                        Context.SelectedPawn.Move(move.Value);
                        GameApp.Instance.Board.Cells.Values.ToList().ForEach(x => x.IsHighlighted = false);
                        GameApp.Instance.Turn.RequireSelectCell = false;
                        GameApp.Instance.Turn.RequireSelectPawn = false;
                        Context.SelectedCell = null;
                        Context.SelectedPawn = null;
                        isMoved = true;
                        break;
                    }
                }

                if (isMoved)
                {
                    // GameApp.Instance.Turn.Update();
                    GameApp.Instance.MainForm.Invalidate(true);
                    GameApp.Instance.Update();
                }
            }
            else
            {
                GameApp.Instance.Turn.InvokeSelectPawnCommand();
            }
            GameApp.Instance.Turn.Update();
        }

        /// <summary>
        /// Execute the automatic logic for play
        /// </summary>
        private void ExecuteAutomatic()
        {
            // Set priorities
            Context.PriorityPawns.Clear();

            foreach (var p in Context.Player.Pawns.Where(x => !x.IsAchieved))
            {
                int priority = 1;

                // Positive: !IsLive, Close to Achieve, Kill Opponent, Protect
                // Negative: Escape
                foreach (PawnMove move in Context.PossibleMoves[p.ID].Where(x => !x.IsUsed))
                {

                    if (!p.IsLive && move.Value == 1)
                    {
                        if (priority < 10)
                            priority = 10;
                    }
                    if (p.IsLive)
                    {
                        int[] m = new int[] { 2, 3, 4, 6, 10, 11 };
                        foreach (int i in m)
                        {
                            Cell c = p.GetCellAfter(i);
                            if (c == null)
                                break;

                            if (c != null && c.IsShield)
                            {
                                if (priority < 20)
                                {
                                    priority = 20;
                                }
                            }
                            else if (c.Owner != null && c.Owner.ID != p.Owner.ID)
                            {
                                if (priority < 30)
                                    priority = 30;
                            }
                        }
                    }
                    if (Context.PriorityPawns.ContainsKey(p.ID))
                    {
                        if (priority > Context.PriorityPawns[p.ID])
                            Context.PriorityPawns[p.ID] = priority;
                    }
                    else
                        Context.PriorityPawns.Add(p.ID, priority);
                }
            }

            foreach (var id in Context.PriorityPawns.Keys.OrderByDescending(x => Context.PriorityPawns[x]))
            {
                var pawn = Context.Player.Pawns.First(x => x.ID == id);
                List<PawnMove> moves = Context.PossibleMoves[pawn.ID];
                if (moves.Count() > 0)
                {
                    PawnMove move = moves.First();
                    if (!move.IsUsed)
                    {
                        move.IsUsed = true;
                        pawn.Move(move.Value);
                        //if (!pawn.IsLive)
                            // pawn.IsLive = true;
                    }
                    foreach (var pmoves in Context.PossibleMoves.Values)
                    {
                        pmoves.RemoveAll(x => x.IsUsed);
                    }
                    break;
                }
            }
            GameApp.Instance.Board.Update();
            GameApp.Instance.Update();
        }

        /// <summary>
        /// Handle the event of command execution complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnCommandExecuted(object sender, EventArgs e)
        {
            base.OnCommandExecuted(sender, e);
        }
    }
}
