using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    // External Variables
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Dialogue[] dialogues;
    public DialogueManager dialogueManager;

    // Internal Variables
    private int currentDialogueIndex = 0;
    private int currentLineIndex = 0;
    private bool inDialogue = false;

    private void Start()
    {
        DisplayCurrentLine();
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
        if (other.CompareTag("Player"))
        {
            CloseDialogue();
        }
    }

    // Internal Functions
    private void LoadNextLine()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];
        if (currentLineIndex < currentDialogue.dialogueLines.Length - 1)
        {
            currentLineIndex++;
            DisplayCurrentLine();
        }
        else
        {
            CloseDialogue();
        }
    }

    private void DisplayCurrentLine()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];
        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];

        speakerText.text = currentLine.speaker;
        dialogueText.text = currentLine.line;

        dialogueManager.OpenDialogue();
        inDialogue = true;
    }

    public void StartDialogue(int dialogueIndex)
    {
        if (!inDialogue)
        {
            currentDialogueIndex = dialogueIndex;
            currentLineIndex = 0;
            DisplayCurrentLine();
        }
    }

    private void CloseDialogue()
    {
        dialogueManager.CloseDialogue();
        inDialogue = false;
    }
}
