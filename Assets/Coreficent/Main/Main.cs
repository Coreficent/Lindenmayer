﻿namespace Coreficent.Main
{
    using Coreficent.Grammar;
    using Coreficent.Graphics;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
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
            }
            else
            {
                enabled = false;
            }
        }
    }
}
