namespace SimpleChessApp.Game
{
    partial class Repository
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Repository));
            this.PawnPromotionDialog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.queenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bishopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.PawnPromotionDialog.SuspendLayout();
            // 
            // PawnPromotionDialog
            // 
            this.PawnPromotionDialog.AccessibleDescription = "";
            this.PawnPromotionDialog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.queenToolStripMenuItem,
            this.rookToolStripMenuItem,
            this.bishopToolStripMenuItem,
            this.knightToolStripMenuItem});
            this.PawnPromotionDialog.Name = "contextMenuStrip1";
            this.PawnPromotionDialog.Size = new System.Drawing.Size(111, 92);
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
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Chess_plt45.svg.png");
            this.imageList2.Images.SetKeyName(1, "50px-Chess_nlt45.svg.png");
            this.imageList2.Images.SetKeyName(2, "Chess_blt45.svg.png");
            this.imageList2.Images.SetKeyName(3, "Chess_rlt45.svg.png");
            this.imageList2.Images.SetKeyName(4, "Chess_klt45.svg.png");
            this.imageList2.Images.SetKeyName(5, "50px-Chess_qlt45.svg.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Chess_pdt45.svg.png");
            this.imageList1.Images.SetKeyName(1, "50px-Chess_ndt45.svg.png");
            this.imageList1.Images.SetKeyName(2, "50px-Chess_bdt45.svg.png");
            this.imageList1.Images.SetKeyName(3, "50px-Chess_rdt45.svg.png");
            this.imageList1.Images.SetKeyName(4, "50px-Chess_kdt45.svg.png");
            this.imageList1.Images.SetKeyName(5, "50px-Chess_qdt45.svg.png");
            this.PawnPromotionDialog.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PawnPromotionDialog;
        private System.Windows.Forms.ToolStripMenuItem queenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bishopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem knightToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
    }
}
