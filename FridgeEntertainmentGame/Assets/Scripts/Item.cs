using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool active = false;
    public bool solved = false;
    public string colTargetName;
    public GameObject person;
    private Person personScript;

    // Start is called before the first frame update
    void Start()
    {
        person.GetComponent<Person>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            if (active == true && Input.GetKeyDown(KeyCode.Return) == true)
            {
                personScript.solved = true;
                solved = true;
            }
        }
    }
}
