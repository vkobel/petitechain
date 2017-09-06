using System;

namespace petitechain.Core {

    public class Transaction : IHashable {

        public byte[] From { get; set; }
        public byte[] To { get; set; }
        public uint Fee { get; set; }
        public uint Nonce { get; set; }
        public uint Value { get; set; }

        private byte[] _hash;
        public byte[] Hash { 
            get {
                if(_hash == null){
                    _hash = GetHash();
                }
                return _hash;
            }
        }  

        public Transaction(byte[] from, byte[] to, uint fee, uint nonce){
            From = from;
            To = to;
            Fee = fee;
            Nonce = nonce;           
        }

        public byte[] GetHash() => Config.BlockHashAlgorithm.ComputeHash(
            Helpers.Combine(From, To, Nonce, Fee)
        );
    }
}