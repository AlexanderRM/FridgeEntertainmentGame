using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class CoffeeMachine : MonoBehaviour
{
    public bool active = false;
    public string colTargetName;
    public GameObject player;
    private TankController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<TankController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        // Check whats colliding
        if (collider.gameObject.name == colTargetName)
        {
            if (active == true && Input.GetMouseButtonDown(0) == true)
            {
                playerScript.coffee = true;
            }
        }
    }
}
