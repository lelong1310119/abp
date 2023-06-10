using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Todo.Provinces
{
    public class ProvinceDto : AuditedEntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AreaCode { get; set; }
        public int OrderBy { get; set; }
        public int Status { get; set; }
    }
}
