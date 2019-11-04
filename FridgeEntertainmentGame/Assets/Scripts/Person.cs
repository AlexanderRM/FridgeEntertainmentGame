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
    public GameObject accessoryPrev;
    public GameObject accessoryNew;

    // Dialogue
    public int chatStart;
    public int activeUnsolved;
    public int notActive;
    public int riddleSolved;
    public int coffeeGive;
    public int coffeeGiven;

    private TankController playerScript;
    private ParticleSystem particle;
    private VIDE_Assign vide;
    private bool chat = false;


    void Start()
    {
        // Set values
        playerScript = player.GetComponent<TankController>();

        particle = particleEffect.GetComponentInChildren<ParticleSystem>();
        particle.Stop();

        vide = GetComponent<VIDE_Assign>();
        vide.overrideStartNode = chatStart;
        accessoryPrev.SetActive(true);
        accessoryNew.SetActive(false);
    }


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
            if (coffee == true)
            {
                vide.overrideStartNode = coffeeGiven;
                accessoryPrev.SetActive(false);
                accessoryNew.SetActive(true);
                return;
            }

            if (bean.solved == true)
            {
                vide.overrideStartNode = riddleSolved;
            }

            // check if player has coffee
            if (playerScript.coffee == true && Input.GetKeyDown(KeyCode.E) == true)
            {
                coffee = true;
                vide.overrideStartNode = coffeeGive;
            }
            // if active give riddle
            else if (active == true && Input.GetKeyDown(KeyCode.E) == true)
            {
                vide.overrideStartNode = chatStart;


                // If talked to before give other dialogue
                if (chat == true)
                {
                    vide.overrideStartNode = activeUnsolved;
                }

                if (VD.nodeData != null)
                {
                    string[] comments = VD.nodeData.comments;
                    // set bool for first chat
                    if (VD.nodeData.isEnd || VD.nodeData.commentIndex == comments.Length - 1)
                    {
                        chat = true;
                    }
                }

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
