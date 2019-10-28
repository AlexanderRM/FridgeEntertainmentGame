using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjChangeCon : MonoBehaviour
{
    // Declare values
    public GameObject controller;
    public float waitSeconds = 2f;
    GameObject prev;
    GameObject post;
    
    bool coffee = false;
    bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set our values
       prev = controller.transform.GetChild(0).gameObject;
       post = controller.transform.GetChild(1).gameObject;

        prev.SetActive(true);
        post.SetActive(false);
        prev.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(coffee == true)
        {
            toggled = true;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        // wait 2 seconds
        prev.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(waitSeconds);
        prev.SetActive(false);
        post.SetActive(true);
    }

    public void Change()
    {
        if (toggled == false)
        {
            coffee = true;
        }
    }
}
