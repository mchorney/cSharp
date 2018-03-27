using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BrightIdeas.Models 
{
    public class User : BaseEntity 
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Name:")]
        public string Name { get; set; }
        [Display(Name = "Alias:")]
        [Required(ErrorMessage = "*Required")]
        public string Alias { get; set; }
        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "*Required")]
        public string Email { get; set; }
        [MinLength(8)]
        [Display(Name = "Password:")]
        [Required(ErrorMessage = "*Required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage="Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPW { get; set; }

        public List<Like> Likes { get; set; }
        public List<Post> Posts { get; set; }
        public User() 
        {
            Likes = new List<Like>();
            Posts = new List<Post>();
        }
    }
}