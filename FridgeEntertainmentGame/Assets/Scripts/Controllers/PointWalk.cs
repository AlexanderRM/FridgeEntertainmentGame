using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VIDE_Data;

public class PointWalk : MonoBehaviour
{
    public string playerName = "VIDE User";
    public bool coffee = false;
    public GameObject player;
    public LayerMask floorLayer;
    public LayerMask Interactable;
    public VIDE_Assign inTrigger;
    public UIManager diagUI;
    public Animator walkCycle;

    private NavMeshAgent myNavAgent;
    private bool objClicked;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        myNavAgent = GetComponent<NavMeshAgent>();
        objClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!VD.isActive)
        {
            if (Input.GetMouseButtonDown(0) && objClicked == false)
            {
                if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, floorLayer))
                {
                    targetPos = hitInfo.point;
                    myNavAgent.destination = hitInfo.point;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, Interactable))
            {
                // Item has been clicked on
                objClicked = true;

                if (hitInfo.collider.GetComponent<Beans>())
                {
                    hitInfo.collider.GetComponent<Beans>().clicked = true;
                    TryInteract();
                }

                if (hitInfo.collider.GetComponent<Person>())
                {
                    hitInfo.collider.GetComponent<Person>().clicked = true;
                    TryInteract();
                }
                return;
            }
        }
        objClicked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if component is there
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

    void OnTriggerExit()
    {
        inTrigger = null;
        objClicked = false;
    }

    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger && Vector3.Distance(transform.position, targetPos) < 1)
        {
            diagUI.Interact(inTrigger);
            return;
        }

        /* If we are not in a trigger, try with raycasts */

        //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rHit, 2))
        //{
        //    //Lets grab the NPC's VIDE_Assign script, if there's any
        //    VIDE_Assign assigned;
        //    if (rHit.collider.GetComponent<VIDE_Assign>() != null)
        //        assigned = rHit.collider.GetComponent<VIDE_Assign>();
        //    else return;

        //    if (assigned.alias == "diagUI")
        //    {
        //        diagUI.Interact(assigned); //Begins interaction
        //    }

        //}
    }
}
