using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
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
                foreach (var listener in _gameStateListeners)
                {
                    if (listener is IStartGameListener l)
                    {
                        l.StartGame();
                    }
                }
            }
        }

        [ContextMenu("Pause")]
        public void PauseGame()
        {
            if (state == GameState.Playing)
            {
                state = GameState.Paused;
                foreach (var listener in _gameStateListeners)
                {
                    if (listener is IPauseGameListener l)
                    {
                        l.PauseGame();
                    }
                }
            }
        }

        [ContextMenu("Resume")]
        public void ResumeGame()
        {
            if (state == GameState.Paused)
            {
                state = GameState.Playing;
                foreach (var listener in _gameStateListeners)
                {
                    if (listener is IResumeGameListener l)
                    {
                        l.ResumeGame();
                    }
                }
            }
        }

        [ContextMenu("Finish")]
        public void FinishGame()
        {
            if (state == GameState.Paused || state == GameState.Playing)
            {
                state = GameState.Finished;
                foreach (var listener in _gameStateListeners)
                {
                    if (listener is IFinishGameListener l)
                    {
                        l.FinishGame();
                    }
                }
            }
        }
    }

}