using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using UnityEngine.SceneManagement;

public class RiddleMana : MonoBehaviour
{
    List<GameObject> people = new List<GameObject>();
    List<GameObject> items = new List<GameObject>();
    VD.NodeData node;
    CoffeeMachine machineScript;
    int peopleCoffeed = 0;

    [HideInInspector]
    public int beansObtained = 0;
    public Menu menu;
    public CoffeeMachine coffeeMachine;
    public string beansCollected = "Go make coffee!";
    public string coffeeMade = "Deliver coffee.";
    public string gameFinishSceneName;
    public Text objectiveText;
    public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        // Loop through and collect our people
        for (int i = 1; i != 7; i++)
        {
            people.Add(GameObject.Find("Person" + i));
        }
		
		        // Loop through and collect our items
        for (int i = 1; i != 6; i++)
        {
            items.Add(GameObject.Find("Bean" + i));
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject person in people)
        {
            // If people are coffeed add 1
            if (person.GetComponent<Person>().coffee == true)
            {
                peopleCoffeed++;
            }
            else
            {
                Debug.Log("Person <" + person.gameObject.name + "> doesnt have coffee");
            }
        }

        if (peopleCoffeed == 5)
        {
            anim.SetBool("gameEnd", true);
            //SceneManager.LoadScene(gameFinishSceneName);
        }

        // Reset our beans collected
        beansObtained = 0;
        peopleCoffeed = 0;

        // Check for menu input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.Toggle();
        }

        // Loop through and check objects if active and toggle
        for (int i = 0; i < people.Count; i++)
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
                if (people[i].GetComponent<Person>().riddleGiven == true)
                {
                    // access node
                    node = VD.GetNodeData(people[i].GetComponent<VIDE_Assign>().GetAssigned(), 4, true);
                    // Set canvas
                    objectiveText.text = node.comments[0];
                }
                return;
            }
            // Persons solved show next Objective
            else if (people[i].GetComponent<Person>().active == true && people[i].GetComponent<Person>().solved == true)
            {
                // access node
                node = VD.GetNodeData(people[i].GetComponent<VIDE_Assign>().GetAssigned(), 5, true);
                // Set canvas
                objectiveText.text = node.comments[0];
            }
        }

        // Check how many beans are collected using a for each loop
        foreach (GameObject bean in items)
        {
            // If beans are solved add 1;
            if (bean.GetComponent<Beans>().solved == true)
            {
                beansObtained -= -1;
            }
        }

        // check if the last item is solved
        if (items[items.Count - 1].GetComponent<Beans>().solved == true)
        {
            objectiveText.text = beansCollected;
            coffeeMachine.active = true;
        }

        if (GameObject.FindWithTag("Player").GetComponent<PointWalk>().coffee == true)
        {
            objectiveText.text = coffeeMade;
        }

        // Check if everyone has Coffee
        //foreach (GameObject person in people)
        //{
        //    // If people are coffeed add 1
        //    if (person.GetComponent<Person>().coffee == true)
        //    {
        //        peopleCoffeed++;
        //    }else
        //    {
        //        Debug.Log("Person <" + person.gameObject.name + "> doesnt have coffee");
        //    }
        //}

        //if (peopleCoffeed == 5)
        //{
        //    anim.SetBool("gameEnd", true);
        //    //SceneManager.LoadScene(gameFinishSceneName);
        //}
    }
}
