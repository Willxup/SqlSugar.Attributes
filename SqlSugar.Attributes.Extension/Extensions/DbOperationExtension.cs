using SqlSugar.Attributes.Extension.Extensions.Attributes.Operation;
using System;
using System.Collections.Generic;

namespace SqlSugar.Attributes.Extension.Extensions
{
    public static class DbOperationExtension
    {
        #region 新增
        /// <summary>
        /// 自动Dto转实体新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="db"></param>
        /// <param name="dto">dto模型</param>
        /// <returns></returns>
        public static IInsertable<TEntity> Insertable<TDto, TEntity>(this SqlSugarScope db, TDto dto)
            where TDto : class, new()
            where TEntity : class, new()

        {
            //获取DTO模型属性
            var props = dto.GetType().GetProperties();

            if (props?.Length > 0)
            {
                Dictionary<string, object> fields = new Dictionary<string, object>();

                foreach (var prop in props)
                {
                    #region 参数校验
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 表字段获取及参数赋值
                    //获取参数值
                    var value = prop.GetValue(dto) ?? null;

                    //判断参数值是否为字符串，如果是字符串且为空
                    if (value is string stringValue && string.IsNullOrEmpty(stringValue))
                        value = null;

                    //映射参数
                    if (prop.IsDefined(typeof(DbOperationFieldAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbOperationFieldAttribute), true)[0] as DbOperationFieldAttribute;

                        fields.Add(attr.GetFieldName(), value); ;
                    }
                    //未配置DbOperationField，直接取字段名称
                    else
                    {
                        fields.Add(prop.Name, value); ;
                    }
                    #endregion

                }
                if (fields.Count <= 0)
                    throw new Exception("没有需要更新的字段!");

                return db.Insertable<TEntity>(fields);
            }
            else
                throw new Exception("DTO对象不存在属性!");
        }
        #endregion

        #region 更新
        /// <summary>
        /// 自动Dto转实体更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="db"></param>
        /// <param name="dto">dto模型</param>
        /// <returns></returns>
        /// <exception cref="UserOperationException"></exception>
        public static IUpdateable<TEntity> Updateable<TDto, TEntity>(this SqlSugarScope db, TDto dto)
            where TDto : class, new()
            where TEntity : class, new()

        {
            //获取DTO模型属性
            var props = dto.GetType().GetProperties();

            if (props?.Length > 0)
            {

                List<string> conditions = new List<string>();
                Dictionary<string, object> fields = new Dictionary<string, object>();

                foreach (var prop in props)
                {
                    #region 参数校验
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 表字段获取及参数赋值
                    //获取参数值
                    var value = prop.GetValue(dto) ?? null;

                    //判断参数值是否为字符串，如果是字符串且为空
                    if (value is string stringValue && string.IsNullOrEmpty(stringValue))
                        value = null;

                    if (prop.IsDefined(typeof(DbOperationFieldAttribute), true))
                    {
                        var attr = prop.GetCustomAttributes(typeof(DbOperationFieldAttribute), true)[0] as DbOperationFieldAttribute;

                        //是否为更新条件
                        if (attr.IsCondition())
                        {
                            conditions.Add(attr.GetFieldName());
                        }

                        //参数是否允许更新为空,不允许更新为空，就直接忽略该字段不进行更新
                        if (!attr.IsAllowEmpty())
                        {
                            if (value is null)
                                continue;
                        }

                        fields.Add(attr.GetFieldName(), value); ;
                    }
                    //未配置DbOperationField，直接取字段名称
                    else
                    {
                        fields.Add(prop.Name, value);
                    }
                    #endregion
                }

                if (conditions.Count <= 0)
                    throw new Exception("请标记更新条件!");

                if (fields.Count <= 0)
                    throw new Exception("没有需要更新的字段!");

                var insertable = db.Updateable<TEntity>(fields).WhereColumns(conditions.ToArray());
                return insertable;
            }
            else
                throw new Exception("DTO对象不存在属性!");
        }
        #endregion
    }
}
