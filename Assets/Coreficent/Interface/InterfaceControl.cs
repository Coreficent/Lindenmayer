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
            Debug.Log("on restart");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Main.Root.Render();
        }

        public void SetPreset(string index)
        {
            //Main.Root.SetPreset(index);
        }

        public void SelectPreset(int index)
        {
            Debug.Log("choosing preset::" + index);
            Main.Root.SetPreset(index);
        }
    }
}