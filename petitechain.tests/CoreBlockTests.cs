using System;
using System.Security.Cryptography;
using System.Text;
using petitechain.Core;
using Xunit;

namespace petitechain.tests {
    
    public class CoreBlockTests {

        private Block block = new Block(1, new byte[]{ 0x0 }, 1, 1);
        
        [Fact]
        public void IsHashingConversionWorking(){
            Assert.Equal(block.Hash.ToHexString(), block.Hash.ToHexString().HexStringToByteArray().ToHexString());
            Assert.Equal(block.Hash, block.Hash.ToHexString().HexStringToByteArray());
        }

        [Fact]
        public void IsHashingLengthCorrect(){
            Assert.Equal(32, block.Hash.Length);
            Assert.Equal(64, block.Hash.ToHexString().Length);
        }
    }
}
