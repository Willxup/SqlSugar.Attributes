using SqlSugar.Attributes.Test.Entities;

namespace SqlSugar.Attributes.Test
{
    /// <summary>
    /// Initial Data
    /// </summary>
    public static class InitData
    {
        public static void Init()
        {
            SqlSugarScope db = DbContext.GetDb();
            
            db.DbMaintenance.CreateDatabase();

            db.CodeFirst.InitTables<User, Module, Role, RolePermission, UserPermission>();
            db.DbMaintenance.TruncateTable<User, Module, Role, RolePermission, UserPermission>();

            db.Insertable(new List<User>
            {
                new() { UserName = "Test1", Email = "test@willxup.top", CreateTime = DateTime.Now },
                new() {  UserName = "Test2", Email = "test@willxup.top", CreateTime = DateTime.Now }
            })
            .ExecuteCommand();

            db.Insertable(new List<Module>
            {
                new() { ModuleName = "Module1" },
                new() { ModuleName = "Module2" },
                new() { ModuleName = "Module3" },
                new() { ModuleName = "Module4" },
                new() { ModuleName = "Module5" },
                new() { ModuleName = "Module6" },
                new() { ModuleName = "Module7" },
                new() { ModuleName = "Module8" },
                new() { ModuleName = "Module9" },
                new() { ModuleName = "Module10" }
            })
            .ExecuteCommand();

            db.Insertable(new List<Role> 
            {
                new() { RoleName = "Role1" },
                new() { RoleName = "Role2" },
                new() { RoleName = "Role3" },
                new() { RoleName = "Role4" }
            }).ExecuteCommand();

            db.Insertable(new List<RolePermission>
            {
                new() {RoleId = 1, ModuleId = 1, RolePermissionName = "Role1-Module1" },
                new() {RoleId = 1, ModuleId = 2, RolePermissionName = "Role1-Module2" },
                new() {RoleId = 1, ModuleId = 3, RolePermissionName = "Role1-Module3" },
                new() {RoleId = 1, ModuleId = 4, RolePermissionName = "Role1-Module4" },
                new() {RoleId = 1, ModuleId = 5, RolePermissionName = "Role1-Module5" },

                new() {RoleId = 2, ModuleId = 6, RolePermissionName = "Role2-Module6" },
                new() {RoleId = 2, ModuleId = 7, RolePermissionName = "Role2-Module7" },
                new() {RoleId = 2, ModuleId = 8, RolePermissionName = "Role2-Module8" },
                new() {RoleId = 2, ModuleId = 9, RolePermissionName = "Role2-Module9" },
                new() {RoleId = 2, ModuleId = 10, RolePermissionName = "Role2-Module10" },

                new() {RoleId = 3, ModuleId = 1, RolePermissionName = "Role3-Module1" },
                new() {RoleId = 3, ModuleId = 3, RolePermissionName = "Role3-Module3" },
                new() {RoleId = 3, ModuleId = 5, RolePermissionName = "Role3-Module5" },
                new() {RoleId = 3, ModuleId = 7, RolePermissionName = "Role3-Module7" },
                new() {RoleId = 3, ModuleId = 9, RolePermissionName = "Role3-Module9" },

                new() {RoleId = 4, ModuleId = 2, RolePermissionName = "Role4-Module2" },
                new() {RoleId = 4, ModuleId = 4, RolePermissionName = "Role4-Module4" },
                new() {RoleId = 4, ModuleId = 6, RolePermissionName = "Role4-Module6" },
                new() {RoleId = 4, ModuleId = 8, RolePermissionName = "Role4-Module8" },
                new() {RoleId = 4, ModuleId = 10, RolePermissionName = "Role4-Module10" }
            })
            .ExecuteCommand();

            db.Insertable(new List<UserPermission>
            {
                new() { UserId = 1, Type = 0, CommonId = 1 },
                new() { UserId = 1, Type = 0, CommonId = 2 },

                new() { UserId = 2, Type = 0, CommonId = 3 },
                new() { UserId = 2, Type = 0, CommonId = 4 },
            })
            .ExecuteCommand();



        }
    }
}
