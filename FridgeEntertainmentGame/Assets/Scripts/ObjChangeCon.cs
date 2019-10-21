using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjChangeCon : MonoBehaviour
{
    // Declare values
    public GameObject controller;
    public string colTagName;
    GameObject prev;
    GameObject post;
    
    bool coffee = false;
    bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
       prev = controller.transform.GetChild(0).gameObject;
       post = controller.transform.GetChild(1).gameObject;

        prev.GetComponent<MeshRenderer>().enabled = true;
        post.GetComponent<MeshRenderer>().enabled = false;
        prev.GetComponent<ParticleSystem>().Stop();
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
        yield return new WaitForSeconds(1);
        // play effect
        prev.GetComponent<MeshRenderer>().enabled = false;
        post.GetComponent<MeshRenderer>().enabled = true;
    }

    public void Change()
    {
        if (toggled == false)
        {
            prev.GetComponent<ParticleSystem>().Play();
            coffee = true;
        }
    }
}
