using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models.DTO
{
    public class AuthAccDTO
    {
        [Required]
        [RegularExpression(@"^[0][1-9]/d{9}$|^[1-9]\d{9}$")]
        public string AccountNumber { get; set; }
        public string Pin { get; set; }

    }
}
