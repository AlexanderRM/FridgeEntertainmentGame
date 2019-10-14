using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public GameObject player;
    public string colTargetName;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == colTargetName)
        {
            if(playerScript.coffee == true && coffee == false)
            {
                playerScript.coffee = false;
                coffee = true;
            }
        }
    }
}
