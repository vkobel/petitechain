using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace petitechain {

    public static class Helpers {
        
        public static string ToHexString(this byte[] byteArray){
            return String.Concat(byteArray.Select(itm => itm.ToString("x2")));
        }

        public static byte[] HexStringToByteArray(this string hexString){
            var hexClean = hexString.StartsWith("0x") ? hexString.Substring(2) : hexString;
            if(!Regex.IsMatch(hexClean, @"\A\b[0-9a-fA-F]+\b\Z")){
                throw new ArgumentException("hexString should contain only hexadecimal characters", "hexString");
            }
            
            return Enumerable.Range(0, hexClean.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexClean.Substring(x, 2), 16))
                .ToArray();
        }

        public static byte[] CombineBytes(params byte[][] arrays){
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays) {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        // Support converting multiple standard types out of the box
        public static byte[] Combine(params object[] any){
            
            return CombineBytes(convertParams().ToArray());

            IEnumerable<byte[]> convertParams(){
                foreach(var p in any){
                    switch(p){
                        case uint intP:
                            yield return BitConverter.GetBytes(intP);
                        break;
                        case ulong longP:
                            yield return BitConverter.GetBytes(longP);
                        break;
                        case byte[] bytesP:
                            yield return bytesP;
                        break;
                        case BigInteger bigP:
                            yield return bigP.ToByteArray();
                        break;
                        case null:
                            yield return new byte[]{ 0x0 };
                        break;
                        default:
                            throw new FormatException($"The provided parameter of type {p.GetType()} is not supported by the convenience method Combine(object[])");
                    }
                }
            }
        }

    }
}
