using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
            particle.Play();
    }

    void OnTriggerStay(Collider collider)
    {
        // Dialogue
    }

    void OnTriggerExit(Collider other)
    {
        particle.Stop();
    }
}
