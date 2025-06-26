using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 GetMoveInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical).normalized;
    }

    public bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool IsJumping()
    {
        return Input.GetButtonDown("Jump");
    }
}