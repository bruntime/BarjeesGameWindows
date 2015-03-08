using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Barjees.Shapes
{
    /// <summary>
    /// Base of all shapes
    /// </summary>
    public abstract class GShape : INotifyPropertyChanged
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GShape()
        {
            PropertyChanged += OnPropertyChanged;
        } 
        #endregion

        #region Public Operations
        /// <summary>
        /// Draw the shape
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g)
        { }
        /// <summary>
        /// Fires when a property has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
        #endregion

        #region Events

#pragma warning disable
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; 
#pragma warning restore

        #endregion
    }
}
