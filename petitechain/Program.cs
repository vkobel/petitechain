using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using petitechain.Core;

namespace petitechain {
    class Program {

        public static IEnumerable<object[]> CreateFakeBlockChain(){
            var genesis = new Block(null, 0, 0);
            Block latestBlock = genesis;
            for(uint i = 1; i <= 64; i++){
                var newBlock = new Block(latestBlock, 0, 0);
                latestBlock = newBlock;
                yield return new []{ newBlock };
            }
        }

        static void Main(string[] args) {
            
            // Welcome to the playground!

            var bc = CreateFakeBlockChain();
            Console.WriteLine(bc.Count());
            foreach(var b in bc){
                var bb = b.SingleOrDefault() as Block;
                Console.WriteLine(bb.Index);
                Console.WriteLine($"   0x{bb.Hash.ToHexString()}");
                Console.WriteLine($"   0x{bb.Hash.ToHexString().HexStringToByteArray().ToHexString()}");
            }

        }
    }
}
