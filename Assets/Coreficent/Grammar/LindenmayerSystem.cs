namespace Coreficent.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    public class LindenmayerSystem
    {
        public string Axiom;
        private List<Tuple<string, string>> rules = new List<Tuple<string, string>>();


        public LindenmayerSystem()
        {

        }
        public void Reset()
        {
            rules.Clear();
        }
        public void AddRule(string rule)
        {
            if (rule != "")
            {
                rules.Add(new Tuple<string, string>(rule.Substring(0, rule.IndexOf("=") - rule.IndexOf("Rule:") - 1), rule.Substring(rule.IndexOf("=") + 1)));
            }
        }
        public string Expand(int iteration)
        {
            string itermediate = Axiom;
            for (var i = 0; i < iteration; ++i)
            {
                itermediate = Expand(itermediate);
                Debug.Log("itermediate::" + itermediate);
            }
            return itermediate;
        }

        private string Expand(string sentence)
        {
            string result = sentence;
            foreach (Tuple<string, string> rule in rules)
            {
                result = Expand(result, rule);
            }
            return result;
        }
        private string Expand(string sentence, Tuple<string, string> rule)
        {
            string result = "";
            foreach (char c in sentence)
            {
                string i = c.ToString();
                result += i == rule.Item1 ? rule.Item2 : i.ToString();
            }
            return result;
        }
    }
}