using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VIDE_Data;

public class PointWalk : MonoBehaviour
{
    public LayerMask floorLayer;
    public LayerMask itemLayer;
    public VIDE_Assign inTrigger;
    public UIManager diagUI;

    private NavMeshAgent myNavAgent;
    private bool objClicked;

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

        if (Input.GetMouseButtonDown(0) && objClicked == false)
        {
            if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, floorLayer))
            {
                myNavAgent.destination = hitInfo.point;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, itemLayer))
            {
                // Item has been clocked on
                objClicked = true;
                if (hitInfo.collider.GetComponent<Beans>()) hitInfo.collider.GetComponent<Beans>().clicked = true;
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

        if (inTrigger)
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
