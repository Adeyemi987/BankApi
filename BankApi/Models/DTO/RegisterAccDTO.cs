using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Profiles.DTO
{
    public class RegisterAccDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        //Lets add regular expression 
        [Required]
        [RegularExpression(@"^([0-9]/4{4})$", ErrorMessage ="Pin must not exceed four digits")]
        public string Pin { get; set; }

        [Compare("Pin", ErrorMessage = "pin do not match")]
        public string ConfirmPin { get; set; }
    }
}
