using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameTracker.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public IEnumerable<string> Platforms{ get; set; }
    }

    
}
