using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeeTransition : MonoBehaviour
{
    private Animator anim;
    public Person coffeed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (coffeed.coffee == true)
        {
            anim.Play("Coffeed");
        }
    }
}
