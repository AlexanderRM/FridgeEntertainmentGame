using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Walking : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }
    
    void Update()
    {
        if (VD.isActive == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetBool("walking", true);
            }
            else
            {
                anim.SetBool("walking", false);
            }

        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
}
