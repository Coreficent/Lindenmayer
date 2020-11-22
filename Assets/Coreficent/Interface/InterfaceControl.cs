namespace Coreficent.Interface
{
    using Coreficent.Main;
    using UnityEngine;

    public class InterfaceControl : MonoBehaviour
    {
        public void Render()
        {
            Main.Root.Render();
        }
        public void SelectPreset(int index)
        {
            Debug.Log("index::" + index);
            Main.Root.SetPreset(index);
        }
    }
}