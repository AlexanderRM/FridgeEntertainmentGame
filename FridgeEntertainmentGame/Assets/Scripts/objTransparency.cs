using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objTransparency : MonoBehaviour
{
    public Camera camera;
    RaycastHit hit;
    Ray ray;
    List<Transform> hits = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // set our raycas values
        Ray ray = camera.ScreenPointToRay(Vector3.forward);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        // If a raycast is hit
        if (Physics.Raycast(ray, out hit))
        {
            // add the object hit to a value
            Transform objHit = hit.transform;
            Debug.Log("Name " + objHit.name);
            // check if the object is in our list, if not, add
            if (hits.Find(x => x == objHit) == false)
            {
                hits.Add(objHit);
            }

            // If the object hit is not the player
            if (objHit.tag != "Player")
            {
                objHit.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        // set our raycas values
        Ray ray = camera.ScreenPointToRay(Vector3.forward);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

        // If a raycast is hit
        if (Physics.Raycast(ray, out hit))
        {
            // add the object hit to a value
            Transform objHit = hit.transform;

            //Loop through and retoggle overything that doesnt equal the object hit
            for (int i = 0; i < hits.Count; i++)
            {
                if (hits[i] != objHit)
                {
                    hits[i].GetComponent<MeshRenderer>().enabled = true;
                    hits.Remove(objHit);
                }
            }
        }
    }
}
