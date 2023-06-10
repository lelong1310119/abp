using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Provinces
{
    public class ExportProvinceDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AreaCode { get; set; }
        public string Description { get; set; }
        public int OrderBy { get; set; }
    }
}
