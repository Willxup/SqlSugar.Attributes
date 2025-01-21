namespace SqlSugar.Attributes.Test.Entities
{
    [SugarTable("UserPermission")]
    public class UserPermission
    {
        /// <summary>
        /// 用户权限Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public long UserPermissionId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long UserId { get; set; }
        /// <summary>
        /// 类型 0角色 1模块
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public byte Type { get; set; }
        /// <summary>
        /// 角色Id/权限Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long CommonId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
