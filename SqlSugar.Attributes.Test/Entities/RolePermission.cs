namespace SqlSugar.Attributes.Test.Entities
{
    /// <summary>
    /// Role Permission
    /// </summary>
    [SugarTable("RolePermission")]
    public class RolePermission
    {
        /// <summary>
        /// Role Permission Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int RolePermissionId { get; set; }
        /// <summary>
        /// Role Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int RoleId { get; set; }
        /// <summary>
        /// Module Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int ModuleId { get; set; }
        /// <summary>
        /// Role permission name
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string RolePermissionName { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
