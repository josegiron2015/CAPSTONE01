using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDialogue
{
	public string name;
	public string QuestName;
	[TextArea(3,5)]
	public string[] QuestSentences;

}
