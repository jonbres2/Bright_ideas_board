using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpExam2.Models
{
    public class Idea
    {
        [Key]
        public int IdeaID {get; set;}

        [Required]
        [MinLength(5)]
        [Display(Name=" Post a New Idea! ")]
        public string Description {get;set;}

        // One to Many - Idea only has one Creator
        public int UserID {get;set;}
        public User Creator {get;set;}

        // Many to Many - Ideas have many Likes
        public List<Like> IdeaLikes {get;set;}

        public int TotalLikes {get;set;} = 0;
        
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}