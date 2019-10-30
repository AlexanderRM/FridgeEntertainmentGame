using UnityEngine;

public class Movement : MonoBehaviour {
	public float speed = 1f;


	[HideInInspector]
	public bool proximitySensors = false;
	[HideInInspector]
	public bool contactSensors = false;
	[HideInInspector]
	public bool rightSensorTriggered = false;
	[HideInInspector]
	public bool leftSensorTriggered = false;

    void Update () {
		transform.Translate (Vector3.forward * speed);
    }
}
