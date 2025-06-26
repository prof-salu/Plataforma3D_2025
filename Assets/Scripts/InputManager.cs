using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector3 GetMoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //normalized = Retorna sempre valor 1
        return new Vector3(horizontal, 0, vertical).normalized;
    }
    
    public bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool IsJumping()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool IsAttacking()
    {
        //0 --> botao esquerdo
        //1 --> botao direito
        //2 --> botao meio
        //return Input.GetMouseButtonDown(0);
        return Input.GetButtonDown("Attack");
    }

    public bool IsChangeCamera()
    {
        return Input.GetKeyDown(KeyCode.C);
    }
}
