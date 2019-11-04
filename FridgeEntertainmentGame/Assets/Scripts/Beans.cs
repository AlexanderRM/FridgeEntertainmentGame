using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Beans : MonoBehaviour
{
    public GameObject particleEffect;
    public bool active = false;
    public bool solved = false;
    public string colTargetName;
    public GameObject person;
    public int activeNode = 0;
    public int inactiveNode = 0;

    private Person personScript;
    private VIDE_Assign vide;
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = particleEffect.GetComponent<ParticleSystem>();
        particle.Stop();
        personScript = person.GetComponent<Person>();
        vide = GetComponent<VIDE_Assign>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!solved)
        {
            if (!active)
            {
                vide.overrideStartNode = inactiveNode;
            }
            else
            {
                vide.overrideStartNode = activeNode;
            }
        }
        else
        {
            vide.overrideStartNode = inactiveNode;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            particle.Play();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == colTargetName)
        {
            if (active == true && Input.GetKeyDown(KeyCode.E) == true)
            {
                personScript.solved = true;

                if (VD.nodeData != null)
                {
                    string[] comments = VD.nodeData.comments;
                    // set bool for first chat
                    if (VD.nodeData.isEnd || VD.nodeData.commentIndex == comments.Length - 1)
                    {
                        solved = true;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        particle.Stop();
    }
}
