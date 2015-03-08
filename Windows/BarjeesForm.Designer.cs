namespace Barjees.Windows
{
    partial class BarjeesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CommandsPanel = new System.Windows.Forms.Panel();
            this.rollButton = new System.Windows.Forms.Button();
            this.panelsContainer = new System.Windows.Forms.SplitContainer();
            this.playerLabel = new System.Windows.Forms.Label();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.MainMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.CommandsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelsContainer)).BeginInit();
            this.panelsContainer.Panel1.SuspendLayout();
            this.panelsContainer.Panel2.SuspendLayout();
            this.panelsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(562, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "Main";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startNewGameToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // startNewGameToolStripMenuItem
            // 
            this.startNewGameToolStripMenuItem.Name = "startNewGameToolStripMenuItem";
            this.startNewGameToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.startNewGameToolStripMenuItem.Text = "Start &New Game";
            this.startNewGameToolStripMenuItem.ToolTipText = "Start a new game instance";
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pauseToolStripMenuItem.Text = "&Pause";
            this.pauseToolStripMenuItem.ToolTipText = "Pause the current running game";
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.resumeToolStripMenuItem.Text = "&Resume";
            this.resumeToolStripMenuItem.ToolTipText = "Resume game if paused";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.ToolTipText = "Exit barjees game";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.logToolStripMenuItem.Text = "&Log";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 422);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(562, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusBarStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(144, 17);
            this.statusLabel.Text = "Welcome to Barjees game";
            // 
            // CommandsPanel
            // 
            this.CommandsPanel.Controls.Add(this.rollButton);
            this.CommandsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CommandsPanel.Location = new System.Drawing.Point(0, 392);
            this.CommandsPanel.Name = "CommandsPanel";
            this.CommandsPanel.Size = new System.Drawing.Size(562, 30);
            this.CommandsPanel.TabIndex = 2;
            // 
            // rollButton
            // 
            this.rollButton.Enabled = false;
            this.rollButton.Location = new System.Drawing.Point(3, 3);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(75, 23);
            this.rollButton.TabIndex = 0;
            this.rollButton.Text = "Roll";
            this.rollButton.UseVisualStyleBackColor = true;
            // 
            // panelsContainer
            // 
            this.panelsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelsContainer.Location = new System.Drawing.Point(0, 24);
            this.panelsContainer.Name = "panelsContainer";
            // 
            // panelsContainer.Panel1
            // 
            this.panelsContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.panelsContainer.Panel1.Controls.Add(this.resultsLabel);
            this.panelsContainer.Panel1.Controls.Add(this.playerLabel);
            // 
            // panelsContainer.Panel2
            // 
            this.panelsContainer.Panel2.Controls.Add(this.Canvas);
            this.panelsContainer.Size = new System.Drawing.Size(562, 368);
            this.panelsContainer.SplitterDistance = 187;
            this.panelsContainer.TabIndex = 4;
            // 
            // playerLabel
            // 
            this.playerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.playerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerLabel.Location = new System.Drawing.Point(0, 0);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(187, 20);
            this.playerLabel.TabIndex = 0;
            this.playerLabel.Text = "No player";
            this.playerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(371, 368);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            // 
            // resultsLabel
            // 
            this.resultsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsLabel.Location = new System.Drawing.Point(0, 20);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(187, 348);
            this.resultsLabel.TabIndex = 1;
            // 
            // BarjeesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 444);
            this.Controls.Add(this.panelsContainer);
            this.Controls.Add(this.CommandsPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MainMenu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "BarjeesForm";
            this.Text = "Barjees";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.CommandsPanel.ResumeLayout(false);
            this.panelsContainer.Panel1.ResumeLayout(false);
            this.panelsContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelsContainer)).EndInit();
            this.panelsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel CommandsPanel;
        private System.Windows.Forms.Button rollButton;
        private System.Windows.Forms.SplitContainer panelsContainer;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label resultsLabel;
    }
}