using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject particleEffect;
    public string colTargetName;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = particleEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            particle.Play();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        // Dialogue
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            particle.Stop();
        }
    }
}
