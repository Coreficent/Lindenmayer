namespace Coreficent.Main
{
    using Coreficent.Grammar;
    using Coreficent.Graphics;
    using UnityEngine;
    public class Main : MonoBehaviour
    {
        public GameObject MainCamera;
        public Material Material;

        private readonly LindenmayerSystem _lindenmayerSystem = new LindenmayerSystem();
        private readonly Turtle _turtle = new Turtle();
        private int _iteration = 5;
        protected void Start()
        {
            Debug.Log("Main Started");

            _turtle.Iteration = _iteration;
            _turtle.Material = Material;
            _turtle.Sentence = _lindenmayerSystem.Expand(_iteration);
        }

        private void Update()
        {
            for (var i = 0; i < _iteration; ++i)
            {
                if (_turtle.HasNext())
                {
                    _turtle.Next();
                    if (_turtle.MaxHeight > MainCamera.GetComponent<Camera>().orthographicSize)
                    {
                        MainCamera.GetComponent<Camera>().orthographicSize = _turtle.MaxHeight * 0.75f;
                        MainCamera.GetComponent<Camera>().transform.position = new Vector3(0.0f, _turtle.MaxHeight / 2.0f, -10);
                    }
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }
}

