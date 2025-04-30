using System.Globalization;

namespace SqlSugar.Attributes.Test
{
    /// <summary>
    /// Db Context
    /// </summary>
    public static class DbContext
    {
        /// <summary>
        /// Is Initial
        /// </summary>
        private static bool _isInit;

        /// <summary>
        /// SqlSugar client
        /// </summary>
        private static SqlSugarScope _scope;

        /// <summary>
        /// connection strings
        /// </summary>
        private const string CONNECTION_STRINGS = "Data Source=sqlsugar.attribute.db;Mode=ReadWriteCreate";

        /// <summary>
        /// Initial
        /// </summary>
        private static void Init()
        {
            _scope = new SqlSugarScope(new ConnectionConfig()
                {
                    ConnectionString = CONNECTION_STRINGS,
                    DbType = DbType.Sqlite,
                    LanguageType = LanguageType.English,
                    IsAutoCloseConnection = true,
                },
                it =>
                {
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
                            Console.Write(
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("【Parameters】：");
                            Console.ResetColor();
                            Console.WriteLine(string.Join(",",
                                parameters.Select(parameter =>
                                    "【" + parameter.ParameterName + "=" + parameter.Value + "】")));
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
            if (!_isInit)
            {
                Init();
            }

            return _scope;
        }
    }
}