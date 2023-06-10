namespace Todo.Permissions;

public static class TodoPermissions
{
    public const string GroupName = "Todo";

    public static class Provinces
    {
        public const string Default = GroupName + ".Provinces";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Districts
    {
        public const string Default = GroupName + ".Districts";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Communes
    {
        public const string Default = GroupName + ".Communes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
