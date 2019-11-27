using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadNewScene : MonoBehaviour
{
    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
