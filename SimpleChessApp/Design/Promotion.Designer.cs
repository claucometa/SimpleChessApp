namespace SimpleChessApp
{
    partial class Promotion
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
            this.square1 = new SimpleChessApp.Game.SimpleSquare();
            this.square2 = new SimpleChessApp.Game.SimpleSquare();
            this.square3 = new SimpleChessApp.Game.SimpleSquare();
            this.square4 = new SimpleChessApp.Game.SimpleSquare();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // square1
            // 
            this.square1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.square1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.square1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.square1.IsBlackSquare = false;
            this.square1.Location = new System.Drawing.Point(0, 0);
            this.square1.Margin = new System.Windows.Forms.Padding(0);
            this.square1.Name = "square1";
            this.square1.Piece = null;
            this.square1.Size = new System.Drawing.Size(58, 67);
            this.square1.TabIndex = 0;
            // 
            // square2
            // 
            this.square2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.square2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.square2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.square2.IsBlackSquare = false;
            this.square2.Location = new System.Drawing.Point(58, 0);
            this.square2.Margin = new System.Windows.Forms.Padding(0);
            this.square2.Name = "square2";
            this.square2.Piece = null;
            this.square2.Size = new System.Drawing.Size(58, 67);
            this.square2.TabIndex = 1;
            // 
            // square3
            // 
            this.square3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.square3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.square3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.square3.IsBlackSquare = false;
            this.square3.Location = new System.Drawing.Point(116, 0);
            this.square3.Margin = new System.Windows.Forms.Padding(0);
            this.square3.Name = "square3";
            this.square3.Piece = null;
            this.square3.Size = new System.Drawing.Size(58, 67);
            this.square3.TabIndex = 2;
            // 
            // square4
            // 
            this.square4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.square4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.square4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.square4.IsBlackSquare = false;
            this.square4.Location = new System.Drawing.Point(174, 0);
            this.square4.Margin = new System.Windows.Forms.Padding(0);
            this.square4.Name = "square4";
            this.square4.Piece = null;
            this.square4.Size = new System.Drawing.Size(61, 67);
            this.square4.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.square1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.square4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.square2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.square3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 67);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 67);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Game.SimpleSquare square1;
        private Game.SimpleSquare square2;
        private Game.SimpleSquare square3;
        private Game.SimpleSquare square4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}