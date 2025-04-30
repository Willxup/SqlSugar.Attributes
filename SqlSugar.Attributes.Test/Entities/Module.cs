namespace SqlSugar.Attributes.Test.Entities
{
    /// <summary>
    /// Module
    /// </summary>
    [SugarTable("Module")]
    public class Module
    {
        /// <summary>
        /// Module Id
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int ModuleId { get; set; }
        /// <summary>
        /// Module name
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string ModuleName { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [SugarColumn(IsNullable = false, InsertServerTime = true)]
        public DateTime CreateTime { get; set; }
    }
}
