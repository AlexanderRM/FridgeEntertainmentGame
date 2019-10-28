using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleCon : MonoBehaviour
{
    List<GameObject> people = new List<GameObject>();
    List<GameObject> items = new List<GameObject>();

    public GameObject coffeeMachine;
    CoffeeMachine machineScript;

    // Start is called before the first frame update
    void Start()
    {
        // Loop through and collect our people and items
        for (int i = 1; i != 5; i++)
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
        for(int i = 0; i < people.Capacity; i++)
        {
            // Check if current object is active
            if (people[i].GetComponent<Person>().active == false)
            {
                people[i].GetComponent<Person>().active = true;
                items[i].GetComponent<Beans>().active = true;
                return;
            }
            // Check if this object is active and solved
            else if (people[i].GetComponent<Person>().active == true && people[i].GetComponent<Person>().solved == false)
            {
                return;
            }
        }

        // check if the last item is solved
        if(items[items.Capacity].GetComponent<Beans>().solved == true)
        {
            machineScript.active = true;
        }
    }
}
