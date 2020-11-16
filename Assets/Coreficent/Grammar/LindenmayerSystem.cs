namespace Coreficent.Grammar
{
    using System.Collections;
    using System.Collections.Generic;
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
            StringBuilder result = new StringBuilder();
            string intermediate = axiom;
            for (var i = 0; i < iteration; ++i)
            {
                foreach (char c in intermediate)
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
                    result.Append(output);
                }
                intermediate = result.ToString();
            }

            Debug.Log(result.ToString());
            return result.ToString();
        }
    }
}