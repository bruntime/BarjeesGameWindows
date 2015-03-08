using Barjees.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Commands
{
    /// <summary>
    /// Command to select a specific pawn
    /// </summary>
    public class SelectPawnCommand : Command
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public SelectPawnCommand(CommandContext context)
            :base(context)
        {
            Name = "Select Pawn";
        }

        /// <summary>
        /// Execute the select pawn command
        /// </summary>
        public override void Execute()
        {
            if(Status == CommandStatus.Active)
            {
                Status = CommandStatus.Running;
                Context.SelectedPawn = null;
                Context.SelectedCell = null;
                Context.Player.Pawns.Where(x => x.CanSelected).ToList().ForEach(x => x.IsSelected = false);
                GameApp.Instance.SetStatus("Please select a pawn");
                GameApp.Instance.Turn.Update();
            }
            else if(Status == CommandStatus.Running)
            {
                if(Context.SelectedPawn == null)
                {
                    GameApp.Instance.Turn.RequireSelectPawn = true;
                    GameApp.Instance.Turn.RequireSelectCell = false;
                }
                else
                {
                    if(Context.SelectedCell == null)
                    {
                        GameApp.Instance.SetStatus("Please select a cell");
                        Context.PossibleCells.Clear();
                        foreach(var move in Context.PossibleMoves[Context.SelectedPawn.ID])
                        {
                            Cell c = null;
                            if(!move.IsUsed)
                            {
                                c = Context.SelectedPawn.GetCellAfter(move.Value);
                                if (c != null)
                                {
                                    c.IsHighlighted = true;
                                    Context.PossibleCells.Add(c);
                                }
                            }
                        }
                        if(Context.PossibleCells.Count() > 0)
                        {
                            // Add select cell command
                            GameApp.Instance.Turn.InvokeSelectCellCommand();
                            GameApp.Instance.Turn.Update();
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Can't play with selected pawn, select another");
                            Context.SelectedPawn.IsSelected = false;
                            Context.SelectedCell = null;
                            Context.SelectedPawn = null;
                            GameApp.Instance.Turn.RequireSelectCell = false;
                            GameApp.Instance.Turn.RequireSelectPawn = true;
                            // GameApp.Instance.Turn.FlipTurn();
                            GameApp.Instance.Turn.Update();

                        }
                        GameApp.Instance.MainForm.UpdateForm();

                        GameApp.Instance.Turn.Update();
                    }
                    else
                    {
                        Status = CommandStatus.Completed;
                        GameApp.Instance.Turn.Update();
                    }
                }
            }
        }
    }
}
