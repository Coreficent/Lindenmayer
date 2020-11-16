namespace Coreficent.Main
{
    using Coreficent.Grammar;
    using Coreficent.Graphics;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        public GameObject MainCamera;
        public Material Material;

        private readonly LindenmayerSystem _lindenmayerSystem = new LindenmayerSystem();
        private readonly Turtle _turtle = new Turtle();
        protected void Start()
        {
            Debug.Log("Main Started");

            _turtle.Material = Material;
            _turtle.Sentence = _lindenmayerSystem.Expand(3);
        }

        private void Update()
        {
            if (_turtle.HasNext())
            {
                _turtle.Next();
                if (_turtle.MaxHeight > MainCamera.GetComponent<Camera>().orthographicSize)
                {
                    MainCamera.GetComponent<Camera>().orthographicSize = _turtle.MaxHeight;
                    MainCamera.GetComponent<Camera>().transform.position = new Vector3(0.0f, _turtle.MaxHeight / 2.0f, -10);
                }
                Debug.Log("aspect" + MainCamera.GetComponent<Camera>().aspect);
            }
            else
            {
                enabled = false;
            }
        }
    }
}

