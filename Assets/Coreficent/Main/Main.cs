namespace Coreficent.Main
{
    using Coreficent.Grammar;
    using Coreficent.Graphics;
    using UnityEngine;
    using UnityEngine.UI;

    public class Main : MonoBehaviour
    {
        static public Main Root = null;

        public GameObject MainCamera;
        public Material Material;
        public InputField Axiom;
        public InputField RuleA;
        public InputField RuleB;
        public InputField Angle;
        public InputField Length;
        public InputField Thickness;
        public InputField Iteration;
        public Button Rerender;
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

        //TODO error handling

        public void Render()
        {
            _lindenmayerSystem.Reset();

            _lindenmayerSystem.Axiom = Axiom.text;
            _lindenmayerSystem.AddRule(RuleA.text);
            _lindenmayerSystem.AddRule(RuleB.text);

            _turtle.Reset();
            _turtle.Angle = float.Parse(Angle.text);
            _turtle.MoveDistance = float.Parse(Length.text);
            _turtle.Thickness = float.Parse(Thickness.text);
            _turtle.Iteration = int.Parse(Iteration.text);
            _turtle.Sentence = _lindenmayerSystem.Expand(_turtle.Iteration);

            Camera camera = MainCamera.GetComponent<Camera>();
            camera.orthographicSize = 1.0f;
            camera.transform.position = new Vector3();
        }

        public void SetPreset(string index)
        {
            Debug.Log("preset index" + index);

            _lindenmayerSystem.Reset();

            switch (index)
            {
                case "0":
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    Iteration.text = "5";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "1":
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F][F]";
                    RuleB.text = "";
                    Iteration.text = "5";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "2":
                    Axiom.text = "F";
                    RuleA.text = "F=FF-[-F+F+F]+[+F-F-F]";
                    RuleB.text = "";
                    Iteration.text = "4";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "3":
                    Axiom.text = "X";
                    RuleA.text = "X=F[+X]F[-X]+X";
                    RuleB.text = "F=FF";
                    Iteration.text = "7";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "4":
                    Axiom.text = "X";
                    RuleA.text = "X=F[+X][-X]FX";
                    RuleB.text = "F=FF";
                    Iteration.text = "7";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "5":
                    Axiom.text = "X";
                    RuleA.text = "X=F-[[X]+X]+F[+FX]-X";
                    RuleB.text = "F=FF";
                    Iteration.text = "5";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "6":
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    Iteration.text = "3";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "7":
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    Iteration.text = "3";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "8":
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    Iteration.text = "3";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                case "9":
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    Iteration.text = "3";
                    Angle.text = "45.0";
                    Length.text = "1.0";
                    Thickness.text = "1.0";
                    break;
                default:
                    Debug.Log("unexpected preset");
                    break;
            }

            Render();
        }

        protected void Start()
        {
            Debug.Log("Main Started");

            _turtle.Material = Material;


            SetPreset("0");
            Render();

            Root = this;
        }

        private void Update()
        {
            Camera camera = MainCamera.GetComponent<Camera>();
            // increase the animation batch for larger trees
            for (var i = 0; i < _turtle.Iteration; ++i)
            {
                if (_turtle.HasNext())
                {
                    _turtle.Next();
                    if (_turtle.MaxHeight > camera.orthographicSize)
                    {
                        camera.orthographicSize = Mathf.Max(_turtle.MaxHeight * 0.5f, camera.orthographicSize);
                    }
                    if (_turtle.MaxWidth > camera.orthographicSize)
                    {
                        camera.orthographicSize = Mathf.Max(_turtle.MaxWidth, camera.orthographicSize);
                    }
                    camera.transform.position = new Vector3(0.0f, camera.orthographicSize, -10);
                }
            }
        }
    }
}

