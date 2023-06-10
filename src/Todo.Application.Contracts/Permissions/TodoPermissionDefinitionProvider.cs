using Todo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Todo.Permissions;

public class TodoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var todoGroup = context.AddGroup(TodoPermissions.GroupName, L("Permission:Todo"));

        var ProvincesPermission = todoGroup.AddPermission(TodoPermissions.Provinces.Default, L("Permission:Provinces"));
        ProvincesPermission.AddChild(TodoPermissions.Provinces.Create, L("Permission:Provinces.Create"));
        ProvincesPermission.AddChild(TodoPermissions.Provinces.Edit, L("Permission:Provinces.Edit"));
        ProvincesPermission.AddChild(TodoPermissions.Provinces.Delete, L("Permission:Provinces.Delete"));
        ProvincesPermission = todoGroup.AddPermission(TodoPermissions.Districts.Default, L("Permission:Districts"));
        ProvincesPermission.AddChild(TodoPermissions.Districts.Create, L("Permission:Districts.Create"));
        ProvincesPermission.AddChild(TodoPermissions.Districts.Edit, L("Permission:Districts.Edit"));
        ProvincesPermission.AddChild(TodoPermissions.Districts.Delete, L("Permission:Districts.Delete"));
        ProvincesPermission = todoGroup.AddPermission(TodoPermissions.Communes.Default, L("Permission:Communes"));
        ProvincesPermission.AddChild(TodoPermissions.Communes.Create, L("Permission:Communes.Create"));
        ProvincesPermission.AddChild(TodoPermissions.Communes.Edit, L("Permission:Communes.Edit"));
        ProvincesPermission.AddChild(TodoPermissions.Communes.Delete, L("Permission:Communes.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TodoResource>(name);
    }
}
