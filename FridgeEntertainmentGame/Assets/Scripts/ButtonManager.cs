using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void newGamebtn(string newGameLvl)
    {
        SceneManager.LoadScene(newGameLvl);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
