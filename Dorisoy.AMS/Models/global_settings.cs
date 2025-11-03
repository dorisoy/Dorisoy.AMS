using SqlSugar;

namespace Dorisoy.AMS.models
{
    [SugarTable("global_settings")] // 确保表名正确
    public class GlobalSetting
    {
        [SugarColumn(IsPrimaryKey = true)] // 正确闭合
        public string Key { get; set; }

        [SugarColumn(Length = 200)] // 正确闭合
        public string Value { get; set; }
    }
}
