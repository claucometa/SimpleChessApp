namespace SimpleChessApp
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singlePieceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knightToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bishopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.queenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rookToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.passantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.castlingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promotionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.designBoard1 = new SimpleChessApp.BoardPanel();
            this.designBoard2 = new SimpleChessApp.BoardPanel();
            this.discoveredCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.clearBoardToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // clearBoardToolStripMenuItem
            // 
            this.clearBoardToolStripMenuItem.Name = "clearBoardToolStripMenuItem";
            this.clearBoardToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.clearBoardToolStripMenuItem.Text = "ClearBoard";
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singlePieceToolStripMenuItem,
            this.passantToolStripMenuItem,
            this.castlingToolStripMenuItem,
            this.promotionToolStripMenuItem,
            this.checkToolStripMenuItem,
            this.discoveredCheckToolStripMenuItem});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.testsToolStripMenuItem.Text = "Tests";
            // 
            // singlePieceToolStripMenuItem
            // 
            this.singlePieceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.knightToolStripMenuItem1,
            this.kingToolStripMenuItem1,
            this.bishopToolStripMenuItem1,
            this.queenToolStripMenuItem1,
            this.rookToolStripMenuItem1});
            this.singlePieceToolStripMenuItem.Name = "singlePieceToolStripMenuItem";
            this.singlePieceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.singlePieceToolStripMenuItem.Text = "Single Piece";
            // 
            // knightToolStripMenuItem1
            // 
            this.knightToolStripMenuItem1.Name = "knightToolStripMenuItem1";
            this.knightToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.knightToolStripMenuItem1.Text = "Knight";
            // 
            // kingToolStripMenuItem1
            // 
            this.kingToolStripMenuItem1.Name = "kingToolStripMenuItem1";
            this.kingToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.kingToolStripMenuItem1.Text = "King";
            // 
            // bishopToolStripMenuItem1
            // 
            this.bishopToolStripMenuItem1.Name = "bishopToolStripMenuItem1";
            this.bishopToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.bishopToolStripMenuItem1.Text = "Bishop";
            // 
            // queenToolStripMenuItem1
            // 
            this.queenToolStripMenuItem1.Name = "queenToolStripMenuItem1";
            this.queenToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.queenToolStripMenuItem1.Text = "Queen";
            // 
            // rookToolStripMenuItem1
            // 
            this.rookToolStripMenuItem1.Name = "rookToolStripMenuItem1";
            this.rookToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.rookToolStripMenuItem1.Text = "Rook";
            // 
            // passantToolStripMenuItem
            // 
            this.passantToolStripMenuItem.Name = "passantToolStripMenuItem";
            this.passantToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.passantToolStripMenuItem.Text = "Passant";
            // 
            // castlingToolStripMenuItem
            // 
            this.castlingToolStripMenuItem.Name = "castlingToolStripMenuItem";
            this.castlingToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.castlingToolStripMenuItem.Text = "Castling";
            // 
            // promotionToolStripMenuItem
            // 
            this.promotionToolStripMenuItem.Name = "promotionToolStripMenuItem";
            this.promotionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.promotionToolStripMenuItem.Text = "Promotion";
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.checkToolStripMenuItem.Text = "Check";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.testsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(10, 10);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(936, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(487, 37);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(113, 378);
            this.listBox1.TabIndex = 16;
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 17;
            this.listBox2.Location = new System.Drawing.Point(606, 37);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(113, 378);
            this.listBox2.TabIndex = 17;
            // 
            // designBoard1
            // 
            this.designBoard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.designBoard1.Location = new System.Drawing.Point(10, 68);
            this.designBoard1.Margin = new System.Windows.Forms.Padding(0);
            this.designBoard1.Name = "designBoard1";
            this.designBoard1.Size = new System.Drawing.Size(460, 460);
            this.designBoard1.TabIndex = 20;
            // 
            // designBoard2
            // 
            this.designBoard2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.designBoard2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.designBoard2.Location = new System.Drawing.Point(646, 50);
            this.designBoard2.Margin = new System.Windows.Forms.Padding(0);
            this.designBoard2.Name = "designBoard2";
            this.designBoard2.Size = new System.Drawing.Size(300, 300);
            this.designBoard2.TabIndex = 19;
            // 
            // discoveredCheckToolStripMenuItem
            // 
            this.discoveredCheckToolStripMenuItem.Name = "discoveredCheckToolStripMenuItem";
            this.discoveredCheckToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.discoveredCheckToolStripMenuItem.Text = "Discovered Check";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 536);
            this.Controls.Add(this.designBoard1);
            this.Controls.Add(this.designBoard2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleChessApp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singlePieceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bishopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rookToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem queenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem knightToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem kingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem passantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem castlingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promotionToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private BoardPanel designBoard2;
        private BoardPanel designBoard1;
        private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discoveredCheckToolStripMenuItem;
    }
}

