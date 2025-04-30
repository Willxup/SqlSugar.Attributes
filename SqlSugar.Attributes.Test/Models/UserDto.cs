using SqlSugar.Attributes.Extension.Common;
using SqlSugar.Attributes.Extension.Extensions.Attributes.Operation;
using SqlSugar.Attributes.Extension.Extensions.Attributes.Query;

namespace SqlSugar.Attributes.Test.Models
{
    /// <summary>
    /// User Query Condition Dto
    /// </summary>
    public class UserPageSearch : DbQueryModel
    {
        /// <summary>
        /// Name
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("UserName")]
        [DbQueryOperator(DbOperator.Like)]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("Email")]
        [DbQueryOperator(DbOperator.Like)]
        public string Email { get; set; }
    }

    /// <summary>
    /// User Query Result Dto
    /// </summary>
    public class UserPageResult
    {
        /// <summary>
        /// User Id
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("UserId")]
        public int? Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("UserName")]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DbTableAlias("user")]
        [DbQueryField("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [DbTableAlias("role")]
        [DbQueryField("RoleName")]
        public string RoleName { get; set; }
    }

    /// <summary>
    /// User Insert Dto
    /// </summary>
    public class InsertUserDto
    {
        /// <summary>
        /// UserId
        /// </summary>
        [DbOperationField("UserName")]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DbOperationField("Email", false, true)]
        public string Email { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }

    /// <summary>
    /// User Update Dto
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// UserId
        /// </summary>
        [DbOperationField("UserId", true)]
        public int? Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DbOperationField("UserName")]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DbOperationField("Email", false, true)]
        public string Email { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}