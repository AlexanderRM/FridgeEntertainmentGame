using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjChangeTargets : MonoBehaviour
{
    // Declare values
    public GameObject person;
    Person personScript;

    // Start is called before the first frame update
    void Start()
    {
        // Set values
        personScript = person.GetComponent<Person>();
    }

    // Update is called once per frame
    void Update()
    {
        if (personScript.coffee == true)
        {
            Component[] children = gameObject.GetComponentsInChildren<ObjChangeCon>(true);
            foreach (ObjChangeCon child in children)
            {
                child.Change();
            }
        }
    }
}