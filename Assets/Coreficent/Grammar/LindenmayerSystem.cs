namespace Coreficent.Grammar
{
    using System.Text;
    using UnityEngine;
    public class LindenmayerSystem
    {
        private string variables;
        private string constants;
        private string axiom;
        private string rules;
        public LindenmayerSystem()
        {
            Debug.Log("Lindenmayer System Started");
            variables = "";
            constants = "";
            axiom = "F";
            rules = "F->F[+F]F[-F]F";
            Expand(1);
        }
        public string Expand(int iteration)
        {
            string itermediate = axiom;
            for (var i = 0; i < iteration; ++i)
            {
                itermediate = Expand(itermediate);
            }
            return itermediate;
        }

        public string Expand(string sentence)
        {
            string result = "";

            foreach (char c in sentence)
            {
                string output = "";
                if (c == 'F')
                {
                    output += "F[+F]F[-F]F";
                }
                else
                {
                    output += c;
                }
                result += output;
            }

            return result;
        }
    }
}