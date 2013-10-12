using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace apnConverter
{
    class ApnHeader
    {
        static readonly string HEADER_TAG = "S00E0000";
        static readonly string FILENAME_ENDING = "202061706E";  //"  apn", must be the last 5 chars
        static readonly string BLANK = "20";
        static readonly byte HEADER_CHECKSUM_MAGIC = 0x72;
        static byte TITLE_CHARS = 6;

        private byte[] rawBytes;
        private byte[] titleBytes;  //the first six bytes after the header_tag
        private string rawData; //the whole row
        private string RawTitle 
        {
            get { return rawData.Substring(HEADER_TAG.Length, rawData.Length - HEADER_TAG.Length - 2); }
        }
        private string RawChecksum
        {
            get { return rawData.Substring(HEADER_TAG.Length + RawTitle.Length); }
        }

        public string RawData
        {
            get { return rawData; }
        }

        public ApnHeader(String rawData)
        {
            this.rawData = rawData;
            this.rawBytes = writeRawBytes(rawData.Substring(HEADER_TAG.Length));
            titleBytes =  writeRawBytes(RawTitle.Substring(0,12));  //title is max. the first 6 bytes after the header tag resulting in 6*2 encoded chars
            calculateChecksum(titleBytes);
        }

        private byte[] writeRawBytes(string encodedTitle)
        {
            byte[] rawBytes = new byte[encodedTitle.Length / 2];
            for (int i = 0; i < encodedTitle.Length; i = i + 2)
            {
                rawBytes[i/2] = Convert.ToByte(encodedTitle.Substring(i, 2), 16);                
            }
            return rawBytes;
        }
        private byte calculateChecksum(byte[] bytes)
        {
            byte sum = 0;
            foreach (byte b in bytes)
                sum += b;
            sum =(byte) (HEADER_CHECKSUM_MAGIC - sum);
            return sum;
        }
        public string Title
        {
            get 
            {
                string encodedTitle = RawTitle;
                string decodedTitle = "";
               
                for (int i = 0; i < encodedTitle.Length; i = i + 2)
                {
                    byte currentByte = Convert.ToByte(encodedTitle.Substring(i,2),16);
                    decodedTitle += Convert.ToChar(currentByte);
                }
                return decodedTitle;
            }
            set
            {
                //only use first 6, fill with " " if less than 6 chars 
                string plaintextTitle = value.PadRight(6).Substring(0, 6);
                string encodedTitle = "";
                foreach(char c in plaintextTitle)
                {
                    byte currentChar = Convert.ToByte(c);
                    encodedTitle += Convert.ToString(currentChar, 16);
                }
                if (encodedTitle.Length < (TITLE_CHARS * 2))    //fill up of less than 6 chars specified
                    for (byte b = 0; b < (encodedTitle.Length / 2 - TITLE_CHARS); b++)
                        encodedTitle += BLANK;
                byte[] bytes = writeRawBytes(encodedTitle);
                byte checksum = calculateChecksum(bytes);

                //write new title to rawData:
                string checksumString = Convert.ToString(checksum, 16).ToUpper();
                if (checksumString.Length < 2)
                    checksumString = "0" + checksumString;
                rawData = HEADER_TAG + encodedTitle + FILENAME_ENDING + checksumString;
            }
        }
        public static ApnHeader createHeader(string title)
        {
            /*title = title.Substring(0,6);
            string rawHeader = HEADER_TAG;
            foreach (char c in title)
            {
                byte bt = Convert.ToByte(c);
                rawHeader += Convert.ToString(bt, 16);
            }
            rawHeader += FILENAME_ENDING;
            */
            ApnHeader header = new ApnHeader("S00E0000"+"00000000000000");
            header.Title = title;
            return header;
        }
    }
}
