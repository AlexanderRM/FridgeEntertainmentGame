using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public GameObject player;
    public string colTargetName;
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == colTargetName)
        {
            if(playerScript.coffee == false)
            {
                playerScript.coffee = true;
            }
        }
    }
}
