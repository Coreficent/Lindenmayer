namespace Coreficent.Graphics
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    public class Turtle
    {
        public Material Material;
        public string Sentence = "F[+F]F[-F]F";
        public float MaxWidth = 0.0f;
        public float MaxHeight = 0.0f;
        public int Iteration = 0;

        public float MoveDistance = 1.0f;
        public float Angle = 45.0f;
        public float Thickness = 1.0f;

        private float defaultAngle = 90.0f;
        private Vector2 Position = new Vector2();
        private int index = 0;
        private Stack<Tuple<Vector2, float>> stack = new Stack<Tuple<Vector2, float>>();
        
        private readonly List<LineRenderer> lines = new List<LineRenderer>();
        private int lineCount = 0;

        public void Reset()
        {
            foreach (LineRenderer i in lines)
            {
                UnityEngine.Object.Destroy(i.gameObject);
            }
            lines.Clear();
            lineCount = 0;
            stack.Clear();
            Position = new Vector2();
            defaultAngle = 90.0f;
            index = 0;
            MaxWidth = 0.0f;
            MaxHeight = 0.0f;
        }
        public bool HasNext()
        {
            return index < Sentence.Length;
        }
        public void Next()
        {
            bool drawn = false;
            if (HasNext())
            {
                switch (Sentence[index])
                {
                    case 'F':
                        MoveForward();
                        drawn = true;
                        break;
                    case '[':
                        stack.Push(new Tuple<Vector2, float>(Position, defaultAngle));
                        break;
                    case ']':
                        Tuple<Vector2, float> tuple = stack.Pop();
                        Position = tuple.Item1;
                        defaultAngle = tuple.Item2;
                        break;
                    case '+':
                        defaultAngle += Angle;
                        break;
                    case '-':
                        defaultAngle -= Angle;
                        break;
                    default:
                        break;
                }
                //Debug.Log("location: " + Position);
                //Debug.Log("angle: " + Angle);
                ++index;
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
            var radian = defaultAngle * Mathf.Deg2Rad;
            // reversed for so that it grows from the bottom to top.
            var x = Mathf.Cos(radian);
            var y = Mathf.Sin(radian);
            var pos = new Vector2(x * MoveDistance, y * MoveDistance);

            LineRenderer line = CreateLine();
            //Debug.Log("line start " + Position);
            line.SetPosition(0, Position);

            Position += pos;

            //Debug.Log("line end " + Position);
            line.SetPosition(1, Position);

            MaxWidth = Mathf.Max(MaxWidth, Mathf.Abs(Position.x));
            MaxHeight = Mathf.Max(MaxHeight, Mathf.Abs(Position.y));

            lineCount++;
        }
        private LineRenderer CreateLine()
        {
            LineRenderer line = new GameObject("Line" + lineCount).AddComponent<LineRenderer>();
            line.material = Material;
            line.positionCount = 2;
            line.startWidth = Thickness;
            line.endWidth = Thickness;
            line.useWorldSpace = false;
            line.numCapVertices = 50;

            lines.Add(line);

            return line;
        }
    }
}