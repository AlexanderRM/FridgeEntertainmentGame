using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    new Light light;
    public Person personScript;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        personScript = GetComponent<Person>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(personScript.coffee == true)
        {
            light.enabled = true;
        }
    }
}
