using System;
using System.Collections.Generic;
using System.Text;
using Todo.Communes;
using Volo.Abp.Application.Dtos;

namespace Todo.Districts
{
    public class ProvinceLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public ListResultDto<DistrictLookupDto> Districts { get; set; }
    }
}
