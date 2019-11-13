using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Walking : MonoBehaviour
{
    public float distanceToObj = 1f;
    public float distanceFromObj = 1f;
    public PointWalk walking;
    public Menu menu;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if user is not in conversation
        if (!VD.isActive)
        {
            if (Vector3.Distance(walking.transform.position, walking.targetPos) > distanceToObj)
            {
                anim.SetBool("walking", true);
                anim.SetBool("walkToStop", false);
            }
            else if (Vector3.Distance(walking.transform.position, walking.targetPos) < distanceFromObj)
            {
                anim.SetBool("walkToStop", true);
                anim.SetBool("walking", false);
            }
            else if (menu.active == true)
            {
                anim.SetBool("walking", false);
                anim.SetBool("walkToStop", true);
                anim.SetBool("idle", true);
            }
            else
            {
                anim.SetBool("walkToStop", true);
                anim.SetBool("walking", false);
                anim.SetBool("idle", true);
            }
        }
        else if (VD.isActive)
        {
            anim.SetBool("walking", false);
            anim.SetBool("walkToStop", true);
            anim.SetBool("idle", true);
        }
    }
}
