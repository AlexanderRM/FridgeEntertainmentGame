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

    public bool coffee = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<TankController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(coffee == true)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.name == colTargetName)
        {
            if(playerScript.coffee == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                coffee = true;
            }
            else if (active == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                // Send Riddle
            }
        }
    }
}
