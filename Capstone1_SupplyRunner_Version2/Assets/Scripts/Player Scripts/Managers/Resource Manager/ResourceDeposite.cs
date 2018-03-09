using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposite : MonoBehaviour {
	public ResourceManager resources;
	// Use this for initialization
	void Start () {
		resources = GameObject.FindGameObjectWithTag ("Resource").GetComponent<ResourceManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			resources.food += other.gameObject.GetComponent<PlayerResources> ().food;
			other.gameObject.GetComponent<PlayerResources> ().food = 0;
			resources.water += other.gameObject.GetComponent<PlayerResources> ().water;
			other.gameObject.GetComponent<PlayerResources> ().water = 0;
		}
	}
	public void DepositPlayerResources()
	{
		resources.food += GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources> ().food;
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources> ().food = 0;
		resources.water +=  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources> ().water;
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources> ().water = 0;
	}
}
