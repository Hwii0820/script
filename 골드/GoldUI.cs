using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    public static GoldUI Instance; // Singleton 패턴

    [SerializeField]
    private TextMeshProUGUI goldText; // 골드 표시 텍스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateGoldDisplay(int goldAmount)
    {
        goldText.text = $"Gold: {goldAmount}";
    }
}
