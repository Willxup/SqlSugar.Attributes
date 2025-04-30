using SqlSugar.Attributes.Extension.Extensions;

namespace SqlSugar.Attributes.Test.Models
{
    /// <summary>
    /// Query Base
    /// </summary>
    public class DbQueryModel
    {
        /// <summary>
        /// Index
        /// </summary>
        [DbIgnoreField]
        public int? Index { get; set; } = null;
        /// <summary>
        /// Size
        /// </summary>
        [DbIgnoreField]
        public int? Size { get; set; } = null;
        /// <summary>
        /// Offset
        /// </summary>
        [DbIgnoreField]
        public virtual int? Offset => (Index - 1) * Size;
    }
}
