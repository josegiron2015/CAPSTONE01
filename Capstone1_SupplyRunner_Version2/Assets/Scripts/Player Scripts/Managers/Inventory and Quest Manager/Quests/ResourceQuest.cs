using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceQuest : Quest {

	public Item ReFuckingSources;

	[TextArea()]
	public string QuestSummary;

    public int MaxResources;
    public int MinResources;

	public Inventory inventory;

	// Use this for initialization
	void Start () 
	{
		
	}


	void OnTriggerStay(Collider other)
	{

        if (other.gameObject.CompareTag("Player") && Inventory.Instance.GetNumberOfItem(ReFuckingSources) >= MaxResources && Input.GetButtonDown("Fire2"))
        {
            Debug.Log("PUTANG INA NANDITO SIYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            HasQuestFinished = true;

            for (int i = 0; i < MaxResources; i++)
            {
                Inventory.Instance.RemoveItem(ReFuckingSources);
                Debug.Log("Removed Quest Items" + MaxResources);
            }
        }
        else
            return;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void OnStart()
	{
		if(HasQuestStarted)
		{
			// Start function for quest
			Debug.Log("RESOURCES Quest Started POTA AHAHAHAHAHAHAHAAHAH");

		}
		base.OnStart();

	}

	public override void OnUpdate()
	{



	}



	public override void OnFinished()
	{
		if (HasQuestFinished)
		{
			Debug.Log("Finished Resource Quest");
		}
		base.OnFinished();

	}

}
