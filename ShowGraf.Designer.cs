namespace myGraf
{
    partial class ShowGraf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowGraf));
            this.mGrafRegion = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mGrafRegion)).BeginInit();
            this.SuspendLayout();
            // 
            // mGrafRegion
            // 
            this.mGrafRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mGrafRegion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mGrafRegion.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mGrafRegion.Location = new System.Drawing.Point(12, 34);
            this.mGrafRegion.Name = "mGrafRegion";
            this.mGrafRegion.Size = new System.Drawing.Size(268, 225);
            this.mGrafRegion.TabIndex = 0;
            this.mGrafRegion.TabStop = false;
            this.mGrafRegion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mGrafRegion_MouseMove);
            this.mGrafRegion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mGrafRegion_MouseDown);
            this.mGrafRegion.Paint += new System.Windows.Forms.PaintEventHandler(this.mGrafRegion_Paint);
            this.mGrafRegion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mGrafRegion_MouseUp);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ShowGraf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 271);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mGrafRegion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowGraf";
            this.Text = "ShowGraf";
            ((System.ComponentModel.ISupportInitialize)(this.mGrafRegion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mGrafRegion;
        private System.Windows.Forms.Button button1;
    }
}