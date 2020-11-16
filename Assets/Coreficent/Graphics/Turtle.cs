using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    public float Angle = 0.0f;
    public Vector2 Position = new Vector2();
    // Start is called before the first frame update
    private int index = 0;
    private string sentence = "F[+F]F[-F]F";
    private Stack<Tuple<Vector2, float>> stack = new Stack<Tuple<Vector2, float>>();
    private float moveDistance = 5.0f;
    void Start()
    {
        Debug.Log("Turtle Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (index < sentence.Length)
        {
            switch (sentence[index])
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
            enabled = false;
        }
    }

    public void MoveForward()
    {
        var radian = Angle * Mathf.Deg2Rad;
        var x = Mathf.Cos(radian);
        var y = Mathf.Sin(radian);
        var pos = new Vector2(x * moveDistance, y * moveDistance);
        Position += pos;
    }
}
