using SqlSugar.Attributes.Test.Test;

namespace SqlSugar.Attributes.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            InitData.Init();

            await QueryTest.Test();
            await InsertTest.Test();
            await UpdateTest.Test();

            Console.ReadKey();
        }
    }
}
