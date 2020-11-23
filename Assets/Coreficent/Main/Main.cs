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
        public Material Invisible;
        public Material Black;
        public Material White;
        public Material Green;
        public Material Red;
        public Material Blue;
        public Material Yellow;
        public Material Silver;
        public Material Aqua;
        public Material TrunkGeneric;
        public Material BranchGeneric;
        public InputField Axiom;
        public InputField RuleA;
        public InputField RuleB;
        public InputField RuleC1;
        public InputField RuleC2;
        public Slider Angle;
        public Slider AngleDeviation;
        public Slider Length;
        public Slider LengthDeviation;
        public Slider Thickness;
        public Slider Iteration;
        public Button Rerender;
        public Image Progress;
        public Toggle Animation;
        public Toggle Style;
        public Text AngleText;
        public Text LengthText;
        public Text ThicknessText;
        public Text IterationText;
        public GameObject LeafGeneric;
        public GameObject LeafSilicon;
        public GameObject LeafMushroom;

        private readonly LindenmayerSystem _lindenmayerSystem = new LindenmayerSystem();
        private readonly Turtle _turtle = new Turtle();

        //TODO error handling

        public void Render()
        {
            Debug.Log("here");
            UpdateSlider();

            _lindenmayerSystem.Reset();
            _lindenmayerSystem.Axiom = Axiom.text;
            _lindenmayerSystem.AddRule(RuleA.text);
            _lindenmayerSystem.AddRule(RuleB.text);
            _lindenmayerSystem.AddChanceRule(RuleC1.text, RuleC2.text);

            _turtle.Reset();
            _turtle.Angle = Angle.value;
            _turtle.AngleDeviation = AngleDeviation.value;
            _turtle.Length = Length.value;
            _turtle.LengthDeviation = LengthDeviation.value;
            _turtle.Thickness = Thickness.value;
            _turtle.Iteration = (int)Iteration.value;
            _turtle.Sentence = _lindenmayerSystem.Expand(_turtle.Iteration);
            _turtle.Style = Style.isOn ? Turtle.RenderStyle.Complex : Turtle.RenderStyle.Simple;

            Camera camera = MainCamera.GetComponent<Camera>();
            camera.orthographicSize = 1.0f;
            camera.transform.position = new Vector3();
        }
        public void UpdateSlider()
        {
            AngleText.text = "Angle:" + Angle.value + ", " + "Deviation:" + AngleDeviation.value;
            LengthText.text = "Length:" + Length.value + ", " + "Deviation:" + LengthDeviation.value;
            ThicknessText.text = "Thickness:" + Thickness.value;
            IterationText.text = "Order:" + Iteration.value;
        }
        public void SetPreset(int index)
        {
            switch (index)
            {
                case 0:
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F]F";
                    RuleB.text = "";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 5;
                    Angle.value = 25.7f;
                    AngleDeviation.value = 0.0f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    LengthDeviation.value = 0.0f;
                    break;
                case 1:
                    Axiom.text = "F";
                    RuleA.text = "F=F[+F]F[-F][F]";
                    RuleB.text = "";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 5;
                    Angle.value = 20.0f;
                    Length.value = 1.0f;
                    Thickness.value = 2.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    break;
                case 2:
                    Axiom.text = "F";
                    RuleA.text = "F=FF-[-F+F+F]+[+F-F-F]";
                    RuleB.text = "";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 4;
                    Angle.value = 22.5f;
                    Length.value = 1.0f;
                    Thickness.value = 1.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    _turtle.Trunk = TrunkGeneric;
                    _turtle.Branch = Yellow;
                    _turtle.Leaf = Red;
                    _turtle.LeafSprite = null;
                    break;
                case 3:
                    Axiom.text = "X";
                    RuleA.text = "X=F[+X]F[-X]+X";
                    RuleB.text = "F=FF";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 7;
                    Angle.value = 20.0f;
                    Length.value = 1.0f;
                    Thickness.value = 8.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    break;
                case 4:
                    Axiom.text = "X";
                    RuleA.text = "X=F[+X][-X]FX";
                    RuleB.text = "F=FF";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 7;
                    Angle.value = 25.7f;
                    Length.value = 1.0f;
                    Thickness.value = 8.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    _turtle.Trunk = Aqua;
                    _turtle.Branch = Silver;
                    _turtle.Leaf = White;
                    _turtle.LeafSprite = null;
                    break;
                case 5:
                    Axiom.text = "X";
                    RuleA.text = "X=F-[[X]+X]+F[+FX]-X";
                    RuleB.text = "F=FF";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 5;
                    Angle.value = 22.5f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    break;
                case 6:
                    Axiom.text = "Z";
                    RuleA.text = "Z=ZFX[+Z][-Z]";
                    RuleB.text = "X=X[-FFF][+FFF]FX";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 4;
                    Angle.value = 15.0f;
                    Length.value = 3.0f;
                    Thickness.value = 1.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    _turtle.Trunk = TrunkGeneric;
                    _turtle.Branch = BranchGeneric;
                    _turtle.Leaf = Yellow;
                    _turtle.LeafSprite = null;
                    break;
                case 7:
                    Axiom.text = "FX";
                    RuleA.text = "X=[-FX][+FX]";
                    RuleB.text = "";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 5;
                    Angle.value = 20.0f;
                    Length.value = 1.0f;
                    Thickness.value = 4.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    _turtle.Trunk = White;
                    _turtle.Branch = Silver;
                    _turtle.Leaf = Silver;
                    _turtle.LeafSprite = LeafMushroom;
                    break;
                case 8:
                    Axiom.text = "F";
                    RuleA.text = "F=FF[+F][-F]";
                    RuleB.text = "";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 6;
                    Angle.value = 90.0f;
                    Length.value = 1.0f;
                    Thickness.value = 3.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    _turtle.Trunk = Silver;
                    _turtle.Branch = Silver;
                    _turtle.Leaf = Silver;
                    _turtle.LeafSprite = LeafSilicon;
                    break;
                case 9:
                    Axiom.text = "X";
                    RuleA.text = "X=[+++FFFF-F-F-F[X]-F--F--F--F--F][---FFFF+F+F+F[X]+F++F++F++F++F]";
                    RuleB.text = "F=FF";
                    RuleC1.text = "";
                    RuleC2.text = "";
                    Iteration.value = 5;
                    Angle.value = 15.0f;
                    Length.value = 1.0f;
                    Thickness.value = 25.0f;
                    AngleDeviation.value = 0.0f;
                    LengthDeviation.value = 0.0f;
                    _turtle.Trunk = Red;
                    _turtle.Branch = Red;
                    _turtle.Leaf = Red;
                    _turtle.LeafSprite = null;
                    break;
                case 10:
                    Axiom.text = "X";
                    RuleA.text = "X=[-FX][+FX]";
                    RuleB.text = "F=FF";
                    RuleC1.text = "X=[-FX]FX[+FX]";
                    RuleC2.text = "X=FX[-FX][+FX]FX";
                    Iteration.value = 4;
                    Angle.value = 15.0f;
                    Length.value = 1.0f;
                    Thickness.value = 10.0f;
                    AngleDeviation.value = 45.0f;
                    LengthDeviation.value = 10.0f;
                    _turtle.Trunk = TrunkGeneric;
                    _turtle.Branch = BranchGeneric;
                    _turtle.Leaf = Green;
                    _turtle.LeafSprite = LeafGeneric;
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

            _turtle.Invisible = Invisible;
            _turtle.Simple = Black;
            _turtle.Trunk = Red;
            _turtle.Branch = Blue;
            _turtle.Leaf = Green;

            SetPreset(0);
            Render();

            Root = this;
        }
        private void Update()
        {
            if (_turtle.HasNext())
            {
                Camera camera = MainCamera.GetComponent<Camera>();
                // increase the animation batch for larger trees
                for (var i = 0; i < (Animation.isOn ? _turtle.Iteration * Time.deltaTime * 50.0f : _turtle.Sentence.Length); ++i)
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
                Progress.transform.localScale = new Vector3(_turtle.Progress, 1.0f, 1.0f);
            }
        }
    }
}