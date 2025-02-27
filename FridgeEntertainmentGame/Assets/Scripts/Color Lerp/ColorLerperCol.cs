﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerperCol : MonoBehaviour
{
    // Declare our values
    public GameObject player;
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public string colObjName;
    private bool coffee = false;
    private TankController playerScript;
    float startTime;
    private new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        // Set our values
        renderer = GetComponent<Renderer>();
        playerScript = player.GetComponent<TankController>();
        renderer.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(coffee == true)
        {
            float t = (Time.time - startTime) * speed;
            renderer.material.color = Color.Lerp(startColor, endColor, t);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == colObjName && startTime == 0)
        {
            if (playerScript.coffee == true) {
                coffee = true;

                startTime = Time.time;
            }
        }
    }
}
