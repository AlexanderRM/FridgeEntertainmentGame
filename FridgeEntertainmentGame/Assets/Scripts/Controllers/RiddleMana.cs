using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class RiddleMana : MonoBehaviour
{
    List<GameObject> people = new List<GameObject>();
    List<GameObject> items = new List<GameObject>();
    VD.NodeData node;
    CoffeeMachine machineScript;

    public GameObject coffeeMachine;
    public string beansCollected = "Go make coffee!";
    public string coffeeMade = "Deliver coffee.";
    public Text objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        // Loop through and collect our people and items
        for (int i = 1; i != 6; i++)
        {
            people.Add(GameObject.Find("Person" + i));
            items.Add(GameObject.Find("Bean" + i));
        }

        machineScript = coffeeMachine.GetComponent<CoffeeMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through and check objects if active and toggle
        for(int i = 0; i < people.Count; i++)
        {
            // Check if current object is active
            if (people[i].GetComponent<Person>().active == false)
            {
                people[i].GetComponent<Person>().active = true;
                people[i].GetComponent<Person>().bean = items[i].GetComponent<Beans>();
                return;
            }
            // Check if this object is active and solved
            else if (people[i].GetComponent<Person>().active == true && people[i].GetComponent<Person>().solved == false)
            {
                // Check if the person has given the riddle
                if(people[i].GetComponent<Person>().riddleGiven == true)
                {
                    // access node
                    node = VD.GetNodeData(people[i].GetComponent<VIDE_Assign>().GetAssigned(), 4, true);
                    // Set canvas
                    objectiveText.text = node.comments[0];
                }
                return;
            }
            // Persons solved show next Objective
            else if(people[i].GetComponent<Person>().active == true && people[i].GetComponent<Person>().solved == true)
            {
                // access node
                node = VD.GetNodeData(people[i].GetComponent<VIDE_Assign>().GetAssigned(), 5, true);
                // Set canvas
                objectiveText.text = node.comments[0];
            }
        }

        // check if the last item is solved
        if(items[items.Count - 1].GetComponent<Beans>().solved == true)
        {
            objectiveText.text = beansCollected;
        }

        if(GameObject.FindWithTag("Player").GetComponent<PointWalk>().coffee == true)
        {
            objectiveText.text = coffeeMade;
        }
    }
}
