using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace apnConverter
{
    class ApnDataRow
    {
        private static readonly string DATA_TAG = "S325086";   //APN-tag marking a data-row (see APN file format reference)
        private static readonly byte ROW_CHECKSUM_MAGIC = 0x92;    //magic value used to calculate row-checksum (see APN file format reference)
        static readonly string NEWLINE = "\r\n";        //well duh..

        private string encodedRow;
        private byte[] dataBytes;
        private ApnPixel[] pixels;

        /// <summary>
        /// Constructs a new <code>ApnDataRow</code> instance from the given <code>string</code> which must be a full line read from a valid
        /// APN-TypeA-file. 
        /// </summary>
        /// <param name="row"></param>
        public ApnDataRow(string row)
        {
            _constructFromRaw(row);
        }
        /// <summary>
        /// Constructs a new <code>ApnDataRow</code> instance from the specified array of <code>ApnPixel</code>.
        /// <b>Note:</b> The array _must_ contain exactly 16 elements!
        /// </summary>
        /// <param name="pixels"></param>
        public ApnDataRow(ApnPixel[] pixels,int offset)
        {
            if (pixels == null)
                throw new Exception("argument was null");
            if (pixels.Length != 16)
                throw new Exception(String.Format("ApnDataRow (constructor): invalid parameter: expected pixels-count: 16, given count: {0}", pixels.Length));
            
            //construct encoded string:
            string offsetString = Convert.ToString(offset, 16).ToUpper();
            for (int b = 5 - offsetString.Length; b > 0; b--)   //prepend leading zeros..
                offsetString = "0" + offsetString;

            string encoded = DATA_TAG + offsetString;
            foreach (ApnPixel px in pixels)
            {
                string pxString = Convert.ToString(px.RawValue,16).ToUpper();
                switch (pxString.Length)    //prepend leading zeros
                {
                    case 3:
                        pxString = "0" + pxString;
                        break;
                    case 2:
                        pxString = "00" + pxString;
                        break;
                    case 1:
                        pxString = "000" + pxString;
                        break;
                }
                encoded += pxString;
            }
            _constructFromRaw(encoded);
            byte checksum = CalculateChecksum(dataBytes);
            encodedRow += Checksum + NEWLINE;
        }
        private void _constructFromRaw(string raw)
        {
            encodedRow = raw;
            string data = Data;
            dataBytes = new byte[data.Length / 2];
            for (byte b = 0; b < data.Length; b += 2)
            {
                dataBytes[b / 2] = Convert.ToByte(data.Substring(b, 2), 16);
            }
        }

        private string Offset
        {
            get 
            {
                return encodedRow.Substring(DATA_TAG.Length, 5);
            }            
        }
        private string Data
        {
            get 
            {
                return encodedRow.Substring(DATA_TAG.Length + Offset.Length,
                    //  encodedRow.Length - (DATA_TAG.Length + Offset.Length + 2));
                  64);
            }
        }
        private byte CalculateChecksum(byte[] data)
        {
            /*bool halt;
            if (Offset == "001E0") 
                halt = true;*/
            byte offset = (byte) Convert.ToInt32(Offset, 16);
            offset += Convert.ToByte(Offset.Substring(1, 2), 16);
            offset += Convert.ToByte(Offset.Substring(0, 1), 16);
            byte sum = 0;
            for (byte b = 0; b < data.Length; b++)
            {
                sum +=(byte) (data[b]);
            }
            sum = (byte) (ROW_CHECKSUM_MAGIC - 0x20 - sum - offset);
            return sum;
        }
        public byte ByteSum
        {
            get 
            { 
                byte sum = 0x0;
                foreach (byte b in dataBytes)
                    sum += b;
                return sum;
            }
        }
        public string Checksum
        {
            get 
            {
                string checksumString = Convert.ToString(CalculateChecksum(dataBytes),16).ToUpper();
                if (checksumString.Length == 1)
                    checksumString = "0" + checksumString;
                return checksumString;
            }
        }
        public ApnPixel[] Pixels
        {
            get
            {
                pixels = new ApnPixel[dataBytes.Length / 2];
                for (byte b = 0; b < dataBytes.Length; b += 2)
                {
                    pixels[b / 2] = new ApnPixel((ushort) (dataBytes[b + 1] | (dataBytes[b] << 8)));
                }
                return pixels;
            }
        }
        public string RawEncoded
        {
            get { return encodedRow; }
        }
    }
}
