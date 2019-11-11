using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Walking : MonoBehaviour
{
    private Animator anim;
    private PointWalk walking;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if user is not min conversation
        if (!VD.isActive)
        {
            if (Vector3.Distance(walking.transform.position, walking.targetPos) > 1)
            {
                anim.SetBool("walking", true);
                anim.SetBool("walkToStop", false);
            }
            else if (Vector3.Distance(walking.transform.position, walking.targetPos) < 1)
            {
                anim.SetBool("walkToStop", true);
                anim.SetBool("walking", false);
            }
            else
            {
                anim.SetBool("walkToStop", true);
                anim.SetBool("walking", false);
                anim.SetBool("idle", true);
                //anim.StopPlayback("Walk Cycle");
            }
        }
        else
        {
            anim.SetBool("walking", false);
            anim.SetBool("walkToStop", false);
            anim.SetBool("idle", true);
        }
    }
}
