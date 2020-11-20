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

        private float Angle = 90.0f;
        private Vector2 Position = new Vector2();
        private int index = 0;
        private Stack<Tuple<Vector2, float>> stack = new Stack<Tuple<Vector2, float>>();
        private readonly float moveDistance = 1.0f;

        private List<LineRenderer> lines = new List<LineRenderer>();
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
            Angle = 90.0f;
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
                        stack.Push(new Tuple<Vector2, float>(Position, Angle));
                        break;
                    case ']':
                        Tuple<Vector2, float> tuple = stack.Pop();
                        Position = tuple.Item1;
                        Angle = tuple.Item2;
                        break;
                    case '+':
                        Angle += 45.0f;
                        break;
                    case '-':
                        Angle -= 45.0f;
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
            var radian = Angle * Mathf.Deg2Rad;
            // reversed for so that it grows from the bottom to top.
            var x = Mathf.Cos(radian);
            var y = Mathf.Sin(radian);
            var pos = new Vector2(x * moveDistance, y * moveDistance);

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
            line.startWidth = Iteration * 0.1f;
            line.endWidth = Iteration * 0.1f;
            line.useWorldSpace = false;
            line.numCapVertices = 50;

            lines.Add(line);

            return line;
        }
    }
}