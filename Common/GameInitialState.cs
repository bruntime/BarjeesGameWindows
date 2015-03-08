using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Game initial state
    /// </summary>
    public class GameInitialState : GameState
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameInitialState()
        {
            name = "Initial";
        }
        #endregion

        #region Handle State
        /// <summary>
        /// Enter initial game state
        /// </summary>
        protected override void Enter()
        {
            base.Enter();
        }
        /// <summary>
        /// Exit initial game state
        /// </summary>
        protected override void Exit()
        {
            base.Exit();
        }
        #endregion
    }
}
