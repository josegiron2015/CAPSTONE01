using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartBlockTrigger : MonoBehaviour {

    public Flowchart flowchart;
    public string blockName;

    void Start ()
    {
        flowchart = GameObject.FindGameObjectWithTag("FlowChart").GetComponent<Flowchart>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
            flowchart.ExecuteBlock(blockName);
           
        }
    }

    public void Activate()
    {
        flowchart.ExecuteBlock(blockName);
    }
}
