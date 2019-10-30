using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeTransition : MonoBehaviour
{
    private Animator anim;
    public Person script;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (script.coffee == true)
        {
            anim.Play("Idle 2");
        }
    }
}
