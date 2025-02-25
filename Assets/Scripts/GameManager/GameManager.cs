using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {

        private void Awake()
        {
            PauseManager.SetPause();
        }

        public void StartGame()
        {
            PauseManager.SetUnpause();
            Debug.Log("Start");
        }

        public void PauseGame()
        {
            PauseManager.SetPause();
            Debug.Log("Pause");
        }

        public void ResumeGame()
        {
            PauseManager.SetUnpause();
            Debug.Log("Resume");
        }

        public void FinishGame()
        {
            Time.timeScale = 0;
            Debug.Log("Game over!");
        }

    }
}