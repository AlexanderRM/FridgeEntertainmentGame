using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public string colObjName;
    private bool coffee = false;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(coffee == true)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == colObjName && startTime == 0)
        {
            coffee = true;

            startTime = Time.time;
        }
    }
}
