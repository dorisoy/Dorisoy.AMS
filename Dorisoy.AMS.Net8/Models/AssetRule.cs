using SqlSugar;

namespace Dorisoy.AMS.models
{
    // 规则实体类
    [SugarTable("asset_rules")]
    public class AssetRule
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 100)]
        public string Keywords { get; set; } = string.Empty;

        [SugarColumn(Length = 20)]
        public string Prefix { get; set; } = string.Empty;

        public int NumberLength { get; set; }

        [SugarColumn(IsIgnore = true)]
        public bool IsDefault { get; set; } // 不存储到数据库
    }
}