using SqlSugar.Attributes.Test.Entities;
using System.ComponentModel.Design;

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
                new() { UserName = "测试1", Email = "test@willxup.top", CreateTime = DateTime.Now },
                new() {  UserName = "测试2", Email = "test@willxup.top", CreateTime = DateTime.Now }
            })
            .ExecuteCommand();

            db.Insertable(new List<Module>
            {
                new() { ModuleName = "模块1" },
                new() { ModuleName = "模块2" },
                new() { ModuleName = "模块3" },
                new() { ModuleName = "模块4" },
                new() { ModuleName = "模块5" },
                new() { ModuleName = "模块6" },
                new() { ModuleName = "模块7" },
                new() { ModuleName = "模块8" },
                new() { ModuleName = "模块9" },
                new() { ModuleName = "模块10" }
            })
            .ExecuteCommand();

            db.Insertable(new List<Role> 
            {
                new() { RoleName = "角色1" },
                new() { RoleName = "角色2" },
                new() { RoleName = "角色3" },
                new() { RoleName = "角色4" }
            }).ExecuteCommand();

            db.Insertable(new List<RolePermission>
            {
                new() {RoleId = 1, ModuleId = 1, RolePermissionName = "角色1-模块1" },
                new() {RoleId = 1, ModuleId = 2, RolePermissionName = "角色1-模块2" },
                new() {RoleId = 1, ModuleId = 3, RolePermissionName = "角色1-模块3" },
                new() {RoleId = 1, ModuleId = 4, RolePermissionName = "角色1-模块4" },
                new() {RoleId = 1, ModuleId = 5, RolePermissionName = "角色1-模块5" },

                new() {RoleId = 2, ModuleId = 6, RolePermissionName = "角色2-模块6" },
                new() {RoleId = 2, ModuleId = 7, RolePermissionName = "角色2-模块7" },
                new() {RoleId = 2, ModuleId = 8, RolePermissionName = "角色2-模块8" },
                new() {RoleId = 2, ModuleId = 9, RolePermissionName = "角色2-模块9" },
                new() {RoleId = 2, ModuleId = 10, RolePermissionName = "角色2-模块10" },

                new() {RoleId = 3, ModuleId = 1, RolePermissionName = "角色3-模块1" },
                new() {RoleId = 3, ModuleId = 3, RolePermissionName = "角色3-模块3" },
                new() {RoleId = 3, ModuleId = 5, RolePermissionName = "角色3-模块5" },
                new() {RoleId = 3, ModuleId = 7, RolePermissionName = "角色3-模块7" },
                new() {RoleId = 3, ModuleId = 9, RolePermissionName = "角色3-模块9" },

                new() {RoleId = 4, ModuleId = 2, RolePermissionName = "角色4-模块2" },
                new() {RoleId = 4, ModuleId = 4, RolePermissionName = "角色4-模块4" },
                new() {RoleId = 4, ModuleId = 6, RolePermissionName = "角色4-模块6" },
                new() {RoleId = 4, ModuleId = 8, RolePermissionName = "角色4-模块8" },
                new() {RoleId = 4, ModuleId = 10, RolePermissionName = "角色4-模块10" }
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
