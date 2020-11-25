namespace Coreficent.Interface
{
    using Coreficent.Main;
    using UnityEngine;

    /*
     * this class contains methods to control the behaviors of the user interface.
     */
    public class InterfaceControl : MonoBehaviour
    {
        /*
         * render the graphics given current rules.
         */
        public void Render()
        {
            if (Main.Root)
            {
                Main.Root.Render();
            }
        }
        /*
         * select a pre-configured rule.
         */
        public void SelectPreset(int index)
        {
            if (Main.Root)
            {
                Main.Root.SetPreset(index);
                Main.Root.Render();
            }
        }
        /*
         * update the slider and render the graphics
         */
        public void Slide()
        {
            if (Main.Root)
            {
                Main.Root.UpdateSlider();
                Main.Root.Render();
            }
        }
    }
}