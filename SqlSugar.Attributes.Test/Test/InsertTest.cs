using SqlSugar.Attributes.Extension.Extensions;
using SqlSugar.Attributes.Test.Entities;
using SqlSugar.Attributes.Test.Models;

namespace SqlSugar.Attributes.Test.Test
{
    /// <summary>
    /// Insert Test
    /// </summary>
    public static class InsertTest
    {
        public static async Task Test()
        {
            var db = DbContext.GetDb();

            InsertUserDto user = new()
            {
                Name = "Test5",
                Email = "test5@willxup.top",
                CreateTime = db.GetDate()
            };

            long id = await db.Insertable<InsertUserDto, User>(user).ExecuteReturnBigIdentityAsync();
            Console.WriteLine(id);
        }
    }
}
