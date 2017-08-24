using System;

namespace petitechain.Core {

    public class Transaction {

        public DateTime Timestamp { get; set; }
        public byte[] From { get; set; }
        public byte[] To { get; set; }
        public ulong Fee { get; set; }
        public uint Nonce { get; set; }

        private byte[] _hash;
        public byte[] Hash { 
            get {
                return new byte[]{ 0x33, 0xad, 0xff, 0x3c, 0x6e };
            }
        }

        public Transaction(byte[] from, byte[] to, ulong fee, uint nonce){
            From = from;
            To = to;
            Fee = fee;
            Nonce = nonce;
            Timestamp = DateTime.UtcNow;            
        }
    }
}