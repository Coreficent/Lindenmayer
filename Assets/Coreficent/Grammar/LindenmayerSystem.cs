namespace Coreficent.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    public class LindenmayerSystem
    {
        public string Axiom;
        private List<Tuple<string, string, float>> rules = new List<Tuple<string, string, float>>();

        public LindenmayerSystem()
        {

        }
        public void Reset()
        {
            rules.Clear();
        }
        public void AddRule(string rule, float chance)
        {
            if (rule != "")
            {
                rules.Add(new Tuple<string, string, float>(rule.Substring(0, rule.IndexOf("=") - rule.IndexOf("Rule:") - 1), rule.Substring(rule.IndexOf("=") + 1), chance));
            }
        }
        public string Expand(int iteration)
        {
            string itermediate = Axiom;
            for (var i = 0; i < iteration; ++i)
            {
                itermediate = Expand(itermediate);
                //Debug.Log("itermediate::" + itermediate);
            }
            return itermediate;
        }

        private string Expand(string sentence)
        {
            string result = sentence;
            foreach (Tuple<string, string, float> rule in rules)
            {
                if (UnityEngine.Random.Range(0.0f, 0.9999f) < rule.Item3)
                {
                    result = Expand(result, rule);
                }
            }
            return result;
        }
        private string Expand(string sentence, Tuple<string, string, float> rule)
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