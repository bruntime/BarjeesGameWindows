using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// The state when game is running
    /// </summary>
    public class GameRunningState : GameState
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameRunningState()
        {
            name = "Running";
        }
        #endregion

        #region Handle State
        /// <summary>
        /// Enter running game state
        /// </summary>
        protected override void Enter()
        {
            base.Enter();
        }
        /// <summary>
        /// Exit current running state
        /// </summary>
        protected override void Exit()
        {
            base.Exit();
        }
        #endregion

    }
}
