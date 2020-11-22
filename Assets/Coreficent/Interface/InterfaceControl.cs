namespace Coreficent.Interface
{
    using Coreficent.Main;
    using UnityEngine;

    public class InterfaceControl : MonoBehaviour
    {
        public void Render()
        {
            if (Main.Root)
            {
                Main.Root.Render();
            }
        }
        public void SelectPreset(int index)
        {
            // Debug.Log("index::" + index);
            if (Main.Root)
            {
                Main.Root.SetPreset(index);
            }
        }
    }
}