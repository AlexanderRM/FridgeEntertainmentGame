using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Walking : MonoBehaviour
{
    public float buttonTimeout;

    private Animator anim;
    private float timeStamp;
    private bool pressed = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        buttonTimeout = GetComponent<TankController>().buttonTimeout;
    }
    
    void Update()
    {
        // Check if user is not min conversation
        if (VD.isActive == false)
        {
            // Collect our user inputs
            if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Vertical") == 1)
            {
                pressed = true;
                timeStamp = Time.time + buttonTimeout;
            }

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                pressed = false;
            }

            // Check if the timeout is less then the time stamp
            if (pressed == true && Time.time <= timeStamp)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    anim.SetBool("walking", true);
                    anim.SetBool("walkToStop", false);
                }
                else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    anim.SetBool("walkToStop", true);
                    anim.SetBool("walking", false);
                }
                else
                {
                    anim.SetBool("walking", false);
                    anim.SetBool("idle", true);
                    //anim.StopPlayback("Walk Cycle");
                }
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
