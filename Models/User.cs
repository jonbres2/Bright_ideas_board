using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bright_ideas_board.Models
{
    public class User
    {
        [Key]
        public int UserID {get; set;}

        [Required]
        [MinLength(2)]
        [RegularExpression("^[A-Za-z ]+$")]
        [Display(Name=" Name: ")]
        public string Name {get;set;}

        [Required]
        [MinLength(2)]
        [RegularExpression("^[A-Za-z ]+$")]
        [Display(Name="Alias: ")]
        public string Alias {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name="Email: ")]
        public string Email {get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name="Password: ")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password: ")]
        public string ConfirmPassword {get;set;}

        // One to many -- User can create many Ideas
        public List<Idea> CreatedIdeas {get;set;}

        // Many to Many -- Many users can Like Many Ideas
        public List<Like> LikedIdeas {get;set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

    }
}