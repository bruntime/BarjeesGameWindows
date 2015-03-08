using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Common
{
    /// <summary>
    /// Base of all visual game objects
    /// </summary>
    public abstract class VisualGameObject : GameObject
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public VisualGameObject()
            : base()
        {
        } 
        #endregion

        #region Public Operations
        /// <summary>
        /// Renders the visual object
        /// </summary>
        /// <param name="g"></param>
        public virtual void Render(Graphics g)
        { } 
        #endregion
    }
}
