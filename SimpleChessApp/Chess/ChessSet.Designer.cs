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

        }

        #endregion

        private System.Windows.Forms.ImageList blackList;
        private System.Windows.Forms.ImageList whiteList;
    }
}
