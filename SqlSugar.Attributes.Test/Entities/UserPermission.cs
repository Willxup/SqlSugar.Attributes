namespace SqlSugar.Attributes.Test.Entities
{
    /// <summary>
    /// User Permission
    /// </summary>
    [SugarTable("UserPermission")]
    public class UserPermission
    {
        /// <summary>
        /// User Permission Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int UserPermissionId { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int UserId { get; set; }
        /// <summary>
        /// Type 0Role 1Module
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public byte Type { get; set; }
        /// <summary>
        /// RoleId / PermissionId
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int CommonId { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
