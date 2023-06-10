using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Districts
{
    public class ExportDistrictDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderBy { get; set; }
        public string ProvinceName { get; set; } 
    }
}
