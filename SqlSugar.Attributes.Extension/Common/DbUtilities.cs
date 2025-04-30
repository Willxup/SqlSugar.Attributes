namespace SqlSugar.Attributes.Extension.Common
{
    internal static class DbUtilities
    {
        /// <summary>
        /// 检查数据库字段名是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="GlobalException"></exception>
        internal static string IsNullDbFieldName(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                throw new GlobalException("数据库字段名不能为空!");
            }
        }
        /// <summary>
        /// 是否为数据库排序方式
        /// </summary>
        /// <param name="sortWay"></param>
        /// <returns></returns>
        internal static bool IsDbSortWay(string sortWay)
        {
            return sortWay.ToUpper() == nameof(DbSortWay.ASC) || sortWay.ToUpper() == nameof(DbSortWay.DESC);
        }
    }
}
