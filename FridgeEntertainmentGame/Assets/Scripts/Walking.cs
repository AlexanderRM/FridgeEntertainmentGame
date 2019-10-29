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
            if (anim.GetBool("walking") == false)
            {
                anim.Play("Idle");
            }
            else if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("walking", true);
            }
            else if (Input.GetKeyUp(KeyCode.W) && (anim.GetBool("walking") == true))
            {
                anim.SetBool("walkToStop", true);
            }   
        }
        
    }
}
