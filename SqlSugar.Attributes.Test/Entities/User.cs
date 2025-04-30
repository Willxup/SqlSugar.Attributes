namespace SqlSugar.Attributes.Test.Entities
{
    /// <summary>
    /// User
    /// </summary>
    [SugarTable("User")]
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int UserId { get; set; }
        /// <summary>
        /// User name
        /// 用户名
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string UserName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Email { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime CreateTime { get; set; }
    }
}
