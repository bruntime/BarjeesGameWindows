using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Commands
{
    /// <summary>
    /// Select cell to move to 
    /// </summary>
    public class SelectCellCommand : Command
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public SelectCellCommand(CommandContext context)
            :base(context)
        {
            Name = "Select Cell";
        }

        public override void Execute()
        {
            if(Status == CommandStatus.Active)
            {
                Status = CommandStatus.Running;
                Context.SelectedCell = null;
                
            }
            else if(Status == CommandStatus.Running)
            {

                if(Context.SelectedCell != null)
                {
                    GameApp.Instance.Turn.RequireSelectCell = false;
                    Status = CommandStatus.Completed;
                    GameApp.Instance.Turn.Update();
                }
                else
                {
                    GameApp.Instance.Turn.RequireSelectCell = true;
                }
            }
        }
    }
}
