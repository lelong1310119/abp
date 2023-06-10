using System.Threading.Tasks;
using Todo.Provinces;
using Microsoft.AspNetCore.Mvc;
using Todo.Districts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

namespace Todo.Web.Pages.Districts
{
    public class CreateModalModel : TodoPageModel
    {
        [BindProperty]
        public CreateDistrictViewModel District { get; set; }

        public List<SelectListItem> Provinces { get; set; }

        private readonly IDistrictAppService _districtAppService;

        public CreateModalModel(
            IDistrictAppService districtAppService)
        {
            _districtAppService = districtAppService;
        }

        public async Task OnGetAsync()
        {
            District = new CreateDistrictViewModel();

            var provinceLookup = await _districtAppService.GetProvinceLookupAsync();
            Provinces = provinceLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _districtAppService.CreateAsync(
                ObjectMapper.Map<CreateDistrictViewModel, CreateUpdateDistrictDto>(District)
                );
            return NoContent();
        }

        public class CreateDistrictViewModel
        {
            [SelectItems(nameof(Provinces))]
            [DisplayName("Province")]
            public Guid ProvinceId { get; set; }

            [Required]
            public string Code { get; set; }

            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            public int OrderBy { get; set; }
        }
    }
}
