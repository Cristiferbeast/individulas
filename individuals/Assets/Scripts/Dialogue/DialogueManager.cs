using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public bool constantstate = false;

    private void Start()
    {
        dialogueUI.SetActive(constantstate);
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
