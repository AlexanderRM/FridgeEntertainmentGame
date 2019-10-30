// Víctor Hasim Elexpe Ahamri
// 2017

using UnityEngine;
public class proximityFrontSensor : MonoBehaviour {
    GameObject Robot;
	GameObject frontSensor;
    float originalSpeed;
    void Start()
    {
        Robot = GameObject.Find("Robot");
		frontSensor = GameObject.Find("proximitySensorFront");
		frontSensor.GetComponent<Renderer>().material.color = Color.red;
		originalSpeed = Robot.GetComponent<Movement>().speed;
    }
    private void OnTriggerEnter(Collider other)
    {
		frontSensor.GetComponent<Renderer>().material.color = Color.blue;
		Robot.GetComponent<Movement>().speed = 0.5f;
		Robot.GetComponent<Movement>().speed = 0.4f;
		Robot.GetComponent<Movement>().speed = 0.3f;
		Robot.GetComponent<Movement>().speed = 0.2f;
		Robot.GetComponent<Movement>().speed = 0.1f;
	}
    void OnTriggerStay(Collider other)
    {
		frontSensor.GetComponent<Renderer>().material.color = Color.blue;
		if (Robot.GetComponent<Movement>().contactSensors == false)
		{
			Robot.GetComponent<Movement>().speed = 0.1f;
		}
    }
    void OnTriggerExit(Collider other)
    {
		frontSensor.GetComponent<Renderer>().material.color = Color.red;
		Robot.GetComponent<Movement>().speed = originalSpeed;
	}
}
