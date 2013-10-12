namespace apnConverter.gui
{
    partial class ToApnCompo
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
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.browseSourceButton = new System.Windows.Forms.Button();
            this.sourceFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.browseDestinationButton = new System.Windows.Forms.Button();
            this.destinationFileTextBox = new System.Windows.Forms.TextBox();
            this.writeApnFileButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sourcePictureBox);
            this.groupBox1.Controls.Add(this.browseSourceButton);
            this.groupBox1.Controls.Add(this.sourceFileTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 291);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source (image file)";
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Location = new System.Drawing.Point(79, 43);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(320, 240);
            this.sourcePictureBox.TabIndex = 3;
            this.sourcePictureBox.TabStop = false;
            // 
            // browseSourceButton
            // 
            this.browseSourceButton.Location = new System.Drawing.Point(324, 14);
            this.browseSourceButton.Name = "browseSourceButton";
            this.browseSourceButton.Size = new System.Drawing.Size(75, 23);
            this.browseSourceButton.TabIndex = 2;
            this.browseSourceButton.Text = "B&rowse..";
            this.browseSourceButton.UseVisualStyleBackColor = true;
            this.browseSourceButton.Click += new System.EventHandler(this.browseSourceButton_Click);
            // 
            // sourceFileTextBox
            // 
            this.sourceFileTextBox.Location = new System.Drawing.Point(79, 17);
            this.sourceFileTextBox.Name = "sourceFileTextBox";
            this.sourceFileTextBox.Size = new System.Drawing.Size(239, 20);
            this.sourceFileTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.browseDestinationButton);
            this.groupBox2.Controls.Add(this.destinationFileTextBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 64);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination (APN file)";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output file";
            // 
            // browseDestinationButton
            // 
            this.browseDestinationButton.Location = new System.Drawing.Point(324, 16);
            this.browseDestinationButton.Name = "browseDestinationButton";
            this.browseDestinationButton.Size = new System.Drawing.Size(75, 23);
            this.browseDestinationButton.TabIndex = 4;
            this.browseDestinationButton.Text = "B&rowse..";
            this.browseDestinationButton.UseVisualStyleBackColor = true;
            this.browseDestinationButton.Click += new System.EventHandler(this.browseDestinationButton_Click);
            // 
            // destinationFileTextBox
            // 
            this.destinationFileTextBox.Location = new System.Drawing.Point(79, 19);
            this.destinationFileTextBox.Name = "destinationFileTextBox";
            this.destinationFileTextBox.Size = new System.Drawing.Size(239, 20);
            this.destinationFileTextBox.TabIndex = 0;
            // 
            // writeApnFileButton
            // 
            this.writeApnFileButton.Location = new System.Drawing.Point(12, 370);
            this.writeApnFileButton.Name = "writeApnFileButton";
            this.writeApnFileButton.Size = new System.Drawing.Size(390, 70);
            this.writeApnFileButton.TabIndex = 2;
            this.writeApnFileButton.Text = "Write APN file";
            this.writeApnFileButton.UseVisualStyleBackColor = true;
            this.writeApnFileButton.Click += new System.EventHandler(this.writeApnFileButton_Click);
            // 
            // ToApnCompo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.writeApnFileButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ToApnCompo";
            this.Size = new System.Drawing.Size(410, 440);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sourceFileTextBox;
        private System.Windows.Forms.Button browseSourceButton;
        private System.Windows.Forms.PictureBox sourcePictureBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseDestinationButton;
        private System.Windows.Forms.TextBox destinationFileTextBox;
        private System.Windows.Forms.Button writeApnFileButton;


    }
}
