using SqlSugar.Attributes.Extension.Extensions;

namespace SqlSugar.Attributes.Test.Models
{
    public class DbQueryModel
    {
        [DbIgnoreField]
        public int? Index { get; set; } = null;
        [DbIgnoreField]
        public int? Size { get; set; } = null;
        [DbIgnoreField]
        public virtual int? Offset => (Index - 1) * Size;
    }
}
