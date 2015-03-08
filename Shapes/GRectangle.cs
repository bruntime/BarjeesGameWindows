using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Shapes
{
    /// <summary>
    /// Wrapping rectangle behavior
    /// </summary>
    public class GRectangle : GShape
    {
        #region Properties
        /// <summary>
        /// Rectangle field
        /// </summary>
        private Rectangle rect;
        /// <summary>
        /// Rectangle property
        /// </summary>
        public Rectangle R
        {
            get { return rect; }
            set 
            { 
                rect = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("R");
                OnPropertyChanged(this, e);
            }
        }
        /// <summary>
        /// Center point field
        /// </summary>
        private Point center;
        /// <summary>
        /// Get the center point of the rectangle
        /// </summary>
        public Point Center
        {
            get
            {
                return center;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GRectangle()
        {
            center = new Point();
            R = new Rectangle();

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rect">Bound rectangle</param>
        public GRectangle(Rectangle rect)
        {
            center = new Point();
            R = rect;
        } 
        #endregion

        #region Operators
        /// <summary>
        /// Implicit conversion to Rectangle
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static implicit operator Rectangle(GRectangle r)
        {
            return r.R;
        } 
        #endregion

        #region Operations
        /// <summary>
        /// Draws the rectangle
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawRectangle(Pens.Black, this);
        } 
        /// <summary>
        /// Change the rect
        /// </summary>
        /// <param name="rect"></param>
        public void SetRect(Rectangle rect)
        {
            R = rect;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Handle property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);
            if (e.PropertyName == "R")
            {
                center.X = R.Left + ((int)R.Width / 2);
                center.Y = R.Top + ((int)R.Height / 2);
            }
        } 
        #endregion
    }
}
