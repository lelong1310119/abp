using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Communes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

namespace Todo.Web.Pages.Communes
{
    public class CreateModalModel : TodoPageModel
    {
        [BindProperty]
        public CreateCommuneViewModel Commune { get; set; }

        public List<SelectListItem> Provinces { get; set; }

        public List<SelectListItem> Districts { get; set; } 

        private readonly ICommuneAppService _communeAppService;

        public CreateModalModel(
            ICommuneAppService communeAppService)
        {
            _communeAppService = communeAppService;
        }

        public async Task OnGetAsync()
        {
            Commune = new CreateCommuneViewModel();
            var districtLookup = await _communeAppService.GetDistrictLookupAsync();
            Districts = districtLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            //var provinceLookup = await _communeAppService.GetProvinceLookupAsync();
            //Provinces = provinceLookup.Items
            //    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            //    .ToList();
            //Districts = provinceLookup.Items.FirstOrDefault().Districts.Items
            //    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            //    .ToList();
            //    ;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _communeAppService.CreateAsync(
                ObjectMapper.Map<CreateCommuneViewModel, CreateUpdateCommuneDto>(Commune)
                );
            return NoContent();
        }

        //public async Task OnProvinceSelected(Guid provinceId)
        //{
        //    var provinceLookup = await _communeAppService.GetProvinceLookupAsync();
        //    var province = provinceLookup.Items.FirstOrDefault(x => x.Id == provinceId);   
        //    Districts = province.Districts.Items
        //        .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
        //        .ToList();
        //}

        public class CreateCommuneViewModel
        {
            //[SelectItems(nameof(Provinces))]
            //[DisplayName("Province")]
            //public Guid ProvinceId { get; set; }

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
