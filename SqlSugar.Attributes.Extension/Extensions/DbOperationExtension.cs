﻿using SqlSugar.Attributes.Extension.Extensions.Attributes.Operation;
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
            var props = dto.GetType().GetProperties();

            if (props?.Length > 0)
            {
                Dictionary<string, object> fields = new Dictionary<string, object>();

                foreach (var prop in props)
                {
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                    {
                        continue;
                    }

                    //校验是否标记特性
                    if (prop.GetCustomAttributes(typeof(DbOperationFieldAttribute), true).Length == 0)
                    {
                        continue;
                    }

                    //映射参数
                    if (prop.IsDefined(typeof(DbOperationFieldAttribute), true))
                    {

                        var value = prop.GetValue(dto);
                        var attr = prop.GetCustomAttributes(typeof(DbOperationFieldAttribute), true)[0] as DbOperationFieldAttribute;

                        value ??= null;

                        if (value is string stringValue && string.IsNullOrEmpty(stringValue))
                            value = null;

                        fields.Add(attr.GetFieldName(), value); ;
                    }
                }
                if (fields?.Count > 0)
                {
                    return db.Insertable<TEntity>(fields);
                }
            }

            throw new Exception("DTO对象不存在需要插入的值!");
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
            var props = dto.GetType().GetProperties();

            if (props?.Length > 0)
            {

                List<string> conditions = new List<string>();
                Dictionary<string, object> fields = new Dictionary<string, object>();

                foreach (var prop in props)
                {
                    //校验是否为忽略字段
                    if (prop.GetCustomAttributes(typeof(DbIgnoreFieldAttribute), true).Length > 0)
                    {
                        continue;
                    }

                    //校验是否标记特性
                    if (prop.GetCustomAttributes(typeof(DbOperationFieldAttribute), true).Length == 0)
                    {
                        continue;
                    }

                    if (prop.IsDefined(typeof(DbOperationFieldAttribute), true))
                    {

                        var value = prop.GetValue(dto);
                        var attr = prop.GetCustomAttributes(typeof(DbOperationFieldAttribute), true)[0] as DbOperationFieldAttribute;

                        //是否为更新条件
                        if (attr.IsCondition())
                        {
                            conditions.Add(attr.GetFieldName());
                        }

                        //映射参数
                        if (attr.IsAllowEmpty())
                        {
                            value ??= null;

                            if (value is string stringValue && string.IsNullOrEmpty(stringValue)) value = null;

                        }
                        else
                        {
                            if (value is null) continue;
                            if (value is string stringValue && string.IsNullOrEmpty(stringValue)) continue;
                        }

                        fields.Add(attr.GetFieldName(), value); ;
                    }
                }
                if (conditions?.Count > 0 && fields?.Count > 0)
                {
                    var insertable = db.Updateable<TEntity>(fields).WhereColumns(conditions.ToArray());
                    return insertable;
                }
            }

            throw new Exception("DTO对象不存在需要更新的值!");
        }
        #endregion
    }
}