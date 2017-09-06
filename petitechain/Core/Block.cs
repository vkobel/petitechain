using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace petitechain.Core {
    
    public class Block : IHashable {

        public uint Index { get; set; }
        public Block ParentBlock { get; set; }
        public BigInteger Nonce { get; set; }
        public ulong Difficulty { get; set; }
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        protected DateTime _datetime { get; set; } = DateTime.UtcNow;
        public ulong Timestamp { get => (ulong)((DateTimeOffset)_datetime).ToUnixTimeSeconds(); }

        private byte[] _hash;
        public byte[] Hash {
            get {
                if(_hash == null){
                    _hash = GetHash();
                }
                return _hash;
            }
        }       

        public Block(Block parentBlock, BigInteger nonce, ulong difficulty) {
            Index = parentBlock?.Index + 1 ?? 1;
            ParentBlock = parentBlock;
            Nonce = nonce;
            Difficulty = difficulty;
        }

        public byte[] GetHash() => Config.BlockHashAlgorithm.ComputeHash(
            Helpers.Combine(
                Index, 
                ParentBlock?.Hash, 
                Nonce, Difficulty, 
                Timestamp,
                BuildTransactionsRoot(Transactions)
            )
        );

        private byte[] BuildTransactionsRoot(List<Transaction> transactions){
            var hashs = Helpers.Combine(transactions.Select(t => t.Hash).ToArray());
            return Config.BlockHashAlgorithm.ComputeHash(hashs);
        }
    }
}
