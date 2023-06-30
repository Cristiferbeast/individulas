using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    //External Variables
    public TextMeshProUGUI dialogueText;
    public Dialogue[] dialogues;
    public DialogueManager dialogueManager;

    //Internal Variables
    private int currentDialogueIndex = 0;
    private int currentLineIndex = 0;
    private bool inDialogue = false;

    private void Start()
    {
        dialogueText.text = dialogues[currentDialogueIndex].lines[currentLineIndex];
    }

    private void Update()
    {
        if (inDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLine();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue(0); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.X))
        {
            // Disable dialogue when the player exits the trigger area
            dialogueManager.CloseDialogue();
        }
    }

    //Internal Functions
    private void LoadNextLine()
    {
        if (currentLineIndex < dialogues[currentDialogueIndex].lines.Length - 1)
        {
            // Advance to the next line
            currentLineIndex++;
            dialogueText.text = dialogues[currentDialogueIndex].lines[currentLineIndex];
        }
        else
        {
            // End of dialogue, close the dialogue UI
            dialogueManager.CloseDialogue();
            inDialogue = false;
        }
    }

    public void StartDialogue(int dialogueIndex)
    {
        if (!inDialogue)
        {
            currentDialogueIndex = dialogueIndex;
            currentLineIndex = 0;
            dialogueText.text = dialogues[currentDialogueIndex].lines[currentLineIndex];
            dialogueManager.OpenDialogue();
            inDialogue = true;
        }
    }
}
