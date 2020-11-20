namespace Coreficent.Interface
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ButtonControl : MonoBehaviour
    {
        public void Restart()
        {
            Debug.Log("on clicked");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}