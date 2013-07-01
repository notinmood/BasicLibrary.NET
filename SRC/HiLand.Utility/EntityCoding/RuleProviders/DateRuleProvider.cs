using System;
using System.Text.RegularExpressions;

namespace HiLand.Utility.EntityCoding.RuleProviders
{
    internal class DateRuleProvider : ICodeRuleProvider
    {
        private string _format;

        public DateRuleProvider(string format)
        {
            _format = format;
        }

        public string Generate(object entity)
        {
            return DateTime.Now.ToString(_format);
        }

        internal static ICodeRuleProvider DateRuleProviderFactory(string literal)
        {
            var match = new Regex("^<日期(:(?<格式>.*?))?>$").Match(literal);

            var format = match.Groups["格式"].Value;

            return new DateRuleProvider(string.IsNullOrEmpty(format) ? "yyyyMMdd" : format);
        }
    }
}