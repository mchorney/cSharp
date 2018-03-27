using System;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models
{
    public class Like : BaseEntity 
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public User Liker { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}