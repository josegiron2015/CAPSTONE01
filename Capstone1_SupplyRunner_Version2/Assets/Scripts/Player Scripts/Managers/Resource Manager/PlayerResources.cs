using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour {

	public int food;
	public int water;
	public int wood;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if (food + water < +10) {
			if (other.tag == "Food") {
				food++;
				Destroy (other.gameObject);
			}
			if (other.tag == "Water") {
				water++;
				Destroy (other.gameObject);
			}
		}
	}
}
