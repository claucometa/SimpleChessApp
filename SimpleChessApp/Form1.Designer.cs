namespace SimpleChessApp
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(41, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 440);
            this.panel1.TabIndex = 0;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 70);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(21, 487);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(692, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(500, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(440, 440);
            this.panel2.TabIndex = 13;
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
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // clearBoardToolStripMenuItem
            // 
            this.clearBoardToolStripMenuItem.Name = "clearBoardToolStripMenuItem";
            this.clearBoardToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearBoardToolStripMenuItem.Text = "ClearBoard";
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singlePieceToolStripMenuItem,
            this.passantToolStripMenuItem,
            this.castlingToolStripMenuItem,
            this.promotionToolStripMenuItem});
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
            this.singlePieceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            this.passantToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.passantToolStripMenuItem.Text = "Passant";
            // 
            // castlingToolStripMenuItem
            // 
            this.castlingToolStripMenuItem.Name = "castlingToolStripMenuItem";
            this.castlingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.castlingToolStripMenuItem.Text = "Castling";
            // 
            // promotionToolStripMenuItem
            // 
            this.promotionToolStripMenuItem.Name = "promotionToolStripMenuItem";
            this.promotionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.promotionToolStripMenuItem.Text = "Promotion";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 536);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleChessApp";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
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
    }
}

