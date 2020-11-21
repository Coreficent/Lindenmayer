namespace Coreficent.Grammar
{
    using System.Text;
    using UnityEngine;
    public class LindenmayerSystem
    {
        public string Axiom;
        private string ruleIn;
        private string ruleOut;

        public LindenmayerSystem()
        {
            Debug.Log("Lindenmayer System Started");
        }
        public void AddRule(string input, string output)
        {
            Debug.Log("rule in::" + input);
            Debug.Log("rule out::" + output);
            ruleIn = input;
            ruleOut = output;
        }
        public string Expand(int iteration)
        {
            string itermediate = Axiom;
            for (var i = 0; i < iteration; ++i)
            {
                itermediate = Expand(itermediate);
            }
            return itermediate;
        }

        private string Expand(string sentence)
        {
            string result = "";

            foreach (char c in sentence)
            {
                string i = c.ToString();
                result += i == ruleIn ? ruleOut : i.ToString();
            }

            return result;
        }
    }
}