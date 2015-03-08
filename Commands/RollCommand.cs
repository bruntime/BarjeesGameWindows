using Barjees.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Commands
{
    /// <summary>
    /// Shell rolling command
    /// </summary>
    public class RollCommand : Command
    {
        static System.Random rand = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public RollCommand(CommandContext context)
            :base(context)
        {
            Name = "Roll";
        }

        /// <summary>
        /// Execute the rolling, the outcome will be stored in the current turn list of roll results
        /// </summary>
        public override void Execute()
        {
            if (Status == CommandStatus.Active)
            {
                Status = CommandStatus.Running;

                if (Context.IsManual)
                {
                    GameApp.Instance.MainForm.UpdateRollCommandGui();
                    Context.RollAgain = true;
                    ExecuteManual();

                }
                else
                {
                    ExecuteAutomatic();
                }
            }

            if(Status == CommandStatus.Running && Context.IsManual)
            {
                ExecuteManual();
            }
            //GameApp.Instance.MainForm.UpdateRollCommandGui();
        }

        /// <summary>
        /// Execute the command providing the player is computer
        /// </summary>
        private void ExecuteAutomatic()
        {
            do
            {
                RollResult rr = NextResult();
                Context.RollResults.Add(rr);
                Context.RollAgain = rr.IsRepeatable;
            } while (Context.RollAgain);
            Status = CommandStatus.Completed;
            GameApp.Instance.Turn.Update();
        }
        /// <summary>
        /// Execute the command providing the player is user
        /// </summary>
        private void ExecuteManual()
        {
            
            if (!Context.RollAgain)
            {
                Status = CommandStatus.Completed;
                GameApp.Instance.Turn.Update();
            }
            else
                GameApp.Instance.MainForm.UpdateRollCommandGui();
        }

        /// <summary>
        /// Handle command completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnCommandExecuted(object sender, EventArgs e)
        {
            // GameApp.Instance.Turn.Update();
            GameApp.Instance.MainForm.UpdateResultsLabel(Context);
            StringBuilder sb = new StringBuilder();
            Context.RollResults.GroupBy(x => x.TotalValue).ToList().ForEach(x => sb.AppendFormat("{0}({1}) ", x.Key.ToString(), x.Count()));
            bool canPlay = Context.Player.CanPlay(Context);
            Logging.Logger.Log("Roll", Context.Player.Name, null, null, sb.ToString(), "Can Play: " + canPlay.ToString());

        }

        /// <summary>
        /// Generates a random roll result
        /// </summary>
        /// <returns></returns>
        public static RollResult NextResult()
        {
            
            int result = rand.Next(0, PossibleResults.Length);
            RollResult rr = new RollResult(PossibleResults[result]);
            
            return rr;
        }
        private static int[] PossibleResults = { 2, 2, 2, 3, 3, 3, 4, 4, 4, 6, 11, 12, 26 };        
    }
}
