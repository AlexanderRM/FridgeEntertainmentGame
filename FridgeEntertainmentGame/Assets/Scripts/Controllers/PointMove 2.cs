using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointWalk : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myNavAgent;

    // Start is called before the first frame update
    void Start()
    {
        myNavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, whatCanBeClickedOn))
            {
                myNavAgent.destination = hitInfo.point;
            }
        }
    }
}
