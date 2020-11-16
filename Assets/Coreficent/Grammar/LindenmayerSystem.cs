namespace Coreficent.Grammar
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class LindenmayerSystem : MonoBehaviour
    {
        private string variables;
        private string constants;
        private string axiom;
        private string rules;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Lindenmayer System Started");
            variables = "";
            constants = "";
            axiom = "F";
            rules = "F->F[+F]F[-F]F";
            Expand(1);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Expand(int iteration)
        {
            StringBuilder result = new StringBuilder();
            for (var i = 0; i < iteration; ++i)
            {
                foreach (char c in axiom)
                {
                    string output = "";
                    if(c == 'F')
                    {
                        output += "F[+F]F[-F]F";
                    }
                    else
                    {
                        output += c;
                    }
                    result.Append(output);
                }
            }
            Debug.Log(result.ToString());
        }
    }
}