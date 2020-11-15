namespace Coreficent.Main
{
    using UnityEngine;
    public class LinearCurve : MonoBehaviour
    {
        public Material Material;

        private LineRenderer _line;
        private Vector3 _mousePosition;
        private int lineCount = 0;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_line == null)
                {
                    createLine();
                }

                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mousePosition.z = 0;
                _line.SetPosition(0, _mousePosition);
                _line.SetPosition(1, _mousePosition);
            }
            else if (Input.GetMouseButtonUp(0) && _line)
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mousePosition.z = 0;
                _line.SetPosition(1, _mousePosition);
                _line = null;
                lineCount++;
            }
            else if (Input.GetMouseButton(0) && _line)
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mousePosition.z = 0;
                _line.SetPosition(1, _mousePosition);
            }
        }

        void createLine()
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