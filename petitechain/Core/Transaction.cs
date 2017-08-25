using System;

namespace petitechain.Core {

    public class Transaction : IHashable {

        public DateTime Timestamp { get; set; }
        public byte[] From { get; set; }
        public byte[] To { get; set; }
        public ulong Fee { get; set; }
        public uint Nonce { get; set; }

        private byte[] _hash;
        public byte[] Hash { 
            get {
                if(_hash == null){
                    _hash = GetHash();
                }
                return _hash;
            }
        }  

        public Transaction(byte[] from, byte[] to, ulong fee, uint nonce){
            From = from;
            To = to;
            Fee = fee;
            Nonce = nonce;
            Timestamp = DateTime.UtcNow;            
        }

        public byte[] GetHash(){
            var fieldsCombined = Helpers.Combine(
                BitConverter.GetBytes(((DateTimeOffset)Timestamp).ToUnixTimeSeconds()),
                From, To, 
                BitConverter.GetBytes(Nonce),  
                BitConverter.GetBytes(Fee)
            );
            return Config.BlockHashAlgorithm.ComputeHash(fieldsCombined);
        }
    }
}