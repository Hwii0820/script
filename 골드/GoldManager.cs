using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance; // Singleton 패턴

    [SerializeField]
    private int currentGold = 0; // 현재 소지 골드

    public int CurrentGold => currentGold; // 읽기 전용 프로퍼티

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true;
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
            return false;
        }
    }

    private void UpdateGoldUI()
    {
        // UIManager 또는 골드 UI를 갱신
        GoldUI.Instance.UpdateGoldDisplay(currentGold);
    }
}
