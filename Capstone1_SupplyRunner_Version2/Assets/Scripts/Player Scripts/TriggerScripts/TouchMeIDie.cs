using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

using UnityEngine.SceneManagement;

public class TouchMeIDie : MonoBehaviour {

    public Flowchart flowchart;
	public Dialogue dialogue;
    public string blockName;
    public GameObject textMeshPro;

    public AudioClip InteractSound;
    AudioSource audioSource;

    void Start()
    {
		//textMeshPro = GameObject.FindGameObjectWithTag ("TextMeshPro");
		//flowchart = GameObject.FindGameObjectWithTag ("FlowChart").GetComponent<Flowchart> ();
        audioSource = GetComponent<AudioSource>();
		//textMeshPro.SetActive (false);
	
    }
	void Update()
	{
		//flowchart = GameObject.FindGameObjectWithTag ("FlowChart").GetComponent<Flowchart> ();
		//textMeshPro = GameObject.FindGameObjectWithTag ("TextMeshPro");
	}
    void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			
			//Destroy (this.gameObject);
			//this.GetComponent<DialogueTrigger>().TriggerDialogue();
			//FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            
            textMeshPro.SetActive(true);
            //audioSource.PlayOneShot(InteractSound, 5.0f);
			//Debug.Log ("Lol");
		}
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButtonDown("Square"))
        {

            //Destroy (this.gameObject);
            //this.GetComponent<DialogueTrigger>().TriggerDialogue();
            //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            // flowchart.ExecuteBlock(blockName);
            //textMeshPro.SetActive(false);
           
            flowchart.ExecuteBlock(blockName);
            textMeshPro.SetActive(false);
            //SendMessage("OpenInventory");
            //Debug.Log ("Lol");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //Destroy (this.gameObject);
            //this.GetComponent<DialogueTrigger>().TriggerDialogue();
            //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
           // flowchart.ExecuteBlock(blockName);
            textMeshPro.SetActive(false);
            //Debug.Log ("Lol");
        }
    }
}
