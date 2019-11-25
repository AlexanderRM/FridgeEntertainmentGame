using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light oldLight;
    public Light newLight;
    public PointWalk personScript;

    // Start is called before the first frame update
    void Start()
    {
        oldLight.enabled = true;
        newLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(personScript.coffee == true)
        {
            newLight.enabled = true;
            oldLight.enabled = false;
        }
    }
}
