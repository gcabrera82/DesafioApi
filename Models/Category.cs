using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioApi.Models
{
    public class Category
    {
        [Key]
        public int IdCategory    { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
