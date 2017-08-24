using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace petitechain.Core {
    
    public class Block : ISerializable {

        public uint Index { get; set; }
        public DateTime Timestamp { get; set; }
        public byte[] ParentHash { get; set; }
        public BigInteger Nonce { get; set; }
        public ulong Difficulty { get; set; }
        public List<Transaction> Transactions { get; set; }

        private byte[] _hash;
        public byte[] Hash { 
            get {
                if(_hash == null){
                    _hash = BuildBlockHash(
                        BitConverter.GetBytes(Index), 
                        BitConverter.GetBytes(((DateTimeOffset)Timestamp).ToUnixTimeSeconds()),
                        ParentHash, 
                        Nonce.ToByteArray(), 
                        BitConverter.GetBytes(Difficulty), 
                        BuildTransactionsRoot(Transactions));
                }
                return _hash;
            }
        }       

        public Block(uint index, byte[] parentHash, BigInteger nonce, ulong difficulty) {
            Index = index;
            ParentHash = parentHash;
            Nonce = nonce;
            Difficulty = difficulty;
            Timestamp = DateTime.UtcNow;
            Transactions = new List<Transaction>();
        }

        protected byte[] BuildBlockHash(params byte[][] fields){
            var fieldsCombined = Combine(fields);
            return Config.BlockHashAlgorithm.ComputeHash(fieldsCombined);
        }

        private byte[] BuildTransactionsRoot(List<Transaction> transactions){
            var hashs = Combine(transactions.Select(t => t.Hash).ToArray());
            return Config.BlockHashAlgorithm.ComputeHash(hashs);
        }

        private byte[] Combine(params byte[][] arrays){
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays) {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context){
            throw new NotImplementedException();
        }
    }
}
