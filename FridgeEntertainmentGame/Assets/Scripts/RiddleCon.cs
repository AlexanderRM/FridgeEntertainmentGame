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
        for (int i = 0; i < 5; i++)
        {
            people.Add(GameObject.Find("person" + i));
            items.Add(GameObject.Find("item" + i));
        }

        machineScript = coffeeMachine.GetComponent<CoffeeMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject person in people)
        {
            if (person.GetComponent<Person>().active == false)
            {
                person.GetComponent<Person>().active = true;
                return;
            }
            else if(person.GetComponent<Person>().active == true && person.GetComponent<Person>().solved == false)
            {
                return;
            }
        }

        for(int i = 0; i < people.Capacity; i++)
        {
            if (people[i].GetComponent<Person>().active == false)
            {
                people[i].GetComponent<Person>().active = true;
                items[i].GetComponent<Item>().active = true;
                return;
            }
            else if (people[i].GetComponent<Person>().active == true && people[i].GetComponent<Person>().solved == false)
            {
                return;
            }
        }

        if(items[items.Capacity].GetComponent<Item>().solved == true)
        {
            machineScript.active = true;
        }
    }
}
