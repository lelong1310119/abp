using Todo.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Todo.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TodoPageModel : AbpPageModel
{
    protected TodoPageModel()
    {
        LocalizationResourceType = typeof(TodoResource);
    }
}
