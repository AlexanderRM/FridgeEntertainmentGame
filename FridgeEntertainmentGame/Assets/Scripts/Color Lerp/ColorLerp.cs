using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    bool lerp = false;
    public float speed = 5;
    public Color start;
    public Color end;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lerp == true)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(start, end, t);
        }
    }

    public void StartLerp()
    {
        lerp = true;
        startTime = Time.time;
    }
}
