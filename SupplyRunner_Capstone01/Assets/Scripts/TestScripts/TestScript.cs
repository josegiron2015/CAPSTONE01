using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public OnTriggerDetector Detector;

	private void Start()
	{
		if (Detector != null)
		{
			Detector.onObjectTriggerEnter += OnEnter;
			Detector.onObjectTriggerExit += OnExit;
			Detector.onObjectTriggerStay += OnStay;
		}
		else
			Debug.Log("Forgot to put reference to detector");
	}

	private void OnDisable()
	{
		Detector.onObjectTriggerEnter -= OnEnter;
		Detector.onObjectTriggerExit -= OnExit;
		Detector.onObjectTriggerStay -= OnStay;
	}



	void OnEnter(GameObject obj)
	{
		Debug.Log(obj.name + " has entered");
	}

	void OnExit(GameObject obj)
	{

		Debug.Log(obj.name + " has exited");
	}

	void OnStay(GameObject obj)
	{

		Debug.Log(obj.name + " is staying");
	}

}
