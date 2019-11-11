using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Walking : MonoBehaviour
{
    public float distanceToObj = 1f;
    public float distanceFromObj = 1f;
    public PointWalk walking;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if user is not min conversation
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
