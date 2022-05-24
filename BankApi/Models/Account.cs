using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; } //Enums (Savings or current)
        public string AccountNumberGenerated { get; set; }

        //store hash and salt of the acct transaction pin
        public byte[] PinHash { get; set; }
        public byte[] PinSalt { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        //I want to generate an accountnumber in the below constructor
        //Generate a randon obj 

        Random rand = new Random();
        public Account()
        {
            AccountNumberGenerated = Convert.ToString((long)rand.NextDouble() * 9_000_000_000L + 1_000_000_000);
            //AccountName Property = FirstName + LastName
            AccountName = $"{FirstName} {LastName}";


        }
    }

    public enum AccountType
    {  
        Savings,
        current,
        Corporate,
        Government
    }
}
