namespace SqlSugar.Attributes.Test.Entities
{
    [SugarTable("Module")]
    public class Module
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public long ModuleId { get; set; }
        /// <summary>
        /// 模块名
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string ModuleName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
