using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class QuestManager : MonoBehaviour {

    #region Singleton Code

    
	public Text nameText;
	public Text dialogueText;
	public Text QuestText;
	public Animator animator;
	public Button QContinue;
	public EventSystem eventstste;
	public PlayerScript player;
	private Queue<string> sentences;

	private static QuestManager instance;

    public static QuestManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = GameObject.FindObjectOfType<QuestManager>();
                if(!instance)
                {
                    GameObject newInstance = new GameObject("Quest Manager");
                    instance = newInstance.AddComponent<QuestManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    public List<Quest> ActiveQuests;

    public delegate void OnQuestFinished();
    public delegate void OnQuestStart();
    public delegate void OnQuestUpdate();

    public OnQuestFinished onQuestFinished;
    public OnQuestStart onQuestStart;
    public OnQuestUpdate onQuestUpdate;

	public void StartDialogue(QuestDialogue dialogue)
	{
		
		nameText.text = dialogue.name;
		QuestText.text = dialogue.QuestName;

		animator.SetBool ("IsOpen", true);
		sentences.Clear ();

		foreach (string sentence in dialogue.QuestSentences) 
		{
			sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue ();
			QContinue.OnDeselect (null);
			eventstste.SetSelectedGameObject(null);
			return;
		}
		eventstste.sendNavigationEvents = true;
		QContinue.Select();
		player.CanMove = true;

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		player.CanMove = false;
		animator.SetBool("IsOpen", false);
	}

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ActiveQuests = new List<Quest>();
    }

    // Use this for initialization
    void Start () 
	{
		sentences = new Queue<string> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ActiveQuests.Count == 0) 
		{
			QuestText.text = " No Quest Available ";
		}

        if (onQuestStart != null)
        {
            onQuestStart.Invoke();
        }
        if (onQuestFinished != null)
        {
            onQuestFinished.Invoke();
        }
    }
		




    public void AddQuest(Quest newQuest)
    {
        if (!ActiveQuests.Contains(newQuest))
        {
            ActiveQuests.Add(newQuest);
            onQuestStart += newQuest.OnStart;
            onQuestUpdate += newQuest.OnUpdate;
            onQuestFinished += newQuest.OnFinished;
        }
    }

    public void RemoveQuest(Quest quest)
    {
        if(ActiveQuests.Contains(quest))
        {
            ActiveQuests.Remove(quest);
            onQuestStart -= quest.OnStart;
            onQuestFinished -= quest.OnFinished;
        }
    }
}
