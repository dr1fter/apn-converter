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
    public partial class ToApnCompo : UserControl
    {
        public ToApnCompo()
        {
            InitializeComponent();
            setSourceImageStatus(false);
            //hook listeners
            sourceFileTextBox.TextChanged += new EventHandler(sourceFileTextBox_TextChanged);
            destinationFileTextBox.TextChanged += new EventHandler(destinationFileTextBox_TextChanged);
        }
        /// <summary>
        /// Verifies the entered source file; 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sourceFileTextBox_TextChanged(object sender, EventArgs e)
        {
            string fileStr = sourceFileTextBox.Text;
            FileInfo file = new FileInfo(fileStr);
            sourceImage = Helper.TryReadSourceImageFile(file);
            //verify file..
            bool isValid = Helper.VerifyBitmap(sourceImage);
            setSourceImageStatus(isValid);          
        }
        /// <summary>
        /// Verifies the specified output location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void destinationFileTextBox_TextChanged(object sender, EventArgs e)
        {
            string fileStr = destinationFileTextBox.Text;
            if (fileStr.ToUpper().EndsWith(".APN"))
            {
                //test whether path is valid..
                try
                {
                    FileInfo f = new FileInfo(fileStr);
                    DirectoryInfo di = f.Directory;    //verify path is valid..
                    setDestinationFileStatus(f.Name.Length == 6 + 4);                    
                }
                catch (Exception)   //exception not of interest
                {
                    //oops, path is invalid
                    setDestinationFileStatus(false);
                }

            }
            else
                setDestinationFileStatus(false);            
        }
        private Bitmap sourceImage = null;
        private bool sourceValid = false;
        private bool destinationValid = false;


    
        private void browseSourceButton_Click(object sender, EventArgs e)
        {
            //show file dialogue, prompt for image file..
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select the source image file (must be 320x240 px!)";
            ofd.Multiselect = false;
            ofd.AddExtension = true;
            ofd.CheckFileExists = false;
            ofd.DefaultExt = "bmp";
            DialogResult dr = ofd.ShowDialog();
            switch (dr)
            {
                case DialogResult.Yes:
                {
                    string fileStr = ofd.FileName ?? "";                    
                    sourceFileTextBox.Text = fileStr;
                    break;
                }
                case DialogResult.OK:
                {
                    goto case DialogResult.Yes;
                }
            }
        }
        private void setDestinationFileStatus(bool destinationOk)
        {
            this.destinationValid = destinationOk;
            checkConvertPossible();
        }
        private void setSourceImageStatus(bool sourceIsOk)
        {
            this.sourceValid = sourceIsOk;   
            sourcePictureBox.Image = sourceImage;
            checkConvertPossible();
        }
        private void checkConvertPossible()
        {
            switch (sourceValid && destinationValid)
            {
                case true:  //enable "convert"-button etc.
                    writeApnFileButton.Enabled = true;
                    break;
                case false: //disable "convert"-button etc.
                    writeApnFileButton.Enabled = false;
                    break;
            }
        }

        private void browseDestinationButton_Click(object sender, EventArgs e)
        {
            //show file dialogue, prompt for image file..
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Specify the APN file to write to (must be 6 characters long!)";
            sfd.AddExtension = true;
            sfd.CheckFileExists = false;
            sfd.DefaultExt = "APN";
            sfd.Filter = "Alpine X100 Files (*.APN)|*.APN";
            DialogResult dr = sfd.ShowDialog();
            switch (dr)
            {
                case DialogResult.Yes:
                {
                    string fileStr = sfd.FileName;
                    fileStr = fileStr ?? "";
                    if (fileStr.Contains("/") || fileStr.Contains(@"\"))
                    {
                        int lastIndex = fileStr.LastIndexOfAny(new char[] {'/','\\'});
                        //make filename uppercase
                        int size = fileStr.Length;
                        fileStr = fileStr.Substring(0, lastIndex)
                            + fileStr.Substring(lastIndex).ToUpper();
                        //if filename is too short, fill it up with zeros..
                        for (int i = -1; (size - lastIndex)+i < 6 + 4; i++)
                        {
                            fileStr = fileStr.Insert(fileStr.Length - 4, "0");
                        }
                        //else if filename too long, shorten it
                        for (int i = 1; (size - lastIndex) - i > 6 + 4; i++)
                        {
                            fileStr = fileStr.Remove(fileStr.Length - 5,1);
                        }
                    }
                    destinationFileTextBox.Text = fileStr;
                    break;
                }
                case DialogResult.OK:
                {
                    goto case DialogResult.Yes;
                }
            }
        }

        private void writeApnFileButton_Click(object sender, EventArgs e)
        {
            //by contract, this control must only then be enabled if all prerequisites are met
            //therefore, no further validation required

            string outFile = destinationFileTextBox.Text.ToUpper();
            FileInfo f = new FileInfo(outFile);
            string name = f.Name.Substring(0,6);

            bool succ = Apn.FromBitmap(sourceImage, name, outFile);
            //todo: report success..
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
