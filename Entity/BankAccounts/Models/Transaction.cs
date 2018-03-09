using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public float TransAmount { get; set; }
        public DateTime Date { get; set; }
    }
}