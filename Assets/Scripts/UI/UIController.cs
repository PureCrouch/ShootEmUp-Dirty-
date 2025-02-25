using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class UIController : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject resumeButton;
        [SerializeField] private GameObject finishButton;

        [SerializeField] private TMP_Text startText;

        [SerializeField] private GameObject worldObjects;

        [SerializeField] private GameStateController gameStateController;

        private IEnumerator StartCountdown()
        {
            startButton.SetActive(false);

            for (int i = 3; i > 0; i--)
            {
                startText.text = i.ToString(); 
                yield return new WaitForSeconds(1f); 
            }
            startText.text = " ";

            gameStateController.StartGame();

            Debug.Log("UI Start");
        }

        public void OnStartButtonClick()
        {
            Debug.Log("Start Pressed");
            StartCoroutine(StartCountdown());
        } 

        public void OnPauseButtonClick()
        {
            gameStateController.PauseGame();
        }

        public void OnResumeButtonClick()
        {
            gameStateController.ResumeGame();
        }
        public void StartGame()
        {
            worldObjects.SetActive(true);
            pauseButton.SetActive(true);
        }

        public void PauseGame()
        {
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
        }

        public void ResumeGame()
        {
            resumeButton.SetActive(false);
            pauseButton.SetActive(true);
        }

        public void FinishGame()
        {
            pauseButton.SetActive(false);
            finishButton.SetActive(true);
        }
    }
}

