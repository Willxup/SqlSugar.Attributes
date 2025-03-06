﻿using SqlSugar.Attributes.Extension.Common;
using SqlSugar.Attributes.Extension.Extensions.Attributes.Operation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlSugar.Attributes.Extension.Extensions
{
    public static class DbOperationExtension
    {
        #region 私有方法
        /// <summary>
        /// 是否为可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsNullableType(Type type)
        {
            return (type.IsValueType && Nullable.GetUnderlyingType(type) != null) || type == typeof(string);
        }
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldName">表字段名</param>
        /// <param name="value">更新的值</param>
        /// <returns></returns>
        /// <exception cref="GlobalException"></exception>
        private static MemberAssignment BindParameter<TEntity>(string fieldName, object value)
        {
            PropertyInfo entityProperty = typeof(TEntity).GetProperty(fieldName);

            // 值为空，且值类型不是可空类型
            if (value is null && !IsNullableType(entityProperty.PropertyType))
            {
                throw new GlobalException($"表字段[{fieldName}]更新的值不能为空!");
            }
            // 值为空，值类型为可空类型
            else if (value is null)
            {
                return Expression.Bind(entityProperty, Expression.Constant(null, entityProperty.PropertyType));
            }
            // 值不为空，值类型为可空类型
            // 值不为空，值类型为不可空类型
            else
            {
                //判断是否可以赋值
                if (!entityProperty.PropertyType.IsAssignableFrom(value.GetType()))
                    throw new GlobalException($"表字段[{fieldName}]类型与传参类型不一致!");
            }

            return Expression.Bind(entityProperty, Expression.Constant(value));
        }
        /// <summary>
        /// 创建更新表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="assignments">赋值</param>
        /// <returns></returns>
        private static Expression<Func<TEntity, TEntity>> CreateUpdateColumnsExpression<TEntity>(IEnumerable<MemberAssignment> assignments)
        {
            // 构建一个Expression<Func<TEntity,TEntity>>的表达式树

            // 实体类型 相当于 lambda表达式 传入的参数
            ParameterExpression entity = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);

            // 参数初始化 相当于 lambda表达式 传出的返回值
            MemberInitExpression memberInit = Expression.MemberInit(Expression.New(typeof(TEntity)), assignments);

            return Expression.Lambda<Func<TEntity, TEntity>>(memberInit, entity);
        }

        /// <summary>
        /// 创建条件表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldName">表字段名</param>
        /// <param name="value">更新的值</param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> CreateUpdateWhereExpression<TEntity>(string fieldName, object value)
        {
            if (value == null)
                throw new GlobalException($"表字段[{fieldName}]的值不能为空!");

            // 获取属性
            PropertyInfo prop = typeof(TEntity).GetProperty(fieldName);

            if (!prop.PropertyType.IsAssignableFrom(value.GetType()))
                throw new GlobalException($"表字段[{fieldName}]类型与传参类型不一致!");

            // 创建参数表达式
            var parameter = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);

            // 创建等式
            var equalExpression = Expression.Equal(Expression.Property(parameter, prop), Expression.Constant(value));

            return Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);
        }
        #endregion

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
                    throw new GlobalException("没有需要更新的字段!");

                return db.Insertable<TEntity>(fields);
            }
            else
                throw new GlobalException("DTO对象不存在属性!");
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
        /// <exception cref="GlobalException"></exception>
        public static IUpdateable<TEntity> Updateable<TDto, TEntity>(this SqlSugarScope db, TDto dto)
            where TDto : class, new()
            where TEntity : class, new()
        {
            //获取DTO模型属性
            var props = dto.GetType().GetProperties();

            if (props?.Length > 0)
            {
                bool haveCondition = false;
                List<MemberAssignment> assignments = new List<MemberAssignment>();
                Expressionable<TEntity> where = Expressionable.Create<TEntity>();

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

                        //参数是否允许更新为空,不允许更新为空，就直接忽略该字段不进行更新
                        if (!attr.IsAllowEmpty())
                        {
                            if (value is null)
                                continue;
                        }

                        //是否为更新条件
                        if (attr.IsCondition())
                        {
                            // 创建条件表达式树，并进行拼接
                            where = where.And(CreateUpdateWhereExpression<TEntity>(attr.GetFieldName(), value));
                            haveCondition = true;
                        }
                        else
                        {
                            assignments.Add(BindParameter<TEntity>(attr.GetFieldName(), value));
                        }
                    }
                    //未配置DbOperationField，直接取字段名称
                    else
                    {
                        if (value is null)
                            throw new GlobalException($"未标记特性的字段[{prop.Name}]的值不能为空!");

                        assignments.Add(BindParameter<TEntity>(prop.Name, value));
                    }
                    #endregion
                }

                if (assignments.Count <= 0)
                    throw new GlobalException("没有需要更新的字段!");

                if (!haveCondition)
                    throw new GlobalException("请添加更新条件!");

                // 创建更新字段表达式树
                var columns = CreateUpdateColumnsExpression<TEntity>(assignments);

                var updateable = db.Updateable<TEntity>().SetColumns(columns).Where(where.ToExpression());

                return updateable;
            }
            else
                throw new GlobalException("DTO对象不存在属性!");
        }
        /// <summary>
        /// 自动Dto转实体更新(旧版本)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="db"></param>
        /// <param name="dto">dto模型</param>
        /// <returns></returns>
        /// <exception cref="GlobalException"></exception>
        public static IUpdateable<TEntity> OldUpdateable<TDto, TEntity>(this SqlSugarScope db, TDto dto)
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

                        //参数是否允许更新为空,不允许更新为空，就直接忽略该字段不进行更新
                        if (!attr.IsAllowEmpty())
                        {
                            if (value is null)
                                continue;
                        }

                        //是否为更新条件
                        if (attr.IsCondition())
                        {
                            conditions.Add(attr.GetFieldName());
                        }

                        fields.Add(attr.GetFieldName(), value);
                    }
                    //未配置DbOperationField，直接取字段名称
                    else
                    {
                        fields.Add(prop.Name, value);
                    }
                    #endregion
                }

                if (conditions.Count <= 0)
                    throw new GlobalException("请标记更新条件!");

                if (fields.Count <= 0)
                    throw new GlobalException("没有需要更新的字段!");

                var insertable = db.Updateable<TEntity>(fields).WhereColumns(conditions.ToArray());
                return insertable;
            }
            else
                throw new GlobalException("DTO对象不存在属性!");
        }
        #endregion
    }
}
