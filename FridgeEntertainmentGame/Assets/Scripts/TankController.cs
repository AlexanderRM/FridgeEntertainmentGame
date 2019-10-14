using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 90.0f;

    private void Update()
    {
        float rotateTank = Input.GetAxis("Horizontal");
        float moveTank = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = transform.forward * speed * moveTank;

        transform.Rotate(Vector3.up * rotationSpeed * rotateTank * Time.deltaTime);
    }
}
