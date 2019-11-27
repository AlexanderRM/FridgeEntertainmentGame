using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public bool active = false;
    public string colTargetName;
    public GameObject player;
    public AudioSource interact;
    public AudioSource getCoffee;

    private PointWalk playerScript;
    private VIDE_Assign vide;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PointWalk>();
        vide = GetComponent<VIDE_Assign>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) vide.overrideStartNode = 1;
    }

    void OnTriggerStay(Collider collider)
    {
        // Check whats colliding
        if (collider.gameObject.name == colTargetName)
        {
            if (active == true && Input.GetMouseButtonDown(0) == true)
            {
                if (getCoffee) getCoffee.Play();
                playerScript.coffee = true;
            }else
            {
                if (interact) interact.Play();
            }
        }
    }
}
