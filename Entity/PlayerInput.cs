using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement movement;
    

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        
    }
}
