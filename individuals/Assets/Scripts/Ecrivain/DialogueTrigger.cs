using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject startTrigger;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        startTrigger.SetActive(false);
    }

    // Method to handle player responses
    public void OnResponseSelected(Dialogue nextDialogue)
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence(nextDialogue);
    }
}

