using Todo.Provinces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Todo.Web.Pages.Provinces
{
    public class EditModalModel : TodoPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProvinceDto Province { get; set; }

        private readonly IProvinceAppService _provinceAppService;

        public EditModalModel (IProvinceAppService provinceAppService)
        {
            _provinceAppService = provinceAppService;
        }

        public async Task OnGetAsync()
        {
            var provinceDto = await _provinceAppService.GetAsync(Id);
            Province = ObjectMapper.Map<ProvinceDto, CreateUpdateProvinceDto>(provinceDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _provinceAppService.UpdateAsync(Id, Province);
            return NoContent();
        }
    }
}
