using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bright_ideas_board.Models
{
    public class Like
    {
        [Key]
        public int LikeID {get;set;}
        public int UserID {get;set;}
        public int IdeaID {get;set;}

        public User Liker {get;set;}
        public Idea LikedIdea {get;set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}