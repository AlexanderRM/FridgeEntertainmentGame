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
    List<GameObject> beanImages = new List<GameObject>();

    VD.NodeData node;
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
    public PointWalk player;
    public AudioSource preCoffee;
    public AudioSource coffee;



    // Start is called before the first frame update
    void Start()
    {
        // Loop through and collect our people
        for (int i = 1; i != 7; i++)
        {
            people.Add(GameObject.Find("Person" + i));
        }

        // Loop through and collect our beans and images
        for (int i = 1; i != 6; i++)
        {
            items.Add(GameObject.Find("Bean" + i));
            beanImages.Add(GameObject.Find("beanImage" + i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Reset our beans collected
        beansObtained = 0;
        peopleCoffeed = 0;

        // Check for menu input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.Toggle();
        }

        if(player.coffee == true)
        {
            coffee.Play();
        }
        else
        {
            preCoffee.Play();
        }

        // Loop through and check objects if active and toggle
        for (int i = 0; i < people.Count; i++)
        {
            if (i == 5) return;


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
    }

    private void LateUpdate()
    {
        // Check how many beans are collected using a for each loop
        foreach (GameObject bean in items)
        {
            // If beans are solved add 1;
            if (bean.GetComponent<Beans>().solved == true)
            {
                int currentIndex = items.IndexOf(bean);
                beansObtained -= -1;

                // Set current bean to active
                beanImages[currentIndex].SetActive(true);

                // Set previous Image to off
                if(currentIndex != 0) beanImages[currentIndex - 1].SetActive(true);

            }
            else
            {
                Debug.Log("Bean <" + bean.name + "> is false");
            }
        }

        // check if the last item is solved
        if (beansObtained == 5)
        {
            objectiveText.text = beansCollected;
            coffeeMachine.active = true;
        }

        if (GameObject.FindWithTag("Player").GetComponent<PointWalk>().coffee == true)
        {
            objectiveText.text = coffeeMade;
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().Play();
        }

        ///////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

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
    }
}
