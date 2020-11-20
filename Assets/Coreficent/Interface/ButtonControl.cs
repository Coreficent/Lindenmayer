namespace Coreficent.Interface
{
    using Coreficent.Main;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ButtonControl : MonoBehaviour
    {
        public void Restart()
        {
            Debug.Log("on restart");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Main.Root.Reset();
        }
    }
}