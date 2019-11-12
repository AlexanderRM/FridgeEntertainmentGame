using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    public Material notCoffee;
    public Material hasCoffee;

    private Skybox skyBox;

    void Start()
    {
        skyBox = GetComponent<Skybox>();
        skyBox.material = notCoffee;
    }

    private void Update()
    {
        if(target.GetComponent<PointWalk>().coffee == true) skyBox.material = hasCoffee;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target.GetComponent<Renderer>().bounds.center);
    }
}
