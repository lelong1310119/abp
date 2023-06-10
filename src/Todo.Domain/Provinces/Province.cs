using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Todo.Provinces
{
    public class Province : AuditedAggregateRoot<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AreaCode { get; set; }
        public int OrderBy { get; set; }
        public int? Status { get; set; } = 0;
    }
}
