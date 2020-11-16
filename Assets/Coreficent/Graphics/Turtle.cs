﻿namespace Coreficent.Graphics
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Turtle
    {

        public Material Material;
        public string Sentence = "F[+F]F[-F]F";

        private float Angle = 0.0f;
        private Vector2 Position = new Vector2();
        private int index = 0;
        private Stack<Tuple<Vector2, float>> stack = new Stack<Tuple<Vector2, float>>();
        private float moveDistance = 1.0f;
        private LineRenderer _line;
        private int lineCount = 0;

        public bool HasNext()
        {
            return index < Sentence.Length;
        }
        public void Next()
        {
            if (HasNext())
            {
                switch (Sentence[index])
                {
                    case 'F':
                        MoveForward();
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
                Debug.Log("location: " + Position);
                Debug.Log("angle: " + Angle);
                ++index;
            }
            else
            {
                Debug.Log("Drawing Complete");
            }
        }
        public void MoveForward()
        {
            Debug.Log("draw" + lineCount);
            var radian = Angle * Mathf.Deg2Rad;
            // reversed for so that it grows from the bottom to top.
            var x = Mathf.Cos(radian);
            var y = Mathf.Sin(radian);
            var pos = new Vector2(x * moveDistance, y * moveDistance);

            CreateLine();
            Debug.Log("line start " + Position);
            _line.SetPosition(0, Position);

            Position += pos;

            Debug.Log("line end " + Position);
            _line.SetPosition(1, Position);
            _line = null;
            lineCount++;
        }
        void CreateLine()
        {
            _line = new GameObject("Line" + lineCount).AddComponent<LineRenderer>();
            _line.material = Material;
            _line.positionCount = 2;
            _line.startWidth = 0.15f;
            _line.endWidth = 0.15f;
            _line.useWorldSpace = false;
            _line.numCapVertices = 50;
        }
    }
}