using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour 
{
	public QuestDialogue questdialogue;

	public void TriggerQuestDialogue()
	{
		FindObjectOfType<QuestManager> ().StartDialogue (questdialogue);
	}
}
