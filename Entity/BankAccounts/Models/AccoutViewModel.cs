using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class AccountViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Please select an account type.")]
        public string AccountType { get; set; }
        public string AccountNickname { get; set; }
        
    }
}