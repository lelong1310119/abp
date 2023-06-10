using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Todo.Provinces
{
    public class CreateUpdateProvinceDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AreaCode { get; set; }

        [Required]
        public int OrderBy { get; set; }
    }
}
