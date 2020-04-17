using System;
using System.Collections.Generic;
using System.Text;

namespace CrytpoPals
{
    public static class Logic
    {
        public static string Xor(string hex1, string hex2)
        {
            var x1 = Converter.FromHex(hex1);
            var x2 = Converter.FromHex(hex2);

            int length = x1.Length;
            if (length != x2.Length)
            {
                throw new InvalidOperationException("Only equal sized buffers allowed.");
            }

            var result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                var b1 = x1[i];
                var b2 = x2[i];
                var xor = (byte)(b1 ^ b2);
                result[i] = xor;
            }

            return Converter.ToHex(result);
        }
    }
}
