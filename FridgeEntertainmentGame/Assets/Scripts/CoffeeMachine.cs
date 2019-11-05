using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (collider.gameObject.name == colTargetName)
        {
            if (active == true && Input.GetKeyDown(KeyCode.E) == true)
            {
                playerScript.coffee = true;
            }
        }
    }
}
