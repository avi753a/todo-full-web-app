using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class StatusDTO
    {
        [Required,MinLength(3)]
        public string Name { get; set; }
        [Required,MinLength(3)]
        public string Description { get; set; }
    }
}
