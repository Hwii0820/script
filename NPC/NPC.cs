using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueData dialogueData; // 대화 데이터 연결
    
    public void StartDialogue()
    {
        if (dialogueData != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueData.dialogue);
        }
    }
}
