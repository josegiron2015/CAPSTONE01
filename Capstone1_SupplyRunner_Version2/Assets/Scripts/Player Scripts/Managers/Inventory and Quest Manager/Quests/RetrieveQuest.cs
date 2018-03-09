using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.SceneManagement;
public class RetrieveQuest : Quest {

	public Item ItemToRetrieve;

	[TextArea()]
	public string QuestSummary;
    public Inventory inventory;
    public Flowchart flowchart;
    public string blockName;

    // Use this for initialization
    void Start ()
    {
       // inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player") && Input.GetButton("Square"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.P))
    //        {
    //            this.GetComponentInParent<QuestTrigger>().TriggerQuestDialogue();

    //            QuestManager.Instance.AddQuest(this);
    //            Debug.Log("Quest Added");
    //        }
    //    }

    //    if (other.gameObject.CompareTag("Player") && Inventory.Instance.Items.Contains(ItemToRetrieve) && Input.GetButtonDown("Square"))
    //    {
    //        Debug.Log("TAPOS NA");
    //        HasQuestFinished = true;
    //        Inventory.Instance.RemoveItem(ItemToRetrieve);
    //    }
    //}

    public void TakeQuest()
    {
        this.GetComponent<QuestTrigger>().TriggerQuestDialogue();
        QuestManager.Instance.AddQuest(this);
        Debug.Log("Quest Added");
    }

    public void FinishQuest()
    {
        if (Inventory.Instance.Items.Contains(ItemToRetrieve))
        {
            HasQuestFinished = true;
            Inventory.Instance.RemoveItem(ItemToRetrieve);
            flowchart.ExecuteBlock(blockName);
            Debug.Log("Quest Finished");
            Application.Quit();
            Debug.Log("APPLICATION EXITED");
        }
    }





    // Update is called once per frame
    void Update () 
	{
		
//		if(Input.GetKeyDown(KeyCode.P))
//        {
//            QuestManager.Instance.AddQuest(this);
//			Debug.Log("Quest Added");
//        }
	}

    public override void OnStart()
    {
        if(HasQuestStarted)
        {
            // Start function for quest
			Debug.Log("Retrieve Quest Started");

        }
        //base.OnStart();
        
    }

    public override void OnUpdate()
    {
		


    }



    public override void OnFinished()
    {
        if (HasQuestFinished)
        {
			Debug.Log("Finished Quest");
        }
        //base.OnFinished();

    }
}
