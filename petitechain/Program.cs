using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using petitechain.Core;

namespace petitechain {
    class Program {
        static void Main(string[] args) {
            
            // Welcome to the playground!

            var block = new Block(1, new byte[]{ 0x0 }, 1, 1);
            //block.Transactions.Add(new BaseTransaction());
            
            Console.WriteLine("Hash is : {0}", block.Hash.ToHexString());
            Console.WriteLine("Reversed: {0}", block.Hash.ToHexString().HexStringToByteArray().ToHexString());

        }
    }
}
