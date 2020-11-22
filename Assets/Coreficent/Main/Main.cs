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
        public Material Green;
        public Material Red;
        public Material Blue;
        public InputField Axiom;
        public InputField RuleA;
        public InputField RuleB;
        public Slider Angle;
        public Slider Length;
        public Slider Thickness;
        public Slider Iteration;
        public Button Rerender;

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
            _turtle.Angle = Angle.value;
            _turtle.MoveDistance = Length.value;
            _turtle.Thickness = Thickness.value;
            _turtle.Iteration = (int)Iteration.value;
            _turtle.Sentence = _lindenmayerSystem.Expand(_turtle.Iteration);

            Camera camera = MainCamera.GetComponent<Camera>();
            camera.orthographicSize = 1.0f;
            camera.transform.position = new Vector3();
        }
        public void SetPreset(int index)
        {
            //Debug.Log("preset index" + index);

            _lindenmayerSystem.Reset();

            switch (index)
            {
                case 0:
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    Iteration.value = 5;
                    Angle.value = 25.7f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    break;
                case 1:
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F][F]";
                    RuleB.text = "";
                    Iteration.value = 5;
                    Angle.value = 20.0f;
                    Length.value = 1.0f;
                    Thickness.value = 2.0f;
                    break;
                case 2:
                    Axiom.text = "F";
                    RuleA.text = "F=FF-[-F+F+F]+[+F-F-F]";
                    RuleB.text = "";
                    Iteration.value = 4;
                    Angle.value = 22.5f;
                    Length.value = 1.0f;
                    Thickness.value = 1.0f;
                    break;
                case 3:
                    Axiom.text = "X";
                    RuleA.text = "X=F[+X]F[-X]+X";
                    RuleB.text = "F=FF";
                    Iteration.value = 7;
                    Angle.value = 20.0f;
                    Length.value = 1.0f;
                    Thickness.value = 8.0f;
                    break;
                case 4:
                    Axiom.text = "X";
                    RuleA.text = "X=F[+X][-X]FX";
                    RuleB.text = "F=FF";
                    Iteration.value = 7;
                    Angle.value = 25.7f;
                    Length.value = 1.0f;
                    Thickness.value = 8.0f;
                    break;
                case 5:
                    Axiom.text = "X";
                    RuleA.text = "X=F-[[X]+X]+F[+FX]-X";
                    RuleB.text = "F=FF";
                    Iteration.value = 5;
                    Angle.value = 22.5f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    break;
                case 6:
                    Axiom.text = "Z";
                    RuleA.text = "Z=ZFX[+Z][-Z]";
                    RuleB.text = "X=X[-FFF][+FFF]FX";
                    Iteration.value = 4;
                    Angle.value = 15.0f;
                    Length.value = 3.0f;
                    Thickness.value = 1.0f;
                    break;
                case 7:
                    Axiom.text = "FX";
                    RuleA.text = "X=[-FX][+FX]";
                    RuleB.text = "";
                    Iteration.value = 10;
                    Angle.value = 20.0f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    break;
                case 8:
                    Axiom.text = "F";
                    RuleA.text = "F=FF[+F][-F]";
                    RuleB.text = "";
                    Iteration.value = 6;
                    Angle.value = 90.0f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    break;
                case 9:
                    Axiom.text = "+++FX";
                    RuleA.text = "X=[-FY]+FX";
                    RuleB.text = "Y=FX+FY-FX";
                    Iteration.value = 4;
                    Angle.value = 10.0f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
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

            _turtle.Trunk = Red;
            _turtle.Branch = Blue;
            _turtle.Leaf = Green;

            SetPreset(0);
            Render();

            Root = this;
        }

        private void Update()
        {
            Camera camera = MainCamera.GetComponent<Camera>();
            // increase the animation batch for larger trees
            for (var i = 0; i < _turtle.Iteration * Time.deltaTime * 50.0f; ++i)
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

