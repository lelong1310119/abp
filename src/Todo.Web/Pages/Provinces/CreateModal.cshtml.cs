using System.Threading.Tasks;
using Todo.Provinces;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Web.Pages.Provinces
{
    public class CreateModalModel : TodoPageModel
    {
        [BindProperty]
        public CreateUpdateProvinceDto Province { get; set; }

        private readonly IProvinceAppService _provinceAppService;

        public CreateModalModel(IProvinceAppService provinceAppService)
        {
            _provinceAppService = provinceAppService;
        }

        public void OnGet()
        {
            Province = new CreateUpdateProvinceDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _provinceAppService.CreateAsync(Province);
            return NoContent();
        }
    }
}
