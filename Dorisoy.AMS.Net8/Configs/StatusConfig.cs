using System.Collections.ObjectModel;

namespace Dorisoy.AMS
{
    public static class StatusConfig
    {
        // 核心状态集合（包含全部状态）
        public static readonly ReadOnlyCollection<StatusItem> AllStatusOptions = new List<StatusItem>
        {
            new StatusItem(-1, "全部状态"), // 默认项
            new StatusItem(0, "正常"),
            new StatusItem(1, "删除"),
            new StatusItem(2, "报废"),
            new StatusItem(3, "转移"),
            new StatusItem(4, "借用")
        }.AsReadOnly();

        public static readonly Dictionary<string, int> StatusMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"正常", 0},
            {"删除", 1},
            {"报废", 2},
            {"转移", 3},
            {"借用", 4}
        };

        // 业务状态集合（排除全部状态）
        public static readonly ReadOnlyCollection<StatusItem> BusinessStatusOptions =
            new List<StatusItem>(AllStatusOptions.Skip(1)).AsReadOnly();

        // 状态项验证方法
        public static bool IsValidCode(int code) =>
            AllStatusOptions.Any(s => s.Code == code);

        // 获取状态名称
        public static string GetStatusName(int code) =>
            AllStatusOptions.FirstOrDefault(s => s.Code == code)?.Name ?? "未知状态";

        public class StatusItem
        {
            public int Code { get; }
            public string Name { get; }

            public StatusItem(int code, string name)
            {
                Code = code;
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }

            public override string ToString() => Name;
        }
    }
}