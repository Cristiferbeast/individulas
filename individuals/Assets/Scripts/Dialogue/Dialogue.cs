using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string line;
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}

