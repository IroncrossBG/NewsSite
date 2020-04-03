using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Input
{
    public class CreateEditCategoryInputModel
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, MinLength(10)]
        public string Description { get; set; }
    }
}
