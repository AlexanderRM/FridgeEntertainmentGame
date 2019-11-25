using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShow : MonoBehaviour
{
    public GameObject clickObj;
    public float distanceToObj = 1f;
    public float distanceFromObj = 1f;

    private PointWalk walking;

    // Start is called before the first frame update
    void Start()
    {
        walking = GetComponent<PointWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(clickObj.transform.position, transform.position) < distanceFromObj) {
            clickObj.SetActive(false);
        }

        if (Vector3.Distance(clickObj.transform.position, transform.position) > distanceToObj) {
            clickObj.SetActive(true);
        }

        clickObj.GetComponent<Transform>().SetPositionAndRotation(walking.GetTargetPos(), new Quaternion());
    }
}
