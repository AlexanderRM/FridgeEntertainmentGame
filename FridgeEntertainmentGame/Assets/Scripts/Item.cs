using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Item : MonoBehaviour
{
    public GameObject particleEffect;
    public string colTargetName;

    private ParticleSystem particle;
    private VIDE_Assign vide;

    // Start is called before the first frame update
    void Start()
    {
        particle = particleEffect.GetComponent<ParticleSystem>();
        particle.Stop();
        vide = GetComponent<VIDE_Assign>();
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
            particle.Stop();
    }
}
