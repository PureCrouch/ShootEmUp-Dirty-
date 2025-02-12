using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        Finished
    }

    public GameState state;

    private IGameStateListener[] _gameStateListeners;

    private void Awake()
    {
        _gameStateListeners = transform.parent.GetComponentsInChildren<IGameStateListener>();
    }

    [ContextMenu("Start")]
    public void StartGame()
    {
        if (state == GameState.MainMenu || state == GameState.Paused)
        {
            state = GameState.Playing;
        }
    }

    [ContextMenu("Pause")]
    public void PauseGame()
    {
        if (state == GameState.Playing)
        {
            state = GameState.Paused;
        }
    }

    [ContextMenu("Resume")]
    public void ResumeGame()
    {
        if (state == GameState.Paused)
        {
            state = GameState.Playing;
        }
    }

    [ContextMenu("Finish")]
    public void FinishGame()
    {
        if (state == GameState.Paused || state == GameState.Playing)
        {
            state = GameState.Finished;
        }
    }
}
