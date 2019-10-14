using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 90.0f;
    public bool coffee = false;
    public GameObject player;

    private GameObject nose;

    private void Start()
    {
        nose = player.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        float rotateTank = Input.GetAxis("Horizontal");
        float moveTank = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = transform.forward * speed * moveTank;

        transform.Rotate(Vector3.up * rotationSpeed * rotateTank * Time.deltaTime);

        if(coffee == true)
        {
            nose.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            nose.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
