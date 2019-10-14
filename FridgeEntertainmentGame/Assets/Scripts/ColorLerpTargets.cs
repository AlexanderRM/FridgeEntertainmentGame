using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerpTargets : MonoBehaviour
{
    public GameObject person;
    Person personScript;

    // Start is called before the first frame update
    void Start()
    {
        personScript = person.GetComponent<Person>();
    }

    // Update is called once per frame
    void Update()
    {
        if(personScript.coffee == true)
        {
            Component[] children = gameObject.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer child in children)
            {
                child.GetComponent<ColorLerp>().StartLerp();
            }
        }
    }
}
