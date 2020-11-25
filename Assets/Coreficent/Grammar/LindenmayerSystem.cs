namespace Coreficent.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LindenmayerSystem
    {
        public string Axiom;
        private readonly List<Tuple<string, string>> rules = new List<Tuple<string, string>>();
        private readonly List<Tuple<Tuple<string, string>, Tuple<string, string>>> chanceRules = new List<Tuple<Tuple<string, string>, Tuple<string, string>>>();

        /*
         * constructor.
         */
        public LindenmayerSystem()
        {

        }
        /*
         * reset to the initial state when selecting a preset.
         */
        public void Reset()
        {
            rules.Clear();
            chanceRules.Clear();
        }
        /*
         * add a rule to the l system.
         */
        public void AddRule(string rule)
        {
            if (rule != "")
            {
                if (rule.IndexOf("=") != -1)
                {
                    rules.Add(new Tuple<string, string>(rule.Substring(0, rule.IndexOf("=")), rule.Substring(rule.IndexOf("=") + 1)));
                }
            }
        }
        /*
         * add a pair of chance rules for stochastic graphics.
         */
        public void AddChanceRule(string ruleA, string ruleB)
        {
            if (ruleA != "" && ruleB != "")
            {
                if (ruleA.IndexOf("=") != -1 && ruleB.IndexOf("=") != -1)
                {
                    Tuple<string, string> chanceRuleA = new Tuple<string, string>(ruleA.Substring(0, ruleA.IndexOf("=")), ruleA.Substring(ruleA.IndexOf("=") + 1));
                    Tuple<string, string> chanceRuleB = new Tuple<string, string>(ruleB.Substring(0, ruleB.IndexOf("=")), ruleB.Substring(ruleB.IndexOf("=") + 1));
                    chanceRules.Add(new Tuple<Tuple<string, string>, Tuple<string, string>>(chanceRuleA, chanceRuleB));
                }
            }
        }
        /*
         * expand the sentence given the number of iterations.
         */
        public string Expand(int iteration)
        {
            string itermediate = Axiom;
            for (var i = 0; i < iteration; ++i)
            {
                itermediate = Expand(itermediate);
            }
            return itermediate;
        }
        /*
         * determine how the rules should be applied.
         */
        private string Expand(string sentence)
        {
            string result = sentence;
            foreach (Tuple<string, string> rule in rules)
            {
                result = Expand(result, rule);
            }
            foreach (var chanceRule in chanceRules)
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    result = Expand(result, chanceRule.Item1);
                }
                else
                {
                    result = Expand(result, chanceRule.Item2);
                }
            }
            return result;
        }
        /*
         * apply the rules to the current sentence.
         */
        private string Expand(string sentence, Tuple<string, string> rule)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in sentence)
            {
                string s = c.ToString();
                result.Append(s == rule.Item1 ? rule.Item2 : s);
            }
            return result.ToString();
        }
    }
}