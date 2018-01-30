using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class ValidDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }
    public class WeddingViewModel : BaseEntity
    {
        [Required(ErrorMessage = "You have to marry SOMEONE!")]
        [MinLength(2, ErrorMessage = "Please enter an actual name.")]
        public string WedderOne { get; set; }

        [Required(ErrorMessage = "You have to marry SOMEONE!")]
        [MinLength(2, ErrorMessage = "Please enter an actual name.")]
        public string WedderTwo { get; set; }

        [Required(ErrorMessage = "Please enter a date!")]
        [ValidDate(ErrorMessage = "You can't get married in the past!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter a valid address")]
        public string Address { get; set; }
    }
}