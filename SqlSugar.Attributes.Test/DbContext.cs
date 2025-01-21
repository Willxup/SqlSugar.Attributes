using System.Globalization;

namespace SqlSugar.Attributes.Test
{
    public class DbContext
    {
        private static bool _isInit = false;
        private static SqlSugarScope _scope;
        private readonly static string _connection = "server=127.0.0.1;Port=3306;Database=sa_test;Uid=root;Pwd=liugui.com;AllowLoadLocalInfile=true";

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init()
        {
            _scope = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = _connection,
                DbType = DbType.MySql,
                LanguageType = LanguageType.English,
                IsAutoCloseConnection = true,
            },
            it => {
                it.Aop.OnLogExecuting = (sql, parameters) =>
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("【SQL】：");
                    Console.ResetColor();
                    Console.WriteLine(sql);
                    if (parameters?.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("【Paramters】：");
                        Console.ResetColor();
                        Console.WriteLine(string.Join(",", parameters.Select(it => "【" + it.ParameterName + "=" + it.Value + "】")));
                    }
                };
            });

            _isInit = true;
        }

        /// <summary>
        /// 获取Db
        /// </summary>
        /// <returns></returns>
        public static SqlSugarScope GetDb()
        {
            if(!_isInit)
            {
                Init();
            }

            return _scope;
        }
    }
}
