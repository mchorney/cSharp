using System.ComponentModel.DataAnnotations;
using System;

namespace RESTauranter.Models
{
    public abstract class BaseEntity {}
    public class Review : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        
        [Display(Name = "Reviewer Name")]
        [Required]
        public string Reviewer { get; set; }

        [Display(Name = "Restaurant Name")]
        [Required]
        public string Restaurant { get; set; }
        
        [Display(Name = "Review")]
        [Required]
        public string TheReview { get; set; }

        [Display(Name = "Visit Date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Rating")]
        [Required]
        public string Rating { get; set; }
    }
}