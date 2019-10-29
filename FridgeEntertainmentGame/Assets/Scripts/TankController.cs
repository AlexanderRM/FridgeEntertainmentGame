using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class TankController : MonoBehaviour
{
    public string playerName = "VIDE User";

    public float speed = 3.0f;
    public float rotationSpeed = 90.0f;
    public bool coffee = false;
    public GameObject player;
    public UIManager diagUI;
    public QuestChartDemo questUI;
    public VIDE_Assign inTrigger;
    public Animator walkCycle;
    public List<string> demo_Items = new List<string>();
    public List<string> demo_ItemInventory = new List<string>();



    private void Start()
    {
    }

    private void Update()
    {
        float rotateTank = Input.GetAxis("Horizontal");
        float moveTank = Input.GetAxis("Vertical");

        if (!VD.isActive)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed * moveTank;

            transform.Rotate(Vector3.up * rotationSpeed * rotateTank * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

    void OnTriggerExit()
    {
        inTrigger = null;
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

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            if (assigned.alias == "QuestUI")
            {
                questUI.Interact(); //Begins interaction with Quest Chart
            }
            else
            {
                diagUI.Interact(assigned); //Begins interaction
            }

        }
    }
}
