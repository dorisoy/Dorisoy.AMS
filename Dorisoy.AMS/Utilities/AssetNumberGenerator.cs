using Dorisoy.AMS.models;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace Dorisoy.AMS.Utilities
{
    public static class AssetNumberGenerator
    {
        public static string Generate(string assetName)
        {
            using (var db = SqliteHelper.GetDb())
            {
                // 检查是否启用默认规则
                bool useDefault = db.Queryable<GlobalSetting>()
                    .First(s => s.Key == "UseDefaultRule")?.Value != "0";

                if (useDefault)
                {
                    return GetDefaultNumber(db);
                }

                // 使用自定义规则
                return GetCustomNumber(db, assetName) ?? GetDefaultNumber(db);
            }
        }

        private static string GetDefaultNumber(SqlSugarClient db)
        {
            // 获取标记为默认的规则
            var defaultRuleId = db.Queryable<GlobalSetting>()
                .Where(s => s.Key == "DefaultRuleId")
                .Select(s => s.Value)
                .First();

            AssetRule rule;
            if (int.TryParse(defaultRuleId, out int id))
            {
                rule = db.Queryable<AssetRule>().First(r => r.Id == id);
                if (rule == null)
                {
                    return GenerateNumber("DNS", 5, db);
                }
            }
            else
            {
                // 默认硬编码规则
                return GenerateNumber("DNS", 5, db);
            }

            return GenerateNumber(rule.Prefix, rule.NumberLength, db);
        }

        private static string GetCustomNumber(SqlSugarClient db, string assetName)
        {
            // 清理输入名称
            string cleanName = assetName.Trim().Replace(" ", "").ToUpper();

            // 获取所有自定义规则（排除默认规则）
            List<AssetRule> rules = db.Queryable<AssetRule>().ToList();

            // 匹配规则逻辑
            var matchedRule = rules
                .Select(r => new
                {
                    Rule = r,
                    Keywords = r.Keywords.Split(',')
                        .Select(k => k.Trim().ToUpper())
                        .Where(k => !string.IsNullOrEmpty(k))
                        .ToList()
                })
                .Where(r => r.Keywords.Any(k => cleanName.Contains(k)))
                .OrderByDescending(r => r.Keywords.Max(k => k.Length))
                .ThenByDescending(r => r.Rule.NumberLength)
                .FirstOrDefault()?.Rule;

            if (matchedRule != null)
            {
                return GenerateNumber(matchedRule.Prefix, matchedRule.NumberLength, db);
            }

            return null; // 未匹配到规则
        }

        private static string GenerateNumber(string prefix, int length, SqlSugarClient db)
        {
            int currentNum = 1;
            bool isNumberGenerated;
            string newNumber;

            do
            {
                isNumberGenerated = false;
                newNumber = $"{prefix}{currentNum.ToString($"D{length}")}";

                // 检查数据库中是否存在该编号
                var exists = db.Queryable<Asset>().Any(a => a.AssetID == newNumber);

                if (!exists)
                {
                    isNumberGenerated = true;
                }
                else
                {
                    // 如果编号已存在，则尝试下一个编号
                    currentNum++;
                }
            } while (!isNumberGenerated);

            return newNumber;
        }
    }
}
