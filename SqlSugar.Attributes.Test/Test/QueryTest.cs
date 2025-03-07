﻿using Newtonsoft.Json;
using SqlSugar.Attributes.Extension.Extensions;
using SqlSugar.Attributes.Test.Entities;
using SqlSugar.Attributes.Test.Models;

namespace SqlSugar.Attributes.Test.Test
{
    public class QueryTest
    {
        public static async Task Test()
        {
            var db = DbContext.GetDb();

            // 前端传入
            UserPageSearch search = new()
            {
                Name = "test",
                Email = "" //表示不查询
            };

            var result = await db.Queryable<User>()
                .LeftJoin<UserPermission>((user, permission) => permission.UserId == user.UserId && permission.Type == 0)
                .LeftJoin<Role>((user, permission, role) => role.RoleId == permission.CommonId)
                .ToListAsync(search, new UserPageResult());

            Console.WriteLine(JsonConvert.SerializeObject(result));

        }
    }

    public static class DbQueryExtension
    {
        public static async Task<List<TResult>> ToListAsync<T, TSearch, TResult>(this ISugarQueryable<T> queryable, TSearch search, TResult result)
             where TSearch : DbQueryModel
        {
            return await queryable.Where(search).Select(result).GroupBy(result).Having(result).OrderBy(search).ToListAsync();
        }
    }

}
