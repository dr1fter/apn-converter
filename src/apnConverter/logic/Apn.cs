using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace apnConverter
{
    /// <summary>
    /// provides methods to convert graphic files from and to the <code>APN-TypeA</code> graphic format. 
    /// </summary>
    class Apn
    {
        /* defines for known values */
        static readonly string HEADER_TAG = "S00E0000";
        static readonly string DATA_TAG = "S325086";
        static readonly string FULL_CHECKSUM_TAG = "S306086";
        static readonly string LAST_OFFSET = "25800";
        static readonly byte ROW_CHECKSUM_MAGIC = 0x92;
        static readonly string FOOTER_TAG = "S705860000074";
        static readonly string NEWLINE = "\r\n";

        private string[] lines;
        private ApnDataRow[] rows;
        private ApnHeader header;

        public Apn(){}

        /// <summary>
        /// tries to load an <code>APN-TypeA</code> file specified by the <code>file</code> parameter. In case the
        /// specified file is a valid APN-TypeA-file, the file can afterwards be written as a BMP file using the 
        /// <code>ToBitmap(string)</code> method.
        /// </summary>
        /// <param name="file">the fully qualified <code>string</code> representation of a path referring to a 
        /// APN-TypeA file. It is allowable for an invoker to pass <code>null</code> or a path to an invalid file
        /// or even an unexisting file - although the load will fail in these cases</param>
        /// <returns><code>true</code> in case the file could be successfully verified and loaded, else 
        /// <code>false</code></returns>
        public bool Load(String file)
        {
            file = file ?? "null";

            if (!File.Exists(file))
            {
                Console.WriteLine("An error was encountered while trying to load apn-file: " + file + " : File not found."
                    +"\nOperation will be aborted..");
                return false;
            }
            lines =  File.ReadAllLines(file);
            rows = new ApnDataRow[lines.Length - 3];
            byte fullsum = 0x0;
            for (short i = 1; i < lines.Length - 2; i++)
            {
                rows[i - 1] = new ApnDataRow(lines[i]);
                if (!lines[i].EndsWith(rows[i - 1].Checksum))
                    Console.WriteLine("missmatch:" + lines[i] + "-" + rows[i - 1].Checksum);
                fullsum += rows[i - 1].ByteSum;
            }
            byte fullChecksum =(byte) (0xFF - fullsum);
            Console.WriteLine("full checksum== "+ Convert.ToString(fullChecksum,16).ToUpper());
            header = new ApnHeader(lines[0]);
            return true;
        }
        public ApnDataRow[] Rows
        {
            get { return rows; }
        }
        public bool Valid
        {
            get { return VerifyFile(lines); }
        }
        public ApnHeader Header 
        { 
            get{ return header; }
            //set
            //{
            //    throw new NotImplementedException();
            //}
        }
        private bool VerifyFile(string[] lines)
        {
            if ((lines.Length == 0)||(!lines[0].StartsWith(HEADER_TAG)))
                return false;
            if (!lines[lines.Length - 1].EndsWith(FOOTER_TAG))
                return false;
            foreach (string line in lines)
            {
                if ((line.StartsWith(DATA_TAG))
                    &&(!VerifyDataLine(line)))
                    return false;
            }
            return true;
        }
        private bool VerifyDataLine(string line)
        {
            return true; //TODO: implement properly
        }

        public void ToBitmapFile(string file)
        {
            Bitmap bmp = ToBitmap();
            bmp.Save(file);
            bmp.Dispose();
        }
        
        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            ApnDataRow[] rows = this.Rows;
            ApnPixel[,] pixelsRows = new ApnPixel[320, 240];
            for (byte b = 0; b < 240; b++)
            {
                for (ushort x = 0; x < 320; x += 16)
                {
                    ApnDataRow current = rows[((b * 320) + x) / 16];
                    ApnPixel[] pixels = current.Pixels;
                    for (byte i = 0; i < 16; i++)
                    {
                        pixelsRows[x + i, b] = pixels[i];
                    }
                }
            }

            for (byte y = 0; y < 240; y++)
                for (ushort x = 0; x < 320; x++)
                    bmp.SetPixel(x, y, Color.FromArgb(
                         /*    (int) (((double)pixelsRows[x, y].Blue / (double)32) * 256),
                           (int)(((double)pixelsRows[x, y].Green / (double)64 )* 256),
                            (int)(((double)pixelsRows[x, y].Red / (double)32) * 256)*/
                     pixelsRows[x, y].Red << 3,
                     pixelsRows[x, y].Green << 2,
                     pixelsRows[x, y].Blue << 3

                  

                        ));
            return bmp;
        }

        public static bool FromBitmap(string source, string name, string destination)
        {
            if (source == null)
                source = "null";
            //check whether file exists:
            if (!File.Exists(source))
            {
                Console.WriteLine("error: file not found: " + source);
                return false;
            }
            Bitmap bmp = new Bitmap(source);
            return FromBitmap(bmp, name, destination);
        }
        public static bool FromBitmap(Bitmap bmp, string name,string destination)
        {
            string output = "";
            
            if (bmp.Size.Width < 320 || bmp.Size.Height < 240)
            {
                Console.WriteLine("error: bitmap does not match required dimensions (320x240)");
                return false;
            }
            else if (bmp.Size.Width > 320 || bmp.Size.Height > 240)
            {
                Console.WriteLine("warning: bitmap dimension does not exactly match expected dimensions:"
                    +"\nExpected: 320x240 px"
                    +"\nFile was: " + bmp.Size.Width + "x" + bmp.Size.Height + " px."
                    +"\nWill continue omitting picture content beyond max. size.");
            }
            Console.WriteLine("Reading pixel information from bitmap..");
            ApnPixel[] pixels = new ApnPixel[320*240];
            for (ushort y = 0; y < 240; y++)
            {
                for (ushort x = 0; x < 320; x++)
                {
                    Color px =  bmp.GetPixel(x, y);
                    pixels[320 * y + x] =
                            new ApnPixel(
                               (byte) (px.R >> 3), //WAS: 3
                               (byte) (px.G >> 2),  //WAS: 2 
                               (byte) (px.B >> 3));//was: 3  
                        /*     new ApnPixel(
                                 (byte) Math.Round((((float) px.R / 256)*32)),
                                 (byte) Math.Round((((float)px.G / 256)*64)),
                                 (byte) Math.Round((((float)px.B / 256)*32))); */
                        
                }
            }
            Console.WriteLine("Done reading from bitmap");
            Console.WriteLine("Creating .apn file, writing output to: " + destination + ".. (please be patient)");
            ApnDataRow[] rows = new ApnDataRow[(320*240/16)]; //+3 = header: 1 row, footer: 2 rows
            //write header..
            string header = ApnHeader.createHeader(name).RawData;
            output += header + NEWLINE;
            for (int i = 0; i < 320 * 240; i += 16)
            {
                ApnPixel[] tmppx = new ApnPixel[16];
                for (byte b = 0; b < 16; b++)
                {
                    tmppx[b] = pixels[i+b];
                }
                rows[i / 16] = new ApnDataRow(tmppx,(int) (0x20 * (i /16)));
            }
            Console.WriteLine("done converting pixels. Now writing to file " + destination);
            char[] rowBuffer = new char[4800 * 80];
           /*exchanged foreah-loop by the for(;;)-loop below to improve performance*/
                    //foreach (ApnDataRow row in rows)
                    //{
                    //    output += row.RawEncoded + NEWLINE;
                    //}
            //improving 
            for (int i = 0; i < rows.Length; i++)
            {
                Array.Copy(rows[i].RawEncoded.ToCharArray(),0,rowBuffer , i*80, 80);
            }
           // output += rowBuffer;
            StreamWriter writer =  File.CreateText(destination);
            writer.Write(output);
            output = "";
            writer.Write(rowBuffer);

            Console.WriteLine("done writing raw data. Now calculating full checksum..");
            //write footer:
            output += FULL_CHECKSUM_TAG + LAST_OFFSET;
            byte fullChecksum = CalculateFullChecksum(rows);
            byte footerChecksum =  CalculateFooterChecksum(fullChecksum);
            output += Convert.ToString(fullChecksum, 16).ToUpper();
            output += Convert.ToString(footerChecksum, 16).ToUpper();
            output += NEWLINE + FOOTER_TAG + NEWLINE;

            
            writer.Write(output);
            writer.Flush();
            writer.Close();
            Console.WriteLine("Done writing file: " + destination);
            return true;
        }
        static byte CalculateFullChecksum(ApnDataRow[] rows)
        {
            byte fullsum = 0x0;
            foreach (ApnDataRow row in rows)
            {
                fullsum += row.ByteSum;
            }
            byte fullChecksum = (byte)(0xFF - fullsum);
            return fullChecksum;
        }
        public static  byte CalculateFooterChecksum(byte data)
        {
            byte offset = (byte)Convert.ToInt32(LAST_OFFSET, 16);
            offset += Convert.ToByte(LAST_OFFSET.Substring(1, 2), 16);
            offset += Convert.ToByte(LAST_OFFSET.Substring(0, 1), 16);
            byte sum = 0;
            sum += (byte)(data);
            sum = (byte)(ROW_CHECKSUM_MAGIC - sum - offset -1);
            return sum;
        }
        /// <summary>
        /// Verifies that the file specified by the 'file' parameter is a valid APN-file.
        /// Note that the implementation of this method is a quick heuristic aproach wich
        /// is suitable to identify obvious cases of invalid APN files; for certainty, it is
        /// necessary to use the Apn.Load(string) / Apn.ToBmp(string)-methods..
        /// </summary>
        /// <param name="file"></param>
        /// <returns>true if the file specified is a valid APN-file, false if it is not</returns>
        public static bool VerifyApnHeuristically(string file)
        {
            if (file == null || !File.Exists(file)) return false;
            //file exists..
            FileInfo f = new FileInfo(file);
            if (f.Length != 384067) 
                return false;
            //size is correct..
            return true;
        }
    }
}
