using UnityEngine;

public class PlayerStats : StatSystem
{
    protected override void Die()
    {
        Debug.Log("Player has died!");
        // 사망 처리 로직
    }
}
