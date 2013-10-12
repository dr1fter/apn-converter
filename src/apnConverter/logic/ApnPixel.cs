using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace apnConverter
{
    class ApnPixel
    {
        /*
         * these three defines are used to erase the respective bits from the given 16-bit value (representing the
         * three colour values of a pixel).
         * 
         * The 16 bits of a pixel are assigned in this way:
         * 
         * (MSB)                    (LSB)
         *      RRRR RGGG GGGB BBBB 
         */ 
        readonly static ushort RED_ERASOR = 0x7FF;        // 0000 0111 1111 1111
        readonly static ushort GREEN_ERASOR =  0xF81F;    // 1111 1000 0001 1111
        readonly static ushort BLUE_ERASOR = 0xFFE0;      // 1111 1111 1110 0000

        readonly static byte FIVE_BITS_FILTER = 0x1F;   // 0001 1111
        readonly static byte SIX_BITS_FILTER = 0x3F;    // 0011 1111

        private ushort rawValue = 0x0;
        public ApnPixel(ushort rawValue)
        {
            this.rawValue = rawValue;
        }
        public ApnPixel(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        public ushort RawValue
        {
            get
            {
                return rawValue;
            }
            set
            {
                rawValue = value;
            }
        }
        /// <summary>
        /// gets or sets the pixel's red value
        /// </summary>
        public byte Red
        {
            /* the 5 MSBs are the relevant ones for Red:
                RRRR Rxxx xxxx xxxx
             */
            get 
            {
                //the red value is stored in the 5 MSBs
                byte red = (byte) (rawValue >> (6 + 5));
                return red;
            }
            set
            {
                value &= FIVE_BITS_FILTER;
                ushort red = value;
                red <<= (5+6);
                rawValue &= RED_ERASOR;
                rawValue |= red;
            }
        }
        /// <summary>
        /// gets or sets the pixel's green value
        /// </summary>
        public byte Green
        {
            /*
             * the following bits are relevant for green:
             *  xxxx xGGG GGGx xxxx
             */
            get 
            {
                ushort tempRaw = rawValue;
                tempRaw &=RED_ERASOR;
                byte green = (byte)(tempRaw >> (5));
                return green;
            }
            set
            {
                value &= SIX_BITS_FILTER;
                ushort green = value;
                green <<= 5;
                rawValue &= GREEN_ERASOR;
                rawValue |= green;
            }
        }
        /// <summary>
        /// gets or sets the pixel's blue value
        /// </summary>
        public byte Blue
        {
            /* the five LSBs are relevant for blue:
                xxxx xxxx xxxB BBBB*/
            get 
            {
                ushort tempRaw = rawValue;
                tempRaw &= RED_ERASOR;
                tempRaw &= GREEN_ERASOR;
                byte blue = (byte)tempRaw;
                return blue;
            }
            set
            {
                value &= FIVE_BITS_FILTER;
                rawValue &= BLUE_ERASOR;
                rawValue |= value;
            }
        }
    }
}


