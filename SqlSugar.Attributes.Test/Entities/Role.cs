namespace SqlSugar.Attributes.Test.Entities
{
    /// <summary>
    /// Role
    /// </summary>
    [SugarTable("Role")]
    public class Role
    {
        /// <summary>
        /// Role Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int RoleId { get; set; }
        /// <summary>
        /// Role name
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string RoleName { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
