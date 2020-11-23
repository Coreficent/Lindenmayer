namespace Coreficent.Graphics
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    public class Turtle
    {
        public enum RenderStyle { Simple, Complex };

        public GameObject FlowerSprite;
        public GameObject LeafSprite;
        public Material Branch;
        public Material Invisible;
        public Material Leaf;
        public Material Simple;
        public Material Trunk;
        public RenderStyle Style = RenderStyle.Simple;
        public float Angle = 45.0f;
        public float AngleDeviation = 0.0f;
        public float Length = 1.0f;
        public float LengthDeviation = 1.0f;
        public float MaxHeight = 0.0f;
        public float MaxWidth = 0.0f;
        public int Iteration = 0;
        public string Sentence = "F[+F]F[-F]F";

        private Vector2 _position = new Vector2();
        private float _currentThickness = 1.0f;
        private float _defaultAngle = 90.0f;
        private float _thickness = 1.0f;
        private float _thicknessDiminisherAmount = 0.75f;
        private int _index = 0;
        private int _lineCount = 0;
        private readonly List<GameObject> _leafSprites = new List<GameObject>();
        private readonly List<LineRenderer> _lines = new List<LineRenderer>();
        private readonly Stack<Tuple<Vector2, float, float>> _stack = new Stack<Tuple<Vector2, float, float>>();
        private readonly float _thicknessMultiplier = 0.5f;


        public float Progress => (float)_index / Sentence.Length;
        public float Thickness
        {
            get { return _thickness; }
            set
            {
                _thickness = value;
                _currentThickness = value;
            }
        }
        public void Reset()
        {
            foreach (LineRenderer i in _lines)
            {
                UnityEngine.Object.Destroy(i.gameObject);
            }
            _lines.Clear();
            _lineCount = 0;
            _stack.Clear();
            foreach (GameObject i in _leafSprites)
            {
                UnityEngine.Object.Destroy(i);
            }
            _leafSprites.Clear();
            _position = new Vector2();
            _defaultAngle = 90.0f;
            _index = 0;
            MaxWidth = 0.0f;
            MaxHeight = 0.0f;
            Style = RenderStyle.Simple;
        }
        public bool HasNext()
        {
            return _index < Sentence.Length;
        }
        public void Next()
        {
            bool drawn = false;
            if (HasNext())
            {
                switch (Sentence[_index])
                {
                    case 'F':
                        MoveForward();
                        drawn = true;
                        break;
                    case '[':
                        _stack.Push(new Tuple<Vector2, float, float>(_position, _defaultAngle, _currentThickness));
                        _currentThickness *= _thicknessDiminisherAmount;
                        break;
                    case ']':
                        Tuple<Vector2, float, float> tuple = _stack.Pop();
                        _position = tuple.Item1;
                        _defaultAngle = tuple.Item2;
                        _currentThickness = tuple.Item3;

                        if (Style == RenderStyle.Complex)
                        {
                            LineRenderer leaf = _lines[_lines.Count - 1];
                            if (LeafSprite)
                            {

                                GameObject sprite;
                                if (FlowerSprite && UnityEngine.Random.Range(0.0f, 250.0f) < 1.0f)
                                {
                                    sprite = UnityEngine.Object.Instantiate(FlowerSprite);
                                    sprite.transform.position += -new Vector3(0.0f, 0.0f, 0.1f);
                                }
                                else
                                {
                                    sprite = UnityEngine.Object.Instantiate(LeafSprite);
                                }
                                Vector3 final = leaf.GetPosition(1);
                                Vector3 initial = leaf.GetPosition(0);
                                sprite.transform.position += initial - new Vector3(0.0f, 0.0f, 0.1f);
                                _leafSprites.Add(sprite);
                                float spriteAngle = Mathf.Atan2(final.y - initial.y, final.x - initial.x) * Mathf.Rad2Deg - _defaultAngle;
                                Vector3 currentAngle = sprite.transform.eulerAngles;
                                currentAngle.z = spriteAngle;
                                sprite.transform.eulerAngles = currentAngle;
                                sprite.transform.localScale = sprite.transform.localScale * (1.0f + UnityEngine.Random.Range(0.0f, LengthDeviation)) * Thickness * 0.05f;

                                leaf.material = Invisible;
                            }
                            else
                            {
                                leaf.material = Leaf;
                            }
                        }
                        break;
                    case '+':
                        _defaultAngle += Angle;
                        break;
                    case '-':
                        _defaultAngle -= Angle;
                        break;
                    default:
                        break;
                }
                ++_index;
            }
            if (drawn)
            {
                return;
            }
            else
            {
                if (HasNext())
                {
                    Next();
                }
            }
        }
        public void MoveForward()
        {
            float radian = (_defaultAngle + UnityEngine.Random.Range(-AngleDeviation * 0.5f, AngleDeviation * 0.5f)) * Mathf.Deg2Rad;
            float currentLength = Length * (1.0f + UnityEngine.Random.Range(0.0f, LengthDeviation));
            Vector2 pos = new Vector2(Mathf.Cos(radian) * currentLength, Mathf.Sin(radian) * currentLength);

            LineRenderer line = CreateLine();
            line.SetPosition(0, _position);

            _position += pos;

            line.SetPosition(1, _position);

            MaxWidth = Mathf.Max(MaxWidth, Mathf.Abs(_position.x));
            MaxHeight = Mathf.Max(MaxHeight, Mathf.Abs(_position.y));

            _lineCount++;
        }
        private LineRenderer CreateLine()
        {
            LineRenderer line = new GameObject("Segment::" + _lineCount).AddComponent<LineRenderer>();
            line.material = Style == RenderStyle.Simple ? Simple : _currentThickness < Thickness * _thicknessDiminisherAmount ? Branch : Trunk;
            line.positionCount = 2;
            line.startWidth = _currentThickness * _thicknessMultiplier;
            line.endWidth = _currentThickness * _thicknessMultiplier;
            line.useWorldSpace = false;
            line.numCapVertices = 4;

            _lines.Add(line);

            return line;
        }
    }
}