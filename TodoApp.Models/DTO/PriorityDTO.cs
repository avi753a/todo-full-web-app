using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class PriorityDTO
    {
        [Required,MinLength(3)]
        public string Name { get; set; }
        [Required,MinLength(5)]
        public string Description { get; set; }
        [Required]
        public int Value { get; set; }
        public string Colour { get; set; }
    }
}
