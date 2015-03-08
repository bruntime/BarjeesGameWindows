using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Game pausing state
    /// </summary>
    public class GamePauseState : GameState
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GamePauseState()
        {
            name = "Paused";
        }
        #endregion

        #region Handle State
        /// <summary>
        /// Enter Pausing game state
        /// </summary>
        protected override void Enter()
        {
            base.Enter();
        }
        /// <summary>
        /// Exit pausing state
        /// </summary>
        protected override void Exit()
        {
            base.Exit();
        }
        #endregion
    }
}
