using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Game Over state
    /// </summary>
    public class GameOverState : GameState
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameOverState()
        {
            name = "Game Over";
        }
        #endregion

        #region Handle State
        /// <summary>
        /// Enter game over state
        /// </summary>
        protected override void Enter()
        {
            base.Enter();
            GameApp.Instance.SetStatus("Game Over, Winner is " + GameApp.Instance.Turn.CurrentPlayer.Name);
        }
        /// <summary>
        /// Exit game over state
        /// </summary>
        protected override void Exit()
        {
            base.Exit();
        }
        #endregion
    }
}
