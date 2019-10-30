// Víctor Hasim Elexpe Ahamri
// 2017

using UnityEngine;
public class contactSensorRight : MonoBehaviour {
    GameObject Robot;
	GameObject rightSensor;
    float originalSpeed;
    void Start()
    {
        Robot = GameObject.Find("Robot");
		rightSensor = GameObject.Find("proximitySensorRight");
		rightSensor.GetComponent<Renderer>().material.color = Color.red;
        originalSpeed = Robot.GetComponent<Movement>().speed;
		Robot.GetComponent<Movement>().rightSensorTriggered = false;
	}
	void OnTriggerEnter(Collider other)
	{
		Robot.GetComponent<Movement>().rightSensorTriggered = true;
		Robot.GetComponent<Movement>().contactSensors = true;
        Robot.GetComponent<Movement>().speed = 0f;
	}
	void OnTriggerStay(Collider other)
	{
		rightSensor.GetComponent<Renderer>().material.color = Color.blue;
		Robot.GetComponent<Movement>().rightSensorTriggered = true;
		Robot.GetComponent<Movement>().contactSensors = true;
		Robot.GetComponent<Movement>().speed = 0f;
		Robot.transform.Rotate(new Vector3(0f, -180, 0f) * Time.deltaTime);
	}
	void OnTriggerExit(Collider other)
	{
		Robot.GetComponent<Movement>().rightSensorTriggered = false;
		rightSensor.GetComponent<Renderer>().material.color = Color.red;
		if (Robot.GetComponent<Movement>().leftSensorTriggered == false)
		{
			Robot.GetComponent<Movement>().contactSensors = false;
			Robot.GetComponent<Movement>().speed = originalSpeed;
		}
		
	}
}
