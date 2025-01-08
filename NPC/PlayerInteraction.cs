using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isInteractingWithNPC = false; // NPC와 상호작용 상태

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 예: E 키로 상호작용
        {
            if (isInteractingWithNPC)
            {
                // 대화 중이면 다음 문장 표시
                DialogueManager.Instance.DisplayNextSentence();
            }
            else
            {
                // NPC와 상호작용 시작
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
                if (hit.collider != null && hit.collider.GetComponent<NPC>() != null)
                {
                    hit.collider.GetComponent<NPC>().StartDialogue();
                    isInteractingWithNPC = true;
                }
            }
        }
    }

    public void EndInteraction()
    {
        isInteractingWithNPC = false; // 대화 종료 시 상호작용 상태 초기화
    }
}
