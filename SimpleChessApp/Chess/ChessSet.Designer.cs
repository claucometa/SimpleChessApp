namespace SimpleChessApp
{
    partial class ChessSet
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessSet));
            this.blackList = new System.Windows.Forms.ImageList(this.components);
            this.whiteList = new System.Windows.Forms.ImageList(this.components);
            this.pawnPromotion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.queenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bishopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pawnPromotion.SuspendLayout();
            // 
            // blackList
            // 
            this.blackList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("blackList.ImageStream")));
            this.blackList.TransparentColor = System.Drawing.Color.Transparent;
            this.blackList.Images.SetKeyName(0, "chess_pawn.png");
            this.blackList.Images.SetKeyName(1, "chess_horse.png");
            this.blackList.Images.SetKeyName(2, "chess_bishop.png");
            this.blackList.Images.SetKeyName(3, "chess_tower.png");
            this.blackList.Images.SetKeyName(4, "chess_king.png");
            this.blackList.Images.SetKeyName(5, "chess_queen.png");
            // 
            // whiteList
            // 
            this.whiteList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("whiteList.ImageStream")));
            this.whiteList.TransparentColor = System.Drawing.Color.Transparent;
            this.whiteList.Images.SetKeyName(0, "chess_pawn_white.png");
            this.whiteList.Images.SetKeyName(1, "chess_horse_white.png");
            this.whiteList.Images.SetKeyName(2, "chess_bishop_white.png");
            this.whiteList.Images.SetKeyName(3, "chess_tower_white.png");
            this.whiteList.Images.SetKeyName(4, "chess_king_white.png");
            this.whiteList.Images.SetKeyName(5, "chess_queen_white.png");
            // 
            // pawnPromotion
            // 
            this.pawnPromotion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queenToolStripMenuItem,
            this.rookToolStripMenuItem,
            this.bishopToolStripMenuItem,
            this.knightToolStripMenuItem});
            this.pawnPromotion.Name = "contextMenuStrip1";
            this.pawnPromotion.Size = new System.Drawing.Size(111, 92);
            // 
            // queenToolStripMenuItem
            // 
            this.queenToolStripMenuItem.Name = "queenToolStripMenuItem";
            this.queenToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.queenToolStripMenuItem.Text = "Queen";
            // 
            // rookToolStripMenuItem
            // 
            this.rookToolStripMenuItem.Name = "rookToolStripMenuItem";
            this.rookToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.rookToolStripMenuItem.Text = "Rook";
            // 
            // bishopToolStripMenuItem
            // 
            this.bishopToolStripMenuItem.Name = "bishopToolStripMenuItem";
            this.bishopToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.bishopToolStripMenuItem.Text = "Bishop";
            // 
            // knightToolStripMenuItem
            // 
            this.knightToolStripMenuItem.Name = "knightToolStripMenuItem";
            this.knightToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.knightToolStripMenuItem.Text = "Knight";
            this.pawnPromotion.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList blackList;
        private System.Windows.Forms.ImageList whiteList;
        private System.Windows.Forms.ContextMenuStrip pawnPromotion;
        private System.Windows.Forms.ToolStripMenuItem queenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bishopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem knightToolStripMenuItem;
    }
}
