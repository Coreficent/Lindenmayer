namespace Coreficent.Main
{
    using Coreficent.Grammar;
    using Coreficent.Graphics;
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    /*
     * the main entry of the program.
     */
    public class Main : MonoBehaviour
    {
        static public Main Root = null;

        public Button Rerender;
        public GameObject Flower;
        public GameObject LeafCactus;
        public GameObject LeafGeneric;
        public GameObject LeafMushroom;
        public GameObject LeafPlum;
        public GameObject LeafSilicon;
        public GameObject MainCamera;
        public Image Progress;
        public InputField Axiom;
        public InputField RuleA;
        public InputField RuleB;
        public InputField RuleC1;
        public InputField RuleC2;
        public Material Aqua;
        public Material Black;
        public Material Blue;
        public Material BranchGeneric;
        public Material Green;
        public Material Invisible;
        public Material Red;
        public Material Silver;
        public Material TrunkGeneric;
        public Material White;
        public Material Yellow;
        public Slider Angle;
        public Slider AngleDeviation;
        public Slider Iteration;
        public Slider Length;
        public Slider LengthDeviation;
        public Slider Thickness;
        public Text AngleText;
        public Text IterationText;
        public Text LengthText;
        public Text MessageText;
        public Text ThicknessText;
        public Toggle Animation;
        public Toggle Style;

        private readonly LindenmayerSystem _lindenmayerSystem = new LindenmayerSystem();
        private readonly Turtle _turtle = new Turtle();
        /*
         * try reading from the parameters and render the graphics or outputting an error if unable to render.
         */
        public void Render()
        {
            MessageText.text = "";

            try
            {
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
            catch (Exception exception)
            {
                MessageText.text = "Could not render due to: " + exception.GetType().Name;
            }
        }
        /*
         * update the display values when sliding.
         */
        public void UpdateSlider()
        {
            AngleText.text = "Angle:" + Angle.value + ", " + "Deviation:" + AngleDeviation.value;
            LengthText.text = "Length:" + Length.value + ", " + "Deviation:" + LengthDeviation.value;
            ThicknessText.text = "Thickness:" + Thickness.value;
            IterationText.text = "Order:" + Iteration.value;
        }
        /*
         * update to pre-configured presets when the dropdown menu is updated.
         */
        public void SetPreset(int index)
        {
            switch (index)
            {
                case 0:
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
                    _turtle.FlowerSprite = Flower;
                    break;
                case 1:
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
                    _turtle.Trunk = BranchGeneric;
                    _turtle.Branch = Green;
                    _turtle.Leaf = Yellow;
                    _turtle.LeafSprite = LeafCactus;
                    _turtle.FlowerSprite = null;
                    break;
                case 2:
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
                    _turtle.Trunk = Green;
                    _turtle.Branch = Yellow;
                    _turtle.Leaf = Green;
                    _turtle.LeafSprite = null;
                    _turtle.FlowerSprite = null;
                    break;
                case 3:
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
                    _turtle.FlowerSprite = null;
                    break;
                case 4:
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
                    _turtle.Trunk = TrunkGeneric;
                    _turtle.Branch = BranchGeneric;
                    _turtle.Leaf = Red;
                    _turtle.LeafSprite = LeafPlum;
                    _turtle.FlowerSprite = null;
                    break;
                case 5:
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
                    _turtle.FlowerSprite = null;
                    break;
                case 6:
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
                    _turtle.Trunk = Green;
                    _turtle.Branch = Yellow;
                    _turtle.Leaf = Red;
                    _turtle.LeafSprite = LeafPlum;
                    _turtle.FlowerSprite = null;
                    break;
                case 7:
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
                    _turtle.Trunk = BranchGeneric;
                    _turtle.Branch = Yellow;
                    _turtle.Leaf = Yellow;
                    _turtle.LeafSprite = null;
                    _turtle.FlowerSprite = null;
                    break;
                case 8:
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
                    _turtle.FlowerSprite = null;
                    break;
                case 9:
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
                    _turtle.Branch = White;
                    _turtle.Leaf = Silver;
                    _turtle.LeafSprite = LeafSilicon;
                    _turtle.FlowerSprite = null;
                    break;
                case 10:
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
                    _turtle.FlowerSprite = null;
                    break;
                default:
                    Debug.Log("unexpected preset");
                    break;
            }

            Render();
        }
        /*
         * initialize member variables and the main singleton.
         */
        protected void Start()
        {
            _turtle.Invisible = Invisible;
            _turtle.Simple = Black;
            _turtle.Trunk = Red;
            _turtle.Branch = Blue;
            _turtle.Leaf = Green;

            SetPreset(0);
            Render();

            Root = this;
        }
        /*
         * animate the current l system or instantly render it on screen.
         */
        private void Update()
        {
            if (_turtle.HasNext())
            {
                // increase the animation batch for larger trees.
                for (var i = 0; i < (Animation.isOn ? _turtle.Iteration * Time.deltaTime * 50.0f : _turtle.Sentence.Length); ++i)
                {
                    _turtle.Next();
                }
                // adjust the camera to fit the graphics on screen.
                float border = 1.25f;
                Camera camera = MainCamera.GetComponent<Camera>();
                if (_turtle.MaxHeight > camera.orthographicSize)
                {
                    camera.orthographicSize = Mathf.Max(_turtle.MaxHeight * 0.5f * border, camera.orthographicSize);
                }
                if (_turtle.MaxWidth > camera.orthographicSize)
                {
                    camera.orthographicSize = Mathf.Max(_turtle.MaxWidth * border, camera.orthographicSize);
                }
                camera.transform.position = new Vector3(0.0f, camera.orthographicSize, -10);
                Progress.transform.localScale = new Vector3(_turtle.Progress, 1.0f, 1.0f);
            }
        }
    }
}