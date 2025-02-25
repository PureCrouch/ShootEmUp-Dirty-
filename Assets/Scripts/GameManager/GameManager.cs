using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private void Start()
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            InputManager inputManager = FindObjectOfType<InputManager>();

            try
            {
                playerController.SetInputHandler(inputManager);
            }
            catch (NullReferenceException)
            {
                Debug.Log("No player controller / input manager found");

            }
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}