using System.Threading.Tasks;
using Todo.Localization;
using Todo.MultiTenancy;
using Todo.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Todo.Web.Menus;

public class TodoMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<TodoResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                TodoMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        context.Menu.AddItem(
        new ApplicationMenuItem(
            "Todo",
            l["Todo"],
            icon: "fa fa-book"
        ).AddItem(
            new ApplicationMenuItem(
                "Todo.Provinces",
                l["Provinces"],
                url: "/Provinces"
            ).RequirePermissions(TodoPermissions.Provinces.Default)
        ).AddItem(
            new ApplicationMenuItem(
                "Todo.Districts",
                l["Districts"],
                url: "/Districts"
            ).RequirePermissions(TodoPermissions.Districts.Default)
        ).AddItem(
            new ApplicationMenuItem(
                "Todo.Communes",
                l["Communes"],
                url: "/Communes"
            ).RequirePermissions(TodoPermissions.Communes.Default)
        ));
    }
}
