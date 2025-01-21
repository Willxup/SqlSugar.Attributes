using SqlSugar.Attributes.Extension.Common;
using SqlSugar.Attributes.Extension.Extensions.Attributes.Operation;
using SqlSugar.Attributes.Extension.Extensions.Attributes.Query;

namespace SqlSugar.Attributes.Test.Models
{

    public class UserPageSearch : DbQueryModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("UserName")]
        [DbQueryOperator(DbOperator.Like)]
        public string Name { get; set; }
        [DbTableAlias("user")]
        [DbQueryField("Email")]
        [DbQueryOperator(DbOperator.Like)]
        public string Email { get; set; }
    }
    public class UserPageResult
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("UserId")]
        public long? Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("UserName")]
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("Email")]
        public string Email { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        [DbTableAlias("role")]
        [DbQueryField("RoleName")]
        public string RoleName { get; set; }
    }

    public class InsertUserDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DbOperationField("UserName")]
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DbOperationField("Email", false, true)]
        public string Email { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
    public class UpdateUserDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DbOperationField("UserId", true)]
        public long? Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DbOperationField("UserName")]
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DbOperationField("Email", false ,true)]
        public string Email { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
