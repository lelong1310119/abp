using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Communes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

namespace Todo.Web.Pages.Communes
{
    public class EditModalModel : TodoPageModel
    {
        [BindProperty]
        public EditCommuneViewModel Commune { get; set; }

        public List<SelectListItem> Districts { get; set; }


        private readonly ICommuneAppService _communeAppService;

        public EditModalModel(ICommuneAppService communeAppService)
        {
            _communeAppService = communeAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var CommuneDto = await _communeAppService.GetAsync(id);
            Commune = ObjectMapper.Map<CommuneDto, EditCommuneViewModel>(CommuneDto);

            var districtLookup = await _communeAppService.GetDistrictLookupAsync();
            Districts = districtLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _communeAppService.UpdateAsync(
                Commune.Id,
                ObjectMapper.Map<EditCommuneViewModel, CreateUpdateCommuneDto>(Commune)
            );

            return NoContent();
        }

        public class EditCommuneViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [SelectItems(nameof(Districts))]
            [DisplayName("District")]
            public Guid DistrictId { get; set; }

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
