using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerAction
{
	Enter,
	Stay,
	Exit
}

public class OnTriggerDetector : MonoBehaviour
{
	public List<string> Tags;

	public delegate void OnObjectTriggerEnter(GameObject objectEntered);
	public delegate void OnObjectTriggerStay(GameObject objectEntered);
	public delegate void OnObjectTriggerExit(GameObject objectEntered);
	public OnObjectTriggerEnter onObjectTriggerEnter;
	public OnObjectTriggerStay onObjectTriggerStay;
	public OnObjectTriggerExit onObjectTriggerExit;

	private void OnTriggerEnter(Collider other)
	{
		foreach (string tag in Tags)
		{
			if (other.CompareTag(tag))
			{
				if (onObjectTriggerEnter != null)
					onObjectTriggerEnter.Invoke(other.gameObject);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		foreach (string tag in Tags)
		{
			if (other.CompareTag(tag))
			{
				if (onObjectTriggerStay != null)
					onObjectTriggerStay.Invoke(other.gameObject);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		foreach (string tag in Tags)
		{
			if (other.CompareTag(tag))
			{
				if (onObjectTriggerExit != null)
					onObjectTriggerExit.Invoke(other.gameObject);
			}
		}
	}


}
