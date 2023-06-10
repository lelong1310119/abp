using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Todo.Communes
{
    public class Commune : AuditedAggregateRoot<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderBy { get; set; }
        public Guid DistrictId { get; set; }
        public int? Status { get; set; } = 0;
    }
}
