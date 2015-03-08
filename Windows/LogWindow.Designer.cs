namespace Barjees.Windows
{
    partial class LogWindow
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
            this.logList = new System.Windows.Forms.ListView();
            this.colLogType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPawn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCell = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // logList
            // 
            this.logList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogType,
            this.colPlayer,
            this.colPawn,
            this.colCell,
            this.colDesc,
            this.colOut});
            this.logList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logList.Location = new System.Drawing.Point(0, 24);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(634, 304);
            this.logList.TabIndex = 0;
            this.logList.UseCompatibleStateImageBehavior = false;
            this.logList.View = System.Windows.Forms.View.Details;
            // 
            // colLogType
            // 
            this.colLogType.Text = "Type";
            // 
            // colPlayer
            // 
            this.colPlayer.Text = "Player";
            // 
            // colPawn
            // 
            this.colPawn.Text = "Pawn";
            // 
            // colCell
            // 
            this.colCell.Text = "Cell";
            // 
            // colDesc
            // 
            this.colDesc.Text = "Description";
            this.colDesc.Width = 200;
            // 
            // colOut
            // 
            this.colOut.Text = "Outcome";
            this.colOut.Width = 200;
            // 
            // LogMenu
            // 
            this.LogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.LogMenu.Location = new System.Drawing.Point(0, 0);
            this.LogMenu.Name = "LogMenu";
            this.LogMenu.Size = new System.Drawing.Size(634, 24);
            this.LogMenu.TabIndex = 1;
            this.LogMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openLogFileToolStripMenuItem.Text = "&Open Log File";
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 328);
            this.Controls.Add(this.logList);
            this.Controls.Add(this.LogMenu);
            this.MainMenuStrip = this.LogMenu;
            this.Name = "LogWindow";
            this.Text = "Log";
            this.LogMenu.ResumeLayout(false);
            this.LogMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView logList;
        private System.Windows.Forms.ColumnHeader colLogType;
        private System.Windows.Forms.ColumnHeader colPlayer;
        private System.Windows.Forms.ColumnHeader colPawn;
        private System.Windows.Forms.ColumnHeader colCell;
        private System.Windows.Forms.ColumnHeader colDesc;
        private System.Windows.Forms.ColumnHeader colOut;
        private System.Windows.Forms.MenuStrip LogMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
    }
}