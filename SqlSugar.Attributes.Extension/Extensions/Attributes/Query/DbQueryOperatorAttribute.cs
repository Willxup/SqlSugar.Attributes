using SqlSugar.Attributes.Extension.Common;
using System;

namespace SqlSugar.Attributes.Extension.Extensions.Attributes.Query
{
    /// <summary>
    /// 数据库操作符
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbQueryOperatorAttribute : Attribute
    {
        /// <summary>
        /// 操作符
        /// </summary>
        private readonly DbOperator _operateSymbol;
        /// <summary>
        /// SqlSugar操作符
        /// </summary>
        private readonly ConditionalType _operator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="operateSymbol">操作符</param>
        public DbQueryOperatorAttribute(DbOperator operateSymbol)
        {
            _operateSymbol = operateSymbol;
            _operator = ConvertDbOperator();
        }

        /// <summary>
        /// 操作符转换(转为SqlSugar)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private ConditionalType ConvertDbOperator()
        {
            return _operateSymbol switch
            {
                DbOperator.Equal => ConditionalType.Equal,
                DbOperator.Like => ConditionalType.Like,
                DbOperator.GreaterThan => ConditionalType.GreaterThan,
                DbOperator.GreaterThanOrEqual => ConditionalType.GreaterThanOrEqual,
                DbOperator.LessThan => ConditionalType.LessThan,
                DbOperator.LessThanOrEqual => ConditionalType.LessThanOrEqual,
                DbOperator.In => ConditionalType.In,
                DbOperator.NotIn => ConditionalType.NotIn,
                DbOperator.LikeLeft => ConditionalType.LikeLeft,
                DbOperator.LikeRight => ConditionalType.LikeRight,
                DbOperator.NoEqual => ConditionalType.NoEqual,
                DbOperator.IsNullOrEmpty => ConditionalType.IsNullOrEmpty,
                DbOperator.IsNot => ConditionalType.IsNot,
                DbOperator.NoLike => ConditionalType.NoLike,
                DbOperator.EqualNull => ConditionalType.EqualNull,
                DbOperator.InLike => ConditionalType.InLike,
                _ => throw new NotImplementedException($"Not Support [{_operateSymbol}] operator!")
            };
        }

        /// <summary>
        /// 获取SqlSugar操作符
        /// </summary>
        /// <returns></returns>
        public ConditionalType GetDbOperator()
        {
            return _operator;
        }
    }


}
