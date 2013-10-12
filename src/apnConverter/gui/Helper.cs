using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace apnConverter.gui
{
    class Helper
    {
        /// <summary>
        /// Tries to read the given file as a source image file; in case the file provided is a valid image file,
        /// the result will be displayed at the source image file view. The file will also be available through
        /// the SourceFile property.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Bitmap TryReadSourceImageFile(FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return null;
            }
            try
            {
                Bitmap bmp = new Bitmap(file.FullName);
                return bmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }            
        }
        public static bool VerifyBitmap(Bitmap bmp)
        {
            if (bmp == null)
                return false;
            return bmp.Width >= 320 && bmp.Height >= 240;
        }        
    }
}
