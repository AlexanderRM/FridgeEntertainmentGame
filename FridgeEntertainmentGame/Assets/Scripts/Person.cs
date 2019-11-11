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
    public bool clicked = false;
    public Beans bean;
    public bool coffee = false;
    public GameObject accessoryPrev;
    public GameObject accessoryNew;

    // Dialogue
    public int activeUnsolved;
    public int notActive;
    public int coffeeGive;

    private PointWalk playerScript;
    private ParticleSystem particle;
    private VIDE_Assign vide;
    private UIManager uiRiddle;


    void Start()
    {
        // Set values
        playerScript = player.GetComponent<PointWalk>();

        particle = particleEffect.GetComponentInChildren<ParticleSystem>();
        particle.Stop();

        vide = GetComponent<VIDE_Assign>();
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

    void LateUpdate()
    {
        clicked = false;
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
                accessoryPrev.SetActive(false);
                accessoryNew.SetActive(true);
                return;
            }
            // check if player has coffee
            if (playerScript.coffee == true && Input.GetMouseButtonDown(0) == true && clicked == true)
            {
                coffee = true;
                vide.overrideStartNode = coffeeGive;
            }

            // if active give riddle
            else if (active == true && Input.GetMouseButtonDown(0) == true && clicked == true)
            {
                vide.overrideStartNode = activeUnsolved;
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
