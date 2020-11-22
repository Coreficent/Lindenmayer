namespace Coreficent.Graphics
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    public class Turtle
    {
        public Material Trunk;
        public Material Branch;
        public Material Leaf;
        public string Sentence = "F[+F]F[-F]F";
        public float MaxWidth = 0.0f;
        public float MaxHeight = 0.0f;
        public int Iteration = 0;

        public float Angle = 45.0f;
        public float AngleDeviation = 0.0f;
        public float Length = 1.0f;
        public float LengthDeviation = 1.0f;


        private float _defaultAngle = 90.0f;
        private Vector2 _position = new Vector2();
        private int _index = 0;
        private readonly Stack<Tuple<Vector2, float, float>> _stack = new Stack<Tuple<Vector2, float, float>>();
        private readonly List<LineRenderer> _lines = new List<LineRenderer>();
        private int _lineCount = 0;
        private readonly float _thicknessMultiplier = 0.5f;
        private float _thickness = 1.0f;
        private float _currentThickness = 1.0f;
        private float _thicknessDiminisherAmount = 0.75f;

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
            _position = new Vector2();
            _defaultAngle = 90.0f;
            _index = 0;
            MaxWidth = 0.0f;
            MaxHeight = 0.0f;
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

                        LineRenderer leaf = _lines[_lines.Count - 1];
                        leaf.material = Leaf;

                        //leaf.SetPosition(0, leaf.GetPosition(0) - new Vector3(0.0f, 0.0f, 0.1f));
                        //leaf.SetPosition(1, leaf.GetPosition(1) - new Vector3(0.0f, 0.0f, 0.1f));
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
                //Debug.Log("location: " + Position);
                //Debug.Log("angle: " + Angle);
                ++_index;
            }
            else
            {
                Debug.Log("Drawing Complete");
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
            //Debug.Log("draw" + lineCount);
            float radian = (_defaultAngle + UnityEngine.Random.Range(-AngleDeviation * 0.5f, AngleDeviation * 0.5f)) * Mathf.Deg2Rad;
            // reversed for so that it grows from the bottom to top.
            float x = Mathf.Cos(radian);
            float y = Mathf.Sin(radian);
            float currentLength = Length * (1.0f + UnityEngine.Random.Range(0.0f, LengthDeviation));
            Vector2 pos = new Vector2(x * currentLength, y * currentLength);

            LineRenderer line = CreateLine();
            //Debug.Log("line start " + Position);
            line.SetPosition(0, _position);

            _position += pos;

            //Debug.Log("line end " + Position);
            line.SetPosition(1, _position);

            MaxWidth = Mathf.Max(MaxWidth, Mathf.Abs(_position.x));
            MaxHeight = Mathf.Max(MaxHeight, Mathf.Abs(_position.y));

            _lineCount++;
        }
        private LineRenderer CreateLine()
        {
            LineRenderer line = new GameObject("Segment::" + _lineCount).AddComponent<LineRenderer>();
            line.material = _currentThickness < Thickness * _thicknessDiminisherAmount ? Branch : Trunk;
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