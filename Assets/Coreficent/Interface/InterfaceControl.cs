namespace Coreficent.Interface
{
    using Coreficent.Main;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class InterfaceControl : MonoBehaviour
    {
        public void Render()
        {
            Main.Root.Render();
        }
        public void SelectPreset(int index)
        {
            Main.Root.SetPreset(index);
        }
    }
}