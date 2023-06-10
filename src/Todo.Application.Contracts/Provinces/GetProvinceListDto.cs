using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Todo.Provinces
{
    public class GetProvinceListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; } = "";
    }
}
