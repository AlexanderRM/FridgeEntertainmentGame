using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Person : MonoBehaviour
{
    // Declare values
    public GameObject player;
    public GameObject particleEffect;
    public string colTargetName;
    public bool active = false;
    public bool solved = false;
    public Beans bean;
    public bool coffee = false;

    // Dialogue
    public int chatStart;
    public int spokenAgain;
    public int activeUnsolved;
    public int notActive;
    public int riddleSolved;
    public int coffeeGive;
    public int coffeeGiven;

    private TankController playerScript;
    private ParticleSystem particle;
    private VIDE_Assign vide;
    private bool chat = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set values
        playerScript = player.GetComponent<TankController>();
        particle = particleEffect.GetComponent<ParticleSystem>();
        vide = GetComponent<VIDE_Assign>();

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
        // check if object collision matches
        if (collider.gameObject.name == colTargetName)
        {
            // if coffee has been given
            if(coffee == true)
            {
                vide.overrideStartNode = coffeeGiven;
                return;
            }

            if(bean.solved == true)
            {
                vide.overrideStartNode = riddleSolved;
            }

            // check if player has coffee
            if (playerScript.coffee == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                coffee = true;
                vide.overrideStartNode = coffeeGive;
            }
            // if active give riddle
            else if (active == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                vide.overrideStartNode = chatStart;

                // If talked to before give other dialogue
                if(chat == true)
                {
                    vide.overrideStartNode = spokenAgain;
                }

                // set bool for first chat
                chat = true;
                // Set bean active
                bean.active = true;
            }
            // active is false
            else if (active == false)
            {
                vide.overrideStartNode = notActive;
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
