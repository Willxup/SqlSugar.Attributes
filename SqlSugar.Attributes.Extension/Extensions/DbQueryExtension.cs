using SqlSugar.Attributes.Extension.Common;
using SqlSugar.Attributes.Extension.Extensions.Attributes.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SqlSugar.Attributes.Extension.Extensions
{
    public static class DbQueryExtension
    {

        #region 私有方法
        /// <summary>
        /// 获取Array的所有元素
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static string[] GetArrayElements(Array array)
        {
            string[] elements = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                elements[i] = array.GetValue(i).ToString().ToSqlFilter();
            }
            return elements;
        }
        /// <summary>
        /// 获取List的所有元素
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetListElements(IEnumerable list)
        {
            foreach (var item in list)
            {
                yield return item.ToString().ToSqlFilter();
            }
        }
        /// <summary>
        /// 获取查询条件参数
        /// </summary>
        /// <typeparam name="TSearch"></typeparam>
        /// <param name="search"></param>
        /// <returns></returns>
        /// <exception cref="GlobalException"></exception>
        private static List<IConditionalModel> GetWhereParameters<TSearch>(this TSearch search)
        {
            List<IConditionalModel> conditions = new List<IConditionalModel>();

            //获取查询条件模型属性
            var props = search.GetType().GetProperties();

            if (props?.Length > 0)
            {
                foreach (var prop in props)
                {
                    #region 参数校验
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                        continue;
                    #endregion

                    #region 获取参数值
                    //获取属性值
                    var value = prop.GetValue(search);

                    //null校验
                    if (value is null)
                        continue;

                    //空字符串校验
                    if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
                        continue;
                    #endregion

                    //条件
                    ConditionalModel condition = new ConditionalModel();

                    //是否为日期查询，用于string类型
                    bool isDateQuery = false;
                    string timeSuffix = "";

                    #region 表字段获取
                    //表别名
                    if (prop.IsDefined(typeof(DbTableAliasAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbTableAliasAttribute), true)[0] as DbTableAliasAttribute;
                        condition.FieldName += attr.GetTableAlias() + ".";
                    }

                    //表字段名
                    if (prop.IsDefined(typeof(DbQueryFieldAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbQueryFieldAttribute), true)[0] as DbQueryFieldAttribute;
                        condition.FieldName += attr.GetFieldName();

                        //日期查询
                        if (attr.IsDateQuery())
                        {
                            isDateQuery = true;
                            if (attr.GetTimeSuffixType() == DbTimeSuffixType.StartTime)
                            {
                                timeSuffix = !string.IsNullOrWhiteSpace(attr.GetTimeSuffix()) ? attr.GetTimeSuffix() : "00:00:00";
                            }
                            else if (attr.GetTimeSuffixType() == DbTimeSuffixType.EndTime)
                            {
                                timeSuffix = !string.IsNullOrWhiteSpace(attr.GetTimeSuffix()) ? attr.GetTimeSuffix() : "23:59:59";
                            }
                            else
                            {
                                throw new GlobalException($"[{prop.Name}]配置的时间后缀类型有误!");
                            }
                        }
                    }
                    //未配置DbQueryField，直接取字段名称
                    else
                    {
                        condition.FieldName += prop.Name;
                    }
                    #endregion

                    #region 操作符
                    if (prop.IsDefined(typeof(DbQueryOperatorAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbQueryOperatorAttribute), true)[0] as DbQueryOperatorAttribute;
                        condition.ConditionalType = attr.GetDbOperator();
                    }
                    else
                        throw new GlobalException($"请配置[{prop.Name}]操作符!");
                    #endregion

                    #region 参数赋值
                    //字段类型
                    Type propType = prop.PropertyType;

                    //字符串
                    if (propType == typeof(string))
                    {
                        condition.CSharpTypeName = typeof(string)?.Name;
                        condition.FieldValue = ((string)value).ToSqlFilter();

                        //日期查询
                        if (isDateQuery)
                        {
                            condition.FieldValue += $" {timeSuffix}".ToSqlFilter();
                        }
                    }
                    //时间
                    else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                    {
                        if (isDateQuery)
                        {
                            condition.CSharpTypeName = typeof(string)?.Name;
                            condition.FieldValue = $"{((DateTime)value).ToFormattedString("yyyy-MM-dd")} {timeSuffix}".ToSqlFilter();
                        }
                        else
                        {
                            condition.CSharpTypeName = typeof(DateTime)?.Name;
                            condition.FieldValue = ((DateTime)value).ToFormattedString();
                        }
                    }
                    //布尔值
                    else if (propType == typeof(bool) || propType == typeof(bool?))
                    {
                        condition.CSharpTypeName = typeof(bool)?.Name;
                        condition.FieldValue = ((bool)value).ToString();
                    }
                    //基础类型
                    else if (propType.IsPrimitive)
                    {
                        condition.CSharpTypeName = propType.Name;
                        condition.FieldValue = value + "";
                    }
                    //数组
                    else if (value is Array array)
                    {
                        condition.CSharpTypeName = propType.GetElementType()?.Name; //获取泛型类型
                        condition.FieldValue = string.Join(",", GetArrayElements(array));
                    }
                    //集合
                    else if (value is IEnumerable enumerable)
                    {
                        condition.CSharpTypeName = propType.GetGenericArguments()?[0]?.Name; //获取泛型类型
                        condition.FieldValue = string.Join(",", GetListElements(enumerable));
                    }
                    //其他类型
                    else
                    {
                        condition.CSharpTypeName = propType.Name;
                        condition.FieldValue = value.ToString().ToSqlFilter();
                    }
                    #endregion

                    conditions.Add(condition);
                }
            }

            return conditions;
        }
        /// <summary>
        /// 获取查询结果参数SQL
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="GlobalException"></exception>
        private static string GetSelectSQL<TResult>(this TResult result)
        {
            StringBuilder select = new StringBuilder();

            // 获取查询模型属性
            var props = result.GetType().GetProperties();

            if (props?.Length > 0)
            {
                foreach (var prop in props)
                {
                    #region 参数校验
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                        continue;

                    //校验是否多次标记
                    if (prop.GetCustomAttributes(typeof(DbQueryAttribute), true).Length > 1)
                        throw new GlobalException("查询字段特性存在多个!");
                    #endregion

                    string sql = string.Empty;

                    //表别名
                    if (prop.IsDefined(typeof(DbTableAliasAttribute), true))
                    {

                        var attr = prop.GetCustomAttributes(typeof(DbTableAliasAttribute), true)[0] as DbTableAliasAttribute;

                        //校验子查询
                        if (prop.IsDefined(typeof(DbSubQueryAttribute), true))
                            throw new GlobalException("使用[DbSubQueryAttribute]子查询时，请去除[DbTableAliasAttribute]!");


                        sql += "`" + attr.GetTableAlias() + "`" + ".";
                    }

                    //表字段名
                    if (prop.IsDefined(typeof(DbQueryFieldAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbQueryFieldAttribute), true)[0] as DbQueryFieldAttribute;

                        string fieldName = attr.GetFieldName();

                        if (fieldName.ToUpper().Contains("SELECT") || fieldName.ToUpper().Contains("FROM") || fieldName.ToUpper().Contains("WHERE"))
                        {
                            throw new GlobalException("请使用[DbSubQueryAttribute]子查询!");
                        }

                        //如果为布尔值结果(查询结果)，转换结果
                        if (attr.IsBoolResult())
                        {
                            if (prop.PropertyType != typeof(bool) && prop.PropertyType != typeof(bool?))
                                throw new GlobalException($"[{prop.Name}]属性类型必须为布尔值类型才可使用[IsBoolResult]功能!");

                            sql = "IF(" + sql + "`" + fieldName + "`" + $" = {attr.GetBoolTrueValue()}, " + "TRUE, FALSE)";
                        }
                        else
                        {
                            sql += "`" + fieldName + "`";
                        }
                    }
                    //子查询
                    else if (prop.IsDefined(typeof(DbSubQueryAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbSubQueryAttribute), true)[0] as DbSubQueryAttribute;

                        sql += "(" + attr.GetSubQuery() + ")";
                    }
                    // 未配置DbQueryField和DbSubQuery，直接取字段名称
                    else
                    {
                        sql += "`" + prop.Name + "`";
                    }

                    //别名 表字段或子查询
                    if (!string.IsNullOrEmpty(sql))
                    {
                        sql += " AS " + "`" + prop.Name + "`" + ", ";

                        select.Append(sql);
                    }
                }
            }

            //校验是否存在SELECT内容
            if (select.Length > 0)
            {
                return select.Remove(select.Length - 2, 2).ToString();
            }

            return string.Empty;
        }
        /// <summary>
        /// 获取查询分组SQL
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="GlobalException"></exception>
        public static string GetGroupBySql<TResult>(this TResult result)
        {
            StringBuilder groupby = new StringBuilder();

            // 获取查询模型属性
            var props = result.GetType().GetProperties();

            if (props?.Length > 0)
            {
                foreach (var prop in props)
                {
                    #region 参数校验
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                        continue;
                    #endregion

                    string sql = string.Empty;

                    if (prop.IsDefined(typeof(DbGroupByAttribute), true))
                    {
                        var groupByAttr = prop.GetCustomAttributes(typeof(DbGroupByAttribute), true)[0] as DbGroupByAttribute;

                        //是否使用查询特性参数
                        //不使用查询特性
                        if (!groupByAttr.IsUseQueryFieldAttribute())
                        {
                            //表别名
                            string tableAlias = groupByAttr.GetTableAlias();

                            if (!string.IsNullOrEmpty(tableAlias))
                            {
                                sql += "`" + tableAlias + "`" + ".";
                            }

                            //表字段名
                            sql += "`" + groupByAttr.GetFieldName() + "`";
                        }
                        //使用查询特性
                        else
                        {
                            //表别名
                            if (prop.IsDefined(typeof(DbTableAliasAttribute), true))
                            {

                                var attr = prop.GetCustomAttributes(typeof(DbTableAliasAttribute), true)[0] as DbTableAliasAttribute;

                                sql += "`" + attr.GetTableAlias() + "`" + ".";
                            }

                            //表字段名
                            if (prop.IsDefined(typeof(DbQueryFieldAttribute), true))
                            {
                                var attr = prop.GetCustomAttributes(typeof(DbQueryFieldAttribute), true)[0] as DbQueryFieldAttribute;

                                sql += "`" + attr.GetFieldName() + "`";
                            }
                            // 未配置DbQueryField，直接取字段名称
                            else
                            {
                                sql += "`" + prop.Name + "`";
                            }
                        }

                        //拼接sql
                        if (!string.IsNullOrEmpty(sql))
                        {
                            groupby.Append(sql + ", ");
                        }
                    }
                }
            }

            //校验是否存在GROUP BY内容
            if (groupby.Length > 0)
            {
                return groupby.Remove(groupby.Length - 2, 2).ToString();
            }

            return string.Empty;
        }
        #endregion

        #region Where
        /// <summary>
        /// 自动拼接查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSearch"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ISugarQueryable<T> Where<T, TSearch>(this ISugarQueryable<T> queryable, TSearch search)
        {
            return queryable.Where(search.GetWhereParameters());
        }
        #endregion

        #region Select
        /// <summary>
        /// 自动拼接查询结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ISugarQueryable<TResult> Select<T, TResult>(this ISugarQueryable<T> queryable, TResult result)
        {
            return queryable.Select<TResult>(result.GetSelectSQL());
        }
        #endregion

        #region OrderBy
        /// <summary>
        /// 自动拼接排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSearch"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ISugarQueryable<T> OrderBy<T, TSearch>(this ISugarQueryable<T> queryable, TSearch search)
        {
            object[] sort = search.GetType().GetCustomAttributes(typeof(DbDefaultOrderByAttribute), true);

            if (sort?.Length > 0)
            {
                foreach (var item in sort)
                {
                    var attr = item as DbDefaultOrderByAttribute;

                    queryable = queryable.OrderBy($"{attr.GetOrderField()} {attr.GetSortWay()}");
                }
            }

            return queryable;
        }
        #endregion

        #region GroupBy
        /// <summary>
        /// 自动拼接查询分组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ISugarQueryable<T> GroupBy<T, TResult>(this ISugarQueryable<T> queryable, TResult result)
        {
            //获取分组部分
            string groupBySql = result.GetGroupBySql();
            if (!string.IsNullOrEmpty(groupBySql))
            {
                queryable = queryable.GroupBy(groupBySql);
            }

            return queryable;
        }
        /// <summary>
        /// 自动拼接分组条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ISugarQueryable<T> Having<T, TResult>(this ISugarQueryable<T> queryable, TResult result)
        {
            object[] condition = result.GetType().GetCustomAttributes(typeof(DbHavingAttribute), true);

            if (condition?.Length > 0)
            {
                foreach (var item in condition)
                {
                    var attr = item as DbHavingAttribute;

                    queryable = queryable.Having($"{attr.GetCondition()}");
                }
            }

            return queryable;
        }
        #endregion
    }
}
