using SqlSugar.Attributes.Extension.Common;
using System;

namespace SqlSugar.Attributes.Extension.Extensions.Attributes.Query
{
    /// <summary>
    /// 数据库查询表字段名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbQueryFieldAttribute : DbQueryAttribute
    {
        /// <summary>
        /// 表字段名
        /// </summary>
        private readonly string _fieldName;
        /// <summary>
        /// 是否为时间查询(仅用于查询条件)
        /// </summary>
        private readonly bool _isDateQuery = false;
        /// <summary>
        /// 时间后缀类型(仅用于查询条件)
        /// </summary>
        private readonly DbTimeSuffixType _suffixType;
        /// <summary>
        /// 时间后缀(仅用于查询条件)
        /// </summary>
        private readonly string _timeSuffix = "";
        /// <summary>
        /// 是否为布尔值(仅用于查询结果)
        /// </summary>
        private readonly bool _isBoolResult = false;
        /// <summary>
        /// 当结果为布尔值时的true值(仅用于查询结果)
        /// </summary>
        private readonly int? _boolTrueValue = null;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fieldName">表字段名</param>
        public DbQueryFieldAttribute(string fieldName)
        {
            _fieldName = DbUtilities.IsNullDbFieldName(fieldName);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fieldName">表字段名</param>
        /// <param name="suffixType">日期的时间后缀类型(仅用于查询条件)，开始/结束</param>
        /// <param name="timeSuffix">日期的时间后缀(HH:mm:ss)，以修改默认时间拼接，默认拼接(00:00:00/23:59:59)</param>
        public DbQueryFieldAttribute(string fieldName, DbTimeSuffixType suffixType, string timeSuffix = "")
        {
            _isDateQuery = true;
            _fieldName = DbUtilities.IsNullDbFieldName(fieldName);
            _suffixType = suffixType;
            _timeSuffix = timeSuffix;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fieldName">表字段名称</param>
        /// <param name="isBoolResult">是否为布尔值(仅用于查询结果)</param>
        /// <param name="boolTrueValue">布尔[true]值(当数据库与传入值相同时为TRUE，默认1为true)[注：数据库如果[0否1是]可直接转换，使用上面的构造即可]</param>
        public DbQueryFieldAttribute(string fieldName, bool isBoolResult, int boolTrueValue = 1)
        {
            _fieldName = fieldName;
            _isBoolResult = isBoolResult;
            _boolTrueValue = boolTrueValue;
        }

        /// <summary>
        /// 获取表字段名
        /// </summary>
        /// <returns></returns>
        public string GetFieldName()
        {
            return _fieldName;
        }
        /// <summary>
        /// 是否为时间(仅用于查询条件)
        /// </summary>
        /// <returns></returns>
        public bool IsDateQuery()
        {
            return _isDateQuery;
        }
        /// <summary>
        /// 获取时间后缀类型(仅用于查询条件)
        /// </summary>
        /// <returns></returns>
        public DbTimeSuffixType GetTimeSuffixType()
        {
            return _suffixType;
        }
        /// <summary>
        /// 获取时间后缀(仅用于查询条件)
        /// </summary>
        /// <returns></returns>
        public string GetTimeSuffix()
        {
            return _timeSuffix;
        }
        /// <summary>
        /// 是否为布尔值(仅用于查询结果)
        /// </summary>
        /// <returns></returns>
        public bool IsBoolResult()
        {
            return _isBoolResult;
        }
        /// <summary>
        /// 获取布尔[true]值(仅用于查询结果)
        /// </summary>
        /// <returns></returns>
        public int? GetBoolTrueValue()
        {
            return _boolTrueValue;
        }
    }
}
