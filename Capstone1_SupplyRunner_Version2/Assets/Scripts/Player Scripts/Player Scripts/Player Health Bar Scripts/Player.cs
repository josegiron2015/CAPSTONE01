using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    /// <summary>
    /// The player's health stat
    /// </summary>
    [SerializeField]
    private Stat health;

    /// <summary>
    /// The player's energy stat
    /// </summary>
    //[SerializeField]
   // private Stat energy;

    /// <summary>
    /// The player's shield
    /// </summary>
   // [SerializeField]
   // private Stat shield;


    void Awake()
    {
        //Initializes the stats
        // energy.Initialize();
           DontDestroyOnLoad(this.gameObject);
       
        health.Initialize();

       // shield.Initialize();
    }

    public void ResetCameraTransform()
    {
        Camera.main.transform.localPosition = new Vector3 (-0.03400102f, 5.514999f, 0.5389942f);
    }
	// Update is called once per frame
	void Update ()
    {
        //Demonstrates the usage of each bar
        //Takes the stats and reduces or increases it for demonstration
        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentValue -= 10;
        }
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            energy.CurrentValue -= 10;
//        }
//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            energy.CurrentValue += 10;
//        }
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            shield.CurrentValue -= 10;
//        }
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            shield.CurrentValue += 10;
//        }
    }
}
