using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace apnConverter.gui
{
    public partial class ToBmpCompo : UserControl
    {
        private bool isSourceFileValid = false;
        private bool isDestinationFileValid = false;

        private Apn apn = null;

        public ToBmpCompo()
        {
            InitializeComponent();
            //hook listeners
            sourceFileTextBox.TextChanged += new EventHandler(sourceFileTextBox_TextChanged);
            destinationFileTextBox.TextChanged += new EventHandler(destinationFileTextBox_TextChanged);
        }

        void destinationFileTextBox_TextChanged(object sender, EventArgs e)
        {
            //determine whether current destination file is a valid filepath..
            string destinationFile = destinationFileTextBox.Text;
            try
            {
                FileInfo fi = new FileInfo(destinationFile);
                setIsDestinationFileValid(true);
            }
            catch (Exception)    //exception is of no interest..
            {
                setIsDestinationFileValid(false);
            }
            
        }

        void sourceFileTextBox_TextChanged(object sender, EventArgs e)
        {
            //verify that the file selected exists and is a valid APN-file
            string fileStr = sourceFileTextBox.Text;
            FileInfo f = new FileInfo(fileStr);
            if (f.Exists)
            {
                bool isValid = Apn.VerifyApnHeuristically(fileStr);
                if (isValid)
                {
                    this.apn = new Apn();
                    isValid = apn.Load(fileStr) && apn.Valid;
                    if (isValid)
                    {
                        this.sourcePictureBox.Image = apn.ToBitmap();                        
                    }
                }
                else
                    sourcePictureBox.Image = null;

                setIsSourceFileValid(isValid);
                if (isValid && destinationFileTextBox.Text.Length == 0)
                {
                    //preset the textBox..
                    string outFile = f.DirectoryName;
                    outFile =  
                        Path.Combine(outFile,
                            f.Name.Substring(0,f.Name.Length - f.Extension.Length)+".bmp");
                    destinationFileTextBox.Text = outFile;
                    isDestinationFileValid = true;
                }
            }
            else
            {
                //nothing to verify.. --> invalid in all cases
                setIsSourceFileValid(false);
                apn = null;
                sourcePictureBox.Image = null;
            }            
        }

        private void browseSourceButton_Click(object sender, EventArgs e)
        {
            //show file dialogue, prompt for image file..
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select the APN file to read";
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.DefaultExt = "APN";
            ofd.Filter = "Alpine X100 Files (*.APN)|*.APN";
            DialogResult dr = ofd.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                {
                    string fileStr = ofd.FileName??"";
                    sourceFileTextBox.Text = fileStr;
                    break;
                }
                case DialogResult.Yes:
                {
                    goto case DialogResult.OK;
                }
            }
        }
        private void setIsSourceFileValid(bool isValid)
        {
            this.isSourceFileValid = isValid;           
            checkIfConversionPossible();
        }
        private void setIsDestinationFileValid(bool isValid)
        {
            this.isDestinationFileValid = isValid;
            checkIfConversionPossible();
        }
        private void checkIfConversionPossible()
        {
            writeBmpFileButton.Enabled = isSourceFileValid && isDestinationFileValid;
        }

        private void writeBmpFileButton_Click(object sender, EventArgs e)
        {
            //this button must only be enabled if checkIfConversionPossible() resulted in positive result
            // --> no need to additional verify parameters =)
            apn.ToBitmapFile(destinationFileTextBox.Text);
        }

        private void browseDestinationButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Specify where to save the converted APN file to..";
            sfd.AddExtension = true;
            sfd.CheckFileExists = true;
            sfd.CheckPathExists = true;
            sfd.DefaultExt = "BMP";

            DialogResult result = sfd.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    destinationFileTextBox.Text = sfd.FileName;
                    break;
                case DialogResult.Yes:
                    goto case DialogResult.OK;
            }
        }
    }
}
