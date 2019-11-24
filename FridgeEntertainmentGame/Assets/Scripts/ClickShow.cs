using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShow : MonoBehaviour
{
    public PointWalk walking;
    public float distanceToObj = 1f;
    public float distanceFromObj = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = walking.targetPos;

        if(Vector3.Distance(walking.transform.position, walking.targetPos) < distanceFromObj) GetComponent<GameObject>().SetActive(false);

        if (Vector3.Distance(walking.transform.position, walking.targetPos) > distanceToObj) GetComponent<GameObject>().SetActive(true);
    }
}
