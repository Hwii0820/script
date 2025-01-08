using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Text speakerNameText; // UI에 표시될 이름
    public Text dialogueText;    // UI에 표시될 대사
    public GameObject dialogueUI; // 대화 UI 오브젝트

    private Queue<string> sentences; // 남은 대사를 저장

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        sentences = new Queue<string>();

        // 대화창 초기 상태를 비활성화
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUI.SetActive(true);
        speakerNameText.text = dialogue.speakerName;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);

        // PlayerInteraction의 상호작용 종료 호출
        FindObjectOfType<PlayerInteraction>().EndInteraction();
    }
}
