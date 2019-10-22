using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("NotWalking", true);
        }
        else
        {
            anim.SetBool("NotWalking", false);
        }
    }
}
