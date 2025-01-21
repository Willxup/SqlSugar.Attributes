using SqlSugar.Attributes.Extension.Extensions;
using SqlSugar.Attributes.Test.Entities;
using SqlSugar.Attributes.Test.Models;

namespace SqlSugar.Attributes.Test.Test
{
    public class UpdateTest
    {
        public static async Task Test()
        {
            SqlSugarScope db = DbContext.GetDb();

            UpdateUserDto dto = new()
            {
                Id = 2,
                Name = "Test5",
                Email = null,
                CreateTime = null
            };

            bool isSucess = db.Updateable<UpdateUserDto, User>(dto).ExecuteCommandHasChange();

            Console.WriteLine($"Update Test1 Result = {isSucess}");
        }
    }
}
