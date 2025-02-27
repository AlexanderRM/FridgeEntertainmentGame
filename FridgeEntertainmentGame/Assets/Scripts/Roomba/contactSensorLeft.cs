﻿// Víctor Hasim Elexpe Ahamri
// 2017

using UnityEngine;
public class contactSensorLeft : MonoBehaviour {
    GameObject Robot;
	GameObject leftSensor;
	float originalSpeed;

	void Start ()
    {
        Robot = transform.parent.gameObject;
        leftSensor = GetComponent<Transform>().gameObject;
        leftSensor.GetComponent<Renderer>().material.color = Color.red;
		originalSpeed = Robot.GetComponent<Movement>().speed;
		Robot.GetComponent<Movement>().leftSensorTriggered = false;
	}

    void OnTriggerEnter(Collider collision)
    {
		Robot.GetComponent<Movement>().leftSensorTriggered = true;
		Robot.GetComponent<Movement>().contactSensors = true;
        Robot.GetComponent<Movement>().speed = 0f;
	}

	void OnTriggerStay(Collider collision)
	{
		if (Robot.GetComponent<Movement>().rightSensorTriggered == false)
		{
			leftSensor.GetComponent<Renderer>().material.color = Color.blue;
			Robot.GetComponent<Movement>().contactSensors = true;
			Robot.GetComponent<Movement>().speed = 0f;
			Robot.transform.Rotate(new Vector3(0f, 180, 0f) * Time.deltaTime);
		}
	}
    void OnTriggerExit(Collider collision)
    {
		Robot.GetComponent<Movement>().leftSensorTriggered = false;
		leftSensor.GetComponent<Renderer>().material.color = Color.red;
		if (Robot.GetComponent<Movement>().rightSensorTriggered == false)
		{
			Robot.GetComponent<Movement>().contactSensors = false;
			Robot.GetComponent<Movement>().speed = originalSpeed;
		}
	}
}
