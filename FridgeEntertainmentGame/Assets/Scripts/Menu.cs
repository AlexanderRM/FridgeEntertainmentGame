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
        if (menuActive == true)
        {
            toggle.SetActive(true);
            menuActive = false;
            playerCon.active = false;
        }
        else
        {
            menuActive = true;
            playerCon.active = true;
            toggle.SetActive(false);
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
