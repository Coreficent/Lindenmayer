namespace Coreficent.Main
{
    using Coreficent.Grammar;
    using Coreficent.Graphics;
    using UnityEngine;
    using UnityEngine.UI;

    public class Main : MonoBehaviour
    {
        static public Main Root = null;

        static private int _iteration = 1;

        public GameObject MainCamera;
        public Material Material;
        public InputField Axion;
        public InputField RuleA;
        public InputField RuleB;
        public InputField Iteration;
        public Button Render;
        public Button Preset0;
        public Button Preset1;
        public Button Preset2;
        public Button Preset3;
        public Button Preset4;
        public Button Preset5;
        public Button Preset6;
        public Button Preset7;
        public Button Preset8;
        public Button Preset9;

        private readonly LindenmayerSystem _lindenmayerSystem = new LindenmayerSystem();
        private readonly Turtle _turtle = new Turtle();

        public void Reset()
        {
            //_lindenmayerSystem.Axiom = "F";
            //_lindenmayerSystem.AddRule("F", "F[+F]F[-F]F");

            //_lindenmayerSystem.Axiom = "F";
            //_lindenmayerSystem.AddRule("F", "F[+F]F[-F][F]");

            _lindenmayerSystem.Reset();

            _lindenmayerSystem.Axiom = Axion.text == "" ? "F" : Axion.text;

            _lindenmayerSystem.AddRule(RuleA.text);

            if (RuleB.text != "")
            {
                _lindenmayerSystem.AddRule(RuleB.text);
            }


            _turtle.Reset();
            //TODO error handling
            _iteration = Iteration.text == "" ? 1 : int.Parse(Iteration.text);
            _turtle.Sentence = _lindenmayerSystem.Expand(_iteration);

            _turtle.Iteration = _iteration;

            Camera camera = MainCamera.GetComponent<Camera>();
            camera.orthographicSize = 1.0f;
            camera.transform.position = new Vector3();
        }

        public void SetPreset(string index)
        {
            Debug.Log("preset index" + index);
        }

        protected void Start()
        {
            Debug.Log("Main Started");

            _turtle.Material = Material;

            Reset();

            Root = this;
        }

        private void Update()
        {
            Camera camera = MainCamera.GetComponent<Camera>();
            //Debug.Log("Main Input" + InputField.text.ToString());
            for (var i = 0; i < _iteration; ++i)
            {
                if (_turtle.HasNext())
                {
                    _turtle.Next();
                    if (_turtle.MaxHeight > camera.orthographicSize)
                    {
                        camera.orthographicSize = _turtle.MaxHeight * 0.5f;
                        camera.transform.position = new Vector3(0.0f, _turtle.MaxHeight / 2.0f, -10);
                    }
                }
            }
        }
    }
}

