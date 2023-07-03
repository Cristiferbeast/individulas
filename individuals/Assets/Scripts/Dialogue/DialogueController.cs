using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    // External Variables
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Dialogue[] dialogues;
    public DialogueManager dialogueManager;
    public Transform optionsContainer; 
    public Button optionButtonPrefab; 

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

        // Clear existing options
        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }

        //Handle Options
        if (currentLine.options != null && currentLine.options.Length > 0)
        {
            for (int i = 0; i < currentLine.options.Length; i++)
            {
                int optionIndex = i; // Store the option index to avoid closure issues
                string optionText = currentLine.options[i];

                // Create a button for each option
                Button optionButtonObject = Instantiate(optionButtonPrefab, optionsContainer);
                Button optionButton = optionButtonObject.GetComponent<Button>();
                optionButton.GetComponentInChildren<TextMeshProUGUI>().text = optionText;
                optionButton.onClick.AddListener(() => OnOptionSelected(optionIndex));
            }
        }

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

    private void OnOptionSelected(int optionIndex)
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];
        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];
        if (optionIndex >= 0 && optionIndex < currentLine.options.Length)
        {
        // Perform actions or trigger events based on the selected option
        // You can also update variables or progress the dialogue based on the chosen option

        // Example: Progress to the next line after selecting an option
        currentLineIndex++;
        DisplayCurrentLine();
        }
    }
}
