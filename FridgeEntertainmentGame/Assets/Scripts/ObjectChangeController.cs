using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeController : MonoBehaviour
{
    public GameObject controller;
    public string tagName;
    private GameObject prev;
    private GameObject post;
    
    private bool collided = false;

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
        if(collided == true)
        {
            StartCoroutine(Wait());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagName)
        {
            collided = true;
            prev.GetComponent<ParticleSystem>().Play();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        // play effect
        prev.GetComponent<MeshRenderer>().enabled = false;
        post.GetComponent<MeshRenderer>().enabled = true;
    }
}
