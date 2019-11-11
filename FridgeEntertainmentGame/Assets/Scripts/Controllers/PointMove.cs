using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRot;

    public float rotSpeed = 5;
    public float speed = 10;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }
        if(moving)
        Move();
    }

    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            targetPosition = hit.point;
            // this.transform.LookAt(targetPosition);
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);

            moving = true;
        }
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, rotSpeed * Time.deltaTime);

        if (transform.position == targetPosition) moving = false;
    }
}
