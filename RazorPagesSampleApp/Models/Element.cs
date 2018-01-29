using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSampleApp.Models
{
    public class Element
    {
        public Element()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }
        public bool IsStarred { get; set; }
        public bool WasStarred { get; set; }
        public bool IsCrossed { get; set; }
    }
}