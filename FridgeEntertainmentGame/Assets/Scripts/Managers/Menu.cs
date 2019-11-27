using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool menuActive;
    public PointWalk playerCon;
    public GameObject toggle;

    // Start is called before the first frame update
    void Start()
    {
        // Set this object to disabled
        toggle.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Toggle()
    {
        // menu already on
        if (menuActive == true)
        {
            menuActive = false;
            playerCon.active = true;
            toggle.SetActive(false);
        }
        // menu already off
        else if (menuActive == false)
        {
            menuActive = true;
            playerCon.active = false;
            toggle.SetActive(true);
        }
    }

    public void ContinueBtn()
    {
        playerCon.active = true;
        toggle.SetActive(false);
    }

    public void MainMenuBtn(string sceneLoad)
    {
        SceneManager.LoadScene(sceneLoad);
    }
}
