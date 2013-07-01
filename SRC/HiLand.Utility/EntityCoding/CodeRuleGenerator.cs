using System.Collections.Generic;
using System.Text;


namespace HiLand.Utility.EntityCoding
{
    public sealed class CodeRuleGenerator : ICodeRuleGenerator
    {
        private readonly IEnumerable<ICodeRuleProvider> _providers = new List<ICodeRuleProvider>();

        internal CodeRuleGenerator(IEnumerable<ICodeRuleProvider> providers)
        {
            _providers = providers;
        }

        public string Generate(object entity)
        {
            var sb = new StringBuilder();

            foreach (var provider in _providers)
            {
                sb.Append(provider.Generate(entity));
            }

            return sb.ToString();
        }
    }
}