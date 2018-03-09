using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	public PlayerScript player;
	public GameObject playerEyes;
	bool updatecamera;
    // Use this for initialization
    

    void Start ()

    {
		playerEyes = GameObject.FindGameObjectWithTag ("PlayerEyes");
		updatecamera = true;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
	}

	// Update is called once per frame
	void Update ()
    {

        playerEyes = GameObject.FindGameObjectWithTag("PlayerEyes");
        updatecamera = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        if (updatecamera)
		transform.position = new Vector3(player.transform.position.x, playerEyes.transform.position.y,player.transform.position.z);
       
    }

	public void CameraCanMove()
	{
		updatecamera = !updatecamera;
	}

}
