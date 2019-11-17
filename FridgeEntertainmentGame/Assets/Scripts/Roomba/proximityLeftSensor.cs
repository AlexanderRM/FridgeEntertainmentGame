// Víctor Hasim Elexpe Ahamri
// 2017

using UnityEngine;
public class proximityLeftSensor : MonoBehaviour {
    GameObject Robot;
	GameObject leftSensor;
    float originalSpeed;
    void Start()
    {
        Robot = transform.parent.gameObject;
        leftSensor = GetComponent<Transform>().gameObject;
        leftSensor.GetComponent<Renderer>().material.color = Color.red;
		originalSpeed = Robot.GetComponent<Movement>().speed;
    }
    private void OnTriggerEnter(Collider other)
    {
		leftSensor.GetComponent<Renderer>().material.color = Color.blue;
		Robot.GetComponent<Movement>().speed = 0.1f;
	}
    void OnTriggerStay(Collider other)
    {
		if (Robot.GetComponent<Movement>().contactSensors == false)
		{
			leftSensor.GetComponent<Renderer>().material.color = Color.blue;
			Robot.transform.Rotate(new Vector3(0f, 45f, 0f) * Time.deltaTime);
		}
    }
    void OnTriggerExit(Collider other)
    {
		leftSensor.GetComponent<Renderer>().material.color = Color.red;
		if (Robot.GetComponent<Movement>().contactSensors == false)
		{
			Robot.GetComponent<Movement>().speed = originalSpeed;
		}
	}
}
