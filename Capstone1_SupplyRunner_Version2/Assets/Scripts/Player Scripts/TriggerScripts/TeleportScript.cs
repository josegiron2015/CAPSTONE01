using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform[] TeleportTo;
    // Use this for initialization

    public void TeleportToTutorial2()
    {
        this.gameObject.transform.position = TeleportTo[0].transform.position;
    }

    public void TeleportToTutorial3()
    {
        this.gameObject.transform.position = TeleportTo[1].transform.position;
    }
}
