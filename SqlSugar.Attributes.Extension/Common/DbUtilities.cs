using System;

namespace SqlSugar.Attributes.Extension.Common
{
    internal static class DbUtilities
    {
        /// <summary>
        /// 检查数据库字段名是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="UserOperationException"></exception>
        internal static string IsNullDbFieldName(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                throw new Exception("数据库字段名不能为空!");
            }
        }
        /// <summary>
        /// 是否为数据库排序方式
        /// </summary>
        /// <param name="sortway"></param>
        /// <returns></returns>
        internal static bool IsDbSortWay(string sortway)
        {
            if (sortway.ToUpper() == DbSortWay.ASC.ToString() || sortway.ToUpper() == DbSortWay.DESC.ToString())
            {
                return true;
            }
            return false;
        }
    }
}
