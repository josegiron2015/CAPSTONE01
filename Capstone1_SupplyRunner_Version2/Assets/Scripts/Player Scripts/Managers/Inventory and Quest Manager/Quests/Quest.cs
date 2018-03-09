using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

    public bool HasQuestStarted;
    public bool HasQuestFinished;
	public Queue QuestDialogue;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	public virtual void OnFinished () {
        if (HasQuestFinished)
        {
            QuestManager.Instance.RemoveQuest(this);
            Debug.Log("Is Finished");
        }
    }

    public virtual void OnStart()
    {
        if(!HasQuestStarted)
        {
            HasQuestStarted = true;
        }
    }

    public virtual void OnUpdate()
    {
        
    }
}
