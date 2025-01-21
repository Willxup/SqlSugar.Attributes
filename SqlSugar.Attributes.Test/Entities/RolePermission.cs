namespace SqlSugar.Attributes.Test.Entities
{
    [SugarTable("RolePermission")]
    public class RolePermission
    {
        /// <summary>
        /// 角色权限Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public long RolePermissionId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long RoleId { get; set; }
        /// <summary>
        /// 模块Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long ModuleId { get; set; }
        /// <summary>
        /// 角色权限名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string RolePermissionName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
