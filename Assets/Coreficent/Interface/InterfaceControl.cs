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
            Main.Root.SetPreset(index);
        }
        public void SlideAngle(float angle)
        {

        }
        public void SlideLength(float length)
        {

        }
        public void SlideThickness(float thickness)
        {
            if (Main.Root)
            {
                Main.Root.SetThickness(thickness);
            }
        }
    }
}