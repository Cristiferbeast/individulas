using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Button[] responseButtons;

    public Animator animator;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        if (sentences.Count == 0)
        {
            if (dialogue.responseOptions != null && dialogue.responseOptions.Length > 0)
            {
                ShowResponseOptions(dialogue);
            }
            else
            {
                EndDialogue();
            }
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void ShowResponseOptions(Dialogue dialogue)
    {
        for (int i = 0; i < responseButtons.Length; i++)
        {
            if (i < dialogue.responseOptions.Length)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<Text>().text = dialogue.responseOptions[i].responseText;
                int index = i;
                responseButtons[i].onClick.RemoveAllListeners();
                responseButtons[i].onClick.AddListener(() => OnResponseSelected(dialogue.responseOptions[index].nextDialogue));
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnResponseSelected(Dialogue nextDialogue)
    {
        foreach (var button in responseButtons)
        {
            button.gameObject.SetActive(false);
        }
        DisplayNextSentence(nextDialogue);
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
