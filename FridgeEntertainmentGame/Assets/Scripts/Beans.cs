using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Beans : MonoBehaviour
{
    public bool active = false;
    public bool solved = false;
    public string colTargetName;
    public GameObject person;
    public int activeNode = 0;
    public int inactiveNode = 0;

    private Person personScript;
    private VIDE_Assign vide;

    // Start is called before the first frame update
    void Start()
    {
        personScript = person.GetComponent<Person>();
        vide = GetComponent<VIDE_Assign>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active == false)
        {
            vide.overrideStartNode = inactiveNode;
        }
        else
        {
            vide.overrideStartNode = activeNode;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            if (active == true && Input.GetKeyDown(KeyCode.E) == true)
            {
                personScript.solved = true;
                solved = true;
            }
        }
    }
}
