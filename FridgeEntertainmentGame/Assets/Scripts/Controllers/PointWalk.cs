using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VIDE_Data;

public class PointWalk : MonoBehaviour
{
    // Declare values
    public string playerName = "VIDE User";
    public bool coffee = false;
    public GameObject player;
    public LayerMask floorLayer;
    public LayerMask Interactable;
    public VIDE_Assign inTrigger;
    public UIManager diagUI;
    public Animator walkCycle;
    public Menu menu;
    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
    public bool active = true;

    private NavMeshAgent myNavAgent;
    private bool objClicked;

    // Start is called before the first frame update
    void Start()
    {
        // Assign values
        myNavAgent = GetComponent<NavMeshAgent>();
        objClicked = false;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            // cast a ray to point
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the VD is active and stop the agent if so
            if (VD.isActive || menu.menuActive)
            {
                myNavAgent.isStopped = true;
                targetPos = transform.position;
                myNavAgent.destination = targetPos;
            }

            // If VD is false allow movement
            if (!VD.isActive)
            {
                myNavAgent.isStopped = false;
                if (Input.GetMouseButtonDown(0) && objClicked == false)
                {
                    if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, floorLayer))
                    {
                        targetPos = hitInfo.point;
                        myNavAgent.destination = hitInfo.point;
                    }
                }
            }

            // If Players not moving set that to our target position
            if (myNavAgent.velocity.Equals(new Vector3(0, 0, 0)))
            {
                targetPos = transform.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Cast a ray at mouse position
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            // IF the beans or person was hit, try interaction
            if (Physics.Raycast(myRay, out RaycastHit hitInfo, 100, Interactable))
            {
                // Item has been clicked on
                objClicked = true;
                // Check if colliding with any of these objects
                if (hitInfo.collider.GetComponent<Beans>() || hitInfo.collider.GetComponent<Person>() || hitInfo.collider.GetComponent<Item>())
                {
                    TryInteract();
                    hitInfo.collider.GetComponent<AudioSource>().Play();
                }
                return;
            }
            else if (VD.isActive && myNavAgent.velocity.Equals(new Vector3(0, 0, 0)))
            {
                TryInteract();
                return;
            }
        }

        // Set to false when done
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
        VD.EndDialogue();
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
