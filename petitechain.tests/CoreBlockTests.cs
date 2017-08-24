using System;
using System.Security.Cryptography;
using System.Text;
using petitechain.Core;
using Xunit;

namespace petitechain.tests {
    
    public class CoreBlockTests {
        
        [Fact]
        public void IsHashingWorking(){
            
            var block = new Block(1, new byte[]{ 0x0 }, 1, 1);

            Assert.Equal(block.Hash.ToHexString(), block.Hash.ToHexString().HexStringToByteArray().ToHexString());
        }
    }
}
