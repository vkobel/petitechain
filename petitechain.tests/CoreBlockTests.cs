using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using petitechain;
using petitechain.Core;
using Xunit;

namespace petitechain.tests {
    
    public class CoreBlockTests {

        public static IEnumerable<object[]> CreateFakeBlockChain(){
            var genesis = new Block(null, 0, 0);
            Block latestBlock = genesis;
            for(uint i = 1; i <= 32; i++){
                var newBlock = new Block(latestBlock, 0, 0);
                latestBlock = newBlock;
                yield return new []{ newBlock };
            }
        }
        
        [Theory]
        [MemberData(nameof(CreateFakeBlockChain))]
        public void IsHashingConversionWorking(Block b){
            Assert.Equal(b.Hash.ToHexString(), b.Hash.ToHexString().HexStringToByteArray().ToHexString());
            Assert.Equal(b.Hash, b.Hash.ToHexString().HexStringToByteArray());
            Assert.Equal(Config.BlockHashAlgorithm.HashSize / 8, b.Hash.Length);
            Assert.Equal(Config.BlockHashAlgorithm.HashSize / 8 * 2, b.Hash.ToHexString().Length);
        }

        [Theory]
        [MemberData(nameof(CreateFakeBlockChain))]
        public void IsBlockSeqenceCorrect(Block b){
            var currentBlock = b;
            while(currentBlock.ParentBlock != null){
                Assert.Equal(currentBlock.ParentBlock.Index + 1, currentBlock.Index);
                currentBlock = currentBlock.ParentBlock;
            }
        }

    }
}
