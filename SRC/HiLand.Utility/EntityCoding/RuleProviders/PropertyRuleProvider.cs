using System.Reflection;
using System.Text.RegularExpressions;

namespace HiLand.Utility.EntityCoding.RuleProviders
{
    internal class PropertyRuleProvider : ICodeRuleProvider
    {
        private string _property;

        public PropertyRuleProvider(string property)
        {
            _property = property;
        }

        public string Generate(object entity)
        {
            string result= string.Empty;
            PropertyInfo propertyInfo= entity.GetType().GetProperty(_property);
            if(propertyInfo!=null)
            {
                result = propertyInfo.GetValue(entity,null).ToString();
            }
            return result;
        }

        internal static ICodeRuleProvider PropertyRuleProviderFactory(string literal)
        {
            var match = new Regex("^<属性(:(?<名称>.*?))?>$").Match(literal);

            var property = match.Groups["名称"].Value;

            return new PropertyRuleProvider(property);
        }
    }
}