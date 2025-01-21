namespace SqlSugar.Attributes.Test.Entities
{
    [SugarTable("User")]
    public class User
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public long UserId { get; set; }
        /// <summary>
        /// User name
        /// 用户名
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string UserName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Email { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime CreateTime { get; set; }
    }
}
