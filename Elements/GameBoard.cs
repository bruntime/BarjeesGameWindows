using Barjees.Common;
using Barjees.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barjees.Elements
{
    /// <summary>
    /// Main barjeest game board, the board contains cells
    /// </summary>
    public sealed class GameBoard : VisualGameObject
    {
        #region Boxes
        /// <summary>
        /// Default achieved pawn rectangles 
        /// </summary>
        private GRectangle[,] achievedPawnRects;
        /// <summary>
        /// Default achieved pawn rectangles 
        /// </summary>
        public GRectangle[,] AchievedPawnRects
        {
            get { return achievedPawnRects; }
            set { achievedPawnRects = value; }
        }

        /// <summary>
        /// Default bound for each pawn
        /// </summary>
        private GRectangle[,] defaultPawnRects;
        /// <summary>
        /// Default bound for each pawn
        /// </summary>
        public GRectangle[,] DefaultPawnRects
        {
            get { return defaultPawnRects; }
            set { defaultPawnRects = value; }
        }

        /// <summary>
        /// Bound rectangle
        /// </summary>
        private GRectangle bound;
        /// <summary>
        /// Bound rectangle
        /// </summary>
        public GRectangle Bound
        {
            get { return bound; }
            set
            {
                bound = value;
                OnPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Bound"));
            }
        }
        /// <summary>
        /// Center rectangle in the board
        /// </summary>
        private GRectangle centerBox;
        /// <summary>
        /// Center rectangle in the board
        /// </summary>
        public GRectangle CenterBox
        {
            get { return centerBox; }
        }
        /// <summary>
        /// Top left rectangle in the board
        /// </summary>
        private GRectangle topLeftBox;
        /// <summary>
        /// Top left rectangle in the board
        /// </summary>
        public GRectangle TopLeftBox
        {
            get { return topLeftBox; }
        }
        /// <summary>
        /// Top right rectangle in the board
        /// </summary>
        private GRectangle topRightBox;
        /// <summary>
        /// Top right rectangle in the board
        /// </summary>
        public GRectangle TopRightBox
        {
            get { return topRightBox; }
        }
        /// <summary>
        /// Bottom left rectangle in the board
        /// </summary>
        private GRectangle bottomLeftBox;
        /// <summary>
        /// Bottom left rectangle in the board
        /// </summary>
        public GRectangle BottomLeftBox
        {
            get { return bottomLeftBox; }
        }
        /// <summary>
        /// Bottom right rectangle in the board
        /// </summary>
        private GRectangle bottomRightBox;
        /// <summary>
        /// Bottom right rectangle in the board
        /// </summary>
        public GRectangle BottomRightBox
        {
            get { return bottomRightBox; }
        } 
        #endregion
        /// <summary>
        /// Dictionary of cells
        /// </summary>
        private Dictionary<int,Cell> cells;
        /// <summary>
        /// Dictionary of cells
        /// </summary>
        public Dictionary<int,Cell> Cells
        {
            get { return cells; }
            set { cells = value; }
        }
        

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameBoard()
        {
            Cells = new Dictionary<int, Cell>();
            centerBox = new GRectangle();
            topLeftBox = new GRectangle();
            topRightBox = new GRectangle();
            bottomLeftBox = new GRectangle();
            bottomRightBox = new GRectangle();
            Bound = new GRectangle();
        }

        /// <summary>
        /// Constructor with bound
        /// </summary>
        /// <param name="rect">The bound of board</param>
        public GameBoard(GRectangle rect)
        {
            Bound = rect;
        } 
        #endregion

        #region Handle Events
        /// <summary>
        /// Fires when propert changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Bound")
                UpdateRects();
            base.OnPropertyChanged(sender, e);
        } 
        #endregion

        /// <summary>
        /// Render the game board
        /// </summary>
        /// <param name="g"></param>
        public override void Render(System.Drawing.Graphics g)
        {
            g.DrawRectangle(Pens.Black, CenterBox);
        }
        /// <summary>
        /// Change the game board rectangle
        /// </summary>
        /// <param name="rect"></param>
        public void SetRect(Rectangle rect)
        {
            rect.Inflate(-10, -10);
            Bound = new GRectangle(rect);
            UpdateRects();
        }
        /// <summary>
        /// Update the internal boxes inside the board
        /// </summary>
        private void UpdateRects()
        {
            int w = (int) Bound.R.Width / 4;
            int sideW = (int)((Bound.R.Width - w) /2);

            centerBox.SetRect( new System.Drawing.Rectangle
                (
                    Bound.Center.X - ((int) w/2),
                    Bound.Center.Y - ((int)w / 2),
                    w,
                    w
                ));
            topLeftBox.SetRect( new System.Drawing.Rectangle
                (
                    Bound.R.Left,
                    Bound.R.Top,
                    sideW,
                    sideW
                ));
            topRightBox.SetRect( new System.Drawing.Rectangle
                (
                    Bound.R.Right - sideW,
                    Bound.R.Top,
                    sideW,
                    sideW
                ));
            bottomLeftBox.SetRect( new System.Drawing.Rectangle
                (
                    Bound.R.Left,
                    Bound.R.Bottom - sideW,
                    sideW,
                    sideW
                ));
            bottomRightBox.SetRect( new System.Drawing.Rectangle
                (
                    Bound.R.Right - sideW,
                    Bound.R.Bottom - sideW,
                    sideW,
                    sideW
                ));
            UpdateDefaultPawnRects();
            UpdateAchievedPawnRects();
            UpdateCells();         
        }

        /// <summary>
        /// Update the achieved pawns rectangles
        /// </summary>
        private void UpdateAchievedPawnRects()
        {
            AchievedPawnRects = new GRectangle [2,4];

            int space = (int) CenterBox.R.Width / 5;

            // Player 0, Pawn 0
            AchievedPawnRects[0, 0] = new GRectangle(new Rectangle(
                    CenterBox.R.Left + space,
                    CenterBox.R.Bottom - 2 * Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));

            for (int i = 1; i <= 3; i++)
            {
                // Player 0, Pawn i
                AchievedPawnRects[0, i] = new GRectangle(new Rectangle(
                        AchievedPawnRects[0, i - 1].R.Right + space,
                        AchievedPawnRects[0, i - 1].R.Bottom - 2 * Pawn.SIZE,
                        Pawn.SIZE,
                        Pawn.SIZE
                    ));
            }

            // Player 1, Pawn 0
            AchievedPawnRects[1, 0] = new GRectangle(new Rectangle(
                    CenterBox.R.Left + space,
                    CenterBox.R.Top + 2 * Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            for (int i = 1; i <= 3; i++)
            {
                // Player 1, Pawn i
                AchievedPawnRects[1, i] = new GRectangle(new Rectangle(
                        AchievedPawnRects[1, i-1].R.Right + space,
                        AchievedPawnRects[1, i-1].R.Top + 2 * Pawn.SIZE,
                        Pawn.SIZE,
                        Pawn.SIZE
                    ));
            }

        }

        /// <summary>
        /// Recalculate the default pawn rectangles
        /// </summary>
        private void UpdateDefaultPawnRects()
        {
            DefaultPawnRects = new GRectangle[2, 4];
            // Player 0, Pawn 0
            DefaultPawnRects[0, 0] = new GRectangle(new Rectangle(
                    TopRightBox.Center.X - Pawn.SIZE,
                    TopRightBox.Center.Y - Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 0, Pawn 1
            DefaultPawnRects[0, 1] = new GRectangle(new Rectangle(
                    TopRightBox.Center.X + Pawn.SIZE,
                    TopRightBox.Center.Y - Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 0, Pawn 2
            DefaultPawnRects[0, 2] = new GRectangle(new Rectangle(
                    TopRightBox.Center.X - Pawn.SIZE,
                    TopRightBox.Center.Y + Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 0, Pawn 3
            DefaultPawnRects[0, 3] = new GRectangle(new Rectangle(
                    TopRightBox.Center.X + Pawn.SIZE,
                    TopRightBox.Center.Y + Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 1, Pawn 0
            DefaultPawnRects[1, 0] = new GRectangle(new Rectangle(
                    BottomLeftBox.Center.X - Pawn.SIZE,
                    BottomLeftBox.Center.Y - Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 1, Pawn 1
            DefaultPawnRects[1, 1] = new GRectangle(new Rectangle(
                    BottomLeftBox.Center.X + Pawn.SIZE,
                    BottomLeftBox.Center.Y - Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 1, Pawn 2
            DefaultPawnRects[1, 2] = new GRectangle(new Rectangle(
                    BottomLeftBox.Center.X - Pawn.SIZE,
                    BottomLeftBox.Center.Y + Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            // Player 1, Pawn 3
            DefaultPawnRects[1, 3] = new GRectangle(new Rectangle(
                    BottomLeftBox.Center.X + Pawn.SIZE,
                    BottomLeftBox.Center.Y + Pawn.SIZE,
                    Pawn.SIZE,
                    Pawn.SIZE
                ));
            if (GameApp.Instance.Players != null)
            {
                foreach (var player in GameApp.Instance.Players)
                    foreach (var pawn in player.Pawns)
                        pawn.Update();
            }
        }
        /// <summary>
        /// Update the cells rectangles
        /// </summary>
        public void UpdateCells()
        {
            if (Cells.Count == 0)
                return;

            int hwidth = (int) CenterBox.R.Width / 3;
            int hhight = (int)TopLeftBox.R.Height / 8;
            int vwidth = hhight;
            int vhight = hwidth;
            int gid = 1;
            // region is North(1), East(2), South(3), or West(4)
            int region = 1;
            int row = 1;

            while(gid <= 96)
            {
                row = 1 + (int)(gid / 12);
                for (int col = 1; col <= 3; col++)
                {
                    
                    Cell c = Cells[gid];
                    Rectangle r;
                    switch(region)
                    {
                        case 1:
                            r = new Rectangle
                                (
                                    CenterBox.R.Left + ((col-1) * hwidth),
                                    CenterBox.R.Top - (row * hhight),
                                    hwidth,
                                    hhight
                                );
                            c.Bound.SetRect(r);
                            break;
                        case 2:
                            r = new Rectangle
                                (
                                    CenterBox.R.Right + ((row-1) * vwidth),
                                    CenterBox.R.Top + ((col-1) * vhight),
                                    vwidth,
                                    vhight
                                );
                            c.Bound.SetRect(r);
                            break;
                        case 3:
                            r = new Rectangle
                                (
                                    CenterBox.R.Right - (col * hwidth),
                                    CenterBox.R.Bottom + ((row-1) * hhight),
                                    hwidth,
                                    hhight
                                );
                            c.Bound.SetRect(r);
                            break;
                        case 4:
                            r = new Rectangle
                                (
                                    CenterBox.R.Left - (row  * vwidth),
                                    CenterBox.R.Bottom - (col * vhight),
                                    vwidth,
                                    vhight
                                );
                            c.Bound.SetRect(r);
                            break;
                    }

                    gid++;
                    
                }
                if (region == 4)
                    region = 1;
                else
                    region++;
            }

            // Set the north cook cell bound area
            Cell northCookCell = Cells[97];
            Rectangle northRect = new Rectangle
                (
                    CenterBox.R.Left,
                    CenterBox.R.Top,
                    CenterBox.R.Width,
                    CenterBox.R.Top + (int)(CenterBox.R.Height / 2)
                );
            northCookCell.Bound.SetRect(northRect);
            // South cook cell 
            Cell southCookCell = Cells[98];
            Rectangle southRect = new Rectangle
                (
                    CenterBox.R.Left,
                    CenterBox.R.Bottom - (int)(CenterBox.R.Height / 2),
                    CenterBox.R.Width,
                    (int)(CenterBox.R.Height / 2)
                );
            southCookCell.Bound.SetRect(southRect);
            

            foreach (Cell c in Cells.Values)
                c.Update();
        }

        /// <summary>
        /// Set the banj cells
        /// </summary>
        public void SetBanjCells()
        {
            Cells[88].IsBanj = true;
            Cells[94].IsBanj = true;
        }
        /// <summary>
        /// Set the shield cells
        /// </summary>
        public void SetShieldCells()
        {
            Cells[61].IsShield = true;
            Cells[63].IsShield = true;
            Cells[64].IsShield = true;
            Cells[66].IsShield = true;
            Cells[67].IsShield = true;
            Cells[69].IsShield = true;
            Cells[70].IsShield = true;
            Cells[72].IsShield = true;
        }

    }
}
