using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 18f;

    private Rigidbody rigi;

    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis);

        rigi.AddForce(movement * speed);
    }
}
