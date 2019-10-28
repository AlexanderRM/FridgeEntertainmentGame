using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    // Declare values
    public GameObject player;
    public string colTargetName;
    public bool active = false;
    public bool solved = false;
    public Beans bean;
    private TankController playerScript;
    private ParticleSystem particle;

    public bool coffee = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set values
        playerScript = player.GetComponent<TankController>();
        particle = GetComponentInChildren<ParticleSystem>();

        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (solved == true)
        {
            particle.Stop();
        }
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
        if (collider.gameObject.name == colTargetName)
        {
            if (playerScript.coffee == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                coffee = true;
            }
            else if (active == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                // Send Riddle

                // Set bean active
                bean.active = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            particle.Stop();
        }
    }
}
