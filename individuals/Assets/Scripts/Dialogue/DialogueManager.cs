using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;

    private void Start()
    {
        dialogueUI.SetActive(false);
    }

    public void OpenDialogue()
    {
        dialogueUI.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueUI.SetActive(false);
    }
}
