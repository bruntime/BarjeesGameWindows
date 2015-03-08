using Barjees.Commands;
using Barjees.Common;
using Barjees.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barjees.Windows
{
    /// <summary>
    /// The Barjees Game main application form
    /// </summary>
    public partial class BarjeesForm : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BarjeesForm()
        {
            InitializeComponent();
            CreateEventHandlers();
        }


        #region Update Form Operations
        /// <summary>
        /// Update the form components
        /// </summary>
        public void UpdateForm()
        {
            Invalidate(true);
            UpdateMenuItemsState();
            //UpdateGUI();
        }
        /// <summary>
        /// Invalidate the form controls 
        /// </summary>
        public void RenderForm()
        {
            // Invalidate(true);
            Canvas.Invalidate();
        }
        /// <summary>
        /// Update the status bar with a message
        /// </summary>
        /// <param name="message">A string to display in the status bar</param>
        public void SetStatus(string message)
        {
            statusLabel.Text = message;
        }
        /// <summary>
        /// Update the form UI
        /// </summary>
        public void UpdateGUI()
        {
            UpdatePlayerName();
        }

        /// <summary>
        /// Update the player name display
        /// </summary>
        private void UpdatePlayerName()
        {
            if (GameApp.Instance.Turn.CurrentPlayer == null)
            {
                playerLabel.Text = "No Player";
                playerLabel.ForeColor = Color.Black;
            }
            else
            {
                playerLabel.Text = GameApp.Instance.Turn.CurrentPlayer.Name;
                if(GameApp.Instance.Turn.CurrentPlayer is UserPlayer)
                {
                    playerLabel.ForeColor = Color.Blue;
                }
                else
                {
                    playerLabel.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Update the roll results label
        /// </summary>
        /// <param name="context"></param>
        public void UpdateResultsLabel(CommandContext context)
        {
            if (GameApp.Instance.IsRunning)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Player: {0}\n\n", context.Player.Name);
                foreach(var rr in context.RollResults.Where(x=> !x.IsUsed).GroupBy(x=> x.ToString()))
                {
                    sb.AppendFormat("{0} ({1})\n", rr.Key, rr.Count());
                }
                resultsLabel.Text = sb.ToString();
            }
            else
            {
                resultsLabel.Text = string.Empty;
            }

        }

        /// <summary>
        /// Update the states of all menu items
        /// </summary>
        protected void UpdateMenuItemsState()
        {
            // startNewGameToolStripMenuItem.Enabled = GameApp.Instance.IsInitial;
            pauseToolStripMenuItem.Enabled = GameApp.Instance.IsRunning;
            pauseToolStripMenuItem.Checked = GameApp.Instance.IsPaused;
            resumeToolStripMenuItem.Enabled = GameApp.Instance.IsPaused;
        }

        /// <summary>
        /// Update the form controls related to roll command
        /// </summary>
        public void UpdateRollCommandGui()
        {
            CommandContext context = GameApp.Instance.Turn.Context;
            rollButton.Enabled = (context.IsManual && (context.RollResults.Count() == 0 || context.RollAgain));
        }

        #endregion

        #region Initialize Form
        /// <summary>
        /// Create form components event handlers
        /// </summary>
        private void CreateEventHandlers()
        {
            FormClosed += BarjeesForm_FormClosed;
            Resize += BarjeesForm_Resize;
            startNewGameToolStripMenuItem.Click += startNewGameToolStripMenuItem_Click;
            pauseToolStripMenuItem.Click += pauseToolStripMenuItem_Click;
            resumeToolStripMenuItem.Click += resumeToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            logToolStripMenuItem.Click += logToolStripMenuItem_Click;
            Canvas.Paint += Canvas_Paint;
            Canvas.MouseClick += Canvas_MouseClick;
            rollButton.Click += rollButton_Click;
        }

        /// <summary>
        /// Handle mouse click on canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if(GameApp.Instance.IsRunning)
            {
                if(GameApp.Instance.Turn.RequireSelectPawn)
                {
                    foreach(var p in GameApp.Instance.Turn.CurrentPlayer.Pawns.Where(x=> x.CanSelected))
                    {
                        if(p.Bound.R.Contains(e.Location))
                        {
                            p.IsSelected = true;
                            GameApp.Instance.Turn.Context.SelectedPawn = p;
                            GameApp.Instance.Turn.RequireSelectPawn = false;
                            Invalidate(true);
                            break;
                        }
                    }
                    if (GameApp.Instance.Turn.Context.SelectedPawn != null)
                    {
                        GameApp.Instance.Board.Update();
                        GameApp.Instance.Turn.Update();
                    }
                }

                if(GameApp.Instance.Turn.RequireSelectCell)
                {
                    foreach(Cell c in GameApp.Instance.Turn.Context.PossibleCells)
                    {
                        if(c.Bound.R.Contains(e.Location))
                        {
                            GameApp.Instance.Turn.Context.SelectedCell = c;
                            GameApp.Instance.Turn.RequireSelectCell = false;
                            Invalidate(true);
                            break;
                        }
                    }
                    if (GameApp.Instance.Turn.Context.SelectedCell != null)
                    {
                        GameApp.Instance.Board.Update();
                        GameApp.Instance.Turn.Update();
                    }
                }
            }
        }

        /// <summary>
        /// Roll the shells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rollButton_Click(object sender, EventArgs e)
        {
            RollResult rr = RollCommand.NextResult();
            GameApp.Instance.Turn.Context.RollResults.Add(rr);
            GameApp.Instance.Turn.Context.RollAgain = rr.IsRepeatable;
            // MessageBox.Show(rr.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            rollButton.Enabled = GameApp.Instance.Turn.Context.RollAgain;
            
            UpdateResultsLabel(GameApp.Instance.Turn.Context);

            if (!GameApp.Instance.Turn.Context.RollAgain)
                GameApp.Instance.Turn.Update();
              
        }
        /// <summary>
        /// Called when the form resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BarjeesForm_Resize(object sender, EventArgs e)
        {
            GameApp.Instance.Board.SetRect(GetCanvasRectangle());
            Invalidate(true);
        }

        /// <summary>
        /// Handle the canvas paint event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Canvas_Paint(object sender, PaintEventArgs e)
        {
            GameApp.Instance.Render(e.Graphics, GetCanvasRectangle());
        }

        #endregion

        /// <summary>
        /// Returns the canvas rectangle
        /// </summary>
        /// <returns></returns>
        public Rectangle GetCanvasRectangle()
        {
            if(Canvas.ClientRectangle.Width == Canvas.ClientRectangle.Height)
            {
                return Canvas.ClientRectangle;
            }
            else
            {
                int diff = (int)System.Math.Abs(Canvas.ClientRectangle.Width - Canvas.ClientRectangle.Height) / 2;
                Rectangle r;
                if (Canvas.ClientRectangle.Width > Canvas.ClientRectangle.Height)
                {
                    r = new Rectangle
                    (
                        Canvas.ClientRectangle.Left + diff,
                        Canvas.ClientRectangle.Top,
                        Canvas.ClientRectangle.Height,
                        Canvas.ClientRectangle.Height
                    );
                }
                else
                {
                    r = new Rectangle
                    (
                        Canvas.ClientRectangle.Left,
                        Canvas.ClientRectangle.Top + diff,
                        Canvas.ClientRectangle.Width,
                        Canvas.ClientRectangle.Width
                    );
                }
                return r;
            }
           
        }

        #region Form Overloaded Operations
        /// <summary>
        /// Handle the main form load event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateForm();
        }

        /// <summary>
        /// Handle the form close event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BarjeesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameApp.Instance.ExitGame();
        }
        #endregion

        #region Menu Event Handlers
        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameApp.Instance.ExitGame();
        }

        void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameApp.Instance.ResumeGame();
        }

        void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameApp.Instance.PauseGame();
        }

        void startNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(GameApp.Instance.IsRunning || GameApp.Instance.IsPaused)
            {
                if (MessageBox.Show("Do you want to start a new game?", "Start New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
            }
            GameApp.Instance.StartNewGame();
        }

        void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            logWindow.ShowDialog();
        }
        #endregion
    }
}
