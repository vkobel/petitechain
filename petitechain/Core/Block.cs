using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace petitechain.Core {
    
    public class Block : IHashable {

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
                    _hash = GetHash();
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

        public byte[] GetHash(){
            var fieldsCombined = Helpers.Combine(
                BitConverter.GetBytes(Index), 
                BitConverter.GetBytes(((DateTimeOffset)Timestamp).ToUnixTimeSeconds()),
                ParentHash, 
                Nonce.ToByteArray(), 
                BitConverter.GetBytes(Difficulty), 
                BuildTransactionsRoot(Transactions)
            );
            return Config.BlockHashAlgorithm.ComputeHash(fieldsCombined);
        }

        private byte[] BuildTransactionsRoot(List<Transaction> transactions){
            var hashs = Helpers.Combine(transactions.Select(t => t.Hash).ToArray());
            return Config.BlockHashAlgorithm.ComputeHash(hashs);
        }
    }
}
