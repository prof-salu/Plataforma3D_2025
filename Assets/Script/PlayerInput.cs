using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        Pausar();
    }
    public void Pausar()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrentState == GameState.Gameplay)
            {
                GameManager.Instance.PauseGame();
            }
        }
    }

    public Vector3 Mover()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector3(horizontal, 0f, vertical).normalized;
    }

    public bool EstaCorrendo()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool EstaPulando()
    {
        return Input.GetButtonDown("Jump");
    }

    public void BotaoResumeGame()
    {
        if (GameManager.Instance.CurrentState == GameState.Paused)
        {
            GameManager.Instance.ResumeGame();
        }
    }
}
