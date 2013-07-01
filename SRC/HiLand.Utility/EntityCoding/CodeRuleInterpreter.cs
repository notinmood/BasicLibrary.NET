using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HiLand.Utility.EntityCoding.RuleProviders;
using HiLand.Utility.Event;

namespace HiLand.Utility.EntityCoding
{
    public static class CodeRuleInterpreter
    {
        private static Dictionary<Regex, Funcs<string, ICodeRuleProvider>> _providerFactorys = new Dictionary<Regex, Funcs<string, ICodeRuleProvider>>();

        static CodeRuleInterpreter()
        {
            SetProviderFactory(new Regex("^[^<].*?[^>]?$"), LiteralRuleProvider.LiteralRuleProviderFactory);
            SetProviderFactory(new Regex("^<日期(:(?<格式>.*?))?>$"), DateRuleProvider.DateRuleProviderFactory);
            SetProviderFactory(new Regex("^<属性(:(?<名称>.*?))?>$"), PropertyRuleProvider.PropertyRuleProviderFactory);
        }

        public static void SetProviderFactory(Regex regex, Funcs<string, ICodeRuleProvider> providerFactory)
        {
            _providerFactorys[regex] = providerFactory;
        }


        public static ICodeRuleGenerator Interpret(string codeRule)
        {
            var providers = GetProviders(codeRule);

            return new CodeRuleGenerator(providers);
        }

        private static IEnumerable<ICodeRuleProvider> GetProviders(string codeRule)
        {
            var literals = codeRule.Replace("<", "$<").Replace(">", ">$").Split('$');

            List<ICodeRuleProvider> providers = new List<ICodeRuleProvider>();
            foreach (string item in literals)
            {
                if (string.IsNullOrEmpty(item) == false)
                { 
                    ICodeRuleProvider provider= GetProvider(item);
                    if (provider != null)
                    {
                        providers.Add(provider);
                    }
                }
            }

            return providers;

            //return literals
            //    .Where(x => !string.IsNullOrEmpty(x))
            //    .Select(GetProvider)
            //    .ToList();
        }

        private static ICodeRuleProvider GetProvider(string literal)
        {
            foreach (KeyValuePair<Regex, Funcs<string, ICodeRuleProvider>> kvp in _providerFactorys)
            {
                if (kvp.Key.IsMatch(literal) == true)
                {
                    Funcs<string, ICodeRuleProvider> providerFactory = kvp.Value;
                    return providerFactory(literal);
                }
            }

            return null;


            //var providerFactory = _providerFactorys
            //    .FirstOrDefault(x => x.Key.IsMatch(literal))
            //    .Value;

            //if (providerFactory == null)
            //{
            //    throw new FormatException("格式化错误");
            //}

            //return providerFactory(literal);
        }
    }
}
