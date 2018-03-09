using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class User : BaseEntity 
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set;}
        public List<Transaction> Transactions { get; set; }

        public User()
        {
            Accounts = new List<Account>();
            Transactions = new List<Transaction>();
        }
    }
}