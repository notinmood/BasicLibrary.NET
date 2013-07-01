
namespace HiLand.Utility.EntityCoding.RuleProviders
{
    internal class LiteralRuleProvider : ICodeRuleProvider
    {
        private string _litial;

        public LiteralRuleProvider(string litial)
        {
            _litial = litial;
        }

        public string Generate(object entity)
        {
            return _litial;
        }

        internal static ICodeRuleProvider LiteralRuleProviderFactory(string literal)
        {
            return new LiteralRuleProvider(literal);
        }
    }
}