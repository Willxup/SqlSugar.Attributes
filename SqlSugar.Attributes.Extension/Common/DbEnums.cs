namespace SqlSugar.Attributes.Extension.Common
{
    /// <summary>
    /// 时间后缀类型
    /// </summary>
    public enum DbTimeSuffixType
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        StartTime,
        /// <summary>
        /// 结束时间
        /// </summary>
        EndTime
    }
    /// <summary>
    /// 数据库操作符
    /// </summary>
    public enum DbOperator
    {
        /// <summary>
        /// 等于 =
        /// </summary>
        Equal,
        /// <summary>
        /// Like
        /// </summary>
        Like,
        /// <summary>
        /// 大于 >
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于等于 >=
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// 小于 <
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于等于 <=
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// IN
        /// </summary>
        In,
        /// <summary>
        /// NOT IN
        /// </summary>
        NotIn,
        /// <summary>
        /// LIKE LEFT
        /// </summary>
        LikeLeft,
        /// <summary>
        /// LIKE RIGHT
        /// </summary>
        LikeRight,
        /// <summary>
        /// 不等于 !=
        /// </summary>
        NoEqual,
        /// <summary>
        /// 字符串是为null或空
        /// </summary>
        IsNullOrEmpty,
        /// <summary>
        /// IS NOT
        /// </summary>
        IsNot,
        /// <summary>
        /// Not LIKE
        /// </summary>
        NoLike,
        /// <summary>
        /// IS NULL
        /// </summary>
        EqualNull,
        /// <summary>
        /// IN LIKE
        /// </summary>
        InLike
    }
    /// <summary>
    /// 数据库排序方式
    /// </summary>
    public enum DbSortWay
    {
        /// <summary>
        /// 正序
        /// </summary>
        ASC,
        /// <summary>
        /// 倒序
        /// </summary>
        DESC
    }
}
