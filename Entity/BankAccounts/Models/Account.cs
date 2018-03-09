using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Account : BaseEntity
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string AccountType { get; set; }
        public string AccountNickname { get; set; }
        public float AccountBalance { get; set; }
        public List<Transaction> Transactions { get; set; }
        
        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}