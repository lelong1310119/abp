using Todo.Provinces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Districts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

namespace Todo.Web.Pages.Districts
{
    public class EditModalModel : TodoPageModel
    {
        [BindProperty]
        public EditDistrictViewModel District { get; set; }

        public List<SelectListItem> Provinces { get; set; }

        private readonly IDistrictAppService _districtAppService;

        public EditModalModel(IDistrictAppService districtAppService)
        {
            _districtAppService = districtAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var DistrictDto = await _districtAppService.GetAsync(id);
            District = ObjectMapper.Map<DistrictDto, EditDistrictViewModel>(DistrictDto);

            var provinceLookup = await _districtAppService.GetProvinceLookupAsync();
            Provinces = provinceLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _districtAppService.UpdateAsync(
                District.Id,
                ObjectMapper.Map<EditDistrictViewModel, CreateUpdateDistrictDto>(District)
            );

            return NoContent();
        }

        public class EditDistrictViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

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
