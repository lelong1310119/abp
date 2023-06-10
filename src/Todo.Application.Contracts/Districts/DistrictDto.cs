using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Todo.Districts
{
    public class DistrictDto : AuditedEntityDto<Guid>
    {
        public Guid ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderBy { get; set; }
        public int Status { get; set; }
    }
}
