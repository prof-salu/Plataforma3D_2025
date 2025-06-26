using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Loading,
    Gameplay,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public MenuPause PauseMenu; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: manter o GameManager entre as cenas
            ChangeState(GameState.Gameplay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        Debug.Log($"Mudando de estado: {CurrentState} -> {newState}");
        CurrentState = newState;

        // Aqui você pode adicionar lógica específica para cada transição de estado
        switch (CurrentState)
        {
            case GameState.MainMenu:
                // Carregar UI do menu principal
                
                break;
            case GameState.Loading:
                // Iniciar carregamento de cena ou dados
                ChangeState(GameState.MainMenu);
                break;
            case GameState.Gameplay:
                // Ativar controles do jogador, iniciar a lógica do jogo
                Debug.Log("Começou!");
                PauseMenu.Resume();
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                // Mostrar menu de pausa, congelar a jogabilidade
                Time.timeScale = 0f; // Congela o tempo do jogo
                PauseMenu.Pausar();
                break;
            case GameState.GameOver:
                // Mostrar tela de game over, processar resultados
                break;
            default:
                break;
        }
    }

    // Uma função para ser chamada por outros scripts para pausar o jogo
    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    // Uma função para ser chamada para retornar ao gameplay
    public void ResumeGame()
    {
        ChangeState(GameState.Gameplay);
    }
}
