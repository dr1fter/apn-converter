namespace apnConverter.gui
{
    partial class MainMenuCompo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toBmpButton = new System.Windows.Forms.Button();
            this.createApnButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toBmpButton);
            this.groupBox1.Controls.Add(this.createApnButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 416);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "what would you like to do?";
            // 
            // toBmpButton
            // 
            this.toBmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toBmpButton.Location = new System.Drawing.Point(6, 85);
            this.toBmpButton.Name = "toBmpButton";
            this.toBmpButton.Size = new System.Drawing.Size(374, 60);
            this.toBmpButton.TabIndex = 1;
            this.toBmpButton.Text = "Convert an APN file to the Bitmap format";
            this.toBmpButton.UseVisualStyleBackColor = true;
            this.toBmpButton.Click += new System.EventHandler(this.toBmpButton_Click);
            // 
            // createApnButton
            // 
            this.createApnButton.Location = new System.Drawing.Point(6, 19);
            this.createApnButton.Name = "createApnButton";
            this.createApnButton.Size = new System.Drawing.Size(374, 60);
            this.createApnButton.TabIndex = 0;
            this.createApnButton.Text = "Create APN file from picture file";
            this.createApnButton.UseVisualStyleBackColor = true;
            this.createApnButton.Click += new System.EventHandler(this.createApnButton_Click);
            // 
            // MainMenuCompo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MainMenuCompo";
            this.Size = new System.Drawing.Size(410, 440);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createApnButton;
        private System.Windows.Forms.Button toBmpButton;
    }
}
