using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {

        private void Awake()
        {
            Time.timeScale = 1;
        }

        public void StartGame()
        {
            Debug.Log("Start");
            Time.timeScale = 1;
        }

        public void PauseGame()
        {
            Debug.Log("Pause");
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Debug.Log("Resume");
            Time.timeScale = 1;
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

    }
}