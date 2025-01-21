namespace SqlSugar.Attributes.Test.Entities
{
    [SugarTable("Role")]
    public class Role
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public long RoleId { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string RoleName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
