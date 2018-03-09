using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour {
	public PlayerScript player;
	public Text nameText;
	public Text dialogueText;
	public Animator animator;
    public Button DContinue;
    public EventSystem eventstste;
	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		
        Cursor.lockState = CursorLockMode.None;
		animator.SetBool("IsOpen", true);
        eventstste.sendNavigationEvents = false;
        nameText.text = dialogue.name;
		sentences.Clear();

        foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();

		//DialoguePauseMenu.Paused ();
	}

	public void DisplayNextSentence ()
    {
		if (sentences.Count == 0)
		{
			EndDialogue();
			DContinue.OnDeselect (null);
			eventstste.SetSelectedGameObject(null);
			return;
		}
		eventstste.sendNavigationEvents = true;
		DContinue.Select();
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

    public void Update()
    {
        DontDestroyOnLoad(gameObject);
        // Continue.Select();

    }

    public void EndDialogue()
	{
        animator.SetBool("IsOpen", false);
		//DialoguePauseMenu.Resume ();
		player.CanMove = false;
		Cursor.lockState = CursorLockMode.Locked;
			
	}

}
