using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace petitechain {

    static class Config {
        
        public static HashAlgorithm BlockHashAlgorithm = SHA256.Create();

    }

}