using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace petitechain {

    public static class Helpers {
        
        public static string ToHexString(this byte[] byteArray){
            return String.Concat(byteArray.Select(itm => itm.ToString("x2")));
        }

        public static byte[] HexStringToByteArray(this string hexString){
            var hexClean = hexString.TrimStart('0', 'x');
            if(!Regex.IsMatch(hexClean, @"\A\b[0-9a-fA-F]+\b\Z")){
                throw new ArgumentException("hexString should contain only hexadecimal characters", "hexString");
            }
            
            return Enumerable.Range(0, hexClean.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexClean.Substring(x, 2), 16))
                .ToArray();
        }

        public static byte[] Combine(params byte[][] arrays){
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays) {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

    }
}
