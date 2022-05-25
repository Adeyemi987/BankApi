using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } // Generates at every instance of this class
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; }
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);
        public string TransactionSourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public TransType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
         
        public Transaction()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1, 27)}";

        }


    }

    public enum TranStatus
    {
        Failed,
        Success,
        Error
    }

    public enum TransType
    {
        Deposit,
        Withdrawl,
        Transfer
    }
}
