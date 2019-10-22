using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public GameObject player;
    public string colTargetName;
    public bool active = false;
    public bool solved = false;
    string riddle = "test test";
    private TankController playerScript;
    private ParticleSystem particle;

    public bool coffee = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<TankController>();
        particle = GetComponent<ParticleSystem>();

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

    void OnTriggerEnter(Collider other)
    {
        if (active == true)
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
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        particle.Stop();
    }
}
