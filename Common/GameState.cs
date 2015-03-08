using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Abstract base class of Game states, Initial, Running, Paused and GameOver
    /// </summary>
    public abstract class GameState
    {        
        #region Members
        /// <summary>
        /// Game state name
        /// </summary>
        protected string name;
        /// <summary>
        /// The game previous state
        /// </summary>
        protected GameState previousState = null;
        #endregion

        #region General Operations
        /// <summary>
        /// Exit the current game state, and returns an instance of the new state
        /// </summary>
        /// <param name="newState">Reference of the new state</param>
        /// <returns>Returns instance of the new state</returns>
        public virtual GameState ChangeState(GameState newState)
        {
            if(newState == null)
            {
                Exit();
                previousState.Enter();
                return previousState;
            }
            else if (newState is GamePauseState)
            {
                newState.previousState = this;
            }

            Exit();
            newState.Enter();
            return newState;
        }
        /// <summary>
        /// Convert current state to string
        /// </summary>
        /// <returns>returns current state description</returns>
        public override string ToString()
        {
            return name;
        }
        #endregion

        #region Control State Enterance and Exit
        /// <summary>
        /// Enter current state
        /// </summary>
        protected virtual void Enter()
        {
            GameApp.Instance.SetStatus(this.ToString());
        }
        /// <summary>
        /// Exit current state
        /// </summary>
        protected virtual void Exit()
        { }
        #endregion
    }
}
