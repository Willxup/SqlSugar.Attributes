using System;

namespace SqlSugar.Attributes.Extension.Extensions.Attributes.Query
{
    /// <summary>
    /// 数据库分组
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbGroupByAttribute : Attribute
    {
        /// <summary>
        /// 表别名
        /// </summary>
        private readonly string _tableAlias;
        /// <summary>
        /// 表字段名
        /// </summary>
        private readonly string _fieldName;
        /// <summary>
        /// 是否使用表字段查询特性
        /// </summary>
        private readonly bool _isUseQueryFieldAttribute;

        /// <summary>
        /// 构造
        /// </summary>
        public DbGroupByAttribute()
        {
            _isUseQueryFieldAttribute = true;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="feildName">表字段名称</param>
        public DbGroupByAttribute(string feildName)
        {
            _fieldName = feildName;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableAlias">表别名</param>
        /// <param name="fieldName">表字段名称</param>
        public DbGroupByAttribute(string tableAlias, string fieldName)
        {
            _tableAlias = tableAlias;
            _fieldName = fieldName;
        }

        /// <summary>
        /// 是否使用表字段查询特性的参数
        /// </summary>
        /// <returns></returns>
        public bool IsUseQueryFieldAttribute()
        {
            return _isUseQueryFieldAttribute;
        }

        /// <summary>
        /// 获取表别名
        /// </summary>
        /// <returns></returns>
        public string GetTableAlias()
        {
            return _tableAlias;
        }

        /// <summary>
        /// 获取表字段名
        /// </summary>
        /// <returns></returns>
        public string GetFieldName()
        {
            return _fieldName;
        }
    }
}
