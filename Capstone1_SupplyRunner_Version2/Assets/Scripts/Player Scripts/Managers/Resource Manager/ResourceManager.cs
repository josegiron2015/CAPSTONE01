using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

	public int food;
	public int water;
	public int wood;
	public int happiness = 100;
	public int sadnessMeter = 25;
	public Text Tfood;
	public Text Twater;
	public Text Twood;
	public Text Thappiness;
	public DayCycle timeD;
	public PlayerResources player;
	public Text Playerfood;
	public Text Playerwater;
	public int quota = 7;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DisplayResources();
        if (timeD.dayReset) {
			Debug.Log ("nagResetTalaga");
			EvaluateDay ();
			timeD.dayReset = false;
		}
	}
	void EvaluateDay()
	{
		if (water <= quota && food <= quota) {
			happiness -= sadnessMeter;

		} else {
			sadnessMeter--;
			happiness += 2;
			food -= quota;
			water -= quota;
		}
	}

	void DisplayResources()
	{
		Tfood.text = "Food: "+food.ToString ();
		Twater.text = "Water: "+water.ToString ();
		Playerfood.text = "Food: "+player.food.ToString ();
		Playerwater.text = "Water: "+player.water.ToString ();
		Twood.text = "Wood: "+wood.ToString ();
		Thappiness.text = "Happy: "+happiness.ToString ();
	}


}
